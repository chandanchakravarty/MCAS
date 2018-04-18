CREATE PROC [dbo].[Proc_SendMail]
AS
DECLARE @count int 
SET @count=1
DECLARE @fromEmail nvarchar(300)
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
  EmailSubject nvarchar(400) NULL,
  EMAIL_CC nvarchar(2000) NULL
)
EXEC Proc_ProcessReminderMail
INSERT INTO @ItemBack1 SELECT [INDEN_ROW_ID],[EMAIL_TO],[EMAIL_FROM],[EMAIL_TEXT],[EmailSubject],[EMAIL_CC] FROM [dbo].[POL_EMAIL_SPOOL] with(nolock) where SENT_STATUS='N' and [EMAIL_TO] is not null
while (@count <=(select COUNT(*) from @ItemBack1))
begin

select top 1 @Recepient_Email=EMAIL_TO,@fromEmail=EMAIL_FROM,@CCRecepient_Email=ISNULL(EMAIL_CC,''),@mailbody=EMAIL_TEXT,@emailSubject=EmailSubject from @ItemBack1 where ID=@count

EXEC msdb.dbo.sp_send_dbmail @from_address=@fromEmail, @profile_name=@profile,@recipients=@Recepient_Email,@copy_recipients=@CCRecepient_Email,@subject=@emailSubject,@body=@mailbody,@body_format = 'HTML'; 
set @count =@count +1
END

update POL_EMAIL_SPOOL SET [SENT_STATUS] = 'Y',[REMARK]='Email Sent Successfully',[SENT_TIME] = getdate(),[ModifiedDate] = getdate() where INDEN_ROW_ID in(select INDEN_ROW_ID from @ItemBack1)


