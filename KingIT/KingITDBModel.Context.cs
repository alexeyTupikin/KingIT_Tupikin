﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingIT
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class KingITDBEntities : DbContext
    {
        public KingITDBEntities()
            : base("name=KingITDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<employers> employers { get; set; }
        public virtual DbSet<halls> halls { get; set; }
        public virtual DbSet<malls> malls { get; set; }
        public virtual DbSet<postes> postes { get; set; }
        public virtual DbSet<rent> rent { get; set; }
        public virtual DbSet<statuses> statuses { get; set; }
        public virtual DbSet<tenants> tenants { get; set; }
    
        public virtual int f_login(string user_login, string user_password)
        {
            var user_loginParameter = user_login != null ?
                new ObjectParameter("user_login", user_login) :
                new ObjectParameter("user_login", typeof(string));
    
            var user_passwordParameter = user_password != null ?
                new ObjectParameter("user_password", user_password) :
                new ObjectParameter("user_password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f_login", user_loginParameter, user_passwordParameter);
        }
    
        public virtual int RentOrBookPavilion(string id_pavilion, Nullable<int> id_shopping_center, Nullable<int> status)
        {
            var id_pavilionParameter = id_pavilion != null ?
                new ObjectParameter("id_pavilion", id_pavilion) :
                new ObjectParameter("id_pavilion", typeof(string));
    
            var id_shopping_centerParameter = id_shopping_center.HasValue ?
                new ObjectParameter("id_shopping_center", id_shopping_center) :
                new ObjectParameter("id_shopping_center", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RentOrBookPavilion", id_pavilionParameter, id_shopping_centerParameter, statusParameter);
        }
    
        public virtual int setDateRent()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("setDateRent");
        }
    }
}
