IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLobAndSubLOBs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLobAndSubLOBs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetLobAndSubLOBs
Created by         : Vijay Joshi
Date               : 27/04/2005
Purpose            : To get Lob And SUBLObs from MNT_SUB_LOB_MASTER table
Revison History    :
Used In            :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_GetLobAndSubLOBs

AS
BEGIN

SELECT MNT_LOB_MASTER.LOB_ID, LOB_DESC, SUB_LOB_ID, SUB_LOB_DESC  
FROM MNT_LOB_MASTER
LEFT JOIN MNT_SUB_LOB_MASTER ON MNT_LOB_MASTER.LOB_ID = MNT_SUB_LOB_MASTER.LOB_ID
ORDER BY  LOB_DESC,SUB_LOB_DESC
END



GO

