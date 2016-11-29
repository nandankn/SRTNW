using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System;

namespace SRTNW.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public string DisplayName { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //this.Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new MyContextInitializer());
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<SRTNW.Models.ItemCategory> ItemCategories { get; set; }

        public System.Data.Entity.DbSet<SRTNW.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<SRTNW.Models.ItemGroup> ItemGroups { get; set; }
    }

    public class MyContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
        }
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            InitializeIdentity(context);
        }
        private void InitializeIdentity(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            RoleManager.Create(new IdentityRole(Constants.Role_Admin));
            foreach (var item in Enum.GetValues(typeof(RoleNames)))
            {
                RoleManager.Create(new IdentityRole(item.ToString()));
            }
            var user = new ApplicationUser() { Id = Constants.AdminId, DisplayName = "Admin", UserName = Constants.Admin_Username, Email = Constants.Admin_Email, EmailConfirmed = true };
            var result = UserManager.Create(user, Constants.AdminPassword);
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, Constants.Role_Admin);
                foreach (var item in Enum.GetValues(typeof(RoleNames)))
                {
                    UserManager.AddToRole(user.Id, item.ToString());
                }
            }
        }
    }
}