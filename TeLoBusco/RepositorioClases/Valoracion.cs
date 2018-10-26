using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioClases
{
    public class Valoracion
    {
        public int IdValoracion { get; set; }
        public string IdCliente { get; set; }
        public string IdDelivery { get; set; }
        public decimal Puntuacion { get; set; }
        public string Comentario { get; set; }
        public int IdPedido { get; set; }
        
    }
}
