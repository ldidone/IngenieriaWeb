using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioClases
{
    public class Postulacion
    {
        public int IdTipoActividad { get; set; }
        public int IdPedido { get; set; }
        public string IdUsuarioPostulado { get; set; }
        public int IdEstadoPostulacion { get; set; }
        public double TiempoEstimado { get; set; }
        public decimal Precio { get; set; }
    }
}
