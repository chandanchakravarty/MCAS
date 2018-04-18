IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_IMPORT_POLICY_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_POLICY_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.PROC_MIG_INSERT_IMPORT_POLICY_LIST                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 12 May 2011                                                          
Purpose               : TO INSERT POLICY DETAILS LIST  
Revison History       :                                                            
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc PROC_MIG_INSERT_IMPORT_POLICY_LIST     4                                              
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
  
                          
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_POLICY_LIST]    
@IMPORT_REQUEST_ID    INT  
AS                                
BEGIN    
  
  
 BEGIN TRY                       
      
    DECLARE @COUNTER   INT =1  
    DECLARE @TOTAL_RECORD_COUNT INT =0  
    DECLARE @IMPORT_SERIAL_NO   INT =0  
    DECLARE @HAS_ERROR          INT =0  
      
     CREATE TABLE #TEMP_POLICY_LIST  
     (  
      ID INT  IDENTITY,      
      IMPORT_SERIAL_NO INT  
     )  
      
      
    INSERT INTO #TEMP_POLICY_LIST  
     (       
      IMPORT_SERIAL_NO  
     )  
     (  
      SELECT IMPORT_SERIAL_NO  
   FROM   MIG_CUSTOMER_POLICY_LIST      
   WHERE  IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='N'       
     )            
       
  
       
     SELECT @TOTAL_RECORD_COUNT=COUNT(ID) FROM #TEMP_POLICY_LIST  
      
     -----------------------------  
     --- LOOP FOR INSERT POLICY   
     -----------------------------  
     WHILE(@COUNTER <=@TOTAL_RECORD_COUNT)  
     BEGIN  
       
        SET @HAS_ERROR=0  
          
        SELECT @IMPORT_SERIAL_NO =IMPORT_SERIAL_NO  
        FROM   #TEMP_POLICY_LIST  
        WHERE  ID =@COUNTER  
          
         -----------------------------  
   --- VALIDATE POLICY DATA  
   -----------------------------  
        EXEC PROC_MIG_VALIDATE_POLICY_DATA  
        @IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID,               
  @IMPORT_SERIAL_NO  =@IMPORT_SERIAL_NO,  
  @HAS_ERRORS     =@HAS_ERROR OUT  
          
        select @HAS_ERROR as '@HAS_ERROR'  
        IF(@HAS_ERROR=0)  
         BEGIN  
           
    -----------------------------  
    --- INSERT POLICY   
    -----------------------------  
           EXEC PROC_MIG_INSERT_POLICY_DETAIL          
    @INPUT_REQUEST_ID =@IMPORT_REQUEST_ID,  
    @INPUT_SERIAL_ID  =@IMPORT_SERIAL_NO  
      
         END  
           
         SET @COUNTER+=1  
           
          
     END  
       
     DROP TABLE #TEMP_POLICY_LIST  
       
END TRY  
BEGIN CATCH  
  
END CATCH  
       
       
END 

GO

