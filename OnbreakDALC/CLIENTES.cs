//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnbreakDALC
{
    using System;
    using System.Collections.Generic;
    
    public partial class CLIENTES
    {
        public CLIENTES()
        {
            this.CONTRATOS = new HashSet<CONTRATOS>();
        }
    
        public string RutCliente { get; set; }
        public string RazonSocial { get; set; }
        public string NombreContacto { get; set; }
        public string MailContacto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdActividadEmpresa { get; set; }
        public int IdTipoEmpresa { get; set; }
    
        public virtual ACTIVIDADEMPRESAS ACTIVIDADEMPRESAS { get; set; }
        public virtual TIPOEMPRESAS TIPOEMPRESAS { get; set; }
        public virtual ICollection<CONTRATOS> CONTRATOS { get; set; }
    }
}
