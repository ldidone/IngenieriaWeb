using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class EstadosPedidoServicio
    {
        public static List<Estados_Pedidos> obtenerTodos()
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Estados_Pedidos.ToList();
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
                    return db.Estados_Pedidos.Where(x => x.Descripcion == descripcion).FirstOrDefault().IdEstado;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
    }
}
