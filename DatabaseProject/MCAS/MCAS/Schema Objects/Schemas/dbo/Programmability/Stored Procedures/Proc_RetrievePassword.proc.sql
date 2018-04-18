CREATE PROCEDURE [dbo].[Proc_RetrievePassword]
	@EmailId [nvarchar] (255),			
	@EmailSubject [nvarchar](200),
	@EmailText [nvarchar](2000),	
	@CreatedBy [nvarchar](25)
WITH EXECUTE AS CALLER
AS
BEGIN
	DECLARE @ReturnValue INT
	Declare @EmailIdFrom nvarchar(255)
	
	BEGIN TRY 
	
	SELECT TOP(1) @EmailIdFrom=FromUserEmailId FROM MNT_SYS_PARAMS	

	INSERT INTO POL_EMAIL_SPOOL ([EMAIL_FROM] ,[EMAIL_TO] ,[EMAIL_TEXT] ,[SENT_STATUS] ,[SENT_TIME] ,[REMARK] ,[ERROR_DESCRIPTION]
	 ,[IsActive] ,[CreatedDate] ,[CreatedBy],[EmailSubject]) 
	 VALUES (@EmailIdFrom ,@EmailId ,@EmailText ,'N' ,'' ,'' ,'' ,'Y' ,getdate() ,@CreatedBy ,@EmailSubject)
	
	SET @ReturnValue=1
	 
	END TRY
	BEGIN CATCH	
        SET @ReturnValue=0
       -- SELECT 
       -- ERROR_NUMBER() AS ErrorNumber
       --,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	
	SELECT @ReturnValue
END