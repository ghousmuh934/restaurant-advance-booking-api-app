﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiDemo.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebApiDemoEntities2 : DbContext
    {
        public WebApiDemoEntities2()
            : base("name=WebApiDemoEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AddChef> AddChefs { get; set; }
        public virtual DbSet<AddMenu> AddMenus { get; set; }
        public virtual DbSet<AddWaiter> AddWaiters { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order_Detail> Order_Detail { get; set; }
        public virtual DbSet<Variation> Variations { get; set; }
        public virtual DbSet<table_layout> table_layout { get; set; }
        public virtual DbSet<SetTable> SetTables { get; set; }
    }
}