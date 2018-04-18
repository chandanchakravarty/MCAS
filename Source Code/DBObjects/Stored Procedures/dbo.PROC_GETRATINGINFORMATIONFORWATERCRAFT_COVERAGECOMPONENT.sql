IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETRATINGINFORMATIONFORWATERCRAFT_COVERAGECOMPONENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETRATINGINFORMATIONFORWATERCRAFT_COVERAGECOMPONENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/* --------------------------------------------------------------------------------------------------                                                                                                      
PROC NAME                : DBO.PROC_GETRATINGINFORMATIONFORWATERCRAFT_COVERAGECOMPONENT                                                                                      
CREATED BY               : PARVEEN SINGH                                                                                                      
DATE                     : 02 JAN,2006                          
PURPOSE                  : TO GET THE COVERAGES FOR WATERCRAFT                                                                                                    
REVISON HISTORY          :                                                                                                      
USED IN                  : WOLVERINE                                                                                                      
----------------------------------------------------------------------------------------------------                                                                                                      
    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
*/    
                           
CREATE PROCEDURE DBO.PROC_GETRATINGINFORMATIONFORWATERCRAFT_COVERAGECOMPONENT                              
(                                
@CUSTOMER_ID   INT,                                
@APP_ID        INT,                                
@APP_VERSION_ID INT,                                
@BOAT_ID       INT                                
)                                
AS                                
BEGIN  --MAIN                                 
SET QUOTED_IDENTIFIER OFF                                 
DECLARE @RETURNVALUE VARCHAR(8000)              
                
                             
              
DECLARE @LOBID INT                                 
DECLARE @STATEID INT                                 
DECLARE @BOATTYPE VARCHAR(100)    
DECLARE  @C  VARCHAR(1000)                           
                            
                              
                             
                                
SELECT @LOBID=4, @STATEID=STATE_ID FROM APP_LIST                                 
WHERE CUSTOMER_ID =@CUSTOMER_ID AND APP_ID =@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                
                                
                              
SELECT @BOATTYPE=TYPE_OF_WATERCRAFT  
      FROM APP_WATERCRAFT_INFO                           
WHERE CUSTOMER_ID =@CUSTOMER_ID AND APP_ID =@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  AND BOAT_ID=@BOAT_ID                              
                               
SET @RETURNVALUE=''                 
SELECT @RETURNVALUE =  (CASE COV_CODE                 
                
WHEN 'EBSMECE'  THEN CASE ISNULL(APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  '<OP720>N</OP720>'  
                           ELSE         '<OP720>Y</OP720>' END                                   
WHEN 'EBSMWL'  THEN   CASE ISNULL(APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  '<OP900>N</OP900>'  
                           ELSE  '<OP900>Y</OP900>'  END            
WHEN 'EBSCEAV'  THEN  CASE ISNULL(APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  '<AV100>N</AV100>'  
                           ELSE         '<AV100>Y</AV100>'  END    
                              
 END ) + ISNULL(@RETURNVALUE,'')                
                              
 FROM  MNT_COVERAGE (NOLOCK)  LEFT OUTER JOIN  APP_WATERCRAFT_COVERAGE_INFO    (NOLOCK)    ON                            
 APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID  AND                              
 APP_WATERCRAFT_COVERAGE_INFO.CUSTOMER_ID = @CUSTOMER_ID  AND                               
 APP_WATERCRAFT_COVERAGE_INFO.APP_ID  = @APP_ID  AND                              
APP_WATERCRAFT_COVERAGE_INFO.APP_VERSION_ID  = @APP_VERSION_ID AND                                
 APP_WATERCRAFT_COVERAGE_INFO.BOAT_ID  = @BOAT_ID                
 WHERE                                     
 MNT_COVERAGE.STATE_ID  = @STATEID  AND                                 
 MNT_COVERAGE.LOB_ID = @LOBID                              
 AND COV_CODE IN ('EBSMECE','EBSMWL','EBSCEAV')                 
              
  
--GET THE DEDUCTIBLE  
                    
SELECT    @C =                                   
(CASE COV_CODE                                   
                 
/*WHEN 'EBPPDAV'  THEN  CASE ISNULL(APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  ''  
                           ELSE  CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+           
                                 CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                                   
WHEN 'EBPPDACV'  THEN   CASE ISNULL(APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  ''  
                           ELSE  CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+           
                                 CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END            
WHEN 'EBPPDJ'  THEN  CASE ISNULL(APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  ''  
                           ELSE  CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+           
                                 CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END  */                              
WHEN 'BDEDUC'  THEN  CASE ISNULL(APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID,0)  
                           WHEN 0 THEN  ''  
                           ELSE  CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE_1, 0) AS VARCHAR)+           
                                 CAST(ISNULL(APP_WATERCRAFT_COVERAGE_INFO.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR) END                                
 
 END ) + isnull(@C,'')  
                              
 FROM  MNT_COVERAGE (NOLOCK)  LEFT OUTER JOIN  APP_WATERCRAFT_COVERAGE_INFO    (NOLOCK)    ON                            
 APP_WATERCRAFT_COVERAGE_INFO.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID  AND                              
 APP_WATERCRAFT_COVERAGE_INFO.CUSTOMER_ID = @CUSTOMER_ID  AND                               
 APP_WATERCRAFT_COVERAGE_INFO.APP_ID  = @APP_ID  AND                              
APP_WATERCRAFT_COVERAGE_INFO.APP_VERSION_ID  = @APP_VERSION_ID AND                                
 APP_WATERCRAFT_COVERAGE_INFO.BOAT_ID  = @BOAT_ID                
 WHERE                                     
 MNT_COVERAGE.STATE_ID  = @STATEID  AND                                 
 MNT_COVERAGE.LOB_ID = @LOBID                              
 AND COV_CODE ='BDEDUC' /* IN ('EBPPDAV','EBPPDACV','EBPPDJ','BDEDUC')*/                             
  
 --Change the format from 100-1% to 1%-100  
  
  
 DECLARE @DA VARCHAR(100)  
 SET @DA=CASE CHARINDEX('-', @C)  
   
 WHEN 0 THEN  
 '<DEDUCTIBLE>' + @C +  '</DEDUCTIBLE>'  
 ELSE  
 '<DEDUCTIBLE>' +  SUBSTRING(@C ,CHARINDEX('-', @C) +1,2) + '-' +  SUBSTRING(@C ,0,CHARINDEX('-', @C)) + '</DEDUCTIBLE>'  
 END  
  
                    
  
  
  
  
                      
------  DEDUCTIBLE  END                       
------------------------------------------------------------------------------------------------                      
               
   SET @C = ISNULL(@RETURNVALUE,'') + ISNULL(@DA,'')  
                      
   SELECT  @C AS  COVERAGES                                  
END   ----END OF MAIN BEGIN                             
                          
                          
                        
                      
                    
                  
                
                
              
            
            
          
        
      
    
  





GO

