IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillUserDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillUserDropDown]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateSystemParams
Created by      : Gaurav
Date            : 4/15/2005
Purpose         : To select  record in MNT_USER_LIST
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_FillUserDropDown

AS

BEGIN

SELECT	 USER_ID,	USER_FNAME+ ' ' + USER_LNAME AS [Manager Name] 	FROM MNT_USER_LIST ORDER BY USER_FNAME+ ' ' + USER_LNAME ASC
SELECT 	USER_TYPE_ID , USER_TYPE_DESC 	FROM 	MNT_USER_TYPES ORDER BY USER_TYPE_DESC ASC
SELECT 	DIV_ID , DIV_NAME 	FROM 	MNT_DIV_LIST ORDER BY DIV_NAME ASC
SELECT 	 DIV_NAME+'-'+DEPT_NAME+'-'+ PC_NAME AS [ DIV_NAME DEPT_NAME PC_NAME], Convert(varchar(5),MNT_DIV_DEPT_MAPPING.DIV_ID)+'_'+Convert(varchar(5),MNT_DIV_DEPT_MAPPING.DEPT_ID)+'_'+Convert(varchar(5),MNT_DEPT_PC_MAPPING.PC_ID) AS MYID from MNT_DIV_LIST left join MNT_DIV_DEPT_MAPPING ON MNT_DIV_LIST.DIV_ID	= MNT_DIV_DEPT_MAPPING.DIV_ID left join MNT_DEPT_LIST ON MNT_DIV_DEPT_MAPPING.DEPT_ID=MNT_DEPT_LIST.DEPT_ID left join MNT_DEPT_PC_MAPPING ON MNT_DEPT_LIST.DEPT_ID	= MNT_DEPT_PC_MAPPING.DEPT_ID left join MNT_PROFIT_CENTER_LIST ON MNT_DEPT_PC_MAPPING.PC_ID=MNT_PROFIT_CENTER_LIST.PC_ID where DIV_NAME+'-'+DEPT_NAME+'-'+ PC_NAME is not null ORDER BY   DIV_NAME+'-'+DEPT_NAME+'-'+ PC_NAME ASC
SELECT TIME_CODE,TIME_DESC FROM  MNT_TIME_ZONE_LIST ORDER BY TIME_DESC

END


GO

