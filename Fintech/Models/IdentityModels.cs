using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Fintech.Models.ModelClass;
using System.Collections.Generic;

namespace Fintech.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Transactions = new HashSet<Transaction>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public int HouseHoldId { get; set; }

        public virtual HouseHold HouseHold { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Fintech.Models.ModelClass.BankAccount> BankAccounts { get; set; }

        public System.Data.Entity.DbSet<Fintech.Models.ModelClass.HouseHold> HouseHolds { get; set; }

        public System.Data.Entity.DbSet<Fintech.Models.ModelClass.Budget> Budgets { get; set; }
        public System.Data.Entity.DbSet<Fintech.Models.ModelClass.BudgetItem> BudgetItems { get; set; }
        public System.Data.Entity.DbSet<Fintech.Models.ModelClass.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<Fintech.Models.ModelClass.Category>Categories { get; set; }

    }
}