Create Procedure Proc_InsertLogRefNo
(
 @LogId int 
)
 As
Update CLM_LogRequest set LogRefNo='Log-'+cast(@LogId as varchar) where LogId=@LogId
  
