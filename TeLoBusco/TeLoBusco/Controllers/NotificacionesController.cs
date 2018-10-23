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
            return View();
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

        public ActionResult Notificacion(int idActividad, int idTipoActividad)
        {
            //Momentaneamente va a funcionar solo con "Postulaciones" ya que idActividadSera una postulacion->Postulaciones vinculado con pedidos por clave foránea
            string tipoActividad = Servicios.AccesoDatos.TiposActividadesServicio.obtenerDescripcionPorId(idTipoActividad);
            Datos.Postulaciones postulacion = new Datos.Postulaciones();
            switch (tipoActividad)
            {
                case "Pedido":
                    //Logica necesaria
                    break;
                case "Mensaje":
                    //Logica necesaria
                    break;
                case "Valoración":
                    //Logica necesaria
                    break;
                case "Denuncia":
                    //Logica necesaria
                    break;
                case "Postulación":
                    postulacion = Servicios.AccesoDatos.PostulacionesServicio.Obtener(idActividad);
                    break;
            }

            /*Obtiene el pedido de la base de datos y lo transforma en un objeto de la clase necesaria (PedidoMapa)*/
            var pedidoDetalles = Servicios.AccesoDatos.PedidosServicio.ConvertirPedidoAPedidoMapa(Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(postulacion.IdPedido));

            PostulacionesViewModel postulacionesViewModel = new PostulacionesViewModel()
            {
                TiempoEstimado = postulacion.TiempoEstimado,
                Precio = postulacion.Precio,
                IdPedido = postulacion.IdPedido,
                pedidoDetalles = pedidoDetalles,
                IdPostulado = postulacion.IdUsuarioPostulado,
                PostuladoNombreApellido = postulacion.AspNetUsers.NombreApellido 
            };

            return View(postulacionesViewModel);
        }
    }
}
