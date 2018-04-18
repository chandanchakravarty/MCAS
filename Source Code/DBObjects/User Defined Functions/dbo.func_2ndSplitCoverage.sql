IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_2ndSplitCoverage]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_2ndSplitCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
created by: Swarup and Manoj  
Date   : 21 sep 2007  
Purpose   : to feth Second Split Coverages  
used in Wolvorine/Reinsurace  
*/  
--DROP FUNCTION dbo.func_2ndSplitCoverage    
create function dbo.func_2ndSplitCoverage    
(    
 @COVERAGE_ID int    
)  RETURNS varchar(1000)    
AS    
BEGIN      
 DECLARE @CATEGORY VARCHAR(100)    
 DECLARE @CATEGORY_DESC VARCHAR(1000)    
 DECLARE @TEMP_CATEGORY_DESC VARCHAR(50)    
     
 SET @CATEGORY_DESC=''    
    
 DECLARE @MYsQL VARCHAR(1000)    
 SELECT @CATEGORY=REIN_2ND_SPLIT_COVERAGE FROM MNT_REIN_SPLIT    
   WHERE         
    REIN_SPLIT_DEDUCTION_ID=@COVERAGE_ID    
    
 DECLARE  CR CURSOR FOR SELECT COV_DES FROM MNT_REINSURANCE_COVERAGE WHERE CONVERT(NVARCHAR,COV_ID) IN ( select * from dbo.func_Split(@CATEGORY, ',') )    
   OPEN CR        
   FETCH NEXT FROM CR INTO @TEMP_CATEGORY_DESC    
   WHILE @@FETCH_STATUS = 0        
    BEGIN         
   SET @CATEGORY_DESC= @CATEGORY_DESC + @TEMP_CATEGORY_DESC + ','    
     
          
    FETCH NEXT FROM CR INTO @TEMP_CATEGORY_DESC    
    END       
      
   CLOSE CR        
   DEALLOCATE CR   
      
  DECLARE  CRN CURSOR FOR SELECT COV_DES FROM MNT_COVERAGE WHERE CONVERT(NVARCHAR,COV_ID) IN ( select * from dbo.func_Split(@CATEGORY, ',') )    
   OPEN CRN        
   FETCH NEXT FROM CRN INTO @TEMP_CATEGORY_DESC    
   WHILE @@FETCH_STATUS = 0        
    BEGIN         
   SET @CATEGORY_DESC= @CATEGORY_DESC + @TEMP_CATEGORY_DESC + ','    
     
          
    FETCH NEXT FROM CRN INTO @TEMP_CATEGORY_DESC    
    END       
      
   CLOSE CRN        
   DEALLOCATE CRN       
set  @CATEGORY_DESC = substring(@CATEGORY_DESC,0,len(@CATEGORY_DESC))  
RETURN  @CATEGORY_DESC    
    
END     
    
    
    
    
  
  
   



GO

