IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MIG_COLOAD_INSERT_COVERAGE_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MIG_COLOAD_INSERT_COVERAGE_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*-- =============================================
 AUTHOR:		ATUL KUMAR SINGH
 CRAETE DATE: 2011-05-11
 DESCRIPTION:	INSERT POLICY ISSUANCE DATA IN ALL REFERED TABLE
 IN-SCOPE: 
			

  OUT-SCOPE
			

CLIENT ASSUMPTION

  
 INTERNAL ASSUMPTIONS	

PROC_MIG_INSERT_IMPORT_POLICY_COVERAGES]
--DROP MIG_COLOAD_INSERT_COVERAGE_INFO
-- =============================================*/
CREATE PROCEDURE [DBO].[MIG_COLOAD_INSERT_COVERAGE_INFO] 

 @FILE_POLICY_NUMBER NVARCHAR(21),
 @FILE_ENDORSEMENT_NUMBER  NVARCHAR(10),
 @INPUT_REQUEST_ID INT,
 @INPUT_SERIAL_ID INT
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @POLICY_LOB INT
	DECLARE @POLICY_SUB_LOB INT
	DECLARE @ENDORSEMENT_NUMBER_VAR INT
	DECLARE @CUST_ID INT
	DECLARE @POLICY_ID INT
	DECLARE @CREATED_BY INT=2
	
	SET @ENDORSEMENT_NUMBER_VAR=(CONVERT(INT,@FILE_ENDORSEMENT_NUMBER)+1)
	
	--------------------------------- EVALUIATE CUSTOMER ID
	
	SELECT TOP 1			@CUST_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID 
	FROM					POL_CUSTOMER_POLICY_LIST (NOLOCK) 
	WHERE					OLD_POLICY_NUMBER=@FILE_POLICY_NUMBER
	AND						POLICY_VERSION_ID=@ENDORSEMENT_NUMBER_VAR
	
	
	--    MIG_POLICY_COVERAGES
	
IF(@POLICY_LOB=11 or @POLICY_LOB=10 or @POLICY_LOB=12 or @POLICY_LOB=14
	or @POLICY_LOB=16 or @POLICY_LOB=19 or @POLICY_LOB=32 or @POLICY_LOB=25
	or @POLICY_LOB=27
	or @POLICY_LOB=9 -- for named perlis
	or @POLICY_LOB=26  -- for Engineering Risks
	)
BEGIN
		
		
		
		
	
		
		
		
		
	
		
	
		INSERT INTO [POL_LOCATIONS]
			   ([CUSTOMER_ID]
			   ,[POLICY_ID]
			   ,[POLICY_VERSION_ID]
			   ,[LOCATION_ID]
			   --,[LOC_NUM]
			   --,[IS_PRIMARY]
			   --,[LOC_ADD1]
			   --,[LOC_ADD2]
			   --,[LOC_CITY]
			   --,[LOC_COUNTY]
			  -- ,[LOC_STATE]
			  -- ,[LOC_ZIP]
			  -- ,[LOC_COUNTRY]
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
			   --,[LOC_TERRITORY]
			   --,[LOCATION_TYPE]
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
			   )
			SELECT  
						@Cust_ID,
						@POLICY_ID, 
						@ENDORSEMENT_NUMBER_VAR, 
						POLICY_RISK_ID,
						--LOCATION_NUMBER,
						--LOCATION_ADDRESS,
						--LOCATION_COMPLEMENT,
						--LOCATION_CITY,
						--STATE_ID,
						--LOCATION_POSTAL_CODE,
						--LOCATION_COUNTRY,
						'Y',
						@CREATED_BY
						,GETDATE()
						--INSURED_EFFECTIVE_DATE,
						--LOCANTION_NAME,
						--LOCATION_NUMBER,
						--LOCATION_DISTRICT
						
			FROM 		MIG_POLICY_COVERAGES (NOLOCK)A
			WHERE		LEADER_POLICY_NUMBER=@FILE_POLICY_NUMBER
			AND			LEADER_ENDORSEMENT_NUMBER=@FILE_ENDORSEMENT_NUMBER
			AND			HAS_ERRORS='N'
			
			
			
							
			
			
			--IF(@POLICY_LOB=11 or @POLICY_LOB=10 or @POLICY_LOB=12 or @POLICY_LOB=14
			--	or @POLICY_LOB=16 or @POLICY_LOB=19 or @POLICY_LOB=32 or @POLICY_LOB=25
			--	or @POLICY_LOB=27
			--	)
			--BEGIN
			--INSERT INTO [dbo].[POL_PRODUCT_LOCATION_INFO]
			--   ([CUSTOMER_ID]
			--   ,[POLICY_ID]
			--   ,[POLICY_VERSION_ID]
			--   ,[PRODUCT_RISK_ID]
			--   ,[LOCATION]
			--   --,[VALUE_AT_RISK]
			--   --,[BUILDING_VALUE]
			--   --,[CONTENTS_VALUE]
			--   --,[RAW_MATERIAL_VALUE]
			--   --,[CONTENTS_RAW_VALUES]
			--   --,[MRI_VALUE]
			--   --,[MAXIMUM_LIMIT]
			--   --,[POSSIBLE_MAX_LOSS]
			--   --,[MULTIPLE_DEDUCTIBLE]
			--  -- ,[PARKING_SPACES]
			--  -- ,[ACTIVITY_TYPE]
			--  -- ,[OCCUPIED_AS]
			--   ,[CONSTRUCTION]
			--  -- ,[RUBRICA]
			--  -- ,[ASSIST24]
			--  -- ,[REMARKS]
			--   ,[IS_ACTIVE]
			--  ,[CREATED_BY]
			--   ,[CREATED_DATETIME]
			--   --,[MODIFIED_BY]
			--   --,[LAST_UPDATED_DATETIME]
			--   --,[CLAIM_RATIO]
			--   --,[BONUS]
			--   ,[CO_APPLICANT_ID]
			--  -- ,[CLASS_FIELD]
			--   --,[LOCATION_NUMBER]
			--   ,[ITEM_NUMBER]
			--   )
			   
			--   SELECT
			--	@CUST_ID,
			--	@POLICY_ID,
			--	@ENDORSEMENT_NUMBER_VAR,
			--	@PRODUCT_RISK_ID,
			--	@LOCATION_ID,
			--	A.LOCATION_CONSTRUCTION_TYPE,
			--	'Y',
			--	@CREATED_BY,
			--	A.INSURED_EFFECTIVE_DATE,
			--	@PRIMARY_CO_APPLICANT,
			--	A.ITEM
			--	FROM 		MIG_INSURED_OBJECTS (NOLOCK)A
			
			
			--ELSE IF(@POLICY_LOB=9 OR @POLICY_LOB=26)
			--BEGIN
			
		
			
			--		INSERT INTO [dbo].[POL_PERILS]
			--	   ([CUSTOMER_ID]
			--	   ,[POLICY_ID]
			--	   ,[POLICY_VERSION_ID]
			--	   ,[PERIL_ID]
			--	   --,[CALCULATION_NUMBER]
			--	   ,[LOCATION]
			--	   --,[ADDRESS]
			--	   --,[NUMBER]
			--	   --,[COMPLEMENT]
			--	   --,[CITY]
			--	   --,[COUNTRY]
			--	   --,[STATE]
			--	   --,[ZIP]
			--	   --,[TELEPHONE]
			--	   --,[EXTENTION]
			--	   --,[FAX]
			--	   --,[CATEGORY]
			--	   --,[ATIV_CONTROL]
			--	   --,[LOC]
			--	   --,[LOCALIZATION]
			--	   ,[OCCUPANCY]
			--	   --,[CONSTRUCTION]
			--	   --,[LOC_CITY]
			--	   ,[CONSTRUCTION_TYPE]
			--	   ,[ACTIVITY_TYPE]
			--	  -- ,[RISK_TYPE]
			--	   --,[VR]
			--	   --,[LMI]
			--	   --,[BUILDING]
			--	  -- ,[MMU]
			--	  -- ,[MMP]
			--	  -- ,[MRI]
			--	   --,[TYPE]
			--	   --,[LOSS]
			--	   --,[LOYALTY]
			--	   --,[PERC_LOYALTY]
			--	   --,[DEDUCTIBLE_OPTION]
			--	   --,[MULTIPLE_DEDUCTIBLE]
			--	   --,[E_FIRE]
			--	   --,[S_FIXED_FOAM]
			--	   --,[S_FIXED_INSERT_GAS]
			--	   --,[CAR_COMBAT]
			--	   --,[S_DETECT_ALARM]
			--	   --,[S_FIRE_UNIT]
			--	   --,[S_FOAM_PER_MANUAL]
			--	   --,[S_MANUAL_INERT_GAS]
			--	   --,[S_SEMI_HOSES]
			--	   --,[HYDRANTS]
			--	   --,[SHOWERS]
			--	   --,[SHOWER_CLASSIFICATION]
			--	   --,[FIRE_CORPS]
			--	   --,[PUNCTUATION_QUEST]
			--	   --,[DMP]
			--	   --,[EXPLOSION_DEGREE]
			--	   --,[PR_LIQUID]
			--	   --,[COD_ATIV_DRAFTS]
			--	   --,[OCCUPATION_TEXT]
			--	   ,[ASSIST24] --10964 default
			--	   --,[LMRA]
			--	   --,[AGGRAVATION_RCG_AIR]
			--	   --,[EXPLOSION_DESC]
			--	   --,[PROTECTIVE_DESC]
			--	   --,[LMI_DESC]
			--	   --,[LOSS_DESC]
			--	   --,[QUESTIONNAIRE_DESC]
			--	   --,[DEDUCTIBLE_DESC]
			--	   --,[GROUPING_DESC]
			--	   --,[LOC_FLOATING]
			--	   --,[ADJUSTABLE]
			--	   ,[IS_ACTIVE]
			--	   ,[CREATED_BY]
			--	   ,[CREATED_DATETIME]
			--	   --,[MODIFIED_BY]
			--	   --,[LAST_UPDATED_DATETIME]
			--	   --,[CORRAL_SYSTEM]
			--	   ,[RAWVALUES]  -- 'N'
			--	   --,[REMARKS]
			--	   --,[PARKING_SPACES]
			--	   --,[CLAIM_RATIO]
			--	   --,[RAW_MATERIAL_VALUE]
			--	   --,[CONTENT_VALUE]
			--	   --,[BONUS]
			--	   ,[CO_APPLICANT_ID]
			--	   --,[LOCATION_NUMBER]
			--	   ,[ITEM_NUMBER]
			--	   )
			--	   SELECT
				   
			--	   @CUST_ID,
			--	   @POLICY_ID,
			--	   @ENDORSEMENT_NUMBER_VAR,
			--	   @PERIL_ID,
			--	   A.LOCATION_NUMBER,
			--	   A.LOCATION_OCCUPIED_AS,
			--	   A.LOCATION_CONSTRUCTION_TYPE,
			--	   A.LOCATION_ACTIVITY,
			--	   --SUM(C.SUM_INSURED),
			--	   --SUM(C.SUM_INSURED),
			--	   10964,
			--	   'Y',
			--	   @CREATED_BY,
			--	   A.INSURED_EFFECTIVE_DATE,
			--	   'N',
			--	   @CO_APPLICANT_ID,
			--	   A.ITEM
			--	   	FROM 		MIG_INSURED_OBJECTS (NOLOCK)A
			--		JOIN		#INSURED_OBJECTS_LOCATION B
			--		ON			A.ID=B.ID
			--		--LEFT OUTER JOIN MIG_COVERAGES (NOLOCK) C
			--		--ON (A.POLICY_NUMBER=C.POLICY_NUMBER AND A.ENDORESME
			--		--AND A.ITEM=C.ITEM)
			--		WHERE		B.ID_IDENTITY=@INSURED_ONJECT_NUMBER
			--		AND			A.POLICY_NUMBER=@POLICY_NO
			--		AND			A.ENDORESMENT_NUMBER=@ENDORSEMENT_NUMBER_
				   
			--END
			--	SET	@PRODUCT_RISK_ID=(@PRODUCT_RISK_ID+1)
			--	SET @LOCATION_ID=(@LOCATION_ID+1)
			--	SET @INSURED_ONJECT_NUMBER=(@INSURED_ONJECT_NUMBER-1)
			--	SET @PERIL_ID=(@PERIL_ID+1)
				--select @LOCATION_ID,@INSURED_ONJECT_NUMBER
		END
		--SET @PRODUCT_RISK_ID=1
	

		

	
	
	
END

GO

