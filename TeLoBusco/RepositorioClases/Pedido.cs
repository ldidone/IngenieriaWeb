using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioClases
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string IdCliente { get; set; }
        public string IdDelivery { get; set; }
        public int NroCalleOrigen { get; set; }
        public string CalleOrigen { get; set; }
        public int IdLocalidadOrigen { get; set; }
        public int NroCalleDestino { get; set; }
        public string CalleDestino { get; set; }
        public int IdLocalidadDestino { get; set; }
        public decimal PrecioPedido { get; set; }
        public decimal PrecioTransporte { get; set; }
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
    }
}
