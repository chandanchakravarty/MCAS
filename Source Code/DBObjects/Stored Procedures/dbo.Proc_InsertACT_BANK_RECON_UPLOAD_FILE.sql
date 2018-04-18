IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_BANK_RECON_UPLOAD_FILE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_BANK_RECON_UPLOAD_FILE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertACT_BANK_RECON_UPLOAD_FILE
Created by      : Swastika    
Date            : 18 Apr,2007    
Purpose         : To Insert uploaded file into the respective table    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_InsertACT_BANK_RECON_UPLOAD_FILE      
CREATE  PROC [dbo].[Proc_InsertACT_BANK_RECON_UPLOAD_FILE]    
(    
 @FILE_NAME NVARCHAR(500), 
 @FILE_DESC NVARCHAR(1000),
 @AC_RECONCILIATION_ID INT,
 @CREATED_BY INT,
 @CREATED_DATETIME DATETIME
)    
AS    
BEGIN

	IF EXISTS (SELECT * FROM ACT_BANK_RECON_UPLOAD_FILE WHERE AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID)
	BEGIN
		UPDATE ACT_BANK_RECON_UPLOAD_FILE
		SET [FILE_NAME] = @FILE_NAME 
		,FILE_DESC = @FILE_DESC 
		,CREATED_BY = @CREATED_BY 
		,CREATED_DATETIME = @CREATED_DATETIME

		WHERE AC_RECONCILIATION_ID =@AC_RECONCILIATION_ID
	END
	ELSE
	BEGIN
		INSERT INTO ACT_BANK_RECON_UPLOAD_FILE
		(
		[FILE_NAME],FILE_DESC,AC_RECONCILIATION_ID,CREATED_BY,CREATED_DATETIME
		)
		VALUES
		(
		@FILE_NAME,@FILE_DESC,@AC_RECONCILIATION_ID,@CREATED_BY,@CREATED_DATETIME
		)
	END



END












GO

