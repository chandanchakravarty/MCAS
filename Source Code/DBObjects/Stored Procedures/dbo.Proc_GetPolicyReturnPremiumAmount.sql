IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyReturnPremiumAmount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyReturnPremiumAmount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
 Proc Name       : dbo.Proc_GetPolicyReturnPremiumAmount      
 Created by      : Vijay Arora      
 Date            : 13-01-2006    
 Purpose         : Get the Return Premium from Table POL_POLICY_PROCESS
 Revison History :                
 Used In     : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC Dbo.Proc_GetPolicyReturnPremiumAmount    
(                
 @CUSTOMER_ID  INT,          
 @POLICY_ID  INT,        
 @POLICY_VERSION_ID INT,
 @ROW_ID INT   
)                
AS                
BEGIN      
 SELECT  RETURN_PREMIUM_AMOUNT,RETURN_MCCA_FEE_AMOUNT,RETURN_OTHER_FEE_AMOUNT 
  FROM  POL_POLICY_PROCESS WHERE CUSTOMER_ID = @CUSTOMER_ID    
 AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
 AND ROW_ID = @ROW_ID
END              
          
        
      
    
    
    
    
    
    
    
  



GO

