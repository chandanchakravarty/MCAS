
/****** Object:  StoredProcedure [dbo].[Proc_ReAssignMail]    Script Date: 07/23/2014 12:46:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReAssignMail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReAssignMail]
GO



/****** Object:  StoredProcedure [dbo].[Proc_ReAssignMail]    Script Date: 07/23/2014 12:46:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Proc_ReAssignMail] @ReAssignFrom INT = 0, @ReAssignTo INT = 0,
@CreatedBy nvarchar(25),@ModifiedBy nvarchar(25),@EmailText nvarchar(2000),@EmailSubject nvarchar(200)
AS

Declare @EmailIdFrom nvarchar(100), @EmailId nvarchar(100),@UserDispName nvarchar(20)

SET @EmailIdFrom = (select EmailId from dbo.MNT_Users where Sno = @ReAssignFrom)
SET @EmailId = (select EmailId from dbo.MNT_Users where Sno = @ReAssignTo)
SET @UserDispName = (select UserDispName from dbo.MNT_Users where Sno = @ReAssignTo)


INSERT INTO [MNT_EMAIL] ([NAME] ,[EMAIL] ,[IsActive] ,
[CreatedDate] ,[CreatedBy] ,[ModifiedBy] ,[ModifiedDate]) VALUES (@UserDispName ,@EmailId ,'Y' ,getdate() ,@CreatedBy ,@ModifiedBy ,getdate())

INSERT INTO POL_EMAIL_SPOOL ([EMAIL_FROM] ,[EMAIL_TO] ,[EMAIL_TEXT] ,[SENT_STATUS] ,[SENT_TIME] ,[REMARK] ,[ERROR_DESCRIPTION]
 ,[IsActive] ,[CreatedDate] ,[CreatedBy] ,[ModifiedBy] ,[ModifiedDate],[EmailSubject]) 
 VALUES (@EmailIdFrom ,@EmailId ,@EmailText ,'N' ,'' ,'' ,'' ,'Y' ,getdate() ,@CreatedBy ,@ModifiedBy  ,getdate(),@EmailSubject)






GO


