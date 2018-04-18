

/****** Object:  StoredProcedure [dbo].[Proc_SendMail]    Script Date: 07/23/2014 12:48:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SendMail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SendMail]
GO


/****** Object:  StoredProcedure [dbo].[Proc_SendMail]    Script Date: 07/23/2014 12:48:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[Proc_SendMail]
AS
DECLARE @count int 
SET @count=1
DECLARE @Recepient_Email nvarchar(1000)
DECLARE @CCRecepient_Email nvarchar(1000)
DECLARE @mailbody nvarchar(2000)
DECLARE @profile nvarchar(200)
DECLARE @emailSubject nvarchar(400)
SET @profile=(select EmailProfile from dbo.MNT_SYS_PARAMS)
DECLARE @ItemBack1 TABLE
(
  ID [int] IDENTITY(1,1) NOT NULL,
  INDEN_ROW_ID [int]  NOT NULL,
  EMAIL_TO nvarchar(1000)  NOT NULL,
  EMAIL_FROM nvarchar(1000)  NOT NULL,
  EMAIL_TEXT nvarchar(2000) NULL,
  EmailSubject nvarchar(400) NULL
)
INSERT INTO @ItemBack1 SELECT [INDEN_ROW_ID],[EMAIL_TO],[EMAIL_FROM],[EMAIL_TEXT],[EmailSubject] FROM [dbo].[POL_EMAIL_SPOOL] with(nolock) where SENT_STATUS='N' and [EMAIL_TO] is not null
while (@count <=(select COUNT(*) from @ItemBack1))
begin

select top 1 @Recepient_Email=EMAIL_TO,@CCRecepient_Email=EMAIL_FROM,@mailBody=EMAIL_TEXT,@emailSubject=EmailSubject from @ItemBack1 where ID=@count

EXEC msdb.dbo.sp_send_dbmail @profile_name=@profile,@recipients=@Recepient_Email,@copy_recipients=@CCRecepient_Email,@subject=@emailSubject,@body=@mailbody,@body_format = 'HTML'; 
set @count =@count +1
END

update POL_EMAIL_SPOOL SET [SENT_STATUS] = 'Y',[REMARK]='Email Sent Successfully',[SENT_TIME] = getdate(),[ModifiedDate] = getdate() where INDEN_ROW_ID in(select INDEN_ROW_ID from @ItemBack1)




GO


