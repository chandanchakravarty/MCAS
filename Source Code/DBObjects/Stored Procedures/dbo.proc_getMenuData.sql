IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_getMenuData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_getMenuData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 --Modified By : Charles Gomes
 --Modified On : 12-Apr-2010    
 --purpose	 : Added Multilingual Support MNT_MENU_LIST_MULTILINGUAL

-- drop proc dbo.proc_getMenuData
 
CREATE  proc [dbo].[proc_getMenuData] 
 @LANG_ID INT  = 1    --Added by Charles on 12-Apr-2010 for Multilingual Support
as  
SELECT  a.menu_id as button_id, ISNULL(al.menu_name, a.menu_name) as button_name, a.menu_link as button_link, a.menu_image as button_image,isnull(a.hidestatus,0) as button_hidestatus,a.default_page,  
 b.menu_id as topmenu_id, ISNULL(bl.menu_name, b.menu_name) as topmenu_name, b.menu_link as  topmenu_link, isnull(b.hidestatus,0) as topmenu_hidestatus,  
 c.menu_id as menu_id,ISNULL(cl.MENU_NAME,c.menu_name) as menu_name, c.menu_link as menu_link, isnull(c.hidestatus,0) as menu_hidestatus,  
 d.menu_id as submenu_id,ISNULL(dl.MENU_NAME, d.menu_name) as submenu_name, d.menu_link as submenu_link, isnull(d.hidestatus,0) as submenu_hidestatus,  
 e.menu_id as subsubmenu_id,ISNULL(el.MENU_NAME, e.menu_name) as subsubmenu_name, e.menu_link as subsubmenu_link, isnull(e.hidestatus,0) as subsubmenu_hidestatus   
FROM MNT_MENU_LIST a WITH(NOLOCK)  
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL al WITH(NOLOCK) on al.menu_id = a.menu_id and al.LANG_ID = @LANG_ID  
LEFT OUTER JOIN MNT_MENU_LIST b WITH(NOLOCK) on a.menu_id = b.parent_id  and isnull(b.is_active,'Y')='Y'   
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL bl WITH(NOLOCK) on bl.menu_id = b.menu_id and bl.LANG_ID = @LANG_ID  
LEFT OUTER JOIN MNT_MENU_LIST c WITH(NOLOCK) on b.menu_id = c.parent_id and isnull(c.is_active,'Y')='Y'  
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL cl WITH(NOLOCK) on cl.menu_id = c.menu_id and cl.LANG_ID = @LANG_ID    
LEFT OUTER JOIN MNT_MENU_LIST d WITH(NOLOCK) on c.menu_id = d.parent_id   and isnull(d.is_active,'Y')='Y'  
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL dl WITH(NOLOCK) on dl.menu_id = d.menu_id and dl.LANG_ID = @LANG_ID  
LEFT OUTER JOIN MNT_MENU_LIST e WITH(NOLOCK) on d.menu_id = e.parent_id   and isnull(e.is_active,'Y')='Y'  
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL el WITH(NOLOCK) on el.menu_id = e.menu_id and el.LANG_ID = @LANG_ID  
WHERE a.parent_id is null and isnull(a.is_active,'Y')='Y'  
ORDER BY a.nestlevel,a.menu_order,b.menu_order,c.menu_order,d.menu_order,e.menu_order  
  


GO

