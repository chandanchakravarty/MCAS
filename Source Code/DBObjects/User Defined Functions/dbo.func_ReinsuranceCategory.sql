IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_ReinsuranceCategory]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_ReinsuranceCategory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
created by:Pravesh K chandel  
Date   :13 aug 2007  
Purpose   : to feth Coverage categories  
used in Wolvorine/Reinsurace  
*/  
--DROP FUNCTION dbo.func_ReinsuranceCategory    
create function dbo.func_ReinsuranceCategory    
(    
@COVERAGE_CATEGORY_ID int    
) RETURNS varchar(1000)    
AS    
BEGIN      
 DECLARE @CATEGORY VARCHAR(100)    
 DECLARE @CATEGORY_DESC VARCHAR(1000)    
 DECLARE @TEMP_CATEGORY_DESC VARCHAR(50)    
     
 SET @CATEGORY_DESC=''    
    
 DECLARE @MYsQL VARCHAR(1000)    
 SELECT @CATEGORY=CATEGORY FROM MNT_REINSURANCE_COVERAGE_CATEGORY    
   WHERE         
    COVERAGE_CATEGORY_ID=@COVERAGE_CATEGORY_ID    
    
--SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE CONVERT(NVARCHAR,LOOKUP_UNIQUE_ID) IN ( select * from dbo.func_Split(@CATEGORY, ',') )    
-- EXECUTE (@MYsQL)    
--SELECT @CATEGORY    
 DECLARE  CR CURSOR FOR SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE CONVERT(NVARCHAR,LOOKUP_UNIQUE_ID) IN ( select * from dbo.func_Split(@CATEGORY, ',') )    
 --LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE CONVERT(NVARCHAR,LOOKUP_UNIQUE_ID) IN ( @CATEGORY)     
   OPEN CR        
   FETCH NEXT FROM CR INTO @TEMP_CATEGORY_DESC    
   WHILE @@FETCH_STATUS = 0        
    BEGIN         
   SET @CATEGORY_DESC= @CATEGORY_DESC + @TEMP_CATEGORY_DESC + ','    
     
          
    FETCH NEXT FROM CR INTO @TEMP_CATEGORY_DESC    
    END       
      
   CLOSE CR        
   DEALLOCATE CR       
set  @CATEGORY_DESC = substring(@CATEGORY_DESC,0,len(@CATEGORY_DESC))  
RETURN  @CATEGORY_DESC    
    
END     
    
    
    
    
  
  
  


GO

