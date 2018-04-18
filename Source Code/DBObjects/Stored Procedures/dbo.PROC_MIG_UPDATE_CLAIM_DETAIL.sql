IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_UPDATE_CLAIM_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_UPDATE_CLAIM_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                            
Proc Name             : Dbo.PROC_MIG_UPDATE_CLAIM_DETAIL                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 03 June 2011                                                          
Purpose               : TO UPDATE CLAIM DATA(FOR MOVMENT TYPE 2)  
Revison History       :                                                            
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc PROC_MIG_UPDATE_CLAIM_DETAIL       826,'1422081'
                                      
------   ------------       -------------------------*/                                                            
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_UPDATE_CLAIM_DETAIL]      
      
@IMPORT_REQUEST_ID         INT,  
@LEADER_CLAIM_NUMBER       NVARCHAR(10)
                                 
AS                                
BEGIN                         
    
 SET NOCOUNT ON;    
      
  DECLARE @CHK_COUNTER				INT =0  
  DECLARE @TOTAL_RECORD_COUNT		INT =0  
 

 DECLARE  @CLAIM_ID                 INT =0
 DECLARE  @TODAY_DATE				DATETIME=GETDATE()
 DECLARE  @ZIP_CODE				  	NVARCHAR(20)  
 DECLARE  @LOSS_TYPE			  	NVARCHAR(50)  
 DECLARE  @LOSS_LOCATION			NVARCHAR(50) 
 DECLARE  @LOSS_LOCATION_CITY		NVARCHAR(50)  
 DECLARE  @LOSS_LOCATION_STATE		NVARCHAR(10)  
 DECLARE  @DAMAGE_DESC				NVARCHAR(50)  
 
 DECLARE  @LOSS_LOCATION_STATE_ID   INT=0

 DECLARE  @LOSS_ID			  	    INT  
 DECLARE  @MODIFIED_BY			    INT  
 DECLARE  @CLAIM_RISK_ID		    INT     
 DECLARE  @OUTSTANDING_RESERVE		DECIMAL(25,4)
 DECLARE  @SUM_INSURED				DECIMAL(25,4)
 DECLARE  @ACTIVITY_ID				INT =0
 DECLARE  @ALBA_CLAIM_NUMBER		NVARCHAR(20) 

 DECLARE  @OCCURRENCE_ID            INT
 DECLARE  @ACTION_ON_PAYMENT        INT =166  -- CHANGE RESERVE ACTIVITY
 DECLARE  @ACTIVITY_REASON          INT =11773
 DECLARE  @POLICY_ID				INT
 DECLARE  @POLICY_VERSION_ID		INT
 DECLARE  @CUSTOMER_ID				INT
 DECLARE  @LOB_ID					INT
 DECLARE  @SUB_LOB_ID               INT
 DECLARE  @RESERVE_TYPE		        INT   
 


DECLARE @LEADER_COVERAGE_CODE      INT  
DECLARE @COVERAGE_ID			   INT    
DECLARE @CLAIM_COVERAGE_ID	       INT 
DECLARE @RESERVE_ID			       INT  
DECLARE @IMPORT_SERIAL_NO     INT   

			         
 
  -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES

DECLARE @ERROR_NUMBER    INT
DECLARE @ERROR_SEVERITY  INT
DECLARE @ERROR_STATE     INT
DECLARE @ERROR_PROCEDURE VARCHAR(512)
DECLARE @ERROR_LINE  	 INT
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)

   
 BEGIN TRY   
						
			CREATE TABLE #TEMP_CLAIM_DETAILS   
				(      
				  ID                  INT  IDENTITY,      
				  IMPORT_SERIAL_NO    INT
				)   
				
				
			-------------------------------------------------
			-- GET SYSTEM CLAIM DETAILS
			-------------------------------------------------
		   	SELECT  @CLAIM_ID           = C.CLAIM_ID,
			        @CLAIM_RISK_ID      = I.INSURED_PRODUCT_ID ,
			        @ALBA_CLAIM_NUMBER  = CLAIM_NUMBER,
			        @POLICY_ID			= C.POLICY_ID,
			        @POLICY_VERSION_ID	= C.POLICY_VERSION_ID,
			        @CUSTOMER_ID		= C.CUSTOMER_ID,
			        @LOB_ID			    = ISNULL(POLICY_LOB,0),
			        @SUB_LOB_ID			= ISNULL(POLICY_SUBLOB,0)
			FROM    CLM_CLAIM_INFO      C WITH(NOLOCK) INNER JOIN
			        CLM_INSURED_PRODUCT I WITH(NOLOCK) ON C.CLAIM_ID=I.CLAIM_ID INNER JOIN
			        POL_CUSTOMER_POLICY_LIST P WITH(NOLOCK) ON C.CUSTOMER_ID=P.CUSTOMER_ID AND C.POLICY_ID=P.POLICY_ID AND C.POLICY_VERSION_ID=P.POLICY_VERSION_ID
			WHERE   LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER
				
									   
		   ------------------------------------------------
		   -- INSERT ALL CLAIM RECORDS TO UPDATE
		   ------------------------------------------------    
		   INSERT INTO #TEMP_CLAIM_DETAILS      
				 (      
					IMPORT_SERIAL_NO
				 )      
				(           
					SELECT IMPORT_SERIAL_NO
					FROM   MIG_CLAIM_DETAILS WITH(NOLOCK)
					WHERE  IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND 
					       LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER AND 
					       MOVEMENT_TYPE       = 2 AND
					       HAS_ERRORS          = 'N' 
				)  
				
		   IF NOT EXISTS (SELECT IMPORT_SERIAL_NO FROM #TEMP_CLAIM_DETAILS)
		   RETURN 
		   
		
						 
	     SELECT  @TOTAL_RECORD_COUNT= COUNT(ID) FROM #TEMP_CLAIM_DETAILS      
	     
	     ------------------------------------------------
		 -- LOOP TO UPDATE CLAIM DETAILS
		 ------------------------------------------------        
	     WHILE(@CHK_COUNTER<@TOTAL_RECORD_COUNT)      
	     BEGIN      				 
	      
	        SET @CHK_COUNTER=@CHK_COUNTER+1 
	        
	        SELECT @IMPORT_SERIAL_NO = IMPORT_SERIAL_NO 
	        FROM   #TEMP_CLAIM_DETAILS WITH(NOLOCK)
	        WHERE  ID= @CHK_COUNTER
	      
	      
			SELECT 	@MODIFIED_BY			= CREATED_BY,
					@LOSS_TYPE				= LOSS_TYPE,
					@LOSS_LOCATION			= LOSS_LOCATION,
					@LOSS_LOCATION_CITY		= LOSS_LOCATION_CITY,
					@LOSS_LOCATION_STATE	= LOSS_LOCATION_STATE,
					@DAMAGE_DESC			= DAMAGE_DESCRIPTION,
					@LEADER_COVERAGE_CODE	= COVERAGE_CODE,
					@RESERVE_TYPE			= RESERVE_TYPE,
					@OUTSTANDING_RESERVE    = OUTSTANDING_RESERVE,
					@SUM_INSURED		    = SUM_INSURED
			 FROM   MIG_CLAIM_DETAILS	 WITH(NOLOCK)
			 WHERE  IMPORT_REQUEST_ID       = @IMPORT_REQUEST_ID   AND 
			        IMPORT_SERIAL_NO	    = @IMPORT_SERIAL_NO    
           
		
			--------------------------------------------------  
			-- GET LOSS TYPE
			--------------------------------------------------    	
		    SELECT @LOSS_ID=CTD.DETAIL_TYPE_ID  
		    FROM   CLM_TYPE_DETAIL CTD  WITH(NOLOCK) LEFT JOIN    
				   CLM_LOSS_CODES CLC   WITH(NOLOCK) ON  CLC.LOSS_CODE_TYPE = CTD.DETAIL_TYPE_ID --AND CLC.LOB_ID=@LOB_ID       
		    WHERE  DETAIL_TYPE_DESCRIPTION =@LOSS_TYPE AND
				   CTD.TYPE_ID=5  AND CTD.IS_ACTIVE='Y'  --5=Loss types/sub types    
				   
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
		    SELECT TOP 1 @ZIP_CODE		= ISNULL(ZIP_CODE ,0)
		    FROM   MNT_ZIP_CODES WITH(NOLOCK) 
		    WHERE  [STATE]				= @LOSS_LOCATION_STATE
		    
		    SELECT @OCCURRENCE_ID		= OCCURRENCE_DETAIL_ID 
		    FROM   CLM_OCCURRENCE_DETAIL 
		    WHERE  CLAIM_ID				= @CLAIM_ID  
		   
			--------------------------------------------------  
			-- UPDATE OCCURENCE DETAILS		
			-------------------------------------------------- 
			UPDATE  CLM_OCCURRENCE_DETAIL            
			SET          
			MODIFIED_BY					= @MODIFIED_BY,            
			LAST_UPDATED_DATETIME		= @TODAY_DATE,              
			LOSS_TYPE			    	= @LOSS_ID,            
			LOSS_LOCATION		    	= @LOSS_LOCATION, 
			LOSS_LOCATION_ZIP			= @ZIP_CODE,       
			LOSS_LOCATION_CITY			= @LOSS_LOCATION_CITY,
			LOSS_LOCATION_STATE			= @LOSS_LOCATION_STATE_ID
			WHERE CLAIM_ID              = @CLAIM_ID  AND
				  OCCURRENCE_DETAIL_ID  = @OCCURRENCE_ID        
			
			SET @LOSS_TYPE				= NULL
			SET @LOSS_LOCATION			= NULL
			SET @LOSS_LOCATION_CITY		= NULL
			SET @LOSS_LOCATION_STATE	= NULL 
			SET @LOSS_LOCATION_STATE_ID = NULL
			
			--------------------------------------------------  
			-- UPDATE RISK INFORMATION
			-------------------------------------------------- 
			UPDATE CLM_INSURED_PRODUCT
			SET    DAMAGE_DESCRIPTION   = @DAMAGE_DESC
			WHERE  CLAIM_ID		        = @CLAIM_ID 
			
		            
		   --===============================================================
		   -- CREATE NEW CHANGE RESREVE ACTIVITY
		   --=============================================================== 
		   EXEC Proc_InsertCLM_ACTIVITY                          				       
				 @CLAIM_ID              = @CLAIM_ID,                          
				 @ACTIVITY_ID           = @ACTIVITY_ID OUTPUT,                          
				 @ACTIVITY_REASON       = @ACTIVITY_REASON ,                          
				 @REASON_DESCRIPTION    = '',                          
				 @CREATED_BY            = @MODIFIED_BY, 
				 @ACTIVITY_STATUS	    = 11800, -- IMCOMPLETE                 				                  
				 @RESERVE_TRAN_CODE     = 0,   
				 @ACTION_ON_PAYMENT     = @ACTION_ON_PAYMENT, 
				 @COI_TRAN_TYPE         = 14849; -- FOR FULL,   ,  
		  
			------------------------------------      
			-- RESERVE TYPE 
			-- 1 = COVERAGE (INDEMNITY) RESERVE, 
			-- 2 = EXPENSE RESERVE, 
			-- 3 = PROFESSIONAL EXPENSE 
			------------------------------------    
			IF(@RESERVE_TYPE=1)
			 BEGIN
			 
	           SELECT @COVERAGE_ID  = C.COV_ID 
	           FROM   MIG_POLICY_COVERAGE_CODE_MAPPING AS MIG WITH(NOLOCK) INNER JOIN 
					  MNT_COVERAGE AS C WITH(NOLOCK) ON CAST(ISNULL(C.CARRIER_COV_CODE,0) AS INT)=MIG.ALBA_COVERAGE_CODE AND C.LOB_ID=@LOB_ID AND C.SUB_LOB_ID=@SUB_LOB_ID
	           WHERE  LEADER_COVERAGE_CODE = @LEADER_COVERAGE_CODE
	           
	         END
	        ELSE IF(@RESERVE_TYPE=2)
	          SET @COVERAGE_ID=50017  -- CODE OF LOSS EXPENSE
	        ELSE IF(@RESERVE_TYPE=3)
	          SET @COVERAGE_ID=50018  -- CODE OF LOSS EXPENSE FOR PROFESSIONAL SERVICES
	        
	        -----------------------------------------------------------      
			-- GET ACTUAL CLAIM COVERAGE ID (CREATED ON CLAIM SIDE)
			------------------------------------------------------------
	        SELECT @CLAIM_COVERAGE_ID = CLAIM_COV_ID 
	        FROM   CLM_PRODUCT_COVERAGES 
	        WHERE  CLAIM_ID			  = @CLAIM_ID AND 
	               COVERAGE_CODE_ID   = @COVERAGE_ID 
	       
			 
			--------------------------------------      
			---- UPDATE CLAIM COVERAGE DETAILS
			--------------------------------------    
			UPDATE CLM_PRODUCT_COVERAGES 
			SET    LIMIT_1=ISNULL(@SUM_INSURED,0)
			WHERE  CLAIM_ID=@CLAIM_ID AND CLAIM_COV_ID = @COVERAGE_ID
			   
	        ------------------------------------        
			-- GET RESERVE ID
			------------------------------------   
	        SELECT @RESERVE_ID  = RESERVE_ID  
			FROM   CLM_ACTIVITY_RESERVE  
			WHERE  CLAIM_ID     = @CLAIM_ID AND 
			       ACTIVITY_ID  = @ACTIVITY_ID AND
			       COVERAGE_ID  = @CLAIM_COVERAGE_ID  
					       
	         ------------------------------------        
			 -- UPDATE ACTIVITY RESERVE DETAILS  
			 ------------------------------------   
			 EXEC [Proc_UpdateClaimCoveragesReserveDetails]     
			  @RESERVE_ID			 = @RESERVE_ID                  
			 ,@ACTIVITY_ID		     = @ACTIVITY_ID                  
			 ,@RISK_ID				 = @CLAIM_RISK_ID                     
			 ,@CLAIM_ID				 = @CLAIM_ID                
			 ,@COVERAGE_ID			 = @CLAIM_COVERAGE_ID                
			 ,@ACTIVITY_TYPE		 = @ACTIVITY_REASON 
			 ,@ACTION_ON_PAYMENT     = @ACTION_ON_PAYMENT   
			 ,@OUTSTANDING			 = @OUTSTANDING_RESERVE                
			 ,@RI_RESERVE			 = 0                
			 ,@CO_RESERVE			 = 0                
			 ,@PAYMENT_AMOUNT		 = 0      
			 ,@RECOVERY_AMOUNT		 = 0  
			 ,@DEDUCTIBLE_1			 = 0      
			 ,@ADJUSTED_AMOUNT		 = 0      
			 ,@PERSONAL_INJURY		 = 'N'        
			 ,@MODIFIED_BY			 = @MODIFIED_BY             
			 ,@LAST_UPDATED_DATETIME = @TODAY_DATE   

			----------------------------------------------      
			-- CALCULATE BREAKDOWN 
			---------------------------------------------
			EXECUTE Proc_CalculateBreakdown
			@CLAIM_ID    = @CLAIM_ID,              
			@ACTIVITY_ID = @ACTIVITY_ID 		
			  			 
		    ------------------------------------      
		    -- COMPLETE ACTIVITY EXCEPT LAST ONE
		    ------------------------------------   	
		    IF(@CHK_COUNTER<>@TOTAL_RECORD_COUNT)
		    BEGIN
			  EXEC [Proc_CompleteClaimActivities]   
					 @CLAIM_ID            = @CLAIM_ID                      
					,@ACTIVITY_ID         = @ACTIVITY_ID      
					,@ACTIVITY_REASON     = @ACTIVITY_REASON                
					,@ACTION_ON_PAYMENT   = @ACTION_ON_PAYMENT      
					,@COMPETED_DATE       = @TODAY_DATE
					,@COMPETED_BY         = @MODIFIED_BY
					,@LANG_ID			  = 2
					,@IS_ACC_COI_FLG	  = 1
	
            END
            
		   ------------------------------------      
		   -- UPDATE CLAIM DETAILS
		   ------------------------------------   	
			UPDATE MIG_CLAIM_DETAILS         
			SET    ALBA_CLAIM_ID       = @CLAIM_ID,
				   ALBA_CLAIM_NUMBER   = @ALBA_CLAIM_NUMBER ,      
				   POLICY_ID		   = @POLICY_ID,
				   POLICY_VERSION_ID   = @POLICY_VERSION_ID,
				   CUSTOMER_ID		   = @CUSTOMER_ID
			WHERE  IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND
				   IMPORT_SERIAL_NO    = @IMPORT_SERIAL_NO 
		
				      	
	   END -- END OF LOOP
     DROP TABLE #TEMP_CLAIM_DETAILS          
					        
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

