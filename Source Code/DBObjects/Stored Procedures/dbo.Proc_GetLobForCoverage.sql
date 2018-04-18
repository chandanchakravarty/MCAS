IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLobForCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLobForCoverage]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLobForCoverage 
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE [dbo].[Proc_GetLobForCoverage] 
(
	@Cov_id int
)
AS
BEGIN
SELECT MNT_COVERAGE.LOB_ID LOB_ID,LOB_DESC,COV_DES FROM MNT_COVERAGE,MNT_LOB_MASTER
WHERE
MNT_COVERAGE.LOB_ID=MNT_LOB_MASTER.LOB_ID AND
COV_ID=@Cov_id
END



GO

