using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Utils
{
    public class ListaPaginacao<T> : List<T>
    {
        public Paginacao paginacao { get; set; }

    }
}
