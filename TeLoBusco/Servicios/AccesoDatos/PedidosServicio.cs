using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class PedidosServicio
    {
        public static List<Pedidos> obtenerTodos()
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Pedidos.Include("Estados_Pedidos")
                                     .Include("Localidades")
                                     .Include("AspNetUsers")
                                     .ToList();
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Pedidos> obtenerPedidosCliente(string idCliente)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Pedidos.Include("Estados_Pedidos")
                                     .Include("Localidades")
                                     .Where(x => x.idCliente == idCliente)
                                     .ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool crear(string idCliente, int nroDirOrigen, string calleOrigen, int idLocalidadOrigen,
                                 int nroDirDestino, string calleDestino, int idLocalidadDestino, decimal precioPedido)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    Pedidos pedido = new Pedidos();
                    pedido.idCliente = idCliente;
                    pedido.nro_calle_origen = nroDirOrigen;
                    pedido.calle_origen = calleOrigen;
                    //Localidades localidadOrigen = LocalidadesServicio.obtener(idLocalidadOrigen);
                    //pedido.Localidades = localidadOrigen;
                    pedido.id_localidad_origen = idLocalidadOrigen;
                    pedido.nro_calle_destino = nroDirDestino;
                    pedido.calle_destino = calleDestino;
                    //Localidades localidadDestino = LocalidadesServicio.obtener(idLocalidadDestino);
                    //pedido.Localidades1 = localidadDestino;
                    pedido.id_localidad_destino = idLocalidadDestino;
                    pedido.precio_predido = precioPedido;
                    pedido.id_estado = EstadosPedidoServicio.obtenerIdPorDescripcion("Pendiente");
                    db.Pedidos.Add(pedido);
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
