
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_REINSURENCE_DETAILS]    Script Date: 12/02/2011 16:17:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_REINSURENCE_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_REINSURENCE_DETAILS]
GO
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_REINSURENCE_DETAILS]    Script Date: 12/02/2011 16:17:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                              
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_POLICY_REINSURENCE_DETAILS]                                                                          
Created by            : ATUL KUMAR SINGH                                                                           
Date                  : 28 Sept 2011                                                                            
Purpose               : Insert Policy reinsurence                  
Mofified by           : Pradeep Kushwaha                                                                        
Date                  : 30 nov 2011     
Revison History       :                                                                              
Used In               : INITIAL LOAD                                 
------------------------------------------------------------                                                                              
Date     Review By          Comments                                                
*/        
----delete POL_REINSURANCE_INFO where CUSTOMER_ID=4508 and POLICY_ID=20             
--drop Proc [PROC_MIG_IL_INSERT_POLICY_REINSURENCE_DETAILS]   
--GO                                        
------   ------------       -------------------------                 
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_REINSURENCE_DETAILS]                   
                    
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
DECLARE @LOB_ID   INT                   
                    
DECLARE @LOOP_REINSURENCE INT                          
DECLARE @LOOP_REIN_COMPANY_ID INT                          
DECLARE @LOOP_CONTRACT_FACULTATIVE INT                          
DECLARE @LOOP_CONTRACT VARCHAR(50)            
DECLARE @LOOP_POLICY_SEQUANCE_NO INT                        
DECLARE @LOOP_END_SEQUANCE_NO INT                        
DECLARE @LOOP_IMPORT_SERIAL_NO INT                        
DECLARE @COUNTER INT  =1                      
DECLARE @MAX_RECORD_COUNT INT                       
DECLARE @ERROR_NO INT=0                      
DECLARE @IMPORT_SERIAL_NO INT                  
DECLARE @REINSURENCE_ID INT  
DECLARE @RISK_ID       INT      
DECLARE @RISK_ID_LINK_WITH_CLAIM NVARCHAR(100)     
                  
                  
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)                     
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE                  
DECLARE @IMPORT_REINSURENCE_FILE_TYPE INT = 14942 -- FOR REINSURENCE FILE TYPE                  
DECLARE @PROCESS_TYPE INT    --- Change                   
                    
BEGIN TRY                    
                    
                      
     CREATE TABLE #TEMP_POLICY_REINSURENCE                      
     (                      
		ID INT IDENTITY(1,1),                      
		POLICY_SEQUANCE_NO INT,                      
		END_SEQUANCE_NO INT,                     
		IMPORT_SERIAL_NO BIGINT,                      
		REINSURENCE_SEQUESNCE_NO BIGINT,                  
		REINSURENCE_COMPANY_ID INT NULL,                  
		LOOP_CONTRACT_FACULTATIVE INT NULL,            
		LOOP_CONTRACT  VARCHAR(20)  ,
		RISK_ID_LINK_WITH_CLAIM NVARCHAR(100)          
    )                      
                    
                    
                    
  INSERT INTO #TEMP_POLICY_REINSURENCE                      
     (                      
		POLICY_SEQUANCE_NO ,                      
		END_SEQUANCE_NO ,                     
		IMPORT_SERIAL_NO,                      
		REINSURENCE_SEQUESNCE_NO,                  
		REINSURENCE_COMPANY_ID,                  
		LOOP_CONTRACT_FACULTATIVE,            
		LOOP_CONTRACT            ,
	     RISK_ID_LINK_WITH_CLAIM     
     )                      
     (                      
      SELECT POLICY_SEQUENTIAL,                    
		ENDORSEMENT_SEQUENTIAL,                    
		IMPORT_SERIAL_NO,                    
		REINSURANCE_SEQUENTIAL,                  
		REIN_COMPANY_ID,                  
		CONTRACT_FACULTATIVE,            
		[CONTRACT]          ,
		RISK_ID_LINK_WITH_CLAIM  
      FROM MIG_IL_POLICY_REINSURANCE_DETAILS WITH(NOLOCK)                      
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0                      
  )                      
                  
                    
   ------------------------------------                            
   -- GET MAX RECOUNT COUNT                      
   ------------------------------------                         
    SELECT @MAX_RECORD_COUNT = COUNT(ID)                       
    FROM   #TEMP_POLICY_REINSURENCE                       
                    
   ------------------------------------                        
   -- GET FILE NAME                  
   ------------------------------------                     
   IF(@MAX_RECORD_COUNT>0)                  
   BEGIN                  
                     
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)                   
    FROM  MIG_IL_IMPORT_REQUEST_FILES                  
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND                  
          IMPORT_FILE_TYPE   = @IMPORT_REINSURENCE_FILE_TYPE                    
                     
   END                  
     WHILE(@COUNTER<=@MAX_RECORD_COUNT)                      
  BEGIN                    
                    
     SET @ERROR_NO=0                      
                       
     SET @CUSTOMER_ID=0                  
     SET @POLICY_ID=0                  
     SET @POLICY_VERSION_ID=0                  
     SET @PROCESS_TYPE =0                 
     SET @REINSURENCE_ID=0
      SELECT                   
		@IMPORT_SERIAL_NO				= IMPORT_SERIAL_NO ,                      
		@LOOP_POLICY_SEQUANCE_NO		= POLICY_SEQUANCE_NO  ,                    
		@LOOP_END_SEQUANCE_NO			= END_SEQUANCE_NO,                      
		@LOOP_REIN_COMPANY_ID			= REINSURENCE_COMPANY_ID,                  
		@LOOP_CONTRACT_FACULTATIVE		= LOOP_CONTRACT_FACULTATIVE,            
		@LOOP_CONTRACT					= LOOP_CONTRACT,    
		@LOOP_REINSURENCE				= REINSURENCE_SEQUESNCE_NO  ,
		@RISK_ID_LINK_WITH_CLAIM        = RISK_ID_LINK_WITH_CLAIM               
     FROM   #TEMP_POLICY_REINSURENCE (NOLOCK) WHERE ID   = @COUNTER                         
                    
                      
   -------------------------------------------------------                        
   -- GET CUSTOMER ID, POLICY ID AND POLICY VERSION ID                  
   -------------------------------------------------------                   
   SELECT @CUSTOMER_ID			= CUSTOMER_ID ,                  
          @POLICY_ID			= POLICY_ID,                  
          @POLICY_VERSION_ID	= POLICY_VERSION_ID,    
          @LOB_ID				= LOB_ID ,
          @PROCESS_TYPE			=	PROCESS_TYPE   
   FROM   MIG_IL_IMPORT_SUMMARY                  
   WHERE  POLICY_SEQUENTIAL     = @LOOP_POLICY_SEQUANCE_NO AND     
    ENDORSEMENT_SEQUENTIAL		= @LOOP_END_SEQUANCE_NO    AND                  
          FILE_TYPE				= @IMPORT_POLICY_FILE_TYPE AND                  
         [FILE_NAME]			= @IMPORT_FILE_NAME   AND                  
          IS_ACTIVE				= 'Y'                  
     
    -------------------------------------------------------                        
   -- GET @REINSURENCE_ID FOR ENDORESMENT POLICY ADDED BY PRADEEP       
   -------------------------------------------------------                   
   SELECT @REINSURENCE_ID   = IMPORTED_RECORD_ID    
   FROM   MIG_IL_IMPORT_SUMMARY                  
   WHERE  POLICY_SEQUENTIAL     = @LOOP_POLICY_SEQUANCE_NO AND                  
          ENDORSEMENT_SEQUENTIAL= @LOOP_END_SEQUANCE_NO    AND                  
          FILE_TYPE				= @IMPORT_REINSURENCE_FILE_TYPE  AND                  
          [FILE_NAME]			= @IMPORT_FILE_NAME   AND                  
          IS_ACTIVE				= 'Y'      
                                  
  
                                  
                                  
    ----------------------------------------------------------            
    ---   GET POLICY LOB            
    ----------------------------------------------------------            
    DECLARE @POLICY_LOB INT            
    DECLARE @APP_EFFECTIVE_DATE DATETIME            
    DECLARE @APP_EXPIRY_DATE DATETIME            
    SELECT             
	   @POLICY_LOB			= POLICY_LOB,            
	   @APP_EFFECTIVE_DATE	= APP_EFFECTIVE_DATE,            
	   @APP_EXPIRY_DATE		= APP_EXPIRATION_DATE            
     FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)             
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID            
  -- select @CUSTOMER_ID  CUSTOMER_ID,@LOOP_REIN_COMPANY_ID LOOP_REIN_COMPANY_ID  ,@LOOP_REIN_COMPANY_ID     LOOP_REIN_COMPANY_ID  ,@POLICY_LOB       
   -------------------------------------------------------                         
   -- CHECK WHETHER APPLICATION/POLICY EXISTS OR NOT                  
   -------------------------------------------------------                   
    IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)                  
		SET @ERROR_NO =53 -- Application/Policy does not exists                  
   -----------------------------------------------------------                        
   -- CHECK WHETHER REINSURENCE IS ALREADY EXISTS FOR THIS COMAPNY                  
   -----------------------------------------------------------                   
     ELSE IF  (@REINSURENCE_ID IS NULL OR @REINSURENCE_ID=0) AND EXISTS(SELECT 1 FROM POL_REINSURANCE_INFO (NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID                  
     AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID       
     AND COMPANY_ID=@LOOP_REIN_COMPANY_ID)          
        SET @ERROR_NO =97 --  re-insurence already exists for this company.    
     ELSE IF (@REINSURENCE_ID IS NULL OR @REINSURENCE_ID=0) AND EXISTS(SELECT 1 FROM  [POL_REINSURANCE_BREAKDOWN_DETAILS] (NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID                  
     AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID       
     AND MAJOR_PARTICIPANT=@LOOP_REIN_COMPANY_ID)          
        SET @ERROR_NO =97 --  re-insurence already exists for this company.            
     ELSE IF (@REINSURENCE_ID IS NULL OR @REINSURENCE_ID=0) AND (@LOOP_CONTRACT_FACULTATIVE=14627)          
     BEGIN          
		  IF NOT EXISTS (SELECT 1 FROM MNT_REIN_CONTRACT_LOB A JOIN MNT_REINSURANCE_CONTRACT B ON A.CONTRACT_ID=B.CONTRACT_ID            
		   WHERE CONTRACT_NUMBER=@LOOP_CONTRACT AND A.CONTRACT_LOB=@POLICY_LOB  )                  
		  SET @ERROR_NO=102     -- CONTRACT NOT EXISTS FOR THIS PRODUCT            
		  ELSE IF NOT EXISTS (SELECT 1 FROM MNT_REINSURANCE_CONTRACT (NOLOCK) WHERE CONTRACT_NUMBER=@LOOP_CONTRACT AND (@APP_EFFECTIVE_DATE >=EFFECTIVE_DATE            
		  AND @APP_EXPIRY_DATE<=EXPIRATION_DATE )             
		   )                  
		  SET @ERROR_NO=103     -- POLICY DOES NOT LIE BETWEEN CONTRACT EFFETIVE AND EXPIRATION DATE            
      END     
     ELSE
     BEGIN
     
		   -------------------------------------------------------                        
		   -- GET RISK ID     
		   -------------------------------------------------------                   		  
			IF(@POLICY_LOB IN(9,26))
			BEGIN
			
				SELECT @RISK_ID = PERIL_ID 
				FROM  POL_PERILS WITH(NOLOCK)
				WHERE CUSTOMER_ID      			 = @CUSTOMER_ID AND
					  POLICY_ID        			 = @POLICY_ID   AND
					  POLICY_VERSION_ID			 = @POLICY_VERSION_ID  AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
			END
	    -------------------------------------------------
		--10	0116	Comprehensive Condominium
		--11	0118	Comprehensive Company
		--12	0351	General Civil Liability
		--14	0171	Diversified Risks
		--16	0115	Robbery
		--19	0114	Dwelling
		--25	0111	Traditional Fire
		--27	0173	Global of Bank
		--32	0750	Judicial Guarantee
		-------------------------------------------------
		ELSE IF(@POLICY_LOB IN (10,11,12,14,16,19,25,27,32))
		BEGIN 
			
				SELECT @RISK_ID = PRODUCT_RISK_ID 
				FROM  POL_PRODUCT_LOCATION_INFO WITH(NOLOCK)
				WHERE CUSTOMER_ID       		 = @CUSTOMER_ID AND
					  POLICY_ID         		 = @POLICY_ID   AND
					  POLICY_VERSION_ID 		 = @POLICY_VERSION_ID AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
			
		END		
		ELSE IF(@POLICY_LOB IN( 15,21,33,34))
		BEGIN 
	
				SELECT @RISK_ID = PERSONAL_INFO_ID 
				FROM  POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)
				WHERE CUSTOMER_ID       		 = @CUSTOMER_ID AND
					  POLICY_ID         		 = @POLICY_ID   AND
					  POLICY_VERSION_ID 		 = @POLICY_VERSION_ID AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
		END
		--17	0553	Facultative Liability
		--18	0523	Civil Liability Transportation
		--28	0435	Aeronautic
		--29	0531	Motor
		--31	0654	Cargo Transportation Civil Liability
		
		ELSE IF(@POLICY_LOB IN(28,17,18,29,31,30,36))
		BEGIN 
				SELECT @RISK_ID = VEHICLE_ID 
				FROM  POL_CIVIL_TRANSPORT_VEHICLES WITH(NOLOCK)
				WHERE CUSTOMER_ID       		 = @CUSTOMER_ID AND
					  POLICY_ID         		 = @POLICY_ID   AND
					  POLICY_VERSION_ID 		 = @POLICY_VERSION_ID AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
					  
		END
		ELSE IF(@POLICY_LOB IN(35,37))
		BEGIN 
		        SELECT @RISK_ID =  PENHOR_RURAL_ID 
				FROM  POL_PENHOR_RURAL_INFO WITH(NOLOCK)
				WHERE CUSTOMER_ID       		 = @CUSTOMER_ID AND
					  POLICY_ID         		 = @POLICY_ID   AND
					  POLICY_VERSION_ID 		 = @POLICY_VERSION_ID AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
					  
		
		END
		ELSE IF(@POLICY_LOB IN(13))
		BEGIN 
		        SELECT @RISK_ID =  MARITIME_ID 
				FROM  POL_MARITIME WITH(NOLOCK)
				WHERE CUSTOMER_ID       		 = @CUSTOMER_ID AND
					  POLICY_ID         		 = @POLICY_ID   AND
					  POLICY_VERSION_ID 		 = @POLICY_VERSION_ID AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
		END		
		ELSE IF(@POLICY_LOB IN(22))
		BEGIN 
				SELECT @RISK_ID =  PERSONAL_ACCIDENT_ID 
				FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)
				WHERE CUSTOMER_ID       		 = @CUSTOMER_ID AND
					  POLICY_ID         		 = @POLICY_ID   AND
					  POLICY_VERSION_ID 		 = @POLICY_VERSION_ID AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
		END
		
		--30	0589	Dpvat(Cat. 3 e 4)
		--36	0588	DPVAT(Cat.1,2,9 e 10)
		ELSE IF(@POLICY_LOB IN(23,20))
		BEGIN 
			    SELECT @RISK_ID = COMMODITY_ID
				FROM  POL_COMMODITY_INFO WITH(NOLOCK)
				WHERE CUSTOMER_ID       		 = @CUSTOMER_ID AND
					  POLICY_ID         		 = @POLICY_ID   AND
					  POLICY_VERSION_ID 		 = @POLICY_VERSION_ID AND
					  IL_RISK_ID_LINK_WITH_CLAIM = @RISK_ID_LINK_WITH_CLAIM
		END
	
	    
	       IF(@RISK_ID IS NULL OR @RISK_ID=0)
	        SET @ERROR_NO =257 -- RISK DOES NOT EXISTS
		
     END     
                  
  --ELSE IF (@LOOP_CONTRACT_FACULTATIVE NOT IN (14627,14628))                  
  --SET @ERROR_NO=94                  
                    
                    
   IF(@ERROR_NO>0)                  
       BEGIN                  
                         
                         
        -----------------------------------------------------------                        
  -- INSERT ERROR DETAILS                  
  -----------------------------------------------------------                   
     UPDATE MIG_IL_POLICY_REINSURANCE_DETAILS                  
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
     @ERROR_SOURCE_TYPE     = 'REIS'                     
                       
                  
       END                   
       ELSE                  
       BEGIN                  
                    
     IF(@REINSURENCE_ID IS NULL OR @REINSURENCE_ID=0) -- ADD ENTRY IN REINSURANCE TABLE AND BREAKDOWN DETAILS TABLE
     BEGIN               
  -----------------------------------------------------------                        
  -- GET REINSURENCE ID                  
  -----------------------------------------------------------               
      IF(@LOOP_CONTRACT_FACULTATIVE=14628)              
      BEGIN            
        SELECT @REINSURENCE_ID		= ISNULL(MAX(REINSURANCE_ID),0) +1                  
        FROM POL_REINSURANCE_INFO                  
        WHERE CUSTOMER_ID			= @CUSTOMER_ID AND                   
              POLICY_ID				= @POLICY_ID   AND                  
              POLICY_VERSION_ID		= @POLICY_VERSION_ID                   
                          
                                
      INSERT INTO [dbo].[POL_REINSURANCE_INFO]                  
         ([REINSURANCE_ID]                  
         ,[COMPANY_ID]                
         ,[CUSTOMER_ID]                  
         ,[POLICY_ID]                  
         ,[POLICY_VERSION_ID]                  
         ,[CONTRACT_FACULTATIVE]                  
         ,[CONTRACT]                  
         ,[REINSURANCE_CEDED]                  
         ,[REINSURANCE_COMMISSION]                  
         ,[IS_ACTIVE]                  
         ,[CREATED_BY]                  
         ,[CREATED_DATETIME]                  
         ,[MODIFIED_BY]                  
         ,[LAST_UPDATED_DATETIME]                  
         ,[REINSURER_NUMBER]                  
         )                     
                        
       SELECT                   
		  @REINSURENCE_ID                  
		 ,@LOOP_REIN_COMPANY_ID                  
		 ,@CUSTOMER_ID                  
		 ,@POLICY_ID                  
		 ,@POLICY_VERSION_ID                  
		 ,@LOOP_CONTRACT_FACULTATIVE                  
		 ,0           
		 ,RETENTION_PERCENT                  
		 ,COMMISSION_PERCENT                  
		 ,'Y'                  
		 , dbo.fun_GetDefaultUserID() --<CREATED_BY, int,>                       
		 ,GETDATE()                  
		 ,NULL                  
		 ,NULL                  
		 ,0                   
       FROM MIG_IL_POLICY_REINSURANCE_DETAILS  (NOLOCK)            
                   
       WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND                  
		    IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO                  
                       
      END            
                  
            
                  
                   
  INSERT INTO [dbo].[POL_REINSURANCE_BREAKDOWN_DETAILS]                
           ([CUSTOMER_ID]                
           ,[POLICY_ID]                
           ,[POLICY_VERSION_ID]                
           ,[LOB_ID]                
           --,[STATE_ID]                
           --,[POLICY_NUMBER]                
           --,[INSURED_NAME]                
           --,[ADDRESS]                
           --,[CITY]                
           --,[ZIP_CODE]                
           --,[LOB_DESC]                
           --,[STATE_DESC]  
           ,[APP_EFFECTIVE_DATE]                
           ,[APP_EXPIRATION_DATE]             
           --,[REINS_SPECIAL_ACPT]                
           --,[PROCESS_TRAN_DATE]                
           --,[TRAN_EFF_DATE]             
           --,[TRAN_DESC]                
           --,[MONTH_NUMBER]                
           --,[YEAR_NUMBER]                
           ,[SEQUENCE_NUMBER]                
           --,[COV_CATEGORY_ID]                
           --,[COMPLETED]                
           --,[STATEMENT_DATE]                
           ,[CONTRACT_NUMBER]                
           --,[PREMIUM_BASE]                
           ,[CONTRACT_YEAR]                
           ,[TOTAL_INS_VALUE]                
           ,[TIV_GROUP]                
           ,[LAYER]                
           ,[LAYER_AMOUNT]                
           ,[REIN_LIMIT_FAB]                
           ,[COV_CATEGORY]                
           ,[TRAN_PREMIUM]                
           --,[EARNED]                
           --,[WRITTEN]                
           --,[CONSTRACTION]                
           --,[PROTECTION]                
           --,[CENTRAL_ALARM]                
           --,[NEW_HOME]                
           --,[HOME_AGE]                
           --,[DEDUCT_LAYER_1]                
           --,[DEDUCT_LAYER_2]                
           --,[RATE]                
           ,[REIN_PREMIUM]                
           ,[COMM_PERCENTAGE]                
           ,[COMM_AMOUNT]                 
           ,[NET_DUE]                
           ,[MAJOR_PARTICIPANT]                
           ,[RETENTION_PER]                
           --,[REIN_CEDED]                
           ,[RISK_ID]                
           --,[PROCESS_ID]                
           ,[IS_COMMISSION_PROCESS]                
           ,[IS_PAID_TO_PAGNET]                
           ,[PAGNET_DATE])                  
                           
   SELECT @CUSTOMER_ID                
			, @POLICY_ID                
			,@POLICY_VERSION_ID                
			,@POLICY_LOB              
			--,NULL  -- POLICY NUMBER                
			-- STATE ID                
			--,[INSURED_NAME]                
			--        ,[ADDRESS]                
			--        ,[CITY]                
			--        ,[ZIP_CODE]                
			--        ,[LOB_DESC]                
			--        ,[STATE_DESC]                
			,@APP_EFFECTIVE_DATE               
			,@APP_EXPIRY_DATE            
			--,[REINS_SPECIAL_ACPT]                
			--       ,[PROCESS_TRAN_DATE]                
			--       ,[TRAN_EFF_DATE]                
			--       ,[TRAN_DESC]                
			--,[MONTH_NUMBER]                
			--,[YEAR_NUMBER]                
			,MRC.SEQUENCE_NUMBER              
			--,[COV_CATEGORY_ID]                
			--,[COMPLETED]                
			--,[STATEMENT_DATE]                
			,CASE WHEN (@LOOP_CONTRACT_FACULTATIVE=14628) THEN 'Facultative' ELSE MRC.CONTRACT_NUMBER END            
			--,[PREMIUM_BASE]                
			,MRC.CONTACT_YEAR                    
			,TIV                
			,0                
			,LAYER                
			,LAYER_CEDED_AMOUNT                
			,0                
			,NULL                
			,TRANSACTION_PREMIUM                
			--,[EARNED]                
			--,[WRITTEN]                
			--,[CONSTRACTION]                
			--,[PROTECTION]                
			--,[CENTRAL_ALARM]             
			--,[NEW_HOME]                
			--,[HOME_AGE]                
			--,[DEDUCT_LAYER_1]                
			--,[DEDUCT_LAYER_2]                
			--,RATE                
			,REIN_PREMIUM                
			,COMMISSION_PERCENT                
			,COMMISSION_AMOUNT                
			,NULL                
			,@LOOP_REIN_COMPANY_ID  
			,RETENTION_PERCENT                
			--,[REIN_CEDE                
			,@RISK_ID   --RISK_ID_LINK_WITH_CLAIM                
			--PROCESS_ID 
			,CASE WHEN (RI_COMMISSION_RECEIVED>0.00) THEN 'Y' ELSE 'N' END                
			,NULL										
			,NULL                 
                    
   FROM MIG_IL_POLICY_REINSURANCE_DETAILS (NOLOCK) MIRD                
   LEFT OUTER JOIN MNT_REINSURANCE_CONTRACT (NOLOCK) MRC            
   ON MIRD.[CONTRACT]=MRC.CONTRACT_NUMBER              
   WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND                  
		 IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO                 
                        
  END
 
  ELSE IF (@PROCESS_TYPE=3 AND @REINSURENCE_ID>0)
  BEGIN
     ------------------------------------                        
     -- UPDATE IMPORT DETAILS                  
     ------------------------------------                           
   UPDATE MIG_IL_POLICY_REINSURANCE_DETAILS                  
   SET    CUSTOMER_ID       = @CUSTOMER_ID,                  
          POLICY_ID         = @POLICY_ID,                  
          POLICY_VERSION_ID = @POLICY_VERSION_ID,                  
          IS_PROCESSED      = 'Y'                  
   WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND                  
          IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO   
          
      	-- UPDATE EXISTING DETAILS
       UPDATE [POL_REINSURANCE_INFO]               
       SET	   
		  [CONTRACT_FACULTATIVE]		= CASE WHEN T.CONTRACT_FACULTATIVE IS NULL OR T.CONTRACT_FACULTATIVE=0 THEN PRI.[CONTRACT_FACULTATIVE] ELSE T.CONTRACT_FACULTATIVE END    
		 ,[REINSURANCE_CEDED]           = CASE WHEN T.[REINSURANCE_CEDED] IS NULL OR T.[REINSURANCE_CEDED]=0 THEN PRI.[REINSURANCE_CEDED] ELSE T.[REINSURANCE_CEDED] END   
		 ,[REINSURANCE_COMMISSION]      = CASE WHEN T.[REINSURANCE_COMMISSION] IS NULL OR T.[REINSURANCE_COMMISSION]=0 THEN PRI.[REINSURANCE_COMMISSION] ELSE T.[REINSURANCE_COMMISSION] END   
		 ,[IS_ACTIVE]					= T.[IS_ACTIVE]  
		 ,[MODIFIED_BY]					= T.[MODIFIED_BY]  
		 ,[LAST_UPDATED_DATETIME]       = T.[LAST_UPDATED_DATETIME]  
			 
	     FROM [POL_REINSURANCE_INFO] PRI INNER JOIN
	     (
	        SELECT  CUSTOMER_ID
	               ,POLICY_ID
	               ,POLICY_VERSION_ID
				   ,@LOOP_CONTRACT_FACULTATIVE  [CONTRACT_FACULTATIVE]       
				   ,RETENTION_PERCENT  [REINSURANCE_CEDED]                   
				   ,COMMISSION_PERCENT   [REINSURANCE_COMMISSION]            
				   ,'Y'      [IS_ACTIVE]            
				   ,dbo.fun_GetDefaultUserID()    [MODIFIED_BY]              
				   ,GETDATE()  [LAST_UPDATED_DATETIME]                       
				   
		    FROM   MIG_IL_POLICY_REINSURANCE_DETAILS 
		    WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		           IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			   
	     )T ON T.CUSTOMER_ID= PRI.CUSTOMER_ID AND T.POLICY_ID=PRI.POLICY_ID AND T.POLICY_VERSION_ID=PRI.POLICY_VERSION_ID
	     
	     WHERE PRI.CUSTOMER_ID =@CUSTOMER_ID AND PRI.POLICY_ID=@POLICY_ID AND PRI.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PRI.REINSURANCE_ID=@REINSURENCE_ID
              
         ---UPDATE REINSURANCE BREAK DOWN TABLE 
         	-- UPDATE EXISTING DETAILS
     UPDATE [POL_REINSURANCE_BREAKDOWN_DETAILS]               
       SET	   
			 [APP_EFFECTIVE_DATE]     = CASE WHEN T.[APP_EFFECTIVE_DATE] IS NULL THEN PRBD.[APP_EFFECTIVE_DATE] ELSE T.[APP_EFFECTIVE_DATE] END         
			,[APP_EXPIRATION_DATE]	  = CASE WHEN T.[APP_EXPIRATION_DATE] IS NULL  THEN PRBD.APP_EXPIRATION_DATE ELSE T.[APP_EXPIRATION_DATE] END  			
			,[SEQUENCE_NUMBER]		  = CASE WHEN T.[SEQUENCE_NUMBER] IS NULL OR T.[SEQUENCE_NUMBER]='0' THEN PRBD.SEQUENCE_NUMBER ELSE T.[SEQUENCE_NUMBER] END  			
			,[CONTRACT_NUMBER]		  = CASE WHEN T.[CONTRACT_NUMBER] IS NULL OR T.[CONTRACT_NUMBER]='' THEN PRBD.CONTRACT_NUMBER ELSE T.[CONTRACT_NUMBER] END  			
			,[CONTRACT_YEAR]		  = CASE WHEN T.CONTACT_YEAR IS NULL OR T.CONTACT_YEAR='0' THEN PRBD.CONTRACT_YEAR ELSE T.CONTACT_YEAR END  			
			,[TOTAL_INS_VALUE]		  = CASE WHEN T.TIV IS NULL OR T.TIV=0 THEN PRBD.TOTAL_INS_VALUE ELSE T.TIV END  			
			,[LAYER]				  = CASE WHEN T.[LAYER] IS NULL OR T.[LAYER]=0 THEN PRBD.LAYER ELSE T.[LAYER] END  			
			,[LAYER_AMOUNT]			  = CASE WHEN T.LAYER_CEDED_AMOUNT IS NULL OR T.LAYER_CEDED_AMOUNT=0 THEN PRBD.LAYER_AMOUNT ELSE T.LAYER_CEDED_AMOUNT END  			
			,[TRAN_PREMIUM]			  = CASE WHEN T.TRANSACTION_PREMIUM IS NULL OR T.TRANSACTION_PREMIUM=0 THEN PRBD.TRAN_PREMIUM ELSE T.TRANSACTION_PREMIUM END  			
			,[REIN_PREMIUM]			  = CASE WHEN T.[REIN_PREMIUM] IS NULL OR T.[REIN_PREMIUM]=0 THEN PRBD.REIN_PREMIUM ELSE T.[REIN_PREMIUM] END  			
			,[COMM_PERCENTAGE]		  = CASE WHEN T.COMMISSION_PERCENT IS NULL OR T.COMMISSION_PERCENT=0 THEN PRBD.COMM_PERCENTAGE ELSE T.COMMISSION_PERCENT END  			
			,[COMM_AMOUNT]			  = CASE WHEN T.COMMISSION_AMOUNT IS NULL OR T.COMMISSION_AMOUNT=0 THEN PRBD.COMM_AMOUNT ELSE T.COMMISSION_AMOUNT END  			
			,[MAJOR_PARTICIPANT]      = CASE WHEN T.[MAJOR_PARTICIPANT] IS NULL OR T.[MAJOR_PARTICIPANT]=0 THEN PRBD.MAJOR_PARTICIPANT ELSE T.[MAJOR_PARTICIPANT] END          
			,[RETENTION_PER]		  = CASE WHEN T.RETENTION_PERCENT IS NULL OR T.RETENTION_PERCENT=0 THEN PRBD.RETENTION_PER ELSE T.RETENTION_PERCENT END  			
			,[RISK_ID]				  = CASE WHEN T.RISK_ID_LINK_WITH_CLAIM IS NULL OR T.RISK_ID_LINK_WITH_CLAIM=0 THEN PRBD.RISK_ID ELSE T.RISK_ID_LINK_WITH_CLAIM END  			
			,[IS_COMMISSION_PROCESS]  = CASE WHEN T.[IS_COMMISSION_PROCESS]  IS NULL OR T.[IS_COMMISSION_PROCESS] ='' THEN PRBD.IS_COMMISSION_PROCESS ELSE T.[IS_COMMISSION_PROCESS]  END         
		  
			 
	     FROM [POL_REINSURANCE_BREAKDOWN_DETAILS] PRBD INNER JOIN
	     (
	     
	          SELECT
	                 CUSTOMER_ID
	                ,POLICY_ID
	                ,POLICY_VERSION_ID
					,@APP_EFFECTIVE_DATE [APP_EFFECTIVE_DATE]               
					,@APP_EXPIRY_DATE [APP_EXPIRATION_DATE]           
					,MRC.SEQUENCE_NUMBER              
					,CASE WHEN (@LOOP_CONTRACT_FACULTATIVE=14628) THEN 'Facultative' ELSE MRC.CONTRACT_NUMBER END  [CONTRACT_NUMBER]    
					,MRC.CONTACT_YEAR                    
					,TIV                
					,LAYER                
					,LAYER_CEDED_AMOUNT                
					,TRANSACTION_PREMIUM                
					,REIN_PREMIUM                
					,COMMISSION_PERCENT                
					,COMMISSION_AMOUNT                
					,@LOOP_REIN_COMPANY_ID  [MAJOR_PARTICIPANT] 
					,RETENTION_PERCENT                
					,RISK_ID_LINK_WITH_CLAIM                
					,CASE WHEN (RI_COMMISSION_RECEIVED>0.00) THEN 'Y' ELSE 'N' END   [IS_COMMISSION_PROCESS]                 
				   
		     FROM MIG_IL_POLICY_REINSURANCE_DETAILS (NOLOCK) MIRD                
			 LEFT OUTER JOIN MNT_REINSURANCE_CONTRACT (NOLOCK) MRC            
			 ON MIRD.[CONTRACT]      = MRC.CONTRACT_NUMBER              
			 WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND                  
				   IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO   
			   
	     )T ON T.CUSTOMER_ID= PRBD.CUSTOMER_ID AND T.POLICY_ID=PRBD.POLICY_ID AND T.POLICY_VERSION_ID=PRBD.POLICY_VERSION_ID
	     
	     WHERE PRBD.CUSTOMER_ID =@CUSTOMER_ID AND PRBD.POLICY_ID=@POLICY_ID AND PRBD.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PRBD.LOB_ID=@LOB_ID
	           AND RISK_ID =@RISK_ID
	      
  END   
                     
     ------------------------------------                        
     -- UPDATE IMPORT DETAILS                  
     ------------------------------------                           
   UPDATE MIG_IL_POLICY_REINSURANCE_DETAILS                  
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
     @FILE_TYPE       = @IMPORT_REINSURENCE_FILE_TYPE,    
     @FILE_NAME              = @IMPORT_FILE_NAME,    
     @CUSTOMER_SEQUENTIAL    = NULL,    
     @POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO,     
     @ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,    
     @IMPORT_SEQUENTIAL      = @LOOP_REINSURENCE,    
     @IMPORT_SEQUENTIAL2     = NULL,    
     @LOB_ID				 = @LOB_ID ,     
     @IMPORTED_RECORD_ID     = @REINSURENCE_ID ,
     @PROCESS_TYPE			 = @PROCESS_TYPE       
               
                   
    END                  
    SET @COUNTER+=1                   
                         
  END  -- END OF WHILE LOOP                  
                    
END TRY          BEGIN CATCH                      
                       
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