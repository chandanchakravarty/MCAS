CREATE PROCEDURE [dbo].[Proc_InsertLogRefNo]
	@LogId [int]
WITH EXECUTE AS CALLER
AS
Update CLM_LogRequest set LogRefNo='LOG-'+cast(@LogId as varchar) where LogId=@LogId


