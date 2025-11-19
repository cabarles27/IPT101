CREATE PROCEDURE [dbo].[DeletePOS]
	@SaleId NVARCHAR(10) = NULL
AS
BEGIN
	DELETE FROM [dbo].[POS] WHERE SaleId = @SaleId
end
