using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeLoBusco.Models;

namespace TeLoBusco.Controllers
{
    public class NotificacionesController : Controller
    {
        // GET: Notificaciones
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var IdCliente = Servicios.AspNetUsersServicio.obtenerIdUsuarioPorUserName(userName);
            var listaNotificaciones = Servicios.AccesoDatos.NotificacionesServicio.ObtenerTodasNotificacionesUsuario(IdCliente);
            return View(listaNotificaciones);
        }

        // GET: Notificaciones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notificaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notificaciones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notificaciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notificaciones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notificaciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notificaciones/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JObject ObtenerNotificacionesUsuario()
        {
            var userName = User.Identity.Name;
            var IdCliente = Servicios.AspNetUsersServicio.obtenerIdUsuarioPorUserName(userName);
            if (IdCliente != null)
            {
                var listaNotificaciones = Servicios.AccesoDatos.NotificacionesServicio.ObtenerNotificacionesUsuario(IdCliente);
                var json = new JObject(new JProperty("Notificaciones", JToken.FromObject(listaNotificaciones)));
                return json;
            }

            string response = @"{
              Message: 'Nulo'         
            }";
            JObject o = JObject.Parse(response);
            return new JObject(o);
        }

        //Postulacion
        public ActionResult Notificacion(int idActividad, int idTipoActividad, int idNotificacion)
        {
            string tipoActividad = Servicios.AccesoDatos.TiposActividadesServicio.obtenerDescripcionPorId(idTipoActividad);

            //Marcar notificación como vista:
            Servicios.AccesoDatos.NotificacionesServicio.MarcarNotificacionComoVisto(idNotificacion);

            switch (tipoActividad)
            {
                case "Pedido":
                    /*Redireccionar a donde corresponda*/
                    return RedirectToAction("Index", "Pedidos");
                case "Mensaje":
                    /*Redireccionar a donde corresponda*/
                    return RedirectToAction("Index", "Pedidos");
                case "Valoración":
                    /*Redireccionar a donde corresponda*/
                    return RedirectToAction("Index", "Pedidos");
                case "Denuncia":
                    /*Redireccionar a donde corresponda*/
                    return RedirectToAction("Index", "Pedidos");
                case "Postulación":
                    /*Redireccionar a donde corresponda*/
                    return RedirectToAction("Postulacion", "Postulaciones", routeValues: new { idPostulacion = idActividad });
                case "Postulación Aceptada":
                    int idPedido = Servicios.AccesoDatos.PostulacionesServicio.Obtener(idActividad).IdPedido;
                    return RedirectToAction("SeguimientoPedido", "Pedidos", routeValues: new { id = idPedido, seguimiento  = false});
                case "Postulación Rechazada":
                    return RedirectToAction("PostulacionRechazada", "Postulaciones", routeValues: new { idPostulacion = idActividad });
                default:
                    return RedirectToAction("Index", "Pedidos");
            }
        }
    }
}
