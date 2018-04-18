IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_CHECK_UPLOADED_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_CHECK_UPLOADED_DATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------                                                                  
Proc Name             : Dbo.PROC_MIG_CHECK_UPLOADED_DATA                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 6 June 2011                                                                
Purpose               : TO CHECK UPLOADED DATA      
Revison History       :                                                                  
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD                     
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc PROC_MIG_CHECK_UPLOADED_DATA    220                                                  
------   ------------       -------------------------*/                                                                  
--                                     
                                      
--                                   
                                
CREATE PROCEDURE [dbo].[PROC_MIG_CHECK_UPLOADED_DATA]          
@IMPORT_REQUEST_ID    INT  ,
@FILE_MODE			  CHAR(4)

AS                                      
BEGIN                               
 
 --=======================================================================  
 --   FILE MODE MEANS FOR WHICH FILE TYPE THIS PROCESS BEING EXECUTED                      
 --     * APP  - ISSUANCE FILE (APPLICATION/POLICY DATA)    
 --     * COV  - COVERAGE FILE (COVERAGE DATA)                                                   
 --     * ISTC - INSTALMENT CANCELLATION FILE (INSTALMENT CANCELLATION DATA)                                                   
 --     * CLM  - FNOL FILE (CLAIM DATA)     
 --     * CLMP - CLAIM PAID FILE (CLAIM PAYMENT DATA)    
 --======================================================================= 
          
 SET NOCOUNT ON;    
      
   ------------------------------------------------------------------
   -- IF NO RECORD FOUND IT MEANS FILE IS NOT PROCESSED BY PACKAGE 
   -- CREATE A DEFAULT RECORD WITH ERROR
   -- ERROR TYPE :34 (File could not be processed becuase uploaded file has incorrect data.)
   ------------------------------------------------------------------
   IF (@FILE_MODE='APP')
    BEGIN
      
         -------------------------------------------------------------
         -- IF RECORD IS EXISTS THEN COPY ERROR RECORDS TO ERROR TABLE
         -------------------------------------------------------------
        IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_CUSTOMER_POLICY_LIST WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
          
           INSERT INTO [MIG_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    
			  ,ERROR_SOURCE_FILE
			  ,ERROR_SOURCE_COLUMN
			  ,ERROR_SOURCE_COLUMN_VALUE
			  ,ERROR_ROW_NUMBER               
			  ,[ERROR_DATETIME]  
			  ,[ERROR_MODE]      
			  ,ERROR_SOURCE_TYPE       
            )                 
            (      
              SELECT                          
			   @IMPORT_REQUEST_ID             
			  ,IMPORT_SERIAL_NO  
			  ,ERROR_TYPES    
			  ,ERROR_SOURCE_FILE          
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,'APP'     
			  FROM  MIG_CUSTOMER_POLICY_LIST
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS='Y'
            )       
          
		  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_CUSTOMER_POLICY_LIST
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_LOB,
				   POLICY_SUBLOB
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   'Y',
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = 'APP'    
	    END
		      
    END  
    ELSE IF (@FILE_MODE='COV')
    BEGIN
      
        -------------------------------------------------------------
         -- IF RECORD IS EXISTS THEN COPY ERROR RECORDS TO ERROR TABLE
        -------------------------------------------------------------
       IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_POLICY_COVERAGES WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
       BEGIN
           
           -------------------------------------------------------------
           -- DELETE HEADER AND FOOTER ROW
           -------------------------------------------------------------
          
           DELETE FROM MIG_POLICY_COVERAGES 
           WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND 
                 SEQUENCE_NO IN('000000','999999')
           
           
           SELECT * from MIG_POLICY_COVERAGES 
           WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID
       
		   INSERT INTO [MIG_IMPORT_ERROR_DETAILS]        
				(    
				  [IMPORT_REQUEST_ID]                 
				  ,[IMPORT_SERIAL_NO]   
				  ,[ERROR_TYPES]    
				  ,ERROR_SOURCE_FILE
				  ,ERROR_SOURCE_COLUMN
				  ,ERROR_SOURCE_COLUMN_VALUE
				  ,ERROR_ROW_NUMBER               
				  ,[ERROR_DATETIME]  
				  ,[ERROR_MODE]      
				  ,ERROR_SOURCE_TYPE       
				)                 
				(      
				  SELECT                          
				   @IMPORT_REQUEST_ID             
				  ,SERIAL_NO  
				  ,ERROR_TYPES    
				  ,ERROR_SOURCE_FILE          
				  ,ERROR_COLUMNS
				  ,ERROR_COLUMN_VALUES    
				  ,SERIAL_NO
				  ,GETDATE()
				  ,'SE'      -- SSIS ERROOR   
				  ,'COV'     
				  FROM  MIG_POLICY_COVERAGES
				  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
						HAS_ERRORS='Y'
				)       
       END
       ELSE
       BEGIN
		----------------------------------------
		-- INSERT DEFAULT RECORD
		----------------------------------------
			  INSERT INTO MIG_POLICY_COVERAGES
			  (
			   IMPORT_REQUEST_ID,
			   SERIAL_NO,
			   HAS_ERRORS,
			   PRODUCT_LOB,
			   SEQUENCE_NO
			  )
			  VALUES
			  (
			   @IMPORT_REQUEST_ID,
			   1,
			   'Y',
			   0,
			   0
			  )
		      
		----------------------------------------
		-- INSERT ERROR
		----------------------------------------      
		EXEC PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                 
			  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
			  @IMPORT_SERIAL_NO      = 1  ,        
			  @ERROR_SOURCE_FILE     = ''     ,    
			  @ERROR_SOURCE_COLUMN   = ''     ,    
			  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
			  @ERROR_ROW_NUMBER      = 1   ,      
			  @ERROR_TYPES           = 34,        
			  @ACTUAL_RECORD_DATA    = ''  ,    
			  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
			  @ERROR_SOURCE_TYPE     = 'COV'     
	 END 
		      
    END      
    ELSE IF (@FILE_MODE='ISTC') 
    BEGIN
      
      IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_POLICY_INSTALLMENT_CANCEL WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
      BEGIN
         INSERT INTO [MIG_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    
			  ,ERROR_SOURCE_FILE
			  ,ERROR_SOURCE_COLUMN
			  ,ERROR_SOURCE_COLUMN_VALUE
			  ,ERROR_ROW_NUMBER               
			  ,[ERROR_DATETIME]  
			  ,[ERROR_MODE]      
			  ,ERROR_SOURCE_TYPE       
            )                 
            (      
              SELECT                          
			   @IMPORT_REQUEST_ID       
			  ,IMPORT_SERIAL_NO  
			  ,ERROR_TYPES    
			  ,ERROR_SOURCE_FILE          
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,'ISTC'     
			  FROM  MIG_POLICY_INSTALLMENT_CANCEL
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS='Y'
            )       
      END
      ELSE
      BEGIN
      
		----------------------------------------
		-- INSERT DEFAULT RECORD
		----------------------------------------
			  INSERT INTO MIG_POLICY_INSTALLMENT_CANCEL
			  (
			   IMPORT_REQUEST_ID,
			   IMPORT_SERIAL_NO,
			   HAS_ERRORS,
			   PRODUCT_LOB
			  )
			  VALUES
			  (
			   @IMPORT_REQUEST_ID,
			   1,
			   'Y',
			   0
			  )
		      
		----------------------------------------
		-- INSERT ERROR
		----------------------------------------      
		EXEC PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                 
			  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
			  @IMPORT_SERIAL_NO      = 1  ,        
			  @ERROR_SOURCE_FILE     = ''     ,    
			  @ERROR_SOURCE_COLUMN   = ''     ,    
			  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
			  @ERROR_ROW_NUMBER      = 1   ,      
			  @ERROR_TYPES           = 34,        
			  @ACTUAL_RECORD_DATA    = ''  ,    
			  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
			  @ERROR_SOURCE_TYPE     = 'ISTC'    
	  
      END  
		      
    END         
    ELSE IF (@FILE_MODE='CLM')
    BEGIN
      
      IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_CLAIM_DETAILS WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
      BEGIN
         INSERT INTO [MIG_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    
			  ,ERROR_SOURCE_FILE
			  ,ERROR_SOURCE_COLUMN
			  ,ERROR_SOURCE_COLUMN_VALUE
			  ,ERROR_ROW_NUMBER               
			  ,[ERROR_DATETIME]  
			  ,[ERROR_MODE]      
			  ,ERROR_SOURCE_TYPE       
            )                 
            (      
              SELECT                          
			   @IMPORT_REQUEST_ID             
			  ,IMPORT_SERIAL_NO  
			  ,ERROR_TYPES    
			  ,ERROR_SOURCE_FILE          
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,'CLM'     
			  FROM  MIG_CLAIM_DETAILS
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS='Y'
            )       
      END
      ELSE
      BEGIN
      
		----------------------------------------
		-- INSERT DEFAULT RECORD
		----------------------------------------
			  INSERT INTO MIG_CLAIM_DETAILS
			  (
			   IMPORT_REQUEST_ID,
			   IMPORT_SERIAL_NO,
			   HAS_ERRORS,
			   PRODUCT_LOB
			  )
			  VALUES
			  (
			   @IMPORT_REQUEST_ID,
			   1,
			   'Y',
			   0
			  )
		      
		----------------------------------------
		-- INSERT ERROR
		----------------------------------------      
		EXEC PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                 
			  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
			  @IMPORT_SERIAL_NO      = 1  ,        
			  @ERROR_SOURCE_FILE     = ''     ,    
			  @ERROR_SOURCE_COLUMN   = ''     ,    
			  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
			  @ERROR_ROW_NUMBER      = 1   ,      
			  @ERROR_TYPES           = 34,        
			  @ACTUAL_RECORD_DATA    = ''  ,    
			  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
			  @ERROR_SOURCE_TYPE     = 'CLM'   
	  END   
		      
    END         
    ELSE IF (@FILE_MODE='CLMP') 
    BEGIN
      
      IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_PAID_CLAIM_DETAILS WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
      BEGIN
         INSERT INTO [MIG_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    
			  ,ERROR_SOURCE_FILE
			  ,ERROR_SOURCE_COLUMN
			  ,ERROR_SOURCE_COLUMN_VALUE
			  ,ERROR_ROW_NUMBER               
			  ,[ERROR_DATETIME]  
			  ,[ERROR_MODE]      
			  ,ERROR_SOURCE_TYPE       
            )                 
            (      
              SELECT                          
			   @IMPORT_REQUEST_ID             
			  ,IMPORT_SERIAL_NO  
			  ,ERROR_TYPES    
			  ,ERROR_SOURCE_FILE          
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,'CLMP'     
			  FROM  MIG_PAID_CLAIM_DETAILS
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS='Y'
            )       
      END
      ELSE
      BEGIN
      
		----------------------------------------
		-- INSERT DEFAULT RECORD
		----------------------------------------
			  INSERT INTO MIG_PAID_CLAIM_DETAILS
			  (
			   IMPORT_REQUEST_ID,
			   IMPORT_SERIAL_NO,
			   HAS_ERRORS,
			   PRODUCT_LOB
			  )
			  VALUES
			  (
			   @IMPORT_REQUEST_ID,
			   1,
			   'Y',
			   0
			  )
		      
		----------------------------------------
		-- INSERT ERROR
		----------------------------------------      
		EXEC PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                 
			  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
			  @IMPORT_SERIAL_NO      = 1  ,        
			  @ERROR_SOURCE_FILE     = ''     ,    
			  @ERROR_SOURCE_COLUMN   = ''     ,    
			  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
			  @ERROR_ROW_NUMBER      = 1   ,      
			  @ERROR_TYPES           = 34,        
			  @ACTUAL_RECORD_DATA    = ''  ,    
			  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
			  @ERROR_SOURCE_TYPE     = 'CLMP'      
	  END
		      
    END                 
   
             
             
             
END 












GO

