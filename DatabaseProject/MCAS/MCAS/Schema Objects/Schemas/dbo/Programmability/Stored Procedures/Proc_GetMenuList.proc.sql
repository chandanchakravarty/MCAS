CREATE PROCEDURE [dbo].[Proc_GetMenuList]
	@LangId [int],
	@GroupId [int]
WITH EXECUTE AS CALLER
AS
BEGIN            
SET FMTONLY OFF;            
              
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL            
    /*Then it exists*/            
DROP TABLE #tmp_GridResults_1            
SELECT * INTO #tmp_GridResults_1            
FROM (            
 select               
 A.TId,A.MenuId,A.TabId,isnull(replace(B.DisplayTitle, '\n',  CHAR(13)+CHAR(10)),replace(A.DisplayTitle, '\n',
 CHAR(13)+CHAR(10))) as DisplayTitle,IsMenuItem,A.IsJsMenuItem,A.VirtualSource,A.Hyp_Link_Address,A.ProductName,
 A.UserType,A.Displayimg,A.DisplayOrder,A.IsHeader,A.GroupType,A.SubMenu,A.MainHeaderId,A.IsAdmin,              
 isnull(B.AdminDisplayText,A.AdminDisplayText) as AdminDisplayText,A.MenuItemWidth,A.MenuItemHeight,              
 isnull(B.ErrorMessDesc,A.ErrorMessDesc) as ErrorMessDesc,isnull(B.ErrorMessTitle,A.ErrorMessTitle) as ErrorMessTitle,              
 isnull(B.ErrorMessHead,A.ErrorMessHead) as ErrorMessHead,A.SubTabId,              
 B.LangId as LangId,A.IsActive as IsActive            
 from [MNT_Menus] A              
 left join [MNT_Menus_Multilingual] B on A.MenuId=B.MenuId and B.langid = @LangId              
  )t;            
              
SELECT            
  *            
FROM #tmp_GridResults_1            
WHERE menuid IN (SELECT            
  MenuId            
FROM MNT_GroupPermission            
WHERE GroupId = @GroupId)            
AND [IsActive] != 'N' 
ORDER BY DisplayOrder ASC          
 
END


