IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateContractType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateContractType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_UpdateContractType

/*
CREATED BY 		: Deepak Batra
CREATED DATE		: 16 Jan 2006
Purpose			: To implement the update on the Contract Type
*/
CREATE PROCEDURE Proc_UpdateContractType
(
	@CONTRACTTYPEID	INT,
	@CONTRACT_TYPE_DESC NVARCHAR(50),
	@MODIFIED_BY INT=0,
	@LAST_UPDATED_DATETIME DATETIME=null
)
AS
BEGIN
	UPDATE MNT_REINSURANCE_CONTRACT_TYPE

	SET 	CONTRACT_TYPE_DESC = @CONTRACT_TYPE_DESC,
		MODIFIED_BY=@MODIFIED_BY,
		LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
	
	WHERE 	
	CONTRACTTYPEID = @CONTRACTTYPEID
END








GO

