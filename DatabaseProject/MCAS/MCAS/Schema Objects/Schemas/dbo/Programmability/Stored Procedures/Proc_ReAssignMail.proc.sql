CREATE PROCEDURE [dbo].[Proc_ReAssignMail]
	@ReAssignFrom [int] = 0,
	@ReAssignTo [int] = 0,
	@ReAssignAdmin [int] = 0,
	@CreatedBy [nvarchar](25),
	@ModifiedBy [nvarchar](25),
	@EmailText [nvarchar](2000),
	@EmailSubject [nvarchar](200)
WITH EXECUTE AS CALLER
AS
Declare @EmailIdFrom nvarchar(100),@EmailIdAdmin nvarchar(100), @EmailId nvarchar(100),@UserDispName nvarchar(20)

if(@ReAssignAdmin = 0)
Begin
SET @EmailIdFrom = (select EmailId from dbo.MNT_Users where Sno = @ReAssignFrom)
End
Else
Begin
SET @EmailIdFrom = (select EmailId from dbo.MNT_Users where Sno = @ReAssignFrom)+ ';' + (select EmailId from dbo.MNT_Users where Sno = @ReAssignAdmin)
End

SET @EmailId = (select EmailId from dbo.MNT_Users where Sno = @ReAssignTo)

SET @UserDispName = (select UserDispName from dbo.MNT_Users where Sno = @ReAssignTo)

INSERT INTO POL_EMAIL_SPOOL ([EMAIL_FROM] ,[EMAIL_TO] ,[EMAIL_TEXT] ,[SENT_STATUS] ,[SENT_TIME] ,[REMARK] ,[ERROR_DESCRIPTION]
 ,[IsActive] ,[CreatedDate] ,[CreatedBy] ,[ModifiedBy] ,[ModifiedDate],[EmailSubject],[EMAIL_CC]) 
 VALUES (ISNULL((select top 1 FromUserEmailId from MNT_SYS_PARAMS),@EmailIdFrom) ,@EmailId ,@EmailText ,'N' ,'' ,'' ,'' ,'Y' ,getdate() ,@CreatedBy ,@ModifiedBy  ,getdate(),@EmailSubject,@EmailIdFrom)

GO


