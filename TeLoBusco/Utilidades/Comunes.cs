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



namespace Utilidades
{
    public class Comunes
    {
        public static float ObtenerCoordenada (string direccion)
        {
            string apiKey = "AIzaSyAm9iOzpnEzKPKWmwKhPOVW811qMoxvZDM";
            IGeocoder geocoder = new Geocoder(apiKey);
            GeocodeResponse response = geocoder.Geocode(direccion);

            // You can then query the response to get what you need
            double latitud = response.Results[0].Geometry.Location.Lat;
            double longitud = response.Results[0].Geometry.Location.Lng;
            return 0;
        }
    }
}
