 
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_CHECK_CUSTOMER_UPLOADED_DATA]    Script Date: 11/25/2011 12:08:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_CHECK_CUSTOMER_UPLOADED_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_CHECK_CUSTOMER_UPLOADED_DATA]
GO

 
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_CHECK_CUSTOMER_UPLOADED_DATA]    Script Date: 11/25/2011 12:08:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------                                                                  
Proc Name             : Dbo.[PROC_MIG_IL_CHECK_CUSTOMER_UPLOADED_DATA]                                                                  
Created by            : Santosh Kumar Gautam                                                                 
Date                  : 18 Aug 2011                                                         
Purpose               : TO CHECK UPLOADED DATA      
Revison History       :                                                                  
Used In               : INITIAL LOAD                     
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc [PROC_MIG_IL_CHECK_CUSTOMER_UPLOADED_DATA]    37,'CAPP'                                                  
------   ------------       -------------------------*/                                                                  
--                                     
                                      
--                                   
                                
CREATE PROCEDURE [dbo].[PROC_MIG_IL_CHECK_CUSTOMER_UPLOADED_DATA]          
@IMPORT_REQUEST_ID    INT  ,
@FILE_MODE			  CHAR(4)

AS                                      
BEGIN                               
 
 --=======================================================================  
 --   FILE MODE MEANS FOR WHICH FILE TYPE THIS PROCESS BEING EXECUTED                      
 --     * CUST  - CUSTOMER FILE
 --     * CAPP   - COAPPLICANT  FILE                             
 --     * CONT  - CONTACT FILE
 --======================================================================= 
          
 SET NOCOUNT ON;    
      
   ------------------------------------------------------------------
   -- IF NO RECORD FOUND IT MEANS FILE IS NOT PROCESSED BY PACKAGE 
   -- CREATE A DEFAULT RECORD WITH ERROR
   -- ERROR TYPE :34 (File could not be processed becuase uploaded file has incorrect data.)
   ------------------------------------------------------------------
   IF (@FILE_MODE='CUST')
    BEGIN
      
         -------------------------------------------------------------
         -- IF RECORD IS EXISTS THEN COPY ERROR RECORDS TO ERROR TABLE
         -------------------------------------------------------------
     IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_CUSTOMER_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
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
			  ,'CUST'     
			  FROM  MIG_IL_CUSTOMER_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
          
		  
	    END  
      ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_CUSTOMER_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   CUSTOMER_CODE,
				   CPF_CNPJ
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
				  @ERROR_SOURCE_TYPE     = 'CUST'    
	    END
     END
    
    ELSE IF (@FILE_MODE='CAPP')	     
    BEGIN 
	--===================================================
	--	FOR COAPPLICANT
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_CO_APPLICANT_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
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
			  ,'CAPP'     
			  FROM  MIG_IL_CO_APPLICANT_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
          
		  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_CO_APPLICANT_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   CUSTOMER_CODE,
				   CPF_CNPJ
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
				  @ERROR_SOURCE_TYPE     = 'CAPP'    
	    END
	    
    END  
    ELSE IF (@FILE_MODE='CONT')	     
    BEGIN 
	--===================================================
	--	FOR CONTACT
	--===================================================	      
	 IF EXISTS(SELECT IMPORT_REQUEST_ID FROM MIG_IL_CONTACT_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID)
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
			  ,'CONT'     
			  FROM  MIG_IL_CONTACT_DETAILS WITH(NOLOCK)
			  WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND   
			        HAS_ERRORS=1
            )       
          
		  
	    END  
	    ELSE
	    BEGIN
	     	----------------------------------------
			-- INSERT DEFAULT RECORD
			----------------------------------------
				  INSERT INTO MIG_IL_CONTACT_DETAILS
				  (
				   IMPORT_REQUEST_ID,
				   IMPORT_SERIAL_NO,
				   HAS_ERRORS,
				   CONTACT_CODE,
				   CPF_CNPJ
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
				  @ERROR_SOURCE_TYPE     = 'CONT'    
	    END
	    
    END  
    
    
  
             
END 
 
GO


