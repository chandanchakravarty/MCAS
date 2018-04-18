IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAllPolicies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAllPolicies]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetAllPolicies            
Created by      : Vijay Arora            
Date            : 14/03/2006            
Purpose        : Gets all policies of customer of currently logged user agency.            
Revison History :                    
Used In    : Wolverine                    
-------------------------------------------------------------*/            
--DROP PROC Proc_GetAllPolicies    
CREATE PROC Proc_GetAllPolicies             
(            
   @CUSTOMER_ID INT,            
   @AGENCY_ID INT,      
   @POLICY_NUMBER VARCHAR(50)           
)            
AS            
BEGIN            
 SELECT DISTINCT POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
        WHERE CUSTOMER_ID = @CUSTOMER_ID   
        AND POLICY_NUMBER <>  @POLICY_NUMBER       
        AND POL_CUSTOMER_POLICY_LIST.IS_ACTIVE = 'Y' AND APP_EXPIRATION_DATE > convert(datetime, GETDATE() )  
  AND POLICY_STATUS IN ( 'NORMAL'  , 'RENEWED')  
        ORDER BY POLICY_NUMBER       
END            
      
  
  
  
  
  
  
  



GO

