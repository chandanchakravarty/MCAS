IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookUpTableDataForLookUpName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookUpTableDataForLookUpName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLookUpTableDataForLookUpName
Created by           : Mohit Gupta
Date                    : 
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE dbo.Proc_GetLookUpTableDataForLookUpName
(
	@LookUpName nvarchar(6)
)
AS
BEGIN
	SELECT LOOKUP_ID,LOOKUP_NAME + ' - ' + LOOKUP_DESC LOOKUP_DESC FROM MNT_LOOKUP_TABLES WHERE LOOKUP_NAME=@LookUpName
END




GO

