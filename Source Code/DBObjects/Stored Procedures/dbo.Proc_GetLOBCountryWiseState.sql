IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBCountryWiseState]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBCountryWiseState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name   : dbo.Proc_GetLOBCountryWiseState
Created by  : Praveen Jain
Date        : 13-July-2010
Purpose     : To display the Country wise assigned State List
Revison History  :
------------------------------------------------------------
Date     Review By          Comments

-------------------------------------------*/
-- Proc_GetLOBCountryWiseState 5, null
-- Drop PROCEDURE dbo.Proc_GetLOBCountryWiseState
CREATE PROCEDURE [dbo].[Proc_GetLOBCountryWiseState]
(
 @CountryID Int, 
 @LobId int
)
AS
BEGIN
--Declare @LobId as Int, @CountryID as Int
--Select @LobId = 2, @CountryID = 5
if(ISNULL(@CountryID, 0) = 0)
	if(ISNULL(@LobId, 0) = 0)
		SELECT Distinct MNTS.STATE_ID,MNTS.STATE_NAME
		FROM MNT_COUNTRY_STATE_LIST MNTS
		INNER JOIN MNT_LOB_STATE MNTL ON MNTS.STATE_ID= MNTL.STATE_ID
		WHERE  MNTL.PARENT_LOB is null -- MNTL.LOB_ID= @LobId and 
		--AND MNTS.COUNTRY_ID = @CountryID
		ORDER BY STATE_NAME
	else
		SELECT Distinct MNTS.STATE_ID,MNTS.STATE_NAME
		FROM MNT_COUNTRY_STATE_LIST MNTS
		INNER JOIN MNT_LOB_STATE MNTL ON MNTS.STATE_ID= MNTL.STATE_ID
		WHERE  MNTL.PARENT_LOB is null AND MNTL.LOB_ID= @LobId
		--AND MNTS.COUNTRY_ID = @CountryID
		ORDER BY STATE_NAME
else if(ISNULL(@CountryID, 0) <> 0)
	if(ISNULL(@LobId, 0) <> 0)
		SELECT Distinct MNTS.STATE_ID,MNTS.STATE_NAME
		FROM MNT_COUNTRY_STATE_LIST MNTS
		INNER JOIN MNT_LOB_STATE MNTL ON MNTS.STATE_ID= MNTL.STATE_ID
		WHERE  MNTL.PARENT_LOB is null AND MNTL.LOB_ID= @LobId
		AND MNTS.COUNTRY_ID = @CountryID
		ORDER BY STATE_NAME
	else
		SELECT Distinct MNTS.STATE_ID,MNTS.STATE_NAME
		FROM MNT_COUNTRY_STATE_LIST MNTS
		--INNER JOIN MNT_LOB_STATE MNTL ON MNTS.STATE_ID= MNTL.STATE_ID
		WHERE -- MNTL.PARENT_LOB is null AND MNTL.LOB_ID= @LobId ----AND 
		MNTS.COUNTRY_ID = @CountryID
		ORDER BY STATE_NAME
		
END


GO

