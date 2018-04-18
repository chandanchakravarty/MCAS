IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBWiseScreenList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBWiseScreenList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name   : dbo.Proc_GetLOBWiseScreenList
Created by  : Praveen Jain
Date        : 27-July-2010
Purpose     : To display the LOB wise screen List from WorkFlow screens
Revison History  :
------------------------------------------------------------
Date     Review By          Comments

-------------------------------------------*/
-- Proc_GetLOBWiseScreenList 'HOME', null
-- Drop PROCEDURE dbo.Proc_GetLOBWiseScreenList
CREATE PROCEDURE [dbo].[Proc_GetLOBWiseScreenList]
(
 @LOBCode varchar(4),
 @CountryID Int = 5 
)
AS
BEGIN
	Declare @strLOB as VARCHAR(20), 
			@LOB_DESC as Varchar(140),
			@GROUP_ID as VARCHAR(40),
			@WorkFlowScreens as VARCHAR(1000),
			@WorkFlowScreensTemp as VARCHAR(1000),
			@strSplit as Varchar(40),
			@lstrScrPos1 as INT
			
	Declare @tblScreens table(rowid int Identity(1, 1), ScreenID Varchar(40))
			
	--Select @LOBCode = 'HOME' , @CountryID = 5
	-- SELECT * FROM MNT_LOB_MASTER ORDER BY LOB_CODE -- MNT_LOB_DESC
	Select @strLOB = LOB_CODE, @LOB_DESC = LOB_DESC From MNT_LOB_MASTER Where LOB_CODE Like  @LOBCode + '%'
	
	Select @GROUP_ID = GROUP_ID, @WorkFlowScreens = WORKFLOW_SCREENS 
	From MNT_WORKFLOW_GROUP 
	Where GROUP_ID Like '%'+ @strLOB +'%' 
	AND GROUP_ID LIKE 'POL%'
	Order By GROUP_ID
	
	Select @WorkFlowScreensTemp=@WorkFlowScreens
		IF(@WorkFlowScreens <> '')
		Begin
			Select @lstrScrPos1=CHARINDEX(',',@WorkFlowScreensTemp)
			If @lstrScrPos1 > 0 
				Begin
					While(@lstrScrPos1<>'')
						Begin
							Select  @lstrScrPos1=CHARINDEX(',',@WorkFlowScreensTemp)
							If @lstrScrPos1>0
								Begin
									INSERT INTO @tblScreens SELECT Left(@WorkFlowScreensTemp, @lstrScrPos1 - 1)
								End	
							else
								Set @lstrScrPos1 = ''
								
							SELECT @WorkFlowScreensTemp = SUBSTRING(@WorkFlowScreensTemp, @lstrScrPos1 + 1, LEN(@WorkFlowScreensTemp))
							-- PRINT @WorkFlowScreensTemp
						End
				End
			Else
				Begin
					INSERT INTO @tblScreens SELECT @WorkFlowScreensTemp
				End
		END
		-- Final Output:
		Select @strLOB as LOB_CODE, @LOB_DESC as LOB_DESC, MNT_SCREEN_LIST.* 
		From MNT_SCREEN_LIST 
		Where SCREEN_ID In (SELECT ScreenID FROM @tblScreens)
		Order by MNT_SCREEN_LIST.SCREEN_ID
		
		Select @strLOB as LOB_CODE, @LOB_DESC as LOB_DESC, MNT_SCREEN_LIST.SCREEN_ID, MNT_SCREEN_LIST.SCREEN_DESC
		, MWL.TABLE_NAME, MWL.PRIMARY_KEYS, MWL.WORKFLOW_ORDER
		From MNT_SCREEN_LIST 
		LEFT OUTER JOIN MNT_WORKFLOW_LIST MWL ON
		MNT_SCREEN_LIST.SCREEN_ID = MWL.SCREEN_ID
		Where MNT_SCREEN_LIST.SCREEN_ID In (SELECT ScreenID FROM @tblScreens)
		Order by MNT_SCREEN_LIST.SCREEN_ID		
		--Select * From MNT_SCREEN_LIST Where SCREEN_ID In ('447_0','447_1', '449_0','449_1')
END
GO

