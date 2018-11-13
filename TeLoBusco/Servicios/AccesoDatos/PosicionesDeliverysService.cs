using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class PosicionesDeliverysService
    {
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
