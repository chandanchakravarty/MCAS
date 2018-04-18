IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLMNT_REINSURANCE_EXCESSLAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLMNT_REINSURANCE_EXCESSLAYER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Created by      : Deepak Batra
Date            : 06 Jan 2006
Purpose    	: Evaluation for the Excess Layer screen
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_GetXMLMNT_REINSURANCE_EXCESSLAYER
(
@EXCESS_ID	INT,
@CONTRACT_ID	INT,
@LAYER_TYPE	CHAR(1)
)
AS
BEGIN

SELECT
	EXCESS_ID, 
	CONTRACT_ID, 
	LAYER_AMOUNT, 
	UNDERLYING_AMOUNT, 
	LAYER_PREMIUM, 
	CEDING_COMMISSION, 	
	AC_PREMIUM,
	IS_ACTIVE,
	CREATED_BY,
	CREATED_DATETIME,
	MODIFIED_BY,
	LAST_UPDATED_DATETIME,
	PARTICIPATION_AMOUNT,
	PRORATA_AMOUNT,
	LAYER_TYPE

FROM  MNT_REINSURANCE_EXCESS

WHERE 	EXCESS_ID 	= @EXCESS_ID	AND 
	CONTRACT_ID 	= @CONTRACT_ID	AND
	LAYER_TYPE	= @LAYER_TYPE
END


GO

