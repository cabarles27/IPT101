CREATE PROCEDURE [dbo].[GetPOS]
	@SaleId NVARCHAR(10) = NULL
AS
BEGIN
	SELECT * FROM [dbo].[POS] AS a WHERE a.[SaleId] = @SaleId;
END