IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateOverPaymentAdjustmentOpenItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateOverPaymentAdjustmentOpenItems]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                
Proc Name       : Vijay Arora                                
Created by      : Proc_UpdateOverPaymentAdjustmentOpenItems  
Date            : 19-01-2006  
Purpose      : To update the over payment due records in Customer Open Items  
Revison History :                                
Used In   : Wolverine                                
                                
Modified By :   
Modified On :   
Purpose  :   
------------------------------------------------------------                                
*/                         
CREATE PROC Dbo.Proc_UpdateOverPaymentAdjustmentOpenItems                                
(                                
 @CUSTOMER_ID      INT,                                
 @POLICY_ID       INT,                                
 @POLICY_VERSION_ID  SMALLINT,    
 @IDEN_ROW_ID     INT,  
 @TOTAL_PAID    DECIMAL(9,2)  
)  
AS  
BEGIN  
  
UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_PAID  = (TOTAL_PAID + @TOTAL_PAID)  
WHERE IDEN_ROW_ID = @IDEN_ROW_ID AND  
CUSTOMER_ID = @CUSTOMER_ID AND  
POLICY_ID = @POLICY_ID AND  
POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
END  



GO

