using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Fintech.Models.CodeFirst;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using Fintech.Models.ModelClass;

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
        public byte[] UserPhoto { get; set; }
        public int? HouseHoldId { get; set; }
        public string InviteEmail { get; set; }

        public virtual HouseHold HouseHold { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
     

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);


            userIdentity.AddClaim(new Claim("LastName", LastName));
            userIdentity.AddClaim(new Claim("FirstName", FirstName));
            userIdentity.AddClaim(new Claim("FullName", FullName));
            
            //add User to an Object- in this case it is HouseHold object
            userIdentity.AddClaim(new Claim("HouseHoldId", HouseHoldId.ToString()));

            // Add custom user claims here
            return userIdentity;
        }
        //public virtual HouseHold HouseHold { get; set; }
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

        public System.Data.Entity.DbSet<BankAccount> BankAccounts { get; set; }

        public System.Data.Entity.DbSet<HouseHold> HouseHolds { get; set; }

        public System.Data.Entity.DbSet<Budget> Budgets { get; set; }
        public System.Data.Entity.DbSet<BudgetItem> BudgetItems { get; set; }
        public System.Data.Entity.DbSet<Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<Category> Categories { get; set; }
        public System.Data.Entity.DbSet<Invite> Invites { get; set; }

    }
}