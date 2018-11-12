using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace TeLoBusco.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
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

        //Prueba JWT
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            bool isCredentialValid = false;

            var user = Servicios.AspNetUsersServicio.obtenerPorEmail(login.Email);
            if (user != null)
            {
                string passwordHash = user.PasswordHash;
                bool passwordEquals = Utilidades.Comunes.VerifyHashedPassword(passwordHash, login.Password);
                isCredentialValid = passwordEquals;
            }

            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Email);
                return Ok(token);
            }
            else
            {
                //return Unauthorized(); -> problema, retorna codigo 200 y el html de la pagina de login
                //var responseMessage = ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized"));
                //return StatusCode(HttpStatusCode.Unauthorized);
                //HttpResponse httpResponse = new HttpResponse(null);
                //httpResponse.SuppressFormsAuthenticationRedirect = true;
                //httpResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                return InternalServerError();
            }
        }
    }
}
