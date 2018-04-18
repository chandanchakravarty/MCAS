--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
ALTER PROC dbo.Proc_Update_MNT_CURRENCY_MASTER (
	@CURRENCY_ID int,
	@CURR_CODE nvarchar(6),
	@CURR_DESC nvarchar(50),
	@CURR_SYMBOL nvarchar(4) )
	
AS

IF exists (select * from MNT_CURRENCY_MASTER where CURRENCY_ID = @CURRENCY_ID )
BEGIN

	
UPDATE [dbo].[MNT_CURRENCY_MASTER]
   SET [CURR_CODE] = @CURR_CODE
      ,[CURR_DESC] = @CURR_DESC
      ,[CURR_SYMBOL] = @CURR_SYMBOL
      
 WHERE CURRENCY_ID = @CURRENCY_ID

END	






