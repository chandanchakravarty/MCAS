Alter Procedure Proc_SetVoidLogRequest
(
 @logId int
)
As
SET FMTONLY OFF;  
Update CLM_LogRequest set IsVoid=1 where LogId=@logId

