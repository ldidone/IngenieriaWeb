//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notificaciones
    {
        public int IdNotificacion { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdEstadoNotificacion { get; set; }
        public string Descripcion { get; set; }
        public string IdUsuarioReceptor { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Estados Estados { get; set; }
        public virtual TiposActividades TiposActividades { get; set; }
    }
}
