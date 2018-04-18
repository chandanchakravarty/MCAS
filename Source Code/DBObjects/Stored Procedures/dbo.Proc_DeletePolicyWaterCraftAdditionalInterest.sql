IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyWaterCraftAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyWaterCraftAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name     : dbo.Proc_DeletePolicyWaterCraftAdditionalInterest            
Created by    : Vijay Arora      
Date          : 23-11-2005      
Purpose       : Delete the record from  POL_WATERCRAFT_COV_ADD_INT  Table            
Revison History :            
Used In   :   Wolverine                   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/               
create  PROC DBO.Proc_DeletePolicyWaterCraftAdditionalInterest            
(            
 @CUSTOMER_ID   INT,            
 @POLICY_ID    INT,            
 @POLICY_VERSION_ID  SMALLINT,            
 @HOLDER_ID   INT,            
 @BOAT_ID  SMALLINT,          
 @ADD_INT_ID INT           
)            
AS            
          
            
BEGIN            
      
 IF EXISTS(SELECT * FROM POL_WATERCRAFT_COV_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND           
   POLICY_VERSION_ID=@POLICY_VERSION_ID  AND  BOAT_ID=@BOAT_ID  
   AND ADD_INT_ID = @ADD_INT_ID)          
      
 BEGIN      
      
    DELETE FROM POL_WATERCRAFT_COV_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND           
     POLICY_VERSION_ID=@POLICY_VERSION_ID AND  BOAT_ID=@BOAT_ID    
     AND ADD_INT_ID = @ADD_INT_ID  
 END      
            
END          
        
      
    



GO

