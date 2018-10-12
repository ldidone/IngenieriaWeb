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
    
    public partial class Pedidos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedidos()
        {
            this.Postulaciones = new HashSet<Postulaciones>();
        }
    
        public int IdPedido { get; set; }
        public string idCliente { get; set; }
        public string idDelivery { get; set; }
        public int nro_calle_origen { get; set; }
        public string calle_origen { get; set; }
        public int id_localidad_origen { get; set; }
        public int nro_calle_destino { get; set; }
        public string calle_destino { get; set; }
        public int id_localidad_destino { get; set; }
        public decimal precio_predido { get; set; }
        public Nullable<decimal> precio_transporte { get; set; }
        public int id_estado { get; set; }
        public string descripcion_pedido { get; set; }
        public string observaciones_pedido { get; set; }
        public int IdTipoActividad { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
        public virtual Estados Estados { get; set; }
        public virtual Localidades Localidades { get; set; }
        public virtual Localidades Localidades1 { get; set; }
        public virtual TiposActividades TiposActividades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Postulaciones> Postulaciones { get; set; }
    }
}
