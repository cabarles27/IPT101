CREATE PROCEDURE [dbo].[UpdatePOS]
    @SaleId NVARCHAR(10) = NULL,
	@ProductName NVARCHAR(10) = NULL,
	@Quantity INT = NULL,
	@Price INT = NULL,
	@TotalAmount INT = NULL
AS
	BEGIN
	UPDATE [dbo].[POS]
	SET 
	[ProductName] = @ProductName,
	[Quantity] = @Quantity,
	[Price] = @Price,
	[TotalAmount] = @TotalAmount

	WHERE [SaleId] = @SaleId;

	END

