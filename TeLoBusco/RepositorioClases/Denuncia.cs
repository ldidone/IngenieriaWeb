using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioClases
{
    public class Denuncia
    {
        public int IdDenuncia { get; set; }
        public int IdPedido { get; set; }
        public int IdEstado { get; set; }
        public string DescripcionPedido { get; set; }
        public string Motivo { get; set; }
    }
}
