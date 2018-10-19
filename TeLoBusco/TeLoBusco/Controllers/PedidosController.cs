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
            ViewBag.Message = TempData["Message"];
            var listaPedidos = Servicios.AccesoDatos.PedidosServicio.ObtenerTodos();
            return View(listaPedidos);
        }

        public JObject ObtenerPedidosCeranos(double lat, double lng, int distancia)
        {
            Utilidades.ClasesAuxiliares.Coordenada posicionUsuario = new Utilidades.ClasesAuxiliares.Coordenada();
            if (lat != 0 && lng!= 0)
            {
                posicionUsuario.lat = lat;
                posicionUsuario.lng = lng;
            }
            var listaPedidos = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidosCercanos(posicionUsuario, distancia);

            //var json = JsonConvert.SerializeObject(listaPedidos, Formatting.Indented,
            //new JsonSerializerSettings
            //{
            //    PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //});
            var json = new JObject(new JProperty("Pedidos", JToken.FromObject(listaPedidos)));
            return json;
        }

        public ActionResult PedidosCliente()
        {
            var userName = User.Identity.Name;
            var IdCliente = Servicios.AspNetUsersServicio.obtenerIdUsuarioPorUserName(userName);
            var listaPedidos = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidosCliente(IdCliente);
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

                Pedido pedidoAlmacenar = new Pedido();
                pedidoAlmacenar.IdCliente = IdCliente;
                pedidoAlmacenar.NroCalleOrigen = pedido.nroDirOrigen;
                pedidoAlmacenar.CalleOrigen = pedido.calleOrigen;
                pedidoAlmacenar.IdLocalidadOrigen = pedido.idLocalidadOrigen;
                pedidoAlmacenar.NroCalleDestino = pedido.nroDirDestino;
                pedidoAlmacenar.CalleDestino = pedido.calleDestino;
                pedidoAlmacenar.IdLocalidadDestino = pedido.idLocalidadDestino;
                pedidoAlmacenar.PrecioPedido = pedido.precioPedido;
                pedidoAlmacenar.Descripcion = pedido.Descripcion;
                pedidoAlmacenar.Observaciones = pedido.Observaciones;

                if (Servicios.AccesoDatos.PedidosServicio.Crear(pedidoAlmacenar))

                //var userName = User.Identity.Name;
                //var IdCliente = Servicios.AspNetUsersServicio.obtenerIdUsuarioPorUserName(userName);
                //var nroDirOrigen = pedido.nroDirOrigen;
                //var calleOrigen = pedido.calleOrigen;
                //var idLocalidadOrigen = pedido.idLocalidadOrigen;
                //var nroDirDestino = pedido.nroDirDestino;
                //var calleDestino = pedido.calleDestino;
                //var idLocalidadDestino = pedido.idLocalidadDestino;
                //var precioPedido = pedido.precioPedido;
                //if (Servicios.AccesoDatos.PedidosServicio.crear(IdCliente, nroDirOrigen, calleOrigen, idLocalidadOrigen,
                //                                                nroDirDestino, calleDestino, idLocalidadDestino, precioPedido))
                {
                    TempData["Message"] = "El pedido se publicó correctamente";
                    return RedirectToAction("Index");
                }
                ViewBag.Localidades = new SelectList(Servicios.AccesoDatos.LocalidadesServicio.obtenerTodas(), "idLocalidad", "Nombre");
                return View(pedido);
            }
            catch
            {
                ViewBag.Localidades = new SelectList(Servicios.AccesoDatos.LocalidadesServicio.obtenerTodas(), "idLocalidad", "Nombre");
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

        public ActionResult Postularse(int idPedido)
        {
            var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(idPedido);
            PostulacionesViewModel postulacion = new PostulacionesViewModel();
            postulacion.IdPedido = idPedido;
            if (pedido != null)
            {              
                postulacion.pedidoDetalles = Servicios.AccesoDatos.PedidosServicio.ConvertirPedidoAPedidoMapa(pedido);
                Utilidades.ClasesAuxiliares.Coordenada coordenadaOrigen = new Utilidades.ClasesAuxiliares.Coordenada
                {
                    lat = Convert.ToDouble(pedido.lat_origen),
                    lng = Convert.ToDouble(pedido.lng_origen)
                };

                Utilidades.ClasesAuxiliares.Coordenada coordenadaDestino = new Utilidades.ClasesAuxiliares.Coordenada
                {
                    lat = Convert.ToDouble(pedido.lat_destino),
                    lng = Convert.ToDouble(pedido.lng_destino)
                };
                postulacion.pedidoDetalles.Distancia = Math.Round(Utilidades.Comunes.DistanciaEntreDosPuntosEnKM(coordenadaOrigen, coordenadaDestino), 2);

                var precio = Utilidades.Comunes.ObtenerRangoPrecios(coordenadaOrigen, coordenadaDestino);
                postulacion.precioMinimo = precio.PrecioMinimo;
                postulacion.precioMaximo = precio.PrecioMaximo;
            }

            return View(postulacion);
        }

        [HttpPost]
        public ActionResult Postularse(PostulacionesViewModel postulacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool precioValido = Servicios.AccesoDatos.PedidosServicio.ValidarPrecio(postulacion.precioMinimo, postulacion.Precio, postulacion.precioMaximo);
                    if (precioValido)
                    {
                        var userName = User.Identity.Name;
                        var IdUsuarioPostulado = Servicios.AspNetUsersServicio.obtenerIdUsuarioPorUserName(userName);
                        var idDueñoPedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(postulacion.IdPedido).idCliente;
                        if (IdUsuarioPostulado != idDueñoPedido)
                        {
                            Postulacion postulacionAlmacenar = new Postulacion()
                            {
                                IdPedido = postulacion.IdPedido,
                                IdUsuarioPostulado = IdUsuarioPostulado,
                                TiempoEstimado = postulacion.TiempoEstimado,
                                Precio = postulacion.Precio
                            };
                            if (Servicios.AccesoDatos.PostulacionesServicio.Crear(postulacionAlmacenar))
                            {
                                TempData["Message"] = "La postulación fue exitosa.";
                                
                            }
                            else
                            {
                                TempData["Message"] = "No ha sido posible efectuar la postulación. Vuelva a intentar más tarde.";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "El cliente y el delivery no pueden ser la misma persona";
                        }
                        return RedirectToAction("Pedidos"); //Redireccionar a vista de pedidos cercanos                  
                    }
                    else
                    {
                        ModelState.AddModelError("Precio", "El precio ingresado no se encuentra en el rango aceptado.");
                    }
                }
                var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(postulacion.IdPedido);
                if (pedido != null)
                {
                    postulacion.pedidoDetalles = Servicios.AccesoDatos.PedidosServicio.ConvertirPedidoAPedidoMapa(pedido);
                }
                return View(postulacion);
            }
            catch
            {
                var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(postulacion.IdPedido);
                if (pedido != null)
                {
                    postulacion.pedidoDetalles = Servicios.AccesoDatos.PedidosServicio.ConvertirPedidoAPedidoMapa(pedido);

                }
                return View(postulacion);
            }
        }
     }
}
