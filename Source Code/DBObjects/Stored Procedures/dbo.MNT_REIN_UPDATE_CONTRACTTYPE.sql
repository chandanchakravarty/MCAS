IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_UPDATE_CONTRACTTYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_UPDATE_CONTRACTTYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
CREATED BY 		: Harmanjeet Singh
CREATED DATE	: April 19,2007
Purpose			: To implement the UPDATE on the Contract Type
*/
CREATE PROCEDURE [dbo].[MNT_REIN_UPDATE_CONTRACTTYPE]
(
	@CONTRACT_TYPE_ID	INT,
	@CONTRACT_TYPE_DESC NVARCHAR(50)
)
AS
BEGIN
	UPDATE MNT_REIN_CONTRACT_TYPE

	SET 	CONTRACT_TYPE_DESC = @CONTRACT_TYPE_DESC
	
	WHERE 	
	CONTRACT_TYPE_ID = @CONTRACT_TYPE_ID
END

GO

