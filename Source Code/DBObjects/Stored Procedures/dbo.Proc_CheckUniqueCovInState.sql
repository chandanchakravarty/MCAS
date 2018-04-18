IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckUniqueCovInState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckUniqueCovInState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_CheckUniqueCovInState
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Modifed by 	:Pravesh k Chandel
Modified Date	: 9 aug 2007

Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------
drop proc dbo.Proc_CheckUniqueCovInState
*/
CREATE   PROCEDURE dbo.Proc_CheckUniqueCovInState
(
	@COV_CODE varchar(10),
	@STATE_ID int,
	@lOB_ID int,
	@covCount int output,
	@CALLED_FROM varchar(15)=null
)
AS
BEGIN
SET @covCount=0
if (@CALLED_FROM='REINSURANCE')
	SELECT @covCount=COUNT(*) FROM MNT_REINSURANCE_COVERAGE
	WHERE STATE_ID=@STATE_ID AND LOB_ID=@LOB_ID AND COV_CODE=@COV_CODE
else
	SELECT @covCount=COUNT(*) FROM MNT_COVERAGE
	WHERE STATE_ID=@STATE_ID AND LOB_ID=@LOB_ID AND COV_CODE=@COV_CODE

END




GO

