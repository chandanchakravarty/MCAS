IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLAPP_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLAPP_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name        :            dbo.Proc_GetXMLAPP_MVR_INFORMATION                                         
Created by         :           Sumit Chhabra                                        
Date                :           9/27/2005                                        
Purpose           :           Get the table informatoin in the form of xml data                                        
Revison History  :                                        
Used In             :           Wolverine                        
Modified By         : Mohit                      
Modified On         : 7/11/2005                      
Purpose             : Including Driver_dob in select statement.                                       
------------------------------------------------------------                                        
Date     Review By          Comments                
DROP PROC dbo.Proc_GetXMLAPP_MVR_INFORMATION      
Proc_GetXMLAPP_MVR_INFORMATION 1250,3,1,93,1                              
------   ------------       -------------------------*/                                        
CREATE PROC dbo.Proc_GetXMLAPP_MVR_INFORMATION                                        
(                                        
@CUSTOMER_ID int,                  
@APP_ID int,                  
@APP_VERSION_ID int,                  
@DRIVER_ID int,                  
@APP_MVR_ID  INT                                   
)                                        
AS                                        
BEGIN                                        
DECLARE @TEMP_VIOLATION_TYPE INT      
      
SELECT @TEMP_VIOLATION_TYPE=VIOLATION_TYPE FROM APP_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND DRIVER_ID=@DRIVER_ID AND APP_ID=@APP_ID      
AND APP_VERSION_ID=@APP_VERSION_ID AND APP_MVR_ID=@APP_MVR_ID      
    
IF (@TEMP_VIOLATION_TYPE>=15000)       
 SELECT A.APP_MVR_ID,                
 A.CUSTOMER_ID,                                        
 A.APP_ID,                                        
 A.APP_VERSION_ID,                                        
 A.DRIVER_ID,                                    
 A.VIOLATION_ID,                                         
 A.MVR_AMOUNT,                                        
 A.MVR_DEATH,                                        
 convert(char,A.mvr_date,101) MVR_DATE,                          
 A.IS_ACTIVE,              
 A.VERIFIED,          
 A.VIOLATION_TYPE,                
-- (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +  '/' +  CAST(isnull(M.SD_POINTS,'') AS VARCHAR)  +  ')' )AS VIOLATION_DES                  
 (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +  ')' )AS VIOLATION_DES,    
  convert(varchar, A.OCCURENCE_DATE,101) OCCURENCE_DATE,    
  A.DETAILS,  
 case when A.POINTS_ASSIGNED is null then '' else convert(varchar,A.POINTS_ASSIGNED) end POINTS_ASSIGNED, 
 case when A.ADJUST_VIOLATION_POINTS is null then '' else convert(varchar,A.ADJUST_VIOLATION_POINTS) end ADJUST_VIOLATION_POINTS
-- VIOLATION_DES                 
 FROM  APP_MVR_INFORMATION A                
  INNER JOIN MNT_VIOLATIONS M ON                
  A.VIOLATION_TYPE = M.VIOLATION_ID                
 WHERE                   
 A.CUSTOMER_ID=@CUSTOMER_ID AND                   
 A.APP_ID=@APP_ID AND                  
 A.APP_VERSION_ID=@APP_VERSION_ID AND                  
 A.DRIVER_ID=@DRIVER_ID AND                  
 A.APP_MVR_ID=@APP_MVR_ID         
ELSE IF (@TEMP_VIOLATION_TYPE!=13220)      
 SELECT A.APP_MVR_ID,                
 A.CUSTOMER_ID,                                        
 A.APP_ID,                                        
 A.APP_VERSION_ID,                                        
 A.DRIVER_ID,                                    
 A.VIOLATION_ID,                                         
 A.MVR_AMOUNT,                                        
 A.MVR_DEATH,                                        
 convert(char,A.mvr_date,101) MVR_DATE,                          
 A.IS_ACTIVE,              
 A.VERIFIED,          
 A.VIOLATION_TYPE,                
-- (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +  '/' +  CAST(isnull(M.SD_POINTS,'') AS VARCHAR)  +  ')' )AS VIOLATION_DES                  
 (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +  ')' )AS VIOLATION_DES,    
  convert(varchar, A.OCCURENCE_DATE,101) OCCURENCE_DATE,    
  A.DETAILS,  
 case when A.POINTS_ASSIGNED is null then '' else convert(varchar,A.POINTS_ASSIGNED) end POINTS_ASSIGNED, 
 case when A.ADJUST_VIOLATION_POINTS is null then '' else convert(varchar,A.ADJUST_VIOLATION_POINTS) end ADJUST_VIOLATION_POINTS                                     
                  
-- VIOLATION_DES                 
 FROM  APP_MVR_INFORMATION A                
  INNER JOIN MNT_VIOLATIONS M ON                
  A.VIOLATION_ID = M.VIOLATION_ID                
 WHERE                   
 A.CUSTOMER_ID=@CUSTOMER_ID AND                   
 A.APP_ID=@APP_ID AND                  
 A.APP_VERSION_ID=@APP_VERSION_ID AND                  
 A.DRIVER_ID=@DRIVER_ID AND                  
 A.APP_MVR_ID=@APP_MVR_ID    
                                               
ELSE      
 SELECT A.APP_MVR_ID,                
 A.CUSTOMER_ID,                                        
 A.APP_ID,                                        
 A.APP_VERSION_ID,                                        
 A.DRIVER_ID,                                    
 A.VIOLATION_ID,                                         
 A.MVR_AMOUNT,                                        
 A.MVR_DEATH,                                        
 convert(char,A.mvr_date,101) MVR_DATE,                          
 A.IS_ACTIVE,              
 A.VERIFIED,          
 A.VIOLATION_TYPE,                
 (isnull(M.MVR_DESCRIPTION,'') + ' (' + CAST(isnull(0,'') AS VARCHAR) +  ')' )AS VIOLATION_DES,                  
-- VIOLATION_DES                 
  convert(varchar, A.OCCURENCE_DATE,101) OCCURENCE_DATE,    
  A.DETAILS,  
 case when A.POINTS_ASSIGNED is null then '' else convert(varchar,A.POINTS_ASSIGNED) end POINTS_ASSIGNED, 
 case when A.ADJUST_VIOLATION_POINTS is null then '' else convert(varchar,A.ADJUST_VIOLATION_POINTS) end ADJUST_VIOLATION_POINTS
 FROM  APP_MVR_INFORMATION A                
  INNER JOIN MVR_EXCEPTION M ON                
  A.VIOLATION_ID = M.ID                
 WHERE                   
 A.CUSTOMER_ID=@CUSTOMER_ID AND                   
 A.APP_ID=@APP_ID AND                  
 A.APP_VERSION_ID=@APP_VERSION_ID AND                  
 A.DRIVER_ID=@DRIVER_ID AND       
 A.APP_MVR_ID=@APP_MVR_ID                                                    
      
      
      
      
      
END                          
                      
                      
      
    
  



GO

