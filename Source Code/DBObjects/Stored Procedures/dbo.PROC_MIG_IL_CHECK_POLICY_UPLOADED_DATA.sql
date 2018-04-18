
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]    Script Date: 12/02/2011 16:43:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]
GO
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]    Script Date: 12/02/2011 16:43:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                  
Proc Name             : Dbo.[PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 15 Sep 2011                                                         
Purpose               : TO CHECK UPLOADED DATA      
Revison History       :                                                                  
Used In               : INITIAL LOAD                     
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc [PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]    37,'CAPP'                                                  
------   ------------       -------------------------*/                                                                  
                              
CREATE PROCEDURE [dbo].[PROC_MIG_IL_CHECK_POLICY_UPLOADED_DATA]          
@IMPORT_REQUEST_ID    INT  ,
@FILE_MODE			  CHAR(4)

AS                                      
BEGIN                               
 
 --=======================================================================  
 --   FILE MODE MEANS FOR WHICH FILE TYPE THIS PROCESS BEING EXECUTED                      
 --     * POLC   - POLICY FILE
 --     * LOCA   - LOCATION  FILE                             
 --     * REMU   - REMUNARRATION FILE
 --======================================================================= 
          
 SET NOCOUNT ON;    
      
 DECLARE @RECORD_NOT_EXISTS INT =0
   ------------------------------------------------------------------
   -- IF NO RECORD FOUND IT MEANS FILE IS NOT PROCESSED BY PACKAGE 
   -- CREATE A DEFAULT RECORD WITH ERROR
   -- ERROR TYPE :34 (File could not be processed becuase uploaded file has incorrect data.)
   ------------------------------------------------------------------
   IF (@FILE_MODE='POLC')
    BEGIN
      
         -------------------------------------------------------------
         -- IF RECORD IS EXISTS THEN COPY ERROR RECORDS TO ERROR TABLE
         -------------------------------------------------------------
     IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
        INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            
	    END  
      ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   CUSTOMER_CODE,
				   POLICY_NUMBER
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   '0',
				   '0'
				  )
			     
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE    
	    END
     END
    
    ELSE IF (@FILE_MODE='LOCA')	     
    BEGIN 
	--===================================================
	--	FOR LOCATION
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_LOCATION_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_LOCATION_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            
		  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_LOCATION_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENCE_NO,
				   LOCATION_CODE
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   '0'
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
    ELSE IF (@FILE_MODE='COIS')	     
    BEGIN 
	--===================================================
	--	FOR COINSURANCE
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_COINSURER_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_COINSURER_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_COINSURER_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
    ELSE IF (@FILE_MODE='REIS')	     
    BEGIN 
	--===================================================
	--	FOR REINSURANCE
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_REINSURANCE_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_REINSURANCE_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_REINSURANCE_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
    ELSE IF (@FILE_MODE='REMU')	     
    BEGIN 
	--===================================================
	--	FOR REMUNARATION
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_REMUNERATION_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_REMUNERATION_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_REMUNERATION_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENCE_NO,
				   END_SEQUENCE_NO,
				   REMUNERATION_SEQUENCE_NO
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
    ELSE IF (@FILE_MODE='CLSS')	     
    BEGIN 
	--===================================================
	--	FOR CLAUSES
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_CLAUSES_DETAILS
				  (
				   IMPORT_REQUEST_ID,      
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL,
				   CLAUSE_SEQUENTIAL,
				 CLAUSE_CODE
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
      ELSE IF (@FILE_MODE='PAPP')	     
    BEGIN 
	--===================================================
	--	FOR POLICY CO-APPLICANT
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_COAPPLICANT_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_COAPPLICANT_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_COAPPLICANT_DETAILS
				  (
				   IMPORT_REQUEST_ID,      
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENCE_NUMBER,
				  ENDORSEMNET_SEQUENCE_NUMBER,
				   COAPPLICANT_SEQUENCE,
				   CO_APPLICANT_CODE
				 
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,
				   0,
				   ''
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
      ELSE IF (@FILE_MODE='PDSC')	     
    BEGIN 
	--===================================================
	--	FOR POLICY DSICOUNT
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_DISCOUNTS_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_DISCOUNTS_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_DISCOUNTS_DETAILS
				  (
				   IMPORT_REQUEST_ID,      
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL,
				   POLICY_DISCOUNT_SURCHARGE_SEQUENTIAL,
				  [TYPE]
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE     = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
      ELSE IF (@FILE_MODE='RDSC')	     
    BEGIN 
	--===================================================
	--	FOR POLICY RISK DISCOUNT
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS
				  (
				   IMPORT_REQUEST_ID,      
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL,
				   DISCOUNT_SURCHARGE_RISK_SEQUENTIAL,
				   RISK_LOCATION_SEQUENTIAL
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
    ELSE IF (@FILE_MODE='PRSK')	     
    BEGIN 
	--===================================================
	--	FOR POLICY RISK DETAILS
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_RISK_DETAILS
				  (
				   IMPORT_REQUEST_ID,      
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENCE_NO,
				   END_SEQUENCE_NO,
				   RISK_SEQUENCE_NO
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE   = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
    ELSE IF (@FILE_MODE='PCOV')	     
    BEGIN 
	--===================================================
	--	FOR POLICY COVERAGE DETAILS
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_RISK_COVERAGES_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_RISK_COVERAGES_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_RISK_COVERAGES_DETAILS
				  (
				   IMPORT_REQUEST_ID,       
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL,
				   RISK_LOCATION_SEQUENTIAL,
				   COVERAGE_SEQUENTIAL
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE   = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
     ELSE IF (@FILE_MODE='BILL')	     
    BEGIN 
	--===================================================
	--	FOR POLICY BILLING DETAILS
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_BILLING_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_BILLING_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_BILLING_DETAILS
				  (
				   IMPORT_REQUEST_ID,       
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL,				   
				   INSTALLMENT_SEQUENTIAL
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,				
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE   = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
     ELSE IF (@FILE_MODE='PBEN')	     
    BEGIN 
	--===================================================
	--	FOR POLICY BENEFICIARY DETAILS
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
        BEGIN
        
           INSERT INTO [MIG_IL_IMPORT_ERROR_DETAILS]        
            (    
			  [IMPORT_REQUEST_ID]                 
			  ,[IMPORT_SERIAL_NO]   
			  ,[ERROR_TYPES]    			
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
			  ,ERROR_COLUMNS
			  ,ERROR_COLUMN_VALUES    
			  ,IMPORT_SERIAL_NO
			  ,GETDATE()
			  ,'SE'      -- SSIS ERROOR   
			  ,@FILE_MODE    
			  FROM  MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
            	  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS
				  (
				   IMPORT_REQUEST_ID,       
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   POLICY_SEQUENTIAL,
				   ENDORSEMENT_SEQUENTIAL,				   
				   RISK_LOCATION_SEQUENTIAL,
				   BENEFICIARY_SEQUENTIAL
				  )
				  VALUES
				  (
				   @IMPORT_REQUEST_ID,
				   1,
				   1,
				   0,
				   0,				
				   0,
				   0
				  )
			      
			----------------------------------------
			-- INSERT ERROR
			----------------------------------------      
			EXEC  PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS                 
				  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,    
				  @IMPORT_SERIAL_NO      = 1  ,        
				  @ERROR_SOURCE_FILE   = ''     ,    
				  @ERROR_SOURCE_COLUMN   = ''     ,    
				  @ERROR_SOURCE_COLUMN_VALUE= '' ,    
				  @ERROR_ROW_NUMBER      = 1   ,      
				  @ERROR_TYPES           = 34,        
				  @ACTUAL_RECORD_DATA    = ''  ,    
				  @ERROR_MODE            = 'VE',  -- VALIDATION ERROR     
				  @ERROR_SOURCE_TYPE     = @FILE_MODE  
	    END
	    
    END  
    
             
END 

 
GO