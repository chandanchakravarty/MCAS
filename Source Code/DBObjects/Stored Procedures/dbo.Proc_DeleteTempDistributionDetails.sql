IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteTempDistributionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteTempDistributionDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_DeleteDistributionDetails
Created by           : ajit
Date                    : 13/9/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_DeleteTempDistributionDetails
(
	@IDEN_ROW_ID int
)
AS
BEGIN

DELETE FROM TEMP_ACT_DISTRIBUTION_DETAILS
WHERE IDEN_ROW_ID=@IDEN_ROW_ID 

return 1
END









GO

