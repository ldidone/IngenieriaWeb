using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GoogleMapsGeocoding;
using GoogleMapsGeocoding.Common;
using SendGrid;
using SendGrid.Helpers.Mail;
using Utilidades.ClasesAuxiliares;

namespace Utilidades
{
    public class Comunes
    {
        public static Coordenada ObtenerCoordenada (string direccion)
        {
            try
            {
                string apiKey = "AIzaSyAm9iOzpnEzKPKWmwKhPOVW811qMoxvZDM";
                IGeocoder geocoder = new Geocoder(apiKey);
                GeocodeResponse response = geocoder.Geocode(direccion);

                Coordenada coordenadas = new Coordenada();
                coordenadas.lat = response.Results[0].Geometry.Location.Lat;
                coordenadas.lng = response.Results[0].Geometry.Location.Lng;

                return coordenadas;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static double DistanciaEntreDosPuntosEnKM(Coordenada punto1, Coordenada punto2)
        {
            double RadioTerrestre = 6371;
            double distancia = 0;
            double lat = (punto2.lat - punto1.lat) * (Math.PI / 180);
            double lng = (punto2.lng - punto1.lng) * (Math.PI / 180);
            double a = Math.Sin(lat / 2) * Math.Sin(lat / 2) + Math.Cos(punto1.lat * (Math.PI / 180)) * Math.Cos(punto2.lat * (Math.PI / 180)) * Math.Sin(lng / 2) * Math.Sin(lng / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distancia = RadioTerrestre * c;
            return distancia;
        }
    }
}
