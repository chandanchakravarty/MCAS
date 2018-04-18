IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_APP_WATERCRAFT_COPY_COVERAGE_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_APP_WATERCRAFT_COPY_COVERAGE_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc PROC_APP_WATERCRAFT_COPY_COVERAGE_INFO          
/*----------------------------------------------------------                            
Proc Name   : dbo.Proc_GetAPP_VEHICLE_COVERAGES_COPY                  
Created by  : Shafi                  
Date        : 17 May,2006                  
Purpose     : Get the coverages IDs for copying                  
Revison History  :  
Modified By 	:Pravesh K Chandel
Modified Date	:26 July
purpose 	: Fetch LOB_ID                                 
 ------------------------------------------------------------                                        
Date     Review By          Comments                                      
                             
------   ------------       -------------------------*/          
CREATE PROCEDURE dbo.PROC_APP_WATERCRAFT_COPY_COVERAGE_INFO                  
(                  
 @WATERCRAFT_ID smallint,                  
 @CUSTOMER_ID int,                  
 @APP_ID int,                  
 @APP_VERSION_ID int              
             
                  
)                      
AS                           
BEGIN                            
                 
           
       
      
    
                                     
--Get the State for the application                                        
SELECT CS.STATE_ID,                  
       CS.STATE_NAME,                
       A.APP_LOB  ,
       YEAR(CONVERT(VARCHAR(20),A.APP_EFFECTIVE_DATE,109)) as APP_EFFECTIVE_DATE,                    
       A.APP_EFFECTIVE_DATE as APP_EFF_DATE        ,
	 A.APP_LOB  as LOB_ID    
 FROM APP_LIST A                  
 INNER JOIN MNT_COUNTRY_STATE_LIST CS ON                  
  A.STATE_ID = CS.STATE_ID                  
 WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND                  
               A.APP_ID=@APP_ID     AND                  
               A.APP_VERSION_ID=@APP_VERSION_ID AND                  
               CS.COUNTRY_ID = 1                  
                                        
                        
--Table 3                       
--Get the Watercraft info from APP_WATERCRAFT_INFO                        
SELECT INSURING_VALUE,            
       TYPE_OF_WATERCRAFT,          
       ISNULL(TYPE,'') AS TYPE,                
       LENGTH,              
       YEAR ,        
       BOAT_ID,        
       MAKE,        
       MODEL,  
       COV_TYPE_BASIS  
  FROM APP_WATERCRAFT_INFO  INNER JOIN MNT_LOOKUP_VALUES           
  ON APP_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID          
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                            
   APP_ID = @APP_ID AND                                            
   APP_VERSION_ID = @APP_VERSION_ID  AND                        
   BOAT_ID = @WATERCRAFT_ID                         
                         
      
                  
                         
                
END                  
                  
                  
   
                
              
            
          
        
      
    
  


        
          
        
      
    
  





GO

