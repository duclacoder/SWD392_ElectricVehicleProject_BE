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

    public virtual DbSet<PaymentsMethod> PaymentsMethods { get; set; }

    public virtual DbSet<PostPackage> PostPackages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPackage> UserPackages { get; set; }

    public virtual DbSet<UserPost> UserPosts { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleImage> VehicleImages { get; set; }

    public virtual DbSet<VehicleInspection> VehicleInspections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local); Database=SWD392_SE1834_G2_T1;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.ActivitiesId).HasName("PK__Activiti__382149557AA13D31");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ReferenceType).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Payment).WithMany(p => p.Activities)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Activitie__Payme__71D1E811");

            entity.HasOne(d => d.User).WithMany(p => p.Activities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Activitie__UserI__70DDC3D8");
        });

        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasKey(e => e.AuctionsId).HasName("PK__Auctions__926DD805E0446901");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.EntryFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FeePerMinute).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OpenFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.AuctionsFee).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.AuctionsFeeId)
                .HasConstraintName("FK__Auctions__Auctio__6477ECF3");

            entity.HasOne(d => d.Seller).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Auctions__Seller__6383C8BA");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Auctions__Vehicl__628FA481");
        });

        modelBuilder.Entity<AuctionBid>(entity =>
        {
            entity.HasKey(e => e.AuctionBidsId).HasName("PK__AuctionB__9A008868AEAE735D");

            entity.Property(e => e.BidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BidTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Auction).WithMany(p => p.AuctionBids)
                .HasForeignKey(d => d.AuctionId)
                .HasConstraintName("FK__AuctionBi__Aucti__6754599E");

            entity.HasOne(d => d.Bidder).WithMany(p => p.AuctionBids)
                .HasForeignKey(d => d.BidderId)
                .HasConstraintName("FK__AuctionBi__Bidde__68487DD7");
        });

        modelBuilder.Entity<AuctionsFee>(entity =>
        {
            entity.HasKey(e => e.AuctionsFeeId).HasName("PK__Auctions__3F45AFA899EE9166");

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
            entity.HasKey(e => e.BatteriesId).HasName("PK__Batterie__C34B8A741FAC9ECD");

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
                .HasConstraintName("FK__Batteries__UserI__403A8C7D");
        });

        modelBuilder.Entity<BatteryImage>(entity =>
        {
            entity.HasKey(e => e.BatteryImagesId).HasName("PK__BatteryI__3938BC565E490704");

            entity.Property(e => e.ImageUrl).HasColumnType("text");

            entity.HasOne(d => d.Battery).WithMany(p => p.BatteryImages)
                .HasForeignKey(d => d.BatteryId)
                .HasConstraintName("FK__BatteryIm__Batte__4316F928");
        });

        modelBuilder.Entity<BuySell>(entity =>
        {
            entity.HasKey(e => e.BuySellId).HasName("PK__BuySell__2E36A9702BF27E0A");

            entity.ToTable("BuySell");

            entity.Property(e => e.BuyDate).HasColumnType("datetime");
            entity.Property(e => e.CarPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Battery).WithMany(p => p.BuySells)
                .HasForeignKey(d => d.BatteryId)
                .HasConstraintName("FK__BuySell__Battery__571DF1D5");

            entity.HasOne(d => d.Buyer).WithMany(p => p.BuySellBuyers)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK__BuySell__BuyerId__5441852A");

            entity.HasOne(d => d.Seller).WithMany(p => p.BuySellSellers)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__BuySell__SellerI__5535A963");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.BuySells)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__BuySell__Vehicle__5629CD9C");
        });

        modelBuilder.Entity<InspectionFee>(entity =>
        {
            entity.HasKey(e => e.InspectionFeesId).HasName("PK__Inspecti__D0FFEC45A7C23692");

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
            entity.HasKey(e => e.PaymentsId).HasName("PK__Payments__FD75744ACC8BA872");

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

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__Payments__Paymen__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payments__UserId__5EBF139D");
        });

        modelBuilder.Entity<PaymentsMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__Payments__DC31C1D3011DD833");

            entity.HasIndex(e => e.MethodCode, "UQ__Payments__11E9210D4DE25D16").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Gateway).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LogoUrl).HasMaxLength(500);
            entity.Property(e => e.MethodCode).HasMaxLength(50);
            entity.Property(e => e.MethodName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<PostPackage>(entity =>
        {
            entity.HasKey(e => e.PostPackagesId).HasName("PK__PostPack__579547786220E22C");

            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.PackageName).HasMaxLength(255);
            entity.Property(e => e.PostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolesId).HasName("PK__Roles__C4B2784021709B81");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F6FE92EA74").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UsersId).HasName("PK__Users__A349B0627BB5D388");

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359E4D9B91C2").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053410071B6D").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456B65AF859").IsUnique();

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
                .HasConstraintName("FK__Users__RoleId__3D5E1FD2");
        });

        modelBuilder.Entity<UserPackage>(entity =>
        {
            entity.HasKey(e => e.UserPackagesId).HasName("PK__UserPack__48B463ECE534374E");

            entity.Property(e => e.Currency).HasMaxLength(100);
            entity.Property(e => e.PurchasedAt).HasColumnType("datetime");
            entity.Property(e => e.PurchasedAtPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Package).WithMany(p => p.UserPackages)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__UserPacka__Packa__6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.UserPackages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserPacka__UserI__6D0D32F4");
        });

        modelBuilder.Entity<UserPost>(entity =>
        {
            entity.HasKey(e => e.UserPostsId).HasName("PK__UserPost__4364383FC8F1316F");

            entity.Property(e => e.ExpiredAt).HasColumnType("datetime");
            entity.Property(e => e.PostedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Battery).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.BatteryId)
                .HasConstraintName("FK__UserPosts__Batte__76969D2E");

            entity.HasOne(d => d.User).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserPosts__UserI__74AE54BC");

            entity.HasOne(d => d.UserPackage).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.UserPackageId)
                .HasConstraintName("FK__UserPosts__UserP__778AC167");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.UserPosts)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__UserPosts__Vehic__75A278F5");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehiclesId).HasName("PK__Vehicles__C683EFB2F8C6D4D0");

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
                .HasConstraintName("FK__Vehicles__UserId__45F365D3");
        });

        modelBuilder.Entity<VehicleImage>(entity =>
        {
            entity.HasKey(e => e.VehicleImagesId).HasName("PK__VehicleI__872777FC33BAC0FC");

            entity.Property(e => e.ImageUrl).HasColumnType("text");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleImages)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__VehicleIm__Vehic__48CFD27E");
        });

        modelBuilder.Entity<VehicleInspection>(entity =>
        {
            entity.HasKey(e => e.VehicleInspectionsId).HasName("PK__VehicleI__E46514280285A9F7");

            entity.Property(e => e.CancelReason).HasColumnType("text");
            entity.Property(e => e.InspectionDate).HasColumnType("datetime");
            entity.Property(e => e.InspectionFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.InspectionFeeNavigation).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.InspectionFeeId)
                .HasConstraintName("FK__VehicleIn__Inspe__4F7CD00D");

            entity.HasOne(d => d.Staff).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__VehicleIn__Staff__4E88ABD4");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleInspections)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__VehicleIn__Vehic__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
