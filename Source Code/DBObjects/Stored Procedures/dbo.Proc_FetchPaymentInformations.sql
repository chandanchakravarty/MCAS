IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPaymentInformations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPaymentInformations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* -----------------------------------------------------------------------------------------------                          
Proc Name				: Dbo.Proc_FetchPaymentInformations                            
Created by				: Sibin                           
Date					:                             
Purpose					: 05 Feb 2009                          
Revison History			:                            
CREATED BY				: Sibin Thomas Philip
purpose					: To Fetch Payment Records 
  
                             
exec Proc_FetchPaymentInformations  
---------------------------------------------------------------------------------------------------                          
Date     Review By          Comments                            
------   ------------       ------------------------- ---------------------------------------------*/          
-- drop proc dbo.Proc_FetchPaymentInformations                                               
create PROCEDURE dbo.Proc_FetchPaymentInformations                        
(                          
 @POLICYNO VARCHAR(15)                                                 
 --@REVERSALBLE_PAYMENT Int Out
)                          
AS      

DECLARE @CUST_ID INT
DECLARE @POLICY_ID INT
DECLARE @ROWCOUNT INT
                      
BEGIN              

SELECT @CUST_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID
FROM   POL_CUSTOMER_POLICY_LIST 
WHERE  POLICY_NUMBER=@POLICYNO 

SELECT @ROWCOUNT=COUNT(OI.IDEN_ROW_ID)
FROM ACT_CUSTOMER_OPEN_ITEMS OI                          
INNER JOIN POL_CUSTOMER_POLICY_LIST PCL   
	ON OI.CUSTOMER_ID = PCL.CUSTOMER_ID   
	AND PCL.POLICY_ID = OI.POLICY_ID AND     
	PCL.POLICY_VERSION_ID = OI.POLICY_VERSION_ID                         
INNER JOIN CLT_CUSTOMER_LIST CLT   
	ON OI.CUSTOMER_ID = CLT.CUSTOMER_ID     
INNER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS LI
	 ON OI.SOURCE_ROW_ID = LI.CD_LINE_ITEM_ID  
	 AND OI.UPDATED_FROM = 'D'                       
INNER JOIN ACT_CURRENT_DEPOSITS ACR  
	ON LI.DEPOSIT_ID = ACR.DEPOSIT_ID   
INNER JOIN MNT_LOOKUP_VALUES MLV
	ON ACR.RECEIPT_MODE=MLV.LOOKUP_UNIQUE_ID    
WHERE  (PCL.POLICY_NUMBER=@POLICYNO) AND OI.UPDATED_FROM = 'D'
	   AND PCL.CUSTOMER_ID=@CUST_ID AND PCL.POLICY_ID=@POLICY_ID
	   AND ISNULL(LI.REVERSED,0)= 0



SELECT OI.IDEN_ROW_ID,PCL.POLICY_NUMBER AS POLICY_NUMBER,PCL.CUSTOMER_ID,PCL.POLICY_ID,PCL.POLICY_VERSION_ID,                       
POSTING_DATE AS PAYMENT_DATE,
--CONVERT(VARCHAR(30),POSTING_DATE,101)AS PAYMENT_DATE, 
LOOKUP_VALUE_DESC AS PAYMENT_MODE,
ACR.RECEIPT_MODE ,
OI.SOURCE_NUM AS DEPOSIT_NO,
LI.RECEIPT_AMOUNT,LI.REVERSAL_DATE,
CASE LI.REVERSED  WHEN '1' THEN 'YES' ELSE 'NO' END AS REVERSED ,
LI.REVERSAL_DATE,LI.CD_LINE_ITEM_ID,
TOTAL_DUE * -1 AS PAYMENT_AMOUNT  
FROM ACT_CUSTOMER_OPEN_ITEMS OI                          
INNER JOIN POL_CUSTOMER_POLICY_LIST PCL   
	ON OI.CUSTOMER_ID = PCL.CUSTOMER_ID   
	AND PCL.POLICY_ID = OI.POLICY_ID AND     
	PCL.POLICY_VERSION_ID = OI.POLICY_VERSION_ID                         
INNER JOIN CLT_CUSTOMER_LIST CLT   
	ON OI.CUSTOMER_ID = CLT.CUSTOMER_ID     
INNER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS LI
	 ON OI.SOURCE_ROW_ID = LI.CD_LINE_ITEM_ID  
	 AND OI.UPDATED_FROM = 'D'                       
INNER JOIN ACT_CURRENT_DEPOSITS ACR  
	ON LI.DEPOSIT_ID = ACR.DEPOSIT_ID   
INNER JOIN MNT_LOOKUP_VALUES MLV
	ON ACR.RECEIPT_MODE=MLV.LOOKUP_UNIQUE_ID    
WHERE  (PCL.POLICY_NUMBER=@POLICYNO) AND OI.UPDATED_FROM = 'D'
	   AND PCL.CUSTOMER_ID=@CUST_ID AND PCL.POLICY_ID=@POLICY_ID

ORDER BY OI.POSTING_DATE DESC ,ACR.DEPOSIT_NUMBER DESC,LI.CD_LINE_ITEM_ID DESC           

IF(@ROWCOUNT > 0)
 BEGIN
	SELECT  TOP 1 (OI.IDEN_ROW_ID)
	FROM ACT_CUSTOMER_OPEN_ITEMS OI                          
	INNER JOIN POL_CUSTOMER_POLICY_LIST PCL   
		ON OI.CUSTOMER_ID = PCL.CUSTOMER_ID   
		AND PCL.POLICY_ID = OI.POLICY_ID AND     
		PCL.POLICY_VERSION_ID = OI.POLICY_VERSION_ID                         
	INNER JOIN CLT_CUSTOMER_LIST CLT   
		ON OI.CUSTOMER_ID = CLT.CUSTOMER_ID     
	INNER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS LI
		 ON OI.SOURCE_ROW_ID = LI.CD_LINE_ITEM_ID  
		 AND OI.UPDATED_FROM = 'D'                       
	INNER JOIN ACT_CURRENT_DEPOSITS ACR  
		ON LI.DEPOSIT_ID = ACR.DEPOSIT_ID   
	INNER JOIN MNT_LOOKUP_VALUES MLV
		ON ACR.RECEIPT_MODE=MLV.LOOKUP_UNIQUE_ID    
	WHERE  (PCL.POLICY_NUMBER=@POLICYNO) AND OI.UPDATED_FROM = 'D'
		   AND PCL.CUSTOMER_ID=@CUST_ID AND PCL.POLICY_ID=@POLICY_ID
		   AND ISNULL(LI.REVERSED,0)= 0
	ORDER BY OI.POSTING_DATE DESC ,ACR.DEPOSIT_NUMBER DESC,LI.CD_LINE_ITEM_ID DESC   

  END
END 







GO

