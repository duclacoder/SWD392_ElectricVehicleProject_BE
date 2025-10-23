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
    Status NVARCHAR(50) --Active, InActive
);

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

CREATE TABLE BatteryImages (
    BatteryImagesId INT PRIMARY KEY IDENTITY(1,1),
    BatteryId INT FOREIGN KEY REFERENCES Batteries(BatteriesId),
    ImageUrl TEXT,
);

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

CREATE TABLE VehicleImages (
    VehicleImagesId INT PRIMARY KEY IDENTITY(1,1),
    VehicleId INT FOREIGN KEY REFERENCES Vehicles(VehiclesId),
    ImageUrl text,
);

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

CREATE TABLE AuctionsFee (
    AuctionsFeeId INT PRIMARY KEY IDENTITY(1,1),
    Description TEXT,
    FeePerMinute DECIMAL(18,2),
    EntryFee DECIMAL(18,2),
    Currency NVARCHAR(50),
    Type NVARCHAR(50), --fixed, percentage
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    Status NVARCHAR(50)
);

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
    Currency NVARCHAR(100), --VND, USD
    Accumulated DECIMAL(18,2), --Remain Cash in account
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
    Status NVARCHAR(50)
);

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

CREATE TABLE AuctionBids (
    AuctionBidsId INT PRIMARY KEY IDENTITY(1,1),
    AuctionId INT FOREIGN KEY REFERENCES Auctions(AuctionsId),
    BidderId INT FOREIGN KEY REFERENCES Users(UsersId),
    BidAmount DECIMAL(18,2),
    BidTime DATETIME,
    Status NVARCHAR(50)
);

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

CREATE TABLE UserPackages (
    UserPackagesId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES Users(UsersId),
    PackageId INT FOREIGN KEY REFERENCES PostPackages(PostPackagesId),
	PurchasedPostDuration INT,
    PurchasedAtPrice DECIMAL(18,2),
	Currency NVARCHAR(100), --VND, USD
    PurchasedAt DATETIME,
    Status NVARCHAR(50)
);

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
