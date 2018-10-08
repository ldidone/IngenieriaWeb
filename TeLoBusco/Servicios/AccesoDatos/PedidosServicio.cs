using Datos;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class PedidosServicio
    {
        public static List<Pedidos> ObtenerTodos()
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

        public static List<Pedidos> ObtenerPedidosCliente(string idCliente)
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

        //public static bool crear(string idCliente, int nroDirOrigen, string calleOrigen, int idLocalidadOrigen,
        //                         int nroDirDestino, string calleDestino, int idLocalidadDestino, decimal precioPedido)
        //{
        //    using (TeloBuscoEntities db = new TeloBuscoEntities())
        //    {
        //        try
        //        {
        //            Pedidos pedido = new Pedidos();
        //            pedido.idCliente = idCliente;
        //            pedido.nro_calle_origen = nroDirOrigen;
        //            pedido.calle_origen = calleOrigen;
        //            Localidades localidadOrigen = LocalidadesServicio.obtener(idLocalidadOrigen);
        //            pedido.Localidades = localidadOrigen;
        //            pedido.id_localidad_origen = idLocalidadOrigen;
        //            pedido.nro_calle_destino = nroDirDestino;
        //            pedido.calle_destino = calleDestino;
        //            Localidades localidadDestino = LocalidadesServicio.obtener(idLocalidadDestino);
        //            pedido.Localidades1 = localidadDestino;
        //            pedido.id_localidad_destino = idLocalidadDestino;
        //            pedido.precio_predido = precioPedido;
        //            pedido.id_estado = EstadosPedidoServicio.obtenerIdPorDescripcion("Pendiente");
        //            db.Pedidos.Add(pedido);
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //}

        public static bool Crear(Pedido pedido)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    Pedidos pedidoAlmacenar = new Pedidos();

                    pedidoAlmacenar.idCliente = pedido.IdCliente;
                    pedidoAlmacenar.nro_calle_origen = pedido.NroCalleOrigen;
                    pedidoAlmacenar.calle_origen = pedido.CalleOrigen;
                    //Localidades localidadOrigen = LocalidadesServicio.obtener(pedido.IdLocalidadOrigen);
                    Localidades localidadOrigen = db.Localidades.Where(x => x.idLocalidad == pedido.IdLocalidadOrigen).FirstOrDefault();
                    pedidoAlmacenar.Localidades = localidadOrigen;
                    pedidoAlmacenar.id_localidad_origen = pedido.IdLocalidadOrigen;
                    pedidoAlmacenar.nro_calle_destino = pedido.NroCalleDestino;
                    pedidoAlmacenar.calle_destino = pedido.CalleDestino;
                    //Localidades localidadDestino = LocalidadesServicio.obtener(pedido.IdLocalidadDestino);
                    Localidades localidadDestino = db.Localidades.Where(x => x.idLocalidad == pedido.IdLocalidadDestino).FirstOrDefault();
                    pedidoAlmacenar.Localidades1 = localidadDestino;
                    pedidoAlmacenar.id_localidad_destino = pedido.IdLocalidadDestino;
                    pedidoAlmacenar.precio_predido = pedido.PrecioPedido;
                    pedidoAlmacenar.descripcion_pedido = pedido.Descripcion;
                    pedidoAlmacenar.observaciones_pedido = pedido.Observaciones;
                    pedidoAlmacenar.id_estado = EstadosPedidoServicio.obtenerIdPorDescripcion("Pendiente");

                    db.Pedidos.Add(pedidoAlmacenar);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
