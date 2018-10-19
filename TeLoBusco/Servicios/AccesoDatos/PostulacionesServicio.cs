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
