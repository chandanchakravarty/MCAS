IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteReconGroupDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteReconGroupDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-----------------------------------------------

CREATE BY		: Vijay Joshi
CREATED DATETIME: 3 March 2006, 6:14 PM
PURPOSE			: Deleting all line items against any recon group

------------------------------------------------*/
CREATE PROC dbo.Proc_DeleteReconGroupDetails
(
	@GROUP_ID INT
)
as
BEGIN 

	DELETE FROM ACT_CUSTOMER_RECON_GROUP_DETAILS
		WHERE GROUP_ID = @GROUP_ID
	DELETE FROM ACT_AGENCY_RECON_GROUP_DETAILS
		WHERE GROUP_ID = @GROUP_ID
	DELETE FROM ACT_VENDOR_RECON_GROUP_DETAILS
		WHERE GROUP_ID = @GROUP_ID
	DELETE FROM ACT_TAX_RECON_GROUP_DETAILS
		WHERE GROUP_ID = @GROUP_ID


END



GO

