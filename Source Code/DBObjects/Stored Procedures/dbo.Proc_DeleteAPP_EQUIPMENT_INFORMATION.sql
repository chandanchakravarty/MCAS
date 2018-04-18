IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_EQUIPMENT_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_EQUIPMENT_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
          
Proc Name       : Proc_DeleteAPP_EQUIPMENT_INFORMATION          
Created by      : Swastika Gaur          
Date            : 3rd Apr '06          
Purpose         : Delete Equipment Information        
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Dbo.Proc_DeleteAPP_EQUIPMENT_INFORMATION          
(          
 @CUSTOMER_ID INT,    
 @APP_ID INT,    
 @APP_VERSION_ID INT,    
 @EQUIP_ID INT   
  
)          
AS          
BEGIN     
     
 -- Delete Equipment Info
 DELETE FROM APP_WATERCRAFT_EQUIP_DETAILLS
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND EQUIP_ID=@EQUIP_ID
 
END         
      
   







GO

