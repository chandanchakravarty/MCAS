IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_EOD_AddREcordToCreditCardSpool]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_EOD_AddREcordToCreditCardSpool]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name        : dbo.Proc_EOD_AddREcordToCreditCardSpool    
Created by       : Ravinda Gupta     
Date             :     
Purpose        :     
Revison History :      
Used In   :Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------------------------------------------------------------*/      
-- drop proc dbo.Proc_EOD_AddREcordToCreditCardSpool    
CREATE PROC dbo.Proc_EOD_AddREcordToCreditCardSpool    
(     
	@ENTITY_ID Int,    
	@ENTITY_TYPE Varchar(10),    
	@POLICY_NUMBER Varchar(15),    
	@POLICY_ID Int,    
	@POLICY_VERSION_ID SmallInt,    
	@REF_DEPOSIT_ID Int,    
	@REF_DEP_DETAIL_ID Int,    
	@REF_CHECK_ID  Int = null,    
	@TRANSACTION_AMOUNT Decimal(18,2),    
	@PROCESSED Char(1),    
	@PAYPALREFID  Varchar(200),    
	@PAYPALRESULT Varchar(50),    
	@ERROR_DESCRIPTION  Varchar(250) ,    
	@PROCESSED_DATETIME DateTime ,  
	@CREATED_BY  Int,  
	@NOTE NVarchar(2000)  
)    
AS    
BEGIN     
	IF(@POLICY_NUMBER IS NULL )    
	BEGIN     
		SELECT @POLICY_NUMBER = POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST     
		WHERE CUSTOMER_ID = @ENTITY_ID     
		AND POLICY_ID = @POLICY_ID     
		AND POLICY_VERSION_ID = @POLICY_VERSION_ID     
	END    
	ELSE IF(@POLICY_ID IS NULL)    
	BEGIN     
		SELECT TOP 1 @POLICY_ID = POLICY_ID ,@POLICY_VERSION_ID = POLICY_VERSION_ID    
		FROM     
		POL_CUSTOMER_POLICY_LIST     
		WHERE CUSTOMER_ID = @ENTITY_ID     
		AND POLICY_NUMBER = @POLICY_NUMBER     
	END    
      
    
	INSERT INTO EOD_CREDIT_CARD_SPOOL
	(    
		ENTITY_ID,ENTITY_TYPE,POLICY_NUMBER,POLICY_ID,POLICY_VERSION_ID,REF_DEPOSIT_ID,    
		REF_DEP_DETAIL_ID,TRANSACTION_AMOUNT,CREATED_DATETIME,PROCESSED,PAYPAL_RESULT,    
		PAY_PAL_REF_ID,ERROR_DESCRIPTION,PROCESSED_DATETIME ,REF_CHECK_ID,CREATED_BY,NOTE    
	)    
	VALUES
	(    
		@ENTITY_ID, @ENTITY_TYPE, @POLICY_NUMBER, @POLICY_ID, @POLICY_VERSION_ID, @REF_DEPOSIT_ID,    
		@REF_DEP_DETAIL_ID, @TRANSACTION_AMOUNT, GETDATE() , @PROCESSED, @PAYPALRESULT,    
		@PAYPALREFID, @ERROR_DESCRIPTION, @PROCESSED_DATETIME , @REF_CHECK_ID , @CREATED_BY ,@NOTE  
	)     
  

	--RAvindra-- To be be moved to payment reversal
	--Payment Received thru Process Credit Card and an Over Payment   
	--	IF EXISTS(SELECT ENTITY_ID FROM EOD_CREDIT_CARD_SPOOL)  
	--	BEGIN  
	--		IF(SUBSTRING(CAST(@TRANSACTION_AMOUNT AS VARCHAR),1,1) = '-') --CASE OF AMT REVERSAL  
	--		BEGIN  
	--		UPDATE  ACT_CUSTOMER_OPEN_ITEMS SET ITEM_STATUS = 'DP'  
	--		WHERE  
	--		SOURCE_ROW_ID = ISNULL(@REF_CHECK_ID,0)  
	--		AND CUSTOMER_ID = @ENTITY_ID  
	--	END  


	--Praveen Kasana-- Insert Data in Deposit SPOOL
	/*IF ( @PAYPALRESULT = '-1' ) --> (-1) COMMIT FAILS IN BRICS SYSTEM
	BEGIN
		DECLARE @REF_SPOOL_ID INT
		SET @REF_SPOOL_ID = @@IDENTITY

		SET @PROCESSED = NULL

		INSERT INTO [ACT_CREDIT_CARD_DEPOSIT_SPOOL]
		(    
			ENTITY_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_NUMBER,TRANSACTION_AMOUNT,CREATED_DATETIME,
			PAY_PAL_REF_ID,REF_SPOOL_ID,PROCESSED_DATETIME,PROCESSED,
			ERROR_DESCRIPTION,CREATED_BY
			
		)    
		VALUES
		(    
			@ENTITY_ID,@POLICY_ID, @POLICY_VERSION_ID, @POLICY_NUMBER,@TRANSACTION_AMOUNT,GETDATE(),    
			@PAYPALREFID, @REF_SPOOL_ID, @PROCESSED_DATETIME,@PROCESSED,
			@ERROR_DESCRIPTION, @CREATED_BY 
		)     
	END*/

 RETURN 1  
END  
   



GO

