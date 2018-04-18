IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_getDefaultMenuDataEx]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_getDefaultMenuDataEx]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--begin tran      
--drop proc proc_getDefaultMenuDataEx      
--go      
/*----------------------------------------------------------                      
 Proc Name       : Dbo.proc_getDefaultMenuDataEx      

 Modified By  : Ravindra       
 Modified On  : 2-14-2007      
 Purpose  : Agency Interface Implementation  
     
 Column AGENCY_LEVEL  will be used now to set the accesibility level of Menu      
 Menu Item With AGENCY_LEVEL "0" or NULL will be visible for Carier Users only      
 Menu Item With AGENCY_LEVEL "1" will be visible for ALL Users only      
 Menu Item With AGENCY_LEVEL "2"  will be visible for Agency Users only      
      
 Modified By : Shailja Rampal      
 Modified On : 03-06-2007      
 purpose : #1195. For adding Variable CalledFor in case of Communication/Letter Menu.      
   
 Modified By : Charles Gomes  
 Modified On : 12-Apr-2010      
 purpose  : Added Multilingual Support  
      
--exec proc_getDefaultMenuDataEx 'loc'      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/         
      
-- drop proc dbo.proc_getDefaultMenuDataEx br01,2      
CREATE proc [dbo].[proc_getDefaultMenuDataEx]      
(      
 @AGENCY_CODE NVARCHAR(20),  
 @LANG_ID INT  = 1    --Added by Charles on 12-Apr-2010 for Multilingual Support  
)      
as      
-- Constant Declaration      
DECLARE @AGENCY_ONLY Smallint,      
 @CARIER_ONLY SmallInt,      
 @BOTH      SmallInt ,
 @CARRIER_CODE NVARCHAR(10) ,
 @SQL VARCHAR(8000)         
--- NULL Will also be considered as both       
SET @CARIER_ONLY  = 0       
SET @BOTH    = 1       
SET @AGENCY_ONLY  = 2      
     
--SELECT  @CARRIER_CODE=ISNULL(AGENCY_CODE,'') FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE ISNULL(IS_CARRIER,'')='Y'  
--Added by Charles on 19-May-2010 for Itrack 51
SELECT  @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'') FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK) 
INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID = SYSP.SYS_CARRIER_ID    
      
SET @SQL =      
'SELECT  a.menu_id as button_id, ISNULL(al.menu_name,a.menu_name) as button_name, a.menu_link as button_link, a.menu_image as button_image,isnull(a.hidestatus,0) as button_hidestatus,a.default_page,      
 b.menu_id as topmenu_id, isnull(bl.menu_name,b.menu_name) as topmenu_name, b.menu_link as  topmenu_link, isnull(b.hidestatus,0) as topmenu_hidestatus,      
 c.menu_id as menu_id, isnull(cl.menu_name ,c.menu_name) as menu_name, CASE c.menu_id WHEN 111 THEN c.menu_link + ''?CalledFor=CUSTOMER'' WHEN 110 
 THEN c.menu_link + ''?CalledFor=CUSTOMER'' ELSE c.menu_link END as menu_link, isnull(c.hidestatus,0) as menu_hidestatus,      
 d.menu_id as submenu_id, isnull(dl.menu_name,d.menu_name) as submenu_name, d.menu_link as submenu_link, isnull(d.hidestatus,0) as submenu_hidestatus,      
 e.menu_id as subsubmenu_id, isnull(el.menu_name,e.menu_name) as subsubmenu_name, e.menu_link as subsubmenu_link, isnull(e.hidestatus,0) as subsubmenu_hidestatus       
FROM MNT_MENU_LIST a WITH(NOLOCK)       
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL al WITH(NOLOCK) on al.menu_id = a.menu_id and al.LANG_ID = ' +CAST(@LANG_ID AS VARCHAR)+  
' LEFT OUTER JOIN MNT_MENU_LIST b WITH(NOLOCK) on a.menu_id = b.parent_id  and ISNULL(b.IS_ACTIVE,''Y'')=''Y''    
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL bl WITH(NOLOCK) on bl.menu_id = b.menu_id and bl.LANG_ID = ' +CAST(@LANG_ID AS VARCHAR)+  
' LEFT OUTER JOIN MNT_MENU_LIST c WITH(NOLOCK) on b.menu_id = c.parent_id AND b.menu_id <> 116  and ISNULL(c.IS_ACTIVE,''Y'')=''Y'' 
LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL cl WITH(NOLOCK) on cl.menu_id = c.menu_id and cl.LANG_ID = ' +CAST(@LANG_ID AS VARCHAR)     
      
IF (@AGENCY_CODE) <> @CARRIER_CODE --'W001' --For agency other then wolverine showing only selected menus      
BEGIN      
 SET @SQL = @SQL + 'AND ISNULL(C.AGENCY_LEVEL,0) IN( '       
   + CONVERT(VARCHAR,@AGENCY_ONLY) + ',' + CONVERT(VARCHAR,@BOTH) + ') '      
END      
ELSE      
BEGIN       
 SET @SQL = @SQL + 'AND ISNULL(C.AGENCY_LEVEL,0) IN( '       
   + CONVERT(VARCHAR,@CARIER_ONLY) + ',' + CONVERT(VARCHAR,@BOTH) + ') '      
END      
      
SET @SQL = @SQL + ' LEFT OUTER JOIN MNT_MENU_LIST d WITH(NOLOCK) on c.menu_id = d.parent_id  and ISNULL(d.IS_ACTIVE,''Y'')=''Y'' '
SET @SQL = @SQL + ' LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL dl WITH(NOLOCK) on dl.menu_id = d.menu_id and dl.LANG_ID = '+CAST(@LANG_ID AS VARCHAR)         
      
--Added For Itrack Issue #6915      
IF (@AGENCY_CODE) <> @CARRIER_CODE --'W001' --For agency other then wolverine showing only selected menus      
BEGIN      
 SET @SQL = @SQL + 'AND ISNULL(d.AGENCY_LEVEL,0) not IN( '       
   + CONVERT(VARCHAR,@CARIER_ONLY) +  ') '      
END      
--end here--      
SET @SQL = @SQL + ' LEFT OUTER JOIN MNT_MENU_LIST e WITH(NOLOCK) on d.menu_id = e.parent_id and ISNULL(e.IS_ACTIVE,''Y'')=''Y'' '
SET @SQL = @SQL + ' LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL el WITH(NOLOCK) on el.menu_id = e.menu_id and el.LANG_ID = '+ CAST(@LANG_ID AS VARCHAR) 
SET @SQL = @SQL + ' WHERE a.parent_id is null and ISNULL(A.IS_ACTIVE,''Y'')=''Y'''   
   
      
      
IF (@AGENCY_CODE) <> @CARRIER_CODE --'W001' --For agency other then wolverine showing only selected menus      
BEGIN      
 SET @SQL = @SQL + ' AND ISNULL(a.AGENCY_LEVEL,0) IN( '       
   + CONVERT(VARCHAR,@AGENCY_ONLY) + ',' + CONVERT(VARCHAR,@BOTH)      
   + ' ) and ( ISNULL(b.agency_level,0) IN(  '       
   +  CONVERT(VARCHAR,@AGENCY_ONLY) + ',' + CONVERT(VARCHAR,@BOTH)      
   + ' )or b.menu_id is null)'      
END      
ELSE      
BEGIN       
 SET @SQL = @SQL + ' AND ISNULL(a.AGENCY_LEVEL,0) IN( '       
   + CONVERT(VARCHAR,@CARIER_ONLY) + ',' + CONVERT(VARCHAR,@BOTH)      
   + ' ) and ( ISNULL(b.agency_level,0) IN(  '       
   +  CONVERT(VARCHAR,@CARIER_ONLY) + ',' + CONVERT(VARCHAR,@BOTH)      
   + ' )or b.menu_id is null)'      
END      

      
SET @SQL = @SQL + ' ORDER BY a.nestlevel,a.menu_order,b.menu_order,c.menu_order,d.menu_order,e.menu_order'      
--PRINT @SQL      
EXECUTE(@SQL )      
--go      
--exec proc_getDefaultMenuDataEx 'w001'      
--rollback tran      


GO

