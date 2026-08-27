USE SCITest;
GO

--==================CREATE====================--
CREATE OR ALTER PROCEDURE dbo.sp_Product_Create
    @Name NVARCHAR(200),
    @Description NVARCHAR(1000) = NULL,
    @Price DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Products
    (
        Name,
        Description,
        Price
    )
    VALUES
    (
        @Name,
        @Description,
        @Price
    );

    SELECT
        Id,
        Name,
        Description,
        Price,
        CreatedDate
    FROM dbo.Products
    WHERE Id = SCOPE_IDENTITY();
END;
GO

--==================GetAll====================--
CREATE OR ALTER PROCEDURE dbo.sp_Product_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        Price,
        CreatedDate
    FROM dbo.Products
    ORDER BY Id DESC;
END;
GO

--==================GetById====================--
CREATE OR ALTER PROCEDURE dbo.sp_Product_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Description,
        Price,
        CreatedDate
    FROM dbo.Products
    WHERE Id = @Id;
END;
GO

--==================UPDATE====================--
CREATE OR ALTER PROCEDURE dbo.sp_Product_Update
    @Id INT,
    @Name NVARCHAR(200),
    @Description NVARCHAR(1000) = NULL,
    @Price DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Products
    SET
        Name = @Name,
        Description = @Description,
        Price = @Price
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0
    BEGIN
        SELECT CAST(0 AS BIT) AS Success;
        RETURN;
    END;

    SELECT CAST(1 AS BIT) AS Success;
END;
GO

--==================DELETE====================--
CREATE OR ALTER PROCEDURE dbo.sp_Product_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Products
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0
    BEGIN
        SELECT CAST(0 AS BIT) AS Success;
        RETURN;
    END;

    SELECT CAST(1 AS BIT) AS Success;
END;
GO