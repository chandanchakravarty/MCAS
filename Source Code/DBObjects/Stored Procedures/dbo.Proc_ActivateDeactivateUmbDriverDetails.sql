IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateUmbDriverDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateUmbDriverDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateUmbDriverDetails
Created by      : NIDHI    
Date            : 16 JUNE ,2005    
Purpose         : To Activate/Deactivate the record of driver
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_ActivateDeactivateUmbDriverDetails
(    
 	@CUSTOMER_ID	int,
	@APP_ID		int,
	@APP_VERSION_ID	int,
	@DRIVER_ID	int,
 	@IS_ACTIVE  	Char(1)    
)    
AS    
BEGIN    
	UPDATE APP_UMBRELLA_DRIVER_DETAILS
	SET     
  		IS_ACTIVE 	= @IS_ACTIVE   
	WHERE    
		CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		DRIVER_ID = @DRIVER_ID
    
END





GO

