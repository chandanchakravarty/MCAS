
ALTER PROCEDURE [dbo].[Proc_ReAssignMail] @ReAssignFrom INT = 0, @ReAssignTo INT = 0,
@CreatedBy nvarchar(25),@ModifiedBy nvarchar(25),@EmailText nvarchar(200)
AS

Declare @EmailIdFrom nvarchar(100), @EmailId nvarchar(100),@UserDispName nvarchar(20)

SET @EmailIdFrom = (select EmailId from dbo.MNT_Users where Sno = @ReAssignFrom)
SET @EmailId = (select EmailId from dbo.MNT_Users where Sno = @ReAssignTo)
SET @UserDispName = (select UserDispName from dbo.MNT_Users where Sno = @ReAssignTo)


INSERT INTO [MNT_EMAIL] ([NAME] ,[EMAIL] ,[IsActive] ,
[CreatedDate] ,[CreatedBy] ,[ModifiedBy] ,[ModifiedDate]) VALUES (@UserDispName ,@EmailId ,'Y' ,getdate() ,@CreatedBy ,@ModifiedBy ,getdate())

INSERT INTO POL_EMAIL_SPOOL ([EMAIL_FROM] ,[EMAIL_TO] ,[EMAIL_TEXT] ,[SENT_STATUS] ,[SENT_TIME] ,[REMARK] ,[ERROR_DESCRIPTION]
 ,[IsActive] ,[CreatedDate] ,[CreatedBy] ,[ModifiedBy] ,[ModifiedDate]) 
 VALUES (@EmailIdFrom ,@EmailId ,@EmailText ,'N' ,'' ,'' ,'' ,'Y' ,getdate() ,@CreatedBy ,@ModifiedBy  ,getdate())







GO


