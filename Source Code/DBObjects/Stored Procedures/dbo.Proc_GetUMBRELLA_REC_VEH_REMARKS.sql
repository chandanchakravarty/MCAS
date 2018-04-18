IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMBRELLA_REC_VEH_REMARKS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMBRELLA_REC_VEH_REMARKS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateUMBRELLA_REC_VEH_REMARKS
Created by      : Pradeep
Date            : 5/18/2005
Purpose    	:Updates remarks in APP_HOME_OWNER_SUB_INSU
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE     PROC Dbo.Proc_GetUMBRELLA_REC_VEH_REMARKS
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@REC_VEH_ID Int
)
AS

BEGIN
	SELECT REMARKS FROM
	APP_UMBRELLA_RECREATIONAL_VEHICLES
	WHERE CUSTOMER_ID = CUSTOMER_ID AND
	      APP_ID = APP_ID AND
	      APP_VERSION_ID = APP_VERSION_ID AND
	      REC_VEH_ID = @REC_VEH_ID	

END








GO

