using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TeLoBusco.Controllers
{
    public class LoginApiController : ApiController
    {
        [HttpGet]
        public bool LoginValido(string email, string password)
        {
            var user = Servicios.AspNetUsersServicio.obtenerPorEmail(email);
            if (user != null)
            {
                string passwordHash = user.PasswordHash;
                bool passwordEquals = Utilidades.Comunes.VerifyHashedPassword(passwordHash, password);
                return passwordEquals;
            }
            return false;
        }
    }
}
