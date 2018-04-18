IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookUpTableData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookUpTableData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLookUpTableData
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- DROP PROCEDURE dbo.Proc_GetLookUpTableData
CREATE   PROCEDURE dbo.Proc_GetLookUpTableData
@Lang_id int=1
AS
BEGIN
SELECT MT.LOOKUP_ID,MT.LOOKUP_NAME + ' - ' + isnull(mntl.LOOKUP_DESC,mt.LOOKUP_DESC) LOOKUP_DESC FROM MNT_LOOKUP_TABLES MT
left outer join MNT_LOOKUP_TABLES_MULTILINGUAL MNTL on MNTL.LOOKUP_ID = MT.LOOKUP_ID and MNTL.LANG_ID=@Lang_id  ORDER BY MT.LOOKUP_NAME
END


GO

