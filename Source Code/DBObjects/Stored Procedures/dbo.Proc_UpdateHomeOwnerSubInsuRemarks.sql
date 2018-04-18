IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateHomeOwnerSubInsuRemarks]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateHomeOwnerSubInsuRemarks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateHomeOwnerSubInsuRemarks
Created by      : Pradeep
Date            : 5/18/2005
Purpose    	:Updates remarks in APP_HOME_OWNER_SUB_INSU
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE    PROC Dbo.Proc_UpdateHomeOwnerSubInsuRemarks
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@SUB_INSU_ID Int,
	@REMARKS NVarChar(500)	
)
AS

BEGIN
	UPDATE APP_HOME_OWNER_SUB_INSU
	SET REMARKS = @REMARKS
	WHERE CUSTOMER_ID = CUSTOMER_ID AND
	      APP_ID = APP_ID AND
	      APP_VERSION_ID = APP_VERSION_ID AND
	      SUb_INSU_ID = @SUB_INSU_ID	

END







GO

