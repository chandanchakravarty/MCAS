

/****** Object:  StoredProcedure [dbo].[Proc_GetMenuListByTabId]    Script Date: 12/11/2014 16:40:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMenuListByTabId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMenuListByTabId]
GO



/****** Object:  StoredProcedure [dbo].[Proc_GetMenuListByTabId]    Script Date: 12/11/2014 16:40:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [dbo].[Proc_GetMenuListByTabId]            
as            
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

GO



