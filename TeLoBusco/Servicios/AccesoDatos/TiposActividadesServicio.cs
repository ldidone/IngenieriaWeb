using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    class TiposActividadesServicio
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
    }
}
