IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name     : dbo.Proc_DeletePolicyAdditionalInterest          
Created by    : Vijay Arora    
Date          : 10-11-2005    
Purpose       : Delete the record from  POL_ADD_OTHER_INT  Table          
Revison History :          
Used In   :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/             
CREATE     PROC DBO.Proc_DeletePolicyAdditionalInterest          
(          
 @CUSTOMER_ID      int,          
 @POLICY_ID   int,          
 @POLICY_VERSION_ID  smallint,          
 @HOLDER_ID   INT,          
 @VEHICLE_ID   smallint,        
 @ADD_INT_ID INT         
               
)          
AS          
        
          
BEGIN          
    
    
    DELETE FROM POL_ADD_OTHER_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND         
     POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID and ADD_INT_ID=@ADD_INT_ID        

          
END        
      
    
  





GO

