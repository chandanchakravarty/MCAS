IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_CLAIM_PAYMENT_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_PAYMENT_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                            
Proc Name             : Dbo.PROC_MIG_INSERT_CLAIM_PAYMENT_DETAIL                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 26 May 2011                                                          
Purpose               : TO INSERT CLAIM PAYMENT DETAILS(CREATE PAYMENT ACTIVITY FOR EACH RECORD)  
Revison History       :                                                            
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc PROC_MIG_INSERT_CLAIM_PAYMENT_DETAIL 384,'1000569'                                                   
------   ------------       -------------------------*/                                                            
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_CLAIM_PAYMENT_DETAIL]      
      
@IMPORT_REQUEST_ID		    INT,
@LEADER_CLAIM_NUMBER  	    NVARCHAR(10)

AS                                
BEGIN                         
    
 SET NOCOUNT ON;    
      
 DECLARE @CHK_COUNTER               INT =0  
 
 DECLARE  @CUSTOMER_ID           	INT =0   
 DECLARE  @POLICY_ID             	INT =0    
 DECLARE  @POLICY_VERSION_ID     	INT =0   
 DECLARE  @CLAIM_ID                 INT =0

 DECLARE  @TODAY_DATE				DATETIME=GETDATE()

 DECLARE  @LOSS_LOCATION_STATE_ID   INT=0

 DECLARE  @LOSS_ID			  	    INT 
 DECLARE  @LOB_ID			  	    INT  
 DECLARE  @SUB_LOB_ID			  	INT  

 DECLARE  @CREATED_BY			    INT  
 DECLARE  @CLAIM_RISK_ID		    INT    
 --DECLARE  @PAYMENT_AMOUNT   		DECIMAL(25,4)
 --DECLARE  @TOTAL_PAYMENT_AMOUNT	DECIMAL(25,4)
 DECLARE  @ACTIVITY_ID				INT =0
 DECLARE  @ERRORS_NO				INT =0
 DECLARE  @ALBA_CLAIM_NUMBER        NVARCHAR(20)
 DECLARE  @ACTIVITY_STATUS          INT
 DECLARE  @OUSTANDING_RESERVE		DECIMAL(25,4)
 DECLARE  @ACTIVITY_TYPE            INT
 DECLARE  @PARTY_ID                 INT
   
 -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES  
DECLARE @ERROR_NUMBER    INT  
DECLARE @ERROR_SEVERITY  INT  
DECLARE @ERROR_STATE     INT  
DECLARE @ERROR_PROCEDURE VARCHAR(512)  
DECLARE @ERROR_LINE    INT  
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)  
  
   
 BEGIN TRY  

     
		 SET @ERRORS_NO=0  
      
	     SELECT TOP 1
			    @CREATED_BY		       = CREATED_BY
	     FROM   MIG_PAID_CLAIM_DETAILS
	     WHERE  LEADER_CLAIM_NUMBER	   = @LEADER_CLAIM_NUMBER  AND 
	            IMPORT_REQUEST_ID	   = @IMPORT_REQUEST_ID
	     
	     --------------------------------------------------    
		 -- GET CLAIM RISK ID
		 -------------------------------------------------- 		
	     SELECT @CLAIM_RISK_ID 	   = I.INSURED_PRODUCT_ID,
	            @LOB_ID		   	   = C.LOB_ID,
	            @SUB_LOB_ID        = ISNULL(P.POLICY_SUBLOB,0),
	            @CLAIM_ID      	   = C.CLAIM_ID,
	            @CUSTOMER_ID   	   = C.CUSTOMER_ID,
	            @POLICY_ID     	   = C.POLICY_ID,
	            @POLICY_VERSION_ID = C.POLICY_VERSION_ID,
	            @ALBA_CLAIM_NUMBER = CLAIM_NUMBER
	     FROM   CLM_CLAIM_INFO AS C INNER JOIN
	            CLM_INSURED_PRODUCT I ON C.CLAIM_ID=I.CLAIM_ID LEFT OUTER JOIN 
	            POL_CUSTOMER_POLICY_LIST P ON C.CUSTOMER_ID=P.CUSTOMER_ID AND C.POLICY_ID=P.POLICY_ID AND C.POLICY_VERSION_ID=P.POLICY_VERSION_ID
	     WHERE  C.LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER
		   
	     --------------------------------------------------    
         -- CHECK CLAIM NUMBER
         -- ERROR TYPE =19 (CLAIM DO NOT EXISTS)    
         --------------------------------------------------             
         IF (@CLAIM_ID IS NULL OR @CLAIM_ID<1)
          BEGIN
			SET @ERRORS_NO=19 
           END 
         ELSE
         BEGIN
			 --------------------------------------------------    
			 -- IF INCOMPLETE ACTVITY EXISTS
			 -- ERROR TYPE =28 
			 --------------------------------------------------      
			 SELECT TOP 1 
			   @ACTIVITY_STATUS    = ACTIVITY_STATUS, 
			   @ACTIVITY_TYPE      = ACTION_ON_PAYMENT,
			   @OUSTANDING_RESERVE = CLAIM_RESERVE_AMOUNT
			 FROM CLM_ACTIVITY 
			 WHERE CLAIM_ID=@CLAIM_ID  AND IS_ACTIVE='Y'
			 ORDER BY ACTIVITY_ID DESC
			 
			 IF(@ACTIVITY_STATUS=11800)
			  SET @ERRORS_NO=28  
			 --------------------------------------------------    
			 -- IF LAST ACTIVITY IS CLOSE RESERVE ACTIVITY
			 -- ERROR TYPE =31 
			 --------------------------------------------------      
			 ELSE IF (@ACTIVITY_TYPE=167) 
			    SET @ERRORS_NO=31  
			 -----------------------------------------------------------------    
			 -- IF RESERVE AMOUNT IS ZERO THEN PAYMENT ACTIVITY IS NOT ALLOWED
			 -- ERROR TYPE =33 
			 -----------------------------------------------------------------
			 ELSE IF (@OUSTANDING_RESERVE<1)
                SET @ERRORS_NO=33  
         END
         
       
        IF(@ERRORS_NO>0) 
        BEGIN
             --------------------------------------------------    
			 -- INSERT ERROR 
			 --------------------------------------------------    
			 INSERT INTO [MIG_IMPORT_ERROR_DETAILS]        
					 (    
						[IMPORT_REQUEST_ID]             
						,[IMPORT_SERIAL_NO] 
						,[ERROR_DATETIME]                
						,[ERROR_TYPES]                      
						,[ERROR_MODE]      
						,ERROR_SOURCE_TYPE       
					 )                 
					 (      
						SELECT                          
						 @IMPORT_REQUEST_ID             
						,IMPORT_SERIAL_NO      
						,@TODAY_DATE                    
						,@ERRORS_NO    
						,'VE'         
						,'CLMP'     
						FROM  MIG_PAID_CLAIM_DETAILS
						WHERE IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID AND    
							  LEADER_CLAIM_NUMBER   = @LEADER_CLAIM_NUMBER  AND
							  HAS_ERRORS			= 'N'
					)  
					
			 UPDATE MIG_PAID_CLAIM_DETAILS SET HAS_ERRORS='Y'
			 WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND
			        LEADER_CLAIM_NUMBER=@LEADER_CLAIM_NUMBER AND 
			        HAS_ERRORS='N'
			 
        END
        ELSE
         BEGIN   
         
          	 
					   CREATE TABLE #TEMP_COVERAGES      
						(      
						  ID INT  IDENTITY,      
						  IMPORT_SERIAL_NO 		INT,      
						  COVERAGE_CODE    		INT,
						  PAYMENT_TYPE     		INT,    --PAY TYPE, MEANING 1 = FINAL PAYMENT FOR INDEMNITY, 2 = PARTIAL PAYMENT FOR INDEMNITY, 3 = FINAL PAYMENT   
						  PAYMENT_AMOUNT   		DECIMAL(25,4),
						  BENEFICIARY_NAME     NVARCHAR(50)     
						)      
				          
				       INSERT INTO #TEMP_COVERAGES      
						(      
							IMPORT_SERIAL_NO,      
							COVERAGE_CODE   ,
							PAYMENT_TYPE ,
							PAYMENT_AMOUNT,
							BENEFICIARY_NAME
						)      
						(           
							SELECT IMPORT_SERIAL_NO,COVERAGE_CODE,PAYMENT_TYPE, PAYMENT_AMOUNT,BENEFICIARY_NAME
							FROM   MIG_PAID_CLAIM_DETAILS 
							WHERE  LEADER_CLAIM_NUMBER =@LEADER_CLAIM_NUMBER  AND 
							       IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID  AND
							       HAS_ERRORS='N' 
						)  
					
				
					   ------------------------------------      
					   -- LOOP FOR VALIDATE COVERAGE CODE      
					   ------------------------------------ 
					   DECLARE @LOOP_COVERAGE_CODE	  INT  
					   DECLARE @COVERAGE_ID			  INT    
					   DECLARE @CLAIM_COVERAGE_ID	  INT  
					   DECLARE @RESERVE_ID			  INT  
					   DECLARE @LOOP_PAYMENT_AMOUNT   DECIMAL(25,4)
					   DECLARE @LOOP_IMPORT_SERIAL_NO INT   
					   DECLARE @PAYMENT_TYPE		  INT   
					   DECLARE @ACTION_ON_PAYMENT	  INT   
					   DECLARE @LOOP_BENEFICIARY_NAME NVARCHAR(50)      
					   DECLARE @COUNTER				  INT   =1
					      
					   SELECT @CHK_COUNTER= COUNT(ID) FROM #TEMP_COVERAGES  
					    
					  
					   WHILE(@COUNTER<=@CHK_COUNTER)      
					   BEGIN      
					        
					  		SET @CLAIM_COVERAGE_ID=0;
					  			              
							SELECT @LOOP_COVERAGE_CODE    = COVERAGE_CODE ,
							       @LOOP_IMPORT_SERIAL_NO = IMPORT_SERIAL_NO ,
							       @PAYMENT_TYPE          = PAYMENT_TYPE   ,
							       @LOOP_PAYMENT_AMOUNT   = PAYMENT_AMOUNT,
							       @LOOP_BENEFICIARY_NAME = BENEFICIARY_NAME
							FROM  #TEMP_COVERAGES      
							WHERE ID=@COUNTER  
							
						    SET @COUNTER=@COUNTER+1  
						   
						   
							  
							SELECT @COVERAGE_ID=C.COV_ID 
					        FROM   MIG_POLICY_COVERAGE_CODE_MAPPING AS MIG INNER JOIN 
								   MNT_COVERAGE AS C ON CAST(ISNULL(C.CARRIER_COV_CODE,0) AS INT)=MIG.ALBA_COVERAGE_CODE
			  		        WHERE  LEADER_COVERAGE_CODE=@LOOP_COVERAGE_CODE AND C.LOB_ID=@LOB_ID AND SUB_LOB_ID=@SUB_LOB_ID
							
							
					        ----------------------------------------------
						    --  PAYMENT TYPES :- 
						    --  1 = FINAL PAYMENT FOR INDEMNITY COVERAGE, 
						    --  2 = PARTIAL PAYMENT FOR INDEMNITY COVERAGE, 
						    --  3 = FINAL PAYMENT FOR EXPENSE COVERAGE (ANY EXPENSE)   
						    --  4 = PARTIAL PAYMENT FOR EXPENSE COVERAGE (ANY EXPENSE)   
						    ----------------------------------------------
						    IF(@PAYMENT_TYPE=1)
						        SET @ACTION_ON_PAYMENT=181    -- CODE FOR PAYMENT - FULL
						    ELSE IF (@PAYMENT_TYPE=2)
						        SET @ACTION_ON_PAYMENT=180    -- CODE FOR PAYMENT - PARTIAL
						    ELSE IF (@PAYMENT_TYPE=3)  
						     BEGIN
						        SET @ACTION_ON_PAYMENT=181    -- CODE FOR PAYMENT - FULL
						        SET @COVERAGE_ID=50017		  -- COVERAGE CODE FOR LOSS EXPENSE 
						     END
						    ELSE
						     BEGIN
						        SET @ACTION_ON_PAYMENT=180    -- CODE FOR PAYMENT - PARTIAL
						        SET @COVERAGE_ID=50017		  -- COVERAGE CODE FOR LOSS EXPENSE 
						     END
						      
						    
							------------------------------------------------------------      
							-- GET ACTUAL CLAIM COVERAGE ID (CREATED ON CLAIM SIDE)
							------------------------------------------------------------
							SELECT @CLAIM_COVERAGE_ID=CLAIM_COV_ID 
							FROM   CLM_PRODUCT_COVERAGES 
							WHERE  COVERAGE_CODE_ID=@COVERAGE_ID AND CLAIM_ID=@CLAIM_ID
							
							---------------------------------------------------      
							-- AN IMCOMPLETE ACTIVITY IS EXISTS
							---------------------------------------------------    
							IF EXISTS (SELECT CLAIM_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_STATUS =11800)
						       SET @ERRORS_NO=28  
							---------------------------------------------------      
							-- PAYMENT AMOUNT SHOULD NOT BE ZERO
							---------------------------------------------------     
							ELSE IF (@LOOP_PAYMENT_AMOUNT=0)
							  SET @ERRORS_NO=38  
							---------------------------------------------------      
						    -- COVERAGE IS NOT FOUND IN CLAIM 
						    ---------------------------------------------------    
					        ELSE IF(@CLAIM_COVERAGE_ID IS NULL OR @CLAIM_COVERAGE_ID<1)   
					          SET @ERRORS_NO=21  
					        -----------------------------------------------------------------    
							-- IF RESERVE AMOUNT IS ZERO THEN PAYMENT ACTIVITY IS NOT ALLOWED
							-----------------------------------------------------------------
					        ELSE IF(SELECT TOP 1 CLAIM_RESERVE_AMOUNT FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID  AND IS_ACTIVE='Y' ORDER BY ACTIVITY_ID DESC)<1
							  SET @ERRORS_NO=33       
					          
					        IF(@ERRORS_NO>0)
							 BEGIN      
							        
							               
								   UPDATE MIG_PAID_CLAIM_DETAILS      
								   SET    HAS_ERRORS='Y'      
								   WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND IMPORT_SERIAL_NO =@LOOP_IMPORT_SERIAL_NO      
							                      
								   EXEC  PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                   
										 @IMPORT_REQUEST_ID     	= @IMPORT_REQUEST_ID,      
										 @IMPORT_SERIAL_NO      	= @LOOP_IMPORT_SERIAL_NO  ,          
										 @ERROR_SOURCE_FILE     	= ''     ,      
										 @ERROR_SOURCE_COLUMN   	= ''     ,      
										 @ERROR_SOURCE_COLUMN_VALUE = '' ,  
										 @ERROR_ROW_NUMBER      	= @LOOP_IMPORT_SERIAL_NO   ,        
										 @ERROR_TYPES           	= @ERRORS_NO,          
										 @ACTUAL_RECORD_DATA    	= '' ,      
										 @ERROR_MODE            	= 'VE',  -- VALIDATION ERROR        
										 @ERROR_SOURCE_TYPE     	= 'CLMP'   
							
							  END
							  ELSE
							  BEGIN  
							  
							    SELECT TOP 1 @ACTIVITY_ID=ACTIVITY_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID  AND IS_ACTIVE='Y' ORDER BY ACTIVITY_ID DESC
							  	
							  	-------------------------------------------------------------------      
								-- RESERVE AMOUNT FOR INDEMINITY COVERAGE IS LESS THAN PAYMENT AMOUNT
								------------------------------------------------------------------
							  	IF(@PAYMENT_TYPE IN (1,2)) AND NOT EXISTS( SELECT TOP 1 A.CLAIM_COV_ID FROM  CLM_ACTIVITY_RESERVE R WITH(NOLOCK) INNER JOIN 
						    											   CLM_PRODUCT_COVERAGES A  WITH(NOLOCK) ON  A.CLAIM_ID=R.CLAIM_ID AND A.CLAIM_COV_ID=R.COVERAGE_ID
						    											   WHERE R.CLAIM_ID=@CLAIM_ID AND R.ACTIVITY_ID=@ACTIVITY_ID AND R.OUTSTANDING>=@LOOP_PAYMENT_AMOUNT AND
						    												A.CLAIM_COV_ID =@CLAIM_COVERAGE_ID 
						    											  )
						    	BEGIN
						    	  SET @ERRORS_NO=37 
						    	END
						    	ELSE 
						    	BEGIN
										SELECT TOP 1 @CLAIM_COVERAGE_ID= A.CLAIM_COV_ID 
										FROM  CLM_ACTIVITY_RESERVE R WITH(NOLOCK) INNER JOIN 
										      CLM_PRODUCT_COVERAGES A  WITH(NOLOCK) ON  A.CLAIM_ID=R.CLAIM_ID AND A.CLAIM_COV_ID=R.COVERAGE_ID
										WHERE R.CLAIM_ID=@CLAIM_ID AND R.ACTIVITY_ID=@ACTIVITY_ID AND R.OUTSTANDING>=@LOOP_PAYMENT_AMOUNT AND
											  A.IS_RISK_COVERAGE='N'
											  
									    -------------------------------------------------------------------      
										-- RESERVE AMOUNT FOR EXPENSE COVERAGE IS LESS THAN PAYMENT AMOUNT
										-------------------------------------------------------------------
						    			IF(@CLAIM_COVERAGE_ID IS NULL OR @CLAIM_COVERAGE_ID=0)
						    			 SET @ERRORS_NO=36  
											
								END
						     
						      IF(@ERRORS_NO >0)
						      BEGIN
						           
						    	      
							           UPDATE MIG_PAID_CLAIM_DETAILS      
									   SET    HAS_ERRORS='Y'      
									   WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND IMPORT_SERIAL_NO =@LOOP_IMPORT_SERIAL_NO      
								                      
									   EXEC  PROC_MIG_INSERT_IMPORT_ERROR_DETAILS                   
											 @IMPORT_REQUEST_ID     	= @IMPORT_REQUEST_ID,      
											 @IMPORT_SERIAL_NO      	= @LOOP_IMPORT_SERIAL_NO  ,          
											 @ERROR_SOURCE_FILE     	= ''     ,      
											 @ERROR_SOURCE_COLUMN   	= ''     ,      
											 @ERROR_SOURCE_COLUMN_VALUE = '' ,      
											 @ERROR_ROW_NUMBER      	= @LOOP_IMPORT_SERIAL_NO   ,        
											 @ERROR_TYPES           	= @ERRORS_NO,          
											 @ACTUAL_RECORD_DATA    	= '' ,      
											 @ERROR_MODE            	= 'VE',  -- VALIDATION ERROR        
											 @ERROR_SOURCE_TYPE     	= 'CLMP'   
						    	  
						      END
						      ELSE
						      BEGIN
						     
							   SET @LOOP_BENEFICIARY_NAME = LTRIM(RTRIM(@LOOP_BENEFICIARY_NAME))
					        
					          ---------------------------------------------------------------------------
							  -- GET BENEFICIARY INFO
							  ---------------------------------------------------------------------------
							  SELECT @PARTY_ID FROM CLM_PARTIES WHERE CLAIM_ID=@CLAIM_ID AND LTRIM(RTRIM(NAME))=@LOOP_BENEFICIARY_NAME
							    
							  ------------------------------------------------------------------------------------
							  -- ADD NEW CLAIM PARTY
							  ------------------------------------------------------------------------------------
							  IF (@PARTY_ID IS NULL OR @PARTY_ID=0)  
							   BEGIN
								    
								      SELECT @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                         
									  FROM   CLM_PARTIES                           
									  WHERE  CLAIM_ID=@CLAIM_ID       
									  
								     INSERT INTO CLM_PARTIES                            
										 (                           
										  PARTY_ID,                            
										  CLAIM_ID,                            
										  NAME,       
										  ADDRESS1,                     
										  STATE, 
										  COUNTRY,  
										  ZIP,                 
										  CREATED_BY,                            
										  CREATED_DATETIME,                        
										  PARTY_TYPE_ID,           
										  IS_ACTIVE, 
										  PARTY_TYPE,
										  PARTY_CPF_CNPJ
										          
										 )                                        
										 VALUES                                        
										 (                                        
										  @PARTY_ID,                            
										  @CLAIM_ID,                  
										  @LOOP_BENEFICIARY_NAME,  
										  'CO Aceito',
										  71,
										  5,  
										  01001000,                        
										  @CREATED_BY,
										  @TODAY_DATE,
										  621, -- OTHER PARTY TYPE
										  'Y',
										  11110, --PARTY TYPE=PERSONAL
										  ''  
										 )             
								       
								    END
								  --===============================================================
								  -- CREATE NEW PAYMENT ACTIVITY
								  --=============================================================== 
								  EXEC Proc_InsertCLM_ACTIVITY                          				       
										@CLAIM_ID           		= @CLAIM_ID,                          
										@ACTIVITY_ID        		= @ACTIVITY_ID OUTPUT,                          
										@ACTIVITY_REASON    		= 11775 ,                          
										@REASON_DESCRIPTION 		= '',                          
										@CREATED_BY         		= @CREATED_BY,                  				                  
										@RESERVE_TRAN_CODE  		= 0,   
										@ACTION_ON_PAYMENT  		= @ACTION_ON_PAYMENT,      
										@COI_TRAN_TYPE      		= 14849, -- FOR FULL, 
										@TEXT_ID				    = 5,
										@TEXT_DESCRIPTION           = 'CO Aceito'
									 
								IF(@ACTIVITY_ID>0)
								BEGIN	 
								    
								    ------------------------------------      
									-- INSERT ACTIVITY RESERVE DETAILS
									------------------------------------    
								    SELECT @RESERVE_ID  = RESERVE_ID
								    FROM   CLM_ACTIVITY_RESERVE
								    WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND COVERAGE_ID=@CLAIM_COVERAGE_ID
								  
							   		------------------------------------      
									-- UPDATE ACTIVITY RESERVE DETAILS
									------------------------------------ 
									EXEC [Proc_UpdateClaimCoveragesReserveDetails]   
									 @RESERVE_ID			= @RESERVE_ID		              
									,@ACTIVITY_ID			= @ACTIVITY_ID                
									,@RISK_ID				= @CLAIM_RISK_ID                   
									,@CLAIM_ID				= @CLAIM_ID              
									,@COVERAGE_ID			= @CLAIM_COVERAGE_ID              
									,@ACTIVITY_TYPE			= 11775 -- ACTIVITY REASON FOR PAYMENT ACTIVITY 
									,@ACTION_ON_PAYMENT     = @ACTION_ON_PAYMENT 
									,@OUTSTANDING			= 0              
									,@RI_RESERVE			= 0              
									,@CO_RESERVE			= 0              
									,@PAYMENT_AMOUNT		= @LOOP_PAYMENT_AMOUNT    
									,@RECOVERY_AMOUNT		= 0
									,@DEDUCTIBLE_1			= 0    
									,@ADJUSTED_AMOUNT		= @LOOP_PAYMENT_AMOUNT    
									,@PERSONAL_INJURY		= 'N'      
									,@MODIFIED_BY			= @CREATED_BY           
									,@LAST_UPDATED_DATETIME = @TODAY_DATE 
									
									----------------------------------------------      
									-- INSERT BENIFICIARY DETAILS (PAYEE DETAILS)
									----------------------------------------------
									DECLARE @PAYEE_ID INT    
									select @PAYEE_ID=isnull(Max(PAYEE_ID),0)+1 
									FROM   CLM_PAYEE 
									WHERE  CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID
								 
	 								INSERT INTO CLM_PAYEE                                            
										(                                            
										CLAIM_ID,                                                 
										ACTIVITY_ID,            
										EXPENSE_ID,                                           
										PAYEE_ID,                                            
										PARTY_ID,                                       
										PAYMENT_METHOD,                                            
										ADDRESS1,                                            
										ADDRESS2,                                            
										CITY,                           
										[STATE],               
										ZIP,                                            
										COUNTRY,                                            
										NARRATIVE,           
										IS_ACTIVE,                                            
										CREATED_BY,                                       
										CREATED_DATETIME,                                    
										AMOUNT,                 
										INVOICE_NUMBER,                          
										INVOICE_DATE,   
										INVOICE_DUE_DATE,   
										INVOICE_SERIAL_NUMBER,   
										PAYEE_BANK_ID,
										SERVICE_TYPE,                          
										SERVICE_DESCRIPTION,                      
										SECONDARY_PARTY_ID,            
										FIRST_NAME,            
										LAST_NAME,            
										TO_ORDER_DESC  ,        
										PAYEE_PARTY_ID   ,  
										REIN_RECOVERY_NUMBER    ,  
										RECOVERY_TYPE                 
										)      
										(
										 SELECT TOP 1
										 CLAIM_ID,
										 @ACTIVITY_ID,
										 0, --EXPENSE ID,
										 @PAYEE_ID,
										 PARTY_ID,
										 14711, --- FOR CHEQUE
										 ISNULL(ADDRESS1,' '),
										 ADDRESS2,
										 CITY,
										 [STATE],
										 ZIP,
										 COUNTRY,
										 '', -- NARATIVE
										 'Y',
										 @CREATED_BY,
										 @TODAY_DATE,
										 @LOOP_PAYMENT_AMOUNT,
										 0,     --INVOICE_NUMBER
										 NULL,  --INVOICE_DATE
										 NULL,  --INVOICE_DUE_DATE
										 NULL,  -- INVOICE_SERIAL_NUMBER
										 0,   --PAYEE_BANK_ID
										 0 ,--SERVICE_TYPE
										 NULL,	--SERVICE_DESCRIPTION,      
										 NAME,	--SECONDARY_PARTY_ID,       
										 SUBSTRING(NAME,1,30),-- FIRST NAME            
										 SUBSTRING(NAME,30,30),-- LAST_NAME,            
										 NULL,--	TO_ORDER_DESC  ,        
										 PARTY_ID,--	PAYEE_PARTY_ID   ,  
										 NULL, --REIN_RECOVERY_NUMBER    , 
										 0   --RECOVERY_TYPE     
										 FROM  CLM_PARTIES
										 WHERE CLAIM_ID=@CLAIM_ID AND PARTY_ID=@PARTY_ID
										)
								    
								    ----------------------------------------------      
									-- CALCULATE BREAKDOWN 
									----------------------------------------------
									EXECUTE Proc_CalculateBreakdown
									@CLAIM_ID    = @CLAIM_ID,              
									@ACTIVITY_ID = @ACTIVITY_ID 
									
									----------------------------------------------      
									---- COMPLETE ACTIVITY
									---- COMLETE LAST ACTIVITY ONLY
									----------------------------------------------   	
									IF((@COUNTER-1)<@CHK_COUNTER)
									BEGIN
										EXEC [Proc_CompleteClaimActivities]   	
										 @CLAIM_ID            = @CLAIM_ID                        
										,@ACTIVITY_ID         = @ACTIVITY_ID         
										,@ACTIVITY_REASON     = 11775  -- ACTIVITY REASON FOR PAYMENT ACTIVITY                                      
										,@ACTION_ON_PAYMENT   = @ACTION_ON_PAYMENT  
										,@IS_VOIDED_ACTIVITY  = 'N'   
										,@VOID_ACTIVITY_ID    = -1    
										,@COMPETED_DATE       = @TODAY_DATE 
										,@COMPETED_BY         = @CREATED_BY
										,@LANG_ID			  = 2
										,@IS_ACC_COI_FLG      = 1
										,@TEXT_ID			  = 5
										,@TEXT_DESCRIPTION    = 'CO Aceito'
									END
											 
					            END -- END OF  IF(@ACTIVITY_ID<1) ELSE
						
							  END -- END OF  IF(@CLAIM_COVERAGE_ID<1) ELSE
							              
						
						  
						 END
					   END -- END OF LOOP
					   
					   DROP TABLE #TEMP_COVERAGES   
					   
					    ------------------------------------      
						-- UPDATE CLAIM DETAILS
						------------------------------------   	
						IF(@CLAIM_ID>0)
						BEGIN
							
							UPDATE MIG_PAID_CLAIM_DETAILS    
							SET    ALBA_CLAIM_ID      = @CLAIM_ID,
							       ALBA_CLAIM_NUMBER  = @ALBA_CLAIM_NUMBER ,      
								   POLICY_ID		  = @POLICY_ID,
								   POLICY_VERSION_ID  = @POLICY_VERSION_ID,
								   CUSTOMER_ID		  = @CUSTOMER_ID
							WHERE  LEADER_CLAIM_NUMBER=@LEADER_CLAIM_NUMBER AND HAS_ERRORS='N'
						END        
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
    
   
       
   
 END CATCH   
  
            
END 

























GO

