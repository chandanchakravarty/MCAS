IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteDistributionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteDistributionDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 

/*----------------------------------------------------------
Proc Name          : Dbo.Proc_DeleteDistributionDetails
Created by           : Mohit Gupta
Date                    : 26/06/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_DeleteDistributionDetails
create   PROCEDURE dbo.Proc_DeleteDistributionDetails
(
	@IDEN_ROW_ID int
)
AS
BEGIN

DELETE FROM ACT_DISTRIBUTION_DETAILS
WHERE IDEN_ROW_ID=@IDEN_ROW_ID 

return 1
END




GO

