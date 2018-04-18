IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETE_APP_IM_SCH_ITEMS_CVGS_ALL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETE_APP_IM_SCH_ITEMS_CVGS_ALL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*                            
----------------------------------------------------------                                
Proc Name       : dbo.PROC_DELETE_APP_IM_SCH_ITEMS_CVGS_ALL                            
Modified by      : Shafi                              
Date            : 08-11-2006
Purpose         : Delete Coverage By Its Code And Delete the Records From Child Table
Revison History :                                
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------                               
*/                                                                                                                                                                                                                                    

CREATE PROCEDURE dbo.PROC_DELETE_APP_IM_SCH_ITEMS_CVGS_ALL
(	
	@CUSTOMER_ID INT,
	@APP_ID INT,
	@APP_VERSION_ID SMALLINT,
    @ITEM_ID int
)

AS

--APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS
DELETE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WHERE 
      CUSTOMER_ID = @CUSTOMER_ID AND
      APP_ID =  @APP_ID AND
      APP_VERSION_ID =  @APP_VERSION_ID AND
      ITEM_ID=@ITEM_ID

--APP_HOME_OWNER_SCH_ITEMS_CVGS
      
DELETE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
      APP_ID =  @APP_ID AND
      APP_VERSION_ID =  @APP_VERSION_ID AND
      ITEM_ID=@ITEM_ID
 
	

RETURN 1




GO

