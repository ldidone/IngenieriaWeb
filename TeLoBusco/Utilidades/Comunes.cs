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

        public static PrecioMinimoMaximo ObtenerRangoPrecios(Coordenada origen, Coordenada destino)
        {
            double distancia = Math.Round(DistanciaEntreDosPuntosEnKM(origen, destino), 2);
            PrecioMinimoMaximo precio = new PrecioMinimoMaximo();

            /*Valor por defecto en caso de que no se puedan calcular la distancia*/
            precio.PrecioMinimo = 20;
            precio.PrecioMaximo = 100;

            if (distancia >= 0 && distancia <= 9.99)
            {
                precio.PrecioMinimo = 20;
                precio.PrecioMaximo = 50;
            }
            else if(distancia >= 10 && distancia <= 19.99)
            {
                precio.PrecioMinimo = 50;
                precio.PrecioMaximo = 100;
            }
            else if (distancia >= 20 && distancia <= 29.99)
            {
                precio.PrecioMinimo = 100;
                precio.PrecioMaximo = 150;
            }
            else if (distancia >= 30 && distancia <= 39.99)
            {
                precio.PrecioMinimo = 150;
                precio.PrecioMaximo = 200;
            }
            else if (distancia >= 40 && distancia <= 50)
            {
                precio.PrecioMinimo = 200;
                precio.PrecioMaximo = 250;
            }
            return precio;
        }

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return ByteArraysEqual(buffer3, buffer4);
        }

    }
}
