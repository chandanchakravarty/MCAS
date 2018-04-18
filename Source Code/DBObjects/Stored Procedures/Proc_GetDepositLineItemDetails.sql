IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDepositLineItemDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDepositLineItemDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================              
-- Author   : Pradeep Kushwaha  
-- Create date: 18-Aug-2011          
-- Description: Get Deposit line items details to show the validation message   
-- DROP PROC Proc_GetDepositLineItemDetails        
-- Proc_GetDepositLineItemDetails 427 ,2   
-- ============================================= 
    
CREATE Proc [dbo].[Proc_GetDepositLineItemDetails]  
(            
@DEPOSIT_ID INT=NULL,
@DEPOSIT_TYPE NVARCHAR(10)=NULL,  
@LANG_ID INT=1  
)       
AS      
BEGIN      
	IF(@DEPOSIT_TYPE='14832')--Co-Insurance 
BEGIN  
--itrack-1049
 SELECT * FROM 
(SELECT  
  ACD.DEPOSIT_NUMBER  
 ,POL_COINS.LEADER_POLICY_NUMBER
 ,ISNULL(ENDO.CO_ENDORSEMENT_NO,'NBS') CO_ENDORSEMENT_NO
 ,ACDLI.INSTALLMENT_NO  
 ,DBO.FUN_FORMATCURRENCY(ACDLI.RECEIPT_AMOUNT,@LANG_ID) AS RECEIPT_AMOUNT  
 ,
 CASE WHEN  EXISTS  
 			 (SELECT  INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) 
				
				WHERE INSTALL.RELEASED_STATUS='N' AND
				INSTALL.CUSTOMER_ID =APID.CUSTOMER_ID  AND 
				INSTALL.POLICY_ID = APID.POLICY_ID AND 
				INSTALL.POLICY_VERSION_ID =APID.POLICY_VERSION_ID AND 
				INSTALL.INSTALLMENT_NO NOT IN (
					SELECT INSTALLMENT_NO FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK) 
					WHERE CUSTOMER_ID=INSTALL.CUSTOMER_ID AND POLICY_ID=INSTALL.POLICY_ID
					AND POLICY_VERSION_ID=INSTALL.POLICY_VERSION_ID AND DEPOSIT_ID=ACD.DEPOSIT_ID 
					) 
				AND INSTALL.INSTALLMENT_NO < APID.INSTALLMENT_NO
			 )
			 
			 THEN CASE WHEN @LANG_ID=1 THEN (SELECT TRANS_TYPE_NAME FROM TRANSACTIONTYPELIST WITH(NOLOCK) WHERE TRANS_TYPE_ID=442) ELSE (SELECT TRANS_TYPE_NAME FROM TRANSACTIONTYPELIST_MULTILINGUAL WITH(NOLOCK) WHERE TRANS_TYPE_ID=442) END 
			END AS EXCEPTION_REASON
 
 FROM   
 ACT_CURRENT_DEPOSITS ACD WITH(NOLOCK) 
 JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS ACDLI WITH(NOLOCK) 
 ON ACD.DEPOSIT_ID =ACDLI.DEPOSIT_ID
 LEFT JOIN ACT_POLICY_INSTALLMENT_DETAILS APID WITH(NOLOCK) ON
 ACDLI.CUSTOMER_ID =APID.CUSTOMER_ID AND
 ACDLI.POLICY_ID =APID.POLICY_ID AND
 ACDLI.POLICY_VERSION_ID =APID.POLICY_VERSION_ID AND
 ACDLI.INSTALLMENT_NO =APID.INSTALLMENT_NO 
 LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON  ACDLI.CUSTOMER_ID=ENDO.CUSTOMER_ID AND ACDLI.POLICY_ID   =ENDO.POLICY_ID AND ACDLI.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID   
 INNER JOIN POL_CO_INSURANCE POL_COINS WITH(NOLOCK) ON POL_COINS.CUSTOMER_ID = ACDLI.CUSTOMER_ID  AND POL_COINS.POLICY_ID = ACDLI.POLICY_ID  AND POL_COINS.POLICY_VERSION_ID = ACDLI.POLICY_VERSION_ID   AND POL_COINS.COINSURANCE_ID=ACDLI.RECEIPT_FROM_ID   
 
 WHERE ACDLI.DEPOSIT_ID=@DEPOSIT_ID  AND APID.RELEASED_STATUS='N' 
 ) TT
 WHERE TT.EXCEPTION_REASON IS NOT NULL
 

END  
ELSE  
BEGIN  
      SELECT * FROM
	(SELECT ACD.DEPOSIT_NUMBER,
		ACDLI.INSTALLMENT_NO ,
		ACDLI.INCORRECT_OUR_NUMBER AS OUR_NUMBER,
		dbo.fun_FormatCurrency(ACDLI.RECEIPT_AMOUNT,@LANG_ID) as RECEIPT_AMOUNT
		,
		 CASE WHEN  EXISTS  
		 (SELECT  INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) 
			
			WHERE INSTALL.RELEASED_STATUS='N' AND
			INSTALL.CUSTOMER_ID =APID.CUSTOMER_ID  AND 
			INSTALL.POLICY_ID = APID.POLICY_ID AND 
			INSTALL.POLICY_VERSION_ID =APID.POLICY_VERSION_ID AND 
			INSTALL.INSTALLMENT_NO NOT IN (
					SELECT INSTALLMENT_NO FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK) 
					WHERE CUSTOMER_ID=INSTALL.CUSTOMER_ID AND POLICY_ID=INSTALL.POLICY_ID
					AND POLICY_VERSION_ID=INSTALL.POLICY_VERSION_ID AND DEPOSIT_ID=ACD.DEPOSIT_ID 
					) 
			AND INSTALL.INSTALLMENT_NO < APID.INSTALLMENT_NO 
			
		 )
		 
		 THEN CASE WHEN @LANG_ID=1 THEN (SELECT TRANS_TYPE_NAME FROM TRANSACTIONTYPELIST WITH(NOLOCK) WHERE TRANS_TYPE_ID=442) ELSE (SELECT TRANS_TYPE_NAME FROM TRANSACTIONTYPELIST_MULTILINGUAL WITH(NOLOCK) WHERE TRANS_TYPE_ID=442) END 
		 
		 END AS EXCEPTION_REASON
		
		 FROM ACT_CURRENT_DEPOSITS ACD WITH(NOLOCK) 
		 JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS ACDLI WITH(NOLOCK) 
		 ON ACD.DEPOSIT_ID =ACDLI.DEPOSIT_ID
		 LEFT JOIN ACT_POLICY_INSTALLMENT_DETAILS APID WITH(NOLOCK) ON
		 ACDLI.CUSTOMER_ID =APID.CUSTOMER_ID AND
		 ACDLI.POLICY_ID =APID.POLICY_ID AND
		 ACDLI.POLICY_VERSION_ID =APID.POLICY_VERSION_ID AND
		 ACDLI.INSTALLMENT_NO =APID.INSTALLMENT_NO 
		 WHERE ACD.DEPOSIT_ID=@DEPOSIT_ID AND APID.RELEASED_STATUS='N'
		 )TT
		 WHERE TT.EXCEPTION_REASON IS NOT NULL 
END    
 
END      
 
GO

