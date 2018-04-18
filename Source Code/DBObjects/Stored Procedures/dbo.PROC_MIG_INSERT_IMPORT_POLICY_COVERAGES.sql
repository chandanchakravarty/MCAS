IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_IMPORT_POLICY_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_POLICY_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-- =============================================      
 AUTHOR:  ATUL KUMAR SINGH      
 CRAETE DATE: 2011-05-11      
 DESCRIPTION: VALIDATE POLICY COVERAGES DATA   
 
 PROC VERSION 1.0
 IN-SCOPE:       
   WE ARE HANDLING FOLLOWING GIVEN TASKS THOROUGH THIS PROC
	1:	NBS ISSUANCE FILE
	2:	END ISSUANCE FILE
	3:  ALL TYPES OF END
	  
  OUT-SCOPE      
	1: POLICY CANCELLATIONS
	2: RENEWAL OF POLICY
	3: UNDO OF END        
      
  CLIENT ASSUMPTION      
	1: CUSTOMER TYPE: IF LENGTH OF PROVIDED CPF/CNPJ NUMBER IS GREATER THAN 11 THEN IT IS COMMERCIAL 
						OTHERWISE PERSONAL
	2:	THERE IS ALWAYS A SINGLE BROKER IN ISSUANCE FILE
	3:	BROKER COMMISSION PERCENT BE 100% AT REMUNERATION TAB, COZ WE ARE IMPLEMENTING POLICY LEVEL COMMISSION				
    4:	WE ALREADY COMMUNICATE THAT   
        
  INTERNAL ASSUMPTIONS       
    1:	WE ARE NOT HANDLING CUSTOMER TYPE AS GOVERNMENT  
    2:	WE ARE MAPPING HARDCODED BILLING PLAN IN CASE OF MASTER POLICIES
    3:	IN CASE OF NORMAL POLICIES WE ARE CONSIDERING BILLING PLAN STARTING FROM (0+1..9) BOLETOS
    
DROP proc [PROC_MIG_INSERT_IMPORT_POLICY_COVERAGES]      
    
      
--  exec  [PROC_MIG_INSERT_IMPORT_POLICY_COVERAGES] 986,'0001647',1
-- =============================================*/      
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_IMPORT_POLICY_COVERAGES]      
          
 @FILE_IMPORT_REQUEST_ID INT,    
 @FILE_POLICY_NUMBER NVARCHAR(21),      
 @FILE_ENDORSEMENT_NUMBER  INT  
   
AS      
BEGIN      
      
 SET NOCOUNT ON;      
       
 DECLARE @POLICY_LOB INT      
 DECLARE @POLICY_SUB_LOB INT      
 DECLARE @ENDORSEMENT_NUMBER_VAR INT      
 DECLARE @CUST_ID INT      
 DECLARE @POLICY_ID INT     
 DECLARE @POLICY_VERSION_ID INT      
   
 DECLARE @CREATED_BY INT=2      
 DECLARE @LOCATION_NUMBER INT      
 DECLARE @POLICY_RISK_ID INT      
 DECLARE @PERIL_ID INT      
 DECLARE @NO_OF_RISK INT      
 DECLARE @PERSONAL_INFO_ID INT      
 DECLARE @CO_APPLICANT INT      
 DECLARE @VEHICALE_ID INT      
 DECLARE @COMMODITY_ID INT      
 DECLARE @PERSONAL_ACCIDENT_ID INT      
 DECLARE @MERITIME_ID INT      
 DECLARE @PENHOR_RURAL_ID INT      
 DECLARE @APP_NUMBER NVARCHAR(20)      
 DECLARE @HAS_ERRORS  INT  =0   
   
 DECLARE @CO_APPLICANT_NAME NVARCHAR(250)     
 DECLARE @CO_APPLICANT_CPF  NVARCHAR(80)    
 DECLARE @DEFAULT NVARCHAR(80)= 'CO Aceito'   
 DECLARE @STATE INT=87							--Use 'Território Nacional' as default  As per Monika Comment
 DECLARE @COUNTRY INT =5  
 DECLARE @SUM_INSURED DECIMAL(20,5)  
 DECLARE @VALUE_OF_RISK DECIMAL(20,5)
  
 DECLARE @SYSTEM_RISK_ID INT
 DECLARE @OLD_LOCATION_NUMBER INT
 DECLARE @OLD_CPAPP_ID INT
     
 DECLARE @MANUFACTURE_YEAR INT=1900				--As per Monika Comment
 DECLARE @DEFAULT_ZIP_CODE INT=00000000		--As per Monika Comment
 DECLARE @DEFAULT_FIPE_CODE NVARCHAR(20)='CL0102'  
 DECLARE @DEFAULT_NO_OF_PASSENGERS INT=1  
   
 DECLARE @POSITION_ID INT   =21000				-- Use '21000' as default Monoka comment
   
   
  
  
 DECLARE @GENDER INT=  6615  
 DECLARE @CO_APPLICANT_CODE VARCHAR(10)  
 DECLARE @CO_APPLICANT_DOB DATE='1900-01-01'	--As per Monika Comment
 DECLARE @CO_APPLICANT_REG_ID_ISSUE DATETIME='1900-01-01'  --As per Monika Comment
 DECLARE @CO_APPLICANT_ORIGINAL_ISSUE VARCHAR(10)  
 DECLARE @CO_APPLICANT_REGIONAL_IDENTIFICATION  VARCHAR(10)  
 DECLARE @CO_APPLICANT_GENDER INT  
 DECLARE @CONVEYANCE_TYPE INT=14646  
    
    
  -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES    
    
 DECLARE @ERROR_NUMBER    INT    
 DECLARE @ERROR_SEVERITY  INT    
 DECLARE @ERROR_STATE     INT    
 DECLARE @ERROR_PROCEDURE VARCHAR(512)    
 DECLARE @ERROR_LINE    INT    
 DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)    
    
       
 BEGIN TRY   
 
  ----------------------------------------------------------------------
  -------------------  CREATE TEMP TABLE FOR COVERAGES-----------------  
  ----------------------------------------------------------------------
   
		  CREATE TABLE #COVERAGE_ITEM      
		  (      
					[RISK_ID] INT      
				   ,[ID] INT IDENTITY(1,1)   
				   --,[COVERAGE_ID]INT    
				   ,[COVERAGE_CODE_ID] INT      
				   --,[RI_APPLIES]      
				   --,[LIMIT_OVERRIDE]      
				   ,[LIMIT_1] DECIMAl (18,0)      
				   --,[LIMIT_1_TYPE]      
				   --,[LIMIT_2]      
				   --,[LIMIT_2_TYPE]      
				   --,[LIMIT1_AMOUNT_TEXT]      
				   --,[LIMIT2_AMOUNT_TEXT]      
				   --,[DEDUCT_OVERRIDE]      
				   ,[DEDUCTIBLE_1] DECIMAl (18,0)      
				   ,[DEDUCTIBLE_1_TYPE] NVARCHAR(6)      
				   --,[DEDUCTIBLE_2]      
				  --,[DEDUCTIBLE_2_TYPE]      
				   ,[MINIMUM_DEDUCTIBLE] DECIMAl (18,0)      
				   ,[DEDUCTIBLE1_AMOUNT_TEXT] NVARCHAR(500)      
				   --,[DEDUCTIBLE2_AMOUNT_TEXT]      
				   --,[DEDUCTIBLE_REDUCES]      
				   ,[INITIAL_RATE] DECIMAl (8,4)      
				  ,[FINAL_RATE]  DECIMAl (8,4)      
				   ----,[AVERAGE_RATE]      
				   ,[WRITTEN_PREMIUM]DECIMAl (18,2)      
				   --,[FULL_TERM_PREMIUM]      
				   --,[IS_SYSTEM_COVERAGE]      
				   --,[LIMIT_ID]      
				   --,[DEDUC_ID]      
				   --,[ADD_INFORMATION]      
				   --,[CREATED_BY]      
				   --,[CREATED_DATETIME]      
				   --,[MODIFIED_BY]      
				 -- ,[LAST_UPDATED_DATETIME]      
				  -- ,[INDEMNITY_PERIOD] 
				  ,ACC_CO_DISCOUNT      DECIMAl (18,2)
		  )       
    
       
 --------------------------------- SET ENDORSEMENT NUMBER      
		SET @ENDORSEMENT_NUMBER_VAR=@FILE_ENDORSEMENT_NUMBER+1      
       
         --------------------------------------------------      
         -- FETCH APPLICATION/POLICY DETAILS      
         --------------------------------------------------      
         
         
         IF(@FILE_ENDORSEMENT_NUMBER<=0)      
          BEGIN      
				SELECT @CUST_ID        =	COI.CUSTOMER_ID,   
				 @POLICY_ID			   =	COI.POLICY_ID,   
				 @POLICY_VERSION_ID    =	COI.POLICY_VERSION_ID,  
				 @POLICY_LOB		   =	ISNULL(POLICY_LOB,0),  
				 @POLICY_SUB_LOB       =	ISNULL(POL.POLICY_SUBLOB,0),  
				 @APP_NUMBER		   =	POL.APP_NUMBER  
				FROM	POL_CO_INSURANCE AS COI INNER JOIN  
				 POL_CUSTOMER_POLICY_LIST AS POL ON  COI.CUSTOMER_ID		   = POL.CUSTOMER_ID 
													 AND COI.POLICY_ID		   = POL.POLICY_ID 
													 AND COI.POLICY_VERSION_ID = POL.POLICY_VERSION_ID                     
				WHERE  COI.LEADER_POLICY_NUMBER =	@FILE_POLICY_NUMBER   
		 END  
		ELSE  
			 BEGIN  
      
				SELECT  @CUST_ID			 =	COI.CUSTOMER_ID,   
						@POLICY_ID			 =	COI.POLICY_ID,   
						@POLICY_VERSION_ID   =	COI.POLICY_VERSION_ID,  
						@POLICY_LOB			 =	ISNULL(POLICY_LOB,0),  
						@POLICY_SUB_LOB      =	ISNULL(POL.POLICY_SUBLOB,0),  
						@APP_NUMBER			 =  POL.APP_NUMBER  
				FROM    POL_CO_INSURANCE AS COI INNER JOIN  
					    POL_POLICY_ENDORSEMENTS AS EN 
			    ON		COI.CUSTOMER_ID		 =	EN.CUSTOMER_ID 
			    AND		COI.POLICY_ID		 =	EN.POLICY_ID 
			    AND		COI.POLICY_VERSION_ID=	EN.POLICY_VERSION_ID 
			    INNER JOIN  
					   POL_CUSTOMER_POLICY_LIST AS POL 
			    ON		COI.CUSTOMER_ID		 =	POL.CUSTOMER_ID 
			    AND		COI.POLICY_ID		 =	POL.POLICY_ID 
			    AND		COI.POLICY_VERSION_ID=	POL.POLICY_VERSION_ID                     
				WHERE  COI.LEADER_POLICY_NUMBER=@FILE_POLICY_NUMBER AND POL.POLICY_STATUS IS NOT NULL AND   
				 --POL.POLICY_STATUS <>'' AND --POL.POLICY_STATUS NOT IN('UISSUE','REJECT','Suspended','APPLICATION') AND   
				 POL.POLICY_NUMBER IS NOT NULL AND POL.POLICY_NUMBER<>'' AND  
				 CAST(EN.CO_ENDORSEMENT_NO AS INT) =@FILE_ENDORSEMENT_NUMBER  
		 END     

   
     
   
      
 ----------------------------------  SET CO APPLICANT ID      
       
				 SELECT
				   @CO_APPLICANT   					     = P.APPLICANT_ID  ,  
				   @CO_APPLICANT_NAME       			 = ISNULL(C.FIRST_NAME,'')+' '+ISNULL(C.MIDDLE_NAME,'')+' '+ISNULL(C.LAST_NAME  ,''),  
				   @CO_APPLICANT_CPF					 = C.CPF_CNPJ,  
				   @CO_APPLICANT_CODE					 = CONTACT_CODE,  
				   @CO_APPLICANT_DOB					 = CO_APPL_DOB,  
				   @CO_APPLICANT_REG_ID_ISSUE			 = REG_ID_ISSUE,  
				   @CO_APPLICANT_ORIGINAL_ISSUE			 = ORIGINAL_ISSUE,  
				   @CO_APPLICANT_REGIONAL_IDENTIFICATION = REGIONAL_IDENTIFICATION  				     
				 FROM       
				   POL_APPLICANT_LIST P WITH(NOLOCK)   INNER JOIN  
				   CLT_APPLICANT_LIST  C WITH(NOLOCK)  ON P.CUSTOMER_ID=C.CUSTOMER_ID AND P.APPLICANT_ID=C.APPLICANT_ID  
				 WHERE      
				   P.IS_PRIMARY_APPLICANT				= 1      AND    
				   P.CUSTOMER_ID						= @CUST_ID           AND  
				   P.POLICY_ID							= @POLICY_ID         AND  
				   P.POLICY_VERSION_ID					= @POLICY_VERSION_ID      
       
       
			------------------------------------------------------------ 
            -- SET POLICY RISK ID       
            ------------------------------------------------------------
       
				 SELECT     DISTINCT POLICY_RISK_ID,IMPORT_REQUEST_ID      
				 INTO      #MIG_POLICY_RISK      
				 FROM     MIG_POLICY_COVERAGES (NOLOCK)      
				 WHERE    IMPORT_REQUEST_ID			= @FILE_IMPORT_REQUEST_ID   
				 AND      LEADER_POLICY_NUMBER		= '00'+@FILE_POLICY_NUMBER      
				 AND      LEADER_ENDORSEMENT_NUMBER	= @FILE_ENDORSEMENT_NUMBER  
				 AND      HAS_ERRORS				= 'N'      
				 AND      IS_DELETED				= 'N'      
       
    
				 ALTER TABLE    #MIG_POLICY_RISK      
				 ADD            ID_IDENTITY INT IDENTITY(1,1)      
       
			------------------------------------------------------------ 
            -- HOW MANY LOCATIONS ARE ATTCHED TO THIS RECORD      
            ------------------------------------------------------------
				 SELECT @NO_OF_RISK = COUNT(1)       
				 FROM   #MIG_POLICY_RISK      
     
       
       	    ------------------------------------------------------------ 
            -- INITIATE LOOP FOR PROCESSING MORE THAN ONE RISK IN SINGLE ENDORSEMENT         
            ------------------------------------------------------------

	WHILE(@NO_OF_RISK>0)      
			BEGIN    
			 
			   SELECT   @POLICY_RISK_ID			 = POLICY_RISK_ID,      
					    @FILE_IMPORT_REQUEST_ID  = IMPORT_REQUEST_ID  
			   FROM     #MIG_POLICY_RISK (NOLOCK)      
			   WHERE    id_identity				 = @NO_OF_RISK      
			  
			  
			  DECLARE @POLICY_EFFECTIVE_DATE DATETIME
			  DECLARE @POLICY_EXPIRY_DATE DATETIME
			  
			  SELECT 
					@POLICY_EFFECTIVE_DATE	  =	POLICY_EFFECTIVE_DATE,
					@POLICY_EXPIRY_DATE		  =	POLICY_EXPIRY_DATE
			  FROM  MIG_POLICY_COVERAGES (NOLOCK) A
			  WHERE	LEADER_POLICY_NUMBER	  =	'00'+@FILE_POLICY_NUMBER
			  AND	LEADER_ENDORSEMENT_NUMBER =	@FILE_ENDORSEMENT_NUMBER
			  AND	IMPORT_REQUEST_ID		  =	@FILE_IMPORT_REQUEST_ID
			  AND	POLICY_RISK_ID			  =	@POLICY_RISK_ID
			  
			  
			  
		   ------------------------------------------------------------
		   -- GET VALUE OF RISK 
		   ------------------------------------------------------------
			SELECT  @VALUE_OF_RISK				  =	MAX(SUM_INSURED)  
			FROM	MIG_POLICY_COVERAGES (NOLOCK) MPC      
			JOIN	MIG_POLICY_COVERAGE_CODE_MAPPING (NOLOCK)MPCM      
			ON		MPCM.LEADER_COVERAGE_CODE	  =	CAST(MPC.LEADER_COVERAGE_CODE  AS INT)    
			JOIN    MNT_COVERAGE (NOLOCK) MC      
			ON		MC.CARRIER_COV_CODE			  =	MPCM.ALBA_COVERAGE_CODE      
			WHERE	MPC.HAS_ERRORS				  =	'N'      
			AND     MPC.IS_DELETED				  = 'N'      
			AND     MPC.LEADER_POLICY_NUMBER	  = '00'+@FILE_POLICY_NUMBER      
			AND     MPC.LEADER_ENDORSEMENT_NUMBER = @FILE_ENDORSEMENT_NUMBER      
			AND     MC.SUB_LOB_ID				  = @POLICY_SUB_LOB   
			AND     MC.LOB_ID					  = @POLICY_LOB  
			AND		MPC.POLICY_RISK_ID			  = @POLICY_RISK_ID
			AND		IMPORT_REQUEST_ID			  =	@FILE_IMPORT_REQUEST_ID
		   
		   ------------------------------------------------------------
		   -- GET LMR(MAX LIMIT) VALUE 
		   ------------------------------------------------------------
		   SELECT		@SUM_INSURED				  =	SUM(SUM_INSURED)  
		   FROM			MIG_POLICY_COVERAGES (NOLOCK) MPC      
		   JOIN			MIG_POLICY_COVERAGE_CODE_MAPPING (NOLOCK)MPCM      
		   ON			MPCM.LEADER_COVERAGE_CODE	  =	CAST(MPC.LEADER_COVERAGE_CODE  AS INT)    
		   JOIN         MNT_COVERAGE (NOLOCK) MC      
		   ON			MC.CARRIER_COV_CODE			  =	MPCM.ALBA_COVERAGE_CODE      
		   WHERE		MPC.HAS_ERRORS				  =	'N'      
		   AND          MPC.IS_DELETED				  =	'N'      
		   AND          MPC.LEADER_POLICY_NUMBER	  =	'00'+@FILE_POLICY_NUMBER      
		   AND          MPC.LEADER_ENDORSEMENT_NUMBER =	@FILE_ENDORSEMENT_NUMBER      
		   AND          MC.SUB_LOB_ID				  = @POLICY_SUB_LOB   
		   AND          MC.LOB_ID					  = @POLICY_LOB  
		   AND			MC.REINSURANCE_LOB			  =	10963 
		   AND			MPC.POLICY_RISK_ID			  =	@POLICY_RISK_ID 
		   AND		    IMPORT_REQUEST_ID             =  @FILE_IMPORT_REQUEST_ID
		   
			 
		   ------------------------------------------------------------
		   --  FOR LOB : -
		   --	9		All Risks and Named Perils
		   --	10		Comprehensive Condominium
		   --	11		Comprehensive Company
		   --	12		General Civil Liability
		   --	14		Diversified Risks
		   --	16		Robbery
		   --	19		Dwelling
		   --	25		Traditional Fire
		   --	26		Engeneering Risks
		   --	27		Global of Bank
		   --	32		Judicial Guarantee
		   ------------------------------------------------------------
			IF(@POLICY_LOB=11 or @POLICY_LOB=10 or @POLICY_LOB=12 or @POLICY_LOB=14      
			 or @POLICY_LOB=16 or @POLICY_LOB=19 or @POLICY_LOB=32 or @POLICY_LOB=25      
			 or @POLICY_LOB=27      
			 or @POLICY_LOB=9 -- for named perlis      
			 or @POLICY_LOB=26  -- for Engineering Risks      
			 )      
			BEGIN      
			      
		
			  -- -- SET LOCATION NUMBER    

			  
				SELECT   @LOCATION_NUMBER=(ISNULL(MAX(LOCATION_ID) ,0)+1)      
				FROM     POL_LOCATIONS (NOLOCK)      
			   WHERE     CUSTOMER_ID   = @CUST_ID 

			  
			  -- NEED TO REMOVE THIS IF CONDITION NO USE
			  -- LEFT DUE TO IMPLEMENTATION FOR PHASE 2
			   
			  IF NOT EXISTS (SELECT 1 FROM [POL_LOCATIONS] (NOLOCK)   
				  WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID)
			  BEGIN  
			   INSERT INTO [POL_LOCATIONS]      
				  ([CUSTOMER_ID]                        
				  ,[POLICY_ID]      
				  ,[POLICY_VERSION_ID]      
				  ,[LOCATION_ID]      
				  ,[LOC_NUM]      
				  --,[IS_PRIMARY]      
				  ,[LOC_ADD1]      
				  ,[LOC_ADD2]      
				  ,[LOC_CITY]      
				  ,[LOC_COUNTY]      
				 ,[LOC_STATE]      
				 -- ,[LOC_ZIP]      
				  ,[LOC_COUNTRY]      
				  --,[PHONE_NUMBER]      
				  --,[FAX_NUMBER]      
				  --,[DEDUCTIBLE]      
				  --,[NAMED_PERILL]  
				  --,[DESCRIPTION]      
				  ,[IS_ACTIVE]      
				  ,[CREATED_BY]      
				  ,[CREATED_DATETIME]      
				  --,[MODIFIED_BY]      
				  --,[LAST_UPDATED_DATETIME]      
				  --,[LOC_TERRITORY]        --,[LOCATION_TYPE]      
				  --,[RENTED_WEEKLY]      
				  --,[WEEKS_RENTED]      
				  --,[LOSSREPORT_ORDER]      
				  --,[LOSSREPORT_DATETIME]      
				  --,[REPORT_STATUS]      
				  --,[CAL_NUM]      
				 -- ,[NAME]      
				 -- ,[NUMBER]      
				 -- ,[DISTRICT]      
				  --,[OCCUPIED]      
				  --,[EXT]      
				  --,[CATEGORY]      
				  --,[ACTIVITY_TYPE]      
				  --,[CONSTRUCTION]      
				  --,[SOURCE_LOCATION_ID]      
				  --,[IS_BILLING]      
				  ,CO_RISK_ID  
				  )      
			   SELECT        
				  @Cust_ID,      
				  @POLICY_ID,       
				  @ENDORSEMENT_NUMBER_VAR,       
				  @LOCATION_NUMBER,      
				  @LOCATION_NUMBER,      
				  --LOCATION_ADDRESS,      
				  --LOCATION_COMPLEMENT,      
				  --LOCATION_CITY,      
				  --STATE_ID,      
				  --LOCATION_POSTAL_CODE,      
				  --LOCATION_COUNTRY,   
				  @DEFAULT,     
				  @DEFAULT,     
				  @DEFAULT,   
				  @COUNTRY,  
				  @STATE,  
					@COUNTRY,  
				  'Y',      
				  @CREATED_BY      
				  ,GETDATE()      
				  ,@POLICY_RISK_ID  
			  END    
			     
			        
			         
			   IF(@POLICY_LOB=11 or @POLICY_LOB=10 or @POLICY_LOB=12 or @POLICY_LOB=14      
				or @POLICY_LOB=16 or @POLICY_LOB=19 or @POLICY_LOB=32 or @POLICY_LOB=25      
				or @POLICY_LOB=27      
				)      
			   BEGIN      
			         
			   -------------------------------- set policy risK id      
			   
			   DECLARE @PRODUCT_RISK_ID_TEMP INT
			   
			   
			   SELECT @PRODUCT_RISK_ID_TEMP=(ISNULL(MAX(PRODUCT_RISK_ID),0)+1) FROM [POL_PRODUCT_LOCATION_INFO] (NOLOCK)   
			   WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
			  
			  DECLARE @OLD_PRODUCT_RISK_ID INT
			  
			 IF(@FILE_ENDORSEMENT_NUMBER>0)
			 BEGIN
				SELECT @OLD_LOCATION_NUMBER=LOCATION,
					   @OLD_CPAPP_ID=CO_APPLICANT_ID,
					   @OLD_PRODUCT_RISK_ID=PRODUCT_RISK_ID	
				FROM [POL_PRODUCT_LOCATION_INFO] (NOLOCK)   
				  WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID-1  AND CO_RISK_ID=@POLICY_RISK_ID
			 END
			 
			 
			   IF NOT EXISTS (SELECT 1 FROM [POL_PRODUCT_LOCATION_INFO] (NOLOCK)   
				  WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID  
				  )  
			  BEGIN  
			         
			  INSERT INTO [dbo].[POL_PRODUCT_LOCATION_INFO]      
				  ([CUSTOMER_ID]      
				  ,[POLICY_ID]      
				  ,[POLICY_VERSION_ID]      
				  ,[PRODUCT_RISK_ID]      
				  ,[LOCATION]      
				  --,[VALUE_AT_RISK]      
				  --,[BUILDING_VALUE]      
				  --,[CONTENTS_VALUE]      
				  --,[RAW_MATERIAL_VALUE]      
				  --,[CONTENTS_RAW_VALUES]      
				  --,[MRI_VALUE]      
				  --,[MAXIMUM_LIMIT]      
				  --,[POSSIBLE_MAX_LOSS]      
				  --,[MULTIPLE_DEDUCTIBLE]      
				 -- ,[PARKING_SPACES]      
				 -- ,[ACTIVITY_TYPE]      
				 -- ,[OCCUPIED_AS]      
				  --,[CONSTRUCTION]      
				 -- ,[RUBRICA]      
				 -- ,[ASSIST24]      
				 -- ,[REMARKS]      
				  ,[IS_ACTIVE]      
				 ,[CREATED_BY]      
				  ,[CREATED_DATETIME]      
				  --,[MODIFIED_BY]      
				  --,[LAST_UPDATED_DATETIME]      
				  --,[CLAIM_RATIO]      
				  --,[BONUS]      
				  ,[CO_APPLICANT_ID]    
				 -- ,[CLASS_FIELD]      
				  ,[LOCATION_NUMBER]      
				  ,[ITEM_NUMBER]     
				  ,CO_RISK_ID   
				  )      
			            
				  SELECT      
				@CUST_ID,      
				@POLICY_ID,      
				@ENDORSEMENT_NUMBER_VAR,      
				CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PRODUCT_RISK_ID IS NOT NULL)
				THEN @OLD_PRODUCT_RISK_ID
				ELSE  @PRODUCT_RISK_ID_TEMP
				END,
				CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_LOCATION_NUMBER IS NOT NULL)
					THEN @OLD_LOCATION_NUMBER
					ELSE  @LOCATION_NUMBER
					END,      
				--A.LOCATION_CONSTRUCTION_TYPE,      
				'Y',      
				@CREATED_BY      
				,GETDATE()    
				,CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_CPAPP_ID IS NOT NULL)
					THEN   @OLD_CPAPP_ID
					ELSE   @CO_APPLICANT
					END
				,@POLICY_RISK_ID    
				,@POLICY_RISK_ID  
				,@POLICY_RISK_ID  
			    
			      
			  SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PRODUCT_RISK_ID IS NOT NULL)
											THEN  @OLD_PRODUCT_RISK_ID
											ELSE  @PRODUCT_RISK_ID_TEMP
											END
			
	
			   END  
			   ELSE  
			   
			   BEGIN  
			   	
			   	
			   	SELECT @SYSTEM_RISK_ID =PRODUCT_RISK_ID FROM [POL_PRODUCT_LOCATION_INFO] WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID
			   	
			
			   END  
			        
			   	 ------------------------------------------------------  UPDATE MAXIMUM LIMIT FIELD IN POL_PRODUCT_LOCATION_INFO  
			 
			       UPDATE		POL_PRODUCT_LOCATION_INFO  
				   SET			MAXIMUM_LIMIT				=	@SUM_INSURED  
				   WHERE		CUSTOMER_ID					=	@CUST_ID  
				   AND			POLICY_ID					=	@POLICY_ID  
				   AND			POLICY_VERSION_ID			=	@ENDORSEMENT_NUMBER_VAR  
				   AND			CO_RISK_ID					=	@POLICY_RISK_ID  
				 
			     
				 IF(@POLICY_LOB=11 or @POLICY_LOB=19 OR @POLICY_LOB=14 OR @POLICY_LOB=10)
				 BEGIN
			     
				   UPDATE		POL_PRODUCT_LOCATION_INFO  
				   SET   VALUE_AT_RISK  = @VALUE_OF_RISK  
				   WHERE  CUSTOMER_ID   = @CUST_ID  
				   AND   POLICY_ID   = @POLICY_ID  
				   AND   POLICY_VERSION_ID = @ENDORSEMENT_NUMBER_VAR  
				   AND   CO_RISK_ID  = @POLICY_RISK_ID 
				 END    
			      
			         
			   END      
			   ELSE IF(@POLICY_LOB=9 OR @POLICY_LOB=26)      
			   BEGIN      
			         
				------------------------------- SET PERIL ID      
			       
			      
			      
			   SELECT     @PERIL_ID=(ISNULL(MAX(PERIL_ID) ,0)+1)      
			   FROM       POL_PERILS (NOLOCK)      
			   WHERE      CUSTOMER_ID=@CUST_ID      
			    

			   DECLARE @OLD_PERIL_ID INT
			   
			   IF(@FILE_ENDORSEMENT_NUMBER>0)
			   BEGIN
					SELECT @OLD_PERIL_ID=PERIL_ID FROM [POL_PERILS] (NOLOCK)   
					WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID-1 AND CO_RISK_ID=@POLICY_RISK_ID
			   END
			 
			   
			   IF NOT EXISTS (SELECT 1 FROM [POL_PERILS] (NOLOCK)   
				WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID)  
			      
				BEGIN    
			         
				  INSERT INTO [dbo].[POL_PERILS]      
				   ([CUSTOMER_ID]      
				  ,[POLICY_ID]      
				   ,[POLICY_VERSION_ID]      
				   ,[PERIL_ID]      
				  --    --,[CALCULATION_NUMBER]      
				   ,[LOCATION]      
				  --    --,[ADDRESS]      
				  --    --,[NUMBER]      
				  --    --,[COMPLEMENT]      
				  --    --,[CITY]      
				  --    --,[COUNTRY]      
				  --    --,[STATE]      
				  --    --,[ZIP]      
				  --    --,[TELEPHONE]      
				  --    --,[EXTENTION]      
				  --    --,[FAX]      
				  --    --,[CATEGORY]      
				  --    --,[ATIV_CONTROL]      
				  --    --,[LOC]      
				  --    --,[LOCALIZATION]      
				  --    ,[OCCUPANCY]      
				  --    --,[CONSTRUCTION]      
				  --    --,[LOC_CITY]      
				  --    ,[CONSTRUCTION_TYPE]      
				  --    ,[ACTIVITY_TYPE]      
				  --   -- ,[RISK_TYPE]      
				          ,[VR]      
				          ,[LMI]         -- relook
				  --    --,[BUILDING]      
				  --   -- ,[MMU]      
				  --   -- ,[MMP]      
				  --   -- ,[MRI]      
				  --    --,[TYPE]      
				  --    --,[LOSS]      
				  --    --,[LOYALTY]      
				  --    --,[PERC_LOYALTY]      
				  --    --,[DEDUCTIBLE_OPTION]      
				  --    --,[MULTIPLE_DEDUCTIBLE]    
				  --    --,[E_FIRE]      
				  --    --,[S_FIXED_FOAM]      
				  --    --,[S_FIXED_INSERT_GAS]      
				 --    --,[CAR_COMBAT]      
				  --    --,[S_DETECT_ALARM]      
				  --    --,[S_FIRE_UNIT]      
				  --    --,[S_FOAM_PER_MANUAL]      
				  --    --,[S_MANUAL_INERT_GAS]      
				  --    --,[S_SEMI_HOSES]      
				  --    --,[HYDRANTS]      
				  --    --,[SHOWERS]      
				  --    --,[SHOWER_CLASSIFICATION]      
				  --    --,[FIRE_CORPS]      
				  --    --,[PUNCTUATION_QUEST]      
				  --    --,[DMP]      
				  --    --,[EXPLOSION_DEGREE]      
				  --    --,[PR_LIQUID]      
				  --    --,[COD_ATIV_DRAFTS]      
				  --    --,[OCCUPATION_TEXT]      
				  --    ,[ASSIST24] --10964 default      
				  --    --,[LMRA]      
				  --    --,[AGGRAVATION_RCG_AIR]      
				  --    --,[EXPLOSION_DESC]      
				  --    --,[PROTECTIVE_DESC]      
				  --    --,[LMI_DESC]      
				  --    --,[LOSS_DESC]      
				  --    --,[QUESTIONNAIRE_DESC]      
				  --    --,[DEDUCTIBLE_DESC]    
				  --    --,[GROUPING_DESC]      
				  --    --,[LOC_FLOATING]      
				  --    --,[ADJUSTABLE]      
				   ,[IS_ACTIVE]      
				 ,[CREATED_BY]      
				   ,[CREATED_DATETIME]      
				  --    --,[MODIFIED_BY]      
				  --    --,[LAST_UPDATED_DATETIME]      
				  --    --,[CORRAL_SYSTEM]      
				  --    ,[RAWVALUES]  -- 'N'      
				  --    --,[REMARKS]      
				  --    --,[PARKING_SPACES]      
				  --    --,[CLAIM_RATIO]      
				  --    --,[RAW_MATERIAL_VALUE]      
				  --    --,[CONTENT_VALUE]      
				  --    --,[BONUS]      
				  --    ,[CO_APPLICANT_ID]      
				 ,[LOCATION_NUMBER]      
				 ,[ITEM_NUMBER]    
				 ,CO_RISK_ID  
						   )      
				  SELECT      
			               
				  @CUST_ID,      
				  @POLICY_ID,      
				  @ENDORSEMENT_NUMBER_VAR,   ----RELOOK ? Policy_Vesion_ID   
				  CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PERIL_ID IS NOT NULL)
					THEn  @OLD_PERIL_ID
					ELSE  @PERIL_ID
					END,
					CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_LOCATION_NUMBER IS NOT NULL)
							THEn @OLD_LOCATION_NUMBER
							ELSE
								@LOCATION_NUMBER
							END ,      
				  --A.LOCATION_OCCUPIED_AS,      
				  --A.LOCATION_CONSTRUCTION_TYPE,      
				  --A.LOCATION_ACTIVITY,      
				  --SUM(C.SUM_INSURED),      
				  --SUM(C.SUM_INSURED),      
				 -- 10964, 
				  @VALUE_OF_RISK, --VR
				  @SUM_INSURED,   --LMI
				       
				  'Y',      
				  @CREATED_BY,      
				  GETDATE(),  
				  @POLICY_RISK_ID,
				  @POLICY_RISK_ID ,  
				  @POLICY_RISK_ID     
				  
			       
				   --UPDATE [POL_PERILS]  
				   --SET   LMI			= @SUM_INSURED,
				   --      VR				= @VALUE_OF_RISK 
				   --WHERE CUSTOMER_ID    = @CUST_ID  
				   --AND   POLICY_ID		= @POLICY_ID  
				   --AND   POLICY_VERSION_ID = @ENDORSEMENT_NUMBER_VAR  
				   --AND   CO_RISK_ID		   = @POLICY_RISK_ID 
			      
			      
				  SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PERIL_ID IS NOT NULL)
										THEN  @OLD_PERIL_ID
										ELSE  @PERIL_ID
										END
			   
			 
				   END      
			          
					ELSE  
			   	   BEGIN  
				
				 	SELECT @SYSTEM_RISK_ID =PERIL_ID FROM [POL_PERILS] WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID
			 
			       END  
			       
			   
			   END      
			        
			  END      
			     
		   ------------------------------------------------------------
		   --  FOR LOB : -
		   --	15	Individual Personal Accident
		   --	21	Group Personal Accident for Passengers
		   --	33	Mortgage
		   --	34	Group Life    
		   ------------------------------------------------------------    
			ELSE IF(@POLICY_LOB=21 or @POLICY_LOB=15 or @POLICY_LOB=33 or @POLICY_LOB=34)      
			  BEGIN      
			       
			   ------------------------------------------------------------  
			   --SET PERSONAL INFO ID    
			   ------------------------------------------------------------     
				SELECT  @PERSONAL_INFO_ID=(ISNULL(MAX(PERSONAL_INFO_ID),0)+1)      
				FROM   POL_PERSONAL_ACCIDENT_INFO (NOLOCK)      
				WHERE  CUSTOMER_ID		 = @CUST_ID      
				AND    POLICY_ID		 = @POLICY_ID      
				AND    POLICY_VERSION_ID = @POLICY_VERSION_ID      
			     
			   DECLARE @OLD_PERSONAL_INFO_ID INT
			   IF(@FILE_ENDORSEMENT_NUMBER>0)
			   BEGIN
				SELECT  @OLD_PERSONAL_INFO_ID = PERSONAL_INFO_ID,
						@OLD_CPAPP_ID		  = APPLICANT_ID
				FROM   POL_PERSONAL_ACCIDENT_INFO (NOLOCK)      
				WHERE  CUSTOMER_ID			  = @CUST_ID      
				AND    POLICY_ID			  = @POLICY_ID      
				AND    POLICY_VERSION_ID      = @POLICY_VERSION_ID-1     
				AND    CO_RISK_ID		      = @POLICY_RISK_ID
			   END
			     
			   IF NOT EXISTS (SELECT 1 FROM [POL_PERSONAL_ACCIDENT_INFO] (NOLOCK)   
				WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID)  
				BEGIN    
			         
				INSERT INTO [dbo].[POL_PERSONAL_ACCIDENT_INFO]      
			   ([PERSONAL_INFO_ID]      
			   ,[POLICY_ID]      
			   ,[POLICY_VERSION_ID]      
			   ,[CUSTOMER_ID]      
			   ,[APPLICANT_ID]      
			   ,[INDIVIDUAL_NAME]      
			   ,[CODE]      
			   ,[POSITION_ID]      
			   ,[CPF_NUM]      
			   ,[STATE_ID]      
			   ,[COUNTRY_ID]      
			   ,[DATE_OF_BIRTH]      
			   ,[GENDER]      
			   ,[REG_IDEN]      
			   ,[REG_ID_ISSUES]      
				,[REG_ID_ORG]      
			   --   ,[REMARKS]      
			   ,[IS_ACTIVE]      
			   ,[CREATED_BY]      
			   ,[CREATED_DATETIME]      
			               
				 -- ,[MODIFIED_BY]      
			   --,[LAST_UPDATED_DATETIME]    
			   ,CO_RISK_ID  
			   ,CITY_OF_BIRTH  
			   )      
			   SELECT   
			   CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PERSONAL_INFO_ID IS NOT NULL)    
				THEN @OLD_PERSONAL_INFO_ID
				ELSE @PERSONAL_INFO_ID
				END
			   ,@POLICY_ID      
			   ,@ENDORSEMENT_NUMBER_VAR      
			   ,@CUST_ID      
			   ,@CO_APPLICANT      
			   ,@CO_APPLICANT_NAME       
			   ,@CO_APPLICANT_CODE    
			   ,@POSITION_ID     
			   ,@CO_APPLICANT_CPF  
			   ,@STATE
			   ,@COUNTRY    
				,@CO_APPLICANT_DOB   
			   ,@GENDER  
			         
			   ,@CO_APPLICANT_REGIONAL_IDENTIFICATION  
			   ,@CO_APPLICANT_REG_ID_ISSUE     
			   ,@CO_APPLICANT_ORIGINAL_ISSUE  
			   --,[REMARKS]       
			   ,'Y'      
			   ,@CREATED_BY      
			   ,GETDATE()     
			   ,@POLICY_RISK_ID   
			   ,@DEFAULT  
			          
          
			   SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PERSONAL_INFO_ID IS NOT NULL)
			   THEN  @OLD_PERSONAL_INFO_ID
			   ELSE  @PERSONAL_INFO_ID
			   END

			  END            
			  ELSE  
			  BEGIN  
    			  SELECT @SYSTEM_RISK_ID =PERSONAL_INFO_ID FROM [POL_PERSONAL_ACCIDENT_INFO] WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID
			   END  
			   END  
	   ------------------------------------------------------------
	   --  FOR LOB : -
	   --	17	Facultative Liability
	   --	18	Civil Liability Transportation
	   --	28	Aeronautic
	   --	29	Motor
	   --   30	Dpvat(Cat. 3 e 4)
	   --	36	DPVAT(Cat.1,2,9 e 10)  
	   ------------------------------------------------------------  
	   ELSE IF(@POLICY_LOB=17 or @POLICY_LOB=28 or @POLICY_LOB=18 or @POLICY_LOB=29      
			 or @POLICY_LOB=31 or @POLICY_LOB=30 or @POLICY_LOB=36)      
	    BEGIN      
			       
			       
			 ----------------------------------------------- 
			 -- SET VEHICLE ID      
			 -----------------------------------------------       
			 SELECT     @VEHICALE_ID=(ISNULL(MAX(VEHICLE_ID),0)+1)      
			 FROM     POL_CIVIL_TRANSPORT_VEHICLES (NOLOCK)      
			 WHERE     CUSTOMER_ID=@CUST_ID      
			 AND      POLICY_ID=@POLICY_ID      
			 AND      POLICY_VERSION_ID=@POLICY_VERSION_ID      
			       
		
			   
			   DECLARE @OLD_VEHICLE_ID INT
			   IF(@FILE_ENDORSEMENT_NUMBER >0) 
			   BEGIN
				 SELECT     @OLD_VEHICLE_ID = VEHICLE_ID ,
							@OLD_CPAPP_ID	= CO_APPLICANT_ID  
				 FROM     POL_CIVIL_TRANSPORT_VEHICLES (NOLOCK)      
				 WHERE     CUSTOMER_ID		= @CUST_ID      
				 AND      POLICY_ID			= @POLICY_ID      
				 AND      POLICY_VERSION_ID = @POLICY_VERSION_ID-1
				 AND		CO_RISK_ID		= @POLICY_RISK_ID  
			   END
			     
			   IF NOT EXISTS (SELECT 1 FROM POL_CIVIL_TRANSPORT_VEHICLES (NOLOCK)   
				WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID )  
				BEGIN    
			       
			       
			      
			  INSERT INTO POL_CIVIL_TRANSPORT_VEHICLES       
					   (      
					   [CUSTOMER_ID]      
					   ,[POLICY_ID]      
					   ,[POLICY_VERSION_ID]      
					   ,[VEHICLE_ID]      
					   ,[CLIENT_ORDER]      
					   ,[VEHICLE_NUMBER]      
					   ,[MANUFACTURED_YEAR]      
					   ,[FIPE_CODE]      
					   --,[CATEGORY]      
					 --,[CAPACITY]      
					   --,[MAKE_MODEL]      
					   ,[LICENSE_PLATE]      
					   ,[CHASSIS]      
					   --,[MANDATORY_DEDUCTIBLE]      
					   --,[FACULTATIVE_DEDUCTIBLE]      
					   --,[SUB_BRANCH]      
					   ,[RISK_EFFECTIVE_DATE]      
					   ,[RISK_EXPIRE_DATE]      
					   --,[REGION]      
					   --,[COV_GROUP_CODE]      
					   --,[FINANCE_ADJUSTMENT]      
					   --,[REFERENCE_PROPOSASL]      
					   --,[REMARKS]      
					   ,[IS_ACTIVE]      
					   ,[CREATED_BY]      
					   ,[CREATED_DATETIME]      
					--   ,[MODIFIED_BY]      
					 --  ,[LAST_UPDATED_DATETIME]      
					  -- ,[VEHICLE_PLAN_ID]      
					  -- ,[VEHICLE_MAKE]      
					   ,[CO_APPLICANT_ID]      
					   --,[TICKET_NUMBER]      
					   --,[STATE_ID]      
					   ,[ZIP_CODE]    
					   ,CO_RISK_ID    
					   )        
					   SELECT       
			   @CUST_ID      
			   ,@POLICY_ID      
			   ,@ENDORSEMENT_NUMBER_VAR      
			   ,CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_VEHICLE_ID IS NOT NULL)
			   THEn @OLD_VEHICLE_ID
			   ELSE @VEHICALE_ID END
			 
			   ,@POLICY_RISK_ID  
			   ,@POLICY_RISK_ID  
			   ,@MANUFACTURE_YEAR  
			   ,@DEFAULT_FIPE_CODE  
			   ,SUBSTRING(@DEFAULT,1,7)      --[LICENSE_PLATE]
			   ,@DEFAULT  
			   ,@POLICY_EFFECTIVE_DATE  
			   ,@POLICY_EXPIRY_DATE  
			   ,'Y'      
			   ,@CREATED_BY      
			   ,GETDATE()  
			   ,CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_VEHICLE_ID IS NOT NULL)
				THEN @OLD_CPAPP_ID
				ELSE @CO_APPLICANT
				END
			   
			     
			   ,@DEFAULT_ZIP_CODE  
			   ,@POLICY_RISK_ID  
			     
			   --FROM  
			     
			   --#MIG_POLICY_RISK (NOLOCK)  
			   --WHERE ID_IDENTITY=@NO_OF_RISK  
			   
				  SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_VEHICLE_ID IS NOT NULL)
										THEN  @OLD_VEHICLE_ID
										ELSE  @VEHICALE_ID
										END
										
										
			        

			 END                
			   ELSE  
				BEGIN  
				
				SELECT @SYSTEM_RISK_ID =VEHICLE_ID FROM POL_CIVIL_TRANSPORT_VEHICLES WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID

			
			     
				END       
			            
			   END   
			   
			 ELSE IF (@POLICY_LOB=20 or @POLICY_LOB=23)          
			 BEGIN      
			  ----------------------------------------SET COMMODITY ID      
			        
			  SELECT    @COMMODITY_ID=(ISNULL(MAX(COMMODITY_ID),0)+1)      
			  FROm    [POL_COMMODITY_INFO] (NOLOCK)      
			  WHERE    CUSTOMER_ID=@CUST_ID      
			  AND     POLICY_ID=@POLICY_ID      
			  AND     POLICY_VERSION_ID=@POLICY_VERSION_ID     
			   
			   DECLARE @OLD_COMMODITY_ID INT  
				IF(@FILE_ENDORSEMENT_NUMBER>0)
				BEGIN
				  SELECT    @OLD_COMMODITY_ID=COMMODITY_ID,
							@OLD_CPAPP_ID=CO_APPLICANT_ID
			  FROm    [POL_COMMODITY_INFO] (NOLOCK)      
			  WHERE    CUSTOMER_ID=@CUST_ID      
			  AND     POLICY_ID=@POLICY_ID      
			  AND     POLICY_VERSION_ID=@POLICY_VERSION_ID   -1
			  AND		CO_RISK_ID=@POLICY_RISK_ID
				END
			     
			   IF NOT EXISTS (SELECT 1 FROM [POL_COMMODITY_INFO] (NOLOCK)   
				WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID)  
				BEGIN      
			  --SELECT @COMMODITY_ID      
			  INSERT INTO [dbo].[POL_COMMODITY_INFO]      
					   (      
						[CUSTOMER_ID]      
					,[POLICY_ID]      
					   ,[POLICY_VERSION_ID]      
					   ,[COMMODITY_ID]      
					   ,[COMMODITY_NUMBER]      
					   ,[COMMODITY]      
					   --,[CONVEYANCE]    
					   ,CONVEYANCE_TYPE
					   --,[SUM_INSURED]      
					   ,[DEPARTING_DATE]      
					   --,[ARRIVAL_DATE]      
					   --,[ORIGIN_COUNTRY] 
					   ,ORIGN_COUNTRY     
					   ,ORIGN_STATE
					   --,[ORIGIN_STATE] 
					     
					   
					   ,[ORIGIN_CITY]      
					   --,[DESTINATION_COUNTRY]  
					    ,DEST_COUNTRY      
					   --,[DESTINATION_STATE]      
					   ,DEST_STATE
					   ,[DESTINATION_CITY]      
					   --,[REMARKS]      
					   ,[IS_ACTIVE]      
					   ,[CREATED_BY]      
					   ,[CREATED_DATETIME]      
					  -- ,[MODIFIED_BY]      
					  -- ,[LAST_UPDATED_DATETIME]      
					  -- ,[CONVEYANCE_TYPE]      
					   ,[CO_APPLICANT_ID]     
					   ,CO_RISK_ID  
					   )      
					   SELECT       
					   @CUST_ID,      
					   @POLICY_ID,      
					   @ENDORSEMENT_NUMBER_VAR  
					   ,CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_COMMODITY_ID IS NOT NULL)   
							THEN @OLD_COMMODITY_ID
						ELSE @COMMODITY_ID
						END
					   ,@POLICY_RISK_ID,    
			   @DEFAULT  
			   ,@CONVEYANCE_TYPE  
			   ,@POLICY_EFFECTIVE_DATE
			   ,@DEFAULT  
			   ,@DEFAULT  
			   ,@DEFAULT  
			   ,@DEFAULT  
			   ,@DEFAULT  
			   ,@DEFAULT  
					   ,'Y'     
					   ,@CREATED_BY  
					   ,GETDATE()  
						,CASE   WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_CPAPP_ID IS NOT NULL)
							THEN @OLD_CPAPP_ID
							ELSE @CO_APPLICANT
							END
			            
						,@POLICY_RISK_ID  
					   --FROM #MIG_POLICY_RISK (NOLOCK)  
			             
					   --WHERE ID_IDENTITY=@NO_OF_RISK  
			           
				 SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_COMMODITY_ID IS NOT NULL)
										THEN  @OLD_COMMODITY_ID
										ELSE  @COMMODITY_ID
										END            
			  
			   
			     

			               
			 END      
			 ELSE  
			 BEGIN  
			 
			 SELECT @SYSTEM_RISK_ID =COMMODITY_ID FROM [POL_COMMODITY_INFO] WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID

		
			 END  
			   
			 END  
			       
			 ELSE IF(@POLICY_LOB=13)      
			 BEGIN      
			 ------------------------------------------------  SET MERITIME ID      
			       
			 SELECT     @MERITIME_ID=(ISNULL(MAX(MARITIME_ID),0)+1)      
			 FROM     [POL_MARITIME] (NOLOCK)      
			 WHERE     CUSTOMER_ID=@CUST_ID      
			 AND      POLICY_ID=@POLICY_ID      
			 AND    POLICY_VERSION_ID=@POLICY_VERSION_ID     
			 
			 
			 DECLARE @OLD_MERITIME_ID INT
			 IF(@FILE_ENDORSEMENT_NUMBER>0)  
			 BEGIN
				 SELECT     @OLD_MERITIME_ID=MARITIME_ID,
							@OLD_CPAPP_ID=CO_APPLICANT_ID    
			 FROM     [POL_MARITIME] (NOLOCK)      
			 WHERE     CUSTOMER_ID=@CUST_ID      
			 AND      POLICY_ID=@POLICY_ID      
			 AND      POLICY_VERSION_ID=@POLICY_VERSION_ID-1
			 AND		CO_RISK_ID=@POLICY_RISK_ID    
			 END
			     
			   IF NOT EXISTS (SELECT 1 FROM [POL_MARITIME] (NOLOCK)   
				WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID)  
				BEGIN       
			  INSERT INTO [dbo].[POL_MARITIME]      
			  ([CUSTOMER_ID]      
					   ,[POLICY_ID]      
					   ,[POLICY_VERSION_ID]      
					   ,[MARITIME_ID]      
					   ,[VESSEL_NUMBER]      
					 ,[NAME_OF_VESSEL]      
					  ,[TYPE_OF_VESSEL]      
					  ,[MANUFACTURE_YEAR]      
					   --,[MANUFACTURER]      
					   --,[BUILDER]      
					   --,[CONSTRUCTION]      
					   --,[PROPULSION]      
					   --,[CLASSIFICATION]      
					   --,[LOCAL_OPERATION]      
					   --,[LIMIT_NAVIGATION]      
					   --,[PORT_REGISTRATION]      
					   --,[REGISTRATION_NUMBER]      
					   --,[TIE_NUMBER]      
					   --,[VESSEL_ACTION_NAUTICO_CLUB]      
					   --,[NAME_OF_CLUB]      
					   --,[LOCAL_CLUB]      
					   --,[NUMBER_OF_CREW]      
					   --,[NUMBER_OF_PASSENGER]      
					   --,[REMARKS]      
					   ,[IS_ACTIVE]      
					   ,[CREATED_BY]      
					   ,[CREATED_DATETIME]      
					   --,[LAST_UPDATED_DATETIME]      
					   --,[MODIFIED_BY]      
					   ,[CO_APPLICANT_ID]      
					   ,CO_RISK_ID  
					   )      
					   SELECT      
					   @CUST_ID,      
					   @POLICY_ID,      
					   @ENDORSEMENT_NUMBER_VAR,    
					   CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_MERITIME_ID IS NOT NULL  )
					   THEN	@OLD_MERITIME_ID
					   ELSE @MERITIME_ID
					   END,
			           
					   @POLICY_RISK_ID,  
					   @DEFAULT,  
					   @DEFAULT,  
						@MANUFACTURE_YEAR,  
					   --A.VESSEL_NUMBER,      
					   --A.VESSEL_NUMBER,      
					   --A.VESSEL_NAME,      
					   --A.VESSEL_TYPE,      
					   --A.MANUFACTURE_YEAR,      
					  -- 'CLUB',      
					   'Y',      
					   @CREATED_BY,      
					   GETDATE(),      
					   @CO_APPLICANT      
					   ,@POLICY_RISK_ID  
			           
					 SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_MERITIME_ID IS NOT NULL)
										THEN  @OLD_MERITIME_ID
										ELSE  @MERITIME_ID
										END               
			                 

			   END  
			   ELSE  
			   BEGIN  
			   
			   			 SELECT @SYSTEM_RISK_ID =MARITIME_ID FROM [POL_MARITIME] WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID

			
			   END            
			     
			 END      
			       
			 ELSE IF (@POLICY_LOB=35 OR @POLICY_LOB=37)      
			 BEGIN      
			       
			 ------------------------------------  SET PENHOR RURAL IINFO ID      
			       
			 SELECT     @PENHOR_RURAL_ID=(ISNULL(MAX(PENHOR_RURAL_ID),0)+1)      
			 FROM     [POL_PENHOR_RURAL_INFO] (NOLOCK)      
			 WHERE     CUSTOMER_ID=@CUST_ID      
			 AND      POLICY_ID=@POLICY_ID      
			 AND      POLICY_VERSION_ID=@POLICY_VERSION_ID      
			    
			DECLARE @OLD_PENHOR_RURAL_ID INT

			IF(@FILE_ENDORSEMENT_NUMBER>0)
			BEGIN
				       
			 SELECT     @PENHOR_RURAL_ID=(ISNULL(MAX(PENHOR_RURAL_ID),0)+1)      
			 FROM     [POL_PENHOR_RURAL_INFO] (NOLOCK)      
			 WHERE     CUSTOMER_ID=@CUST_ID      
			 AND      POLICY_ID=@POLICY_ID      
			 AND      POLICY_VERSION_ID=@POLICY_VERSION_ID-1 
			 AND		CO_RISK_ID=@POLICY_RISK_ID   
			END
			     
			   IF NOT EXISTS (SELECT 1 FROM [POL_PENHOR_RURAL_INFO] (NOLOCK)   
				WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID)  
				BEGIN   
			        
			 INSERT INTO [dbo].[POL_PENHOR_RURAL_INFO]      
					   ([CUSTOMER_ID]      
					   ,[POLICY_ID]      
					   ,[POLICY_VERSION_ID]      
					   ,[PENHOR_RURAL_ID]      
					   ,[ITEM_NUMBER]      
					   --,[FESR_COVERAGE]      
					   --,[MODE]      
					   --,[PROPERTY]      
					   --,[CULTIVATION]      
					   --,[CITY]      
					   --,[STATE_ID]      
					   --,[INSURED_AREA]      
					   --,[SUBSIDY_PREMIUM]      
					   --,[SUBSIDY_STATE]      
					   ,[IS_ACTIVE]      
					   ,[CREATED_BY]      
					   ,[CREATED_DATETIME]      
					   --,[MODIFIED_BY]      
					   --,[LAST_UPDATED_DATETIME]      
					   --,[REMARKS]      
					   ,CO_RISK_ID  
					   )      
			                 
					   SELECT       
					   @CUST_ID,      
					   @POLICY_ID,      
					   @ENDORSEMENT_NUMBER_VAR,   
					   CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PENHOR_RURAL_ID IS NOT NULL)
					   THEN    @OLD_PENHOR_RURAL_ID
					   ELSE		@PENHOR_RURAL_ID
					   END,    
					   @POLICY_RISK_ID,    
			   --        A.ITEM,      
			   --        CASE WHEN (@POLICY_LOB=37)      
			   --THEN 10964      
			   --ELSE      
			   --        A.[FESR_ COVERAGE]      
			   --        END,      
			   --        A.MODE,      
			   --        A.PROPERTY,      
			   --        A.CULTIVATION,      
			   --        A.CITY,      
			   --        C.STATE_ID,      
			   --        A.INSURED_AREA,      
			   --        A.SUBSIDY_PREMIUM,      
			   --        D.STATE_ID,      
					   'y',      
					   @CREATED_BY,      
					   GETDATE()   
					   ,@POLICY_RISK_ID     
			    
					SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PENHOR_RURAL_ID IS NOT NULL)
										THEN  @OLD_PENHOR_RURAL_ID
										ELSE  @PENHOR_RURAL_ID
										END               
			                        

				  END  
			        
				  ELSE  
				  BEGIN  
				  
				 SELECT @SYSTEM_RISK_ID =PENHOR_RURAL_ID FROM [POL_PENHOR_RURAL_INFO] WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID

										
			
			     
			   END  
			                 
			 END      
			       
			 ELSE IF(@POLICY_LOB=22)      
			 BEGIN      
			  ---------------------------------------  SET PERSONAL ACCIDENT ID      
			        
			  SELECT     @PERSONAL_ACCIDENT_ID=(ISNULL(MAX(PERSONAL_ACCIDENT_ID),0)+1)      
			  FROm     POL_PASSENGERS_PERSONAL_ACCIDENT_INFO (NOLOCK)      
			  WHERE     CUSTOMER_ID=@CUST_ID      
			  AND      POLICY_ID=@POLICY_ID      
			  AND      POLICY_VERSION_ID=@POLICY_VERSION_ID     
			    
			 DECLARE @OLD_PERSONAL_ACCIDENT_ID INT
			 IF(@FILE_ENDORSEMENT_NUMBER>0 )
			 BEGIN
				  SELECT     @OLD_PERSONAL_ACCIDENT_ID=PERSONAL_ACCIDENT_ID ,
							@OLD_CPAPP_ID=CO_APPLICANT_ID   
			  FROm     POL_PASSENGERS_PERSONAL_ACCIDENT_INFO (NOLOCK)      
			  WHERE     CUSTOMER_ID=@CUST_ID      
			  AND      POLICY_ID=@POLICY_ID      
			  AND      POLICY_VERSION_ID=@POLICY_VERSION_ID -1
			  AND		CO_RISK_ID=@POLICY_RISK_ID
			 END  
			   IF NOT EXISTS (SELECT 1 FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO (NOLOCK)   
				WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID)  
				BEGIN      
				  INSERT INTO POL_PASSENGERS_PERSONAL_ACCIDENT_INFO      
					   (      
						[CUSTOMER_ID]       
					   ,[POLICY_ID]       
					   ,[POLICY_VERSION_ID]       
					   ,[PERSONAL_ACCIDENT_ID]       
					   ,[START_DATE]       
					   ,[END_DATE]       
					  ,[NUMBER_OF_PASSENGERS]       
					   ,[IS_ACTIVE]       
					   ,[CREATED_BY]       
					   ,[CREATED_DATETIME]       
					   --,[MODIFIED_BY]      
					   --,[LAST_UPDATED_DATETIME]      
					   ,[CO_APPLICANT_ID]   
					   ,CO_RISK_ID  
					   )      
					SELECT      
					@CUST_ID,      
					@POLICY_ID,      
					@ENDORSEMENT_NUMBER_VAR, 
					CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PERSONAL_ACCIDENT_ID IS NOT NULL)
					THEN		@OLD_PERSONAL_ACCIDENT_ID
					ELSE	     @PERSONAL_ACCIDENT_ID
					END,
			        
				   @POLICY_EFFECTIVE_DATE,  
				   @POLICY_EXPIRY_DATE,      
					@DEFAULT_NO_OF_PASSENGERS,      
					'Y',      
					@CREATED_BY,      
					GETDATE(),  
					 CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_CPAPP_ID IS NOT NULL)   
					 THEN @OLD_CPAPP_ID
					 ELSE @CO_APPLICANT
						END
					,@POLICY_RISK_ID   
					----FROM #MIG_POLICY_RISK  
					----WHERE ID_IDENTITY=@NO_OF_RISK  
			       
					SET @SYSTEM_RISK_ID = CASE WHEN (@FILE_ENDORSEMENT_NUMBER>0 AND @OLD_PERSONAL_ACCIDENT_ID IS NOT NULL)
										THEN  @OLD_PERSONAL_ACCIDENT_ID
										ELSE  @PERSONAL_ACCIDENT_ID
										END     
			  
			        

			   END  
			   ELSE  
			   BEGIN  
			   
			   	SELECT @SYSTEM_RISK_ID =[PERSONAL_ACCIDENT_ID] FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CO_RISK_ID=@POLICY_RISK_ID

				
				
			      
			   END  
			     
			     
			               
			   END
			   
		---------------------------    COVERAGES AREA  ---------------------------------------------------	
		-- STEP 1: INSERT ALL COVERAGES FOR SELECTED RISK IN TEMPRORY TABLE
		-- STEP 2: IF NBS THEN INSERT ALL COVERAGES DIRECTLY IN COV TABLE FROM TEMP TABLE 
        --		   ELSE END CHECK IF SELECTED COVERAGE ALREADY EXISTS THEN UPDATE IT OTHERWISE INSERT 		   
		--------------------------------------------------------------------------------------------------	     
		DECLARE @MAX_COVERAGE_ID INT =0
		
		SELECT @MAX_COVERAGE_ID =MAX(COVERAGE_ID)
		FROM POL_PRODUCT_COVERAGES 
		WHERE  CUSTOMER_ID=@CUST_ID AND POLICY_ID  =@POLICY_ID AND POLICY_VERSION_ID=@ENDORSEMENT_NUMBER_VAR AND RISK_ID=@SYSTEM_RISK_ID
		
		
		
		-------------------------------- INSERT INTO TEMORARY TABLE
			  
			   INSERT INTO #COVERAGE_ITEM      
			   (      
						
					    [RISK_ID]      
					   ,[COVERAGE_CODE_ID]      
					   ,[LIMIT_1]       
						,[INITIAL_RATE]      
						,[FINAL_RATE]      
						,[WRITTEN_PREMIUM] 
						,ACC_CO_DISCOUNT     
			   )      
				 SELECT  
				  @SYSTEM_RISK_ID      
				 ,MC.COV_ID      
				 ,SUM(MPC.SUM_INSURED)			--NEW IMPLEMENTION FOR SOME LEADER COVERAGE CODE THERE IS SINGLE ALBA COVERAGE CODE      
				 ,MAX(MPC.INITIAL_RATE)			-- MAX
				 ,MAX(MPC.FINAL_RATE)			-- MAX
				 ,SUM(MPC.RISK_PREMIUM)			-- SUM	
				 ,MAX(MPC.DISCOUNT_PERCENTAGE)
				  FROM      
				 MIG_POLICY_COVERAGES (NOLOCK) MPC      
				 JOIN      
				 MIG_POLICY_COVERAGE_CODE_MAPPING (NOLOCK)MPCM      
				 ON MPCM.LEADER_COVERAGE_CODE=CAST(MPC.LEADER_COVERAGE_CODE  AS INT)    
				 JOIN      
				  MNT_COVERAGE (NOLOCK) MC      
				 ON MC.CARRIER_COV_CODE=MPCM.ALBA_COVERAGE_CODE 
				 WHERE 		IMPORT_REQUEST_ID=			@FILE_IMPORT_REQUEST_ID
				 --LEFT OUTER JOIN 
				 --POL_PRODUCT_COVERAGES (NOLOCK) PPC
				 --ON PPC.CUSTOMER_ID=@CUST_ID AND PPC.POLICY_ID  =@POLICY_ID AND PPC.POLICY_VERSION_ID=@ENDORSEMENT_NUMBER_VAR AND PPC.RISK_ID=@SYSTEM_RISK_ID
				 --AND PPC.COVERAGE_CODE_ID=MC.COV_ID  
				 
				 GROUP BY MC.COV_ID,MPC.HAS_ERRORS,MPC.IS_DELETED,MPC.LEADER_POLICY_NUMBER
				 , MPC.LEADER_ENDORSEMENT_NUMBER,MC.SUB_LOB_ID,MPC.POLICY_RISK_ID,MC.LOB_ID 
				 HAVING
				 MPC.HAS_ERRORS='N'      
				AND      MPC.IS_DELETED='N'      
				AND      MPC.LEADER_POLICY_NUMBER='00'+@FILE_POLICY_NUMBER      
				AND      MPC.LEADER_ENDORSEMENT_NUMBER=@FILE_ENDORSEMENT_NUMBER      
				AND      MC.SUB_LOB_ID=@POLICY_SUB_LOB   
				AND   MC.LOB_ID=@POLICY_LOB   
				AND    MPC.POLICY_RISK_ID= @POLICY_RISK_ID  
				
				
				--select @POLICY_RISK_ID as '@POLICY_RISK_ID',@FILE_POLICY_NUMBER,@FILE_ENDORSEMENT_NUMBER,@POLICY_LOB,@POLICY_SUB_LOB
				
		   DECLARE @COV_ID INT		
        
           --UPDATE #COVERAGE_ITEM
           --SET    COVERAGE_ID = 
           
           --=ISNULL(PPC.COVERAGE_ID, @MAX_COVERAGE_ID+row_number() OVER(ORDER BY CI.ID asc)) 
           --FROM  #COVERAGE_ITEM CI LEFT OUTER JOIN
           --       POL_PRODUCT_COVERAGES PPC ON PPC.CUSTOMER_ID=@CUST_ID AND PPC.POLICY_ID  =@POLICY_ID 
           --          AND PPC.POLICY_VERSION_ID=@ENDORSEMENT_NUMBER_VAR AND PPC.RISK_ID=@SYSTEM_RISK_ID AND
           --          CI.COVERAGE_CODE_ID=PPC.COVERAGE_CODE_ID
                     
			    
			    
  
        
			   -- select * FROM #COVERAGE_ITEM
		
				 DECLARE @TOTAL_RECORD_COUNT INT
				 SELECT @TOTAL_RECORD_COUNT=count(ID) FROM #COVERAGE_ITEM
				 DECLARE @COUNT INT =0
				 
				 --IF(@FILE_ENDORSEMENT_NUMBER>0)
				 --BEGIN
				 
				 DECLARE @LOOP_LIMIT_1			 DECIMAL(18,4),
					     @LOOP_INITIAL_RATE	     DECIMAL(18,4),
					     @LOOP_FINAL_RATE	     DECIMAL(18,4),
					     @LOOP_WRITTEN_PREMIUM   DECIMAL(18,4),
					     @LOOP_COVERAGE_ID       DECIMAL(18,4),
						 @ACC_CO_DISCOUNT		 DECIMAL(18,4)	
				 
				 WHILE(@COUNT<@TOTAL_RECORD_COUNT)
				 BEGIN
						 SET @COUNT=(@COUNT+1)
						 
						SET @LOOP_COVERAGE_ID=0
						
						--select @SYSTEM_RISK_ID as '@SYSTEM_RISK_ID'
						--SELECT [COVERAGE_CODE_ID] as COV_ID,RISK_ID FROM #COVERAGE_ITEM
						--WHERE ID=@COUNT
						
						--SELECT @COUNT as '@COUNT'
						
						--SELECT RISK_ID as '@---RSIK' FROM POL_PRODUCT_COVERAGES (NOLOCK) 
						--WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
						--AND RISK_ID=@SYSTEM_RISK_ID AND COVERAGE_CODE_ID =(SELECT [COVERAGE_CODE_ID] FROM #COVERAGE_ITEM
						--WHERE ID=@COUNT)
						
						SELECT @LOOP_COVERAGE_ID=COVERAGE_ID FROM POL_PRODUCT_COVERAGES (NOLOCK) 
						WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
						AND RISK_ID=@SYSTEM_RISK_ID AND 
						COVERAGE_CODE_ID =(SELECT [COVERAGE_CODE_ID] FROM #COVERAGE_ITEM WHERE ID=@COUNT)
						
						--SELECT @LOOP_COVERAGE_ID as '@LOOP_COVERAGE_ID'
						
						-------------------------------- UPDATE COVERAGES WITH FILE INFORMATION IN CASE OF END
						IF (@LOOP_COVERAGE_ID>0)
						BEGIN
						
					            -------------------------------------------
					            -- UPDATE EXISTS COVERAGE DETAILS
					            -------------------------------------------
					            SELECT	@LOOP_LIMIT_1	      =	LIMIT_1	,
									  	@LOOP_INITIAL_RATE	  = INITIAL_RATE,
									  	@LOOP_FINAL_RATE	  = FINAL_RATE,
									  	@LOOP_WRITTEN_PREMIUM = WRITTEN_PREMIUM,
									  	@ACC_CO_DISCOUNT	  = ACC_CO_DISCOUNT
							    FROM    #COVERAGE_ITEM
					            WHERE   ID=@COUNT
					            
					     --       select @LOOP_LIMIT_1 as'@LOOP_LIMIT_1', 
					     --       @LOOP_INITIAL_RATE as '@LOOP_INITIAL_RATE',
					     --       @LOOP_FINAL_RATE as '@LOOP_FINAL_RATE',
					     --       @LOOP_WRITTEN_PREMIUM as'@LOOP_WRITTEN_PREMIUM'
					            
					            
					     --       select * FROM [POL_PRODUCT_COVERAGES] WHERE	CUSTOMER_ID			    =		@CUST_ID           AND	
										--POLICY_ID			    =		@POLICY_ID		   AND	
										--POLICY_VERSION_ID	    =		@POLICY_VERSION_ID AND	
										--RISK_ID				    =		@SYSTEM_RISK_ID	   
					            
								UPDATE	[POL_PRODUCT_COVERAGES]      
								SET		[LIMIT_1]				=		@LOOP_LIMIT_1,
										[INITIAL_RATE]			=		@LOOP_INITIAL_RATE,
										[FINAL_RATE]			=		@LOOP_FINAL_RATE,
										[WRITTEN_PREMIUM]		=		@LOOP_WRITTEN_PREMIUM,
										ACC_CO_DISCOUNT			=		@ACC_CO_DISCOUNT
								WHERE	CUSTOMER_ID			    =		@CUST_ID           AND	
										POLICY_ID			    =		@POLICY_ID		   AND	
										POLICY_VERSION_ID	    =		@POLICY_VERSION_ID AND	
										RISK_ID				    =		@SYSTEM_RISK_ID	   AND	
										COVERAGE_ID			    =		@LOOP_COVERAGE_ID			
								
							
								
								
						END
						
						ELSE -------------------------------- INSERT NEW COVERAGES IN CASE OF END
						BEGIN
							
							    -- GET MAXIUM COVERAGE ID FOR RISK	    
					            SELECT @LOOP_COVERAGE_ID= (ISNULL(MAX([COVERAGE_ID]),0)+1)  
					            FROM  [POL_PRODUCT_COVERAGES]
					            WHERE	CUSTOMER_ID			    =		@CUST_ID           AND	
										POLICY_ID			    =		@POLICY_ID		   AND	
										POLICY_VERSION_ID	    =		@POLICY_VERSION_ID AND	
										RISK_ID				    =		@SYSTEM_RISK_ID	   
										
					            -------------------------------------------
					            -- ADD NEW COVERAGE 
					            -------------------------------------------
								 INSERT INTO [dbo].[POL_PRODUCT_COVERAGES]      
									 ([CUSTOMER_ID]      
									 ,[POLICY_ID]      
									 ,[POLICY_VERSION_ID]      
									 ,[RISK_ID]      
									 ,[COVERAGE_ID]      
									,[COVERAGE_CODE_ID]      
									 --,[RI_APPLIES]      
									 --,[LIMIT_OVERRIDE]      
									 ,[LIMIT_1]      
									 --,[LIMIT_1_TYPE]      
									 --,[LIMIT_2]      
									 --,[LIMIT_2_TYPE]      
									 --,[LIMIT1_AMOUNT_TEXT]      
									 --,[LIMIT2_AMOUNT_TEXT]      
									 --,[DEDUCT_OVERRIDE]      
									 --,[DEDUCTIBLE_1]      
									 --,[DEDUCTIBLE_1_TYPE]      
									 --,[DEDUCTIBLE_2]      
									 --,[DEDUCTIBLE_2_TYPE]      
									 --,[MINIMUM_DEDUCTIBLE]      
									 --,[DEDUCTIBLE1_AMOUNT_TEXT]      
									 --,[DEDUCTIBLE2_AMOUNT_TEXT]      
									 --,[DEDUCTIBLE_REDUCES]      
									 ,[INITIAL_RATE]      
									 ,[FINAL_RATE]      
									 --,[AVERAGE_RATE]      
									 ,[WRITTEN_PREMIUM]      
									 --,[FULL_TERM_PREMIUM]      
									 --,[IS_SYSTEM_COVERAGE]      
									 --,[LIMIT_ID]      
									 --,[DEDUC_ID]      
									 --,[ADD_INFORMATION]      
									 ,[CREATED_BY]      
									 ,[CREATED_DATETIME]      
									 --,[MODIFIED_BY]      
									 --,[LAST_UPDATED_DATETIME]      
									 --,[INDEMNITY_PERIOD] 
									 ,ACC_CO_DISCOUNT     
									 )      
								   SELECT  @CUST_ID,      
								  @POLICY_ID,      
								  @ENDORSEMENT_NUMBER_VAR,      
								  [RISK_ID],      
								  @LOOP_COVERAGE_ID,      
								  [COVERAGE_CODE_ID],      
								  LIMIT_1,      
								  INITIAL_RATE,      
								  FINAL_RATE,      
								  WRITTEN_PREMIUM,      
								  @CREATED_BY, 
								    
								  GETDATE(), 
								   ACC_CO_DISCOUNT  
								  FROM #COVERAGE_ITEM (NOLOCK)   
								  WHERE ID=@COUNT  
					 END
				 END -- END OF WHILE(@COUNT<@MAX_COVERAGE_ID)
			     
				
				--END
				--ELSE  -------------------------------- INSERT COVERAGES DIRECTLY IN CASE OF NBS
				--  BEGIN	    
					
				--		INSERT INTO [dbo].[POL_PRODUCT_COVERAGES]      
				--	 ([CUSTOMER_ID]      
				--	 ,[POLICY_ID]      
				--	 ,[POLICY_VERSION_ID]      
				--	 ,[RISK_ID]      
				--	 ,[COVERAGE_ID]      
				--	,[COVERAGE_CODE_ID]      
				--	 --,[RI_APPLIES]      
				--	 --,[LIMIT_OVERRIDE]      
				--	 ,[LIMIT_1]      
				--	 --,[LIMIT_1_TYPE]      
				--	 --,[LIMIT_2]      
				--	 --,[LIMIT_2_TYPE]      
				--	 --,[LIMIT1_AMOUNT_TEXT]      
				--	 --,[LIMIT2_AMOUNT_TEXT]      
				--	 --,[DEDUCT_OVERRIDE]      
				--	 --,[DEDUCTIBLE_1]      
				--	 --,[DEDUCTIBLE_1_TYPE]      
				--	 --,[DEDUCTIBLE_2]      
				--	 --,[DEDUCTIBLE_2_TYPE]      
				--	 --,[MINIMUM_DEDUCTIBLE]      
				--	 --,[DEDUCTIBLE1_AMOUNT_TEXT]      
				--	 --,[DEDUCTIBLE2_AMOUNT_TEXT]      
				--	 --,[DEDUCTIBLE_REDUCES]      
				--	 ,[INITIAL_RATE]      
				--	 ,[FINAL_RATE]      
				--	 --,[AVERAGE_RATE]      
				--	 ,[WRITTEN_PREMIUM]      
				--	 --,[FULL_TERM_PREMIUM]      
				--	 --,[IS_SYSTEM_COVERAGE]      
				--	 --,[LIMIT_ID]      
				--	 --,[DEDUC_ID]      
				--	 --,[ADD_INFORMATION]      
				--	 ,[CREATED_BY]      
				--	 ,[CREATED_DATETIME]      
				--	 --,[MODIFIED_BY]      
				--	 --,[LAST_UPDATED_DATETIME]      
				--	 --,[INDEMNITY_PERIOD]      
				--	 )      
				--   SELECT  @CUST_ID,      
				--  @POLICY_ID,      
				--  @ENDORSEMENT_NUMBER_VAR,      
				--  [RISK_ID],      
				--  [COVERAGE_ID],      
				--  [COVERAGE_CODE_ID],      
				--  LIMIT_1,      
				--  INITIAL_RATE,      
				--  FINAL_RATE,      
				--  WRITTEN_PREMIUM,      
				--  @CREATED_BY,      
				--  GETDATE()      
			               
				--  FROM #COVERAGE_ITEM (NOLOCK)     
				--  END
			    
			  TRUNCATE TABLE #COVERAGE_ITEM 
			                  
			SET @NO_OF_RISK=(@NO_OF_RISK-1)      
			END      
   
			--------------------------------------- DROP TEMPORARY TABLE

   			DROP TABLE #MIG_POLICY_RISK   

			-----------------------------------------------------------------------------------------       
			-----------------------------*** UPDATE SOURCE TABLE ***--------------------------------- 
			-----------------------------------------------------------------------------------------
		IF(@HAS_ERRORS=0)  
		BEGIN  
		  
		  
		  UPDATE MIG_POLICY_COVERAGES      
		  SET POLICY_ID=@POLICY_ID,      
		   POLICY_VERSION_ID=@ENDORSEMENT_NUMBER_VAR,      
		   ALBA_POLICY_NUMBER=@APP_NUMBER,      
		   ALBA_ENDORSEMENT_NO=@ENDORSEMENT_NUMBER_VAR,      
		   CUSTOMER_ID=@CUST_ID  ,    
		   IS_DELETED='Y'    
		   WHERE IMPORT_REQUEST_ID=@FILE_IMPORT_REQUEST_ID AND  
				 LEADER_POLICY_NUMBER='00'+@FILE_POLICY_NUMBER  AND     
				 LEADER_ENDORSEMENT_NUMBER=@FILE_ENDORSEMENT_NUMBER   
          
           
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
  @IMPORT_REQUEST_ID    = @FILE_IMPORT_REQUEST_ID    
 ,@IMPORT_SERIAL_NO  = 0    
 ,@ERROR_NUMBER      = @ERROR_NUMBER    
 ,@ERROR_SEVERITY    = @ERROR_SEVERITY    
 ,@ERROR_STATE          = @ERROR_STATE    
 ,@ERROR_PROCEDURE   = @ERROR_PROCEDURE    
 ,@ERROR_LINE        = @ERROR_LINE    
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE    
      
     
         
     
 END CATCH   
 DROP TABLE #COVERAGE_ITEM
END      
      
      
      
  
  








GO

