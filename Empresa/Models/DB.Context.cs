﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Empresa.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EmpresaConexionString : DbContext
    {
        public EmpresaConexionString()
            : base("name=EmpresaConexionString")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Nomina> Nomina { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Rango_venta> Rango_venta { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }
    
        public virtual ObjectResult<VentasDeVendedoresPorMes2_Result> VentasDeVendedoresPorMes2()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentasDeVendedoresPorMes2_Result>("VentasDeVendedoresPorMes2");
        }
    }
}
