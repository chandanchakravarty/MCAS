IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AddNACHAHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AddNACHAHistory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : Proc_AddNACHAHistory
Created by      : Ravindra  
Date            : 10-08-2007
Purpose      	: 
Revison History :  
------   ------------       --------------------------------*/  
--drop proc dbo.Proc_AddNACHAHistory
CREATE PROC dbo.Proc_AddNACHAHistory
(
	@ENTITY_TYPE		Varchar(20),
	@NACHA_FILE_NAME	Varchar(50),
	@TOTAL_DEBIT		Decimal(18,2),
	@TOTAL_CREDIT		Decimal(18,2),
	@BATCH_NUMBER		Int
)
AS
BEGIN 
	INSERT INTO ACT_NACHA_UPLOAD_HISTORY (ENTITY_TYPE , NACHA_FILE_NAME , TOTAL_DEBIT ,
			TOTAL_CREDIT ,CREATED_DATE_TIME , BATCH_NUMBER)
	VALUES  (@ENTITY_TYPE , @NACHA_FILE_NAME , @TOTAL_DEBIT ,@TOTAL_CREDIT , GETDATE() , @BATCH_NUMBER)
END




GO

