﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Utils
{
    public class Paginacao
    {
        public int? NumeroPagina { get; set; }

        public int? RegistroPorPagina { get; set; }

        public int TotalRegistros { get; set; }

        public int TotalPaginas { get; set; }
    }
}
