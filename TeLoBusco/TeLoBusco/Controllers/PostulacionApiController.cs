using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Servicios.AccesoDatos;

namespace TeLoBusco.Controllers
{
    public class PostulacionApiController : ApiController
    {
        [HttpPost]
        public bool Postularse(RepositorioClases.PostulacionApi postulacionApi)
        {
            bool result = PostulacionesServicio.PostularseApi(postulacionApi);
            return result;
        }
    }
}
