IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAcord_Quote_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAcord_Quote_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateAcord_Quote_Details
Created by      : Nidhi
Date            : 1/5/2007
Purpose         : To update record in Acord Quote Details
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_UpdateAcord_Quote_Details
(
	@CUSTOMER_ID  		INT,
	@APP_ID  		INT,
	@APP_VERSION_ID  		INT,
	@STATE_ID  		INT,
	@GUID  		VARCHAR(100)
)
AS

BEGIN
	UPDATE ACORD_QUOTE_DETAILS
	SET 
	  APP_ID = @APP_ID  ,                      
	  APP_VERSION_ID = @APP_VERSION_ID,                   
	  STATE_ID = @STATE_ID,
	  CUSTOMER_ID=@CUSTOMER_ID 		  
	WHERE 
	  INSURANCE_SVC_RQ = @GUID 

END



GO

