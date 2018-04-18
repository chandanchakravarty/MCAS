IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ACTIVATEDEACTIVATEAPP_HOME_OWNER_CHIMNEY_STOVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ACTIVATEDEACTIVATEAPP_HOME_OWNER_CHIMNEY_STOVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_ActivateDeactivateAPP_HOME_OWNER_CHIMNEY_STOVE  
Created by      : Sumit Chhabra
Date            : 21/10/2005  
Purpose     	: Activate/ Deactivate record in APP_HOME_OWNER_CHIMNEY_STOVE  
Revison History :  
Used In  : BRICS  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC DBO.PROC_ACTIVATEDEACTIVATEAPP_HOME_OWNER_CHIMNEY_STOVE  
(  
 @CUSTOMER_ID     INT,  
 @APP_ID     INT,  
 @APP_VERSION_ID     SMALLINT,  
 @FUEL_ID     SMALLINT,  
 @IS_ACTIVE NCHAR(2)
)  
AS  
BEGIN  
	IF EXISTS(SELECT * FROM APP_HOME_OWNER_CHIMNEY_STOVE WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID
		AND APP_VERSION_ID=@APP_VERSION_ID AND FUEL_ID=@FUEL_ID)
		UPDATE APP_HOME_OWNER_CHIMNEY_STOVE SET IS_ACTIVE=@IS_ACTIVE WHERE CUSTOMER_ID=@CUSTOMER_ID 
			AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND FUEL_ID=@FUEL_ID

END



GO

