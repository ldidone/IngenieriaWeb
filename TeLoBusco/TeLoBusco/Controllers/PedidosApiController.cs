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
        //[HttpGet]
        //public IEnumerable<Datos.Pedidos> GetAllPedidos()
        //{
        //    List<Datos.Pedidos> Pedidos = Servicios.AccesoDatos.PedidosServicio.ObtenerTodos();
        //    return Pedidos;
        //}

        [HttpGet]
        public IEnumerable<PedidoMapa> ObtenerPendientes()
        {
            var pedidosPendientes = PedidosServicio.ObtenerPendientesApi();
            return pedidosPendientes;
        }

        [HttpGet]
        public bool Prueba()
        {
            return true;
        }
    }
}
