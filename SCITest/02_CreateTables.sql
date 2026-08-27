USE SCITest;
GO

--==================PRODUCTS====================--
CREATE TABLE dbo.Products
(
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Products PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    Price DECIMAL(18,2) NOT NULL,
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Products_CreatedDate DEFAULT GETDATE(),
    CONSTRAINT CK_Products_Price CHECK (Price > 0)
);
GO