IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_RECONCILIATION_GROUPS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_RECONCILIATION_GROUPS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Proc_UpdateACT_RECONCILIATION_GROUPS
Created by      : Vijay Joshi
Date            : 6/29/2005
Purpose    	:Update values in ACT_RECONCIAITION_GROUP
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_UpdateACT_RECONCILIATION_GROUPS
CREATE PROC dbo.Proc_UpdateACT_RECONCILIATION_GROUPS
(
	@GROUP_ID     		int,
	@RECON_ENTITY_ID     	int,
	@RECON_ENTITY_TYPE     	nvarchar(10),
	@IS_COMMITTED     	nchar(2),
	@DATE_COMMITTED     	datetime,
	@COMMITTED_BY     	int,
	@MODIFIED_BY     	int,
	@LAST_UPDATED_DATETIME 	datetime
)
AS
BEGIN

	IF NOT EXISTS( SELECT GROUP_ID FROM ACT_RECONCILIATION_GROUPS
			WHERE GROUP_ID  = @GROUP_ID AND  RECON_ENTITY_ID = @RECON_ENTITY_ID)
	BEGIN 
		DELETE FROM ACT_CUSTOMER_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID
		DELETE FROM ACT_AGENCY_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID
		DELETE FROM ACT_VENDOR_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID
	END

	UPDATE ACT_RECONCILIATION_GROUPS
	SET    RECON_ENTITY_ID = @RECON_ENTITY_ID,
		RECON_ENTITY_TYPE = @RECON_ENTITY_TYPE,
		IS_COMMITTED = @IS_COMMITTED,
		DATE_COMMITTED = @DATE_COMMITTED,
		COMMITTED_BY = @COMMITTED_BY,
		MODIFIED_BY = @MODIFIED_BY,
		LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
	WHERE GROUP_ID = @GROUP_ID

END




GO

