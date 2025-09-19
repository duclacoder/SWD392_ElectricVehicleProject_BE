using EV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EV.Infrastructure.DBContext;

public partial class Swd392Se1834G2T1Context : DbContext
{
    public Swd392Se1834G2T1Context()
    {
    }

    public Swd392Se1834G2T1Context(DbContextOptions<Swd392Se1834G2T1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<AuctionBid> AuctionBids { get; set; }

    public virtual DbSet<AuctionsFee> AuctionsFees { get; set; }

    public virtual DbSet<Battery> Batteries { get; set; }

    public virtual DbSet<BatteryImage> BatteryImages { get; set; }

    public virtual DbSet<BuySell> BuySells { get; set; }

    public virtual DbSet<InspectionFee> InspectionFees { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PostPackage> PostPackages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPackage> UserPackages { get; set; }

    public virtual DbSet<UserPost> UserPosts { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleImage> VehicleImages { get; set; }

    public virtual DbSet<VehicleInspection> VehicleInspections { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=1;database=SWD392_SE1834_G2_T1;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.ActivitiesId).HasName("PK__Activiti__3821495541CC25AD");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ReferenceType).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Payment).WithMany(p => p.Activities)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Activitie__Payme__59063A47");

            entity.HasOne(d => d.User).WithMany(p => p.Activities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Activitie__UserI__5812160E");
        });

        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasKey(e => e.AuctionsId).HasName("PK__Auctions__926DD8052B3239DF");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.EntryFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FeePerMinute).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OpenFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.AuctionsFee).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.AuctionsFeeId)
                .HasConstraintName("FK__Auctions__Auctio__4BAC3F29");

            entity.HasOne(d => d.Seller).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Auctions__Seller__4AB81AF0");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Auctions__Vehicl__49C3F6B7");
        });

        modelBuilder.Entity<AuctionBid>(entity =>
        {
            entity.HasKey(e => e.AuctionBidsId).HasName("PK__AuctionB__9A008868D276A079");

            entity.Property(e => e.BidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BidTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Auction).WithMany(p => p.AuctionBids)
                .HasForeignKey(d => d.AuctionId)
                .HasConstraintName("FK__AuctionBi__Aucti__4E88ABD4");

            entity.HasOne(d => d.Bidder).WithMany(p => p.AuctionBids)
                .HasForeignKey(d => d.BidderId)
                .HasConstraintName("FK__AuctionBi__Bidde__4F7CD00D");
        });

        modelBuilder.Entity<AuctionsFee>(entity =>
        {
            entity.HasKey(e => e.AuctionsFeeId).HasName("PK__Auctions__3F45AFA8BBA3FFA5");

            entity.ToTable("AuctionsFee");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EntryFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FeePerMinute).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Battery>(entity =>
        {
            entity.HasKey(e => e.BatteriesId).HasName("PK__Batterie__C34B8A741175F004");

            entity.Property(e => e.BatteryName).HasMaxLength(255);
            entity.Property(e => e.Brand).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Voltage).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Batteries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Batteries__UserI__2D27B809");
        });

        modelBuilder.Entity<BatteryImage>(entity =>
        {
            entity.HasKey(e => e.BatteryImagesId).HasName("PK__BatteryI__3938BC568AAA4163");

            entity.Property(e => e.ImageUrl).HasColumnType("text");

            entity.HasOne(d => d.Battery).WithMany(p => p.BatteryImages)
                .HasForeignKey(d => d.BatteryId)
                .HasConstraintName("FK__BatteryIm__Batte__300424B4");
        });

        modelBuilder.Entity<BuySell>(entity =>
        {
            entity.HasKey(e => e.BuySellId).HasName("PK__BuySell__2E36A9706C88F6F7");

            entity.ToTable("BuySell");

            entity.Property(e => e.BuyDate).HasColumnType("datetime");
            entity.Property(e => e.CarPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Battery).WithMany(p => p.BuySells)
                .HasForeignKey(d => d.BatteryId)
                .HasConstraintName("FK__BuySell__Battery__440B1D61");

            entity.HasOne(d => d.Buyer).WithMany(p => p.BuySellBuyers)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK__BuySell__BuyerId__412EB0B6");

            entity.HasOne(d => d.Seller).WithMany(p => p.BuySellSellers)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__BuySell__SellerI__4222D4EF");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.BuySells)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__BuySell__Vehicle__4316F928");
        });

        modelBuilder.Entity<InspectionFee>(entity =>
        {
            entity.HasKey(e => e.InspectionFeesId).HasName("PK__Inspecti__D0FFEC457E50B47D");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.FeeAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentsId).HasName("PK__Payments__FD75744A5DDF5C32");

            entity.Property(e => e.AccountNumber).HasMaxLength(100);
            entity.Property(e => e.Accumulated).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Gateway).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransferAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransferType).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payments__UserId__46E78A0C");
        });

        modelBuilder.Entity<PostPackage>(entity =>
        {
            entity.HasKey(e => e.PostPackagesId).HasName("PK__PostPack__57954778D9AB0FF5");

            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.PackageName).HasMaxLength(255);
            entity.Property(e => e.PostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolesId).HasName("PK__Roles__C4B27840D59D21FC");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F64D31FB7B").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PK__Users__A349B062F87DB6A3");

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359EFA43384F").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053466961CD3").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456FDA530FF").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasColumnType("text");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__2A4B4B5E");
        });

        modelBuilder.Entity<UserPackage>(entity =>
        {
            entity.HasKey(e => e.UserPackagesId).HasName("PK__UserPack__48B463EC083A4955");

            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.PurchasedAt).HasColumnType("datetime");
            entity.Property(e => e.PurchasedAtPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Package).WithMany(p => p.UserPackages)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__UserPacka__Packa__5535A963");

            entity.HasOne(d => d.User).WithMany(p => p.UserPackages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserPacka__UserI__5441852A");
        });

        modelBuilder.Entity<UserPost>(entity =>
        {
            entity.HasKey(e => e.UserPostsId).HasName("PK__UserPost__4364383F2E121A89");

            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.PostedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Battery).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.BatteryId)
                .HasConstraintName("FK__UserPosts__Batte__5DCAEF64");

            entity.HasOne(d => d.User).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserPosts__UserI__5BE2A6F2");

            entity.HasOne(d => d.UserPackage).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.UserPackageId)
                .HasConstraintName("FK__UserPosts__UserP__5EBF139D");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__UserPosts__Vehic__5CD6CB2B");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehiclesId).HasName("PK__Vehicles__C683EFB27693999F");

            entity.Property(e => e.Acceleration).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.BatteryCapacity).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.BatteryStatus).HasMaxLength(100);
            entity.Property(e => e.BodyType).HasMaxLength(50);
            entity.Property(e => e.Brand).HasMaxLength(255);
            entity.Property(e => e.ChargingTimeHours).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.ConnectorType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Model).HasMaxLength(255);
            entity.Property(e => e.MotorPowerKw).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.VehicleName).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Vehicles__UserId__32E0915F");
        });

        modelBuilder.Entity<VehicleImage>(entity =>
        {
            entity.HasKey(e => e.VehicleImagesId).HasName("PK__VehicleI__872777FCC49099A5");

            entity.Property(e => e.ImageUrl).HasColumnType("text");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleImages)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__VehicleIm__Vehic__35BCFE0A");
        });

        modelBuilder.Entity<VehicleInspection>(entity =>
        {
            entity.HasKey(e => e.VehicleInspectionsId).HasName("PK__VehicleI__E46514281CF092FA");

            entity.Property(e => e.CancelReason).HasColumnType("text");
            entity.Property(e => e.InspectionDate).HasColumnType("datetime");
            entity.Property(e => e.InspectionFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.InspectionFeeNavigation).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.InspectionFeeId)
                .HasConstraintName("FK__VehicleIn__Inspe__3C69FB99");

            entity.HasOne(d => d.Staff).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__VehicleIn__Staff__3B75D760");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__VehicleIn__Vehic__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
