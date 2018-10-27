using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace TeLoBusco.Models
{
    public class DenunciasViewModel
    {
        public int IdDenuncia { get; set; }
        public int IdPedido { get; set; }
        public int IdEstado { get; set; }

        [Display(Name = "Descripcion del pedido")]
        public string DescripcionPedido { get; set; }

        [Required]
        [Display(Name = "Motivo de la denuncia")]
        public string Motivo { get; set; }
    }
}