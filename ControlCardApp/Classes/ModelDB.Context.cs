﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlCardApp.Classes
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ControlCardEntities : DbContext
    {
        private static ControlCardEntities _context;

        public ControlCardEntities()
            : base("name=ControlCardEntities")
        {
        }

        public static ControlCardEntities GetContext()
        {
            if (_context == null) _context = new ControlCardEntities();
            return _context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<Points> Points { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Templates> Templates { get; set; }
        public virtual DbSet<TypeUsers> TypeUsers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WorkingCards> WorkingCards { get; set; }
    }
}