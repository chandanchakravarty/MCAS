/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_CLAUSE_DETAILS]    Script Date: 12/02/2011 15:59:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_CLAUSE_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_CLAUSE_DETAILS]
GO
GO


/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_CLAUSE_DETAILS]    Script Date: 12/02/2011 15:59:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                              
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_POLICY_CLAUSE_DETAILS]                                                          
Created by            : Santosh Kumar Gautam                                                             
Date                  : 29 Sept 2011                                                            
Purpose               : Insert Policy Clause  
Revison History       :                                                              
Used In               : INITIAL LOAD                 
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc [PROC_MIG_IL_INSERT_POLICY_CLAUSE_DETAILS]                                                  
------   ------------       -------------------------*/      
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_CLAUSE_DETAILS]     
    
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
    
    
   
DECLARE @CUSTOMER_ID INT    
DECLARE @POLICY_ID INT    
DECLARE @POLICY_VERSION_ID INT    
DECLARE @POLICY_NUMBER NVARCHAR(21)    
DECLARE @ENDORSEMENT_NUMBER INT    

  
    
    
DECLARE @LOOP_CLAUSE_CODE NVARCHAR(50)             
DECLARE @LOOP_POLICY_SEQUANCE_NO INT        
DECLARE @LOOP_END_SEQUANCE_NO INT     
DECLARE @LOOP_CLAUSE_SEQUANCE_NO INT   
DECLARE @CLAUSE_TITLE NVARCHAR(MAX)    
DECLARE @LOOP_CLAUSE_TITLE NVARCHAR(MAX)         
DECLARE @LOOP_CLAUSE_FILE_NAME NVARCHAR(MAX)         
   
DECLARE @LOOP_IMPORT_SERIAL_NO INT        
DECLARE @COUNTER INT  =1      
DECLARE @MAX_RECORD_COUNT INT       
DECLARE @ERROR_NO INT=0      
DECLARE @IMPORT_SERIAL_NO INT  
DECLARE @CLAUSE_ID   INT  
DECLARE @POL_CLAUSE_ID   INT  
DECLARE @LOB_ID   INT  
DECLARE @SUB_LOB_ID   INT  
DECLARE @IS_USER_DEFINED_CLAUSE  NVARCHAR(50)  
  
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)     
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE  
DECLARE @IMPORT_CLAUSE_FILE_TYPE INT = 14962 -- FOR CLAUSES FILE TYPE  
DECLARE @PROCESS_TYPE INT    --- Change     
    
BEGIN TRY    
    
      
     CREATE TABLE #TEMP_POLICY_CLAUSE      
     (      
    ID INT IDENTITY(1,1),      
    POLICY_SEQUANCE_NO INT NULL,   
    CLAUSE_SEQUANCE_NO INT NULL,    
    END_SEQUANCE_NO INT NULL,     
    IMPORT_SERIAL_NO BIGINT NULL,      
    CLAUSE_CODE NVARCHAR(50) NULL,  
    IS_USER_DEFINED_CLAUSE  NVARCHAR(50) NULL, 
       CLAUSE_TITLE  NVARCHAR(MAX)  NULL
     )      
    
    
    
  INSERT INTO #TEMP_POLICY_CLAUSE      
     (      
   POLICY_SEQUANCE_NO ,      
   END_SEQUANCE_NO ,     
   IMPORT_SERIAL_NO,      
   CLAUSE_CODE   ,  
   IS_USER_DEFINED_CLAUSE  ,
   CLAUSE_TITLE,
   CLAUSE_SEQUANCE_NO
     )      
     (      
      SELECT POLICY_SEQUENTIAL,    
		ENDORSEMENT_SEQUENTIAL,    
		IMPORT_SERIAL_NO,    
       CLAUSE_CODE       ,  
       USER_DEFINED_CLAUSE ,
       CLAUSE_TITLE  ,
       CLAUSE_SEQUENTIAL                   
      FROM MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK)      
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0      
     )      
  
    
   ------------------------------------            
   -- GET MAX RECOUNT COUNT      
   ------------------------------------         
    SELECT @MAX_RECORD_COUNT = COUNT(ID)       
    FROM   #TEMP_POLICY_CLAUSE       
    
   ------------------------------------        
   -- GET FILE NAME  
   ------------------------------------     
   IF(@MAX_RECORD_COUNT>0)  
   BEGIN  
     
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)   
    FROM  MIG_IL_IMPORT_REQUEST_FILES WITH(NOLOCK)  
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND  
          IMPORT_FILE_TYPE   = @IMPORT_CLAUSE_FILE_TYPE    
     
   END  
     
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)        BEGIN    
    
     SET @ERROR_NO=0      
     SET @POL_CLAUSE_ID=0  
     SET @CLAUSE_ID=0  
     SET @CUSTOMER_ID=0  
     SET @POLICY_ID=0  
     SET @POLICY_VERSION_ID=0  
     SET @LOB_ID=0  
     SET @SUB_LOB_ID=0  
     SET @IS_USER_DEFINED_CLAUSE=''  
     SET @LOOP_CLAUSE_TITLE=''
     SET @PROCESS_TYPE=0
       
      SELECT   
       @IMPORT_SERIAL_NO        = IMPORT_SERIAL_NO ,      
       @LOOP_POLICY_SEQUANCE_NO = POLICY_SEQUANCE_NO  ,    
       @LOOP_END_SEQUANCE_NO    = END_SEQUANCE_NO,      
       @LOOP_CLAUSE_CODE        = CLAUSE_CODE,  
       @IS_USER_DEFINED_CLAUSE  = IS_USER_DEFINED_CLAUSE ,
       @LOOP_CLAUSE_TITLE       = CLAUSE_TITLE ,
       @LOOP_CLAUSE_SEQUANCE_NO = CLAUSE_SEQUANCE_NO
     FROM  #TEMP_POLICY_CLAUSE (NOLOCK) WHERE ID   = @COUNTER         
    
      
   -------------------------------------------------------        
   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID  
   -------------------------------------------------------   
   SELECT @CUSTOMER_ID           = CUSTOMER_ID ,  
          @POLICY_ID			 = POLICY_ID,  
          @POLICY_VERSION_ID     = POLICY_VERSION_ID ,
          @PROCESS_TYPE			 = PROCESS_TYPE	 
   FROM   MIG_IL_IMPORT_SUMMARY WITH(NOLOCK)  
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND  
          ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO    AND  
          FILE_TYPE               = @IMPORT_POLICY_FILE_TYPE AND  
          [FILE_NAME]             = @IMPORT_FILE_NAME        AND  
          IS_ACTIVE         = 'Y'  
        
    IF(@IS_USER_DEFINED_CLAUSE!='Y')      
      SET @IS_USER_DEFINED_CLAUSE='N'  
        
 -----------------------------------------------------------        
 -- GET LOB AND SUB LOB ID  
 -----------------------------------------------------------   
 SELECT @LOB_ID=ISNULL(POLICY_LOB,0),@SUB_LOB_ID=ISNULL(POLICY_SUBLOB,0)  
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
 WHERE CUSTOMER_ID  = @CUSTOMER_ID AND   
    POLICY_ID      = @POLICY_ID   AND  
    POLICY_VERSION_ID = @POLICY_VERSION_ID   
   
 -----------------------------------------------------------        
 -- GET CLAUSE ID  
 -----------------------------------------------------------      
    SELECT TOP 1 @CLAUSE_ID      = CLAUSE_ID   ,
          @CLAUSE_TITLE          = CLAUSE_TITLE,
          @LOOP_CLAUSE_FILE_NAME = ATTACH_FILE_NAME
    FROM  MNT_CLAUSES WITH(NOLOCK)  
    WHERE LOB_ID=@LOB_ID AND SUBLOB_ID=@SUB_LOB_ID AND CLAUSE_CODE=@LOOP_CLAUSE_CODE  
         
   -------------------------------------------------------        
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT  
   -------------------------------------------------------   
    IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)  
        SET @ERROR_NO =53 -- Application/Policy does not exists  
   -----------------------------------------------------------        
   -- CHECK WHETHER CLAUSES IS ALREADY EXISTS FOR CUSTOMER  
   -----------------------------------------------------------   
    ELSE IF (@IS_USER_DEFINED_CLAUSE='Y')AND 
             EXISTS(SELECT CUSTOMER_ID FROM POL_CLAUSES WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND  (CLAUSE_CODE=@LOOP_CLAUSE_CODE OR CLAUSE_TITLE=@LOOP_CLAUSE_TITLE))  
        SET @ERROR_NO =63 --  User defined clause is already added in this policy    
    ELSE IF (@IS_USER_DEFINED_CLAUSE='N' AND (@CLAUSE_ID IS NULL OR @CLAUSE_ID='' OR @CLAUSE_ID=0 ))         
        SET @ERROR_NO =65  --   Invalid Clause Code  
    ELSE IF (@IS_USER_DEFINED_CLAUSE='N' AND EXISTS(SELECT CUSTOMER_ID FROM POL_CLAUSES WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND  CLAUSE_ID=@CLAUSE_ID) )  
        SET @ERROR_NO =64 --  System defined clause is already added in this policy  
  
  
    IF(@ERROR_NO>0)  
       BEGIN  
         
         
        -----------------------------------------------------------        
  -- INSERT ERROR DETAILS  
  -----------------------------------------------------------   
     UPDATE MIG_IL_POLICY_CLAUSES_DETAILS  
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
     @ERROR_SOURCE_TYPE     = 'CLSS'     
       
  
       END   
       ELSE  
       BEGIN  
         
    
  -----------------------------------------------------------        
  -- SET CLAUSE ID  
  -----------------------------------------------------------   
        IF(@IS_USER_DEFINED_CLAUSE='Y')  
           SET @CLAUSE_ID=0  
  
  IF(@PROCESS_TYPE=1)
  BEGIN        
  -----------------------------------------------------------        
  -- GET POLICY CLAUSE ID  
  -----------------------------------------------------------     
  SELECT @POL_CLAUSE_ID= ISNULL(MAX(POL_CLAUSE_ID),0) +1  
  FROM [POL_CLAUSES] WITH(NOLOCK)  
  WHERE CUSTOMER_ID  = @CUSTOMER_ID AND   
     POLICY_ID      = @POLICY_ID   AND  
     POLICY_VERSION_ID = @POLICY_VERSION_ID   
          
                
  INSERT INTO [dbo].[POL_CLAUSES]  
           ([POL_CLAUSE_ID]  
           ,[CUSTOMER_ID]  
           ,[POLICY_ID]  
           ,[POLICY_VERSION_ID]  
           ,[CLAUSE_ID]  
           ,[CLAUSE_TITLE]  
           ,[CLAUSE_DESCRIPTION]  
           ,[IS_ACTIVE]  
           ,[CREATED_BY]  
           ,[CREATED_DATETIME]            
           ,[SUSEP_LOB_ID]  
           ,[CLAUSE_TYPE]  
           ,[ATTACH_FILE_NAME]  
           ,[CLAUSE_CODE]  
           ,[PREVIOUS_VERSION_ID])  
           (  
           SELECT  
            @POL_CLAUSE_ID  
           ,@CUSTOMER_ID  
           ,@POLICY_ID  
           ,@POLICY_VERSION_ID  
           ,@CLAUSE_ID  
           ,CASE WHEN @IS_USER_DEFINED_CLAUSE='Y' THEN CLAUSE_TITLE  ELSE @CLAUSE_TITLE END
           ,NULL  
           ,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y'  END
           ,dbo.fun_GetDefaultUserID()  
           ,GETDATE()  
           ,@SUB_LOB_ID             
           ,14696 --CLAUSE_TYPE -- 14696 => FOR ATTACHMENT  
           ,CASE WHEN @IS_USER_DEFINED_CLAUSE='Y' THEN [FILE_NAME]  ELSE @LOOP_CLAUSE_FILE_NAME END  
           ,CLAUSE_CODE  
           ,NULL-- PREVIOUS_VERSION_ID  
           FROM MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK)  
     WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND  
        IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO  
           )  
  
  END
  ELSE IF(@PROCESS_TYPE=3)
  BEGIN
  
   -------------------------------------------------------        
   -- GET CLASES DETAILS added by pradeep
   -------------------------------------------------------   
   SELECT @POL_CLAUSE_ID          = IMPORTED_RECORD_ID  
   FROM   MIG_IL_IMPORT_SUMMARY WITH(NOLOCK)  
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND  
          ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO    AND  
          FILE_TYPE               = @IMPORT_CLAUSE_FILE_TYPE AND   
          [FILE_NAME]             = @IMPORT_FILE_NAME        AND  
          IS_ACTIVE				  = 'Y' 
   -----------------------------------------------------------------------------      
    -- IF CLAUSES IS EXISTS THEN UPDATE CLAUSES FOR POLICY ENDORSEMENT - ADDED BY PRADEEP
    -----------------------------------------------------------------------------      
   IF(@POL_CLAUSE_ID >0)
   BEGIN
    			 
		  UPDATE MIG_IL_POLICY_CLAUSES_DETAILS  
		  SET     CUSTOMER_ID		= @CUSTOMER_ID,  
				  POLICY_ID         = @POLICY_ID,  
				  POLICY_VERSION_ID = @POLICY_VERSION_ID,  
				  IS_PROCESSED      = 'Y'  
		  WHERE  IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND  
				 IMPORT_SERIAL_NO   = @IMPORT_SERIAL_NO  
   
		-- UPDATE EXISTING DETAILS
       UPDATE [POL_CLAUSES]               
       SET	     	
		 
            [CLAUSE_ID]					=  CASE WHEN T.CLAUSE_ID IS NULL OR T.CLAUSE_ID=0 THEN PCAS.[CLAUSE_ID] ELSE T.CLAUSE_ID END	 
           ,[CLAUSE_TITLE]				=  CASE WHEN T.CLAUSE_TITLE IS NULL OR T.CLAUSE_TITLE='' THEN  PCAS.[CLAUSE_TITLE] ELSE T.CLAUSE_TITLE END	 
           ,[IS_ACTIVE]					=  T.IS_DEACTIVATE
           ,[MODIFIED_BY]				=  T.MODIFIED_BY
           ,[LAST_UPDATED_DATETIME]     =  T.LAST_UPDATED_DATETIME   
           ,[SUSEP_LOB_ID]				=  CASE WHEN T.SUB_LOB_ID IS NULL OR T.SUB_LOB_ID=0 THEN [SUSEP_LOB_ID] ELSE T.SUB_LOB_ID END	 
           ,[CLAUSE_TYPE]				=  CASE WHEN T.[CLAUSE_TYPE] IS NULL OR T.[CLAUSE_TYPE]=0 THEN  PCAS.[CLAUSE_TYPE] ELSE T.[CLAUSE_TYPE] END	
           ,[ATTACH_FILE_NAME]			= CASE WHEN T.[FILE_NAME] IS NULL OR T.[FILE_NAME]='' THEN  PCAS.[ATTACH_FILE_NAME] ELSE T.[FILE_NAME] END 
           ,[CLAUSE_CODE]				= CASE WHEN T.CLAUSE_CODE IS NULL OR T.CLAUSE_CODE='' THEN  PCAS.[CLAUSE_CODE] ELSE T.CLAUSE_CODE END 
          
	     FROM [POL_CLAUSES] PCAS INNER JOIN
	     (
	        SELECT  
	        CUSTOMER_ID  
           ,POLICY_ID  
           ,POLICY_VERSION_ID  
		   ,@CLAUSE_ID  AS CLAUSE_ID
           ,CASE WHEN @IS_USER_DEFINED_CLAUSE='Y' THEN CLAUSE_TITLE  ELSE @CLAUSE_TITLE END AS CLAUSE_TITLE
           ,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END AS IS_DEACTIVATE
           ,dbo.fun_GetDefaultUserID()  AS MODIFIED_BY
           ,GETDATE()  AS LAST_UPDATED_DATETIME
           ,@SUB_LOB_ID  AS SUB_LOB_ID           
           ,14696 AS CLAUSE_TYPE  
           ,CASE WHEN @IS_USER_DEFINED_CLAUSE='Y' THEN [FILE_NAME]  ELSE @LOOP_CLAUSE_FILE_NAME END  AS [FILE_NAME] 
           ,CLAUSE_CODE  
		    FROM   MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK)
		    WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		           IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
	     )T ON T.CUSTOMER_ID= PCAS.CUSTOMER_ID AND T.POLICY_ID=PCAS.POLICY_ID AND T.POLICY_VERSION_ID=PCAS.POLICY_VERSION_ID
	     
	     WHERE PCAS.CUSTOMER_ID =@CUSTOMER_ID AND PCAS.POLICY_ID=@POLICY_ID AND PCAS.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PCAS.POL_CLAUSE_ID=@POL_CLAUSE_ID
   END
   -----------------------------------------------------------------------------      
    -- IF CLAUSES IS NOT EXISTS THEN ADD NEW CLAUSES FOR POLICY ENDORSEMENT - ADDED BY PRADEEP
    -----------------------------------------------------------------------------      
   ELSE
   BEGIN
		  -----------------------------------------------------------        
		  -- GET POLICY CLAUSE ID  
		  -----------------------------------------------------------     
		  SELECT @POL_CLAUSE_ID= ISNULL(MAX(POL_CLAUSE_ID),0) +1  
		  FROM [POL_CLAUSES] WITH(NOLOCK)  
		  WHERE CUSTOMER_ID  = @CUSTOMER_ID AND   
			 POLICY_ID      = @POLICY_ID   AND  
			 POLICY_VERSION_ID = @POLICY_VERSION_ID   
		     
		                
		  INSERT INTO [dbo].[POL_CLAUSES]  
           ([POL_CLAUSE_ID]  
           ,[CUSTOMER_ID]  
           ,[POLICY_ID]  
           ,[POLICY_VERSION_ID]  
           ,[CLAUSE_ID]  
           ,[CLAUSE_TITLE]  
           ,[CLAUSE_DESCRIPTION]  
           ,[IS_ACTIVE]  
           ,[CREATED_BY]  
           ,[CREATED_DATETIME]            
           ,[SUSEP_LOB_ID]  
           ,[CLAUSE_TYPE]  
           ,[ATTACH_FILE_NAME]  
           ,[CLAUSE_CODE]  
           ,[PREVIOUS_VERSION_ID])  
           (  
           SELECT  
            @POL_CLAUSE_ID  
           ,@CUSTOMER_ID  
           ,@POLICY_ID  
           ,@POLICY_VERSION_ID  
           ,@CLAUSE_ID  
           ,CASE WHEN @IS_USER_DEFINED_CLAUSE='Y' THEN CLAUSE_TITLE  ELSE @CLAUSE_TITLE END
           ,NULL  
           ,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END
           ,dbo.fun_GetDefaultUserID()  
           ,GETDATE()  
           ,@SUB_LOB_ID             
           ,14696 --CLAUSE_TYPE -- 14696 => FOR ATTACHMENT  
           ,CASE WHEN @IS_USER_DEFINED_CLAUSE='Y' THEN [FILE_NAME]  ELSE @LOOP_CLAUSE_FILE_NAME END  
           ,CLAUSE_CODE  
           ,NULL-- PREVIOUS_VERSION_ID  
           FROM MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK)  
		   WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND  
				IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO  
           )  
   
  END    
  END          
  ------------------------------------        
     -- UPDATE IMPORT DETAILS  
     ------------------------------------           
   UPDATE MIG_IL_POLICY_CLAUSES_DETAILS  
   SET    CUSTOMER_ID    = @CUSTOMER_ID,  
          POLICY_ID         = @POLICY_ID,  
          POLICY_VERSION_ID = @POLICY_VERSION_ID,  
          IS_PROCESSED      = 'Y'  
   WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND  
         IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO  
  
  
  
  
  	  EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]     
		 @IMPORT_REQUEST_ID 	 = @IMPORT_REQUEST_ID,
		 @IMPORT_SERIAL_NO  	 = @IMPORT_SERIAL_NO,	
		 @CUSTOMER_ID		  	 = @CUSTOMER_ID	,
		 @POLICY_ID		  		 = NULL,
		 @POLICY_VERSION_ID 	 = NULL,
		 @IS_ACTIVE		  		 = 'Y',
		 @IS_PROCESSED	  		 = 'Y',
		 @FILE_TYPE		  		 = @IMPORT_CLAUSE_FILE_TYPE,
		 @FILE_NAME              = @IMPORT_FILE_NAME,
		 @CUSTOMER_SEQUENTIAL    = NULL,
		 @POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO, 
		 @ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,
		 @IMPORT_SEQUENTIAL      = @LOOP_CLAUSE_SEQUANCE_NO,
		 @IMPORT_SEQUENTIAL2     = NULL,
		 @LOB_ID		  		 = @LOB_ID , 
		 @IMPORTED_RECORD_ID     = @POL_CLAUSE_ID,
		 @PROCESS_TYPE			 = @PROCESS_TYPE      
				 
    END  
    SET @COUNTER+=1   
         
  END  -- END OF WHILE LOOP  
    
END TRY    
BEGIN CATCH      
       
 SELECT       
@ERROR_NUMBER = ERROR_NUMBER(),      
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


