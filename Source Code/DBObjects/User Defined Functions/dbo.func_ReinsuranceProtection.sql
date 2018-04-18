IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_ReinsuranceProtection]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_ReinsuranceProtection]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*    
created by:Swarup Pal   
Date   :29 aug 2007    
Purpose   : to fetch Premium builder protection    
modified by:Pravesh K Chandel
Date   : 17 March 2009    
Purpose   : to optimize the code (remove cursor)
used in Wolvorine/Reinsurace    
*/    
--DROP FUNCTION dbo.func_ReinsuranceProtection   
create function [dbo].[func_ReinsuranceProtection]      
(      
@PREMIUM_BUILDER_ID int      
) RETURNS  NVARCHAR(200) 
as      
BEGIN        
 DECLARE @PROTECTION VARCHAR(100)      
 DECLARE @PROTECTION_DESC VARCHAR(1000)      
-- DECLARE @TEMP_PROTECTION_DESC VARCHAR(200)      
 SET @PROTECTION_DESC=''      
      
-- DECLARE @MYSQL VARCHAR(1000)      
 --SELECT @PROTECTION=PROTECTION FROM MNT_REIN_PREMIUM_BUILDER with(nolock)     
  -- WHERE   PREMIUM_BUILDER_ID=@PREMIUM_BUILDER_ID
      
------SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE CONVERT(NVARCHAR,LOOKUP_UNIQUE_ID) IN ( select * from dbo.func_Split(@CATEGORY, ',') )      
------ EXECUTE (@MYsQL)      
------SELECT @CATEGORY      
---- DECLARE  CR CURSOR FOR SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES with(nolock) WHERE CONVERT(NVARCHAR,LOOKUP_UNIQUE_ID) IN ( select * from dbo.func_Split(@PROTECTION, ',') )      
---- --LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE CONVERT(NVARCHAR,LOOKUP_UNIQUE_ID) IN ( @CATEGORY)       
----   OPEN CR          
----   FETCH NEXT FROM CR INTO @TEMP_PROTECTION_DESC      
----   WHILE @@FETCH_STATUS = 0          
----    BEGIN           
----   SET @PROTECTION_DESC= @PROTECTION_DESC + @TEMP_PROTECTION_DESC + ','      
----       
----            
----    FETCH NEXT FROM CR INTO @TEMP_PROTECTION_DESC      
----    END         
----        
----   CLOSE CR          
----   DEALLOCATE CR   
select @PROTECTION_DESC=protection
from
(
select distinct tt1.PREMIUM_BUILDER_ID,
isnull(tt1.lookup_value_desc,'') +  isnull(',' + tt2.lookup_value_desc,'') +  isnull(',' + tt3.lookup_value_desc,'') 
+  isnull(',' + tt4.lookup_value_desc,'') +  isnull(',' +tt5.lookup_value_desc,'') +  isnull(',' +tt6.lookup_value_desc,'') 
+  isnull(',' +tt7.lookup_value_desc,'') +  isnull(',' + tt8.lookup_value_desc,'') +  isnull(',' + tt10.lookup_value_desc,'') 
+  isnull(',' +tt11.lookup_value_desc,'') +  isnull(',' + tt12.lookup_value_desc,'') as protection

 from
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%'  
) tt1
left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on  lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt2 on tt1.PREMIUM_BUILDER_ID=tt2.PREMIUM_BUILDER_ID and tt2.lookup_unique_id=11855


left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on  lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt3 on tt1.PREMIUM_BUILDER_ID=tt3.PREMIUM_BUILDER_ID and tt3.lookup_unique_id=11856

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on  lookup_id= 1285 and  bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt4 on tt1.PREMIUM_BUILDER_ID=tt4.PREMIUM_BUILDER_ID and tt4.lookup_unique_id=11857

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on  lookup_id= 1285 and  bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt5 on tt1.PREMIUM_BUILDER_ID=tt5.PREMIUM_BUILDER_ID and tt5.lookup_unique_id=11858

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on  lookup_id= 1285 and  bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt6 on tt1.PREMIUM_BUILDER_ID=tt6.PREMIUM_BUILDER_ID and tt6.lookup_unique_id=11859

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt7 on tt1.PREMIUM_BUILDER_ID=tt7.PREMIUM_BUILDER_ID and tt7.lookup_unique_id=11860

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt8 on tt1.PREMIUM_BUILDER_ID=tt8.PREMIUM_BUILDER_ID and tt8.lookup_unique_id=11861

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt10 on tt1.PREMIUM_BUILDER_ID=tt10.PREMIUM_BUILDER_ID and tt10.lookup_unique_id=11862

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock)on lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt11 on tt1.PREMIUM_BUILDER_ID=tt11.PREMIUM_BUILDER_ID and tt11.lookup_unique_id=11863

left outer join
(
SELECT bld.PREMIUM_BUILDER_ID,LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC from MNT_REIN_PREMIUM_BUILDER bld with(nolock)
left outer join mnt_lookup_values m with(nolock) on lookup_id= 1285 and bld.PROTECTION like '%' +  convert(varchar,m.lookup_unique_id) + '%' 
) tt12 on tt1.PREMIUM_BUILDER_ID=tt12.PREMIUM_BUILDER_ID and tt12.lookup_unique_id=11864

where tt1.lookup_unique_id=11854
) tmp
where PREMIUM_BUILDER_ID = @PREMIUM_BUILDER_ID

--set  @PROTECTION_DESC = substring(@PROTECTION_DESC,0,len(@PROTECTION_DESC))    
RETURN  @PROTECTION_DESC  
      
END       


GO

