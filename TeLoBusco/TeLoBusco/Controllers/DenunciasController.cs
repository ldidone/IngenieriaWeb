using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeLoBusco.Models;
using Servicios;
using RepositorioClases;

namespace TeLoBusco.Controllers
{
    public class DenunciasController : Controller
    {
        // GET: Denuncias
        public ActionResult Denuncia(int id)
        {
            Datos.Pedidos pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(id);

            TeLoBusco.Models.DenunciasViewModel var = new TeLoBusco.Models.DenunciasViewModel() {IdPedido=id,DescripcionPedido=pedido.descripcion_pedido};


            return View(var);
        }

        // GET: Denuncias/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Denuncias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Denuncias/Create
        [HttpPost]
        public ActionResult Denunciar(DenunciasViewModel model)
        {
            try
            {            
                Denuncia var = new Denuncia();
                var.IdPedido = model.IdPedido;
                var.Motivo = model.Motivo;
                var.DescripcionPedido = model.DescripcionPedido;

                Servicios.AccesoDatos.DenunciasServicio.Crear(var);

                return View("DenunciaEnviada");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DenunciaEnviada()
        {
            return View();
        }

        // POST: Denuncias/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Home/Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Denuncias/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Denuncias/Delete/5
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
