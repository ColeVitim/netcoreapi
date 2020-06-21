using APIcomSQLITE.DB;
using APIcomSQLITE.Models;
using APIcomSQLITE.ParamQuery;
using APIcomSQLITE.Repositories.Interface;
using APIcomSQLITE.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Repositories
{
    public class PalavraRepository : IPalavraRepository
    {
        readonly BancoContext _banco;
        public PalavraRepository(BancoContext banco)
        {
            _banco = banco;
        }
        public void Atualizar(Palavra palavra)
        {
            palavra.Atualizado = DateTime.Now;
            _banco.palavras.Update(palavra);
            _banco.SaveChanges();
        }

        public void Cadastrar(Palavra palavra)
        {
            _banco.palavras.Add(palavra);
            _banco.SaveChanges();
        }

        public void Deletar(int id)
        {
            var palavra = Obter(id);
            palavra.Ativo = false;
            _banco.palavras.Update(palavra);
            _banco.SaveChanges();
        }

        public Palavra Obter(int id)
        {
            return _banco.palavras.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public ListaPaginacao<Palavra> ObterTodas(PalavraUrlParams query)
        {
            var lista = new ListaPaginacao<Palavra>();
            var itens = _banco.palavras.AsNoTracking().AsQueryable();
            if (query.DataFiltro.HasValue)
                itens = itens.Where(x => x.Criado >= query.DataFiltro.Value);

            if (query.PagNumero.HasValue)
            {
                var qtditens = itens.Count();
                itens = itens.Skip((query.PagNumero.Value - 1) * query.RegistroPorPag.Value).Take(query.RegistroPorPag.Value);

                Paginacao pagina = new Paginacao();
                pagina.NumeroPagina = query.PagNumero.Value;
                pagina.RegistroPorPagina = query.RegistroPorPag.Value;
                pagina.TotalRegistros = qtditens;
                pagina.TotalPaginas = (int)Math.Ceiling((double)qtditens / query.RegistroPorPag.Value);

                lista.paginacao = pagina;
            }

            lista.AddRange(itens.ToList());
            return lista;
        }
    }
}
