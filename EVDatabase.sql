use master
go

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SWD392_SE1834_G2_T1')
	BEGIN
		ALTER DATABASE SWD392_SE1834_G2_T1 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
		DROP DATABASE SWD392_SE1834_G2_T1;
	end
go

create database SWD392_SE1834_G2_T1
go

use SWD392_SE1834_G2_T1
go

CREATE TABLE Roles (
    RolesId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) UNIQUE,
);
go

CREATE TABLE Users (
    UsersId INT PRIMARY KEY IDENTITY(1,1),
	UserName NVARCHAR(255) UNIQUE,
    FullName NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE,
    Phone NVARCHAR(50) UNIQUE,
    Password VARCHAR(255),
    ImageUrl text,
    RoleId INT FOREIGN KEY REFERENCES Roles(RolesId),
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    Wallet DECIMAL(18,2),
    Status NVARCHAR(50) --Active, InActive
);
go

--------------------------------------------------
-- BATTERIES
--------------------------------------------------

CREATE TABLE Batteries (
    BatteriesId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId), -- seller
    BatteryName NVARCHAR(255),
    Description TEXT,
    Brand NVARCHAR(255),
    Capacity INT,          -- in Ah
    Voltage DECIMAL(5,2),  -- e.g. 12.0, 48.0
    WarrantyMonths INT,
    Price DECIMAL(18,2),
    Currency NVARCHAR(100), --VND, USD
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    Status NVARCHAR(50) -- available, sold, etc.
);
go

CREATE TABLE BatteryImages (
    BatteryImagesId INT PRIMARY KEY IDENTITY(1,1),
    BatteryId INT FOREIGN KEY REFERENCES Batteries(BatteriesId),
    ImageUrl TEXT,
);
go

--------------------------------------------------
-- VEHICLES
--------------------------------------------------

CREATE TABLE Vehicles (
    VehiclesId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId),
    VehicleName NVARCHAR(255),
    Description TEXT,
    Brand NVARCHAR(255),
    Model NVARCHAR(255),
	Color NVARCHAR(50),
	Seats INT,
	BodyType NVARCHAR(50),             -- SUV, Sedan, etc.
	BatteryCapacity DECIMAL(5,2),      -- in kWh
	RangeKm INT,                       -- range per charge
	ChargingTimeHours DECIMAL(4,2),    -- charging time
	FastChargingSupport BIT,
	MotorPowerKw DECIMAL(6,2),         -- motor output
	TopSpeedKph INT,
    Acceleration DECIMAL(4,2),         -- 0-100 km/h
	ConnectorType NVARCHAR(50),
    Year INT,
    Km INT,
    BatteryStatus NVARCHAR(100),
	WarrantyMonths INT,
    Price DECIMAL(18,2),
	Currency NVARCHAR(100), --VND, USD
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
	Verified BIT,
    Status NVARCHAR(50)
);
go

CREATE TABLE VehicleImages (
    VehicleImagesId INT PRIMARY KEY IDENTITY(1,1),
    VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehiclesId),
    ImageUrl text,
);
go

CREATE TABLE InspectionFees (
    InspectionFeesId INT PRIMARY KEY IDENTITY(1,1),
    Description TEXT,
    FeeAmount DECIMAL(18,2),
    Currency NVARCHAR(100), --VND, USD
    Type NVARCHAR(50), --fixed, percentage
    InspectionDays INT,
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    Status NVARCHAR(50) --Active, InActive
);
go

CREATE TABLE VehicleInspections (
    VehicleInspectionsId INT PRIMARY KEY IDENTITY(1,1),
    VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehiclesId),
    StaffId INT FOREIGN KEY REFERENCES Users(UsersId),
    InspectionDate DATETIME,
    Notes TEXT,
	CancelReason TEXT,
    InspectionFeeId INT FOREIGN KEY REFERENCES InspectionFees(InspectionFeesId),
    InspectionFee DECIMAL(18,2),
    Status NVARCHAR(50)
);
go

CREATE TABLE AuctionsFee (
    AuctionsFeeId INT PRIMARY KEY IDENTITY(1,1),
    Description Nvarchar(255),
    FeePerMinute DECIMAL(18,2),
    EntryFee DECIMAL(18,2),
    Currency NVARCHAR(50),
    Type NVARCHAR(50), --fixed, percentage
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    Status NVARCHAR(50)
);
go

--------------------------------------------------
-- TRANSACTIONS
--------------------------------------------------

CREATE TABLE BuySell (
    BuySellId INT PRIMARY KEY IDENTITY(1,1),
    BuyerId INT FOREIGN KEY REFERENCES Users(UsersId),
    SellerId INT FOREIGN KEY REFERENCES Users(UsersId),
    VehicleId INT NULL FOREIGN KEY REFERENCES Vehicles(VehiclesId),
    BatteryId INT NULL FOREIGN KEY REFERENCES Batteries(BatteriesId),
    BuyDate DATETIME,
    CarPrice DECIMAL(18,2),
	Currency NVARCHAR(100), --VND, USD
    Status NVARCHAR(50)
);
go

CREATE TABLE PaymentsMethods (
    PaymentMethodId INT PRIMARY KEY IDENTITY(1,1),
    MethodCode NVARCHAR(50) UNIQUE NOT NULL,     -- Ví dụ: 'MOMO', 'ZALOPAY', 'BANK', 'CREDIT'
    MethodName NVARCHAR(100) NOT NULL,           -- Tên hiển thị: "Momo Wallet", "ZaloPay", ...
    Gateway NVARCHAR(255),                       -- Cổng kết nối (nếu có)
    Description NVARCHAR(500),                   -- Mô tả thêm
    LogoUrl NVARCHAR(500),                       -- Link logo để hiển thị trong UI
    IsActive BIT DEFAULT 1,                      -- Có đang được sử dụng hay không
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
);
go

CREATE TABLE Payments (
    PaymentsId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId),
    PaymentMethodId INT FOREIGN KEY REFERENCES PaymentsMethods(PaymentMethodId),
    Gateway NVARCHAR(255),
    TransactionDate DATETIME,
    AccountNumber NVARCHAR(100),
    Content NVARCHAR(500),
    TransferType NVARCHAR(50),
    TransferAmount DECIMAL(18,2),
    Currency NVARCHAR(100),          -- VND, USD
    Accumulated DECIMAL(18,2),       -- Remain cash in account
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    ReferenceId INT NULL,            
    ReferenceType NVARCHAR(100) NULL,    
    Status NVARCHAR(50)
);
GO

--------------------------------------------------
-- AUCTION
--------------------------------------------------

CREATE TABLE Auctions (
    AuctionsId INT PRIMARY KEY IDENTITY(1,1),
    VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehiclesId),
    SellerId INT FOREIGN KEY REFERENCES Users(UsersId),
    StartPrice DECIMAL(18,2),
    StartTime DATETIME,
    EndTime DATETIME,
    AuctionsFeeId INT FOREIGN KEY REFERENCES AuctionsFee(AuctionsFeeId),
    FeePerMinute DECIMAL(18,2),
    OpenFee DECIMAL(18,2),
    EntryFee DECIMAL(18,2),
    Status NVARCHAR(50)
);
go

CREATE TABLE AuctionParticipants (
    AuctionParticipantId INT IDENTITY PRIMARY KEY,
    PaymentsId INT FOREIGN KEY REFERENCES  Payments(PaymentsId),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId),
    AuctionsId INT FOREIGN KEY REFERENCES Auctions(AuctionsId),
    AuctionId INT NOT NULL,
    DepositAmount DECIMAL(18,2) NOT NULL,
    DepositTime DATETIME,
    RefundStatus VARCHAR(50),
    Status VARCHAR(50), 
    IsWinningBid BIT DEFAULT 0,
);
go

CREATE TABLE AuctionBids (
    AuctionBidsId INT PRIMARY KEY IDENTITY(1,1),
    AuctionId INT FOREIGN KEY REFERENCES Auctions(AuctionsId),
    AuctionParticipantId INT FOREIGN KEY REFERENCES AuctionParticipants(AuctionParticipantId),
    BidderId INT FOREIGN KEY REFERENCES Users(UsersId),
    BidAmount DECIMAL(18,2),
    BidTime DATETIME,
    Status NVARCHAR(50),
);
go

--------------------------------------------------
-- PACKAGES
--------------------------------------------------

CREATE TABLE PostPackages (
    PostPackagesId INT PRIMARY KEY IDENTITY(1,1),
    PackageName NVARCHAR(255),
    Description TEXT,
    PostPrice DECIMAL(18,2),
	Currency NVARCHAR(100), --VND, USD
    PostDuration INT,
    Status NVARCHAR(50)
);
go

CREATE TABLE UserPackages (
    UserPackagesId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId),
    PackageId INT FOREIGN KEY REFERENCES PostPackages(PostPackagesId),
    PaymentsId INT FOREIGN KEY REFERENCES Payments(PaymentsId),
	PurchasedPostDuration INT,
    PurchasedAtPrice DECIMAL(18,2),
	Currency NVARCHAR(100), --VND, USD
    PurchasedAt DATETIME,
    Status NVARCHAR(50)
);
go
--------------------------------------------------
-- HISTORY
--------------------------------------------------

CREATE TABLE Activities (
    ActivitiesId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId),
    PaymentId INT NULL FOREIGN KEY REFERENCES Payments(PaymentsId),
    Action NVARCHAR(50),
    ReferenceId INT,
    ReferenceType NVARCHAR(100),
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    Status NVARCHAR(50)
);
go

CREATE TABLE UserPosts (
    UserPostsId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId),
    VehicleId INT NULL FOREIGN KEY REFERENCES Vehicles(VehiclesId),
    BatteryId INT NULL FOREIGN KEY REFERENCES Batteries(BatteriesId),
    UserPackageId INT FOREIGN KEY REFERENCES UserPackages(UserPackagesId),
    PostedAt DATETIME,
    ExpiredAt DATETIME,
    Status NVARCHAR(50) -- active, expired, removed
);
go


-- Sample Data for SWD392_EV_Management_Project Database

--------------------------------------------------
-- ROLES DATA
--------------------------------------------------
INSERT INTO Roles (Name) VALUES 
('Member'),
('Staff'),
('Admin');

-- USERS
INSERT INTO Users (UserName, FullName, Email, Phone, Password, ImageUrl, RoleId, CreatedAt, UpdatedAt, Wallet, Status) VALUES
('admin', N'Bố Admin', 'ducpvse183843@fpt.edu.vn', '0901234567', '1', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSJEqnKT0022XaMCyb6K37bte9OIjdUGLCHTA&s', 3, '2024-01-15 08:00:00', '2024-01-15 08:00:00', 0,'Active'),
('staff', N'Má Staff', 'staff1@gm.c', '0901234568', '1', 'https://tiemchupanh.com/wp-content/uploads/2024/07/4ed9efe2b3fd60a339ec23-683x1024.jpg', 2, '2024-01-20 08:00:00', '2024-01-20 08:00:00', 0, 'Active'),
('staff01', N'Bố Staff', 'staff2@gm.c', '0901234569', '1', 'https://chothuestudio.com/wp-content/uploads/2024/07/TCA_3837.jpg', 2, '2024-01-25 08:00:00', '2024-01-25 08:00:00', 0, 'Active'),
('seller01', N'Phạm Minh Hải', 'hai.pham@gmail.com', '0912345678', 'hashed_password_seller1', 'https://example.com/images/seller1.jpg', 1, '2024-02-01 10:00:00', '2024-02-01 10:00:00', 0, 'Active'),
('customer01', N'Võ Thị Lan', 'lan.vo@gmail.com', '0923456789', 'hashed_password_customer1', 'https://example.com/images/customer1.jpg', 1, '2024-02-05 14:30:00', '2024-02-05 14:30:00', 0, 'Active'),
('seller02', N'Hoàng Văn Thắng', 'thang.hoang@gmail.com', '0934567890', 'hashed_password_seller2', 'https://example.com/images/seller2.jpg', 1, '2024-02-10 09:15:00', '2024-02-10 09:15:00', 0, 'Active'),
('customer02', N'Nguyễn Thị Mai', 'mai.nguyen@gmail.com', '0945678901', 'hashed_password_customer2', 'https://example.com/images/customer2.jpg', 1, '2024-02-15 16:20:00', '2024-02-15 16:20:00', 0, 'Active'),
('seller03', N'Đỗ Minh Tuấn', 'tuan.do@gmail.com', '0956789012', 'hashed_password_seller3', 'https://example.com/images/seller3.jpg', 1, '2024-02-20 11:45:00', '2024-02-20 11:45:00', 0, 'Active'),
('customer03', N'Trần Văn Dũng', 'dung.tran@gmail.com', '0967890123', 'hashed_password_customer3', 'https://example.com/images/customer3.jpg', 1, '2024-02-25 08:30:00', '2024-02-25 08:30:00', 0, 'Active'),
('seller04', N'Lê Thị Hương', 'huong.le@gmail.com', '0978901234', 'hashed_password_seller4', 'https://example.com/images/seller4.jpg', 1, '2024-03-01 13:20:00', '2024-03-01 13:20:00', 0, 'Active');

-- BATTERIES
INSERT INTO Batteries (UserId, BatteryName, Description, Brand, Capacity, Voltage, WarrantyMonths, Price, Currency, CreatedAt, UpdatedAt, Status) VALUES
(4, N'Pin Lithium VinFast 50Ah', N'Pin lithium chất lượng cao cho xe điện VinFast, còn mới 95%', 'VinFast', 50, 48.0, 24, 15000000, 'VND', '2024-03-01 09:00:00', '2024-03-01 09:00:00', 'Available'),
(6, N'Tesla Model 3 Battery Pack', N'Genuine Tesla battery pack, excellent condition, 90% health', 'Tesla', 75, 350.0, 36, 8500, 'USD', '2024-03-05 14:20:00', '2024-03-05 14:20:00', 'Available'),
(8, N'BYD Blade Battery 60Ah', N'Advanced BYD Blade battery technology, warranty còn 28 tháng', 'BYD', 60, 52.0, 30, 18000000, 'VND', '2024-03-10 10:30:00', '2024-03-10 10:30:00', 'Available'),
(4, N'LiFePO4 Battery 40Ah', N'Long-lasting lithium iron phosphate battery', 'CATL', 40, 48.0, 60, 12000000, 'VND', '2024-03-15 08:45:00', '2024-03-15 08:45:00', 'Sold'),
(10, N'Hyundai IONIQ Battery 55Ah', N'Pin chính hãng Hyundai, độ chai 85%, còn BH 18 tháng', 'Hyundai', 55, 52.0, 18, 16500000, 'VND', '2024-03-18 11:00:00', '2024-03-18 11:00:00', 'Available'),
(6, N'VinFast VF9 Battery Pack', N'Pin zin từ VinFast VF9, dung lượng lớn 123kWh', 'VinFast', 123, 400.0, 48, 45000000, 'VND', '2024-03-20 15:30:00', '2024-03-20 15:30:00', 'Available');

-- BATTERY IMAGES
INSERT INTO BatteryImages (BatteryId, ImageUrl) VALUES
(1, 'https://example.com/batteries/vinfast_battery_1.jpg'),
(1, 'https://example.com/batteries/vinfast_battery_2.jpg'),
(2, 'https://example.com/batteries/tesla_battery_1.jpg'),
(2, 'https://example.com/batteries/tesla_battery_2.jpg'),
(3, 'https://example.com/batteries/byd_battery_1.jpg'),
(3, 'https://example.com/batteries/byd_battery_2.jpg'),
(4, 'https://example.com/batteries/lifepo4_battery_1.jpg'),
(5, 'https://example.com/batteries/hyundai_battery_1.jpg'),
(5, 'https://example.com/batteries/hyundai_battery_2.jpg'),
(6, 'https://example.com/batteries/vinfast_vf9_battery_1.jpg');

-- VEHICLES
INSERT INTO Vehicles (UserId, VehicleName, Description, Brand, Model, Color, Seats, BodyType, BatteryCapacity, RangeKm, ChargingTimeHours, FastChargingSupport, MotorPowerKw, TopSpeedKph, Acceleration, ConnectorType, Year, Km, BatteryStatus, WarrantyMonths, Price, Currency, CreatedAt, UpdatedAt, Verified, Status) VALUES
(4, N'VinFast VF8 Plus', N'SUV điện cao cấp, tình trạng như mới, full option', 'VinFast', 'VF8 Plus', N'Xanh đen', 5, 'SUV', 87.7, 420, 6.5, 1, 300, 200, 5.9, 'CCS2', 2023, 5000, N'Excellent', 48, 1250000000, 'VND', '2024-03-01 10:00:00', '2024-03-01 10:00:00', 1, 'Available'),
(6, N'Tesla Model Y Long Range', N'Premium electric SUV with autopilot, white interior', 'Tesla', 'Model Y', 'Pearl White', 7, 'SUV', 75.0, 526, 5.0, 1, 346, 217, 4.8, 'CCS2', 2022, 15000, 'Very Good', 24, 52000, 'USD', '2024-03-05 11:30:00', '2024-03-05 11:30:00', 1, 'Available'),
(8, N'BYD Tang EV', N'7-seat electric SUV with luxury features, chạy êm', 'BYD', 'Tang EV', 'Midnight Black', 7, 'SUV', 86.4, 505, 7.0, 1, 380, 180, 6.2, 'GB/T', 2023, 8000, 'Excellent', 36, 980000000, 'VND', '2024-03-10 09:15:00', '2024-03-10 09:15:00', 1, 'Available'),
(4, N'VinFast VF9 Premium', N'Flagship electric SUV, xe chủ tịch đi', 'VinFast', 'VF9', N'Trắng ngọc trai', 7, 'SUV', 123.0, 594, 8.0, 1, 408, 200, 6.5, 'CCS2', 2024, 2000, 'Like New', 60, 1690000000, 'VND', '2024-03-15 14:00:00', '2024-03-15 14:00:00', 0, 'Pending Verification'),
(6, N'Hyundai IONIQ 6', N'Sleek electric sedan with great efficiency', 'Hyundai', 'IONIQ 6', 'Gravity Gold', 5, 'Sedan', 77.4, 614, 4.5, 1, 239, 185, 7.4, 'CCS2', 2023, 12000, 'Good', 24, 45000, 'USD', '2024-03-20 16:30:00', '2024-03-20 16:30:00', 1, 'Sold'),
(10, N'VinFast VF5 Plus', N'Xe điện đô thị nhỏ gọn, tiết kiệm, phù hợp gia đình trẻ', 'VinFast', 'VF5 Plus', N'Đỏ cam', 5, 'Hatchback', 37.2, 305, 5.0, 1, 100, 140, 9.5, 'CCS2', 2024, 3500, 'Excellent', 60, 550000000, 'VND', '2024-03-22 10:15:00', '2024-03-22 10:15:00', 1, 'Available'),
(9, N'Tesla Model 3 Standard Range', N'Best-selling electric sedan, autopilot capable', 'Tesla', 'Model 3', 'Midnight Silver', 5, 'Sedan', 60.0, 491, 4.0, 1, 239, 225, 5.3, 'CCS2', 2021, 28000, 'Good', 12, 35000, 'USD', '2024-03-24 13:45:00', '2024-03-24 13:45:00', 1, 'Available'),
(10, N'BYD Dolphin', N'Xe điện compact, phong cách trẻ trung, tiện nghi', 'BYD', 'Dolphin', N'Xanh dương', 5, 'Hatchback', 44.9, 405, 5.5, 1, 150, 160, 7.5, 'GB/T', 2023, 6800, 'Excellent', 42, 620000000, 'VND', '2024-03-25 09:30:00', '2024-03-25 09:30:00', 1, 'Available');

-- VEHICLE IMAGES
INSERT INTO VehicleImages (VehicleId, ImageUrl) VALUES
(1, 'https://example.com/vehicles/vf8_exterior_1.jpg'),
(1, 'https://example.com/vehicles/vf8_exterior_2.jpg'),
(1, 'https://example.com/vehicles/vf8_interior_1.jpg'),
(2, 'https://example.com/vehicles/tesla_model_y_1.jpg'),
(2, 'https://example.com/vehicles/tesla_model_y_2.jpg'),
(2, 'https://example.com/vehicles/tesla_model_y_interior.jpg'),
(3, 'https://example.com/vehicles/byd_tang_1.jpg'),
(3, 'https://example.com/vehicles/byd_tang_2.jpg'),
(4, 'https://example.com/vehicles/vf9_premium_1.jpg'),
(4, 'https://example.com/vehicles/vf9_premium_2.jpg'),
(5, 'https://example.com/vehicles/ioniq6_1.jpg'),
(5, 'https://example.com/vehicles/ioniq6_2.jpg'),
(6, 'https://example.com/vehicles/vf5_1.jpg'),
(6, 'https://example.com/vehicles/vf5_2.jpg'),
(7, 'https://example.com/vehicles/tesla_model3_1.jpg'),
(8, 'https://example.com/vehicles/byd_dolphin_1.jpg'),
(8, 'https://example.com/vehicles/byd_dolphin_2.jpg');

-- INSPECTION FEES
INSERT INTO InspectionFees (Description, FeeAmount, Currency, Type, InspectionDays, CreatedAt, UpdatedAt, Status) VALUES
(N'Phí kiểm định xe điện cơ bản', 2000000, 'VND', 'fixed', 3, '2024-01-01 00:00:00', '2024-01-01 00:00:00', 'Active'),
(N'Phí kiểm định xe điện cao cấp', 3500000, 'VND', 'fixed', 5, '2024-01-01 00:00:00', '2024-01-01 00:00:00', 'Active'),
(N'Premium Vehicle Inspection', 150, 'USD', 'fixed', 7, '2024-01-01 00:00:00', '2024-01-01 00:00:00', 'Active'),
(N'Kiểm định xe điện express (24h)', 5000000, 'VND', 'fixed', 1, '2024-01-01 00:00:00', '2024-01-01 00:00:00', 'Active');

-- VEHICLE INSPECTIONS
INSERT INTO VehicleInspections (VehicleId, StaffId, InspectionDate, Notes, CancelReason, InspectionFeeId, InspectionFee, Status) VALUES
(1, 2, '2024-03-02 09:00:00', N'Xe trong tình trạng tốt, pin hoạt động bình thường, độ chai pin 5%', NULL, 2, 3500000, 'Completed'),
(2, 3, '2024-03-06 10:30:00', N'Vehicle in excellent condition, autopilot features working perfectly', NULL, 3, 150, 'Completed'),
(3, 2, '2024-03-11 14:00:00', N'All systems operational, battery health at 95%, phanh ABS tốt', NULL, 2, 3500000, 'Completed'),
(4, 3, '2024-03-18 11:00:00', N'New vehicle, all systems check passed, đang chờ xác minh giấy tờ', NULL, 2, 3500000, 'Scheduled'),
(6, 2, '2024-03-23 08:30:00', N'Xe nhỏ gọn, phù hợp đô thị, pin tốt, hệ thống điện ổn định', NULL, 1, 2000000, 'Completed'),
(7, 3, '2024-03-25 15:00:00', N'Tesla Model 3 in good working order, minor wear on tires', NULL, 3, 150, 'Completed'),
(8, 2, '2024-03-26 10:00:00', N'BYD Dolphin kiểm tra đạt, pin Blade an toàn, nội thất sạch sẽ', NULL, 1, 2000000, 'Completed');

-- AUCTIONS FEE
INSERT INTO AuctionsFee (Description, FeePerMinute, EntryFee, Currency, Type, CreatedAt, UpdatedAt, Status) VALUES
(N'Phí đấu giá tiêu chuẩn', 50000, 500000, 'VND', 'fixed', '2024-01-01 00:00:00', '2024-01-01 00:00:00', 'Active'),
(N'Premium auction fee', 2, 25, 'USD', 'fixed', '2024-01-01 00:00:00', '2024-01-01 00:00:00', 'Active'),
(N'Phí đấu giá VIP (xe cao cấp)', 100000, 1000000, 'VND', 'fixed', '2024-01-01 00:00:00', '2024-01-01 00:00:00', 'Active');

-- POST PACKAGES
INSERT INTO PostPackages (PackageName, Description, PostPrice, Currency, PostDuration, Status) VALUES
(N'Gói Cơ Bản', N'Đăng tin 30 ngày, hiển thị thông thường', 100000, 'VND', 30, 'Active'),
(N'Gói Nổi Bật', N'Đăng tin 45 ngày, ưu tiên hiển thị', 200000, 'VND', 45, 'Active'),
(N'Gói VIP', N'Đăng tin 60 ngày, hiển thị đầu trang', 350000, 'VND', 60, 'Active'),
(N'Basic Package (USD)', N'30-day listing, standard display', 5, 'USD', 30, 'Active'),
(N'Premium Package (USD)', N'60-day listing, priority display', 15, 'USD', 60, 'Active');

-- PAYMENT METHODS
INSERT INTO PaymentsMethods (MethodCode, MethodName, Gateway, Description, LogoUrl, IsActive, CreatedAt, UpdatedAt) VALUES
('VNPAY', N'VNPay', 'vnpay.vn', N'Cổng thanh toán VNPay', 'https://cdn.vnpay.vn/logo.png', 1, GETDATE(), GETDATE()),
('MOMO', N'MoMo Wallet', 'momo.vn', N'Ví điện tử MoMo', 'https://cdn.momo.vn/logo.png', 1, GETDATE(), GETDATE()),
('ZALOPAY', N'ZaloPay', 'zalopay.vn', N'Ví điện tử ZaloPay', 'https://cdn.zalopay.vn/logo.png', 1, GETDATE(), GETDATE()),
('BANK', N'Chuyển khoản ngân hàng', '', N'Chuyển khoản qua ngân hàng', 'https://example.com/bank-logo.png', 1, GETDATE(), GETDATE()),
('DIRECT', N'Thanh toán trực tiếp', '', N'Thanh toán trực tiếp tại quầy', 'https://example.com/direct-logo.png', 1, GETDATE(), GETDATE());
go

-- Insert remaining data for SWD392_SE1834_G2_T1 Database

--------------------------------------------------
-- PAYMENTS DATA
--------------------------------------------------
INSERT INTO Payments (UserId, PaymentMethodId, Gateway, TransactionDate, AccountNumber, Content, TransferType, TransferAmount, Currency, Accumulated, CreatedAt, UpdatedAt, ReferenceId, ReferenceType , Status) VALUES
-- Seller deposits and withdrawals
(4, 1, 'vnpay.vn', '2024-03-01 08:00:00', 'ACC001', N'Nạp tiền vào tài khoản', 'Deposit', 20000000, 'VND', 20000000, '2024-03-01 08:00:00', '2024-03-01 08:00:00', 1, 'Package','Completed'),
(6, 2, 'momo.vn', '2024-03-05 09:30:00', 'ACC002', N'Nạp tiền qua MoMo', 'Deposit', 10000000, 'VND', 10000000, '2024-03-05 09:30:00', '2024-03-05 09:30:00', 2, 'Auction fee','Completed'),
(8, 3, 'zalopay.vn', '2024-03-10 08:15:00', 'ACC003', N'Nạp tiền qua ZaloPay', 'Deposit', 15000000, 'VND', 15000000, '2024-03-10 08:15:00', '2024-03-10 08:15:00', 1, 'Package','Completed'),
(10, 1, 'vnpay.vn', '2024-03-18 10:00:00', 'ACC004', N'Nạp tiền đấu giá', 'Deposit', 50000000, 'VND', 50000000, '2024-03-18 10:00:00', '2024-03-18 10:00:00', 1, 'Package','Completed'),

-- Customer deposits for auctions and purchases
(5, 2, 'momo.vn', '2024-03-19 14:20:00', 'ACC005', N'Đặt cọc tham gia đấu giá VF8 Plus', 'Deposit', 125000000, 'VND', 125000000, '2024-03-19 14:20:00', '2024-03-19 14:20:00', 1, 'Package','Completed'),
(7, 1, 'vnpay.vn', '2024-03-20 11:00:00', 'ACC006', N'Thanh toán gói đăng tin VIP', 'Payment', 350000, 'VND', 0, '2024-03-20 11:00:00', '2024-03-20 11:00:00', 1, 'Package','Completed'),
(9, 3, 'zalopay.vn', '2024-03-22 15:30:00', 'ACC007', N'Đặt cọc mua xe Tesla Model Y', 'Deposit', 5000, 'USD', 5000, '2024-03-22 15:30:00', '2024-03-22 15:30:00', 1, 'Package','Completed'),
(5, 4, '', '2024-03-25 10:00:00', 'BANK123', N'Thanh toán phí kiểm định', 'Payment', 3500000, 'VND', 121500000, '2024-03-25 10:00:00', '2024-03-25 10:00:00', 1, 'Package','Completed'),

-- Package purchases
(4, 1, 'vnpay.vn', '2024-03-26 09:00:00', 'ACC001', N'Mua gói đăng tin Nổi Bật', 'Payment', 200000, 'VND', 19800000, '2024-03-26 09:00:00', '2024-03-26 09:00:00', 1, 'Package','Completed'),
(6, 2, 'momo.vn', '2024-03-27 10:30:00', 'ACC002', N'Mua gói đăng tin VIP', 'Payment', 350000, 'VND', 9650000, '2024-03-27 10:30:00', '2024-03-27 10:30:00', 1, 'Package','Completed'),
(8, 1, 'vnpay.vn', '2024-03-28 08:00:00', 'ACC003', N'Mua gói đăng tin Cơ Bản', 'Payment', 100000, 'VND', 14900000, '2024-03-28 08:00:00', '2024-03-28 08:00:00', 1, 'Package','Completed'),

-- Inspection fee payments
(4, 1, 'vnpay.vn', '2024-03-02 08:00:00', 'ACC001', N'Phí kiểm định VF8 Plus', 'Payment', 3500000, 'VND', 16500000, '2024-03-02 08:00:00', '2024-03-02 08:00:00', 1, 'Package','Completed'),
(6, 2, 'momo.vn', '2024-03-06 09:00:00', 'ACC002', N'Inspection fee Tesla Model Y', 'Payment', 150, 'USD', 9650000, '2024-03-06 09:00:00', '2024-03-06 09:00:00', 1, 'Package','Completed'),
(8, 3, 'zalopay.vn', '2024-03-11 13:00:00', 'ACC003', N'Phí kiểm định BYD Tang', 'Payment', 3500000, 'VND', 11500000, '2024-03-11 13:00:00', '2024-03-11 13:00:00', 1, 'Package','Completed');

--------------------------------------------------
-- USER PACKAGES DATA
--------------------------------------------------
INSERT INTO UserPackages (UserId, PackageId, PaymentsId, PurchasedPostDuration, PurchasedAtPrice, Currency, PurchasedAt, Status) VALUES
(4, 2, 9, 45, 200000, 'VND', '2024-03-26 09:00:00', 'Active'),
(6, 3, 10, 60, 350000, 'VND', '2024-03-27 10:30:00', 'Active'),
(8, 1, 11, 30, 100000, 'VND', '2024-03-28 08:00:00', 'Active'),
(10, 2, NULL, 45, 200000, 'VND', '2024-03-15 14:00:00', 'Active'),
(4, 1, NULL, 30, 100000, 'VND', '2024-02-01 10:00:00', 'Expired'),
(6, 4, NULL, 30, 5, 'USD', '2024-02-10 11:00:00', 'Expired');

--------------------------------------------------
-- USER POSTS DATA
--------------------------------------------------
INSERT INTO UserPosts (UserId, VehicleId, BatteryId, UserPackageId, PostedAt, ExpiredAt, Status) VALUES
-- Vehicle posts
(4, 1, 1, 1, '2024-03-26 10:00:00', '2024-05-10 10:00:00', 'Active'),
(6, 2, 1, 2, '2024-03-27 11:00:00', '2024-05-26 11:00:00', 'Active'),
(8, 3, 1, 3, '2024-03-28 09:00:00', '2024-04-27 09:00:00', 'Active'),
(10, 6, 1, 2, '2024-03-22 12:00:00', '2024-05-06 12:00:00', 'Active'),
(9, 7, 1, 1, '2024-03-24 14:00:00', '2024-04-23 14:00:00', 'Active'),
(10, 8, 1, 3, '2024-03-25 10:00:00', '2024-04-24 10:00:00', 'Active')
go

--------------------------------------------------
-- AUCTIONS DATA
--------------------------------------------------
-- AUCTIONS
INSERT INTO Auctions (VehicleId, SellerId, StartPrice, StartTime, EndTime, AuctionsFeeId, FeePerMinute, OpenFee, EntryFee, Status) VALUES (1, 4, 1100000000, '2024-04-01 09:00:00', '2024-04-01 12:00:00', 3, 100000, 5000000, 1000000, 'Scheduled');
INSERT INTO Auctions (VehicleId, SellerId, StartPrice, StartTime, EndTime, AuctionsFeeId, FeePerMinute, OpenFee, EntryFee, Status) VALUES (2, 6, 45000, '2024-04-05 14:00:00', '2024-04-05 17:00:00', 2, 2, 100, 25, 'Scheduled');
INSERT INTO Auctions (VehicleId, SellerId, StartPrice, StartTime, EndTime, AuctionsFeeId, FeePerMinute, OpenFee, EntryFee, Status) VALUES (3, 8, 900000000, '2024-03-28 10:00:00', '2024-03-28 13:00:00', 3, 100000, 5000000, 1000000, 'Completed');
INSERT INTO Auctions (VehicleId, SellerId, StartPrice, StartTime, EndTime, AuctionsFeeId, FeePerMinute, OpenFee, EntryFee, Status) VALUES (7, 9, 30000, '2024-04-10 15:00:00', '2024-04-10 18:00:00', 2, 2, 100, 25, 'Scheduled');

-- AUCTION PARTICIPANTS
-- Columns: PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid
INSERT INTO AuctionParticipants (PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid) VALUES (5, 5, 1, 1, 125000000, '2024-03-30 10:00:00', 'NotRefunded', 'Active', 0);
INSERT INTO AuctionParticipants (PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid) VALUES (4, 10, 1, 1, 125000000, '2024-03-30 14:30:00', 'NotRefunded', 'Active', 0);
INSERT INTO AuctionParticipants (PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid) VALUES (7, 9, 2, 2, 5000, '2024-04-04 11:00:00', 'NotRefunded', 'Active', 0);
INSERT INTO AuctionParticipants (PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid) VALUES (5, 5, 2, 2, 5000, '2024-04-04 15:20:00', 'NotRefunded', 'Active', 0);
INSERT INTO AuctionParticipants (PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid) VALUES (5, 5, 3, 3, 100000000, '2024-03-27 09:00:00', 'NotRefunded', 'Completed', 0);
INSERT INTO AuctionParticipants (PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid) VALUES (NULL, 7, 3, 3, 100000000, '2024-03-27 10:30:00', 'NotRefunded', 'Completed', 0);
INSERT INTO AuctionParticipants (PaymentsId, UserId, AuctionsId, AuctionId, DepositAmount, DepositTime, RefundStatus, Status, IsWinningBid) VALUES (4, 10, 3, 3, 100000000, '2024-03-27 11:45:00', 'NotRefunded', 'Completed', 0);

-- AUCTION BIDS
-- Columns: AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status
-- NOTE: AuctionParticipantId ở đây dùng giá trị như trong data mẫu bạn gửi (nếu identity khác sau khi chèn, cần điều chỉnh hoặc lấy bằng subquery)
INSERT INTO AuctionBids (AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status) VALUES (3, 5, 5, 900000000, '2024-03-28 10:05:00', 'Valid');
INSERT INTO AuctionBids (AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status) VALUES (3, 6, 7, 920000000, '2024-03-28 10:15:00', 'Valid');
INSERT INTO AuctionBids (AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status) VALUES (3, 5, 5, 940000000, '2024-03-28 10:30:00', 'Valid');
INSERT INTO AuctionBids (AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status) VALUES (3, 7, 10, 955000000, '2024-03-28 10:45:00', 'Valid');
INSERT INTO AuctionBids (AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status) VALUES (3, 5, 5, 970000000, '2024-03-28 11:00:00', 'Valid');
INSERT INTO AuctionBids (AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status) VALUES (3, 6, 7, 980000000, '2024-03-28 11:20:00', 'Valid');
INSERT INTO AuctionBids (AuctionId, AuctionParticipantId, BidderId, BidAmount, BidTime, Status) VALUES (3, 5, 5, 1000000000, '2024-03-28 12:50:00', 'Valid');


--------------------------------------------------
-- BUY SELL TRANSACTIONS DATA
--------------------------------------------------
INSERT INTO BuySell (BuyerId, SellerId, VehicleId, BatteryId, BuyDate, CarPrice, Currency, Status) VALUES
-- Completed vehicle sales
(5, 6, 5, NULL, '2024-03-21 15:00:00', 45000, 'USD', 'Completed'),
(5, 8, 3, NULL, '2024-03-29 14:30:00', 1000000000, 'VND', 'Completed'),

-- Battery sales
(7, 4, NULL, 4, '2024-03-16 11:00:00', 12000000, 'VND', 'Completed'),

-- Pending transactions
(9, 10, 8, NULL, '2024-03-26 16:00:00', 620000000, 'VND', 'Pending'),
(5, 4, 1, NULL, '2024-04-01 13:00:00', 1250000000, 'VND', 'Pending');

--------------------------------------------------
-- ACTIVITIES DATA
--------------------------------------------------
INSERT INTO Activities (UserId, PaymentId, Action, ReferenceId, ReferenceType, CreatedAt, UpdatedAt, Status) VALUES
-- User registrations
(1, NULL, 'Register', 1, 'User', '2024-01-15 08:00:00', '2024-01-15 08:00:00', 'Completed'),
(2, NULL, 'Register', 2, 'User', '2024-01-20 08:00:00', '2024-01-20 08:00:00', 'Completed'),
(3, NULL, 'Register', 3, 'User', '2024-01-25 08:00:00', '2024-01-25 08:00:00', 'Completed'),
(4, NULL, 'Register', 4, 'User', '2024-02-01 10:00:00', '2024-02-01 10:00:00', 'Completed'),
(5, NULL, 'Register', 5, 'User', '2024-02-05 14:30:00', '2024-02-05 14:30:00', 'Completed'),

-- Vehicle posting activities
(4, NULL, 'PostVehicle', 1, 'Vehicle', '2024-03-01 10:00:00', '2024-03-01 10:00:00', 'Completed'),
(6, NULL, 'PostVehicle', 2, 'Vehicle', '2024-03-05 11:30:00', '2024-03-05 11:30:00', 'Completed'),
(8, NULL, 'PostVehicle', 3, 'Vehicle', '2024-03-10 09:15:00', '2024-03-10 09:15:00', 'Completed'),

-- Battery posting activities
(4, NULL, 'PostBattery', 1, 'Battery', '2024-03-01 09:00:00', '2024-03-01 09:00:00', 'Completed'),
(6, NULL, 'PostBattery', 2, 'Battery', '2024-03-05 14:20:00', '2024-03-05 14:20:00', 'Completed'),
(8, NULL, 'PostBattery', 3, 'Battery', '2024-03-10 10:30:00', '2024-03-10 10:30:00', 'Completed'),

-- Deposit activities
(4, 1, 'Deposit', 1, 'Payment', '2024-03-01 08:00:00', '2024-03-01 08:00:00', 'Completed'),
(6, 2, 'Deposit', 2, 'Payment', '2024-03-05 09:30:00', '2024-03-05 09:30:00', 'Completed'),
(5, 5, 'AuctionDeposit', 1, 'Auction', '2024-03-19 14:20:00', '2024-03-19 14:20:00', 'Completed'),

-- Purchase activities
(5, NULL, 'PurchaseVehicle', 5, 'Vehicle', '2024-03-21 15:00:00', '2024-03-21 15:00:00', 'Completed'),
(7, NULL, 'PurchaseBattery', 4, 'Battery', '2024-03-16 11:00:00', '2024-03-16 11:00:00', 'Completed'),

-- Inspection activities
(4, 12, 'VehicleInspection', 1, 'VehicleInspection', '2024-03-02 09:00:00', '2024-03-02 09:00:00', 'Completed'),
(6, 13, 'VehicleInspection', 2, 'VehicleInspection', '2024-03-06 10:30:00', '2024-03-06 10:30:00', 'Completed'),

-- Package purchase activities
(4, 9, 'PurchasePackage', 1, 'UserPackage', '2024-03-26 09:00:00', '2024-03-26 09:00:00', 'Completed'),
(6, 10, 'PurchasePackage', 2, 'UserPackage', '2024-03-27 10:30:00', '2024-03-27 10:30:00', 'Completed'),

-- Auction activities
(5, 5, 'JoinAuction', 3, 'Auction', '2024-03-27 09:00:00', '2024-03-27 09:00:00', 'Completed'),
(5, 5, 'PlaceBid', 1, 'AuctionBid', '2024-03-28 10:05:00', '2024-03-28 10:05:00', 'Completed'),
(5, NULL, 'WinAuction', 3, 'Auction', '2024-03-28 13:00:00', '2024-03-28 13:00:00', 'Completed');

-- Query để kiểm tra data
SELECT 'Payments' as TableName, COUNT(*) as RecordCount FROM Payments
UNION ALL
SELECT 'UserPackages', COUNT(*) FROM UserPackages
UNION ALL
SELECT 'UserPosts', COUNT(*) FROM UserPosts
UNION ALL
SELECT 'Auctions', COUNT(*) FROM Auctions
UNION ALL
SELECT 'AuctionParticipants', COUNT(*) FROM AuctionParticipants
UNION ALL
SELECT 'AuctionBids', COUNT(*) FROM AuctionBids
UNION ALL
SELECT 'BuySell', COUNT(*) FROM BuySell
UNION ALL
SELECT 'Activities', COUNT(*) FROM Activities;

select * from Users

--select * from Payments

--select * from PaymentsMethods

--select * from UserPosts

--select * from PostPackages

select * from AuctionsFee

select * from Auctions

select * from AuctionBids

select * from AuctionParticipants


