IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDuePremiumOpenItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDuePremiumOpenItems]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                
Proc Name       : Vijay Arora                                
Created by      : Proc_GetDuePremiumOpenItems  
Date            : 19-01-2006  
Purpose      : To get the Due Premium records from Customer Open Items  
Revison History :                                
Used In   : Wolverine                                
                                
Modified By :   
Modified On :   
Purpose  :   
------------------------------------------------------------                                
*/                         
CREATE PROC Dbo.Proc_GetDuePremiumOpenItems                                
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
AND TOTAL_DUE > TOTAL_PAID  
AND ITEM_STATUS IS NULL  
ORDER BY SOURCE_EFF_DATE  
  
END  
  



GO

