IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_DiaryType_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_DiaryType_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*  
Created By : Anurag Verma  
Created On : 02/03/2007  
Purpose  : Fetch diary type according to module id  
  
Modified By : Anurag verma  
Modified on : 20/03/2007  
Purpose		: Fetch diary type according to module id and type flag  

Modified By :Pradeep Kushwaha
Modified on : 22/07/2010  
Purpose		: for the multilingual

DROP PROC Proc_Fetch_DiaryType_Details 1,'F' , 1  
*/  
  
CREATE PROC [dbo].[Proc_Fetch_DiaryType_Details]  
(  
 @moduleID int,  
 @type_flag nchar(1),  
 @LANG_ID smallint=1   
)  
AS  
BEGIN  
SELECT ISNULL(TDM.TYPEID,TD.TYPEID) AS TYPEID ,ISNULL(TDM.TYPEDESC,TD.TYPEDESC) AS   TYPEDESC
FROM MNT_MODULE_DIARYTYPE_ASSOCIATION MDA   
INNER JOIN TODOLISTTYPES TD ON MDA.MDA_DIARYTYPE_ID=TD.TYPEID 
LEFT OUTER join TODOLISTTYPES_MULTILINGUAL TDM ON TD.TYPEID=TDM.TYPEID AND TDM.LANG_ID=@LANG_ID
WHERE MDA.MDA_MODULE_ID=@moduleID and td.type_flag=@type_flag  
order by ISNULL(TDM.TYPEDESC,TD.TYPEDESC)  
END  
  
  
  
GO

