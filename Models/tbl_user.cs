//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_user
    {
        public int fld_idUser { get; set; }
        public string fld_username { get; set; }
        public string fld_encryptedPassword { get; set; }
        public string fld_password { get; set; }
        public int fk_idTipo { get; set; }
    
        public virtual tbl_typeUser tbl_typeUser { get; set; }
    }
}
