using APIcomSQLITE.DB;
using APIcomSQLITE.Models;
using APIcomSQLITE.ParamQuery;
using APIcomSQLITE.Repositories.Interface;
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
        private readonly IPalavraRepository _repository;


        public PalavrasController(IPalavraRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas([FromQuery] PalavraUrlParams query)
        {

            var itens = _repository.ObterTodas(query);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(itens.paginacao));

                if(query.PagNumero.Value > itens.paginacao.TotalPaginas)
                {
                    return NotFound();
                }
            

            return Ok(itens);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var palavra = _repository.Obter(id);
            if (palavra == null)
                return NotFound();

            return Ok(palavra);
        }
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _repository.Cadastrar(palavra);
            return Created($"api/palavra/{palavra.Id}",palavra);
        }
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id,[FromBody] Palavra palavra)
        {
            var palavraFind = _repository.Obter(id);

            if (palavraFind == null)
                return NotFound();

            palavra.Id = id;          
            _repository.Atualizar(palavra);

            return Ok();
        }
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _repository.Obter(id);

            if (palavra == null)
                return NotFound();

            _repository.Deletar(id);

          
          
            return NoContent();
        }

    }
}
