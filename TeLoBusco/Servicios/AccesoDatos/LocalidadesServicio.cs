using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class LocalidadesServicio
    {
        public static List<Localidades> obtenerTodas()
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Localidades.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static Localidades obtener(int id)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Localidades.Where(x => x.idLocalidad == id).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
