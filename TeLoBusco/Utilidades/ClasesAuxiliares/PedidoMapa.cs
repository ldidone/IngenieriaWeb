using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.ClasesAuxiliares
{
    public class PedidoMapa
    {
        public string NombreCliente { get; set; }
        public string DescripcionPedido { get; set; }
        public string ObservacionesPedido { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public decimal Precio { get; set; }
        public double latOrigen { get; set; }
        public double lngOrigen { get; set; }
    }
}
