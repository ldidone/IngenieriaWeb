using Datos;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class PosicionesDeliverysService
    {
        public static void Crear(CoordenadaApi coordenada)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                string IdDelivery = AspNetUsersServicio.obtenerIdPorEmail(coordenada.Email);
                if (!string.IsNullOrEmpty(IdDelivery))
                {
                    var posicionUsuario = db.PosicionesDeliverys.Where(x => x.idDelivery == IdDelivery).FirstOrDefault();                  
                    if (posicionUsuario != null)
                    {
                        //Si el delivery ya tiene alguna posición almacenada, la actualizo
                        posicionUsuario.lat = coordenada.Lat;
                        posicionUsuario.lng = coordenada.Lng;
                        posicionUsuario.FechaHoraUltimaPos = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        //Si el delivery no tiene alguna posición almacenada, la creo
                        PosicionesDeliverys posicionDelivery = new PosicionesDeliverys()
                        {
                            idDelivery = IdDelivery != null ? IdDelivery : "",
                            lat = coordenada.Lat,
                            lng = coordenada.Lng,
                            FechaHoraUltimaPos = DateTime.Now
                        };
                        db.PosicionesDeliverys.Add(posicionDelivery);
                        db.SaveChanges();
                    }                  
                }              
            }
        }

        public static PosicionesDeliverys ObtenerUltimaPosicionDelivery(string idDelivery)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var listaPosiciones = db.PosicionesDeliverys.Where(x => x.idDelivery == idDelivery).ToList();
                    if (listaPosiciones.Count == 0)
                    {
                        return null;
                    }
                    else if (listaPosiciones.Count == 1)
                    {
                        return listaPosiciones.FirstOrDefault();
                    }
                    else
                    {
                        DateTime ultimaFechaHora = listaPosiciones.Max(x => x.FechaHoraUltimaPos);
                        return listaPosiciones.Where(x => x.FechaHoraUltimaPos == ultimaFechaHora).FirstOrDefault();
                    }
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
