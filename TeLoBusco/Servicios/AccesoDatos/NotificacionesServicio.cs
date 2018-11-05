using Datos;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class NotificacionesServicio
    {
        //No vistas
        public static List<Notificaciones> ObtenerNotificacionesUsuario(string idUsuario)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    int idNoVisto = EstadosServicio.obtenerIdPorDescripcion("No vista");
                    return db.Notificaciones.Include("TiposActividades")
                                            .Include("Estados")
                                            .Include("AspNetUsers")
                                            .Include("Postulaciones")
                                            .Where(x => x.IdUsuarioReceptor == idUsuario && x.IdEstadoNotificacion == idNoVisto)
                                            .ToList();
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Notificaciones> ObtenerTodasNotificaciones()
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Notificaciones.Include("TiposActividades")
                                            .Include("Estados")
                                            .Include("AspNetUsers")
                                            .Include("Postulaciones").
                                            ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool MarcarNotificacionComoVisto(int idNotificacion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var idVisto = EstadosServicio.obtenerIdPorDescripcion("Vista");
                    var notificacion = db.Notificaciones.Where(x => x.IdNotificacion == idNotificacion).FirstOrDefault();                 
                    notificacion.IdEstadoNotificacion = idVisto;
                    db.SaveChanges();

                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<NotificacionApi> ObtenerNotificacionesUsuarioApi(string idUsuario)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    List<NotificacionApi> notificacionesApi = new List<NotificacionApi>();
                    int idNoVisto = EstadosServicio.obtenerIdPorDescripcion("No vista");
                    var listaNotificaciones = db.Notificaciones
                                            .Include("TiposActividades")
                                            .Include("Estados")
                                            .Include("AspNetUsers")
                                            .Include("Postulaciones")
                                            .Include("Postulaciones.Pedidos")
                                            .Include("Postulaciones.Pedidos.Localidades")
                                            .Include("Postulaciones.Pedidos.Localidades1")
                                            .Where(x => x.IdUsuarioReceptor == idUsuario && x.IdEstadoNotificacion == idNoVisto)
                                            .ToList();
                    foreach (var notificacion in listaNotificaciones)
                    {
                        NotificacionApi notificacionApi = new NotificacionApi()
                        {
                            Descripcion = notificacion.Descripcion,
                            DescripcionPedido = notificacion.Postulaciones.Pedidos.descripcion_pedido,
                            ObservacionPedido = notificacion.Postulaciones.Pedidos.observaciones_pedido != null? notificacion.Postulaciones.Pedidos.observaciones_pedido : "Ninguna",
                            NombreCliente = notificacion.AspNetUsers.NombreApellido,
                            DireccionOrigen = notificacion.Postulaciones.Pedidos.calle_origen + " " + notificacion.Postulaciones.Pedidos.nro_calle_origen + ", " + notificacion.Postulaciones.Pedidos.Localidades.Nombre,
                            DireccionDestino = notificacion.Postulaciones.Pedidos.calle_destino + " " + notificacion.Postulaciones.Pedidos.nro_calle_destino + ", " + notificacion.Postulaciones.Pedidos.Localidades1.Nombre,
                            Precio = Math.Round(notificacion.Postulaciones.Pedidos.precio_predido, 2)
                        };
                        notificacionesApi.Add(notificacionApi);
                    }
                    return notificacionesApi;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
