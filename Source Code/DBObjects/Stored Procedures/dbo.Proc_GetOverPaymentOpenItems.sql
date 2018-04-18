IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOverPaymentOpenItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOverPaymentOpenItems]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                
Proc Name       : Vijay Arora                                
Created by      : Proc_GetOverPaymentOpenItems  
Date            : 19-01-2006  
Purpose      : To get the over payment from Customer Open Items  
Revison History :                                
Used In   : Wolverine                                
                                
Modified By :   
Modified On :   
Purpose  :   
------------------------------------------------------------                                
*/                         
CREATE PROC Dbo.Proc_GetOverPaymentOpenItems                                
(                                
 @CUSTOMER_ID      INT,                                
 @POLICY_ID       INT,                                
 @POLICY_VERSION_ID  SMALLINT  
)  
AS  
BEGIN  
  
SELECT * FROM ACT_CUSTOMER_OPEN_ITEMS WITH (NOLOCK)
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
AND ITEM_STATUS = 'OP'  
  
SELECT (SUM(TOTAL_DUE) - SUM(TOTAL_PAID)) AS TOTAL_OVER_PAYMENT FROM ACT_CUSTOMER_OPEN_ITEMS  WITH (NOLOCK)
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
AND ITEM_STATUS = 'OP'  
  
END  
                                
  



GO

