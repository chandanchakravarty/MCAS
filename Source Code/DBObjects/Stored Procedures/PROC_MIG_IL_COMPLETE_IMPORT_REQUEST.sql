
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_COMPLETE_IMPORT_REQUEST]    Script Date: 09/02/2011 15:38:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_COMPLETE_IMPORT_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_COMPLETE_IMPORT_REQUEST]
GO


GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_COMPLETE_IMPORT_REQUEST]    Script Date: 09/02/2011 15:38:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                    
Proc Name             : Dbo.[PROC_MIG_IL_COMPLETE_IMPORT_REQUEST]                                                    
Created by            : Santosh Kumar Gautam                                                   
Date                  : 19 Aug 2011                                            
Purpose               : TO MARK COMPLETE IMPORT REQUEST    
Revison History       :                                                    
Used In               : INITIAL LOAD       
------------------------------------------------------------                                                    
Date     Review By          Comments                       
              
drop Proc [PROC_MIG_IL_COMPLETE_IMPORT_REQUEST]                                           
------   ------------       -------------------------*/                                                    
--                       
                        
--                     
                  
CREATE PROCEDURE [dbo].[PROC_MIG_IL_COMPLETE_IMPORT_REQUEST]              
 @IMPORT_REQUEST_ID        INT    
     
AS                        
BEGIN                 
            
            
    DECLARE @TOTAL_RECORDS INT =0    
    DECLARE @FAILED_RECORDS INT =0    
    DECLARE @HAS_ERRORS CHAR(1) ='N'    
            
      
	 ------------------------------------      
	 -- GET CUSTOMER DETAILS
	 ------------------------------------           
     SELECT @TOTAL_RECORDS=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CUSTOMER_DETAILS  WITH(NOLOCK) 
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
     SELECT @FAILED_RECORDS = ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CUSTOMER_DETAILS    WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND
           HAS_ERRORS=1
     
      
     ------------------------------------      
	 -- GET CO-APPLICANT DETAILS
	 ------------------------------------           
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CO_APPLICANT_DETAILS  WITH(NOLOCK) 
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CO_APPLICANT_DETAILS   WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND
           HAS_ERRORS=1  
           
     ------------------------------------      
	 -- GET CONTACT DETAILS
	 ------------------------------------           
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CONTACT_DETAILS  WITH(NOLOCK) 
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CONTACT_DETAILS WITH(NOLOCK)  
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND
           HAS_ERRORS=1  

	 ----------------------------------- 
      --   ADDED BY ATUL KUMAR SINGH 02-09-2011
      --   START 
      ------------------------------------
          ------------------------------------        
	  -- GET CONTACT DETAILS  
      ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1         
           
       -- END 



           
     IF(@FAILED_RECORDS>0)  
      SET @HAS_ERRORS='Y'    
   
     
    
            
    UPDATE  MIG_IL_IMPORT_REQUEST    
    SET REQUEST_STATUS		  = 'COMP',    
       IS_PROCESSED			  = 'Y',    
       FAILED_RECORD_COUNT    = @FAILED_RECORDS,
       SUCCEDDED_RECORD_COUNT = ISNULL(@TOTAL_RECORDS-@FAILED_RECORDS,0),
       PROCESSED_DATE		  = GETDATE(),    
       IMPORT_RECORD_COUNT	  = @TOTAL_RECORDS   ,    
       HAS_ERRORS			  = @HAS_ERRORS        
    WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID          
           
END                        









GO


