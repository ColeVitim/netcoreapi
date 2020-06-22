using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Models.DTO
{
    public class LinkDTO
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Metodo { get; set; } 

        public LinkDTO(string rel,string href, string metodo)
        {
            Rel = rel;
            Href = href;
            Metodo = metodo;
        }
    }
}
