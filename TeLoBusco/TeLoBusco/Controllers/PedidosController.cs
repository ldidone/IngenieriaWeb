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
            return View();
        }

        public JObject ObtenerPedidosCeranos(double lat, double lng, int distancia)
        {
            Utilidades.ClasesAuxiliares.Coordenada posicionUsuario = new Utilidades.ClasesAuxiliares.Coordenada();
            if (lat != 0 && lng != 0)
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
            ViewBag.Message = TempData["Message"];
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
            ViewBag.Localidades = new SelectList(Servicios.AccesoDatos.LocalidadesServicio.obtenerTodas(), "idLocalidad", "Nombre");
            var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(id);
            PedidosViewModel pedidoVIewModel = new PedidosViewModel()
            {
                IdPedido = pedido.IdPedido,
                nroDirOrigen = pedido.nro_calle_origen,
                calleOrigen = pedido.calle_origen,
                idLocalidadOrigen = pedido.id_localidad_origen,
                nroDirDestino = pedido.nro_calle_destino,
                calleDestino = pedido.calle_destino,
                idLocalidadDestino = pedido.id_localidad_destino,
                precioPedido = pedido.precio_predido,
                Descripcion = pedido.descripcion_pedido,
                Observaciones = pedido.observaciones_pedido
            };
            return View(pedidoVIewModel);
        }
        //GET
        [HttpGet]
        public ActionResult Valorar(int id)
        {
            
            var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(id);
            if (pedido.idDelivery != null)
            {
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
            else
            {
                ViewBag.Delivery = false;
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Valorar(ValoracionesViewModel valoracion)
        {
            try
            {
                var pedido = Servicios.AccesoDatos.PedidosServicio.ObtenerPedidoPorId(valoracion.IdPedido);
                Valoracion val = new Valoracion
                {
                    IdCliente = pedido.idCliente,
                    IdDelivery = pedido.idDelivery,
                    Puntuacion = valoracion.Puntuacion,
                    Comentario = valoracion.Comentario,
                    IdPedido = valoracion.IdPedido
                };
                if (Servicios.AccesoDatos.ValoracionesServicios.Crear(val))
                {
                    TempData["Message"] = "¡Gracias por tu colaboración!";
                }
                else
                {
                    TempData["Message"] = "Ups. Tu pedido no pudo ser finalizado.";
                };
                return RedirectToAction("PedidosCliente");
            }
            catch
            {

            }
            return View();

        }
        // POST: Pedidos/Edit/5
        [HttpPost]
        public ActionResult Edit(PedidosViewModel pedidoViewModel)
        {
            try
            {
                Pedido pedido = new Pedido()
                {
                    IdPedido = pedidoViewModel.IdPedido,
                    NroCalleOrigen = pedidoViewModel.nroDirOrigen,
                    CalleOrigen = pedidoViewModel.calleOrigen,
                    IdLocalidadOrigen = pedidoViewModel.idLocalidadOrigen,
                    NroCalleDestino = pedidoViewModel.nroDirDestino,
                    CalleDestino = pedidoViewModel.calleDestino,
                    IdLocalidadDestino = pedidoViewModel.idLocalidadDestino,
                    PrecioPedido = pedidoViewModel.precioPedido,
                    Descripcion = pedidoViewModel.Descripcion,
                    Observaciones = pedidoViewModel.Observaciones
                };
                if (Servicios.AccesoDatos.PedidosServicio.Editar(pedido))
                {
                    
                }
                else
                {
                    TempData["Message"] = "El pedido no se pudo editar";
                }               
                return RedirectToAction("PedidosCliente");
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

                        bool? Postulado = Servicios.AccesoDatos.PostulacionesServicio.Postulado(postulacion.IdPedido, IdUsuarioPostulado);                       
                        if(Postulado == false)
                        {
                            
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
                        }
                        else if (Postulado == true)
                        {
                            TempData["Message"] = "Ya se encuentra postulado. No es posible postularse más de una vez al mismo pedido.";
                        }
                        else
                        {
                            TempData["Message"] = "No ha sido posible efectuar la postulación. Vuelva a intentar más tarde.";
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

        public ActionResult PedidosTodos()
        {
            var listaPedidos = Servicios.AccesoDatos.PedidosServicio.ObtenerTodos();
            return View(listaPedidos);
        }
     }
}
