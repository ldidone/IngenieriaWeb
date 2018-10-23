using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Utilidades.ClasesAuxiliares;

namespace TeLoBusco.Models
{
    public class PostulacionesViewModel
    {
        /* Completados por el 'Delivery' */
        [Required]
        [Display(Name = "Tiempo estimado en minutos (*)")]
        public double TiempoEstimado { get; set; }

        [Required]
        [Display(Name = "Precio ($)")]
        public decimal Precio { get; set; }

        public int IdPedido { get; set; }

        /*Datos otorgados*/
        public string IdPostulado { get; set; }
        public string PostuladoNombreApellido { get; set; }
        public PedidoMapa pedidoDetalles { get; set; }
        public decimal precioMinimo { get; set; }
        public decimal precioMaximo { get; set; }

        public PostulacionesViewModel()
        {
            pedidoDetalles = new PedidoMapa();
        }
    }
}