using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using RepositorioClases;

namespace Servicios.AccesoDatos
{
    public class ValoracionesServicios
    {
        public static bool Crear(Valoracion valoracion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    Valoraciones val = new Valoraciones
                    {
                        IdValoracion = valoracion.IdValoracion,
                        IdCliente = valoracion.IdCliente,
                        IdDelivery = valoracion.IdDelivery,
                        Valoracion = valoracion.Puntuacion,
                        Comentario = valoracion.Comentario
                    };
                    db.Valoraciones.Add(val);
                    db.SaveChanges();

                    return true;

                }
                catch
                {
                    return false;
                }

            }
        }
        public static double ObtenerPuntaje(string id)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var valoraciones = db.Valoraciones.Where(x => x.IdDelivery == id).ToArray();
                    return valoraciones.Average(x => x.Valoracion);
                }
                catch
                {

                }
                return 0;
            }
        }

    }
}
