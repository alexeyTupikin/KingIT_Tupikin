//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingIT.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class KingITDBEntities1 : DbContext
    {
        public KingITDBEntities1()
            : base("name=KingITDBEntities1")
        {
        }
        public KingITDBEntities1(string s)
            : base(s)
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
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tenants> tenants { get; set; }
        public virtual DbSet<City_p> City_p { get; set; }
        public virtual DbSet<for_employers> for_employers { get; set; }
        public virtual DbSet<for_managerA> for_managerA { get; set; }
        public virtual DbSet<for_proc_managera> for_proc_managera { get; set; }
        public virtual DbSet<forMalls> forMalls { get; set; }
        public virtual DbSet<getHallsView> getHallsView { get; set; }
    
        [DbFunction("KingITDBEntities1", "getHalls")]
        public virtual IQueryable<getHalls_Result> getHalls(Nullable<int> current)
        {
            var currentParameter = current.HasValue ?
                new ObjectParameter("current", current) :
                new ObjectParameter("current", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<getHalls_Result>("[KingITDBEntities1].[getHalls](@current)", currentParameter);
        }
    
        [DbFunction("KingITDBEntities1", "getMalls")]
        public virtual IQueryable<getMalls_Result> getMalls()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<getMalls_Result>("[KingITDBEntities1].[getMalls]()");
        }
    
        public virtual int add_rent(Nullable<int> idHall, Nullable<bool> status_action, string hallNum, Nullable<int> idMall, Nullable<System.DateTime> dateStart, Nullable<System.DateTime> datend, Nullable<int> idTenant, Nullable<int> employer_id)
        {
            var idHallParameter = idHall.HasValue ?
                new ObjectParameter("idHall", idHall) :
                new ObjectParameter("idHall", typeof(int));
    
            var status_actionParameter = status_action.HasValue ?
                new ObjectParameter("status_action", status_action) :
                new ObjectParameter("status_action", typeof(bool));
    
            var hallNumParameter = hallNum != null ?
                new ObjectParameter("hallNum", hallNum) :
                new ObjectParameter("hallNum", typeof(string));
    
            var idMallParameter = idMall.HasValue ?
                new ObjectParameter("idMall", idMall) :
                new ObjectParameter("idMall", typeof(int));
    
            var dateStartParameter = dateStart.HasValue ?
                new ObjectParameter("dateStart", dateStart) :
                new ObjectParameter("dateStart", typeof(System.DateTime));
    
            var datendParameter = datend.HasValue ?
                new ObjectParameter("datend", datend) :
                new ObjectParameter("datend", typeof(System.DateTime));
    
            var idTenantParameter = idTenant.HasValue ?
                new ObjectParameter("idTenant", idTenant) :
                new ObjectParameter("idTenant", typeof(int));
    
            var employer_idParameter = employer_id.HasValue ?
                new ObjectParameter("employer_id", employer_id) :
                new ObjectParameter("employer_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("add_rent", idHallParameter, status_actionParameter, hallNumParameter, idMallParameter, dateStartParameter, datendParameter, idTenantParameter, employer_idParameter);
        }
    
        public virtual int change_rent_date()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("change_rent_date");
        }
    
        public virtual ObjectResult<for_managerA_proc_Result> for_managerA_proc()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<for_managerA_proc_Result>("for_managerA_proc");
        }
    
        public virtual ObjectResult<Nullable<int>> login_proc(string login, string password)
        {
            var loginParameter = login != null ?
                new ObjectParameter("login", login) :
                new ObjectParameter("login", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("login_proc", loginParameter, passwordParameter);
        }
    
        public virtual int rent_pavilion(string action, Nullable<System.DateTime> date_start, Nullable<System.DateTime> date_end, Nullable<int> idTenant, Nullable<int> idHall, Nullable<int> idMall, Nullable<int> idEmployer, string hallNum)
        {
            var actionParameter = action != null ?
                new ObjectParameter("action", action) :
                new ObjectParameter("action", typeof(string));
    
            var date_startParameter = date_start.HasValue ?
                new ObjectParameter("date_start", date_start) :
                new ObjectParameter("date_start", typeof(System.DateTime));
    
            var date_endParameter = date_end.HasValue ?
                new ObjectParameter("date_end", date_end) :
                new ObjectParameter("date_end", typeof(System.DateTime));
    
            var idTenantParameter = idTenant.HasValue ?
                new ObjectParameter("idTenant", idTenant) :
                new ObjectParameter("idTenant", typeof(int));
    
            var idHallParameter = idHall.HasValue ?
                new ObjectParameter("idHall", idHall) :
                new ObjectParameter("idHall", typeof(int));
    
            var idMallParameter = idMall.HasValue ?
                new ObjectParameter("idMall", idMall) :
                new ObjectParameter("idMall", typeof(int));
    
            var idEmployerParameter = idEmployer.HasValue ?
                new ObjectParameter("idEmployer", idEmployer) :
                new ObjectParameter("idEmployer", typeof(int));
    
            var hallNumParameter = hallNum != null ?
                new ObjectParameter("hallNum", hallNum) :
                new ObjectParameter("hallNum", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("rent_pavilion", actionParameter, date_startParameter, date_endParameter, idTenantParameter, idHallParameter, idMallParameter, idEmployerParameter, hallNumParameter);
        }
    
        public virtual int rentHall(Nullable<bool> status_action, string hall_number, Nullable<int> mall_id, Nullable<System.DateTime> start_date, Nullable<System.DateTime> end_date, Nullable<int> tenant_id, Nullable<int> employer_id)
        {
            var status_actionParameter = status_action.HasValue ?
                new ObjectParameter("status_action", status_action) :
                new ObjectParameter("status_action", typeof(bool));
    
            var hall_numberParameter = hall_number != null ?
                new ObjectParameter("hall_number", hall_number) :
                new ObjectParameter("hall_number", typeof(string));
    
            var mall_idParameter = mall_id.HasValue ?
                new ObjectParameter("mall_id", mall_id) :
                new ObjectParameter("mall_id", typeof(int));
    
            var start_dateParameter = start_date.HasValue ?
                new ObjectParameter("start_date", start_date) :
                new ObjectParameter("start_date", typeof(System.DateTime));
    
            var end_dateParameter = end_date.HasValue ?
                new ObjectParameter("end_date", end_date) :
                new ObjectParameter("end_date", typeof(System.DateTime));
    
            var tenant_idParameter = tenant_id.HasValue ?
                new ObjectParameter("tenant_id", tenant_id) :
                new ObjectParameter("tenant_id", typeof(int));
    
            var employer_idParameter = employer_id.HasValue ?
                new ObjectParameter("employer_id", employer_id) :
                new ObjectParameter("employer_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("rentHall", status_actionParameter, hall_numberParameter, mall_idParameter, start_dateParameter, end_dateParameter, tenant_idParameter, employer_idParameter);
        }
    
        public virtual int RentOrBookPavilionInMall(Nullable<bool> status_action, string pavilion_number, Nullable<int> idMall, Nullable<System.DateTime> dateStart, Nullable<System.DateTime> dateEnd, Nullable<int> idTenant, Nullable<int> employee_id)
        {
            var status_actionParameter = status_action.HasValue ?
                new ObjectParameter("status_action", status_action) :
                new ObjectParameter("status_action", typeof(bool));
    
            var pavilion_numberParameter = pavilion_number != null ?
                new ObjectParameter("pavilion_number", pavilion_number) :
                new ObjectParameter("pavilion_number", typeof(string));
    
            var idMallParameter = idMall.HasValue ?
                new ObjectParameter("idMall", idMall) :
                new ObjectParameter("idMall", typeof(int));
    
            var dateStartParameter = dateStart.HasValue ?
                new ObjectParameter("dateStart", dateStart) :
                new ObjectParameter("dateStart", typeof(System.DateTime));
    
            var dateEndParameter = dateEnd.HasValue ?
                new ObjectParameter("dateEnd", dateEnd) :
                new ObjectParameter("dateEnd", typeof(System.DateTime));
    
            var idTenantParameter = idTenant.HasValue ?
                new ObjectParameter("idTenant", idTenant) :
                new ObjectParameter("idTenant", typeof(int));
    
            var employee_idParameter = employee_id.HasValue ?
                new ObjectParameter("employee_id", employee_id) :
                new ObjectParameter("employee_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RentOrBookPavilionInMall", status_actionParameter, pavilion_numberParameter, idMallParameter, dateStartParameter, dateEndParameter, idTenantParameter, employee_idParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
