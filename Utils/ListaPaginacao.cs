using APIcomSQLITE.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Utils
{
    public class ListaPaginacao<T> 
    {
        public List<T> Resultados { get; set; } = new List<T>();
        public Paginacao paginacao { get; set; }
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();

    }
}
