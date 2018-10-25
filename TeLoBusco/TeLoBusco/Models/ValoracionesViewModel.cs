using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeLoBusco.Models
{
    public class ValoracionesViewModel
    {
        public int IdValoracion { get; set; }
        public string IdCliente { get; set; }
        public string IdDelivery { get; set; }
        public decimal Puntuacion { get; set; }
        public string Comentario { get; set; }
        public int IdPedido { get; set; }
    }
    
}