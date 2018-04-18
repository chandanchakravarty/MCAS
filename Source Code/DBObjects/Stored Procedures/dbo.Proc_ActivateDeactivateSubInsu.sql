IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateSubInsu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateSubInsu]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateSubInsu
Created by      : Pradeep    
Date            : May 23,2005    
Purpose         : To Activate/Deactivate the record of Subject of Insurance
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROC dbo.Proc_ActivateDeactivateSubInsu
(    
 	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID int,
	@SUB_INSU_ID int,   
 	@IS_ACTIVE  	Char(1)    
)    
AS    
BEGIN    
	UPDATE APP_HOME_OWNER_SUB_INSU
	SET IS_ACTIVE = @IS_ACTIVE
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	      APP_ID = 	@APP_ID AND
	      APP_VERSION_ID = 	@APP_VERSION_ID AND
	      SUB_INSU_ID = @SUB_INSU_ID 	
	
    
END






GO

