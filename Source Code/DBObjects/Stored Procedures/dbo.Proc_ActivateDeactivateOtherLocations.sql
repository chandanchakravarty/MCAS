IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateOtherLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateOtherLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : Proc_ActivateDeactivateOtherLocations
Created by      : Swastika   
Date            : 19th Jun'06
Purpose         : To Activate/Deactivate the record in Customer table    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop proc dbo.Proc_ActivateDeactivateOtherLocations
CREATE  PROC dbo.Proc_ActivateDeactivateOtherLocations
(    
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID SmallInt,
 	@LOCATION_ID  SmallInt,    
 	@IS_ACTIVE  	NChar(1)    
)    
AS    
BEGIN  

	UPDATE APP_OTHER_LOCATIONS
	SET     
  		Is_Active 	= @IS_ACTIVE   
	WHERE    
    		LOCATION_ID   	= @LOCATION_ID AND
		CUSTOMER_ID =  @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		LOCATION_ID = @LOCATION_ID   

	RETURN 1
    
END




GO

