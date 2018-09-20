using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeLoBusco.Models;

namespace TeLoBusco.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            var listaUsuarios = Servicios.AspNetUsersServicio.obtenerTodos();
            return View(listaUsuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
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

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            var usuario = Servicios.AspNetUsersServicio.obtener(id);
            EditarUsuarioViewModel usuarioModelo = new EditarUsuarioViewModel();
            usuarioModelo.Id = usuario.Id;
            usuarioModelo.NombreApellido = usuario.NombreApellido;
            usuarioModelo.Email = usuario.Email;
            usuarioModelo.UserName = usuario.UserName;
            return View(usuarioModelo);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(EditarUsuarioViewModel usuario)
        {
            try
            {
                var Usuario = Servicios.AspNetUsersServicio.obtener(usuario.Id);
                var EmailNuevo = usuario.Email;
                bool mailRepetido = false;
                if (EmailNuevo != Usuario.Email)
                {
                    mailRepetido = Servicios.AspNetUsersServicio.existeEmail(EmailNuevo);
                }
                var UserNameNuevo = usuario.UserName;
                bool userNameRepetido = false;
                if (UserNameNuevo != Usuario.UserName)
                {
                    userNameRepetido = Servicios.AspNetUsersServicio.existeUserName(UserNameNuevo);
                }
                if (!mailRepetido)
                {
                    if(!userNameRepetido)
                    {
                        var Id = usuario.Id;
                        var NombreApellido = usuario.NombreApellido;                     
                        if (Servicios.AspNetUsersServicio.editar(Id, NombreApellido, EmailNuevo, UserNameNuevo))
                        {
                            return RedirectToAction("Index");
                        }                       
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "El UserName ingresado ya existe");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "El Email ingresado ya existe");
                }
                return View(usuario);
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
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
