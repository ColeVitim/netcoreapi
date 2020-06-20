using APIcomSQLITE.DB;
using APIcomSQLITE.Models;
using APIcomSQLITE.ParamQuery;
using APIcomSQLITE.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIcomSQLITE.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly BancoContext _banco;


        public PalavrasController(BancoContext banco)
        {
            _banco = banco;
        }

        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas([FromQuery] PalavraUrlParams query)
        {
            var itens = _banco.palavras.AsQueryable();
            if (query.DataFiltro.HasValue)
                itens = itens.Where(x => x.Criado >= query.DataFiltro.Value);

            if(query.PagNumero.HasValue)
            {
                var qtditens = itens.Count();
                itens = itens.Skip((query.PagNumero.Value - 1) * query.RegistroPorPag.Value).Take(query.RegistroPorPag.Value);

                Paginacao pagina = new Paginacao();
                pagina.NumeroPagina = query.PagNumero.Value;
                pagina.RegistroPorPagina = query.RegistroPorPag.Value;
                pagina.TotalRegistros = qtditens;
                pagina.TotalPaginas = (int)Math.Ceiling((double)qtditens / query.RegistroPorPag.Value);


                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagina));

                if(query.PagNumero.Value > pagina.TotalPaginas)
                {
                    return NotFound();
                }
            }

            return Ok(itens);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var palavra = _banco.palavras.Find(id);
            if (palavra == null)
                return NotFound();

            return Ok();
        }
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _banco.palavras.Add(palavra);
            _banco.SaveChanges();
            return Created($"api/palavra/{palavra.Id}",palavra);
        }
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id,[FromBody] Palavra palavra)
        {
            var palavraFind = _banco.palavras.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (palavraFind == null)
                return NotFound();

            palavra.Id = id;
            palavra.Atualizado = DateTime.Now;
            _banco.palavras.Update(palavra);
            _banco.SaveChanges();

            return Ok();
        }
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _banco.palavras.Find(id);

            if (palavra == null)
                return NotFound();

            palavra.Ativo = false;
            _banco.palavras.Update(palavra);
            _banco.SaveChanges();
          
            return NoContent();
        }

    }
}
