IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyTerms]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyTerms]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetPolicyTerms        
Created by      : Ravindra        
Date            : 03-10-2005       
Purpose         :       
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
      
--drop proc Proc_GetPolicyTerms        
    
CREATE PROCEDURE dbo.Proc_GetPolicyTerms        
(            
 @CUSTOMER_ID int  ,        
 @POLICY_ID   int  ,      
 @POLICY_VERSION_ID int,  
 @CALLED_FROM varchar(10)        
)                
AS                     
     
BEGIN      
  
if (@CALLED_FROM is null or @CALLED_FROM='')  
begin        
 SELECT         
 CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE, 101) AS POL_START_DATE,                   
 CONVERT(VARCHAR(10),APP_EXPIRATION_DATE, 101) AS POL_END_DATE ,      
 A.PREMIUM_AMOUNT AS PREMIUM_AMOUNT,
 P.STATE_ID
 FROM                    
 POL_CUSTOMER_POLICY_LIST P        
 LEFT JOIN ACT_PREMIUM_PROCESS_DETAILS A ON P.POLICY_ID=A.POLICY_ID      
  AND P.POLICY_VERSION_ID=A.POLICY_VERSION_ID      
  AND P.CUSTOMER_ID=A.CUSTOMER_ID      
         
 WHERE                    
  P.CUSTOMER_ID=@CUSTOMER_ID        
  AND P.POLICY_ID=@POLICY_ID       
  AND P.POLICY_VERSION_ID =@POLICY_VERSION_ID    
end  
else  
begin  
 SELECT  
  CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE, 101) AS POL_START_DATE,                   
  CONVERT(VARCHAR(10),APP_EXPIRATION_DATE, 101) AS POL_END_DATE,
  APP_LIST.STATE_ID    
 FROM  
  APP_LIST   
 WHERE  
  CUSTOMER_ID=@CUSTOMER_ID        
  AND APP_ID=@POLICY_ID       
  AND APP_VERSION_ID =@POLICY_VERSION_ID    
end  
      
         
End      
      
    
  



GO

