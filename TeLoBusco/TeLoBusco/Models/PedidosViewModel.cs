using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TeLoBusco.Models
{
    public class PedidosViewModel
    {
        [Required]
        [Display(Name = "Nro. domicilio")]
        public int nroDirOrigen { get; set; }

        [Required]
        [Display(Name = "Calle")]
        public string calleOrigen { get; set; }

        [Required]
        [Display(Name = "Localidad")]
        public int idLocalidadOrigen { get; set; }

        [Required]
        [Display(Name = "Nro. domicilio")]
        public int nroDirDestino { get; set; }

        [Required]
        [Display(Name = "Calle")]
        public string calleDestino{ get; set; }

        [Required]
        [Display(Name = "Localidad")]
        public int idLocalidadDestino { get; set; }

        [Required]
        [Display(Name = "Precio pedido")]
        public decimal precioPedido { get; set; }
    }
}