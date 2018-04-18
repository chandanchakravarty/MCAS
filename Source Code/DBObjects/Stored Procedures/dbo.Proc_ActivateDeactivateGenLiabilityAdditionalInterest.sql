IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateGenLiabilityAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateGenLiabilityAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name    : dbo.Proc_ActivateDeactivateGenLiabilityAdditionalInterest              
Created by   : Sumit Chhabra          
Date         : 04/05/2006           
Purpose      : Activate/Deactivate records at POL_GENERAL_HOLDER_INTEREST  

Revison History :            
Used In  :   Wolverine                   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/               
          
          
CREATE PROC Dbo.Proc_ActivateDeactivateGenLiabilityAdditionalInterest        
(    
 @CUSTOMER_ID int,  
 @POLICY_ID int,  
 @POLICY_VERSION_ID smallint,  
 @HOLDER_ID int,          
 @ADD_INT_ID INT,      
 @IS_ACTIVE NCHAR(4)          
                 
)            
AS            
          
            
BEGIN            
 UPDATE POL_GENERAL_HOLDER_INTEREST SET IS_ACTIVE=@IS_ACTIVE       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND           
    POLICY_VERSION_ID=@POLICY_VERSION_ID AND ADD_INT_ID = @ADD_INT_ID     
END          
        
      
      
      
    
  



GO

