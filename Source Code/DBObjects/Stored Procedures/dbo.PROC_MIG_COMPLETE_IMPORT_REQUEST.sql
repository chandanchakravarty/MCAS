IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_COMPLETE_IMPORT_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_COMPLETE_IMPORT_REQUEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                    
Proc Name             : Dbo.PROC_MIG_COMPLETE_IMPORT_REQUEST                                                    
Created by            : Santosh Kumar Gautam                                                   
Date                  : 06 May 2011                                                  
Purpose               : TO MARK COMPLETE IMPORT REQUEST    
Revison History       :                                                    
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD       
------------------------------------------------------------                                                    
Date     Review By          Comments                       
              
drop Proc PROC_MIG_COMPLETE_IMPORT_REQUEST                                           
------   ------------       -------------------------*/                                                    
--                       
                        
--                     
                  
CREATE PROCEDURE [dbo].[PROC_MIG_COMPLETE_IMPORT_REQUEST]              
 @IMPORT_REQUEST_ID        INT    
     
AS                        
BEGIN                 
            
            
    DECLARE @TOTAL_RECORDS INT =0    
    DECLARE @HAS_ERRORS CHAR(1) ='N'    
        
    -- IF IMPORT REQUEST HAS ANY ERRORED RECORD     
       IF   (SELECT COUNT(IMPORT_REQUEST_ID) FROM  MIG_CUSTOMER_POLICY_LIST      WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='Y')>0 
        OR  (SELECT COUNT(IMPORT_REQUEST_ID) FROM  MIG_POLICY_COVERAGES          WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='Y')>0   
        OR  (SELECT COUNT(IMPORT_REQUEST_ID) FROM  MIG_POLICY_INSTALLMENT_CANCEL WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='Y')>0
        OR  (SELECT COUNT(IMPORT_REQUEST_ID) FROM  MIG_CLAIM_DETAILS             WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='Y')>0
        OR  (SELECT COUNT(IMPORT_REQUEST_ID) FROM  MIG_PAID_CLAIM_DETAILS        WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND HAS_ERRORS='Y')>0
    BEGIN    
       SET @HAS_ERRORS='Y'    
    END        
        
           
     -- TO GET TOTAL RECORDS OF APPLICATION/POLICY IMPORT
     SELECT @TOTAL_RECORDS=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_CUSTOMER_POLICY_LIST     
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
        
      -- TO GET TOTAL RECORDS OF COERAGE IMPORTED  
     SELECT @TOTAL_RECORDS=@TOTAL_RECORDS+ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_POLICY_COVERAGES
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
      -- TO GET TOTAL RECORDS OF ISTALLMENT CANCELATION
     SELECT @TOTAL_RECORDS=@TOTAL_RECORDS+ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_POLICY_INSTALLMENT_CANCEL
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
      -- TO GET TOTAL RECORDS OF CLAIM DETAILS
     SELECT @TOTAL_RECORDS=@TOTAL_RECORDS+ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM   MIG_CLAIM_DETAILS
     WHERE  IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
      -- TO GET TOTAL RECORDS OF CLAIM PAYMENT DETAILS
     SELECT @TOTAL_RECORDS=@TOTAL_RECORDS+ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM   MIG_PAID_CLAIM_DETAILS
     WHERE  IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
           
            
    UPDATE  MIG_IMPORT_REQUEST    
    SET REQUEST_STATUS='COMP',    
       IS_PROCESSED='Y',    
       PROCESSED_DATE=GETDATE(),    
       IMPORT_RECORD_COUNT=@TOTAL_RECORDS   ,    
       HAS_ERRORS=@HAS_ERRORS        
    WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID          
           
END                        




GO

