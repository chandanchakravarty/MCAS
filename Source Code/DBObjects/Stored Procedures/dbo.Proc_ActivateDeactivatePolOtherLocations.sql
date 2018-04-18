IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolOtherLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolOtherLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : Proc_ActivateDeactivatePolOtherLocations
Created by      : Swastika   
Date            : 19th Jun'06
Purpose         : To Activate/Deactivate the record in Customer table    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--drop proc dbo.Proc_ActivateDeactivatePolOtherLocations
CREATE  PROC dbo.Proc_ActivateDeactivatePolOtherLocations
(    
	@CUSTOMER_ID Int,
	@POLICY_ID Int,
	@POLICY_VERSION_ID SmallInt,
 	@LOCATION_ID  SmallInt,    
 	@IS_ACTIVE  	NChar(1)    
)    
AS    
BEGIN   

	UPDATE POL_OTHER_LOCATIONS
	SET     
  		IS_ACTIVE    = @IS_ACTIVE   
	WHERE    
    		LOCATION_ID  = @LOCATION_ID AND
		CUSTOMER_ID  = @CUSTOMER_ID AND
		POLICY_ID    = @POLICY_ID AND
		LOCATION_ID  = @LOCATION_ID   

	RETURN 1
    
END






GO

