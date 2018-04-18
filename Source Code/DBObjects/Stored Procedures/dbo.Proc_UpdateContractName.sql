IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateContractName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateContractName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*
CREATED BY 		: Deepak Batra
CREATED DATE		: 16 Jan 2006
Purpose			: To implement the update on the Contract Name Screen
*/
CREATE PROCEDURE Proc_UpdateContractName
(
	@CONTRACT_NAME_ID	INT,
	@CONTRACT_NAME 		NVARCHAR(150),
	@CONTRACT_DESCRIPTION	NVARCHAR(500)
)
AS
BEGIN
	UPDATE MNT_CONTRACT_NAME

	SET 	CONTRACT_NAME = @CONTRACT_NAME,	CONTRACT_DESCRIPTION = @CONTRACT_DESCRIPTION
	
	WHERE 	
	CONTRACT_NAME_ID = @CONTRACT_NAME_ID
END




GO

