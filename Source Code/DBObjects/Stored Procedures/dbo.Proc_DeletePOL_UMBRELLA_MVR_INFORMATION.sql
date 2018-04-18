IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePOL_UMBRELLA_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePOL_UMBRELLA_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
            
Proc Name       : Proc_DeletePOL_UMBRELLA_MVR_INFORMATION    
Created by      : Sumit Chhabra
Date            : 22-03-2006  
Purpose         : Delete of Driver POL_UMBRELLA_MVR_INFORMATION            
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC Dbo.Proc_DeletePOL_UMBRELLA_MVR_INFORMATION            
(            
 @CUSTOMER_ID INT,      
 @POLICY_ID INT,      
 @POLICY_VERSION_ID INT,      
 @DRIVER_ID INT,      
 @POL_UMB_MVR_ID INT      
)            
AS            
BEGIN            
 DELETE FROM POL_UMBRELLA_MVR_INFORMATION WHERE       
  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND       
  DRIVER_ID=@DRIVER_ID AND POL_UMB_MVR_ID=@POL_UMB_MVR_ID        
END           
        
      
    
  



GO

