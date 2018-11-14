using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeLoBusco.Models;

namespace TeLoBusco.Controllers
{
    public class PostulacionesController : Controller
    {
        // GET: Postulaciones
        public ActionResult Index()
        {
            return View();
        }

        // GET: Postulaciones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Postulaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Postulaciones/Create
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

        // GET: Postulaciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Postulaciones/Edit/5
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

        // GET: Postulaciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Postulaciones/Delete/5
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

        public ActionResult Postulacion(int idPostulacion)
        {
            Datos.Postulaciones postulacion = Servicios.AccesoDatos.PostulacionesServicio.Obtener(idPostulacion);

            /*Obtiene el pedido de la base de datos y lo transforma en un objeto de la clase necesaria (PedidoMapa)*/
            var pedidoDetalles = Servicios.AccesoDatos.PedidosServicio.ConvertirPedidoAPedidoMapa(Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(postulacion.IdPedido));
            var valoracion = Servicios.AccesoDatos.ValoracionesServicios.ObtenerPuntaje(postulacion.IdUsuarioPostulado);
            PostulacionesViewModel postulacionesViewModel = new PostulacionesViewModel()
            {
                TiempoEstimado = postulacion.TiempoEstimado,
                Precio = postulacion.Precio,
                IdPedido = postulacion.IdPedido,
                IdPostulacion = postulacion.IdPostulacion,
                pedidoDetalles = pedidoDetalles,
                IdPostulado = postulacion.IdUsuarioPostulado,
                PostuladoNombreApellido = postulacion.AspNetUsers.NombreApellido,
                ValoracionDelivery = valoracion,
                Estado = postulacion.Estados.Descripcion
            };

            return View(postulacionesViewModel);
        }

        [HttpPost]
        public ActionResult Postulacion(PostulacionesViewModel postulacion)
        {
            int idPostulacion = postulacion.IdPostulacion;
            if (Servicios.AccesoDatos.PostulacionesServicio.Aceptar(idPostulacion))
            {
                TempData["Message"] = "Se ha aceptado la postulación correctamente";
            }
            else
            {
                TempData["Message"] = "No se ha podido aceptar la postulación.";
            }
            return RedirectToAction("Index", "Pedidos");
        }

        public ActionResult PostulacionAceptada()
        {
            return View();
        }

        public ActionResult PostulacionRechazada(int idPostulacion)
        {
            var idPedido = Servicios.AccesoDatos.PostulacionesServicio.Obtener(idPostulacion).IdPedido;
            var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(idPedido);
            return View(pedido);
        }
    }
}
