using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeLoBusco.Controllers
{
    public class DenunciasController : Controller
    {
        // GET: Denuncias
        public ActionResult Denuncia()
        {
            return View();
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

        // GET: Denuncias/Edit/5
        public ActionResult Edit(int id)
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

                return RedirectToAction("Index");
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
