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
                catch(Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
