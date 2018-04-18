CREATE PROCEDURE [dbo].[Proc_SetVoidLogRequest]
	@logId [int]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  
Update CLM_LogRequest set IsVoid=1 where LogId=@logId


