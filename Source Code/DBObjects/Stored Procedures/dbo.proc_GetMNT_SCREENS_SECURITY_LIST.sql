IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetMNT_SCREENS_SECURITY_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetMNT_SCREENS_SECURITY_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : dbo.proc_GetMNT_SCREENS_SECURITY_LIST                  
Created by      : Anshuman                  
Date            : 5/30/2005                  
Purpose     : Fetch records from MNT_SCREEN_LIST, MNT_USER_TYPE_PERMISSION & MNT_MENU_LIST                  
    Give permission xml baased on userTypeId, and 2nd level of menu_id                  
Revison History :                  
Used In  : BRICS                  
------------------------------------------------------------                  
Date     Review By          Comments                  
Modify By  : Anshuman                  
Modified On  : 01 June,2005                  
Purpose  : Get security from user_type and user both, added paramameter user_id                  
------   ------------       -------------------------*/                  
-- drop PROCEDURE dbo.proc_GetMNT_SCREENS_SECURITY_LIST                  
CREATE PROCEDURE dbo.proc_GetMNT_SCREENS_SECURITY_LIST                  
(                  
 @SUB_SECTION_ID int,                  
 @USER_TYPE_ID smallint = null,                  
 @USER_ID int = null,          
 @MODULE_NAME VARCHAR(10)=NULL,      
 @CALLED_FOR VARCHAR(20)=NULL                  
)                  
AS                  
BEGIN      
 DECLARE @AGENCY_LEVEL smallint      
   
 IF(@CALLED_FOR is null)      
  SET @AGENCY_LEVEL = 0      
 ELSE      
  SET @AGENCY_LEVEL = 1      
      
 IF @MODULE_NAME IS NULL          
 BEGIN             
  IF @USER_ID IS NULL                  
  BEGIN                  
     
   SELECT a.menu_name as level1,a.menu_id as level1_id,                  
   b.menu_name as level2,b.menu_id as level2_id,                  
   c.menu_name as level3,c.menu_id as level3_id,                  
   d.menu_name as level4,d.menu_id as level4_id,                  
   e.screen_id,screen_desc,screen_read,screen_write,screen_delete,screen_execute,isnull(e.is_active,'Y') as screen_isactive,                  
   f.permission_xml,b.module_name                  
                  
   FROM  MNT_MENU_LIST a   WITH(NOLOCK)               
                 
   LEFT OUTER JOIN MNT_MENU_LIST b on a.menu_id = b.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST c on b.menu_id = c.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST d on c.menu_id = d.parent_id                   
   LEFT OUTER JOIN MNT_SCREEN_LIST e ON                   
   CASE                   
    WHEN NOT d.menu_id IS NULL THEN d.menu_id                   
    WHEN NOT c.menu_id IS NULL THEN c.menu_id                   
    WHEN NOT b.menu_id IS NULL THEN b.menu_id                   
    WHEN NOT a.menu_id IS NULL THEN a.menu_id                   
   END                  
   = convert(int,substring(e.screen_id,0,case charindex('_',e.screen_id) when 0 then len(e.screen_id)+1 else charindex('_',e.screen_id) end))                   
   LEFT OUTER JOIN MNT_USER_TYPE_PERMISSION f ON e.screen_id = f.screen_id and f.user_type_id = @USER_TYPE_ID                  
              
   WHERE                   
   case                   
   when                   
    (SELECT COUNT(MENU_ID) FROM MNT_MENU_LIST WITH(NOLOCK) WHERE PARENT_ID = @SUB_SECTION_ID ) > 0 THEN a.parent_id                  
   else                  
    a.menu_id                      
   end =  @SUB_SECTION_ID AND ISNULL(a.AGENCY_LEVEL,0) IN (1, 2, @AGENCY_LEVEL)                  
                  
   AND  isnull(e.is_active,'Y') = 'Y'                
   ORDER BY                  
   a.nestlevel asc , a.menu_order asc,                  
   b.nestlevel asc , b.menu_order asc,                  
   c.nestlevel asc , c.menu_order asc,                  
   d.nestlevel asc , d.menu_order asc                  
                
  end                  
 else                  
  begin                  
     
   SELECT a.menu_name as level1,a.menu_id as level1_id,                  
   b.menu_name as level2,b.menu_id as level2_id,                  
   c.menu_name as level3,c.menu_id as level3_id,                  
   d.menu_name as level4,d.menu_id as level4_id,                  
   e.screen_id,screen_desc,screen_read,screen_write,screen_delete,screen_execute,isnull(e.is_active,'Y') as screen_isactive,                  
   f.permission_xml,b.module_name            
                  
   FROM  MNT_MENU_LIST a   WITH(NOLOCK)               
                
   LEFT OUTER JOIN MNT_MENU_LIST b on a.menu_id = b.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST c on b.menu_id = c.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST d on c.menu_id = d.parent_id                   
   LEFT OUTER JOIN MNT_SCREEN_LIST e ON                   
   CASE                   
    WHEN NOT d.menu_id IS NULL THEN d.menu_id                   
    WHEN NOT c.menu_id IS NULL THEN c.menu_id                   
    WHEN NOT b.menu_id IS NULL THEN b.menu_id                   
    WHEN NOT a.menu_id IS NULL THEN a.menu_id                   
   END                  
   = convert(int,substring(e.screen_id,0,case charindex('_',e.screen_id) when 0 then len(e.screen_id)+1 else charindex('_',e.screen_id) end))                   
   LEFT OUTER JOIN                  
   (                  
    SELECT SCREEN_ID,PERMISSION_XML,IS_ACTIVE                   
    FROM  MNT_USER_TYPE_PERMISSION  WITH(NOLOCK)                 
    WHERE  USER_TYPE_ID=@USER_TYPE_ID AND                  
    SCREEN_ID NOT IN                  
    (                  
     SELECT SCREEN_ID                  
     FROM  MNT_USER_PERMISSION                  
     WHERE USER_ID=@USER_ID                  
    )                  
    UNION                  
    SELECT SCREEN_ID,PERMISSION_XML,IS_ACTIVE                   
    FROM  MNT_USER_PERMISSION WITH(NOLOCK)                 
    WHERE USER_ID=@USER_ID                  
   ) f ON e.screen_id = f.screen_id                  
              
   WHERE                 
   CASE                   
   when                   
    (SELECT COUNT(MENU_ID) FROM MNT_MENU_LIST WITH(NOLOCK) WHERE PARENT_ID = @SUB_SECTION_ID ) > 0 THEN a.parent_id                  
   else                  
   a.menu_id                      
   end =  @SUB_SECTION_ID AND ISNULL(a.AGENCY_LEVEL,0) IN (1, 2, @AGENCY_LEVEL)                 
   
   AND  isnull(e.is_active,'Y') = 'Y'           
                
   ORDER BY                  
   a.nestlevel asc , a.menu_order asc,                  
   b.nestlevel asc , b.menu_order asc,                  
   c.nestlevel asc , c.menu_order asc,                  
   d.nestlevel asc , d.menu_order asc                  
              
  end                  
   
 end          
  else          
  begin          
   if upper(@module_name)='POL'          
   begin          
    if @USER_ID is null                  
    begin                  
       
     SELECT a.menu_name as level1,a.menu_id as level1_id,                  
     b.menu_name as level2,b.menu_id as level2_id,                  
     c.menu_name as level3,c.menu_id as level3_id,                  
     d.menu_name as level4,d.menu_id as level4_id,                  
     e.screen_id,screen_desc,screen_read,screen_write,screen_delete,screen_execute,isnull(e.is_active,'Y') as screen_isactive,                  
     f.permission_xml,b.module_name                  
                
     FROM  MNT_MENU_LIST a     WITH(NOLOCK)             
                    
     LEFT OUTER JOIN MNT_MENU_LIST b on a.menu_id = b.parent_id                   
     LEFT OUTER JOIN MNT_MENU_LIST c on b.menu_id = c.parent_id                   
     LEFT OUTER JOIN MNT_MENU_LIST d on c.menu_id = d.parent_id                   
     LEFT OUTER JOIN MNT_SCREEN_LIST e ON                   
     CASE                   
      WHEN NOT d.menu_id IS NULL THEN d.menu_id                   
      WHEN NOT c.menu_id IS NULL THEN c.menu_id                   
      WHEN NOT b.menu_id IS NULL THEN b.menu_id                   
      WHEN NOT a.menu_id IS NULL THEN a.menu_id                   
     END                  
     = convert(int,substring(e.screen_id,0,case charindex('_',e.screen_id) when 0 then len(e.screen_id)+1 else charindex('_',e.screen_id) end))                   
     LEFT OUTER JOIN MNT_USER_TYPE_PERMISSION f ON e.screen_id = f.screen_id and f.user_type_id = @USER_TYPE_ID                  
              
     WHERE                   
     case                   
     when                   
     (SELECT COUNT(MENU_ID) FROM MNT_MENU_LIST WITH(NOLOCK) WHERE PARENT_ID = @SUB_SECTION_ID ) > 0 THEN a.parent_id                  
     else                  
     a.menu_id             
     end =  @SUB_SECTION_ID                   
     and b.module_name= @module_name  AND ISNULL(a.AGENCY_LEVEL,0) IN (1, 2, @AGENCY_LEVEL)                
     AND  isnull(e.is_active,'Y') = 'Y'                  
     ORDER BY                  
     a.nestlevel asc , a.menu_order asc,                  
     b.nestlevel asc , b.menu_order asc,                  
     c.nestlevel asc , c.menu_order asc,                  
     d.nestlevel asc , d.menu_order asc                  
                
    end                  
   else                  
    begin    
                  
     SELECT a.menu_name as level1,a.menu_id as level1_id,                  
     b.menu_name as level2,b.menu_id as level2_id,                  
     c.menu_name as level3,c.menu_id as level3_id,                  
     d.menu_name as level4,d.menu_id as level4_id,                  
     e.screen_id,screen_desc,screen_read,screen_write,screen_delete,screen_execute,isnull(e.is_active,'Y') as screen_isactive,                  
     f.permission_xml,b.module_name            
                    
     FROM  MNT_MENU_LIST a   WITH(NOLOCK)               
                
     LEFT OUTER JOIN MNT_MENU_LIST b on a.menu_id = b.parent_id                   
     LEFT OUTER JOIN MNT_MENU_LIST c on b.menu_id = c.parent_id                   
     LEFT OUTER JOIN MNT_MENU_LIST d on c.menu_id = d.parent_id                   
     LEFT OUTER JOIN MNT_SCREEN_LIST e ON                   
     CASE                   
      WHEN NOT d.menu_id IS NULL THEN d.menu_id                   
      WHEN NOT c.menu_id IS NULL THEN c.menu_id                   
      WHEN NOT b.menu_id IS NULL THEN b.menu_id                   
      WHEN NOT a.menu_id IS NULL THEN a.menu_id                   
     END                  
     = convert(int,substring(e.screen_id,0,case charindex('_',e.screen_id) when 0 then len(e.screen_id)+1 else charindex('_',e.screen_id) end))                   
     LEFT OUTER JOIN                  
     (                  
      SELECT SCREEN_ID,PERMISSION_XML,IS_ACTIVE                   
      FROM  MNT_USER_TYPE_PERMISSION WITH(NOLOCK)                   
      WHERE  USER_TYPE_ID=@USER_TYPE_ID AND                  
      SCREEN_ID NOT IN                  
      (                  
       SELECT SCREEN_ID                  
       FROM  MNT_USER_PERMISSION                  
       WHERE USER_ID=@USER_ID                  
      )                  
      UNION                  
      SELECT SCREEN_ID,PERMISSION_XML,IS_ACTIVE                   
      FROM  MNT_USER_PERMISSION   WITH(NOLOCK)               
      WHERE USER_ID=@USER_ID                  
     ) f ON e.screen_id = f.screen_id                  
                  
     WHERE                   
     case                   
     when                   
     (SELECT COUNT(MENU_ID) FROM MNT_MENU_LIST WITH(NOLOCK) WHERE PARENT_ID = @SUB_SECTION_ID ) > 0 THEN a.parent_id                  
     else                  
     a.menu_id                      
     end =  @SUB_SECTION_ID                  
     and b.module_name = @module_name AND ISNULL(a.AGENCY_LEVEL,0) IN (1, 2, @AGENCY_LEVEL)                 
     AND  isnull(e.is_active,'Y') = 'Y'                  
     ORDER BY                  
     a.nestlevel asc , a.menu_order asc,                  
     b.nestlevel asc , b.menu_order asc,                  
     c.nestlevel asc , c.menu_order asc,                  
     d.nestlevel asc , d.menu_order asc                  
              
    end                  
   
   end          
  else          
 begin          
  if @USER_ID is null                  
  begin      
     
   SELECT a.menu_name as level1,a.menu_id as level1_id,                  
   b.menu_name as level2,b.menu_id as level2_id,                  
   c.menu_name as level3,c.menu_id as level3_id,                  
   d.menu_name as level4,d.menu_id as level4_id,                  
   e.screen_id,screen_desc,screen_read,screen_write,screen_delete,screen_execute,isnull(e.is_active,'Y') as screen_isactive,                  
   f.permission_xml,b.module_name                  
                  
   FROM  MNT_MENU_LIST a     WITH(NOLOCK)             
                  
   LEFT OUTER JOIN MNT_MENU_LIST b on a.menu_id = b.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST c on b.menu_id = c.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST d on c.menu_id = d.parent_id                   
   LEFT OUTER JOIN MNT_SCREEN_LIST e ON                   
   CASE                   
    WHEN NOT d.menu_id IS NULL THEN d.menu_id                   
    WHEN NOT c.menu_id IS NULL THEN c.menu_id                   
    WHEN NOT b.menu_id IS NULL THEN b.menu_id                   
    WHEN NOT a.menu_id IS NULL THEN a.menu_id                   
   END                  
   = convert(int,substring(e.screen_id,0,case charindex('_',e.screen_id) when 0 then len(e.screen_id)+1 else charindex('_',e.screen_id) end))                   
   LEFT OUTER JOIN MNT_USER_TYPE_PERMISSION f ON e.screen_id = f.screen_id and f.user_type_id = @USER_TYPE_ID                  
                
   WHERE                   
   case                   
   when                   
   (SELECT COUNT(MENU_ID) FROM MNT_MENU_LIST WITH(NOLOCK) WHERE PARENT_ID = @SUB_SECTION_ID ) > 0 THEN a.parent_id                  
   else                  
   a.menu_id                      
   end =  @SUB_SECTION_ID                   
   and b.module_name is null AND ISNULL(a.AGENCY_LEVEL,0) IN (1, 2, @AGENCY_LEVEL)       
   AND  isnull(e.is_active,'Y') = 'Y'                   
   ORDER BY                  
   a.nestlevel asc , a.menu_order asc,                  
   b.nestlevel asc , b.menu_order asc,                  
   c.nestlevel asc , c.menu_order asc,                  
   d.nestlevel asc , d.menu_order asc                  
                  
  end                  
 else                  
  begin     
             
   SELECT a.menu_name as level1,a.menu_id as level1_id,                  
   b.menu_name as level2,b.menu_id as level2_id,                
   c.menu_name as level3,c.menu_id as level3_id,                  
   d.menu_name as level4,d.menu_id as level4_id,                  
   e.screen_id,screen_desc,screen_read,screen_write,screen_delete,screen_execute,isnull(e.is_active,'Y') as screen_isactive,                  
   f.permission_xml,b.module_name            
                  
   FROM  MNT_MENU_LIST a  WITH(NOLOCK)                
                  
   LEFT OUTER JOIN MNT_MENU_LIST b on a.menu_id = b.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST c on b.menu_id = c.parent_id                   
   LEFT OUTER JOIN MNT_MENU_LIST d on c.menu_id = d.parent_id                   
   LEFT OUTER JOIN MNT_SCREEN_LIST e ON                   
   CASE                   
    WHEN NOT d.menu_id IS NULL THEN d.menu_id                   
    WHEN NOT c.menu_id IS NULL THEN c.menu_id                   
    WHEN NOT b.menu_id IS NULL THEN b.menu_id                   
    WHEN NOT a.menu_id IS NULL THEN a.menu_id                   
   END                  
   = convert(int,substring(e.screen_id,0,case charindex('_',e.screen_id) when 0 then len(e.screen_id)+1 else charindex('_',e.screen_id) end))                   
   LEFT OUTER JOIN                  
   (                  
    SELECT SCREEN_ID,PERMISSION_XML,IS_ACTIVE                   
    FROM  MNT_USER_TYPE_PERMISSION  WITH(NOLOCK)                 
    WHERE  --USER_TYPE_ID=@USER_TYPE_ID AND                  
    SCREEN_ID NOT IN                  
    (                  
     SELECT SCREEN_ID                  
     FROM  MNT_USER_PERMISSION   WITH(NOLOCK)               
     WHERE USER_ID=@USER_ID                  
    )                  
    UNION                  
    SELECT SCREEN_ID,PERMISSION_XML,IS_ACTIVE                   
    FROM  MNT_USER_PERMISSION WITH(NOLOCK)                 
    WHERE USER_ID=@USER_ID                  
   ) f ON e.screen_id = f.screen_id                  
              
   WHERE                   
   case                   
   when                   
   (SELECT COUNT(MENU_ID) FROM MNT_MENU_LIST WITH(NOLOCK) WHERE PARENT_ID = @SUB_SECTION_ID ) > 0 THEN a.parent_id                  
   else                  
   a.menu_id                      
   end =  @SUB_SECTION_ID                  
   and b.module_name is null AND ISNULL(a.AGENCY_LEVEL,0) IN (1, 2, @AGENCY_LEVEL)       
   AND  isnull(e.is_active,'Y') = 'Y'                  
   ORDER BY                  
   a.nestlevel asc , a.menu_order asc,                  
   b.nestlevel asc , b.menu_order asc,                  
   c.nestlevel asc , c.menu_order asc,                  
   d.nestlevel asc , d.menu_order asc            
                
   END                  
   
  END            
 END          
END        
  


GO

