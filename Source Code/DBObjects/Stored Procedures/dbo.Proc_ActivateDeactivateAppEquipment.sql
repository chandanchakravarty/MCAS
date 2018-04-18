IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAppEquipment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAppEquipment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateAppEquipment     
Created by      : Swastika Gaur          
Date            :  3rd Apr'06                          
Purpose         :Activate/ Deactivate Equipment       
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC DBO.Proc_ActivateDeactivateAppEquipment          
(                
@CUSTOMER_ID     INT,                
@APP_ID     INT,                
@APP_VERSION_ID     SMALLINT,                
@EQUIP_ID     SMALLINT,        
@IS_ACTIVE NCHAR(1)        
)                
AS                
BEGIN                
        
	UPDATE APP_WATERCRAFT_EQUIP_DETAILLS SET IS_ACTIVE=@IS_ACTIVE WHERE        
	 CUSTOMER_ID=@CUSTOMER_ID AND         
	 APP_ID=@APP_ID AND        
	 APP_VERSION_ID=@APP_VERSION_ID AND        
	 EQUIP_ID=@EQUIP_ID     

END        
    


GO

