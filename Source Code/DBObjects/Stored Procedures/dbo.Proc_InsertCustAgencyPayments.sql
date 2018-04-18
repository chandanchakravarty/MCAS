IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustAgencyPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustAgencyPayments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*------------------------------------------------------------------------     
Proc Name       : dbo.Proc_InsertCustAgencyPayments     
Created by      : Swastika Gaur   
Date            :       
Purpose         :      
Revison History : Praven kAsana (Removed dependency on USSERID )     

Modified By		: Ravindra Gupta
Modified On		: 09-10-09
Purpose			: Revised SP for optimisation
Used In  : Wolverine      
  
-----        -------------------------------------------------------------*/      
--drop proc dbo.Proc_InsertCustAgencyPayments  
CREATE PROCEDURE dbo.Proc_InsertCustAgencyPayments  
(  
	@IDEN_ROW_ID INT  
)  
AS  
BEGIN  
  
	DECLARE @EFT INT,@TYPE VARCHAR(10),@TRAN_CODE INT,@TRAN_REQD CHAR(1),@ACC_USE INT  , 
			@REF_EFT_SPOOL_ID	Int
	
	SET @EFT = 11976  
	SET @TYPE = 'AGN'  
	SET @TRAN_CODE = 11  
	SET @TRAN_REQD ='Y'  
	SET @ACC_USE = 2  
	  
	IF EXISTS ( SELECT IDEN_ROW_ID FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY WITH(NOLOCK)  
				WHERE IDEN_ROW_ID = @IDEN_ROW_ID AND MODE = @EFT
				)
	BEGIN 
		INSERT INTO EOD_EFT_SPOOL  
		(  
			ENTITY_ID,  ENTITY_TYPE,  POLICY_NUMBER,  POLICY_ID,  POLICY_VERSION_ID,  TRANSACTION_AMOUNT,  
			CREATED_DATETIME,  TRANSACTION_CODE,  ACC_TRAN_REQUIRED,  ACCOUNT_TO_USE  , BRICS_TRAN_TYPE
		)   
		SELECT 	AGENCY_ID,  @TYPE, UPPER(POLICY_NUMBER) ,  POLICY_ID,  POLICY_VERSION_ID,  AMOUNT,  
		GETDATE(),  @TRAN_CODE,  @TRAN_REQD,  @ACC_USE   , 103
		FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY WITH(NOLOCK)  
		WHERE   IDEN_ROW_ID = @IDEN_ROW_ID  AND MODE = @EFT  

		SELECT @REF_EFT_SPOOL_ID = @@IDENTITY
	END
	
	INSERT INTO ACT_CUSTOMER_PAYMENTS_FROM_AGENCY  
	(  
		POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,POLICY_NUMBER,AGENCY_ID,  REF_EFT_SPOOL_ID , 
		AMOUNT,DATE_CREATED,DATE_COMMITTED,MODE,CREATED_BY_USER,ACT_TMP_ID  
	)  
	SELECT POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,POLICY_NUMBER,AGENCY_ID,  @REF_EFT_SPOOL_ID ,
	AMOUNT,DATE_CREATED,GETDATE(),MODE,CREATED_BY_USER,@IDEN_ROW_ID 
	FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY WITH(NOLOCK)  
	WHERE IDEN_ROW_ID = @IDEN_ROW_ID  

	DELETE FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY   
	WHERE IDEN_ROW_ID = @IDEN_ROW_ID  
  
END  

GO

