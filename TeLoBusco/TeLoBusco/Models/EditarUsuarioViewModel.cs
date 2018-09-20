using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeLoBusco.Models
{
    public class EditarUsuarioViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nombre y apellido")]
        public string NombreApellido { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}