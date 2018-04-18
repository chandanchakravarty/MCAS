--- proc_getLobMenuDataEx 'MOT','1','W001',1    
    
      
/*----------------------------------------------------------                                
 Proc Name       : Dbo.proc_getLobMenuDataEx                
      
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
 purpose  : Added Multilingual Support MNT_MENU_LIST_MULTILINGUAL        
           
select * from mnt_lob_master            
exec proc_getLobMenuDataEx  'ARPERIL',1,'w001'            
                          
------   ------------       -------------------------*/                   
-- drop proc dbo.proc_getLobMenuDataEx                  
ALTER proc [dbo].[proc_getLobMenuDataEx]                  
(                  
 @LOB_STRING VARCHAR(25),                  
 @POLICY_LEVEL VARCHAR(1),                  
 @AGENCY_CODE VARCHAR(20),        
 @LANG_ID INT = 1  --Added by Charles on 12-Apr-10 for Multilingual Support                 
)                  
AS                  
                
-- Constant Declaration                
DECLARE @AGENCY_ONLY Smallint,                
 @CARIER_ONLY SmallInt,                
 @BOTH      SmallInt,      
 @CARRIER_CODE NVARCHAR(10),      
  @SQL VARCHAR(8000)                
--- NULL Will also be considered as both                 
SET @CARIER_ONLY  = 0                 
SET @BOTH    = 1                 
SET @AGENCY_ONLY  = 2                
       
--SELECT  @CARRIER_CODE=ISNULL(AGENCY_CODE,'') FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE ISNULL(IS_CARRIER,'')='Y'       
--Added by Charles on 19-May-10 for Itrack 51      
SELECT  @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'') FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)       
INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID = SYSP.SYS_CARRIER_ID           
                
if (@POLICY_LEVEL = '0')                  
BEGIN                  
 /*Showing the application level menu*/                  
 SET @SQL =                  
 'SELECT  a.menu_id as button_id, isnull(al.menu_name, a.menu_name) as button_name, a.menu_link as button_link, a.menu_image as button_image,isnull(a.hidestatus,0) as button_hidestatus,a.default_page,                  
 b.menu_id as topmenu_id, isnull(bl.menu_name,b.menu_name) as topmenu_name, b.menu_link as  topmenu_link, isnull(b.hidestatus,0) as topmenu_hidestatus,                  
 c.menu_id as menu_id, isnull(cl.menu_name, c.menu_name) as menu_name, CASE c.menu_id WHEN 111 THEN c.menu_link + ''?CalledFor=APPLICATION'' ELSE c.menu_link END as menu_link, isnull(c.hidestatus,0) as menu_hidestatus,                  
 d.menu_id as submenu_id, isnull(dl.menu_name, d.menu_name) as submenu_name, d.menu_link as submenu_link, isnull(d.hidestatus,0) as submenu_hidestatus,                  
 e.menu_id as subsubmenu_id, isnull(el.menu_name, e.menu_name) as subsubmenu_name, e.menu_link as subsubmenu_link, isnull(e.hidestatus,0) as subsubmenu_hidestatus                   
 FROM MNT_MENU_LIST a WITH(NOLOCK)          
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL al WITH(NOLOCK) on al.MENU_ID = a.MENU_ID and al.LANG_ID = ' + cast(@LANG_ID as varchar) + '                 
 LEFT OUTER JOIN MNT_MENU_LIST b WITH(NOLOCK) on a.menu_id = b.parent_id  and isnull(b.IS_ACTIVE,''Y'')=''Y''                 
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL bl WITH(NOLOCK) on bl.MENU_ID = b.MENU_ID and bl.LANG_ID = ' + cast(@LANG_ID as varchar) + '                 
 LEFT OUTER JOIN MNT_MENU_LIST c WITH(NOLOCK) on b.menu_id = c.parent_id  and isnull(c.IS_ACTIVE,''Y'')=''Y'' AND '                 
                 
 IF (@AGENCY_CODE) <> @CARRIER_CODE --'W001' --For agency other then wolverine showing only selected menus                
 BEGIN                
  SET @SQL = @SQL + ' ISNULL(C.AGENCY_LEVEL,0) IN( '                 
   + CONVERT(VARCHAR,@AGENCY_ONLY) + ',' + CONVERT(VARCHAR,@BOTH) + ') AND '                
 END                
 ELSE                
 BEGIN                 
  SET @SQL = @SQL + ' ISNULL(C.AGENCY_LEVEL,0) IN( '                 
    + CONVERT(VARCHAR,@CARIER_ONLY) + ',' + CONVERT(VARCHAR,@BOTH) + ') AND '                
 END                
                 
 SET @SQL = @SQL + '( case b.menu_name                   
 when ''Risk Information'' THEN c.menu_name                   
 ELSE                   
 CASE @LOB_STRING                  
 WHEN ''UMB'' THEN ''Personal Umbrella Liability''                  
 WHEN ''WAT'' THEN ''Watercraft''                  
 WHEN ''PPA'' THEN ''Automobile''                  
 WHEN ''MOT'' THEN ''Motorcycle''                  
 WHEN ''HOME'' THEN ''Fire''                  
 WHEN ''RENT'' THEN ''Rental Dwelling''                  
 WHEN ''GEN'' THEN ''General Liability''                  
 WHEN ''AVIATION'' THEN ''Aviation''               
 else isnull(c.lob_code,'''')            
 END                  
 END                  
 = CASE @LOB_STRING                  
 WHEN ''UMB'' THEN ''Personal Umbrella Liability''                  
 WHEN ''WAT'' THEN ''Watercraft''                  
 WHEN ''PPA'' THEN ''Automobile''                  
 WHEN ''MOT'' THEN ''Motorcycle''                  
 WHEN ''HOME'' THEN ''Fire''                  
 WHEN ''RENT'' THEN ''Rental Dwelling''                  
 WHEN ''GEN'' THEN ''General Liability''                  
 WHEN ''AVIATION'' THEN ''Aviation''               
  else case when isnull(c.lob_code,'''') ='''' then '''' else ltrim(rtrim(@LOB_STRING)) end            
                
 END            
 OR            
  case b.menu_name            
  when ''Risk Information'' THEN  isnull(c.lob_code,'''') END                  
  =ltrim(rtrim(@LOB_STRING))                  
 )            
 AND ISNULL(c.module_name,'''') = ''''           
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL cl WITH(NOLOCK) on cl.MENU_ID = c.MENU_ID and cl.LANG_ID = ' + cast(@LANG_ID as varchar) + '                        
 LEFT OUTER JOIN MNT_MENU_LIST d WITH(NOLOCK) on c.menu_id = d.parent_id    and isnull(d.IS_ACTIVE,''Y'')=''Y''         
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL dl WITH(NOLOCK) on dl.MENU_ID = d.MENU_ID and dl.LANG_ID = ' + cast(@LANG_ID as varchar) + '                           
 LEFT OUTER JOIN MNT_MENU_LIST e WITH(NOLOCK) on d.menu_id = e.parent_id    and isnull(e.IS_ACTIVE,''Y'')=''Y''                
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL el WITH(NOLOCK) on el.MENU_ID = e.MENU_ID and el.LANG_ID = ' + cast(@LANG_ID as varchar) + '                 
 WHERE a.parent_id is null  and isnull(a.IS_ACTIVE,''Y'')=''Y''   '                  
                 
--  IF @AGENCY_CODE <> 'W001' --For agency other then wolverine showing only selected menus                  
--  BEGIN                  
--   SET @SQL = @SQL + ' AND a.AGENCY_LEVEL = 1 and (b.agency_level = 1 or b.menu_id is null)'                  
--  END                  
--                  
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
END                  
ELSE                  
BEGIN                  
 /*Showing the policy level menu*/                  
 SET @SQL =                  
 'SELECT  a.menu_id as button_id, isnull(al.menu_name, a.menu_name) as button_name, a.menu_link as button_link,                 
 a.menu_image as button_image,isnull(a.hidestatus,0) as button_hidestatus,a.default_page,                  
 b.menu_id as topmenu_id, isnull(bl.menu_name,b.menu_name) as topmenu_name, b.menu_link as  topmenu_link, isnull(b.hidestatus,0)                 
 as topmenu_hidestatus, c.menu_id as menu_id, isnull(cl.menu_name, c.menu_name) as menu_name, CASE c.menu_id WHEN 111 THEN c.menu_link + ''?CalledFor=POLICY'' WHEN 110 THEN c.menu_link + ''?CalledFor=POLICY'' ELSE c.menu_link END as menu_link,           
   
    
 isnull(c.hidestatus,0) as menu_hidestatus,   d.menu_id as submenu_id, isnull(dl.menu_name, d.menu_name) as submenu_name,                 
 d.menu_link as submenu_link, isnull(d.hidestatus,0) as submenu_hidestatus,   e.menu_id as subsubmenu_id,                 
 isnull(el.menu_name, e.menu_name) as subsubmenu_name, e.menu_link as subsubmenu_link, isnull(e.hidestatus,0) as subsubmenu_hidestatus                   
 FROM MNT_MENU_LIST a WITH(NOLOCK)          
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL al WITH(NOLOCK) on al.MENU_ID = a.MENU_ID and al.LANG_ID = ' + cast(@LANG_ID as varchar) + '                 
 LEFT OUTER JOIN MNT_MENU_LIST b WITH(NOLOCK) on a.menu_id = b.parent_id     and isnull(b.IS_ACTIVE,''Y'')=''Y''             
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL bl WITH(NOLOCK) on bl.MENU_ID = b.MENU_ID and bl.LANG_ID = ' + cast(@LANG_ID as varchar) + '              
 LEFT OUTER JOIN MNT_MENU_LIST c WITH(NOLOCK) on b.menu_id = c.parent_id  and isnull(c.IS_ACTIVE,''Y'')=''Y''   AND '                
                 
 IF (@AGENCY_CODE) <> @CARRIER_CODE --'W001' --For agency other then wolverine showing only selected menus                
 BEGIN                
  SET @SQL = @SQL + ' ISNULL(C.AGENCY_LEVEL,0) IN( '                 
   + CONVERT(VARCHAR,@AGENCY_ONLY) + ',' + CONVERT(VARCHAR,@BOTH) + ') AND '                
 END                
 ELSE                
 BEGIN                 
  SET @SQL = @SQL + ' ISNULL(C.AGENCY_LEVEL,0) IN( '                 
    + CONVERT(VARCHAR,@CARIER_ONLY) + ',' + CONVERT(VARCHAR,@BOTH) + ') AND '                
 END                
                 
 SET @SQL = @SQL + ' (case b.menu_name                   
 when ''Risk Information'' THEN  c.menu_name              
 ELSE                   
 CASE ltrim(rtrim(@LOB_STRING))                  
 WHEN ''UMB'' THEN ''Personal Umbrella Liability''                  
 WHEN ''WAT'' THEN ''Watercraft''                  
 WHEN ''PPA'' THEN ''Automobile''                  
 WHEN ''MOT'' THEN ''Motorcycle''                  
 WHEN ''HOME'' THEN ''Fire''                  
 WHEN ''RENT'' THEN ''Rental Dwelling''                  
 WHEN ''GEN'' THEN ''General Liability''                  
 WHEN ''Automobile'' THEN ''Automobile''                 
 WHEN ''AVIATION'' THEN ''Aviation''              
   else isnull(c.lob_code,'''')               
 END                  
 END                  
 = CASE ltrim(rtrim(@LOB_STRING))                  
 WHEN ''UMB'' THEN ''Personal Umbrella Liability''                  
 WHEN ''WAT'' THEN ''Watercraft''                  
 WHEN ''PPA'' THEN ''Automobile''                  
 WHEN ''MOT'' THEN ''Motorcycle''                  
 WHEN ''HOME'' THEN ''Fire''                  
 WHEN ''RENT'' THEN ''Rental Dwelling''                  
 WHEN ''GEN'' THEN ''General Liability''                  
 WHEN ''Automobile'' THEN ''Automobile''                  
 WHEN ''AVIATION'' THEN ''Aviation''               
  else case when isnull(c.lob_code,'''') ='''' then '''' else ltrim(rtrim(@LOB_STRING)) end            
 END                   
 OR            
  case b.menu_name            
  when ''Risk Information'' THEN  isnull(c.lob_code,'''') END                  
  =ltrim(rtrim(@LOB_STRING))              
 )            
             
 AND                   
 CASE b.menu_name                  
 when ''Risk Information'' THEN ISNULL(c.module_name,'''')                 
 when ''Forms'' THEN ISNULL(c.module_name,'''')                   
 ELSE ''POL''                  
 END = ''POL''                  
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL cl WITH(NOLOCK) on cl.MENU_ID = c.MENU_ID and cl.LANG_ID = ' + cast(@LANG_ID as varchar) + '            
 LEFT OUTER JOIN MNT_MENU_LIST d WITH(NOLOCK) on c.menu_id = d.parent_id    and isnull(d.IS_ACTIVE,''Y'')=''Y''          
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL dl WITH(NOLOCK) on dl.MENU_ID = d.MENU_ID and dl.LANG_ID = ' + cast(@LANG_ID as varchar) + '                                    
 LEFT OUTER JOIN MNT_MENU_LIST e WITH(NOLOCK) on d.menu_id = e.parent_id  and isnull(e.IS_ACTIVE,''Y'')=''Y''                 
 LEFT OUTER JOIN MNT_MENU_LIST_MULTILINGUAL el WITH(NOLOCK) on el.MENU_ID = e.MENU_ID and el.LANG_ID = ' + cast(@LANG_ID as varchar) + '                     
 WHERE a.parent_id is null  and isnull(a.IS_ACTIVE,''Y'')=''Y''   '               
                  
--  IF @AGENCY_CODE <> 'W001' --For agency other then wolverine showing only selected menus                  
--  BEGIN                  
--   SET @SQL = @SQL + ' AND a.AGENCY_LEVEL = 1 and (b.agency_level = 1 or b.menu_id is null)'                  
--  END                  
                
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
END                  
                  
SET @SQL = 'DECLARE @LOB_STRING VARCHAR(25) ' +                  
   ' SET @LOB_STRING = ''' + @LOB_STRING + '''' +                  
   @SQL                  
EXECUTE(@SQL )                  
--print @sql       