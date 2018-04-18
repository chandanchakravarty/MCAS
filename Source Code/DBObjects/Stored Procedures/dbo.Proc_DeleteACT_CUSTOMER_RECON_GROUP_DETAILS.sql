IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_CUSTOMER_RECON_GROUP_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_CUSTOMER_RECON_GROUP_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Proc_DeleteACT_CUSTOMER_RECON_GROUP_DETAILS
Created by      : Vijay
Date            : 07/05/2005
Purpose    	: delete the values in ACT_CUSTOMER_RECON_GROUP_DETAILS
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_DeleteACT_CUSTOMER_RECON_GROUP_DETAILS
(
	@IDEN_ROW_NO    	int
)
AS
BEGIN

	IF EXISTS ( SELECT IS_COMMITTED FROM ACT_RECONCILIATION_GROUPS MAIN
				LEFT JOIN ACT_CUSTOMER_RECON_GROUP_DETAILS DET ON  MAIN.GROUP_ID = DET.GROUP_ID
				WHERE DET.IDEN_ROW_NO = @IDEN_ROW_NO AND ISNULL(MAIN.IS_ACTIVE,'N') = 'N')
	BEGIN 
		DELETE FROM ACT_CUSTOMER_RECON_GROUP_DETAILS
		WHERE IDEN_ROW_NO = @IDEN_ROW_NO
	END
END







GO

