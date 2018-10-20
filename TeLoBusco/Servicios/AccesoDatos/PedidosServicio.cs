using Datos;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.ClasesAuxiliares;

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
                    return db.Pedidos.Include("Estados")
                                     .Include("Localidades")
                                     .Include("Localidades1")
                                     .Include("AspNetUsers")
                                     .Include("Postulaciones")
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
                    return db.Pedidos.Include("Estados")
                                     .Include("Localidades")
                                     .Include("Localidades1")
                                     .Where(x => x.idCliente == idCliente)
                                     .ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static Pedidos ObtenerPedidoPorId(int IdPedido)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.Pedidos.Include("AspNetUsers")
                                     .Include("Localidades")
                                     .Include("Localidades1")
                                     .Include("Postulaciones")
                                     .Where(x => x.IdPedido == IdPedido)
                                     .FirstOrDefault();                                  
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
                    pedidoAlmacenar.id_estado = EstadosServicio.obtenerIdEstadoPedidoPorDescripcion("Pendiente");
                    pedidoAlmacenar.descripcion_pedido = pedido.Descripcion;
                    pedidoAlmacenar.observaciones_pedido = pedido.Observaciones;                   
                    pedidoAlmacenar.IdTipoActividad = TiposActividadesServicio.obtenerIdPorDescripcion("Pedido");

                    /*ALMACENAR COORDENADAS*/                 
                    string Provincia = "Santa Fe";
                    string Pais = "Argentina";

                    /*ORIGEN*/
                    string DireccionOrigen = pedido.CalleOrigen + " " + pedido.NroCalleOrigen + ", " + localidadOrigen.Nombre + ", " + Provincia + ", " + Pais;
                    var coordenadasOrigen = Utilidades.Comunes.ObtenerCoordenada(DireccionOrigen);
                    if (coordenadasOrigen != null)
                    {
                        pedidoAlmacenar.lat_origen = coordenadasOrigen.lat;
                        pedidoAlmacenar.lng_origen = coordenadasOrigen.lng;
                    }

                    /*DESTINO*/
                    string DireccionDestino = pedido.CalleDestino + " " + pedido.NroCalleDestino + ", " + localidadDestino.Nombre + ", " + Provincia + ", " + Pais;
                    var coordenadasDestino = Utilidades.Comunes.ObtenerCoordenada(DireccionDestino);
                    if (coordenadasDestino != null)
                    {
                        pedidoAlmacenar.lat_destino = coordenadasDestino.lat;
                        pedidoAlmacenar.lng_destino = coordenadasDestino.lng;
                    }

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

        public static List<PedidoMapa> ObtenerPedidosCercanos(Coordenada posicionUsuario, int distancia)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Configuration.ProxyCreationEnabled = false;
                    int codigoEstadoPendiente = EstadosServicio.obtenerIdEstadoPedidoPorDescripcion("Pendiente");
                    var pedidos = db.Pedidos.Include("AspNetUsers")
                                            .Include("Localidades")
                                            .Include("Localidades1")
                                            .Include("Postulaciones")
                                            .Where(x => x.id_estado == codigoEstadoPendiente)
                                            .ToList();
                    List<PedidoMapa> pedidosCercanos = new List<PedidoMapa>();
                    foreach (var pedido in pedidos)
                    {
                        if (pedido.lat_origen != null && pedido.lng_origen != null)
                        {
                            Coordenada posicionPedido = new Coordenada();
                            posicionPedido.lat = Convert.ToDouble(pedido.lat_origen);
                            posicionPedido.lng = Convert.ToDouble(pedido.lng_origen);
                            double distanciaPedido = Utilidades.Comunes.DistanciaEntreDosPuntosEnKM(posicionUsuario, posicionPedido);
                            if (distanciaPedido <= distancia)
                            {
                                PedidoMapa pedidoMapa = ConvertirPedidoAPedidoMapa(pedido);
                                pedidosCercanos.Add(pedidoMapa);
                            }
                        }
                    }                
                    return pedidosCercanos;
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        public static bool ValidarPrecio(decimal precioMinimo, decimal precio, decimal precioMaximo)
        {
            return precio >= precioMinimo && precio <= precioMaximo;
        }

        public static PedidoMapa ConvertirPedidoAPedidoMapa (Pedidos pedido)
        {
            bool postulado = false;
            if (pedido.Postulaciones != null)
            {
                postulado = pedido.Postulaciones.Count > 0 ? true : false;
            }           
            PedidoMapa pedidoDetalles = new PedidoMapa
            {
                IdPedido = pedido.IdPedido,
                IdCliente = pedido.idCliente,
                NombreCliente = pedido.AspNetUsers.NombreApellido,
                DescripcionPedido = pedido.descripcion_pedido,
                ObservacionesPedido = pedido.observaciones_pedido != null ? pedido.observaciones_pedido : "Ninguna",
                DireccionOrigen = pedido.calle_origen + " " + pedido.nro_calle_origen + ", " + pedido.Localidades.Nombre,
                DireccionDestino = pedido.calle_destino + " " + pedido.nro_calle_destino + ", " + pedido.Localidades1.Nombre,
                Precio = pedido.precio_predido,
                LatOrigen = Convert.ToDouble(pedido.lat_origen),
                LngOrigen = Convert.ToDouble(pedido.lng_origen),
                Postulado = postulado
            };

            return pedidoDetalles;
        }
    }
}
