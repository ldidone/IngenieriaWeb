using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioClases
{
    public class PostulacionApi
    {
        public string EmailUsuario { get; set; }
        public string JWT { get; set; }
        public int IdPedido { get; set; }
        public int Tiempo { get; set; }
        public decimal Precio { get; set; }
    }
}
