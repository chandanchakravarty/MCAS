
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_BENEFICIARY_DETAILS]    Script Date: 12/02/2011 15:59:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_BENEFICIARY_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_BENEFICIARY_DETAILS]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_BENEFICIARY_DETAILS]    Script Date: 12/02/2011 15:59:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_POLICY_BENEFICIARY_DETAILS]                                                 
Created by            : Puneet Kumar
Date                  : 04 OCT 2011                                                          
Purpose               : Insert Risk Info
Modified by           : Pradeep  Kushwaha
Date                  : OCT 21 2011
Purpose               : Insert beneficiary Details
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_INSERT_POLICY_BENEFICIARY_DETAILS]   888                                       
------   ------------       -------------------------*/    
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_BENEFICIARY_DETAILS]   
  
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
  
  
DECLARE @CUSTOMER_CODE VARCHAR(20)  
DECLARE @CUSTOMER_ID INT  
DECLARE @POLICY_ID INT  
DECLARE @POLICY_VERSION_ID INT  
DECLARE @POLICY_NUMBER NVARCHAR(21)  
DECLARE @ENDORSEMENT_NUMBER INT 
DECLARE @BENEFICIARY_SHARE_PERCENTAGE DECIMAL(18,2)
DECLARE @BENEFICIARY_ID		INT
DECLARE @BENEFICIARY_NAME   NVARCHAR(250)
DECLARE @BENEFICIARY_RELATION NVARCHAR(250)


DECLARE @LOOP_POLICY_SEQUANCE_NO INT      
DECLARE @LOOP_END_SEQUANCE_NO INT      
DECLARE @LOOP_RISK_SEQUANCE_NO INT  
DECLARE @LOOP_BENEFICIARY_SEQUANCE_NO INT  
DECLARE @LOOP_IMPORT_SERIAL_NO INT      
DECLARE @COUNTER INT  =1    
DECLARE @MAX_RECORD_COUNT INT     
DECLARE @ERROR_NO INT=0    
DECLARE @IMPORT_SERIAL_NO INT
DECLARE @RISK_ID   INT

DECLARE @IMPORT_FILE_NAME NVARCHAR(50)   
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE
DECLARE @IMPORT_BENEFICIARY_FILE_TYPE INT = 14967 -- FOR BENEFICIARY
DECLARE @IMPORT_RISK_FILE_TYPE INT = 15008 -- FOR POLICY RISK FILE TYPE
DECLARE @LOB_ID INT  
DECLARE @PROCESS_TYPE INT    --- Change   
  
BEGIN TRY  
  
    
     CREATE TABLE #TEMP_BENEFICIARY    
     (    
	   ID INT IDENTITY(1,1),    
	   POLICY_SEQUENTIAL INT,    
	   ENDORSEMENT_SEQUENTIAL INT,   
	   RISK_SEQUENTIAL INT,   
	   BENEFICIARY_SEQUENTIAL INT,   
	   IMPORT_SERIAL_NO BIGINT,    
	   BENEFICIARY_NAME NVARCHAR(250),
	   BENEFICIARY_SHARE_PERCENTAGE DECIMAL(18,2),
	   BENEFICIARY_RELATION NVARCHAR(250)
	      
     )    
  
  
  
  INSERT INTO #TEMP_BENEFICIARY    
     (    
		 POLICY_SEQUENTIAL ,    
		 ENDORSEMENT_SEQUENTIAL ,   
         IMPORT_SERIAL_NO,    
		 BENEFICIARY_NAME,
		 BENEFICIARY_SHARE_PERCENTAGE,
		 BENEFICIARY_RELATION,
		 RISK_SEQUENTIAL,
		 BENEFICIARY_SEQUENTIAL
             
     )    
     (    
      SELECT POLICY_SEQUENTIAL ,    
			 ENDORSEMENT_SEQUENTIAL , 
			 IMPORT_SERIAL_NO,    
			 BENEFICIARY_NAME,
		     BENEFICIARY_SHARE_PERCENTAGE,
			 BENEFICIARY_RELATION  ,
			 RISK_LOCATION_SEQUENTIAL,
			 BENEFICIARY_SEQUENTIAL    
      FROM MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS WITH(NOLOCK)    
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0    
     )    

  
   ------------------------------------          
   -- GET MAX RECOUNT COUNT    
   ------------------------------------       
    SELECT @MAX_RECORD_COUNT = COUNT(ID)     
    FROM   #TEMP_BENEFICIARY     
  
   ------------------------------------      
   -- GET FILE NAME
   ------------------------------------   
   IF(@MAX_RECORD_COUNT>0)
   BEGIN
   
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9) 
    FROM  MIG_IL_IMPORT_REQUEST_FILES
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND
          IMPORT_FILE_TYPE   = @IMPORT_BENEFICIARY_FILE_TYPE  
   
   END
   
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)    
  BEGIN  
  
     SET @ERROR_NO=0    
     SET @BENEFICIARY_ID=0
     SET @CUSTOMER_ID=0
     SET @POLICY_ID=0
     SET @POLICY_VERSION_ID=0
     SET @BENEFICIARY_NAME	=''		
	 SET @BENEFICIARY_SHARE_PERCENTAGE=0
	 SET @BENEFICIARY_RELATION=''
	 SET @LOB_ID=0
	 set @RISK_ID=0	
	 SET @PROCESS_TYPE=0
      SELECT 
       @IMPORT_SERIAL_NO        		= IMPORT_SERIAL_NO ,    
       @LOOP_POLICY_SEQUANCE_NO 		= POLICY_SEQUENTIAL  ,  
       @LOOP_END_SEQUANCE_NO    		= ENDORSEMENT_SEQUENTIAL,    
	   @BENEFICIARY_NAME				= BENEFICIARY_NAME,
	   @BENEFICIARY_SHARE_PERCENTAGE	= BENEFICIARY_SHARE_PERCENTAGE,
	   @BENEFICIARY_RELATION			= BENEFICIARY_RELATION,
	   @LOOP_RISK_SEQUANCE_NO			= RISK_SEQUENTIAL,
	   @LOOP_BENEFICIARY_SEQUANCE_NO	= BENEFICIARY_SEQUENTIAL
     FROM   #TEMP_BENEFICIARY (NOLOCK) WHERE ID   = @COUNTER       
  
    
   -------------------------------------------------------      
   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID
   ------------------------------------------------------- 
   SELECT @CUSTOMER_ID		 = CUSTOMER_ID ,
          @POLICY_ID		 = POLICY_ID,
          @POLICY_VERSION_ID = POLICY_VERSION_ID,
          @LOB_ID            = LOB_ID ,
          @PROCESS_TYPE	     = PROCESS_TYPE          
   FROM   MIG_IL_IMPORT_SUMMARY
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND
		  ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO    AND
          FILE_TYPE				  = @IMPORT_POLICY_FILE_TYPE AND
          [FILE_NAME]			  = @IMPORT_FILE_NAME        AND
          IS_ACTIVE			      = 'Y'
          
 
   -------------------------------------------------------      
   -- GET RISK DETAILS
   ------------------------------------------------------- 
   SELECT @RISK_ID           = IMPORTED_RECORD_ID          
   FROM   MIG_IL_IMPORT_SUMMARY
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO AND
		  ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO    AND
		  IMPORT_SEQUENTIAL       = @LOOP_RISK_SEQUANCE_NO   AND
          FILE_TYPE				  = @IMPORT_RISK_FILE_TYPE   AND
          [FILE_NAME]			  = @IMPORT_FILE_NAME        AND
          IS_ACTIVE			      = 'Y'
   
 
   -------------------------------------------------------      
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT
   ------------------------------------------------------- 
    IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)
        SET @ERROR_NO =53 -- Application/Policy does not exists
    
   -------------------------------------------------------      
   -- CHECK RISK EXISTS OR NOT
   ------------------------------------------------------- 
    IF(@RISK_ID IS NULL OR @RISK_ID='' OR @RISK_ID=0)
        SET @ERROR_NO =20 -- RISK IS NOT FOUND IN POLICY
        
    -----------------------------------------------------------      
   -- CHECK WHETHER RISK IS ALREADY EXISTS FOR CUSTOMER
   ----------------------------------------------------------- 
     ELSE IF EXISTS(SELECT 1 FROM POL_BENEFICIARY WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=@RISK_ID AND BENEFICIARY_NAME=@BENEFICIARY_NAME)
        SET @ERROR_NO =214 --  Beneficiary already added in risk
        

  

    IF(@ERROR_NO>0)
      BEGIN
       
       
        -----------------------------------------------------------      
		-- INSERT ERROR DETAILS
		----------------------------------------------------------- 
		   UPDATE MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS 
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
				 @ERROR_SOURCE_TYPE     = 'PBEN'   
				 

       END 
       ELSE
       BEGIN
  IF(@PROCESS_TYPE =1)
  BEGIN
  
		-----------------------------------------------------------      
		-- GET BENEFICIARY ID
		----------------------------------------------------------- 
        SELECT @BENEFICIARY_ID= ISNULL(MAX(BENEFICIARY_ID),0) +1
        FROM POL_BENEFICIARY
        WHERE CUSTOMER_ID		= @CUSTOMER_ID AND 
              POLICY_ID		    = @POLICY_ID AND 
              POLICY_VERSION_ID = @POLICY_VERSION_ID AND
              RISK_ID           = @RISK_ID 
              
                
              
		INSERT INTO [dbo].POL_BENEFICIARY(
			    CUSTOMER_ID,
				POLICY_ID,
				POLICY_VERSION_ID,
				RISK_ID,
				BENEFICIARY_ID,
				BENEFICIARY_NAME,
				BENEFICIARY_SHARE,
				BENEFICIARY_RELATION,
				IS_ACTIVE,
				CREATED_BY,
				CREATED_DATETIME,
				MODIFIED_BY,
				LAST_UPDATED_DATETIME
			   )
			   (
			   SELECT 
			    @CUSTOMER_ID
			   ,@POLICY_ID
			   ,@POLICY_VERSION_ID
			   ,@RISK_ID
			   ,@BENEFICIARY_ID
			   ,@BENEFICIARY_NAME
			   ,@BENEFICIARY_SHARE_PERCENTAGE
			   ,@BENEFICIARY_RELATION
			   ,'Y'
			   ,dbo.fun_GetDefaultUserID()
			   ,GETDATE()
			   ,null
			   ,null
			   
			   )

 END
  --========================================================
 -- FOR POLICY ENDORSEMENT PROCESS --ADDED BY PRADEEP
 --========================================================
 ELSE IF(@PROCESS_TYPE =3)
 BEGIN
    -------------------------------------------------------      
   -- GET@BENEFICIARY_ID
   ------------------------------------------------------- 
   SELECT @BENEFICIARY_ID		  = IMPORTED_RECORD_ID 
   FROM   MIG_IL_IMPORT_SUMMARY
   WHERE  POLICY_SEQUENTIAL       = @LOOP_POLICY_SEQUANCE_NO      AND
		  ENDORSEMENT_SEQUENTIAL  = @LOOP_END_SEQUANCE_NO		  AND
          FILE_TYPE				  = @IMPORT_BENEFICIARY_FILE_TYPE AND
          [FILE_NAME]			  = @IMPORT_FILE_NAME             AND
          IS_ACTIVE			      = 'Y'
   -- IF BENEFICIARY  IS EXISTS THEN UPDATE EXISTING BENEFICIARY DETAILS
   IF(@BENEFICIARY_ID>0)
   BEGIN
			------------------------------------      
			-- UPDATE IMPORT DETAILS
			------------------------------------  		 			 
			 UPDATE MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS
			 SET    CUSTOMER_ID		  = @CUSTOMER_ID,
					POLICY_ID         = @POLICY_ID,
					POLICY_VERSION_ID = @POLICY_VERSION_ID,
					IS_PROCESSED      = 'Y'
			 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
					IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			        
			  -- UPDATE EXISTING DETAILS
       UPDATE POL_BENEFICIARY               
       SET	  
					BENEFICIARY_NAME			=	CASE WHEN T.BENEFICIARY_NAME IS NULL OR T.BENEFICIARY_NAME='' THEN PB.BENEFICIARY_NAME ELSE T.BENEFICIARY_NAME END	 
					,BENEFICIARY_SHARE			=	CASE WHEN T.BENEFICIARY_SHARE_PERCENTAGE IS NULL OR T.BENEFICIARY_SHARE_PERCENTAGE=0 THEN PB.BENEFICIARY_SHARE ELSE T.BENEFICIARY_SHARE_PERCENTAGE END	 
					,BENEFICIARY_RELATION		=	CASE WHEN T.BENEFICIARY_RELATION IS NULL OR T.BENEFICIARY_RELATION='' THEN PB.BENEFICIARY_RELATION ELSE T.BENEFICIARY_RELATION END	 
					,IS_ACTIVE					=	T.IS_DEACTIVATE
					,MODIFIED_BY				=	T.MODIFIED_BY
					,LAST_UPDATED_DATETIME		=	T.LAST_UPDATED_DATETIME	 
					
	     FROM POL_BENEFICIARY PB INNER JOIN
	     (
	        SELECT  CUSTOMER_ID
	               ,POLICY_ID
	               ,POLICY_VERSION_ID
	               ,BENEFICIARY_NAME
				   ,BENEFICIARY_SHARE_PERCENTAGE
				   ,BENEFICIARY_RELATION
				   ,CASE WHEN IS_DEACTIVATE ='Y' THEN 'N' ELSE 'Y' END IS_DEACTIVATE
				   ,dbo.fun_GetDefaultUserID() MODIFIED_BY
				   ,GETDATE() LAST_UPDATED_DATETIME
		    FROM   MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS WITH(NOLOCK) 
		    WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		           IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
	     )T ON T.CUSTOMER_ID= PB.CUSTOMER_ID AND T.POLICY_ID=PB.POLICY_ID AND T.POLICY_VERSION_ID=PB.POLICY_VERSION_ID
	     
	     WHERE PB.CUSTOMER_ID =@CUSTOMER_ID AND PB.POLICY_ID=@POLICY_ID AND PB.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PB.BENEFICIARY_ID=@BENEFICIARY_ID      
   END
   ELSE
   BEGIN
	-----------------------------------------------------------      
		-- GET BENEFICIARY ID
		----------------------------------------------------------- 
        SELECT @BENEFICIARY_ID= ISNULL(MAX(BENEFICIARY_ID),0) +1
        FROM POL_BENEFICIARY
        WHERE CUSTOMER_ID		= @CUSTOMER_ID AND 
              POLICY_ID		    = @POLICY_ID AND 
              POLICY_VERSION_ID = @POLICY_VERSION_ID AND
              RISK_ID           = @RISK_ID 
              
                
              
		INSERT INTO [dbo].POL_BENEFICIARY(
			    CUSTOMER_ID,
				POLICY_ID,
				POLICY_VERSION_ID,
				RISK_ID,
				BENEFICIARY_ID,
				BENEFICIARY_NAME,
				BENEFICIARY_SHARE,
				BENEFICIARY_RELATION,
				IS_ACTIVE,
				CREATED_BY,
				CREATED_DATETIME,
				MODIFIED_BY,
				LAST_UPDATED_DATETIME
			   )
			   (
			   SELECT 
			    @CUSTOMER_ID
			   ,@POLICY_ID
			   ,@POLICY_VERSION_ID
			   ,@RISK_ID
			   ,@BENEFICIARY_ID
			   ,@BENEFICIARY_NAME
			   ,@BENEFICIARY_SHARE_PERCENTAGE
			   ,@BENEFICIARY_RELATION
			   ,'Y'
			   ,dbo.fun_GetDefaultUserID()
			   ,GETDATE()
			   ,null
			   ,null
			   
			   )
   END       
 END
			------------------------------------      
			-- UPDATE IMPORT DETAILS
			------------------------------------  		 			 
			 UPDATE MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS
			 SET    CUSTOMER_ID		  = @CUSTOMER_ID,
					POLICY_ID         = @POLICY_ID,
					POLICY_VERSION_ID = @POLICY_VERSION_ID,
					IS_PROCESSED      = 'Y'
			 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
					IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			        
				------------------------------------          
			-- INSERT IMPORT SUMMARY  
			------------------------------------         
			EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]       
			@IMPORT_REQUEST_ID      = @IMPORT_REQUEST_ID,  
			@IMPORT_SERIAL_NO       = @IMPORT_SERIAL_NO,   
			@CUSTOMER_ID            = @CUSTOMER_ID ,  
			@POLICY_ID              = @POLICY_ID,  
			@POLICY_VERSION_ID      = @POLICY_VERSION_ID,  
			@IS_ACTIVE              = 'Y',  
			@IS_PROCESSED           = 'Y',  
			@FILE_TYPE				= @IMPORT_BENEFICIARY_FILE_TYPE,  
			@FILE_NAME              = @IMPORT_FILE_NAME,  
			@CUSTOMER_SEQUENTIAL    = NULL,  
			@POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO,   
			@ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,  
			@IMPORT_SEQUENTIAL      = @LOOP_RISK_SEQUANCE_NO,  
			@IMPORT_SEQUENTIAL2     = @LOOP_BENEFICIARY_SEQUANCE_NO,  
			@LOB_ID					= @LOB_ID ,   
			@IMPORTED_RECORD_ID     = @BENEFICIARY_ID ,
			@PROCESS_TYPE			= @PROCESS_TYPE           
           

    END
    SET @COUNTER+=1 
       
  END  -- END OF WHILE LOOP
  
END TRY  
BEGIN CATCH    
     
 SELECT     
    @ERROR_NUMBER    = ERROR_NUMBER(),    
    @ERROR_SEVERITY  = ERROR_SEVERITY(),    
    @ERROR_STATE = ERROR_STATE(),    
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
