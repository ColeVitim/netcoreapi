using APIcomSQLITE.DB;
using APIcomSQLITE.Models;
using APIcomSQLITE.Models.DTO;
using APIcomSQLITE.ParamQuery;
using APIcomSQLITE.Repositories.Interface;
using APIcomSQLITE.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly IMapper _mapper;


        public PalavrasController(IPalavraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("",Name = "ObterTodas")]
        public ActionResult ObterTodas([FromQuery] PalavraUrlParams query)
        {
            var itens = _repository.ObterTodas(query);

            if (itens.Resultados.Count == 0)
                return NotFound();

            ListaPaginacao<PalavraDTO> lista = CriarLinks(query, itens);
            return Ok(lista);
        }

        private ListaPaginacao<PalavraDTO> CriarLinks(PalavraUrlParams query, ListaPaginacao<Palavra> itens)
        {
            var lista = _mapper.Map<ListaPaginacao<Palavra>, ListaPaginacao<PalavraDTO>>(itens);

            foreach (var palavra in lista.Resultados)
            {
                palavra.Links = new List<LinkDTO>();
                palavra.Links.Add(
                    new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavra.Id }), "GET")
                    );
            }
            if (itens.paginacao != null)
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(itens.paginacao));

            lista.Links.Add(new LinkDTO("self", Url.Link("ObterTodas", query), "GET"));

            if (query.PagNumero.HasValue && query.PagNumero.Value + 1 <= itens.paginacao.TotalPaginas)
            {
                var querytrig = new PalavraUrlParams() { PagNumero = query.PagNumero + 1, RegistroPorPag = query.RegistroPorPag, DataFiltro = query.DataFiltro };
                lista.Links.Add(new LinkDTO("proximo", Url.Link("ObterTodas", querytrig), "GET"));
            }
            if (query.PagNumero.HasValue && query.PagNumero.Value - 1 > 0)
            {
                var querytrig = new PalavraUrlParams() { PagNumero = query.PagNumero - 1, RegistroPorPag = query.RegistroPorPag, DataFiltro = query.DataFiltro };
                lista.Links.Add(new LinkDTO("Anterior", Url.Link("ObterTodas", querytrig), "GET"));
            }

            return lista;
        }

        [HttpGet("{id}", Name = "ObterPalavra")]
        public ActionResult Obter(int id)
        {
            var palavra = _repository.Obter(id);
            if (palavra == null)
                return NotFound();

            PalavraDTO palavraDTO = _mapper.Map<Palavra, PalavraDTO>(palavra);
            palavraDTO.Links.Add(
                new LinkDTO("self",Url.Link("ObterPalavra", new { id = palavraDTO.Id}),"GET")
                );
            palavraDTO.Links.Add(
                new LinkDTO("update", Url.Link("AtualizarPalavra", new { id = palavraDTO.Id }), "PUT")
                );
            palavraDTO.Links.Add(
                new LinkDTO("delete", Url.Link("DeletarPalavra", new { id = palavraDTO.Id }), "DELETE")
                );


            return Ok(palavraDTO);
        }
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            if (palavra == null)
                return BadRequest();
            if (ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _repository.Cadastrar(palavra);

            var palavraDTO = _mapper.Map<Palavra, PalavraDTO>(palavra);
            palavraDTO.Links.Add(
                   new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDTO.Id }), "GET")
                   );
            return Created($"api/palavra/{palavra.Id}",palavra);
        }

        [HttpPut("{id}",Name = "AtualizarPalavra")]
        public ActionResult Atualizar(int id,[FromBody] Palavra palavra)
        {
            if (palavra == null)
                return BadRequest();
            if (ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var palavraFind = _repository.Obter(id);

            if (palavraFind == null)
                return NotFound();

            var palavraDTO = _mapper.Map<Palavra, PalavraDTO>(palavra);
            palavraDTO.Links.Add(
                   new LinkDTO("self", Url.Link("ObterPalavra", new { id = palavraDTO.Id }), "GET")
                   );

            palavra.Id = id;          
            _repository.Atualizar(palavra);

            return Ok();
        }
 
        [HttpDelete("{id}",Name = "DeletarPalavra")]
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
