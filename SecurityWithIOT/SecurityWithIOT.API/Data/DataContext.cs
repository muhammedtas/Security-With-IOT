using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Data
{
    public class DataContext : DbContext
    {
        //public DataContext Context;

         public DataContext(DbContextOptions<DataContext> options ) : base(options){

            //this.Context = new DataContext(options);
         }
      
        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments {get;set;}

        public DbSet<Company> Companies {get;set;}
        public DbSet<Photo> Photos {get;set;}
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<District> Districts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  

            builder.Entity<User>().HasOne(x=>x.Department).WithMany(x=>x.Users).HasForeignKey(x=>x.DepartmentId).OnDelete(DeleteBehavior.Restrict);;
            builder.Entity<User>().HasOne(x=>x.Company).WithMany(x=>x.Users).HasForeignKey(x=>x.CompanyId);
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<User>().HasMany(x=>x.Photos).WithOne(x=>x.User).HasForeignKey(x=>x.UserId);
            builder.Entity<User>().HasMany(x=>x.Addresses).WithOne(x=>x.User).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Department>().HasOne(x=>x.Company).WithMany(x=>x.Departments).HasForeignKey(x=>x.CompanyId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Address>().HasOne(x=>x.Country);
            builder.Entity<Address>().HasOne(x=>x.City);
            
            builder.Entity<District>().HasOne(x=>x.City).WithMany(x=>x.Districts).HasForeignKey(x=>x.Id).OnDelete(DeleteBehavior.Restrict);;

            builder.Entity<City>().HasOne(x=>x.Country).WithMany(x=>x.Cities).OnDelete(DeleteBehavior.Restrict);
            // builder.Entity<User>().HasKey(x=>x.Id);
            // builder.Entity<User>().HasOne( x => x.Department).WithMany( x => x.Users);
            // builder.Entity<User>().HasOne( x => x.Company).WithMany(x => x.Users).HasPrincipalKey(x=>x.Id);
            // builder.Entity<Department>().HasKey(x=>x.Id);
            // builder.Entity<Department>().HasOne(x=>x.Company).WithMany(x=>x.Departments);
            // builder.Entity<Company>().HasKey(x=>x.Id);
        }  
        public virtual void Save()  
        {  
        base.SaveChanges();  
        }  

        public string UserProvider  
        {  
            get  
            {  
                if (!string.IsNullOrEmpty(WindowsIdentity.GetCurrent().Name))  
                return WindowsIdentity.GetCurrent().Name.Split('\\')[1];  
                return string.Empty;  
            }  
        }  
    
        public Func<DateTime> TimestampProvider { get; set; } = ()  
            => DateTime.UtcNow;  
        public override int SaveChanges()  
        {  
        TrackChanges();  
        return base.SaveChanges();  
        }  
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())  
        {  
        TrackChanges();  
        return await base.SaveChangesAsync(cancellationToken);  
        }  
    
        private void TrackChanges()  
        {  
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))  
            {  
                if (entry.Entity is BaseEntity)  
                {  
                    var auditable = entry.Entity as BaseEntity;  
                    if (entry.State == EntityState.Added)  
                    {  
                        auditable.CreatedUser = UserProvider;//  
                        auditable.CreatedDate = TimestampProvider();  
                        auditable.ModifiedDate = TimestampProvider();  
                    }  
                    else  
                    {  
                        auditable.ModifiedUser = UserProvider;  
                        auditable.ModifiedDate = TimestampProvider();  
                    }  
                }  
            }  
        }  
    }
}