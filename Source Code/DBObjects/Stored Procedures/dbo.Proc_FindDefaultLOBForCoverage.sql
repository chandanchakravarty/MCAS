IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FindDefaultLOBForCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FindDefaultLOBForCoverage]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_FindDefaultLOBForCoverage
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_FindDefaultLOBForCoverage
(
	@Lob_Id int,
	@TotalRecords int output
)
AS
BEGIN

SELECT @TotalRecords= COUNT(*) FROM MNT_COVERAGE WHERE LOB_ID=@Lob_Id and IS_DEFAULT='1'

END


GO

