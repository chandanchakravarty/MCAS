IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHolder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHolder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetHolder
Created by           : Shrikant Bhatt
Date                    : 26/04/2005
Purpose               : To get Producer  from MNT_User_LIST table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
drop PROC Dbo.Proc_GetHolder
------   ------------       -------------------------*/
CREATE PROC [dbo].[Proc_GetHolder]
(
@VEHICLE_ID 	varchar(4)
)
AS
SELECT	A.HOLDER_ID, A.HOLDER_NAME 
FROM	MNT_HOLDER_INTEREST_LIST A
WHERE 	A.HOLDER_ID  NOT IN(SELECT HOLDER_ID FROM POL_ADD_OTHER_INT WHERE VEHICLE_ID= @VEHICLE_ID)
ORDER BY HOLDER_NAME 






GO

