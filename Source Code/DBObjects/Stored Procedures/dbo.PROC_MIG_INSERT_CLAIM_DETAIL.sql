IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_CLAIM_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                            
Proc Name             : Dbo.PROC_MIG_INSERT_CLAIM_DETAIL                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 25 May 2011                                                          
Purpose               : TO CREATE NEW CLAIM  
Revison History       :                                                            
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc PROC_MIG_INSERT_CLAIM_DETAIL       ,'1600028'
                                      
------   ------------       -------------------------*/                                                            
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_DETAIL]      
      
@IMPORT_REQUEST_ID         INT,  
@LEADER_CLAIM_NUMBER       NVARCHAR(10)
                                 
AS                                
BEGIN                         
    
 SET NOCOUNT ON;    
 
 -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES

DECLARE @ERROR_NUMBER    INT
DECLARE @ERROR_SEVERITY  INT
DECLARE @ERROR_STATE     INT
DECLARE @ERROR_PROCEDURE VARCHAR(512)
DECLARE @ERROR_LINE  	 INT
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)

 
 BEGIN TRY
    
 DECLARE @CHK_COUNTER        INT =0  
 
 DECLARE  @CUSTOMER_ID           	INT =0   
 DECLARE  @POLICY_ID             	INT =0    
 DECLARE  @POLICY_VERSION_ID     	INT =0   
 DECLARE  @CLAIM_ID                 INT =0
 DECLARE  @LOB_ID		           	INT    
 DECLARE  @POLICY_SUBLOB        	INT  
 
 DECLARE  @DATE_OF_LOSS           	DATETIME 
 DECLARE  @POICY_EFFECTIVE_DATE   	DATETIME  
 DECLARE  @POICY_EXPIRY_DATE      	DATETIME	 
 
 DECLARE  @TODAY_DATE				DATETIME=GETDATE()
 DECLARE  @CLAIMANT_NAME			NVARCHAR(50)                                             
 DECLARE  @COUNTRY					INT                                              
 DECLARE  @ZIP					    VARCHAR(11)                                             
 DECLARE  @ADDRESS1					VARCHAR(50)                                             
 DECLARE  @ADDRESS2					VARCHAR(50)                                             
 DECLARE  @CITY					    VARCHAR(50)       
 DECLARE  @STATE				    INT    
 DECLARE  @ZIP_CODE				  	NVARCHAR(20)  
 DECLARE  @LOSS_TYPE			  	NVARCHAR(50)  
 DECLARE  @LOSS_LOCATION			NVARCHAR(50) 
 DECLARE  @LOSS_LOCATION_CITY		NVARCHAR(50)  
 DECLARE  @LOSS_LOCATION_STATE		NVARCHAR(10)  
 DECLARE  @DAMAGE_DESC				NVARCHAR(50)  
 
 DECLARE  @LOSS_LOCATION_STATE_ID   INT=0

 DECLARE  @LOSS_ID			  	    VARCHAR(12)  
 DECLARE  @CREATED_BY			    INT  
 DECLARE  @RISK_ID				    INT   
 DECLARE  @CLAIM_RISK_ID		    INT     
 DECLARE  @OUTSTANDING_RESERVE		DECIMAL(25,4)
 DECLARE  @SUM_INSURED				DECIMAL(25,4)
 DECLARE  @ACTIVITY_ID				INT =0
 DECLARE  @ERROR_NO					INT =0  
 DECLARE  @ALBA_CLAIM_NUMBER		NVARCHAR(20) 
 DECLARE  @LEADER_POLICY_NUMBER     NVARCHAR(10)   
 DECLARE  @LEADER_ENDORSEMENT_NUMBER    INT 
 DECLARE  @POLICY_BRANCH_CODE   	NVARCHAR(10)
 --DECLARE  @DIV_ID               	INT
 DECLARE  @ADJUSTER_ID             	INT 


     
		 SET @ERROR_NO=0  
      
	     SELECT TOP 1   
			    @LEADER_POLICY_NUMBER      = LEADER_POLICY_NUMBER  ,       
			    @LEADER_ENDORSEMENT_NUMBER = LEADER_ENDORSEMENT_NUMBER ,  
			    @DATE_OF_LOSS              = CONVERT (date,DATE_OF_LOSS,101),  
			    @CREATED_BY				   = CREATED_BY,
			    @LOSS_TYPE				   = LOSS_TYPE,
			    @LOSS_LOCATION			   = LOSS_LOCATION,
			    @LOSS_LOCATION_CITY		   = LOSS_LOCATION_CITY,
			    @LOSS_LOCATION_STATE	   = LOSS_LOCATION_STATE,
			    @DAMAGE_DESC			   = DAMAGE_DESCRIPTION	,
			    @RISK_ID				   = INSURED_OBJECT_CLAIMED
	     FROM   MIG_CLAIM_DETAILS  WITH(NOLOCK)
	     WHERE  IMPORT_REQUEST_ID		   = @IMPORT_REQUEST_ID   AND
	            LEADER_CLAIM_NUMBER		   = @LEADER_CLAIM_NUMBER AND 
	            MOVEMENT_TYPE			   = 1					  AND
	            HAS_ERRORS				   = 'N'
 			
			 --------------------------------------------------  
			 -- GET POLICY DETAILS		
			 -- IF ENDORSEMENT IS NOT CREATED ON POLICY		 
			 --------------------------------------------------    
			
			 IF(@LEADER_ENDORSEMENT_NUMBER =0)     
			   BEGIN
	    
	    
	             -- FIND POLICY ACTIVE RECORD FOR GIVEN LOSS DATE
				 
				 SELECT @CUSTOMER_ID		  = COI.CUSTOMER_ID, 
						@POLICY_ID			  = COI.POLICY_ID, 
						@POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,
						@POICY_EFFECTIVE_DATE = POL.POLICY_EFFECTIVE_DATE,
						@POICY_EXPIRY_DATE	  = POL.POLICY_EXPIRATION_DATE,
						@LOB_ID			      = ISNULL(POLICY_LOB,0),
						@POLICY_SUBLOB		  = ISNULL(POLICY_SUBLOB,0)
				 FROM   POL_CO_INSURANCE AS COI INNER JOIN
						POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID				               
				 WHERE  COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND POL.POLICY_STATUS IS NOT NULL AND 
						POL.POLICY_STATUS <>'' AND POL.POLICY_STATUS ='NORMAL' AND 
						POL.POLICY_NUMBER IS NOT NULL AND POL.POLICY_NUMBER<>'' AND
						CONVERT(DATE,POL_VER_EFFECTIVE_DATE,101)<=@DATE_OF_LOSS AND @DATE_OF_LOSS<=CONVERT(DATE,POL_VER_EXPIRATION_DATE,101)
						
	    
				-- FIND POLICY ANY RECORD AFTER NBS COMMIT FOR GIVEN LOSS DATE			
				 IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)
				  BEGIN
					 SELECT TOP 1
						@CUSTOMER_ID		  = COI.CUSTOMER_ID, 
						@POLICY_ID			  = COI.POLICY_ID, 
						@POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,
						@POICY_EFFECTIVE_DATE = POL.POLICY_EFFECTIVE_DATE,
						@POICY_EXPIRY_DATE	  = POL.POLICY_EXPIRATION_DATE,
						@LOB_ID			      = ISNULL(POLICY_LOB,0),
						@POLICY_SUBLOB		  = ISNULL(POLICY_SUBLOB,0)
					 FROM   POL_CO_INSURANCE AS COI INNER JOIN
							POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID				               
					 WHERE  COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND POL.POLICY_STATUS IS NOT NULL AND 
							POL.POLICY_STATUS <>'' AND POL.POLICY_STATUS NOT IN('UISSUE','REJECT','Suspended','APPLICATION') AND 
							POL.POLICY_NUMBER IS NOT NULL AND POL.POLICY_NUMBER<>'' AND
							CONVERT(DATE,POL_VER_EFFECTIVE_DATE,101)<=@DATE_OF_LOSS AND @DATE_OF_LOSS<=CONVERT(DATE,POL_VER_EXPIRATION_DATE,101)
				  END	
				 
					-- FIND WHETHER APPLICATION EXISTS OR NOT		 
				 IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID=''  OR @CUSTOMER_ID=0)
				   BEGIN
							
					 SELECT @CUSTOMER_ID		  = COI.CUSTOMER_ID, 
							@POLICY_ID			  = COI.POLICY_ID, 
							@POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,
							@POICY_EFFECTIVE_DATE = POL.POLICY_EFFECTIVE_DATE,
							@POICY_EXPIRY_DATE	  = POL.POLICY_EXPIRATION_DATE,
							@LOB_ID			      = ISNULL(POLICY_LOB,0),
							@POLICY_SUBLOB		  = ISNULL(POLICY_SUBLOB,0)
					 FROM   POL_CO_INSURANCE AS COI INNER JOIN
							POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID				               
					 WHERE  COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND --POL.POLICY_NUMBER<>'' AND POL.POLICY_NUMBER IS NOT NULL AND
							CONVERT(DATE,POL_VER_EFFECTIVE_DATE,101)<=@DATE_OF_LOSS AND @DATE_OF_LOSS<=CONVERT(DATE,POL_VER_EXPIRATION_DATE,101)
					      
					END		
			 
				END
			  ELSE
				BEGIN
			    
			 --------------------------------------------------  
			 -- GET POLICY DETAILS		
			 -- IF ENDORSEMENT HAS CREATED ON POLICY		 
			 --------------------------------------------------      
				SELECT 	@CUSTOMER_ID		  = COI.CUSTOMER_ID, 
					   	@POLICY_ID			  = COI.POLICY_ID, 
					   	@POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,
					   	@POICY_EFFECTIVE_DATE = POL.POLICY_EFFECTIVE_DATE,
					   	@POICY_EXPIRY_DATE	  = POL.POLICY_EXPIRATION_DATE,
					   	@LOB_ID			      = ISNULL(POLICY_LOB,0),
					   	@POLICY_SUBLOB		  = ISNULL(POLICY_SUBLOB,0)
				 FROM   POL_CO_INSURANCE AS COI INNER JOIN
						POL_POLICY_ENDORSEMENTS AS EN ON  COI.CUSTOMER_ID=EN.CUSTOMER_ID AND COI.POLICY_ID=EN.POLICY_ID AND COI.POLICY_VERSION_ID=EN.POLICY_VERSION_ID INNER JOIN
						POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID				               
				 WHERE  COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND POL.POLICY_STATUS IS NOT NULL AND 
						POL.POLICY_STATUS <>'' AND POL.POLICY_STATUS NOT IN('UISSUE','REJECT','Suspended','APPLICATION') AND 
						POL.POLICY_NUMBER IS NOT NULL AND POL.POLICY_NUMBER<>'' AND
						CAST(EN.CO_ENDORSEMENT_NO AS INT) =@LEADER_ENDORSEMENT_NUMBER
			    
				END
			
            --------------------------------------------------  
			-- GET CLAIIMANT DETAILS (INSURED PERSON DETAILS)
			--------------------------------------------------  
            SELECT   @CLAIMANT_NAME = CLT.FIRST_NAME+' '+ISNULL(CLT.MIDDLE_NAME,'')+' '+ISNULL(CLT.LAST_NAME,'') ,
			   	     @ADDRESS1	    = CLT.ADDRESS1,
			   		 @ADDRESS2	    = CLT.ADDRESS2,
			   		 @CITY		    = CLT.CITY,
			   		 @STATE		    = CLT.STATE,
			   		 @COUNTRY	    = CLT.COUNTRY,
			   		 @ZIP_CODE	    = CLT.ZIP_CODE 
			 FROM    POL_APPLICANT_LIST APP WITH(NOLOCK) INNER JOIN
				     CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON APP.APPLICANT_ID=CLT.APPLICANT_ID AND APP.CUSTOMER_ID=CLT.CUSTOMER_ID
			 WHERE   APP.CUSTOMER_ID=@CUSTOMER_ID AND APP.POLICY_ID=@POLICY_ID AND APP.POLICY_VERSION_ID=@POLICY_VERSION_ID  
			 
			 
             
             IF(@COUNTRY IS NULL )
              SET @COUNTRY =5
             IF(@STATE IS NULL )
              SET @STATE =0
               
            --------------------------------------------------  
			-- GET CLAIM ADJUSTER ID
			--------------------------------------------------
			 SELECT @ADJUSTER_ID= A.ADJUSTER_ID FROM CLM_ADJUSTER  A  WITH(NOLOCK) LEFT OUTER JOIN
			        CLM_ADJUSTER_AUTHORITY  B WITH(NOLOCK) ON A.ADJUSTER_ID=B.ADJUSTER_ID
			 WHERE ADJUSTER_CODE='58' AND B.LOB_ID=@LOB_ID AND A.IS_ACTIVE='Y'
		       
            --------------------------------------------------  
			-- INSERT CLAIM NOTFICATION DETAILS		
			--------------------------------------------------          
			EXEC [Proc_InsertCLM_CLAIM_INFO]                                              
				 @CUSTOMER_ID			 = @CUSTOMER_ID,                                              
				 @POLICY_ID				 = @POLICY_ID,                                              
				 @POLICY_VERSION_ID		 = @POLICY_VERSION_ID,                                              
				 @CLAIM_ID				 = @CLAIM_ID OUT,                                              
				 @LOSS_DATE				 = @DATE_OF_LOSS,                                              
				 @ADJUSTER_CODE			 = '58',                    
				 @ADJUSTER_ID			 = @ADJUSTER_ID,                          
				 @REPORTED_BY			 = NULL,                                              
				 @CATASTROPHE_EVENT_CODE = 0,                                              
				 --@CLAIMANT_INSURED bit,                                              
				 @INSURED_RELATIONSHIP   = NULL,                                              
				 @CLAIMANT_NAME          = @CLAIMANT_NAME,                                              
				 @COUNTRY				 = @COUNTRY,                                              
				 @ZIP					 = @ZIP_CODE,            
				 @ADDRESS1				 = @ADDRESS1,                                              
				 @ADDRESS2				 = @ADDRESS2,              
				 @CITY					 = @CITY,                                              
				 @HOME_PHONE			 = NULL,                                              
				 @WORK_PHONE			 = NULL,                                              
				 @MOBILE_PHONE			 = NULL,                                              
				 @WHERE_CONTACT			 = '',                                              
				 @WHEN_CONTACT           = '',                                              
				 @DIARY_DATE			 = @TODAY_DATE,                                            
				 @CLAIM_STATUS			 = 11739,  -- OPEN                                            
				 @OUTSTANDING_RESERVE    = 0,               
				 @RESINSURANCE_RESERVE   = 0,                                              
				 @PAID_LOSS              = 0,                                              
				 @PAID_EXPENSE			 = 0,                                             
				 @RECOVERIES			 = 0,                                         
				 @CLAIM_DESCRIPTION		 = '',                                              
				 @CREATED_BY			 = @CREATED_BY,                                              
				 @CREATED_DATETIME		 = @TODAY_DATE,                                              
				 --@SUB_ADJUSTER varchar(50),                                              
				 --@SUB_ADJUSTER_CONTACT varchar(50),                                   
				 @EXTENSION				 = '',                                              
				 @LOSS_TIME_AM_PM		 = 0,              
				 @LITIGATION_FILE		 = 10964,  --NO                                            
				 @HOMEOWNER				 = 0,                                              
				 @RECR_VEH				 = 0,                                          
				 @IN_MARINE				 = 0,                                                
				 @STATE					 = @STATE,                                              
				 @CLAIMANT_PARTY		 = 14133,   
				 @CLAIMANT_TYPE			 = 0,                                            
				 --@LINKED_TO_CLAIM varchar(500),                                              
				 --@ADD_FAULT char(1),                                              
				 --@TOTAL_LOSS char(1),                                              
				 @NOTIFY_REINSURER		 = 0,                
				 @LOB_ID				 = @LOB_ID,                                        
				 @REPORTED_TO			 = NULL,                                            
				 @FIRST_NOTICE_OF_LOSS   = @DATE_OF_LOSS,       
				 @LAST_DOC_RECEIVE_DATE  = NULL,                    
				 @LINKED_CLAIM_ID_LIST   = NULL,                              
				 @RECIEVE_PINK_SLIP_USERS_LIST =NULL,                            
				 @NEW_RECIEVE_PINK_SLIP_USERS_LIST =NULL,                          
				 @PINK_SLIP_TYPE_LIST	 = NULL,            
				 @CLAIM_STATUS_UNDER	 = NULL,      
				 @AT_FAULT_INDICATOR	 = 10964 , --NO
				 @REINSURANCE_TYPE		 = NULL ,    
				 @REIN_CLAIM_NUMBER		 = NULL,   
				 @REIN_LOSS_NOTICE_NUM	 = NULL,  
				 @IS_VICTIM_CLAIM		 = 10964, --NO 
				 @POSSIBLE_PAYMENT_DATE  = NULL     
			
		    SET @CLAIMANT_NAME			 = NULL
			SET @COUNTRY				 = NULL			
			SET @ZIP					 = NULL	
			SET @ADDRESS1				 = NULL	
			SET @ADDRESS2				 = NULL	
			SET @CITY					 = NULL	
			SET @STATE					 = NULL
			SET @ZIP_CODE				 = NULL	
				
			--------------------------------------------------  
			-- UPDATE CLAIM RECORD 
			--------------------------------------------------    					
			UPDATE CLM_CLAIM_INFO 
			SET LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER ,
			    ACC_COI_FLG         = 'Y'
			WHERE CLAIM_ID			= @CLAIM_ID
			
			--------------------------------------------------  
			-- GET LOSS TYPE
			--------------------------------------------------  
			SELECT TOP 1 @LOSS_ID= EBIX_LOSS_ID
			FROM   MIG_CLAIM_LOSS_TYPE_MAPPING M  WITH(NOLOCK)  
			WHERE ((ALBA_LOSS_TYPE=@LOSS_TYPE) OR (LEADER_LOSS_DESC1=@LOSS_TYPE)OR (LEADER_LOSS_DESC2=@LOSS_TYPE) OR (LEADER_LOSS_DESC3=@LOSS_TYPE))
			
		    --SELECT @LOSS_ID=CTD.DETAIL_TYPE_ID  
		    --FROM   CLM_TYPE_DETAIL CTD  WITH(NOLOCK) LEFT JOIN    
				  -- CLM_LOSS_CODES CLC WITH(NOLOCK) ON  CLC.LOSS_CODE_TYPE = CTD.DETAIL_TYPE_ID AND CLC.LOB_ID=@LOB_ID       
		    --WHERE  DETAIL_TYPE_DESCRIPTION =@LOSS_TYPE AND
				  -- CTD.TYPE_ID=5  AND CTD.IS_ACTIVE='Y'  --5=Loss types/sub types    
				   
			--------------------------------------------------  
			-- GET STATE ID FOR GIVEN STATE CODE
			--------------------------------------------------    	   
		    SELECT TOP 1 @LOSS_LOCATION_STATE_ID=ISNULL(STATE_ID ,0)
		    FROM   MNT_COUNTRY_STATE_LIST WITH(NOLOCK) 
		    WHERE  COUNTRY_ID=5 AND   -- COUNTRY ID = 5 MEANS FOR BRAZIL COUNTRY
		           STATE_CODE =@LOSS_LOCATION_STATE
		           
		           
		    --------------------------------------------------  
			-- GET ZIP CODE FOR GIVEN STATE CODE
			--------------------------------------------------    	   
		    SELECT TOP 1 @ZIP_CODE=ISNULL(ZIP_CODE ,0)
		    FROM   MNT_ZIP_CODES WITH(NOLOCK) 
		    WHERE [STATE] =@LOSS_LOCATION_STATE
		           
		    SET @LOSS_ID= CAST(@LOSS_ID AS VARCHAR(10))+','
			--------------------------------------------------  
			-- INSERT OCCURENCE DETAILS		
			--------------------------------------------------    	 
			EXEC [Proc_InsertCLM_OCCURRENCE_DETAIL]                           
				 @OCCURRENCE_DETAIL_ID       = 0,
				 @CLAIM_ID					 = @CLAIM_ID,              
				 @LOSS_DESCRIPTION      	 = NULL,              
				 @AUTHORITY_CONTACTED   	 = NULL,              
				 @REPORT_NUMBER         	 = NULL,              
				 @VIOLATIONS            	 = NULL ,              
				 @CREATED_BY            	 = @CREATED_BY ,              
				 @LOSS_TYPE					 = @LOSS_ID,              
				 @LOSS_LOCATION				 = @LOSS_LOCATION,       
				 @LOSS_LOCATION_ZIP     	 = @ZIP_CODE,   
				 @LOSS_LOCATION_CITY    	 = @LOSS_LOCATION_CITY,    
				 @LOSS_LOCATION_STATE   	 = @LOSS_LOCATION_STATE_ID,
				 @ESTIMATE_AMOUNT			 = 0,        
				 @OTHER_DESCRIPTION     	 = @DAMAGE_DESC,    
				 @WATERBACKUP_SUMPPUMP_LOSS  = NULL,   
				 @WEATHER_RELATED_LOSS       = NULL 
				
			SET @LOSS_TYPE				= NULL
			SET @LOSS_LOCATION			= NULL
			SET @LOSS_LOCATION_CITY		= NULL
			SET @LOSS_LOCATION_STATE	= NULL 
			SET @LOSS_LOCATION_STATE_ID = NULL
			
			--------------------------------------------------------
			-- VARIABLE USED TO HOLD RISK RELATED DATA
			--------------------------------------------------------
			DECLARE 
					 @INSURED_PRODUCT_ID     int              
					,@CREATED_DATETIME       datetime                       
					,@IS_ACTIVE              CHAR(1) 
					,@DAMAGE_DESCRIPTION     nvarchar(150)                           
				    ,@POL_RISK_ID			int   
					,@YEAR 					smallint   
					,@VEHICLE_MAKER          nvarchar(150)                     
					,@VEHICLE_MODEL          nvarchar(150)                     
					,@VEHICLE_VIN            nvarchar(150)  
					,@VESSEL_TYPE            nvarchar(70)                      
					,@VESSEL_NAME            nvarchar(70)                   
					,@VESSEL_MANUFACTURER    nvarchar(50)                   
					,@LOCATION_ADDRESS       nvarchar(150)                  
					,@LOCATION_COMPLIMENT    nvarchar(75)                   
					,@LOCATION_DISTRICT      nvarchar(75)                   
					,@LOCATION_ZIPCODE       nvarchar(11)   
					,@CITY1                  nvarchar(250)
					,@STATE1                 nvarchar(150)                    
					,@COUNTRY1               nvarchar(150)     
					,@CITY2                  nvarchar(250)  
					,@STATE2                 nvarchar(150)                  
					,@COUNTRY2               nvarchar(150)                 
					,@VOYAGE_CONVEYENCE_TYPE nvarchar(150)              
					,@VOYAGE_DEPARTURE_DATE  datetime    
					,@INSURED_NAME			 nvarchar(150)                 
					,@EFFECTIVE_DATE		 datetime                        
					,@EXPIRE_DATE			 datetime   					                 
					,@LICENCE_PLATE_NUMBER   nvarchar(50)  
					,@PERSON_DOB			 datetime	 
					,@VESSEL_NUMBER			 nvarchar(50) 
					,@VOYAGE_ARRIVAL_DATE	 datetime	 
					,@VOYAGE_SURVEY_DATE	 datetime	 
					,@ITEM_NUMBER			 int			 
					,@RURAL_INSURED_AREA	 int			  
					,@RURAL_PROPERTY		 int			 
					,@RURAL_CULTIVATION		 int			   
					,@RURAL_FESR_COVERAGE	 int			   
					,@RURAL_MODE			 int			   
					,@RURAL_SUBSIDY_PREMIUM	 decimal(18,2)  
					,@PA_NUM_OF_PASS		 numeric(18,0)  
					,@DP_TICKET_NUMBER		 int			       
					,@DP_CATEGORY			 int			   
					,@ACTUAL_INSURED_OBJECT	 nvarchar(250)  
					,@RISK_CO_APP_ID	     int
														   
							
				--===============================================================
				-- GET RISK INFORMATION 
				--===============================================================
				
				 ---------------------------------------------------------  
				 -- 9  : All Risks and Named Perils     
				 -- 26 : Engeneering Risks   
				 ---------------------------------------------------------         
				 IF(@LOB_ID IN (9,26))  
				 BEGIN        
				          
				  SELECT
						@LOCATION_ADDRESS        = L.LOC_ADD1 + ' - ' +  ISNULL(L.NUMBER,'') ,          
						@LOCATION_COMPLIMENT     = L.LOC_ADD2,    
						@LOCATION_DISTRICT       = L.DISTRICT ,    
						@LOCATION_ZIPCODE 		 = L.LOC_ZIP  ,    
						@STATE1 				 = L.LOC_STATE  ,    
						@COUNTRY1   			 = L.LOC_COUNTRY  ,  
						@ITEM_NUMBER			 = P.ITEM_NUMBER ,  
						@ACTUAL_INSURED_OBJECT   = P.ACTUAL_INSURED_OBJECT  ,
						@RISK_CO_APP_ID          = P.CO_APPLICANT_ID                     
				  FROM    POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
						  POL_PERILS    P WITH(NOLOCK) ON  P.LOCATION>0 AND P.LOCATION=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID             
				  WHERE ( P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
				  END           
				   
				 ---------------------------------------------------------  
				 -- 10 : Comprehensive Condominium  
				 -- 11 : DWELLING  
				 -- 12 : GENERAL CIVIL LIABILITY  
				 -- 14 : Diversified Risks  
				 -- 16 : RObbery  
				 -- 19 : Comprehensive Company    
				 -- 25 : Traditional Fire  
				 -- 27 : Global of Bank  
				 -- 32 : Judicial Guarantee  
				 ---------------------------------------------------------            
				ELSE IF(@LOB_ID IN (10,11,12,14,16,19,25,27,32))    
				 BEGIN        
				         
				  SELECT 
						@LOCATION_ADDRESS        = L.LOC_ADD1 + ' - ' +  ISNULL(L.NUMBER,'')  ,          
						@LOCATION_COMPLIMENT     = L.LOC_ADD2    ,    
						@LOCATION_DISTRICT       = L.DISTRICT,    
						@LOCATION_ZIPCODE 		 = L.LOC_ZIP  ,    
						@STATE1 				 = L.LOC_STATE    ,    
						@COUNTRY1   			 = L.LOC_COUNTRY ,  
						@ITEM_NUMBER			 = P.ITEM_NUMBER ,  
						@ACTUAL_INSURED_OBJECT   = P.ACTUAL_INSURED_OBJECT,
						@RISK_CO_APP_ID          = P.CO_APPLICANT_ID               
				  FROM  POL_LOCATIONS L WITH(NOLOCK) INNER JOIN          
						POL_PRODUCT_LOCATION_INFO  P WITH(NOLOCK)  ON  P.LOCATION>0 AND P.LOCATION=L.LOCATION_ID AND L.CUSTOMER_ID=P.CUSTOMER_ID AND L.POLICY_ID=P.POLICY_ID AND L.POLICY_VERSION_ID=P.POLICY_VERSION_ID             
				  WHERE (P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
				 END             
				               
				 ---------------------------------------------------------                         
				  -- 13 : MARITIME  
				 ---------------------------------------------------------                    
				ELSE IF(@LOB_ID=13)      
				 BEGIN        
				          
					SELECT 
							@VESSEL_NAME         = M.NAME_OF_VESSEL,    
							@VESSEL_TYPE         = M.TYPE_OF_VESSEL,    
							@YEAR				 = M.MANUFACTURE_YEAR,    
							@VESSEL_MANUFACTURER = M.MANUFACTURER ,  
							@VESSEL_NUMBER       = VESSEL_NUMBER ,
						    @RISK_CO_APP_ID      = M.CO_APPLICANT_ID        
					FROM POL_MARITIME M WITH(NOLOCK)   
					WHERE  ( M.MARITIME_ID=@RISK_ID AND M.CUSTOMER_ID=@CUSTOMER_ID AND M.POLICY_ID=@POLICY_ID AND M.POLICY_VERSION_ID= @POLICY_VERSION_ID AND M.IS_ACTIVE='Y')             
				              
				  END             
   
				 ---------------------------------------------------------  
				 -- 21 : Group Personal Accident for Passenger   
				 -- 34 : Group Life   
				 -- 15 : Individual Personal Accident  
				 -- 33 : Mortgage  
				 ---------------------------------------------------------            
				ELSE IF(@LOB_ID IN (21,34,15,33))   
				 BEGIN        
				            
				  SELECT         
						@INSURED_NAME   = ISNULL(P.INDIVIDUAL_NAME,'')  ,  
						@PERSON_DOB		= DATE_OF_BIRTH,  
						@EFFECTIVE_DATE = ISNULL(PR.EFFECTIVE_DATETIME,CP.POLICY_EFFECTIVE_DATE) ,  
						@EXPIRE_DATE 	= ISNULL(PR.[EXPIRY_DATE],CP.POLICY_EXPIRATION_DATE) ,
						@RISK_CO_APP_ID = P.APPLICANT_ID         
				  FROM   POL_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)  
						 INNER JOIN  POL_POLICY_PROCESS PR WITH(NOLOCK) ON PR.CUSTOMER_ID=P.CUSTOMER_ID AND PR.POLICY_ID=P.POLICY_ID AND PR.NEW_POLICY_VERSION_ID=P.POLICY_VERSION_ID AND  PR.PROCESS_STATUS='COMPLETE' -- AND PR.PROCESS_ID=14  
						 LEFT OUTER JOIN  POL_CUSTOMER_POLICY_LIST CP WITH(NOLOCK) ON CP.CUSTOMER_ID=PR.CUSTOMER_ID AND CP.POLICY_ID=PR.POLICY_ID AND CP.POLICY_VERSION_ID=PR.POLICY_VERSION_ID --AND  CP.PROCESS_STATUS='COMPLETE'  -- AND PR.PROCESS_ID=14  
				  WHERE ( P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
				    
				  END             
				 ---------------------------------------------------------  
				 -- 17 : Facultative Liability   (THIS IS MASTER POLICY)      
				 -- 18 : Civil Liability Transportation   (THIS IS MASTER POLICY)      
				 -- 28 : Aeronautic  
				 -- 29 : Motor  
				 -- 31 : Cargo Transportation Civil Liability  
				 ---------------------------------------------------------                  
				ELSE IF(@LOB_ID IN(17,18,28,29,31))  
				 BEGIN        
				     
				  SELECT 
					@VEHICLE_VIN			= CHASSIS  ,       
					@YEAR					= MANUFACTURED_YEAR ,    
					@VEHICLE_MAKER			= VEHICLE_MAKE ,    
					@VEHICLE_MODEL			= MAKE_MODEL   ,  
					@LICENCE_PLATE_NUMBER	= LICENSE_PLATE      ,  
					@EFFECTIVE_DATE			= P.RISK_EFFECTIVE_DATE,  
					@EXPIRE_DATE			= P.RISK_EXPIRE_DATE,
					@RISK_CO_APP_ID         = P.CO_APPLICANT_ID             
				  FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK)    
				  WHERE (  CO_RISK_ID=@RISK_ID AND  P.CUSTOMER_ID=@CUSTOMER_ID AND  P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND  P.IS_ACTIVE='Y')                  
				  END           
          
          
				---------------------------------------------------------   
				  --  20 : National Cargo Transport  
				  --  23 : International Cargo Transport  
				 ---------------------------------------------------------                     
				ELSE IF(@LOB_ID IN (20,23))   
				   BEGIN        
				          
				  SELECT       
    						@VOYAGE_DEPARTURE_DATE   = P.DEPARTING_DATE,   
    						@VOYAGE_ARRIVAL_DATE     = P.ARRIVAL_DATE,   
							@VOYAGE_CONVEYENCE_TYPE  = P.CONVEYANCE_TYPE,    
							@CITY2					= P.DESTINATION_CITY,    
	 						@COUNTRY2				= P.DEST_COUNTRY ,    
							@STATE2					= P.DEST_STATE ,    
							@CITY1					= P.ORIGIN_CITY,    
	 						@COUNTRY1				= P.ORIGN_COUNTRY ,    
							@STATE1					= P.ORIGN_STATE,
					        @RISK_CO_APP_ID         = P.CO_APPLICANT_ID                      
				  FROM    POL_COMMODITY_INFO  P WITH(NOLOCK) 
				  WHERE  ( CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
				 END      
   
				  ---------------------------------------------------------   
				  -- 30 : Dpvat(Cat. 3 e 4)  
				  -- 36 : DPVAT(Cat.1,2,9 e 10)  
				  ---------------------------------------------------------        
				 ELSE IF(@LOB_ID IN (30,36))      
				  BEGIN        
				     
				   SELECT 
						  @DP_TICKET_NUMBER = P.TICKET_NUMBER ,  
						  @STATE1           = P.STATE_ID   ,    
						  @DP_CATEGORY      = P.CATEGORY   ,
						  @RISK_CO_APP_ID   = P.CO_APPLICANT_ID          
				   FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK) 
				   WHERE  ( P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
				  END     
				 ---------------------------------------------------------   
				  -- 22 : Personal Accident for Passengers   
				 ---------------------------------------------------------      
				  ELSE IF(@LOB_ID =22)      
				   BEGIN        
				     
				   SELECT     
						  @EFFECTIVE_DATE =	P.[START_DATE],          
						  @EXPIRE_DATE	  =	P.END_DATE    ,            
						  @PA_NUM_OF_PASS =	P.NUMBER_OF_PASSENGERS ,
					      @RISK_CO_APP_ID = P.CO_APPLICANT_ID               
				   FROM   POL_PASSENGERS_PERSONAL_ACCIDENT_INFO  P WITH(NOLOCK) 
				   WHERE  ( P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
				     
				   END  
     
				---------------------------------------------------------   
				  -- 35 : Rural Lien  
				---------------------------------------------------------       
				  ELSE IF(@LOB_ID = 35)  
				   BEGIN        
				            
				  SELECT  
					@ITEM_NUMBER		     = P.ITEM_NUMBER    ,                
					@STATE1				     = P.STATE_ID       ,  
					@CITY1				     = P.CITY  ,  
					@RURAL_INSURED_AREA      = P.INSURED_AREA ,  
					@RURAL_PROPERTY          = P.PROPERTY  ,  
					@RURAL_CULTIVATION       = P.CULTIVATION ,  
					@RURAL_FESR_COVERAGE	 = P.FESR_COVERAGE ,  
					@RURAL_MODE			     = P.MODE   ,  
					@RURAL_SUBSIDY_PREMIUM   = P.SUBSIDY_PREMIUM ,  
					@STATE2				     = P.SUBSIDY_STATE   ,
					@RISK_CO_APP_ID          = P.CO_APPLICANT_ID        
				   FROM    POL_PENHOR_RURAL_INFO  P WITH(NOLOCK) 
				   WHERE  ( P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')     
				    
				  END         
    
				 ---------------------------------------------------------   
				  -- 37 : Rental Security  
				 ---------------------------------------------------------       
				  ELSE IF(@LOB_ID IN (37))  
				   BEGIN        
				            
				  SELECT 
						 @ITEM_NUMBER		    = P.ITEM_NUMBER     ,  
						 @ACTUAL_INSURED_OBJECT = P.REMARKS      ,
					     @RISK_CO_APP_ID        = P.CO_APPLICANT_ID           
				   FROM   POL_PENHOR_RURAL_INFO  P WITH(NOLOCK) 
				   WHERE ( P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                  
				     
				  END   
						
						SELECT @CLAIM_ID		
			  --===============================================================
			  -- INSERT RISK INFORMATION 
			  --===============================================================
		         EXEC   [Proc_InsertRiskInformation]
						@CUSTOMER_ID			        = @CUSTOMER_ID,                      
						@POLICY_ID						= @POLICY_ID ,                      
						@POLICY_VERSION_ID				= @POLICY_VERSION_ID,  
						@INSURED_PRODUCT_ID				= @CLAIM_RISK_ID  OUTPUT  ,          
						@CLAIM_ID						= @CLAIM_ID,          
						@CREATED_BY						= @CREATED_BY                   ,
						@CREATED_DATETIME				= @TODAY_DATE  ,            
						@IS_ACTIVE						= 'Y',   
						@DAMAGE_DESCRIPTION				= @DAMAGE_DESC  ,
						@POL_RISK_ID					= @RISK_ID ,
						@YEAR							= @YEAR ,             
						@VEHICLE_INSURED_PLEADED_GUILTY = NULL,
						@VEHICLE_MAKER                  = @VEHICLE_MAKER ,          
						@VEHICLE_MODEL					= @VEHICLE_MODEL  ,       
						@VEHICLE_VIN					= @VEHICLE_VIN     ,      
						@VESSEL_TYPE					= @VESSEL_TYPE      ,     
						@VESSEL_NAME					= @VESSEL_NAME       ,    
						@VESSEL_MANUFACTURER			= @VESSEL_MANUFACTURER,   
						@LOCATION_ADDRESS				= @LOCATION_ADDRESS    ,  
						@LOCATION_COMPLIMENT			= @LOCATION_COMPLIMENT  , 
						@LOCATION_DISTRICT				= @LOCATION_DISTRICT     ,
						@LOCATION_ZIPCODE				= @LOCATION_ZIPCODE      ,
						@CITY1							= @CITY1                 ,
						@STATE1							= @STATE1                ,
						@COUNTRY1						= @COUNTRY1              ,
						@CITY2							= @CITY2                 ,
						@STATE2							= @STATE2                ,
						@COUNTRY2						= @COUNTRY2              ,
						@VOYAGE_CONVEYENCE_TYPE			= @VOYAGE_CONVEYENCE_TYPE,
						@VOYAGE_DEPARTURE_DATE			= @VOYAGE_DEPARTURE_DATE ,
						@INSURED_NAME                   = @INSURED_NAME,
						@EFFECTIVE_DATE                 = @EFFECTIVE_DATE,
						@EXPIRE_DATE					= @EXPIRE_DATE,
						@LICENCE_PLATE_NUMBER           = @LICENCE_PLATE_NUMBER,
						@DAMAGE_TYPE					= NULL,
						@PERSON_DOB						= @PERSON_DOB,
						@PERSON_DiSEASE_DATE			= NULL,
						@VOYAGE_CERT_NUMBER				= NULL,
						@VOYAGE_PREFIX					= NULL,
						@VESSEL_NUMBER					= @VESSEL_NUMBER,
						@VOYAGE_TRAN_COMPANY            = NULL,
						@VOYAGE_IO_DESC					= NULL ,   
						@VOYAGE_ARRIVAL_DATE			= @VOYAGE_ARRIVAL_DATE  ,  
						@VOYAGE_SURVEY_DATE				= @VOYAGE_SURVEY_DATE,
						@ITEM_NUMBER					= @ITEM_NUMBER,
						@RURAL_INSURED_AREA				= @RURAL_INSURED_AREA    ,
						@RURAL_PROPERTY					= @RURAL_PROPERTY     ,     
						@RURAL_CULTIVATION				= @RURAL_CULTIVATION   ,    
						@RURAL_FESR_COVERAGE			= @RURAL_FESR_COVERAGE  ,   
						@RURAL_MODE						= @RURAL_MODE       ,
						@RURAL_SUBSIDY_PREMIUM			= @RURAL_SUBSIDY_PREMIUM,
						@PA_NUM_OF_PASS					= @PA_NUM_OF_PASS		,	   
						@DP_TICKET_NUMBER				= @DP_TICKET_NUMBER      ,
						@DP_CATEGORY					= @DP_CATEGORY       ,
						@ACTUAL_INSURED_OBJECT			= @ACTUAL_INSURED_OBJECT   ,
						@RISK_CO_APP_ID	   			    = @RISK_CO_APP_ID  
  
		  
						SET @DAMAGE_DESC           		= NULL   
						SET @YEAR                  		= NULL  
						SET @VEHICLE_MAKER         		= NULL 
						SET @VEHICLE_MODEL		   		= NULL 
						SET @VEHICLE_VIN		   		= NULL 
						SET @VESSEL_TYPE		   		= NULL 
						SET @VESSEL_NAME           		= NULL 
						SET @VESSEL_MANUFACTURER   		= NULL 
						SET @LOCATION_ADDRESS      		= NULL 
						SET @LOCATION_COMPLIMENT   		= NULL 
						SET @LOCATION_DISTRICT     		= NULL 
						SET @LOCATION_ZIPCODE      		= NULL 
						SET @CITY1                 		= NULL 
						SET @STATE1                		= NULL 
						SET @COUNTRY1              		= NULL 
						SET @CITY2                 		= NULL 
						SET @STATE2                		= NULL 
						SET @COUNTRY2              		= NULL 
						SET @VOYAGE_CONVEYENCE_TYPE		= NULL 
						SET @VOYAGE_DEPARTURE_DATE 		= NULL 
						SET @INSURED_NAME		   		= NULL 
						SET @EFFECTIVE_DATE		   		= NULL 
						SET @EXPIRE_DATE		   		= NULL 
						SET @LICENCE_PLATE_NUMBER  		= NULL 
						SET @PERSON_DOB            		= NULL 
						SET @VESSEL_NUMBER         		= NULL 
						SET @VOYAGE_ARRIVAL_DATE   		= NULL  
						SET @VOYAGE_SURVEY_DATE	   		= NULL 
						SET @ITEM_NUMBER		   		= NULL 
						SET @RURAL_INSURED_AREA    		= NULL 
						SET @RURAL_PROPERTY        		= NULL   
						SET @RURAL_CULTIVATION     		= NULL   
						SET @RURAL_FESR_COVERAGE   		= NULL   
						SET @RURAL_MODE			   		= NULL 
						SET @RURAL_SUBSIDY_PREMIUM 		= NULL 
						SET @PA_NUM_OF_PASS		   		= NULL 	   
						SET @DP_TICKET_NUMBER      		= NULL 
						SET @DP_CATEGORY		   		= NULL 
						SET @ACTUAL_INSURED_OBJECT 		= NULL   
						
		   ---------------------------------------------------------   
		   -- UPDATE COINSURANCE INFORMATION
		   ---------------------------------------------------------   
		 				
		   IF EXISTS(SELECT  COINSURANCE_ID FROM CLM_CO_INSURANCE WHERE CLAIM_ID=@CLAIM_ID )
		     BEGIN
		     
		       UPDATE CLM_CO_INSURANCE
		       SET    LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER
		       WHERE  CLAIM_ID            = @CLAIM_ID 
		       
		     END
		       
		  
		  IF(@CLAIM_RISK_ID>0)
		    BEGIN 
		            
				   --===============================================================
				   -- CREATE NEW RESREVE ACTIVITY
				   --=============================================================== 
				   EXEC Proc_InsertCLM_ACTIVITY                          				       
						 @CLAIM_ID            = @CLAIM_ID,                          
						 @ACTIVITY_ID         = @ACTIVITY_ID OUTPUT,         
						 @ACTIVITY_REASON     = 11836 ,                          
						 @REASON_DESCRIPTION  = '',                          
						 @CREATED_BY          = @CREATED_BY,                  				                  
						 @RESERVE_TRAN_CODE   = 0,   
						 @ACTION_ON_PAYMENT   = 165,      
						 @COI_TRAN_TYPE       = 14849; -- FOR FULL,   ,  
		   
		     
					   CREATE TABLE #TEMP_COVERAGES      
						(      
						  ID                  INT  IDENTITY,      
						  IMPORT_SERIAL_NO    INT,      
						  COVERAGE_CODE       INT,
						  CLAIM_COVERAGE_ID   INT,
						  OUTSTANDING_RESERVE DECIMAL(25,4),
						  SUM_INSURED         DECIMAL(25,4),
						  RESERVE_TYPE        INT      
						)   
						   
				          
				       INSERT INTO #TEMP_COVERAGES      
						(      
							IMPORT_SERIAL_NO,      
							COVERAGE_CODE   ,
							OUTSTANDING_RESERVE,
							SUM_INSURED,
							RESERVE_TYPE   
						)      
						(           
							SELECT IMPORT_SERIAL_NO,COVERAGE_CODE,OUTSTANDING_RESERVE,SUM_INSURED,RESERVE_TYPE
							FROM   MIG_CLAIM_DETAILS WITH(NOLOCK) 
							WHERE  IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID   AND
							       LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER AND 
							       MOVEMENT_TYPE       = 1					  AND 
							       HAS_ERRORS		   = 'N' 
						)  
						 
					   ------------------------------------      
					   -- LOOP FOR VALIDATE COVERAGE CODE      
					   ------------------------------------ 
					   DECLARE @LOOP_COVERAGE_CODE	  INT  
					   DECLARE @COVERAGE_ID			  INT    
					   DECLARE @CLAIM_COVERAGE_ID	  INT 
					   DECLARE @RESERVE_ID			 INT  
					   DECLARE @TRANSACTION_ID		  INT  
					   DECLARE @LOOP_IMPORT_SERIAL_NO INT   
					   DECLARE @RESERVE_TYPE		  INT   
					         
					
					      
					   SELECT  @CHK_COUNTER= COUNT(ID) FROM #TEMP_COVERAGES      
					         
					   WHILE(@CHK_COUNTER>0)      
					   BEGIN      
					              
							SELECT @LOOP_COVERAGE_CODE    = COVERAGE_CODE ,
							       @LOOP_IMPORT_SERIAL_NO = IMPORT_SERIAL_NO ,
							       @RESERVE_TYPE          = RESERVE_TYPE  ,
							       @OUTSTANDING_RESERVE   = OUTSTANDING_RESERVE,
							       @SUM_INSURED			  = SUM_INSURED   
							FROM   #TEMP_COVERAGES      
							WHERE ID=@CHK_COUNTER      
					        
							------------------------------------      
							-- RESERVE TYPE (1 = COVERAGE (INDEMNITY) RESERVE, 2 = EXPENSE RESERVE, 3 = PROFESSIONAL EXPENSE  )
							------------------------------------    
					        SET @ERROR_NO =0
							IF(@RESERVE_TYPE=1)
							 BEGIN
							 
					           SELECT @COVERAGE_ID=C.COV_ID 
					           FROM   MIG_POLICY_COVERAGE_CODE_MAPPING AS MIG INNER JOIN 
									  MNT_COVERAGE AS C ON CAST(ISNULL(C.CARRIER_COV_CODE,0) AS INT)=MIG.ALBA_COVERAGE_CODE
					           WHERE  LEADER_COVERAGE_CODE=@LOOP_COVERAGE_CODE  AND C.LOB_ID=@LOB_ID AND C.SUB_LOB_ID=@POLICY_SUBLOB
					           
					         END
					        ELSE IF(@RESERVE_TYPE=2)
					          SET @COVERAGE_ID=50017  -- FOR Loss Expense
					        ELSE IF(@RESERVE_TYPE=3)
					          SET @COVERAGE_ID=50018  -- FOR Loss Expense for Professional Services
					        
					        -----------------------------------------------------------      
							-- GET ACTUAL CLAIM COVERAGE ID (CREATED ON CLAIM SIDE)
							------------------------------------------------------------
					        SELECT @CLAIM_COVERAGE_ID=CLAIM_COV_ID FROM CLM_PRODUCT_COVERAGES WHERE COVERAGE_CODE_ID=@COVERAGE_ID AND CLAIM_ID=@CLAIM_ID
					            
					        IF(@CLAIM_COVERAGE_ID IS NULL OR @CLAIM_COVERAGE_ID<1)      
							  BEGIN      
							       
							       --------------------------------------      
								   -- COVERAGE DOES NOT EXISTS IN POLICY
								   --------------------------------------     
								   SET @ERROR_NO=45        
							               
								   UPDATE MIG_CLAIM_DETAILS      
								   SET    HAS_ERRORS='Y'      
								   WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND IMPORT_SERIAL_NO =@LOOP_IMPORT_SERIAL_NO      
							     
								   EXEC  PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                   
										 @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,      
										 @IMPORT_SERIAL_NO      = @LOOP_IMPORT_SERIAL_NO  ,          
										 @ERROR_SOURCE_FILE     = ''     ,      
										 @ERROR_SOURCE_COLUMN   = ''     ,      
										 @ERROR_SOURCE_COLUMN_VALUE= '' ,      
										 @ERROR_ROW_NUMBER      = @LOOP_IMPORT_SERIAL_NO   ,        
										 @ERROR_TYPES           = @ERROR_NO,          
										 @ACTUAL_RECORD_DATA    = '' ,      
										 @ERROR_MODE            = 'VE',  -- VALIDATION ERROR        
										 @ERROR_SOURCE_TYPE     = 'CLM'   
							
							   END   
							 ELSE
							 BEGIN
							 
									-----------------------------------------------------------------------------
									-- UPDATE CLAIM COVERAGE DETAILS
									-- UPDATE POLICY LIMIT OF ALL COVERAGES HAVING COVERAGE CODE= @COVERAGE_ID
									-----------------------------------------------------------------------------    
							        UPDATE CLM_PRODUCT_COVERAGES 
							        SET    LIMIT_1=ISNULL(@SUM_INSURED,0)
							        WHERE  CLAIM_ID=@CLAIM_ID AND COVERAGE_CODE_ID=@COVERAGE_ID
							       
							        ------------------------------------      
									-- UPDATE CLAIM COVERAGE ID IN TEMP TABLE
									------------------------------------ 
									UPDATE #TEMP_COVERAGES 
									SET    CLAIM_COVERAGE_ID=@CLAIM_COVERAGE_ID 
									WHERE  ID=@CHK_COUNTER
							   
							 END    
							              
						  SET @CHK_COUNTER=@CHK_COUNTER-1 
						      
					   END -- END OF LOOP
					   
                       IF (SELECT COUNT(CLAIM_COVERAGE_ID) FROM #TEMP_COVERAGES WHERE ISNULL(CLAIM_COVERAGE_ID,0)>0) >0  
                       BEGIN 
                       
                            SET @RESERVE_ID     = 0
                            SET @TRANSACTION_ID = 0
                        	       
							------------------------------------      
							-- INSERT ACTIVITY RESERVE DETAILS
							------------------------------------    
							INSERT INTO [dbo].[CLM_ACTIVITY_RESERVE]            
								   (            
									[CLAIM_ID]                   
								   ,[RESERVE_ID]          
								   ,[ACTUAL_RISK_ID]            
								   ,[ACTIVITY_ID]            
								   ,[COVERAGE_ID]                       
								   ,[OUTSTANDING]             
								   ,[PREV_OUTSTANDING]                        
								   ,[OUTSTANDING_TRAN]               
								   ,[TRANSACTION_ID]        
								   ,[IS_ACTIVE]            
								   ,[CREATED_BY]            
								   ,[CREATED_DATETIME]   
								   ,[DEDUCTIBLE_1]     
								   ,[ADJUSTED_AMOUNT]    
								   ,PERSONAL_INJURY     
								   ,VICTIM_ID  
						                      
								  )            
								   (   
								   SELECT          
									@CLAIM_ID 
								   ,@RESERVE_ID + row_number() OVER(ORDER BY C.CLAIM_COV_ID asc)         
								   ,@CLAIM_RISK_ID     
								   ,@ACTIVITY_ID            
								   ,C.CLAIM_COV_ID                    
								   ,ISNULL(T.OUTSTANDING_RESERVE,0)       
								   ,0            --[PREV_OUTSTANDING]                 
								   ,ISNULL(T.OUTSTANDING_RESERVE,0)   --[OUTSTANDING_TRAN]              
								   ,@TRANSACTION_ID + row_number() OVER(ORDER BY C.CLAIM_COV_ID asc)                   
								   ,'Y'            
								   ,@CREATED_BY            
								   ,@TODAY_DATE   
								   ,0  
								   ,0   
								   ,'N'     
								   ,NULL  
								   FROM CLM_PRODUCT_COVERAGES AS C WITH(NOLOCK) LEFT OUTER JOIN
										#TEMP_COVERAGES AS T ON   T.CLAIM_COVERAGE_ID=C.CLAIM_COV_ID AND T.CLAIM_COVERAGE_ID IS NOT NULL
								   WHERE C.CLAIM_ID=@CLAIM_ID
								   )           
								
							
							----------------------------------------------      
							-- CALCULATE BREAKDOWN 
							---------------------------------------------
							EXECUTE Proc_CalculateBreakdown
							@CLAIM_ID    = @CLAIM_ID,              
							@ACTIVITY_ID = @ACTIVITY_ID 		
							------------------------------------      
							-- COMPLETE ACTIVITY
							------------------------------------   	
							EXEC [Proc_CompleteClaimActivities]   
								 @CLAIM_ID            = @CLAIM_ID                      
								,@ACTIVITY_ID         = @ACTIVITY_ID      
								,@ACTIVITY_REASON     = 11836                
								,@ACTION_ON_PAYMENT   = 165      
								,@COMPETED_DATE       = @TODAY_DATE
								,@COMPETED_BY         = @CREATED_BY
								,@LANG_ID			  = 2
								,@IS_ACC_COI_FLG	  = 1
										
					   END	
					   
					   DROP TABLE #TEMP_COVERAGES     
					   
				
					   ------------------------------------      
					   -- UPDATE CLAIM DETAILS
					   ------------------------------------   	
					  IF(@CLAIM_ID>0)
					   BEGIN
							SELECT @ALBA_CLAIM_NUMBER =CLAIM_NUMBER 						
							FROM   CLM_CLAIM_INFO WITH(NOLOCK)
							WHERE  CLAIM_ID = @CLAIM_ID
							
							UPDATE MIG_CLAIM_DETAILS         
							SET    ALBA_CLAIM_ID       = @CLAIM_ID,
							       ALBA_CLAIM_NUMBER   = @ALBA_CLAIM_NUMBER ,      
								   POLICY_ID		   = @POLICY_ID,
								   POLICY_VERSION_ID   = @POLICY_VERSION_ID,
								   CUSTOMER_ID		   = @CUSTOMER_ID
							WHERE  IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND
							       LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER AND 
							       MOVEMENT_TYPE	   = 1 AND 
							       HAS_ERRORS		   = 'N'
						END        
						
					        
			END	 
					   
	END TRY
	BEGIN CATCH
	
	SELECT 
			 @ERROR_NUMBER    = ERROR_NUMBER(),
			 @ERROR_SEVERITY  = ERROR_SEVERITY(),
			 @ERROR_STATE     = ERROR_STATE(),
			 @ERROR_PROCEDURE = ERROR_PROCEDURE(),
			 @ERROR_LINE	  = ERROR_LINE(),
			 @ERROR_MESSAGE   = ERROR_MESSAGE()
     
  -- CREATING LOG OF EXCEPTION 
  EXEC [PROC_MIG_INSERT_ERROR_LOG]  
  @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID
 ,@IMPORT_SERIAL_NO		= 0
 ,@ERROR_NUMBER    		= @ERROR_NUMBER
 ,@ERROR_SEVERITY  		= @ERROR_SEVERITY
 ,@ERROR_STATE     	    = @ERROR_STATE
 ,@ERROR_PROCEDURE 		= @ERROR_PROCEDURE
 ,@ERROR_LINE  	   		= @ERROR_LINE
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE
  
	
     
	
	END CATCH
		  
  
  
            
END 







GO

