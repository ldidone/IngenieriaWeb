using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TeLoBusco.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/localizacion")]
    public class LocalizacionApiController : ApiController
    {
        [HttpPost]
        [Route("almacenarlocalizacion")]
        public void AlmacenarLocalizacion(CoordenadaApi coordenada)
        {
            if (coordenada != null)
            {
                Servicios.AccesoDatos.PosicionesDeliverysService.Crear(coordenada);
            }           
        }
    }
}
