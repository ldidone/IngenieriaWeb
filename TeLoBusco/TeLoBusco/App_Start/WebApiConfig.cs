using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TeLoBusco
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear(); //Quitar XML que viene por defecto
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; //Hacer que retorne JSON identado
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; //Hacer que ignore referencias circulares

            config.MapHttpAttributeRoutes();

            //ORIGINAL
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //CUSTOM
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}


