IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckValidity_Insurance_Score]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckValidity_Insurance_Score]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------        
CREATED BY   : Vijay Arora        
CREATED DATE TIME : 18-01-2006        
PURPOSE    :  Check Validity for Fetching the Insurance Score for Renewal Process
------------------------------------------------------------------------------
REVIEW HISTORY        
REVIEW BY  DATE  PURPOSE        
---------------------------------------------------------------------*/        
CREATE PROCEDURE dbo.Proc_CheckValidity_Insurance_Score
(        
 @CUSTOMER_ID  INT,
 @RETURN_VALUE INT OUTPUT
)        
AS        
BEGIN        
        

	DECLARE @LAST_FETCHED_DATE DATETIME
	DECLARE @TEMP_LAST_DATE DATETIME
	DECLARE @SCORE_VALIDITY VARCHAR(4)
	DECLARE @INSURANCE_SCORE_VALIDITY INT


	SELECT  @LAST_FETCHED_DATE = LAST_INSURANCE_SCORE_FETCHED FROM  CLT_CUSTOMER_LIST
	WHERE CUSTOMER_ID = @CUSTOMER_ID

	SELECT @SCORE_VALIDITY = SYS_INSURANCE_SCORE_VALIDITY FROM MNT_SYSTEM_PARAMS		

	SET @INSURANCE_SCORE_VALIDITY = CONVERT(INT,@SCORE_VALIDITY)

	SET @TEMP_LAST_DATE = DATEADD(mm,@INSURANCE_SCORE_VALIDITY,@LAST_FETCHED_DATE)

	IF @TEMP_LAST_DATE > GETDATE()
	BEGIN
		SET @RETURN_VALUE = 1	
	END
	ELSE
	BEGIN
		SET @RETURN_VALUE = 0
	END
RETURN @RETURN_VALUE
END        
        
      
    
  



GO

