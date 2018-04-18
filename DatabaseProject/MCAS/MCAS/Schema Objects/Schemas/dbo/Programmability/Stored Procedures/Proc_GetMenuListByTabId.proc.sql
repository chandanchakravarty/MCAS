CREATE PROCEDURE [dbo].[Proc_GetMenuListByTabId]
WITH EXECUTE AS CALLER
AS
BEGIN            
SET FMTONLY OFF;            
SELECT            
  [TId],            
  [MenuId],            
  [TabId],            
  [DisplayTitle],            
  [IsMenuItem],            
  [IsJsMenuItem],            
  [VirtualSource],            
  [Hyp_Link_Address],            
  [ProductName],            
  [UserType],            
  [Displayimg],            
  [DisplayOrder],            
  [IsHeader],            
  [SubMenu],            
  [MainHeaderId],            
  [IsAdmin],            
  [AdminDisplayText],            
  [MenuItemWidth],            
  [MenuItemHeight],            
  [ErrorMessDesc],            
  [ErrorMessTitle],            
  [ErrorMessHead],            
  [SubTabId],            
  [LangId],            
  [IsActive]            
FROM MNT_Menus where [IsActive] !='N'  
ORDER BY DisplayOrder ASC        
  
END


