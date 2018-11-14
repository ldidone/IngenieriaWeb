using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Servicios;
using System.Web.Http;
using Utilidades.ClasesAuxiliares;
using Servicios.AccesoDatos;

namespace TeLoBusco.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [RoutePrefix("api/pedidos")]
    public class PedidosApiController : ApiController
    {
        [HttpGet]
        [Route("obtenerpendientes")]
        public IEnumerable<PedidoMapa> ObtenerPendientes(string emailDelivery, string JWT)
        {
            var user = AspNetUsersServicio.obtenerPorEmail(emailDelivery);
            if (user != null)
            {
                if (user.JWT == JWT)
                {
                    var pedidosPendientes = PedidosServicio.ObtenerPendientesApi();
                    return pedidosPendientes;
                }              
            }
            List<PedidoMapa> listaVacia = new List<PedidoMapa>();
            return listaVacia;
        }

        [HttpGet]
        [Route("obtenerasignados")]
        public IEnumerable<PedidoMapa> ObtenerAsignados(string emailDelivery, string JWT)
        {
            var user = AspNetUsersServicio.obtenerPorEmail(emailDelivery);
            if (user != null)
            {
                if (user.JWT == JWT)
                {
                    var IdDelivery = user.Id;
                    var pedidosAsignados = PedidosServicio.ObtenerPedidosAsignadosApi(IdDelivery);
                    return pedidosAsignados;
                }                    
            }
            List<PedidoMapa> listaVacia = new List<PedidoMapa>();
            return listaVacia;
        }
    }
}
