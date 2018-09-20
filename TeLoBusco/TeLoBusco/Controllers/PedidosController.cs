using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeLoBusco.Models;

namespace TeLoBusco.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Administrador")]
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Pedidos()
        {
            var listaPedidos = Servicios.AccesoDatos.PedidosServicio.obtenerTodos();
            return View(listaPedidos);
        }

        public ActionResult PedidosCliente()
        {
            var userName = User.Identity.Name;
            var IdCliente = Servicios.AspNetUsersServicio.obtenerIdUsuarioPorUserName(userName);
            var listaPedidos = Servicios.AccesoDatos.PedidosServicio.obtenerPedidosCliente(IdCliente);
            return View(listaPedidos);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.Localidades = new SelectList(Servicios.AccesoDatos.LocalidadesServicio.obtenerTodas(), "idLocalidad", "Nombre");       
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        public ActionResult Create(PedidosViewModel pedido)
        {
            try
            {
                var userName = User.Identity.Name;
                var IdCliente = Servicios.AspNetUsersServicio.obtenerIdUsuarioPorUserName(userName);
                var nroDirOrigen = pedido.nroDirOrigen;
                var calleOrigen = pedido.calleOrigen;
                var idLocalidadOrigen = pedido.idLocalidadOrigen;
                var nroDirDestino = pedido.nroDirDestino;
                var calleDestino = pedido.calleDestino;
                var idLocalidadDestino = pedido.idLocalidadDestino;
                var precioPedido = pedido.precioPedido;
                if (Servicios.AccesoDatos.PedidosServicio.crear(IdCliente, nroDirOrigen, calleOrigen, idLocalidadOrigen,
                                                                nroDirDestino, calleDestino, idLocalidadDestino, precioPedido))
                {
                    TempData["Message"] = "El pedido se publicó correctamente";
                    return RedirectToAction("Index");
                }
                return View(pedido);
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedidos/Edit/5
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

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedidos/Delete/5
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
