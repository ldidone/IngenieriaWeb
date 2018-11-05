using Datos;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class PostulacionesServicio
    {
        public static Postulaciones Obtener(int id)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Postulaciones.Include("TiposActividades")
                                           .Include("Pedidos")
                                           .Include("AspNetUsers")
                                           .Include("Estados")
                                           .Where(x => x.IdPostulacion == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool Crear(Postulacion postulacion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {   
                    int idTipoActividad = TiposActividadesServicio.obtenerIdPorDescripcion("Postulación");
                    int idEstadoPostulacion = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("Postulado");
                    Postulaciones postulacionAlmacenar = new Postulaciones
                    {
                        IdTipoActividad = idTipoActividad,
                        IdPedido = postulacion.IdPedido,
                        IdUsuarioPostulado = postulacion.IdUsuarioPostulado,
                        IdEstadoPostulacion = idEstadoPostulacion,
                        TiempoEstimado = postulacion.TiempoEstimado,
                        Precio = postulacion.Precio
                    };

                    db.Postulaciones.Add(postulacionAlmacenar);
                    db.SaveChanges();

                    // Crear notificacion
                    int idEstadoPostulacionNotificacion = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("No vista");
                    string idUsuarioReceptor = PedidosServicio.ObtenerIdDueñoPedido(postulacion.IdPedido);
                    string nombrePostulado = AspNetUsersServicio.ObtenerNombrePorId(postulacion.IdUsuarioPostulado);
                    nombrePostulado = nombrePostulado != null ? nombrePostulado : "";

                    Notificaciones notificacion = new Notificaciones()
                    {
                        IdTipoActividad = idTipoActividad,
                        IdEstadoNotificacion = idEstadoPostulacionNotificacion,
                        Descripcion = "Nueva postulación a su pedido: " + nombrePostulado,
                        IdUsuarioReceptor = idUsuarioReceptor,
                        IdActividad = postulacionAlmacenar.IdPostulacion
                    };

                    db.Notificaciones.Add(notificacion);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool Aceptar(int idPostulacion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {       
                    /*Cambiar el estado de la postulación a: Aceptado*/
                    int idEstadoAceptado = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("Aceptado");
                    var postulacionAlmacenada = db.Postulaciones.Include("AspNetUsers")
                                                                .Include("Estados")
                                                                .Where(x => x.IdPostulacion == idPostulacion)
                                                                .FirstOrDefault();
                    postulacionAlmacenada.IdEstadoPostulacion = idEstadoAceptado;
                    db.SaveChanges();

                    /*Agregar notificacion de aceptado*/
                    int idTipoActividad = TiposActividadesServicio.obtenerIdPorDescripcion("Postulación Aceptada");
                    int idEstadoPostulacionNotificacion = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("No vista");
                    string idUsuarioReceptor = postulacionAlmacenada.IdUsuarioPostulado;
                    int idActividad = postulacionAlmacenada.IdPostulacion;
                    Notificaciones notificacion = new Notificaciones()
                    {
                        IdTipoActividad = idTipoActividad,
                        IdEstadoNotificacion = idEstadoPostulacionNotificacion,
                        Descripcion = "¡Postulación aceptada!",
                        IdUsuarioReceptor = idUsuarioReceptor,
                        IdActividad = idActividad
                    };
                    db.Notificaciones.Add(notificacion);
                    db.SaveChanges();

                    /*Agregar Delivery y precio al pedido*/
                    int idPedido = postulacionAlmacenada.IdPedido;
                    int idEstadoAsignado = EstadosServicio.obtenerIdEstadoPedidoPorDescripcion("Asignado");
                    var pedido = db.Pedidos.Where(x => x.IdPedido == idPedido).FirstOrDefault();
                    pedido.idDelivery = postulacionAlmacenada.IdUsuarioPostulado;
                    pedido.precio_transporte = postulacionAlmacenada.Precio;
                    pedido.id_estado = idEstadoAsignado; /*Colocar el pedido en estado asignado*/
                    db.SaveChanges();

                    /*Cambiar el estado del resto de postulaciones a ese pedudo a: Rechazado*/
                    int idEstadoRechazado = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("Rechazado");
                    var listaPostulaciones = db.Postulaciones.Include("AspNetUsers")
                                                                .Include("Estados")
                                                                .Where(x => x.IdPedido == idPedido && x.IdPostulacion != idPostulacion)
                                                                .ToList();
                    foreach (var postulacion in listaPostulaciones)
                    {
                        postulacion.IdEstadoPostulacion = idEstadoRechazado;
                        db.SaveChanges();

                        /*Agregar notificación de rechazado por cada pedido*/
                        int idTipoActividadR = TiposActividadesServicio.obtenerIdPorDescripcion("Postulación Rechazada");
                        int idEstadoPostulacionNotificacionR = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("No vista");
                        string idUsuarioReceptorR = postulacion.IdUsuarioPostulado;
                        int idActividadR = postulacion.IdPostulacion;
                        Notificaciones notificacionR = new Notificaciones()
                        {
                            IdTipoActividad = idTipoActividadR,
                            IdEstadoNotificacion = idEstadoPostulacionNotificacionR,
                            Descripcion = "Lo sentimos, su postulación ha sido rechazada.",
                            IdUsuarioReceptor = idUsuarioReceptorR,
                            IdActividad = idActividadR
                        };
                        db.Notificaciones.Add(notificacionR);
                        db.SaveChanges();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool? Postulado(int idPedido, string IdUsuario)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var listaPostulaciones = db.Postulaciones.Where(x => x.IdPedido == idPedido);
                    return listaPostulaciones.Any(x => x.IdUsuarioPostulado == IdUsuario);
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool PostularseApi(PostulacionApi postulacionApi)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    int idTipoActividad = TiposActividadesServicio.obtenerIdPorDescripcion("Postulación");
                    int idEstadoPostulacion = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("Postulado"); 
                    string idUsuarioPostulado = AspNetUsersServicio.obtenerPorEmail(postulacionApi.EmailUsuario).Id;
                    Postulaciones postulacion = new Postulaciones()
                    {
                        IdTipoActividad = idTipoActividad,
                        IdPedido = postulacionApi.IdPedido,
                        IdUsuarioPostulado = idUsuarioPostulado,
                        IdEstadoPostulacion = idEstadoPostulacion,
                        TiempoEstimado = postulacionApi.Tiempo,
                        Precio = postulacionApi.Precio
                    };
                    db.Postulaciones.Add(postulacion);
                    db.SaveChanges();

                    // Crear notificacion
                    int idEstadoPostulacionNotificacion = EstadosServicio.obtenerIdEstadoPostulacionPorDescripcion("No vista");
                    string idUsuarioReceptor = PedidosServicio.ObtenerIdDueñoPedido(postulacion.IdPedido);
                    string nombrePostulado = AspNetUsersServicio.ObtenerNombrePorId(postulacion.IdUsuarioPostulado);
                    nombrePostulado = nombrePostulado != null ? nombrePostulado : "";

                    Notificaciones notificacion = new Notificaciones()
                    {
                        IdTipoActividad = idTipoActividad,
                        IdEstadoNotificacion = idEstadoPostulacionNotificacion,
                        Descripcion = "Nueva postulación a su pedido: " + nombrePostulado,
                        IdUsuarioReceptor = idUsuarioReceptor,
                        IdActividad = postulacion.IdPostulacion
                    };

                    db.Notificaciones.Add(notificacion);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
