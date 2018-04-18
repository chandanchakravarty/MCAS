IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBMasterInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBMasterInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLOBMasterInformation
Created by         : Priya
Date               : 24/06/2005
Purpose            : To get details from MNT_LOB_MASTER
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_GetLOBMasterInformation
(
	@LOB_ID   int
)
AS
BEGIN
	SELECT 
		LOB_ID, LOB_CODE, LOB_DESC, LOB_CATEGORY, LOB_TYPE,
		LOB_PKG, LOB_ACORD_STD, DEF_CLAIMS_TYPE, LOB_PREFIX,
		LOB_SUFFIX, LOB_SEED, MAPPING_LOOKUP_ID, OVERRIDE_LOB_PREFIX
	FROM MNT_LOB_MASTER 
	WHERE LOB_ID=@LOB_ID 

END


GO

