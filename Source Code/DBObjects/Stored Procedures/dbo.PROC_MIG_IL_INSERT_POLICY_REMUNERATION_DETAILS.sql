
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_REMUNERATION_DETAILS]    Script Date: 12/02/2011 16:17:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_REMUNERATION_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_REMUNERATION_DETAILS]
GO
 

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_REMUNERATION_DETAILS]    Script Date: 12/02/2011 16:17:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:   Pradeep Kushwaha   
-- created date:  18-OCt-2011   
-- Description:  <insert data in POL_REMUNERATION Table>         
-- =============================================    
        
--DROP PROC [PROC_MIG_IL_INSERT_POLICY_REMUNERATION_DETAILS] 1499
--GO       
-- =============================================                  
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_REMUNERATION_DETAILS]                   
                  
--------------------------------- INPUT PARAMETER                  
@IMPORT_REQUEST_ID  INT                  
-------------------------------------------------                  
                    
AS                  
BEGIN                  
                   
-------------------------------- DECLARATION PART                  
----------------------------------------------------------------------------------------                  
DECLARE @IMPORT_SERIAL_NO INT                
DECLARE @REMUNERATION_ID INT                
DECLARE @COMPANY_ID INT                
DECLARE @CUSTOMER_ID INT                
DECLARE @POLICY_ID INT                
DECLARE @POLICY_VERSION_ID INT                
DECLARE @RISK_ID INT=0                
              
DECLARE @ERROR_NUMBER    INT                    
DECLARE @ERROR_SEVERITY  INT                    
DECLARE @ERROR_STATE     INT                    
DECLARE @ERROR_PROCEDURE VARCHAR(512)                    
DECLARE @ERROR_LINE    INT                    
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)                    
              
DECLARE @LOOP_REMUNERATION INT                        
DECLARE @LOOP_POLICY_SEQUANCE_NO INT                      
DECLARE @LOOP_END_SEQUANCE_NO INT                      
DECLARE @LOOP_IMPORT_SERIAL_NO INT                      
DECLARE @LOOP_NAME NVARCHAR(20)              
DECLARE @COUNTER INT  =1                    
DECLARE @MAX_RECORD_COUNT INT                     
DECLARE @ERROR_NO INT=0    
DECLARE @LOB_ID   INT               
              
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)                   
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE                
DECLARE @IMPORT_REMUNERATION_FILE_TYPE INT = 14940 -- FOR REMUNERATION FILE TYPE                
                 
         
DECLARE @LOOP_LEADER_FOLLOWER INT                 
DECLARE @LOOP_COAPPLICANT_CODE VARCHAR(50)                
DECLARE @LOOP_COMMISION_TYPE INT              
              
DECLARE @COAPPLICANT_ID INT              
DECLARE @TRANSACTION_TYPE INT
DECLARE @BROKER_ID INT             
DECLARE @PROCESS_TYPE INT    --- Change   
                
BEGIN TRY                  
             
CREATE TABLE #TEMP_POLICY_REMUNERATION                    
     (                    
    ID INT IDENTITY(1,1),                    
    POLICY_SEQUANCE_NO INT,                    
    END_SEQUANCE_NO INT,                   
    IMPORT_SERIAL_NO BIGINT,                    
    REMUNERATION_SEQUESNCE_NO BIGINT,                
    NAME NVARCHAR(20),              
    LOOP_LEADER_FOLLOWER    INT NULL,              
    LOOP_COAPPLICANT_CODE VARCHAR(50),              
    LOOP_COMMISION_TYPE INT NULL                   
     )                    
                  
                  
                  
  INSERT INTO #TEMP_POLICY_REMUNERATION                    
     (                    
		POLICY_SEQUANCE_NO ,                    
		END_SEQUANCE_NO ,                   
		IMPORT_SERIAL_NO,                    
		REMUNERATION_SEQUESNCE_NO,                
		NAME  ,              
		LOOP_LEADER_FOLLOWER ,              
		LOOP_COAPPLICANT_CODE ,              
		LOOP_COMMISION_TYPE                
     )                    
     (                    
      SELECT 
		POLICY_SEQUENCE_NO,                  
		END_SEQUENCE_NO,                  
		IMPORT_SERIAL_NO,                  
		REMUNERATION_SEQUENCE_NO,                
		NAME,              
		LEADER,              
		COAPPLICANT_CODE,              
		COMMISION_TYPE                               
      FROM MIG_IL_POLICY_REMUNERATION_DETAILS WITH(NOLOCK)                    
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0                    
     )                    
               
        
  ------------------------------------                          
   -- GET MAX RECOUNT COUNT       
   ------------------------------------                       
    SELECT @MAX_RECORD_COUNT = COUNT(ID)                     
    FROM   #TEMP_POLICY_REMUNERATION                     
                  
   ------------------------------------                      
   -- GET FILE NAME                
   ------------------------------------                   
   IF(@MAX_RECORD_COUNT>0)                
   BEGIN                
                   
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)                 
    FROM  MIG_IL_IMPORT_REQUEST_FILES                
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND                
          IMPORT_FILE_TYPE   = @IMPORT_REMUNERATION_FILE_TYPE        
                  
      END                
   ------------------------------------                
   -- FETCH ALL ACTIVE AGENCY   
   ------------------------------------   
     SELECT  CAST(AGENCY_CODE AS INT)AS AGENCY_CODE, AGENCY_ID
				 INTO #TempAgencyList
				 FROM MNT_AGENCY_LIST 
				 WHERE AGENCY_CODE NOT IN (SELECT REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK)) AND
				       IS_ACTIVE='Y'
				                         
      WHILE(@COUNTER<=@MAX_RECORD_COUNT)                    
  BEGIN                  
                  
     SET @ERROR_NO=0                    
                   
     SET @CUSTOMER_ID=0                
     SET @POLICY_ID=0              
     SET @POLICY_VERSION_ID=0             
     SET @LOOP_COAPPLICANT_CODE='0'             
     SET @LOOP_COMMISION_TYPE=0          
     SET @LOOP_LEADER_FOLLOWER=0          
     SET @LOOP_NAME='0'
     SET @LOOP_REMUNERATION=0          
     SET @TRANSACTION_TYPE=0
     SET @BROKER_ID=0;               
     SET @PROCESS_TYPE=0 
     
      SELECT                 
      @IMPORT_SERIAL_NO			= IMPORT_SERIAL_NO ,                    
      @LOOP_POLICY_SEQUANCE_NO	= POLICY_SEQUANCE_NO  ,                  
      @LOOP_END_SEQUANCE_NO		= END_SEQUANCE_NO,                    
	  @LOOP_NAME				= NAME,              
	  @LOOP_LEADER_FOLLOWER		=LOOP_LEADER_FOLLOWER,              
	  @LOOP_COAPPLICANT_CODE	=LOOP_COAPPLICANT_CODE,              
	  @LOOP_COMMISION_TYPE		=LOOP_COMMISION_TYPE,  
	  @LOOP_REMUNERATION		= REMUNERATION_SEQUESNCE_NO              
     FROM   #TEMP_POLICY_REMUNERATION (NOLOCK) WHERE ID   = @COUNTER                 
                   
                
     -- select * from #TEMP_POLICY_REMUNERATION     
   -------------------------------------------------------                      
   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID                
   -------------------------------------------------------                 
   SELECT @CUSTOMER_ID				= CUSTOMER_ID ,                
          @POLICY_ID				= POLICY_ID,                
          @POLICY_VERSION_ID		= POLICY_VERSION_ID,  
          @LOB_ID					= LOB_ID       ,
          @PROCESS_TYPE				=PROCESS_TYPE         
   FROM   MIG_IL_IMPORT_SUMMARY                
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND                
         ENDORSEMENT_SEQUENTIAL   = @LOOP_END_SEQUANCE_NO    AND                
          FILE_TYPE               = @IMPORT_POLICY_FILE_TYPE AND  
		  [FILE_NAME]             = @IMPORT_FILE_NAME        AND                
		  IS_ACTIVE               = 'Y'                
                   
                  
  ----------------------------------------------------------------              
  --   CHECK WHETER CO-APPLICANT CODE EXISTS              
  ---------------------------------------------------------------              
  SELECT  @COAPPLICANT_ID=APPLICANT_ID FROM CLT_APPLICANT_LIST (NOLOCK) WHERE CONTACT_CODE=@LOOP_COAPPLICANT_CODE              
  SELECT @TRANSACTION_TYPE= TRANSACTION_TYPE FROM POL_CUSTOMER_POLICY_LIST NOLOCK WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
  SELECT @BROKER_ID=AGENCY_ID FROM #TempAgencyList WITH (NOLOCK) WHERE AGENCY_CODE=CAST(@LOOP_NAME AS INT)
  --select  @TRANSACTION_TYPE as TRANSACTION_TYPE     ,@COAPPLICANT_ID as COAPPLICANT_ID,@LOOP_COMMISION_TYPE as LOOP_COMMISION_TYPE ,@LOOP_LEADER_FOLLOWER as LOOP_LEADER_FOLLOWER
  ---------------------------- FIND LEADER FOLLOWER---------------              
  -- IF(@LOOP_LEADER_FOLLOWER=@LOOP_NAME)              
  -- SET @LEADER=10963                   
  -- -------------------------------------------------------                      
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT                
   -------------------------------------------------------       
 
    IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)                
		SET @ERROR_NO =53 -- Application/Policy does not exists   
    ELSE IF(@LOOP_COAPPLICANT_CODE IS NOT NULL OR @LOOP_COAPPLICANT_CODE<>'')              
		  BEGIN              
			IF (@COAPPLICANT_ID = '' OR @COAPPLICANT_ID IS NULL)              
					SET @ERROR_NO=51           
		       
		  END              
	---------------------------------------------------------------           
    -- CHECK WHETHER MORE THAN ONE LEADER              
    -----------------------------------------------------------                 
    IF (@TRANSACTION_TYPE<>14560 and EXISTS (SELECT 1 FROM POL_REMUNERATION WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LEADER=10963) and @LOOP_LEADER_FOLLOWER=10963  )
		BEGIN 
		 --IF @LOOP_LEADER_FOLLOWER=10963 
			SET @ERROR_NO =59           
		END
		
    IF(@TRANSACTION_TYPE<>14560 AND @LOOP_COMMISION_TYPE <> 43)	   
		SET @ERROR_NO=96  -- invalide comm type            
		
	---------------------------------------------------------------          
   -- CHECK WHETHER BROKER IS ALREADY EXISTS   -for nomal policy            
   -----------------------------------------------------------                 
   --43 -Commison COM
	IF (@TRANSACTION_TYPE<>14560)
	BEGIN
	IF EXISTS(SELECT * FROM POL_REMUNERATION WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID              
		 AND BROKER_ID=@BROKER_ID and COMMISSION_TYPE=@LOOP_COMMISION_TYPE)
		 SET @ERROR_NO =98  
	END
    IF (@TRANSACTION_TYPE=14560)
    BEGIN
    IF EXISTS(SELECT * FROM POL_REMUNERATION WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID              
		 AND BROKER_ID=@BROKER_ID AND CO_APPLICANT_ID=@COAPPLICANT_ID and COMMISSION_TYPE=@LOOP_COMMISION_TYPE)
		SET @ERROR_NO =98           
	END	
    
  -----------------------------------------------------------                  
  -- INSERT ERROR DETAILS            
  -----------------------------------------------------------           
          
        
  --select @ERROR_NO        
  IF(@ERROR_NO>0)            
     BEGIN            
             
   UPDATE MIG_IL_POLICY_REMUNERATION_DETAILS          
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
   @ERROR_SOURCE_TYPE     = 'REMU'               
                   
           
     END             
       ELSE            
       BEGIN    
              
      IF(@PROCESS_TYPE=1)--For new business added by pradeep -
      BEGIN   
       SELECT @REMUNERATION_ID=ISNULL(MAX(REMUNERATION_ID),0)+1 FROM POL_REMUNERATION WITH(NOLOCK)              
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID                 
      
       
     -------------------------------- INSERT INTO POL_REMUNERATION                 
        INSERT INTO POL_REMUNERATION                
        ([REMUNERATION_ID],                
        [CUSTOMER_ID],                
        [POLICY_ID],                
        [POLICY_VERSION_ID],                
        [COMMISSION_PERCENT],                
        [COMMISSION_TYPE],                
        [IS_ACTIVE],                
        [CREATED_BY],                
        [CREATED_DATETIME],                
        [BRANCH],                
        [AMOUNT],                
        [LEADER],                
        [NAME],                
        [RISK_ID],                
        [CO_APPLICANT_ID],  
        BROKER_ID                
        )                
        SELECT 
        @REMUNERATION_ID,                
        @CUSTOMER_ID,             
        @POLICY_ID,                
        @POLICY_VERSION_ID,                
        COMMISSION_PERCENTAGE,                
        @LOOP_COMMISION_TYPE,                
        'Y', --IS_ACTIVE                
        dbo.fun_GetDefaultUserID(), --<CREATED_BY, int,>               
        GETDATE(), --CREATED_DATETIME    
        BRANCH,                
        0.00, --AMOUNT                
        @LOOP_LEADER_FOLLOWER,                
        NULL, --NAME                
        @RISK_ID,                 
        @COAPPLICANT_ID    ,  
        @BROKER_ID            
                        
        FROM MIG_IL_POLICY_REMUNERATION_DETAILS                
        WHERE      IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID                  
        AND       IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO                
      
      END
      ELSE IF(@PROCESS_TYPE =3) -- FOR ENDORESMENT POLICY - ADDED BY PRADEEP 
      BEGIN
      
	   -------------------------------------------------------                      
	   -- GET @REMUNERATION_ID  
	   -------------------------------------------------------                 
	   SELECT @REMUNERATION_ID		  = IMPORTED_RECORD_ID      
	   FROM   MIG_IL_IMPORT_SUMMARY                
	   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND                
			 ENDORSEMENT_SEQUENTIAL   = @LOOP_END_SEQUANCE_NO    AND                
			  FILE_TYPE               = @IMPORT_REMUNERATION_FILE_TYPE AND  
			  [FILE_NAME]             = @IMPORT_FILE_NAME        AND                
			  IS_ACTIVE               = 'Y'  
		  
	
	    -- IF REMUNERATION IS EXISTS THEN UPDATE EXISTING REMUNERATION DETAILS
	   IF(@REMUNERATION_ID>0)
	   BEGIN
			   ------------------------------------                      
			   -- UPDATE IMPORT DETAILS                
			   ------------------------------------                 
		                            
			  UPDATE MIG_IL_POLICY_REMUNERATION_DETAILS              
			  SET    CUSTOMER_ID    = @CUSTOMER_ID,                
			   POLICY_ID			= @POLICY_ID,                
			   POLICY_VERSION_ID	= @POLICY_VERSION_ID,                
			   IS_PROCESSED			= 'Y'                
			  WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND                
			   IMPORT_SERIAL_NO		   = @IMPORT_SERIAL_NO   
	   
			-- UPDATE EXISTING DETAILS
		   UPDATE [POL_REMUNERATION]               
		   SET	[COMMISSION_PERCENT]   =    CASE WHEN T.COMMISSION_PERCENTAGE  IS NULL OR T.COMMISSION_PERCENTAGE =0 THEN [COMMISSION_PERCENT] ELSE T.COMMISSION_PERCENTAGE END	            
				,[COMMISSION_TYPE]     =	CASE WHEN T.COMMISION_TYPE IS NULL OR T.COMMISION_TYPE=0 THEN [COMMISSION_TYPE] ELSE T.COMMISION_TYPE END            
				,[IS_ACTIVE]           =	IS_DEACTIVATE
				,[BRANCH]              =	CASE WHEN T.BRANCH IS NULL OR T.BRANCH=0 THEN PR.[BRANCH] ELSE T.BRANCH END          
				,[LEADER]              =	CASE WHEN T.LEADER IS NULL OR T.LEADER=0 THEN PR.[LEADER] ELSE T.LEADER END         
				,BROKER_ID             =	CASE WHEN T.COMMISION_TYPE  IS NULL OR T.COMMISION_TYPE =0 THEN PR.BROKER_ID ELSE T.COMMISION_TYPE  END 
				,MODIFIED_BY		   =	T.MODIFIED_BY
				,LAST_UPDATED_DATETIME =    T.LAST_UPDATED_DATETIME   
				     
			 FROM [POL_REMUNERATION] PR INNER JOIN
			 (
				SELECT   
					 CUSTOMER_ID          
					,POLICY_ID            
					,POLICY_VERSION_ID   
					,COMMISSION_PERCENTAGE 
					,COMMISION_TYPE 
					,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END AS IS_DEACTIVATE
					,BRANCH                
					,LEADER 
					,@BROKER_ID AS BROKER_ID 
					,dbo.fun_GetDefaultUserID() MODIFIED_BY
					,GETDATE() LAST_UPDATED_DATETIME         
				FROM   MIG_IL_POLICY_REMUNERATION_DETAILS 
				WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
					   IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
				   
			 )T ON T.CUSTOMER_ID= PR.CUSTOMER_ID AND T.POLICY_ID=PR.POLICY_ID AND T.POLICY_VERSION_ID=PR.POLICY_VERSION_ID
		     
			 WHERE PR.CUSTOMER_ID =@CUSTOMER_ID AND PR.POLICY_ID=@POLICY_ID AND PR.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PR.REMUNERATION_ID=@REMUNERATION_ID
	   END
	   	-----------------------------------------------------------------------------      
		-- IF REMUNERATION_ID IS NOT EXISTS THEN ADD NEW REMUNERATION IN POLICY ENDORSEMENT
		-----------------------------------------------------------------------------      
	   ELSE
	   BEGIN
		   SELECT @REMUNERATION_ID=ISNULL(MAX(REMUNERATION_ID),0)+1 FROM POL_REMUNERATION WITH(NOLOCK)              
		   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID                 
	      
       
			-------------------------------- INSERT INTO POL_REMUNERATION                 
			INSERT INTO POL_REMUNERATION                
			([REMUNERATION_ID],                
			[CUSTOMER_ID],                
			[POLICY_ID],                
			[POLICY_VERSION_ID],                
			[COMMISSION_PERCENT],                
			[COMMISSION_TYPE],                
			[IS_ACTIVE],                
			[CREATED_BY],                
			[CREATED_DATETIME],                
			[BRANCH],                
			[AMOUNT],                
			[LEADER],                
			[NAME],                
			[RISK_ID],                
			[CO_APPLICANT_ID],  
			BROKER_ID                
			)                
			SELECT 
			@REMUNERATION_ID,                
			@CUSTOMER_ID,             
			@POLICY_ID,                
			@POLICY_VERSION_ID,                
			COMMISSION_PERCENTAGE,                
			@LOOP_COMMISION_TYPE,                
			'Y', --IS_ACTIVE                
			dbo.fun_GetDefaultUserID(), --<CREATED_BY, int,>               
			GETDATE(), --CREATED_DATETIME    
			BRANCH,                
			0.00, --AMOUNT                
			@LOOP_LEADER_FOLLOWER,                
			NULL, --NAME                
			@RISK_ID,                 
			@COAPPLICANT_ID    ,  
			@BROKER_ID            
	                        
			FROM MIG_IL_POLICY_REMUNERATION_DETAILS                
			WHERE      IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID                  
			AND       IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO         
	        
	   END
	   
      END      
         
                       
      ------------------------------------     
       -- UPDATE IMPORT DETAILS                
       ------------------------------------                 
                            
      UPDATE MIG_IL_POLICY_REMUNERATION_DETAILS              
      SET    CUSTOMER_ID    = @CUSTOMER_ID,                
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
     @FILE_TYPE       = @IMPORT_REMUNERATION_FILE_TYPE,  
     @FILE_NAME              = @IMPORT_FILE_NAME,  
     @CUSTOMER_SEQUENTIAL    = NULL,  
     @POLICY_SEQUENTIAL   = @LOOP_POLICY_SEQUANCE_NO,   
  @ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,  
     @IMPORT_SEQUENTIAL      = @LOOP_REMUNERATION,  
     @IMPORT_SEQUENTIAL2     = NULL,  
     @LOB_ID       = @LOB_ID ,   
     @IMPORTED_RECORD_ID     = @REMUNERATION_ID,
     @PROCESS_TYPE			 =@PROCESS_TYPE                
                
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
 ,@ERROR_LINE        = @ERROR_LINE             ,@ERROR_MESSAGE        = @ERROR_MESSAGE                    
 ,@INITIAL_LOAD_FLAG    = 'Y'  
                      
                     
 END CATCH                    
 DROP TABLE #TEMP_POLICY_REMUNERATION          
END   
  



GO
