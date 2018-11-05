﻿using System;
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
    public class PedidosApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<PedidoMapa> ObtenerPendientes()
        {
            var pedidosPendientes = PedidosServicio.ObtenerPendientesApi();
            return pedidosPendientes;
        }

        [HttpGet]
        public IEnumerable<PedidoMapa> ObtenerAsignados(string emailDelivery)
        {
            var IdDelivery = AspNetUsersServicio.obtenerIdPorEmail(emailDelivery);
            var pedidosAsignados = PedidosServicio.ObtenerPedidosAsignadosApi(IdDelivery);
            return pedidosAsignados;
        }
    }
}
