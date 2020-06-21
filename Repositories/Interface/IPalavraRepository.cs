using APIcomSQLITE.Models;
using APIcomSQLITE.ParamQuery;
using APIcomSQLITE.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Repositories.Interface
{
    public interface IPalavraRepository
    {
        ListaPaginacao<Palavra> ObterTodas(PalavraUrlParams query);
        Palavra Obter(int id);
        void Cadastrar(Palavra palavra);
        void Atualizar(Palavra palavra);
        void Deletar(int id);
    }
}
