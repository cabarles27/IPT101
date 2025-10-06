CREATE PROCEDURE [dbo].[CreatePOS]
@SaleID NVARCHAR (10)= NULL,
@ProductName NVARCHAR (10) = NULL,
@Quantity INT  = NULL,
@Price INT = NULL,
@TotalAmount INT = NULL
AS
 BEGIN 
    INSERT INTO POS (SaleId, ProductName, Quantity, Price)
      Values(@SaleId, @ProductName, @Quantity, @Price);
End