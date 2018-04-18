IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillTimeZoneDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillTimeZoneDropDown]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillTimeZoneDropDown
Created by      : Gaurav
Date            : 4/15/2005
Purpose         : To select  record in MNT_TIME_ZONE_LIST
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_FillTimeZoneDropDown
@Lang_id int=1
AS  
BEGIN  
SELECT TZ.TIME_CODE,isnull(MTZ.TIME_DESC,TZ.TIME_DESC)TIME_DESC FROM  MNT_TIME_ZONE_LIST TZ
left outer join  MNT_TIME_ZONE_LIST_MULTILINGUAL MTZ 
ON MTZ.TIME_ID=TZ.TIME_ID and MTZ.LANG_ID=@lang_id ORDER BY TIME_DESC 
END


GO

