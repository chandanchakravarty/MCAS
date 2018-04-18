IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePOL_EQUIPMENT_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePOL_EQUIPMENT_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
          
Proc Name       : Proc_DeletePOL_EQUIPMENT_INFORMATION          
Created by      : Swastika Gaur          
Date            : 3rd Apr '06          
Purpose         : Delete Pol Equipment Information        
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Dbo.Proc_DeletePOL_EQUIPMENT_INFORMATION          
(          
 @CUSTOMER_ID INT,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID INT,    
 @EQUIP_ID INT   
  
)          
AS          
BEGIN     
     
 -- Delete Equipment Info
 DELETE FROM POL_WATERCRAFT_EQUIP_DETAILLS
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND EQUIP_ID=@EQUIP_ID
 
END         


GO

