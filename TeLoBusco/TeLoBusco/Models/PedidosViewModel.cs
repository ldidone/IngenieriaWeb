using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TeLoBusco.Models
{
    public class PedidosViewModel
    {
        public int IdPedido { get; set; }
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
        [Display(Name = "Precio del pedido")]
        public decimal precioPedido { get; set; }

        [Required]
        [Display(Name = "Descripción del pedido")]
        public string Descripcion { get; set; }

        [Display(Name = "Observaciones del pedido")]
        public string Observaciones { get; set; }
    }
}