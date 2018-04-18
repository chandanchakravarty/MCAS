IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLAPP_WATER_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLAPP_WATER_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                
Proc Name        :            dbo.Proc_GetXMLAPP_WATER_MVR_INFORMATION                                 
Created by         :           Sumit Chhabra                                
Date                :           14/10/2005                                
Purpose           :           Get the table information in the form of xml data                                
Revison History  :                                
Used In             :           Wolverine                                
------------------------------------------------------------                                
Date     Review By          Comments    
drop PROC Dbo.Proc_GetXMLAPP_WATER_MVR_INFORMATION                                
------   ------------       -------------------------*/                                
CREATE PROC Dbo.Proc_GetXMLAPP_WATER_MVR_INFORMATION                
(                                
 @CUSTOMER_ID INT,            
 @APP_ID INT,            
 @APP_VERSION_ID INT,            
 @DRIVER_ID INT,                     
 @APP_WATER_MVR_ID  INT                           
)                                
AS                                
BEGIN   
DECLARE @TEMP_VIOLATION_TYPE INT    
    
SELECT @TEMP_VIOLATION_TYPE=VIOLATION_TYPE FROM APP_WATER_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND DRIVER_ID=@DRIVER_ID AND APP_ID=@APP_ID    
AND APP_VERSION_ID=@APP_VERSION_ID AND APP_WATER_MVR_ID=@APP_WATER_MVR_ID  
  
IF (@TEMP_VIOLATION_TYPE>=15000)     
 SELECT A.APP_WATER_MVR_ID,                                
 A.CUSTOMER_ID,                                
 A.APP_ID,                                
 A.APP_VERSION_ID,                                
 A.DRIVER_ID,                            
 A.VIOLATION_ID,                                
 A.DRIVER_ID,                                
 A.MVR_AMOUNT,                                
 A.MVR_DEATH,        
 A.VERIFIED,        
 A.VIOLATION_TYPE,                              
 convert(char,A.mvr_date,101) MVR_DATE,  
 convert(char,A.occurence_date,101) OCCURENCE_DATE,   
 A.DETAILS, 
 case when A.POINTS_ASSIGNED is null then '' else convert(varchar,A.POINTS_ASSIGNED) end POINTS_ASSIGNED, 
 case when A.ADJUST_VIOLATION_POINTS is null then '' else convert(varchar,A.ADJUST_VIOLATION_POINTS) end ADJUST_VIOLATION_POINTS,                                     
-- (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +  '/' +  CAST(isnull(M.SD_POINTS,'') AS VARCHAR)  +  ')' )AS VIOLATION_DES,            
 (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +    ')' )AS VIOLATION_DES,            
 A.IS_ACTIVE                   
 FROM  APP_WATER_MVR_INFORMATION A JOIN MNT_VIOLATIONS M          
 ON A.VIOLATION_TYPE=M.VIOLATION_ID          
 WHERE            
 A.CUSTOMER_ID=@CUSTOMER_ID AND             
 A.APP_ID=@APP_ID AND            
 A.APP_VERSION_ID=@APP_VERSION_ID AND            
 A.DRIVER_ID=@DRIVER_ID AND             
 A.APP_WATER_MVR_ID=@APP_WATER_MVR_ID       
ELSE                               
 SELECT A.APP_WATER_MVR_ID,                                
 A.CUSTOMER_ID,                                
 A.APP_ID,                                
 A.APP_VERSION_ID,                                
 A.DRIVER_ID,                            
 A.VIOLATION_ID,                                
 A.DRIVER_ID,                                
 A.MVR_AMOUNT,                                
 A.MVR_DEATH,        
 A.VERIFIED,        
 A.VIOLATION_TYPE,                              
 convert(char,A.mvr_date,101) MVR_DATE,                  
 convert(char,A.occurence_date,101) OCCURENCE_DATE,   
 A.DETAILS,
 case when A.POINTS_ASSIGNED is null then '' else convert(varchar,A.POINTS_ASSIGNED) end POINTS_ASSIGNED, 
 case when A.ADJUST_VIOLATION_POINTS is null then '' else convert(varchar,A.ADJUST_VIOLATION_POINTS) end ADJUST_VIOLATION_POINTS,                                     
-- (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +  '/' +  CAST(isnull(M.SD_POINTS,'') AS VARCHAR)  +  ')' )AS VIOLATION_DES,            
 (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +    ')' )AS VIOLATION_DES,            
 A.IS_ACTIVE                   
 FROM  APP_WATER_MVR_INFORMATION A JOIN MNT_VIOLATIONS M          
 ON A.VIOLATION_ID=M.VIOLATION_ID          
 WHERE            
 A.CUSTOMER_ID=@CUSTOMER_ID AND             
 A.APP_ID=@APP_ID AND            
 A.APP_VERSION_ID=@APP_VERSION_ID AND            
 A.DRIVER_ID=@DRIVER_ID AND             
 A.APP_WATER_MVR_ID=@APP_WATER_MVR_ID                                    
END                
                
              
            
          
        
      
    
  



GO

