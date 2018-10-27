using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeLoBusco.Models;
namespace TeLoBusco.Controllers
{
    public class ValoracionesController : Controller
    {
        // GET: Valraciones
        

        // GET: Valraciones/Details/5
        public ActionResult Guardar(ValoracionesViewModel valoracion)
        {
            try
            {
                Valoracion puntaje = new Valoracion
                {
                    IdValoracion = valoracion.IdValoracion,
                    IdCliente = valoracion.IdCliente,
                    IdDelivery = valoracion.IdCliente,
                    Puntuacion = valoracion.Puntuacion,
                    Comentario = valoracion.Comentario
                };
                if (Servicios.AccesoDatos.ValoracionesServicios.Crear(puntaje))
                {

                    return View();
                }
               
            }
            catch{
                
            }
            return View();
        }

        public ActionResult ValorarCliente(int idPedido)
        {
            var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(idPedido);
            
            ViewBag.Delivery = true;
            ValoracionesViewModel model = new ValoracionesViewModel
            {
                IdValoracion = 1,
                IdCliente = pedido.idCliente,
                IdDelivery = pedido.idDelivery,
                IdPedido = pedido.IdPedido
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ValorarCliente(ValoracionesViewModel valoracion)
        {
            try
            {
                Valoracion val = new Valoracion
                {
                    IdCliente = valoracion.IdCliente,
                    IdDelivery = valoracion.IdDelivery,
                    Puntuacion = valoracion.Puntuacion,
                    Comentario = valoracion.Comentario,
                    IdPedido = valoracion.IdPedido
                };
                if (Servicios.AccesoDatos.ValoracionesServicios.CrearValoracionCliente(val))
                {
                    TempData["Message"] = "¡Gracias por tu colaboración!";
                }
                else
                {
                    TempData["Message"] = "Ups. Tu pedido no pudo ser finalizado.";
                };
                return RedirectToAction("PedidosAsignados", "Pedidos");
            }
            catch
            {
                return View();
            }
        }

        /*Valorar delivery*/
        [HttpPost]
        public ActionResult Valorar(ValoracionesViewModel valoracion)
        {
            try
            {
                Valoracion val = new Valoracion
                {
                    IdValoracion = 1,
                    IdCliente = valoracion.IdCliente,
                    IdDelivery = valoracion.IdDelivery,
                    Puntuacion = valoracion.Puntuacion,
                    Comentario = valoracion.Comentario,
                    IdPedido = valoracion.IdPedido
                };
                if (Servicios.AccesoDatos.ValoracionesServicios.Crear(val))
                {
                    TempData["Message"] = "Gracias por tu colaboracion!";
                }
                else
                {
                    TempData["Message"] = "Ups";
                };
                return RedirectToAction("PedidosCliente");
            }
            catch
            {
                return View();
            }         
        }

        // GET: Valraciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Valraciones/Create
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

        // GET: Valraciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Valraciones/Edit/5
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

        // GET: Valraciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Valraciones/Delete/5
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
    }
}
