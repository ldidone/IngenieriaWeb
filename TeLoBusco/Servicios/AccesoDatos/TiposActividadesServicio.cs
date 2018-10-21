using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class TiposActividadesServicio
    {
        public static int obtenerIdPorDescripcion(string descripcion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.TiposActividades.Where(x => x.Descripcion == descripcion).FirstOrDefault().IdTipoActividad;
                }
                catch(Exception ex)
                {
                    return 0;
                }
            }
        }

        public static string obtenerDescripcionPorId(int idTipoActividad)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.TiposActividades.Where(x => x.IdTipoActividad == idTipoActividad).FirstOrDefault().Descripcion;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
