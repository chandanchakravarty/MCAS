IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_IMPORT_POLICY_COVERAGE_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_POLICY_COVERAGE_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                              
Proc Name             : Dbo.PROC_MIG_INSERT_IMPORT_POLICY_COVERAGE_LIST                                                              
Created by            : Santosh Kumar Gautam                                                             
Date                  : 13 May 2011                                                            
Purpose               : TO INSERT POLICY COVERAGE DETAILS LIST  
Revison History       :                                                              
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                 
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc PROC_MIG_INSERT_IMPORT_POLICY_COVERAGE_LIST  138                                                   
------   ------------       -------------------------*/                                                              
--                                 
                                  
--                               
                            
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_POLICY_COVERAGE_LIST]      
@IMPORT_REQUEST_ID    INT    
AS                                  
BEGIN                           
      
      
  BEGIN TRY    
       
         
    DECLARE @COUNTER                    INT =1    
    DECLARE @TOTAL_RECORD_COUNT         INT =0    
    DECLARE @LEADER_POLICY_NUMBER       NVARCHAR(20) =0    
    DECLARE @HAS_ERROR                  INT =0    
    DECLARE @LEADER_ENDORSEMENT_NUMBER  INT    
        
     CREATE TABLE #TEMP_COVERAGE_LIST    
     (    
      ID INT  IDENTITY,        
      IMPORT_SERIAL_NO INT,    
      LEADER_POLICY_NUMBER NVARCHAR(20),    
      LEADER_ENDORSEMENT_NUMBER  INT    
     )    
        
        
    INSERT INTO #TEMP_COVERAGE_LIST    
     (              
      LEADER_POLICY_NUMBER,    
      LEADER_ENDORSEMENT_NUMBER    
     )    
     (    
      SELECT DISTINCT SUBSTRING(LEADER_POLICY_NUMBER,3,7),LEADER_ENDORSEMENT_NUMBER    
      FROM   MIG_POLICY_COVERAGES    
      WHERE  IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='N' AND IS_DELETED='N'        
     )              
         
  
         
     SELECT @TOTAL_RECORD_COUNT =COUNT(ID) FROM #TEMP_COVERAGE_LIST    
    
    
     ---------------------------------------    
     --- LOOP FOR INSERT POLICY COVERAGES     
     ---------------------------------------    
     WHILE(@COUNTER <=@TOTAL_RECORD_COUNT)    
     BEGIN    
         
        SET @HAS_ERROR=0    
            
        SELECT @LEADER_POLICY_NUMBER =LEADER_POLICY_NUMBER,    
               @LEADER_ENDORSEMENT_NUMBER=LEADER_ENDORSEMENT_NUMBER    
        FROM   #TEMP_COVERAGE_LIST    
        WHERE  ID =@COUNTER    
            
    --------------------------------------    
    --- VALIDATE POLICY COVERAGES    
    --------------------------------------    
    EXEC PROC_MIG_VALIDATE_COVERAGE_DATA    
    @IMPORT_REQUEST_ID         =@IMPORT_REQUEST_ID,                 
    @LEADER_POLICY_NUMBER      =@LEADER_POLICY_NUMBER,    
    @LEADER_ENDORSEMENT_NUMBER =@LEADER_ENDORSEMENT_NUMBER,    
    @HAS_ERRORS             =@HAS_ERROR OUT    
              
        IF(@HAS_ERROR=0)    
         BEGIN    
           
           
                --------------------------------------    
    --- INSERT POLICY COVERAGES    
    --------------------------------------    
    EXEC PROC_MIG_INSERT_IMPORT_POLICY_COVERAGES    
   @FILE_IMPORT_REQUEST_ID    =@IMPORT_REQUEST_ID,    
   @FILE_POLICY_NUMBER         =@LEADER_POLICY_NUMBER,    
   @FILE_ENDORSEMENT_NUMBER    =@LEADER_ENDORSEMENT_NUMBER    
        
         END    
             
               
        
        SET @COUNTER+=1    
            
        
     END    
         
         
         
     DROP TABLE #TEMP_COVERAGE_LIST    
         
     END TRY    
   BEGIN CATCH    
       
       
   END CATCH    
         
         
         
END 



GO

