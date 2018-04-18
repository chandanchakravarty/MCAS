IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHolderHomeOwner]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHolderHomeOwner]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Proc_GetHolderHomeOwner
Created by         : Vjay Joshi
Date               : 17 May,2005
Purpose            : To get Holder which are not under the specified dwelling id
Revison History    :
Used In            :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_GetHolderHomeOwner
(
	@DWELLING_ID int
)
AS
SELECT	A.HOLDER_ID, A.HOLDER_NAME 
FROM	MNT_HOLDER_INTEREST_LIST A
WHERE 	A.HOLDER_ID NOT IN(SELECT HOLDER_ID FROM APP_HOME_OWNER_ADD_INT WHERE DWELLING_ID = @DWELLING_ID)





GO

