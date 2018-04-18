IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookupValuesForBoatType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookupValuesForBoatType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : dbo.Proc_GetLookupValuesForBoatType              
Created by      : Praveen Singh           
Date            : 4 jan ,2006                  
Purpose         : To retrieve records from look up table              
Revison History :                  
Used In         : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------              
drop proc Proc_GetLookupValuesForBoatType               
*/                  
              
CREATE PROC [dbo].[Proc_GetLookupValuesForBoatType]                  
(                  
 @LookupCode  NVarChar(6),              
 @LookupParam NVarChar(20) = null,            
 @OrderBy NVarChar(50) = null,  
 @LANG_ID int = 1 --Added by Charles on 15-Mar-10 for Multilingual Implementation        
)                  
            
AS                  
     DECLARE @SQL NVARCHAR(4000)         
BEGIN       
    
 SET @SQL=''      
 SET @SQL = 'SELECT  ISNULL(MLVL.LOOKUP_UNIQUE_ID,MLV.LOOKUP_UNIQUE_ID) AS LOOKUP_UNIQUE_ID,  
     ISNULL(MLVL.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) AS LOOKUP_VALUE_DESC, MLV.LOOKUP_VALUE_CODE AS LOOKUP_VALUE_CODE  
     FROM  MNT_LOOKUP_VALUES MLV              
        INNER JOIN MNT_LOOKUP_TABLES MLT ON MLV.LOOKUP_ID = MLT.LOOKUP_ID     
        LEFT OUTER  JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVL ON MLV.LOOKUP_UNIQUE_ID = MLVL.LOOKUP_UNIQUE_ID     
  AND MLVL.LANG_ID= ' + convert(varchar,@LANG_ID) + '          
        WHERE  MLT.LOOKUP_NAME = ' + '''' + @LookupCode + '''' + ' AND MLV.IS_ACTIVE=' + '''' + 'Y' + '''' +  ' ORDER BY '  +    @OrderBy           
        --lookup_value_desc ASC              
 SET    @SQL=CONVERT(NVARCHAR(4000),@SQL)      
 EXEC(@SQL)        
END        
           
/*        
select lookup_value_desc from mnt_lookup_values        
where lookup_unique_id=11369    
   or lookup_unique_id=11370    
    
     
update mnt_lookup_values    
set lookup_value_desc='Inboard / Outboard Boat'    
where lookup_unique_id=11370    
     
    
update mnt_lookup_values    
set lookup_value_desc='Outboard Boat'    
where lookup_unique_id=11369    
     
    */    
             
          
-- exec Proc_GetLookupValuesForBoatType 'WCTCD',null,'lookup_value_desc',2        
      
  /*    
SELECT  MLV.LOOKUP_UNIQUE_ID,MLV.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_CODE  FROM  MNT_LOOKUP_VALUES MLV            
       INNER JOIN MNT_LOOKUP_TABLES MLT ON MLV.LOOKUP_ID = MLT.LOOKUP_ID            
       WHERE  MLT.LOOKUP_NAME = 'WCTCD' AND MLV.IS_ACTIVE='Y' ORDER BY lookup_value_desc    
*/     
GO

