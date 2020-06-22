using APIcomSQLITE.Models;
using APIcomSQLITE.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Utils
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<Palavra, PalavraDTO>();
            CreateMap<ListaPaginacao<Palavra>, ListaPaginacao<PalavraDTO>>();
        }
    }
}
