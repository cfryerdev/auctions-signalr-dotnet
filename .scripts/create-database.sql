IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Auctioneer')
BEGIN
    CREATE DATABASE Auctioneer;
END

USE Auctioneer;

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Auction')
BEGIN
    CREATE TABLE Auction  
    (
        [Id] UNIQUEIDENTIFIER PRIMARY KEY,
        [Name] NVARCHAR(500) NOT NULL,
        [TypeId] INT NOT NULL,
        [VisibilityId] INT NOT NULL,
        [StartDateTime] DATETIME NOT NULL,
        [EndDateTime] DATETIME NOT NULL
    );
END

IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'AuctionItem')
BEGIN
    CREATE TABLE AuctionItem
    (
        [Id] UNIQUEIDENTIFIER PRIMARY KEY,
        [Index] INT,
        [Name] NVARCHAR(500) NOT NULL,
        [Payload] NVARCHAR(MAX) NOT NULL,
        [AuctionId] UNIQUEIDENTIFIER NOT NULL,
        FOREIGN KEY (AuctionId) REFERENCES Auction (Id)
    );
END