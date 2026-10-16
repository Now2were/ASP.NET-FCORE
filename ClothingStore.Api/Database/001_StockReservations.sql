-- Добавление новой таблицы в существующую БД. Исходный ~vs1350.sql пересоздаёт БД,
-- поэтому для этой практической его повторно запускать НЕ требуется.
USE [ClothingStoreDB];
GO
IF OBJECT_ID(N'dbo.StockReservations', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.StockReservations
    (
        Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_StockReservations PRIMARY KEY,
        UserId int NOT NULL,
        VariantId int NOT NULL,
        Quantity int NOT NULL,
        CreatedAt datetime2 NOT NULL,
        CancelledAt datetime2 NULL,
        CONSTRAINT CK_StockReservations_Quantity CHECK (Quantity > 0),
        CONSTRAINT FK_StockReservations_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id),
        CONSTRAINT FK_StockReservations_ProductVariants FOREIGN KEY (VariantId) REFERENCES dbo.ProductVariants(Id)
    );
    CREATE INDEX IX_StockReservations_UserId_VariantId_CancelledAt
        ON dbo.StockReservations(UserId, VariantId, CancelledAt);
END;
GO
