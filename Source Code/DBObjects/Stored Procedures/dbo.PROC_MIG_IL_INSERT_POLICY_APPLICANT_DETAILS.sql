
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_APPLICANT_DETAILS]    Script Date: 12/02/2011 15:59:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_APPLICANT_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_APPLICANT_DETAILS]
GO

 
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_APPLICANT_DETAILS]    Script Date: 12/02/2011 15:59:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                  
Proc Name             : [PROC_MIG_IL_INSERT_POLICY_APPLICANT_DETAILS]                                                  
Created by            : ATUL KUMAR SINGH                                                              
Date                  : 30-SEPT 2011                                                            
Purpose               : Insert Policy APPLICANT    
Revison History       :                                                                  
Used In               : INITIAL LOAD                     
------------------------------------------------------------                                                                  
Date     Review By          Comments                                     
                            
drop Proc [PROC_MIG_IL_INSERT_POLICY_APPLICANT_DETAILS]   422                                                     
------   ------------       -------------------------*/          
CREATE  PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_APPLICANT_DETAILS]         
        
--------------------------------- INPUT PARAMETER        
@IMPORT_REQUEST_ID  INT        
-------------------------------------------------        
          
AS        
BEGIN        
         
-------------------------------- DECLARATION PART        
----------------------------------------------------------------------------------------        
DECLARE @ERROR_NUMBER    INT          
DECLARE @ERROR_SEVERITY  INT          
DECLARE @ERROR_STATE     INT          
DECLARE @ERROR_PROCEDURE VARCHAR(512)          
DECLARE @ERROR_LINE    INT          
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)          
        
DECLARE @is_primary_applicant int=0
DECLARE @CUSTOMER_CODE VARCHAR(20)        
DECLARE @LOB_ID INT  
DECLARE @CUSTOMER_ID INT        
DECLARE @POLICY_ID INT        
DECLARE @POLICY_VERSION_ID INT        
DECLARE @POLICY_NUMBER NVARCHAR(21)        
DECLARE @ENDORSEMENT_NUMBER INT        
        
    
        
DECLARE @LOOP_COAPPLICANT_CODE VARCHAR(50)    
DECLARE @LOOP_POLICY_SEQUANCE_NO INT            
DECLARE @LOOP_END_SEQUANCE_NO INT      
DECLARE @LOOP_COAPPLICANT_SEQUANCE_NO INT         
DECLARE @LOOP_IMPORT_SERIAL_NO INT            
DECLARE @COUNTER INT  =1          
DECLARE @MAX_RECORD_COUNT INT           
DECLARE @ERROR_NO INT=0          
DECLARE @IMPORT_SERIAL_NO INT      
    
DECLARE @APPLICANT_ID INT      

  
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)         
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE      
DECLARE @IMPORT_COAPPLICANT_FILE_TYPE INT = 14961 -- FOR POLICY APPLICANT FILE TYPE     

DECLARE @LOOP_COMMISION   DECIMAL(18,2)=0.00  
DECLARE @LOOP_FEES   DECIMAL(18,2)=0.00  
DECLARE @LOOP_PRO_LOBORE   DECIMAL(18,2)=0.00  
DECLARE @TRANSACTION_TYPE INT         
DECLARE @PROCESS_TYPE INT    --- Change   
BEGIN TRY        
        
          
     CREATE TABLE #TEMP_POLICY_APPLICANT          
     (          
		ID INT IDENTITY(1,1),          
		POLICY_SEQUANCE_NO INT NULL,         
		END_SEQUANCE_NO INT NULL,        
		IMPORT_SERIAL_NO BIGINT NULL,  
		COAPPLICANT_SEQUENCE BIGINT NULL,   
		COAPPLICANT_CODE VARCHAR(50)  NULL,  
		COMMISSION DECIMAL(18,2) NULL,  
		FEES DECIMAL(18,2) NULL,  
		PRO_LOBORE DECIMAL(18,2) NULL  
     )          
        
        
        
  INSERT INTO #TEMP_POLICY_APPLICANT          
     (          
		POLICY_SEQUANCE_NO ,          
		END_SEQUANCE_NO ,         
		IMPORT_SERIAL_NO,          
		COAPPLICANT_SEQUENCE,      
		COAPPLICANT_CODE,  
		COMMISSION ,  
		FEES,  
		PRO_LOBORE  
     )          
     (          
      SELECT   
		  POLICY_SEQUENCE_NUMBER,        
		  ENDORSEMNET_SEQUENCE_NUMBER,        
		  IMPORT_SERIAL_NO,        
		  COAPPLICANT_SEQUENCE,      
		  CO_APPLICANT_CODE,  
		  COMMISION,  
		  FEES,  
		  PRO_LOBORE                    
      FROM MIG_IL_POLICY_COAPPLICANT_DETAILS WITH(NOLOCK)          
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0          
     )          
  
  --select * from #TEMP_POLICY_APPLICANT    
      
   ------------------------------------                
   -- GET MAX RECOUNT COUNT          
   ------------------------------------             
    SELECT @MAX_RECORD_COUNT = COUNT(ID)           
    FROM   #TEMP_POLICY_APPLICANT           
        
   ------------------------------------            
   -- GET FILE NAME      
   ------------------------------------         
   IF(@MAX_RECORD_COUNT>0)      
   BEGIN      
         
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)       
    FROM  MIG_IL_IMPORT_REQUEST_FILES      
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND      
          IMPORT_FILE_TYPE   = @IMPORT_COAPPLICANT_FILE_TYPE        
         
   END      
     
         
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)          
  BEGIN 
		--CODE ADDED BY ANKIT-----       
		 SET @is_primary_applicant   =0
		 ----------------------
		 SET @ERROR_NO=0         
		 SET @APPLICANT_ID=0    
		 SET @CUSTOMER_ID=0      
		 SET @POLICY_ID=0      
		 SET @POLICY_VERSION_ID=0      
		 SET @TRANSACTION_TYPE=0     
		 SET @PROCESS_TYPE=0 
      SELECT       
		   @IMPORT_SERIAL_NO            = IMPORT_SERIAL_NO ,          
		   @LOOP_POLICY_SEQUANCE_NO     = POLICY_SEQUANCE_NO  ,        
		   @LOOP_END_SEQUANCE_NO        = END_SEQUANCE_NO,          
		   @LOOP_COAPPLICANT_CODE       = COAPPLICANT_CODE,  
		   @LOOP_COMMISION              = COMMISSION,  
		   @LOOP_FEES				    = FEES,  
		   @LOOP_PRO_LOBORE             = PRO_LOBORE  ,  
		   @LOOP_COAPPLICANT_SEQUANCE_NO = COAPPLICANT_SEQUENCE  
     FROM   #TEMP_POLICY_APPLICANT (NOLOCK) WHERE ID   = @COUNTER             
        
          
   -------------------------------------------------------            
   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID      
   -------------------------------------------------------       
   SELECT @CUSTOMER_ID		 = CUSTOMER_ID ,      
          @POLICY_ID		 = POLICY_ID,      
          @POLICY_VERSION_ID = POLICY_VERSION_ID ,  
          @LOB_ID            = LOB_ID     ,
          @PROCESS_TYPE		 = PROCESS_TYPE	
   FROM   MIG_IL_IMPORT_SUMMARY      
   WHERE  POLICY_SEQUENTIAL   = @LOOP_POLICY_SEQUANCE_NO AND      
    ENDORSEMENT_SEQUENTIAL	  = @LOOP_END_SEQUANCE_NO    AND      
          FILE_TYPE           = @IMPORT_POLICY_FILE_TYPE AND      
          [FILE_NAME]         = @IMPORT_FILE_NAME        AND      
          IS_ACTIVE			  = 'Y'      
       
  
   -------------------------------------------------------------------    
   --   FIND APPLICANT ID FROM APPLICANT CODE    
   -------------------------------------------------------------------    
       
   SELECT @APPLICANT_ID=APPLICANT_ID FROM CLT_APPLICANT_LIST (NOLOCK) WHERE 
   --CUSTOMER_ID=@CUSTOMER_ID AND 
   CONTACT_CODE=@LOOP_COAPPLICANT_CODE    
          
   -------------------------------------------------------            
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT      
   -------------------------------------------------------       
    IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)      
        SET @ERROR_NO =53 -- Application/Policy does not exists      
          
    ELSE IF (@APPLICANT_ID IS NULL OR @APPLICANT_ID='' OR @APPLICANT_ID=0)      
        SET @ERROR_NO =51 -- Application/Policy does not exists      
   -----------------------------------------------------------            
   -- CHECK WHETHER APPLICANT IS ALREADY EXISTS FOR SAME POLICY  VERSION    
   -----------------------------------------------------------       
     ELSE IF  EXISTS(SELECT 1 FROM POL_APPLICANT_LIST WITH(NOLOCK)     
   WHERE      
    CUSTOMER_ID=@CUSTOMER_ID AND    
    POLICY_ID =@POLICY_ID  AND    
    POLICY_VERSION_ID=@POLICY_VERSION_ID AND    
    APPLICANT_ID=@APPLICANT_ID      
    )  BEGIN    
  SET @ERROR_NO =49 --  Co-applicant already exists.  
       END  
     ELSE IF(@LOB_ID  IN (21,33) AND (@LOOP_PRO_LOBORE IS  NULL OR @LOOP_PRO_LOBORE<0.00))-- Applies only to products 0982 and 0977  
     BEGIN  
  SET @ERROR_NO=107  
     END  
     ELSE IF(@LOB_ID  IN (21,33) AND (@LOOP_FEES IS NULL OR @LOOP_FEES<0.00)) --Applies only to products 0982 and 0977  
     BEGIN  
  SET @ERROR_NO=108  
     END  
     ELSE IF(@LOB_ID  IN (21,33,34,18,17) AND (@LOOP_COMMISION IS NULL OR @LOOP_COMMISION<0.00))--Applies only to products 0982, 0977, 0993, 0523 and 0553  
     BEGIN  
  SET @ERROR_NO=109  
     END  
    --0982, 0977, 0993, 0523 and 0553  
    SELECT @TRANSACTION_TYPE=TRANSACTION_TYPE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
    
 
    IF(@ERROR_NO>0)     
       BEGIN      
             
             
        -----------------------------------------------------------            
  -- INSERT ERROR DETAILS      
  -----------------------------------------------------------       
     UPDATE MIG_IL_POLICY_COAPPLICANT_DETAILS    
     SET    HAS_ERRORS=1            
     WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND       
            IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO            
            
    EXEC  [PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS]                       
     @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,            
     @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,                
     @ERROR_SOURCE_FILE     = ''     ,            
     @ERROR_SOURCE_COLUMN   = ''     ,            
     @ERROR_SOURCE_COLUMN_VALUE= '' ,            
     @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO   ,              
     @ERROR_TYPES           = @ERROR_NO,         
     @ACTUAL_RECORD_DATA    = '' ,            
     @ERROR_MODE            = 'VE',  -- VALIDATION ERROR            
     @ERROR_SOURCE_TYPE     = 'PAPP'         
           
      
       END       
    ELSE      
       BEGIN 
     ----CODE ADDED BY ANKIT SET PRIMARY APPLICANT VARIABLE----   
    DECLARE @CUST_CONTACT_CODE VARCHAR(100)
    SELECT @CUST_CONTACT_CODE=CUSTOMER_CODE FROM CLT_CUSTOMER_LIST CAL2 WITH(NOLOCK) WHERE 
			CAL2.CUSTOMER_ID = @CUSTOMER_ID       
     
 
	
	IF 	(@CUST_CONTACT_CODE =  @LOOP_COAPPLICANT_CODE)
	BEGIN
		SET @IS_PRIMARY_APPLICANT =1
	END
	
    IF(@PROCESS_TYPE =1)--FOR NBS
    BEGIN
   
    ------------------------------------------------------  
    -- INSERT POLICY APPLICANT  
    ------------------------------------------------------     

	  INSERT INTO [dbo].[POL_APPLICANT_LIST]    
			   ([POLICY_ID]               
			   ,[POLICY_VERSION_ID]    
			   ,[CUSTOMER_ID]    
			   ,[APPLICANT_ID]    
			   ,[CREATED_BY]    
			   ,[MODIFIED_BY]    
			   ,[CREATED_DATETIME]    
			   ,[LAST_UPDATED_TIME]    
			   ,[IS_PRIMARY_APPLICANT]    
			   ,[COMMISSION_PERCENT]    
			   ,[FEES_PERCENT]    
			   ,[PRO_LABORE_PERCENT]             
			   )    
	  VALUES  
	   (      
		  @POLICY_ID    
		 ,@POLICY_VERSION_ID    
		 ,@CUSTOMER_ID  
		 ,@APPLICANT_ID    
		 ,dbo.fun_GetDefaultUserID()    
		 ,NULL    
		 ,GETDATE()    
		 ,NULL          
		 ,@is_primary_applicant   
		 ,CASE WHEN @TRANSACTION_TYPE=14560 THEN @LOOP_COMMISION ELSE 0 END   
		 ,CASE WHEN @TRANSACTION_TYPE=14560 THEN @LOOP_FEES ELSE 0 END      
		 ,CASE WHEN @TRANSACTION_TYPE=14560 THEN @LOOP_PRO_LOBORE ELSE 0 END         
		)  
            
   END
	--========================================================
	-- FOR POLICY ENDORSEMENT PROCESS -- ADDED BY PRADEEP 
	--========================================================

   ELSE IF(@PROCESS_TYPE=3)
   BEGIN
	   -------------------------------------------------------            
	   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID      
	   -------------------------------------------------------       
	   SELECT @APPLICANT_ID		 = CUSTOMER_ID 
	   FROM   MIG_IL_IMPORT_SUMMARY      
	   WHERE  POLICY_SEQUENTIAL   = @LOOP_POLICY_SEQUANCE_NO AND      
		ENDORSEMENT_SEQUENTIAL	  = @LOOP_END_SEQUANCE_NO    AND      
			  FILE_TYPE           = @IMPORT_COAPPLICANT_FILE_TYPE AND      
			  [FILE_NAME]         = @IMPORT_FILE_NAME        AND      
			  IS_ACTIVE			  = 'Y'      
               
	   -- IF APPLICANT IS EXISTS THEN UPDATE EXISTING APPLICANT DETAILS
	   IF(@APPLICANT_ID>0)
	   BEGIN
			------------------------------------            
			-- UPDATE IMPORT DETAILS      
			------------------------------------               
		   UPDATE MIG_IL_POLICY_COAPPLICANT_DETAILS      
		   SET    CUSTOMER_ID       = @CUSTOMER_ID,      
				  POLICY_ID         = @POLICY_ID,      
				  POLICY_VERSION_ID = @POLICY_VERSION_ID,      
				  IS_PROCESSED      = 'Y'      
		   WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND      
				  IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO     
          
          -- UPDATE EXISTING DETAILS
       UPDATE POL_APPLICANT_LIST               
       SET	    
			 [CREATED_DATETIME]			=	T.CREATED_DATETIME
			,[LAST_UPDATED_TIME]		=	T.LAST_UPDATED_TIME
			,[IS_PRIMARY_APPLICANT]		=	CASE WHEN T.[IS_PRIMARY_APPLICANT] IS NULL OR T.[IS_PRIMARY_APPLICANT]=0 THEN PAL.[IS_PRIMARY_APPLICANT] ELSE T.[IS_PRIMARY_APPLICANT] END 			 
			,[COMMISSION_PERCENT]		=	CASE WHEN T.COMMISION IS NULL OR T.COMMISION=0 THEN [COMMISSION_PERCENT] ELSE T.COMMISION END 			
			,[FEES_PERCENT]				=	CASE WHEN T.FEES IS NULL OR T.FEES=0 THEN [FEES_PERCENT] ELSE T.FEES END 			
			,[PRO_LABORE_PERCENT]		=	CASE WHEN T.PRO_LOBORE IS NULL OR T.PRO_LOBORE=0 THEN [PRO_LABORE_PERCENT] ELSE T.PRO_LOBORE END 			
			
	     FROM POL_APPLICANT_LIST PAL INNER JOIN
	     (
	        SELECT  CUSTOMER_ID
	               ,POLICY_ID
	               ,POLICY_VERSION_ID
	               ,dbo.fun_GetDefaultUserID() AS [CREATED_DATETIME]    
	               ,GETDATE()  AS [LAST_UPDATED_TIME]
	               ,@is_primary_applicant AS [IS_PRIMARY_APPLICANT]
				   ,CASE WHEN @TRANSACTION_TYPE=14560 THEN COMMISION ELSE 0 END AS COMMISION  
				   ,CASE WHEN @TRANSACTION_TYPE=14560 THEN FEES ELSE 0 END  AS FEES    
				   ,CASE WHEN @TRANSACTION_TYPE=14560 THEN PRO_LOBORE ELSE 0 END AS PRO_LOBORE        
							   
		    FROM   MIG_IL_POLICY_COAPPLICANT_DETAILS 
		    WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		           IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
	     )T ON T.CUSTOMER_ID= PAL.CUSTOMER_ID AND T.POLICY_ID=PAL.POLICY_ID AND T.POLICY_VERSION_ID=PAL.POLICY_VERSION_ID
	     
	     WHERE PAL.CUSTOMER_ID =@CUSTOMER_ID AND PAL.POLICY_ID=@POLICY_ID AND PAL.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PAL.APPLICANT_ID=@APPLICANT_ID
          
	   END
	   ELSE
	   BEGIN
			 
			------------------------------------------------------  
			-- INSERT POLICY APPLICANT  
			------------------------------------------------------     

			  INSERT INTO [dbo].[POL_APPLICANT_LIST]    
					   ([POLICY_ID]               
					   ,[POLICY_VERSION_ID]    
					   ,[CUSTOMER_ID]    
					   ,[APPLICANT_ID]    
					   ,[CREATED_BY]    
					   ,[MODIFIED_BY]    
					   ,[CREATED_DATETIME]    
					   ,[LAST_UPDATED_TIME]    
					   ,[IS_PRIMARY_APPLICANT]    
					   ,[COMMISSION_PERCENT]    
					   ,[FEES_PERCENT]    
					   ,[PRO_LABORE_PERCENT]             
					   )    
			  VALUES  
			   (      
				  @POLICY_ID    
				 ,@POLICY_VERSION_ID    
				 ,@CUSTOMER_ID  
				 ,@APPLICANT_ID    
				 ,dbo.fun_GetDefaultUserID()    
				 ,NULL    
				 ,GETDATE()    
				 ,NULL          
				 ,@is_primary_applicant   
				 ,CASE WHEN @TRANSACTION_TYPE=14560 THEN @LOOP_COMMISION ELSE 0 END   
				 ,CASE WHEN @TRANSACTION_TYPE=14560 THEN @LOOP_FEES ELSE 0 END      
				 ,CASE WHEN @TRANSACTION_TYPE=14560 THEN @LOOP_PRO_LOBORE ELSE 0 END         
				)  
	   END
 
   END
      
     ------------------------------------            
     -- UPDATE IMPORT DETAILS      
     ------------------------------------               
   UPDATE MIG_IL_POLICY_COAPPLICANT_DETAILS      
   SET    CUSTOMER_ID       = @CUSTOMER_ID,      
          POLICY_ID         = @POLICY_ID,      
          POLICY_VERSION_ID = @POLICY_VERSION_ID,      
          IS_PROCESSED      = 'Y'      
   WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND      
          IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO     
            
            
     EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]       
   @IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID,  
   @IMPORT_SERIAL_NO    = @IMPORT_SERIAL_NO,   
   @CUSTOMER_ID      = @CUSTOMER_ID ,  
   @POLICY_ID       = @POLICY_ID,  
   @POLICY_VERSION_ID   = @POLICY_VERSION_ID,  
   @IS_ACTIVE       = 'Y',  
   @IS_PROCESSED      = 'Y',  
   @FILE_TYPE       = @IMPORT_COAPPLICANT_FILE_TYPE,  
   @FILE_NAME              = @IMPORT_FILE_NAME,  
   @CUSTOMER_SEQUENTIAL    = NULL,  
   @POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO,   
   @ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,  
   @IMPORT_SEQUENTIAL      = @LOOP_COAPPLICANT_SEQUANCE_NO,  
   @IMPORT_SEQUENTIAL2     = NULL,  
   @LOB_ID       = @LOB_ID ,   
   @IMPORTED_RECORD_ID     = @APPLICANT_ID ,
   @PROCESS_TYPE		   = @PROCESS_TYPE    
             
      
    END      
    SET @COUNTER+=1       
             
  END  -- END OF WHILE LOOP      
        
END TRY        
BEGIN CATCH          
           
 SELECT           
    @ERROR_NUMBER    = ERROR_NUMBER(),          
    @ERROR_SEVERITY  = ERROR_SEVERITY(),          
    @ERROR_STATE     = ERROR_STATE(),          
    @ERROR_PROCEDURE = ERROR_PROCEDURE(),          
    @ERROR_LINE   = ERROR_LINE(),          
    @ERROR_MESSAGE   = ERROR_MESSAGE()          
               
  -- CREATING LOG OF EXCEPTION           
  EXEC [PROC_MIG_INSERT_ERROR_LOG]            
  @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID          
 ,@IMPORT_SERIAL_NO  = 0          
 ,@ERROR_NUMBER      = @ERROR_NUMBER          
 ,@ERROR_SEVERITY    = @ERROR_SEVERITY          
 ,@ERROR_STATE          = @ERROR_STATE          
 ,@ERROR_PROCEDURE   = @ERROR_PROCEDURE          
 ,@ERROR_LINE        = @ERROR_LINE          
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE          
 ,@INITIAL_LOAD_FLAG    = 'Y'          
            
           
 END CATCH          
              
        
        
         
END       



