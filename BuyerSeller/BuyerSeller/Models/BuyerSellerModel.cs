namespace BuyerSeller.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BuyerSellerModel : DbContext
    {
        public BuyerSellerModel()
            : base("name=BuyerSellerModel")
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Buyer_OrderDetailsMapping> Buyer_OrderDetailsMapping { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<SellerAdvertise> SellerAdvertises { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Role_PermissionMapping> Role_PermissionMapping { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<LanguageForm> LanguageForms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellerAdvertise>()
               .Property(e => e.ActualPrice)
               .HasPrecision(18, 0);

            modelBuilder.Entity<SellerAdvertise>()
                .Property(e => e.DiscountPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Permission>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Rating)
                .HasPrecision(18, 0);

            modelBuilder.Entity<User>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OrderDetails)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.SellerID);

           
        }
    }
}
