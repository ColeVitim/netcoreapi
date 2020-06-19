using APIcomSQLITE.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Controllers
{
    public class PalavrasController : ControllerBase
    {
        private readonly BancoContext _banco;

        public PalavrasController(BancoContext banco)
        {
            _banco = banco;
        }

        public ActionResult ObterTdas()
        {
            return Ok(_banco.palavras);
        }


    }
}
