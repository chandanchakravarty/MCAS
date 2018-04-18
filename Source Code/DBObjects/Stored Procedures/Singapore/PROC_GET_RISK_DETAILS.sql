  
    
    
/*----------------------------------------------------------                                                      
 Proc Name       : [dbo].[PROC_GET_RISK_DETAILS]                               
 Created by      : praveer panghal             
 Date            : 15 Nov 2011                                     
 Purpose         : Stored procedure that returns the  data of Risk Table                                
 Revison History :                                                      
 Used In       : EbixAdvanatge             
             
 drop proc [dbo].[PROC_GET_RISK_DETAILS]  18,2100,33,1  
------   ------------       -------------------------*/                 
            
    
ALTER PROC dbo.[PROC_GET_RISK_DETAILS]        
(        
@LOB_ID INT  ,  
@CUSTOMER_ID INT  ,  
@POLICY_ID  INT ,  
@POLICY_VERSION_ID INT         
)        
AS     
DECLARE  @QUERY VARCHAR(MAX)  ,  
@DISPLAY_COLUMN_NAME VARCHAR(MAX),  
@TABLE_NAME VARCHAR(MAX),  
@COUNT INT=0,  
@COUNTER INT =1,  
@DESCRIPTION_RISK VARCHAR(500),  
@DISPLAY_COLUMN VARCHAR(MAX),  
@CONDITION VARCHAR(MAX)='+''-''+',  
@RISK_ID VARCHAR(100)  
  
BEGIN         
     SELECT @DISPLAY_COLUMN_NAME=DISPLAY_COLUMN_NAME ,@TABLE_NAME=TABLE_NAME ,@RISK_ID=RISK_ID FROM POL_RISK_DETAIL WITH(NOLOCK) WHERE LOB_ID=@LOB_ID   
  CREATE TABLE  #TEMP_RISK_DETAILS   
     (ID INT IDENTITY(1,1), DESCRIPTION_RISK VARCHAR(500))  
               
INSERT  INTO #TEMP_RISK_DETAILS  EXEC Proc_SplitText @DISPLAY_COLUMN_NAME,','  
  
SELECT @COUNT=COUNT(*) FROM #TEMP_RISK_DETAILS WITH(NOLOCK)  
  
  
WHILE(@COUNT>0 )  
 BEGIN  
 SELECT @DESCRIPTION_RISK=DESCRIPTION_RISK  FROM #TEMP_RISK_DETAILS WITH(NOLOCK) WHERE ID=@COUNTER  
 SET @COUNT=@COUNT-1  
 SET @COUNTER=@COUNTER+1  
  IF(@COUNT<>0)  
  BEGIN  
  SET @DISPLAY_COLUMN = ISNULL(@DISPLAY_COLUMN,'') + 'ISNULL('+'CAST('+@DESCRIPTION_RISK+' AS VARCHAR(500))'+','''')' + ISNULL(@CONDITION,'')  
  END  
  ELSE   
  BEGIN  
  SET @DISPLAY_COLUMN = ISNULL(@DISPLAY_COLUMN,'') + 'ISNULL('+'CAST('+@DESCRIPTION_RISK+' AS VARCHAR(500))'+','''')'  
  END  
  
 SET @QUERY = 'SELECT '+ISNULL(CAST(@RISK_ID AS VARCHAR(50)),0)+' AS RISK_ID, '+ CAST(@CUSTOMER_ID AS VARCHAR(20)) +' AS CUSTOMER_ID,'+ CAST(@POLICY_ID AS VARCHAR(20)) +' AS POLICY_ID,'  
 + CAST(@POLICY_VERSION_ID AS VARCHAR(20)) +' AS POLICY_VERSION_ID,(' +@DISPLAY_COLUMN+') AS DESCRIPTION  
   FROM ' +@TABLE_NAME+' WHERE RISK.CUSTOMER_ID= '+CAST(@CUSTOMER_ID AS VARCHAR(20))+'  
  AND RISK.POLICY_ID = '+ CAST(@POLICY_ID AS VARCHAR(20))+' AND RISK.POLICY_VERSION_ID= '+CAST(@POLICY_VERSION_ID AS VARCHAR(20))+'    
  AND RISK.IS_ACTIVE = ''Y'' '   
 END  
  
  
EXEC (@QUERY)  
DROP TABLE #TEMP_RISK_DETAILS  
 END       
   
  