﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Point01Service
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EPESEntities : DbContext
    {
        public EPESEntities()
            : base("name=EPESEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DataForEvaluations> DataForEvaluations { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<PointOfEvaluations> PointOfEvaluations { get; set; }
        public virtual DbSet<Scores> Scores { get; set; }
    }
}
