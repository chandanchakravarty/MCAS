IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyWCTrailerAdditionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyWCTrailerAdditionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name     : dbo.Proc_DeletePolicyWCTrailerAdditionalInterest          
Created by    : Vijay Arora    
Date          : 29-11-2005
Purpose       : Delete the record from  POL_WATERCRAFT_TRAILER_ADD_INT  Table          
Revison History :          
Modified By		: Pravesh K Chandel
Modified By		: 7 Jan 2009
Purpose			: remove Holder Id condition from Where clouse while deleting itrack 5263
Used In   :   Wolverine                 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
 drop PROC DBO.Proc_DeletePolicyWCTrailerAdditionalInterest                         
------   ------------       -------------------------*/             
CREATE    PROC DBO.Proc_DeletePolicyWCTrailerAdditionalInterest          
(          
 @CUSTOMER_ID   INT,          
 @POLICY_ID    INT,          
 @POLICY_VERSION_ID  SMALLINT,          
 @HOLDER_ID   INT,          
 @TRAILER_ID  SMALLINT,        
 @ADD_INT_ID INT         
               
)          
AS          
        
          
BEGIN          
--    
-- IF EXISTS(SELECT * FROM POL_WATERCRAFT_TRAILER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND         
--   POLICY_VERSION_ID=@POLICY_VERSION_ID AND HOLDER_ID=@HOLDER_ID AND  TRAILER_ID=@TRAILER_ID
--   AND ADD_INT_ID = @ADD_INT_ID)        

 IF EXISTS(SELECT * FROM POL_WATERCRAFT_TRAILER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND         
   POLICY_VERSION_ID=@POLICY_VERSION_ID AND  TRAILER_ID=@TRAILER_ID
   AND ADD_INT_ID = @ADD_INT_ID)            
 BEGIN    
    
    DELETE FROM POL_WATERCRAFT_TRAILER_ADD_INT WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND         
     POLICY_VERSION_ID=@POLICY_VERSION_ID AND  TRAILER_ID=@TRAILER_ID
     AND ADD_INT_ID = @ADD_INT_ID
 END    
          
END        
      
    
  




GO

