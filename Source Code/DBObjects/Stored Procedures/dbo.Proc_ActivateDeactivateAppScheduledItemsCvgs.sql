IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAppScheduledItemsCvgs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAppScheduledItemsCvgs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateAppScheduledItemsCvgs        
Created by      : Swastika Gaur          
Date            : 24th July'06                         
Purpose         :Activate/ Deactivate Scheduled Items/Coverages       
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                

-- drop proc dbo.Proc_ActivateDeactivateAppScheduledItemsCvgs              
CREATE PROC dbo.Proc_ActivateDeactivateAppScheduledItemsCvgs              
(                
@CUSTOMER_ID     INT,                
@APP_ID     INT,                
@APP_VERSION_ID     INT,                
@ITEM_ID     INT, 
@ITEM_DETAIL_ID     INT,  
@IS_ACTIVE NCHAR(1)        
)                
AS                
BEGIN                
        
	UPDATE APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS SET IS_ACTIVE=@IS_ACTIVE WHERE        
	 CUSTOMER_ID=@CUSTOMER_ID AND         
	 APP_ID=@APP_ID AND        
	 APP_VERSION_ID=@APP_VERSION_ID AND        
	 ITEM_ID=@ITEM_ID AND
	 ITEM_DETAIL_ID = @ITEM_DETAIL_ID
	  

END        
    
      



GO

