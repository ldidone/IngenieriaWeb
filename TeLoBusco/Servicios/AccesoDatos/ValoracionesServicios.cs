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
        /*Crear valoración - cliente y finalizar el pedido (cliente)*/
        public static bool Crear(Valoracion valoracion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    bool result = false;
                    Valoraciones val = new Valoraciones
                    {
                        IdValoracion = valoracion.IdValoracion,
                        IdCliente = valoracion.IdCliente,
                        IdDelivery = valoracion.IdDelivery,
                        Valoracion = valoracion.Puntuacion,
                        Comentario = valoracion.Comentario,
                        IdPedido = valoracion.IdPedido,
                        valoracion_cliente = true,
                        valoracion_delivery = false
                    };
                    
                    if (PedidosServicio.FinalizarPedidoCliente(valoracion.IdPedido)) //Finalizo el pedido -> si está ok agrego la valoración
                    {
                        db.Valoraciones.Add(val);
                        db.SaveChanges();
                        result = true;
                    }

                    return result;

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

        /*El delivery califica al cliente y finaliza*/
        public static bool CrearValoracionCliente(Valoracion valoracion)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    bool result = false;
                    Valoraciones val = new Valoraciones
                    {
                        IdCliente = valoracion.IdCliente,
                        IdDelivery = valoracion.IdDelivery,
                        Valoracion = valoracion.Puntuacion,
                        Comentario = valoracion.Comentario,
                        IdPedido = valoracion.IdPedido,
                        valoracion_cliente = false,
                        valoracion_delivery = true
                    };

                    if (PedidosServicio.FinalizarPedidoDelivery(valoracion.IdPedido)) //Finalizo el pedido -> si está ok agrego la valoración
                    {
                        db.Valoraciones.Add(val);
                        db.SaveChanges();
                        result = true;
                    }

                    return result;

                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
    }
}
