using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilidades.ClasesAuxiliares;
using Servicios.AccesoDatos;

namespace TeLoBusco.Controllers
{
    public class PedidosApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<PedidoMapa> ObtenerPendientes()
        {
            var pedidosPendientes = PedidosServicio.ObtenerPendientesApi();
            return pedidosPendientes;
        }
    }
}
