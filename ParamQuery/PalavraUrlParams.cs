using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.ParamQuery
{
    public class PalavraUrlParams
    {
        public DateTime? DataFiltro { get; set; }
        public int? PagNumero { get; set; }
        public int? RegistroPorPag { get; set; }
    }
}
