IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyGenLiabilityAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyGenLiabilityAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name    : dbo.Proc_DeletePolicyGenLiabilityAdditionalInterest            
Created by   : Sumit Chhabra        
Date         : 20 October.,2005             
Purpose      : Delete the record from  POL_GENERAL_HOLDER_INTEREST  Table          
Revison History :          
Used In  :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
        
        
        
CREATE PROC Proc_DeletePolicyGenLiabilityAdditionalInterest          
(          
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID smallint,  
 @HOLDER_ID int,        
 @ADD_INT_ID INT                    
)          
AS          
           
          
BEGIN          
  DELETE FROM POL_GENERAL_HOLDER_INTEREST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND         
   POLICY_VERSION_ID=@POLICY_VERSION_ID AND ADD_INT_ID=@ADD_INT_ID      
           
END        
      
    
  



GO

