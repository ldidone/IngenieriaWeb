using Servicios;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TeLoBusco.Controllers
{
    public class NotificacionesApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<NotificacionApi> Obtener(string emailUsuario)
        {
            var IdUsuario = AspNetUsersServicio.obtenerPorEmail(emailUsuario).Id;
            var listaNotificaciones = Servicios.AccesoDatos.NotificacionesServicio.ObtenerNotificacionesUsuarioApi(IdUsuario);
            return listaNotificaciones;
        }
    }
}
