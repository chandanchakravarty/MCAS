IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_getDefaultMenuDataEx_OLD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_getDefaultMenuDataEx_OLD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc proc_getDefaultMenuDataEx_OLD
(
	@AGENCY_CODE	VARCHAR(20) 
)
as
DECLARE @SQL VARCHAR(8000)

SET @SQL =
'SELECT 	a.menu_id as button_id, a.menu_name as button_name, a.menu_link as button_link, a.menu_image as button_image,isnull(a.hidestatus,0) as button_hidestatus,a.default_page,
	b.menu_id as topmenu_id, b.menu_name as topmenu_name, b.menu_link as  topmenu_link, isnull(b.hidestatus,0) as topmenu_hidestatus,
	c.menu_id as menu_id, c.menu_name as menu_name, c.menu_link as menu_link, isnull(c.hidestatus,0) as menu_hidestatus,
	d.menu_id as submenu_id, d.menu_name as submenu_name, d.menu_link as submenu_link, isnull(d.hidestatus,0) as submenu_hidestatus,
	e.menu_id as subsubmenu_id, e.menu_name as subsubmenu_name, e.menu_link as subsubmenu_link, isnull(e.hidestatus,0) as subsubmenu_hidestatus 
FROM MNT_MENU_LIST a 
LEFT OUTER JOIN MNT_MENU_LIST b on a.menu_id = b.parent_id 
LEFT OUTER JOIN MNT_MENU_LIST c on b.menu_id = c.parent_id AND b.menu_id <> 116
LEFT OUTER JOIN MNT_MENU_LIST d on c.menu_id = d.parent_id 
LEFT OUTER JOIN MNT_MENU_LIST e on d.menu_id = e.parent_id 
WHERE a.parent_id is null '

IF @AGENCY_CODE <> 'W001' --For agency other then wolverine showing only selected menus
BEGIN
	SET @SQL = @SQL + ' AND a.AGENCY_LEVEL = 1 and (b.agency_level = 1 or b.menu_id is null)'
END

SET @SQL = @SQL + 'ORDER BY a.nestlevel,a.menu_order,b.menu_order,c.menu_order,d.menu_order,e.menu_order'
PRINT @SQL
EXECUTE(@SQL )








GO

