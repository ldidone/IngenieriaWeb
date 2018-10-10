using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TeLoBusco.Models;
using SendGrid;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using SendGrid.Helpers.Mail;
using System.Net.Http;
using System.Net.Mail;

namespace TeLoBusco
{
    public class EmailService : IIdentityMessageService
    {
        /* ORIGNIAL */
        //public Task SendAsync(IdentityMessage message)
        //{
        //    // Conecte su servicio de correo electrónico aquí para enviar correo electrónico.
        //    return Task.FromResult(0);
        //}

        /*MANDA MAIL SENDGRID - FUNCIONA: PROBLEMA: CUENTA DE PAGO*/
        /*public async Task SendAsync(IdentityMessage message)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("wappo.pedidos@hotmail.com", "Wappo");
            var subject = message.Subject;
            var to = new EmailAddress(message.Destination);
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }*/

        /*ALTERNATIVA - NO ASÍNCRONO (aunque el método lo sea)*/
        public async Task SendAsync(IdentityMessage message)
        {
            MailMessage mail = new MailMessage("wappo.pedidos@gmail.com", message.Destination, message.Subject, message.Body)
            {
                From = new MailAddress(message.Destination, "Wappo"),
                IsBodyHtml = true
            };

            NetworkCredential credential = new NetworkCredential("wappo.pedidos@gmail.com", "wappopedidos123*");
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = credential
            };
            client.Send(mail); 
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte el servicio SMS aquí para enviar un mensaje de texto.
            return Task.FromResult(0);
        }
    }

    // Configure el administrador de usuarios de aplicación que se usa en esta aplicación. UserManager se define en ASP.NET Identity y se usa en la aplicación.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure la lógica de validación de nombres de usuario
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure la lógica de validación de contraseñas
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configurar valores predeterminados para bloqueo de usuario
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Registre proveedores de autenticación en dos fases. Esta aplicación usa los pasos Teléfono y Correo electrónico para recibir un código para comprobar el usuario
            // Puede escribir su propio proveedor y conectarlo aquí.
            manager.RegisterTwoFactorProvider("Código telefónico", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Su código de seguridad es {0}"
            });
            manager.RegisterTwoFactorProvider("Código de correo electrónico", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de seguridad",
                BodyFormat = "Su código de seguridad es {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure el administrador de inicios de sesión que se usa en esta aplicación.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    //NUEVO
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));

            return appRoleManager;
        }
    }
}
