IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_HOME_OWNER_SCH_ITEMS_CVGS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_HOME_OWNER_SCH_ITEMS_CVGS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------        
Proc Name   : dbo.Proc_DeleteAPP_HOME_OWNER_SCH_ITEMS_CVGS       
Created by  : gAURAV        
Date        : 16 June,2005      
Purpose     :         
Revison History  :              
------------------------------------------------------------                    
Date     Review By          Comments                  
-----------------------------------------------------------*/  
CREATE PROCEDURE Proc_DeleteAPP_HOME_OWNER_SCH_ITEMS_CVGS
(	
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID smallint, 
	@ITEM_ID smallint
)

As

DELETE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
      APP_ID =  @APP_ID AND
      APP_VERSION_ID =  @APP_VERSION_ID AND		
     ITEM_ID =  @ITEM_ID	
	

RETURN 1




GO

