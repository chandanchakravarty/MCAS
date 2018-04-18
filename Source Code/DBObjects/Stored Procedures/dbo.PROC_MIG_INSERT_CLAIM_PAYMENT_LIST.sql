IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_CLAIM_PAYMENT_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_PAYMENT_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                                
Proc Name             : Dbo.PROC_MIG_INSERT_CLAIM_PAYMENT_LIST                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 13 May 2011                                                              
Purpose               : TO INSERT CLAIM PAYMENT DETAILS
Revison History       :                                                                
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                   
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc PROC_MIG_INSERT_CLAIM_PAYMENT_LIST                                                    
------   ------------       -------------------------*/                                                                
--                                   
                                    
--                                 
                              
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_PAYMENT_LIST]        
@IMPORT_REQUEST_ID    INT      
AS                                    
BEGIN                             
        
        
  BEGIN TRY      
         
           
    DECLARE @COUNTER           INT =1      
    DECLARE @TOTAL_RECORD_COUNT         INT =0      
    DECLARE @LEADER_CLAIM_NUMBER        NVARCHAR(20) =0      
    
          
     CREATE TABLE #TEMP_CLAIM_LIST      
     (      
		  ID INT  IDENTITY,          
		  LEADER_CLAIM_NUMBER NVARCHAR(20)
     )      
          
          
    INSERT INTO #TEMP_CLAIM_LIST      
     (                
       LEADER_CLAIM_NUMBER
     )      
     (      
      SELECT DISTINCT(LEADER_CLAIM_NUMBER )
      FROM   MIG_PAID_CLAIM_DETAILS      
      WHERE  IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='N' AND IS_DELETED='N'          
     )                
           
           
           
     SELECT @TOTAL_RECORD_COUNT =COUNT(ID) FROM #TEMP_CLAIM_LIST      
      
      
     ---------------------------------------      
     --- LOOP FOR INSERT CLAIM PAYMENT     
     ---------------------------------------      
     WHILE(@COUNTER <=@TOTAL_RECORD_COUNT)      
     BEGIN      
           
       
              
        SELECT @LEADER_CLAIM_NUMBER = LEADER_CLAIM_NUMBER
        FROM   #TEMP_CLAIM_LIST      
        WHERE  ID =@COUNTER      
           
            --------------------------------------      
			--- INSERT POLICY COVERAGES      
			--------------------------------------      
        EXEC PROC_MIG_INSERT_CLAIM_PAYMENT_DETAIL           
			 @IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID,	  
			 @LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER
			 
        SET @COUNTER+=1      
              
        
     END      
           
     DROP TABLE #TEMP_CLAIM_LIST      
           
     END TRY      
   BEGIN CATCH      
         
         
   END CATCH      
           
           
           
END 

GO

