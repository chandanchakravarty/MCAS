/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_COINSURER_DETAILS]    Script Date: 12/02/2011 16:07:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_COINSURER_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_COINSURER_DETAILS]
GO

 
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_COINSURER_DETAILS]    Script Date: 12/02/2011 16:07:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                              
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_POLICY_COINSURER_DETAILS]                                                          
Created by            : Santosh Kumar Gautam                                                             
Date                  : 27 Sept 2011                                                            
Purpose               : Insert Policy COI  
Modified by           : Pradeep Kushwaha
Date                  : 14 nov 2011                                                            
Revison History       :                                                              
Used In               : INITIAL LOAD                 
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc [PROC_MIG_IL_INSERT_POLICY_COINSURER_DETAILS]    555                                                 
------   ------------       -------------------------*/   
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_COINSURER_DETAILS]       
      
--------------------------------- INPUT PARAMETER      
@IMPORT_REQUEST_ID  INT      
-------------------------------------------------      
        
AS      
BEGIN      
       
-------------------------------- DECLARATION PART      
----------------------------------------------------------------------------------------      
  
DECLARE @COINSURANCE_ID INT    
DECLARE @COMPANY_ID INT    
  
DECLARE @ERROR_NUMBER    INT        
DECLARE @ERROR_SEVERITY  INT        
DECLARE @ERROR_STATE     INT        
DECLARE @ERROR_PROCEDURE VARCHAR(512)        
DECLARE @ERROR_LINE    INT        
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)        
  
  
DECLARE @CUSTOMER_ID INT      
DECLARE @POLICY_ID INT      
DECLARE @POLICY_VERSION_ID INT      
DECLARE @POLICY_NUMBER NVARCHAR(21)    
DECLARE @LOB_ID   INT     
   
DECLARE @LOOP_COINSURANCE_SEQUENTIAL INT  
DECLARE @LOOP_LEADER_FOLLOWER INT            
DECLARE @LOOP_POLICY_SEQUANCE_NO INT          
DECLARE @LOOP_END_SEQUANCE_NO INT          
DECLARE @LOOP_IMPORT_SERIAL_NO INT          
DECLARE @COUNTER INT  =1        
DECLARE @MAX_RECORD_COUNT INT         
DECLARE @ERROR_NO INT=0        
DECLARE @IMPORT_SERIAL_NO INT    
DECLARE @REIN_COMAPANY_ID INT  
DECLARE @COI_SHARE  DECIMAL(18,2)   
    
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)       
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE    
DECLARE @IMPORT_COINSURENCE_FILE_TYPE INT = 14941 -- FOR  CO-INSURENCE FILE TYPE      
DECLARE @PROCESS_TYPE INT    --- Change       
BEGIN TRY      
  
  
   
      
      SELECT  
   ROW_NUMBER() OVER(Order BY POLICY_SEQUENTIAL) as ID,   
   POLICY_SEQUENTIAL,      
   ENDORSEMENT_SEQUENTIAL,      
   IMPORT_SERIAL_NO,      
   LEADER_FOLLOWER,    
   COINSURANCE_NAME,  
   COINSURANCE_SEQUENTIAL,
   COI_SHARE     
   INTO  
  #TEMP_POLICY_COINSURENCE                   
      FROM MIG_IL_POLICY_COINSURER_DETAILS WITH(NOLOCK)        
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0        
       
    
    
      
  ------------------------------------              
   -- GET MAX RECOUNT COUNT        
   ------------------------------------           
    SELECT @MAX_RECORD_COUNT = COUNT(ID)         
    FROM   #TEMP_POLICY_COINSURENCE         
      
     
    ------------------------------------          
   -- GET FILE NAME    
   ------------------------------------       
   IF(@MAX_RECORD_COUNT>0)    
   BEGIN    
       
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)     
    FROM  MIG_IL_IMPORT_REQUEST_FILES    
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND    
          IMPORT_FILE_TYPE   = @IMPORT_COINSURENCE_FILE_TYPE      
       
   END    
     
  WHILE(@COUNTER<=@MAX_RECORD_COUNT) 
  BEGIN    
    
     SET @ERROR_NO=0    
     SET @LOOP_LEADER_FOLLOWER=0  
     SET @CUSTOMER_ID=0    
	 SET @POLICY_ID=0    
     SET @POLICY_VERSION_ID=0    
     SET @COINSURANCE_ID=0  
     SET @LOOP_COINSURANCE_SEQUENTIAL=0  
     SET @COI_SHARE=0
     SET @PROCESS_TYPE=0
         
      SELECT     
   @IMPORT_SERIAL_NO        = IMPORT_SERIAL_NO ,        
   @LOOP_POLICY_SEQUANCE_NO = POLICY_SEQUENTIAL  ,      
   @LOOP_END_SEQUANCE_NO    = ENDORSEMENT_SEQUENTIAL,        
   @COMPANY_ID				= COINSURANCE_NAME  ,  
   @LOOP_LEADER_FOLLOWER    = LEADER_FOLLOWER,   
   @LOOP_COINSURANCE_SEQUENTIAL = COINSURANCE_SEQUENTIAL ,
   @COI_SHARE					=COI_SHARE   
     FROM   #TEMP_POLICY_COINSURENCE (NOLOCK) WHERE ID   = @COUNTER    
       
   ------------------------------------------------------          
   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID    
   -------------------------------------------------------     
   SELECT @CUSTOMER_ID       = CUSTOMER_ID ,    
          @POLICY_ID         = POLICY_ID,    
          @POLICY_VERSION_ID = POLICY_VERSION_ID,  
          @LOB_ID			 = LOB_ID    ,
          @PROCESS_TYPE		 = PROCESS_TYPE  --- Change   
   FROM   MIG_IL_IMPORT_SUMMARY    
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND    
    ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO    AND    
          FILE_TYPE           = @IMPORT_POLICY_FILE_TYPE AND    
          [FILE_NAME]         = @IMPORT_FILE_NAME        AND    
          IS_ACTIVE     = 'Y'    
    
    SELECT TOP 1 @REIN_COMAPANY_ID=REIN_COMAPANY_ID FROM MNT_REIN_COMAPANY_LIST MRCL
		INNER JOIN MNT_SYSTEM_PARAMS MSP ON 
		MRCL.REIN_COMAPANY_ID=MSP.SYS_CARRIER_ID     
   --    SELECT   
   --@CUSTOMER_ID  ,      
   --@POLICY_ID  ,        
   --@POLICY_VERSION_ID  
         
         
         
    -------------------------------------------------------          
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT    
   -------------------------------------------------------     
    IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)    
        SET @ERROR_NO =53 -- Supplied APPLICATION/POLICY doesn't exists   
          
   -----------------------------------------------------------          
   -- IF POLICY IS DIRECT(14547) TYPE THAN COI CANNOT ADDED  
   -----------------------------------------------------------     
     ELSE IF EXISTS(SELECT * FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_INSURANCE=14547 AND IS_ACTIVE='Y')    
        SET @ERROR_NO =61 --  Co-Insurance not available for Direct.    
       
   -----------------------------------------------------------          
   -- CHECK WHETHER COINSURER IS ALREADY EXISTS FOR POLICY  
   -----------------------------------------------------------     
     ELSE IF EXISTS(SELECT * FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND COMPANY_ID=@COMPANY_ID)    
        SET @ERROR_NO =57 --  COINSURENCE ALREADY EXISTS FOR THIS COMPANY   
   
   -----------------------------------------------------------          
   -- CHECK WHETHER LEADER ALREADY EXISTS FOR POLICY  
   -----------------------------------------------------------     
     ELSE IF @LOOP_LEADER_FOLLOWER= 14548  AND EXISTS(SELECT * FROM POL_CO_INSURANCE WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LEADER_FOLLOWER=14548)    
        SET @ERROR_NO =59 --  LEADER ALREADY EXISTS FOR POLICY  
        
     ELSE IF( @COMPANY_ID=@REIN_COMAPANY_ID AND @LOOP_LEADER_FOLLOWER =14548)
     BEGIN
       IF(@COI_SHARE IS NULL OR @COI_SHARE=0)
        SET @ERROR_NO =142 --  Please enter valid COI share
       --14548--LEADER
     END  
   
 IF(@ERROR_NO>0)    
       BEGIN    
     
           
   -----------------------------------------------------------          
   -- INSERT ERROR DETAILS    
  -----------------------------------------------------------     
     UPDATE MIG_IL_POLICY_COINSURER_DETAILS  
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
     @ERROR_SOURCE_TYPE     = 'COIS'       
         
    
       END     
         
    ELSE  
    BEGIN        
  
  IF(@PROCESS_TYPE=1)--FOR NBS
  BEGIN    
      
    SELECT @COINSURANCE_ID= ISNULL(MAX(COINSURANCE_ID),0) + 1 FROM POL_CO_INSURANCE  WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  and POLICY_VERSION_ID=@POLICY_VERSION_ID  
      
     
  INSERT INTO POL_CO_INSURANCE    
    ([COINSURANCE_ID],         
    [COMPANY_ID],    
    [CUSTOMER_ID],    
    [POLICY_ID],    
    [POLICY_VERSION_ID],    
    [CO_INSURER_NAME],    
    [LEADER_FOLLOWER],    
    [COINSURANCE_PERCENT],    
    [COINSURANCE_FEE],    
    [BROKER_COMMISSION],    
    [TRANSACTION_ID],    
    [LEADER_POLICY_NUMBER],    
    [IS_ACTIVE],    
    [CREATED_BY],    
    [CREATED_DATETIME],    
    [MODIFIED_BY],    
    [LAST_UPDATED_DATETIME],    
    [BRANCH_COINSURANCE_ID],    
    [ENDORSEMENT_POLICY_NUMBER]    
  )    
   (SELECT   
       @COINSURANCE_ID,    
    @COMPANY_ID,    
    @CUSTOMER_ID,    
    @POLICY_ID,    
    @POLICY_VERSION_ID,    
    '',    
    LEADER_FOLLOWER,    
    COI_SHARE, --COINSURANCE_PERCENT    
    COINSURANCE_FEE,    
    NULL,  --BROKER_COMMISSION    
    TRANSACTION_ID,    
    LEADER_POLICY_NUMBER,    
    'Y', --IS_ACTIVE    
    dbo.fun_GetDefaultUserID(),    
    GETDATE(), --CREATED_DATETIME    
    NULL,    
    NULL, --LAST_UPDATED_DATETIME    
    NULL, --BRANCH_COINSURANCE_ID    
    LEADER_ENDORSEMENT_NUMBER --ENDORSEMENT_POLICY_NUMBER    ,
    
    FROM MIG_IL_POLICY_COINSURER_DETAILS    
    WHERE      IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID      
     AND       IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO           
   )    
     
  END
 --========================================================
 -- FOR POLICY ENDORSEMENT PROCESS --- ADDED BY PRADEEP 
 --========================================================
  ELSE IF(@PROCESS_TYPE=3)
  BEGIN
   ------------------------------------------------------          
   -- GET @COINSURANCE_ID
   -------------------------------------------------------     
   SELECT @COINSURANCE_ID       = IMPORTED_RECORD_ID
   FROM   MIG_IL_IMPORT_SUMMARY    
   WHERE  POLICY_SEQUENTIAL			= @LOOP_POLICY_SEQUANCE_NO AND    
    ENDORSEMENT_SEQUENTIAL			= @LOOP_END_SEQUANCE_NO    AND    
          FILE_TYPE					= @IMPORT_COINSURENCE_FILE_TYPE AND    
          [FILE_NAME]				= @IMPORT_FILE_NAME        AND    
          IS_ACTIVE					= 'Y'    
   
   -- IF COINSURANCE IS EXISTS THEN UPDATE EXISTING COINSURANCE DETAILS
   IF(@COINSURANCE_ID>0)
   BEGIN
			 ------------------------------------        
			 -- UPDATE IMPORT DETAILS  
			 ------------------------------------           
		   UPDATE MIG_IL_POLICY_COINSURER_DETAILS  
		   SET    CUSTOMER_ID    = @CUSTOMER_ID,  
				  POLICY_ID         = @POLICY_ID,  
				  POLICY_VERSION_ID = @POLICY_VERSION_ID,  
				  IS_PROCESSED      = 'Y'  
		   WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND  
				  IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO  
          
          -- UPDATE EXISTING DETAILS
		   UPDATE POL_CO_INSURANCE               
		   SET	 
				[LEADER_FOLLOWER]			=	CASE WHEN T.LEADER_FOLLOWER IS NULL OR T.LEADER_FOLLOWER=0 THEN PCI.[LEADER_FOLLOWER] ELSE T.LEADER_FOLLOWER END  
				,[COINSURANCE_PERCENT]		=	CASE WHEN T.COI_SHARE IS NULL OR T.COI_SHARE=0 THEN PCI.[COINSURANCE_PERCENT] ELSE T.COI_SHARE END  
				,[COINSURANCE_FEE]			=	CASE WHEN T.COINSURANCE_FEE IS NULL OR T.COINSURANCE_FEE=0 THEN PCI.[COINSURANCE_FEE] ELSE T.COINSURANCE_FEE END  
				,[TRANSACTION_ID]			=	CASE WHEN T.TRANSACTION_ID IS NULL OR T.TRANSACTION_ID='0' THEN PCI.[TRANSACTION_ID] ELSE T.TRANSACTION_ID END 
				,[LEADER_POLICY_NUMBER]		=	CASE WHEN T.LEADER_POLICY_NUMBER IS NULL OR T.LEADER_POLICY_NUMBER=0 THEN PCI.[LEADER_POLICY_NUMBER] ELSE T.LEADER_POLICY_NUMBER END 
				,[ENDORSEMENT_POLICY_NUMBER]=	CASE WHEN T.LEADER_ENDORSEMENT_NUMBER IS NULL OR T.LEADER_ENDORSEMENT_NUMBER=0 THEN PCI.[ENDORSEMENT_POLICY_NUMBER] ELSE T.LEADER_ENDORSEMENT_NUMBER END 
				,[MODIFIED_BY]				=	T.MODIFIED_BY
				,[LAST_UPDATED_DATETIME]	=	T.LAST_UPDATED_DATETIME
		      
			 FROM POL_CO_INSURANCE PCI INNER JOIN
			 (
				SELECT  CUSTOMER_ID
					   ,POLICY_ID
					   ,POLICY_VERSION_ID
					   ,LEADER_FOLLOWER
					   ,COI_SHARE
					   ,COINSURANCE_FEE
					   ,TRANSACTION_ID
					   ,LEADER_POLICY_NUMBER
					   ,LEADER_ENDORSEMENT_NUMBER
					   ,MODIFIED_BY
					   ,LAST_UPDATED_DATETIME
				FROM   MIG_IL_POLICY_COINSURER_DETAILS 
				WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
					   IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
				   
			 )T ON T.CUSTOMER_ID= PCI.CUSTOMER_ID AND T.POLICY_ID=PCI.POLICY_ID AND T.POLICY_VERSION_ID=PCI.POLICY_VERSION_ID
		     
			 WHERE PCI.CUSTOMER_ID =@CUSTOMER_ID AND PCI.POLICY_ID=@POLICY_ID AND PCI.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PCI.COINSURANCE_ID=@COINSURANCE_ID
   END
   ELSE
   BEGIN
		     
	  SELECT @COINSURANCE_ID= ISNULL(MAX(COINSURANCE_ID),0) + 1 FROM POL_CO_INSURANCE  WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  and POLICY_VERSION_ID=@POLICY_VERSION_ID  
	      
	     
	  INSERT INTO POL_CO_INSURANCE    
		([COINSURANCE_ID],         
		[COMPANY_ID],    
		[CUSTOMER_ID],    
		[POLICY_ID],    
		[POLICY_VERSION_ID],    
		[CO_INSURER_NAME],    
		[LEADER_FOLLOWER],    
		[COINSURANCE_PERCENT],    
		[COINSURANCE_FEE],    
		[BROKER_COMMISSION],    
		[TRANSACTION_ID],    
		[LEADER_POLICY_NUMBER],    
		[IS_ACTIVE],    
		[CREATED_BY],    
		[CREATED_DATETIME],    
		[MODIFIED_BY],    
		[LAST_UPDATED_DATETIME],    
		[BRANCH_COINSURANCE_ID],    
		[ENDORSEMENT_POLICY_NUMBER]    
	  )    
	   (SELECT   
		   @COINSURANCE_ID,    
		@COMPANY_ID,    
		@CUSTOMER_ID,    
		@POLICY_ID,    
		@POLICY_VERSION_ID,    
		'',    
		LEADER_FOLLOWER,    
		COI_SHARE, --COINSURANCE_PERCENT    
		COINSURANCE_FEE,    
		NULL,  --BROKER_COMMISSION    
		TRANSACTION_ID,    
		LEADER_POLICY_NUMBER,    
		'Y', --IS_ACTIVE    
		dbo.fun_GetDefaultUserID(),    
		GETDATE(), --CREATED_DATETIME    
		NULL,    
		NULL, --LAST_UPDATED_DATETIME    
		NULL, --BRANCH_COINSURANCE_ID    
		LEADER_ENDORSEMENT_NUMBER --ENDORSEMENT_POLICY_NUMBER    ,
	    
		FROM MIG_IL_POLICY_COINSURER_DETAILS    
		WHERE      IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID      
		 AND       IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO           
	   )    
	     
   END
   
  END   
  
   ------------------------------------        
     -- UPDATE IMPORT DETAILS  
     ------------------------------------           
   UPDATE MIG_IL_POLICY_COINSURER_DETAILS  
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
     @FILE_TYPE       = @IMPORT_COINSURENCE_FILE_TYPE,  
     @FILE_NAME              = @IMPORT_FILE_NAME,  
     @CUSTOMER_SEQUENTIAL    = NULL,  
     @POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO,   
     @ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,  
     @IMPORT_SEQUENTIAL      = @LOOP_COINSURANCE_SEQUENTIAL,  
     @IMPORT_SEQUENTIAL2     = NULL,  
     @LOB_ID       = @LOB_ID ,   
     @IMPORTED_RECORD_ID     = @COINSURANCE_ID,
     @PROCESS_TYPE			= @PROCESS_TYPE    --- Change                    
   
 END        
  
  
   SET @COUNTER+=1  
   
  END -- END OF WHILE  
-------------------------------- INSERT INTO POL_CO_INSURANCE     
  
  
    
END  TRY  
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