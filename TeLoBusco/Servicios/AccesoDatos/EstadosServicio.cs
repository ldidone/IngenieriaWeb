using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class EstadosServicio
    {
        public static List<Estados> obtenerTodos()
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Estados.ToList();
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public static int obtenerIdPorDescripcion(string descripcion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Estados.Where(x => x.Descripcion == descripcion).FirstOrDefault().IdEstado;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static int obtenerIdEstadoPedidoPorDescripcion(string descripcion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    int idTipoActividad = TiposActividadesServicio.obtenerIdPorDescripcion("Pedido");
                    return db.Estados.Where(x => x.IdTipoActividad == idTipoActividad 
                                              && x.Descripcion == descripcion)
                                              .FirstOrDefault().IdEstado;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        } 
    }
}
