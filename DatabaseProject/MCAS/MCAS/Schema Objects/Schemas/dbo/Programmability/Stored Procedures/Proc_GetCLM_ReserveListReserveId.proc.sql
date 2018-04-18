CREATE PROCEDURE [dbo].[Proc_GetCLM_ReserveListReserveId]
	@ReserveId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
SELECT * FROM [CLM_Reserve] where [ReserveId]=@ReserveId
END


