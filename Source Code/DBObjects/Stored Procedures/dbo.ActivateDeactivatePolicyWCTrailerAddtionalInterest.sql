IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateDeactivatePolicyWCTrailerAddtionalInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ActivateDeactivatePolicyWCTrailerAddtionalInterest]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name    : dbo.ActivateDeactivatePolicyWCTrailerAddtionalInterest              
Created by   : Vijay Arora    
Date         : 29-11-2005  
Purpose      : Activate Deactivate the record in POL_WATERCRAFT_TRAILER_ADD_INT Table.    
Revison History :            
Used In  :   Wolverine                   
 ------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/               
create PROC dbo.ActivateDeactivatePolicyWCTrailerAddtionalInterest            
(            
 @CUSTOMER_ID      int,            
 @POLICY_ID   int,            
 @POLICY_VERSION_ID  smallint,            
 @TRAILER_ID   smallint,            
 @HOLDER_ID int,          
 @ADD_INT_ID INT,      
 @IS_ACTIVE char(1)          
)            
AS            
BEGIN            
 UPDATE POL_WATERCRAFT_TRAILER_ADD_INT SET IS_ACTIVE=@IS_ACTIVE       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND           
    POLICY_VERSION_ID=@POLICY_VERSION_ID  AND TRAILER_ID=@TRAILER_ID    AND ADD_INT_ID = @ADD_INT_ID      
END          
        
      
      
      
    
  



GO

