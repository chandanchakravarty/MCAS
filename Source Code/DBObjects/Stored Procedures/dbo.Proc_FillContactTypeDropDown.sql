IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillContactTypeDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillContactTypeDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

   /*----------------------------------------------------------  
Proc Name       : dbo.Proc_FillContactTypeDropDown  
Created by      :  Ajit Singh Chahal  
Date                :  04/15/2005  
Purpose         :  To fill drop down of contact types  
Revison History :  
Used In         :    Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP PROC Proc_FillContactTypeDropDown
CREATE    PROCEDURE [dbo].[Proc_FillContactTypeDropDown] 
@LANG_ID INT=1
AS  
BEGIN
  
SELECT MNT.CONTACT_TYPE_ID,ISNULL(MNT_M.CONTACT_TYPE_DESC, MNT.CONTACT_TYPE_DESC) AS CONTACT_TYPE_DESC
FROM   MNT_CONTACT_TYPES MNT LEFT OUTER JOIN
	   MNT_CONTACT_TYPES_MULTILINGUAL MNT_M ON MNT.CONTACT_TYPE_ID=MNT_M.CONTACT_TYPE_ID AND MNT_M.LANG_ID=@LANG_ID
WHERE  IS_ACTIVE = 'Y'
ORDER BY CONTACT_TYPE_DESC  
  
END
  
  
 
  
  
  
  
GO

