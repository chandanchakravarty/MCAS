IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_GET_EXCEPTION_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_GET_EXCEPTION_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                  
Proc Name             : Dbo.PROC_MIG_GET_EXCEPTION_DETAILS                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 11 May 2011                                                                
Purpose               : TO GET EXCEPTION DETAILS   
Revison History       :                                                                  
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                     
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
b                           
drop Proc PROC_MIG_GET_EXCEPTION_DETAILS    155,1,1,'COV'                                                     
------   ------------       -------------------------*/                                                                  
--      
                                     
--                                   
                                
CREATE PROCEDURE [dbo].[PROC_MIG_GET_EXCEPTION_DETAILS]          
        
@IMPORT_REQUEST_ID    INT,                           
@IMPORT_SERIAL_NO     INT,        
@LANG_ID              INT,       
@MODE                 CHAR(4)='APP'      
      
        
                                     
AS                                      
BEGIN                               
          
       
 DECLARE @ERROR_TYPES NVARCHAR(512)      
 DECLARE @ERROR_COLUMN NVARCHAR(512)      
 DECLARE @ERROR_COLUMN_VALUE NVARCHAR(MAX)      
 DECLARE @ERROR_ROW_NUM INT    
 DECLARE @TEMP INT =0   
      
     
  SELECT @ERROR_TYPES   = ERROR_TYPES,    
         @ERROR_COLUMN   = ERROR_SOURCE_COLUMN,    
         @ERROR_COLUMN_VALUE = ERROR_SOURCE_COLUMN_VALUE,    
         @ERROR_ROW_NUM      = ERROR_ROW_NUMBER      
  FROM MIG_IMPORT_ERROR_DETAILS     
  WHERE IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID AND     
        IMPORT_SERIAL_NO     = @IMPORT_SERIAL_NO  AND     
        ERROR_SOURCE_TYPE    = @MODE    
   
      
  SELECT IMPORT_REQUEST_ID ,      
         IMPORT_SERIAL_NO,            
         ERROR_MODE          
  FROM   MIG_IMPORT_ERROR_DETAILS       
  WHERE  IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND     
         IMPORT_SERIAL_NO   = @IMPORT_SERIAL_NO  AND     
         ERROR_SOURCE_TYPE  = @MODE    

         
  SELECT M.ERROR_TYPE_ID,  ISNULL(N.TYPE_DESC , M.ERROR_DESCRIPTION) AS ERROR_DESC , ERROR_COLUMN,ERROR_COLUMN_VALUE    
    FROM   MIG_ERROR_TYPE_DETAIL M LEFT OUTER JOIN      
        MIG_ERROR_TYPE_DETAIL_MULTILINGUAL  N ON M.ERROR_TYPE_ID=N.ERROR_TYPE_ID AND N.LANG_ID=@LANG_ID  INNER JOIN      
    (      
       SELECT row_number() OVER(ORDER BY @temp asc) AS ROW_NO,    
              item AS ERROR_ID    
       FROM dbo.func_Split(@ERROR_TYPES,',' )      
    )A ON A.ERROR_ID=M.ERROR_TYPE_ID  LEFT OUTER JOIN      
             (      
                SELECT row_number() OVER(ORDER BY @temp asc) AS ROW_NO    
                       ,item AS ERROR_COLUMN     
                FROM dbo.func_Split(@ERROR_COLUMN,',' )    
             )B ON A.ROW_NO=B.ROW_NO  LEFT OUTER JOIN    
            (      
                SELECT row_number() OVER(ORDER BY @temp asc) AS ROW_NO ,    
                       item AS ERROR_COLUMN_VALUE    
                FROM dbo.func_Split(@ERROR_COLUMN_VALUE,',' )    
            )C ON B.ROW_NO=C.ROW_NO      
        
        
                  
END 



GO

