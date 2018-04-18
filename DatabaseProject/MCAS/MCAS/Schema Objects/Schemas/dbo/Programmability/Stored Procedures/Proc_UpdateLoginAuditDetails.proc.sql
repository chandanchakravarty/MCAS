Create Procedure Proc_UpdateLoginAuditDetails
(
 @sNo varchar(max)
)
As
UPDATE LoginAuditLog SET LogOutTime=GETDATE() WHERE SNo=@sNo
