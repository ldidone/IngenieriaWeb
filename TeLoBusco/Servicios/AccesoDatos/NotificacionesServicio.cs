using Datos;
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
    }
}
