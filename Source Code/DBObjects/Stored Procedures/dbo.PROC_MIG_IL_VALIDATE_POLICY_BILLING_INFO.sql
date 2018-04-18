/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_BILLING_INFO]    Script Date: 12/02/2011 17:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_BILLING_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_BILLING_INFO]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_BILLING_INFO]    Script Date: 12/02/2011 17:08:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop PROC [PROC_MIG_IL_VALIDATE_POLICY_BILLING_INFO]
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROC [dbo].[PROC_MIG_IL_VALIDATE_POLICY_BILLING_INFO]
@IMPORT_REQUEST_ID  INT  
AS    
BEGIN  
DECLARE @ERROR_NUMBER    INT      
DECLARE @ERROR_SEVERITY  INT      
DECLARE @ERROR_STATE     INT      
DECLARE @ERROR_PROCEDURE VARCHAR(512)      
DECLARE @ERROR_LINE    INT      
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)      

    
    
DECLARE @LOOP_POLICY_NUMBER NVARCHAR(50)         
DECLARE @LOOP_POLICY_SEQUANCE_NO INT        
DECLARE @LOOP_END_SEQUANCE_NO INT        
DECLARE @LOOP_INSTALL_SERIAL_NO INT    
   
      
DECLARE @COUNTER INT  =1      
DECLARE @MAX_RECORD_COUNT INT       
DECLARE @ERROR_NO INT=0      
DECLARE @IMPORT_SERIAL_NO INT     
          
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)           
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE        
DECLARE @IMPORT_BILLING_FILE_TYPE INT = 14969 -- FOR POLICY BILLING INFO     
  
DECLARE @STATUS NVARCHAR(2)
DECLARE @ACTIVITY_TYPE INT   
DECLARE @OCCUPIED_AS INT   
DECLARE @CONSTRUCTION INT     
DECLARE @OUR_NUMBER NVARCHAR(20)
	BEGIN TRY
	 CREATE TABLE #TEMP_BILLING
     (      
       ID INT IDENTITY(1,1),      
       IMPORT_SERIAL_NO INT NULL,    
       [STATUS] NVARCHAR(50) NULL,
       POLICY_SEQUENCE_NO INT,    
	   END_SEQUENCE_NO INT,    
	   INSTALLMENT_SEQUENCE_NO  INT,
	   OUR_NUMBER   NVARCHAR(20)
     )   
     
     
     INSERT INTO #TEMP_BILLING      
     (      
      IMPORT_SERIAL_NO ,  
      [STATUS],
      POLICY_SEQUENCE_NO,  
	  END_SEQUENCE_NO,  
      INSTALLMENT_SEQUENCE_NO , 
      OUR_NUMBER
     )      
     (  
      SELECT       
      IMPORT_SERIAL_NO  ,  
      [STATUS],     
      POLICY_SEQUENTIAL,
	  ENDORSEMENT_SEQUENTIAL,
	  INSTALLMENT_SEQUENTIAL ,
	  OUR_NUMBER
      FROM MIG_IL_POLICY_BILLING_DETAILS WITH(NOLOCK)      
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0      
     )  
      
   -- GET MAX RECOUNT COUNT      
   ------------------------------------         
    SELECT @MAX_RECORD_COUNT = COUNT(ID)       
    FROM   #TEMP_BILLING
    
    
     IF(@MAX_RECORD_COUNT>0)            
    BEGIN            
                
      SELECT  @IMPORT_FILE_NAME=SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)            
      FROM MIG_IL_IMPORT_REQUEST_FILES WITH(NOLOCK)            
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND             
            IMPORT_FILE_TYPE  = @IMPORT_BILLING_FILE_TYPE            
         
    END    
    
    
	WHILE(@COUNTER<=@MAX_RECORD_COUNT)      
	  BEGIN    
			 SET @ERROR_NO=0      
			 SET @ACTIVITY_TYPE=0   
			 SET @OCCUPIED_AS=0   
			 SET @CONSTRUCTION =0  
			 SET @OUR_NUMBER=NULL
		  SELECT     
			@IMPORT_SERIAL_NO   = IMPORT_SERIAL_NO  ,  
			@STATUS     = STATUS,  
			@LOOP_POLICY_SEQUANCE_NO = POLICY_SEQUENCE_NO,       
			@LOOP_END_SEQUANCE_NO       = END_SEQUENCE_NO,     
			@LOOP_INSTALL_SERIAL_NO    = INSTALLMENT_SEQUENCE_NO,       
			@OUR_NUMBER					=OUR_NUMBER
		  FROM   #TEMP_BILLING (NOLOCK) WHERE ID   = @COUNTER         
	    
		  ------------------------------------  VALIDATION OF ALREADY PROCESSED RECORD  -----------------------------------------    
	  IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_BILLING_FILE_TYPE     
							AND POLICY_SEQUENTIAL= @LOOP_POLICY_SEQUANCE_NO  AND     
								ENDORSEMENT_SEQUENTIAL= @LOOP_END_SEQUANCE_NO AND    
								IMPORT_SEQUENTIAL=@LOOP_INSTALL_SERIAL_NO AND --CHANGE    
								IS_ACTIVE='Y'     
						 )              
	   SET @ERROR_NO=66      -- This record already processed.      
	         
	    
	  IF(@STATUS  NOT IN ('Y','N','C'))   
			SET @ERROR_NO=248 --installment staus not exists

	  ELSE IF(@OUR_NUMBER IS NULL AND LEN(@OUR_NUMBER)<13 )
			SET @ERROR_NO =140 --  Invalid Our Number.     
		 ------------------------------------  VALIDATION OF STATE    
	
		  IF(@ERROR_NO>0)      
		  BEGIN      
	             
		  UPDATE MIG_IL_POLICY_BILLING_DETAILS      
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
			@ERROR_SOURCE_TYPE     = 'BILL'         
		END    
	        
	    
		SET @COUNTER+=1     
	    
    END
           
 
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



GO