//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace softcomputacion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            this.proveedorXproducto = new HashSet<proveedorXproducto>();
        }
    
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string observacion { get; set; }
        public int stockMinimo { get; set; }
        public int stockIdeal { get; set; }
        public int stockActual { get; set; }
        public decimal precioCosto { get; set; }
        public decimal precioGremio { get; set; }
        public decimal precioContado { get; set; }
        public decimal precioLista { get; set; }
        public int idCategoria { get; set; }
        public int idSubCategoria { get; set; }
        public int idEstado { get; set; }
        public bool precioFijo { get; set; }
    
        public virtual categoria categoria { get; set; }
        public virtual estado estado { get; set; }
        public virtual subcategoria subcategoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<proveedorXproducto> proveedorXproducto { get; set; }
    }
}
