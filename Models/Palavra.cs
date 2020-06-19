using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIcomSQLITE.Models
{
    public class Palavra
    {
        public int id { get; set; }

        public string nome { get; set; }

        public int pontuacao { get; set; }

        public bool ativo { get; set; }

        public DateTime criado { get; set; }

        public DateTime? atualizado { get; set; }

    }
}
