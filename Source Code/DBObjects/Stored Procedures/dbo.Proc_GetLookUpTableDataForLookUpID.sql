IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookUpTableDataForLookUpID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookUpTableDataForLookUpID]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLookUpTableDataForLookUpID
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetLookUpTableDataForLookUpID
(
  @LookUpId int
)
AS
BEGIN
SELECT LOOKUP_ID,LOOKUP_NAME + ' - ' + LOOKUP_DESC LOOKUP_DESC FROM MNT_LOOKUP_TABLES WHERE LOOKUP_ID=@LookUpId
END


GO

