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
        public static List<Notificaciones> ObtenerNotificacionesUsuario(string idUsuario)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.Notificaciones.Include("TiposActividades")
                                            .Include("Estados")
                                            .Include("AspNetUsers")
                                            .Include("Postulaciones")
                                            .Where(x => x.IdUsuarioReceptor == idUsuario)
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
    }
}
