Create Procedure Proc_InsertLogOutTime  
(  
 @SNo int  
)  
As  
 Update  LoginAuditLog set LogOutTime=GETDATE() where SNo=@SNo