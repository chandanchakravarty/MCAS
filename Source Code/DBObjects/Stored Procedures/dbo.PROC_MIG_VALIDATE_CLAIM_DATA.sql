IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_VALIDATE_CLAIM_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_VALIDATE_CLAIM_DATA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                            
Proc Name             : Dbo.PROC_MIG_VALIDATE_CLAIM_DATA                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 25 May 2011                                                          
Purpose               : TO VALIDATE CLAIM DATA (FOR NEW CLAIM AND CLAIM UPDATE)   
Revison History       :                                                            
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc PROC_MIG_VALIDATE_CLAIM_DATA      870,'1332185',1,0                                            
------   ------------       -------------------------*/                                                            
--                               
                                
--                             
                          
CREATE PROCEDURE [dbo].[PROC_MIG_VALIDATE_CLAIM_DATA]    
  
@IMPORT_REQUEST_ID			INT,
@LEADER_CLAIM_NUMBER  		NVARCHAR(10),    
@MOVEMENT_TYPE				INT,                 
@HAS_ERRORS					INT OUT  
  
                               
AS                                
BEGIN                         
    

   
		 DECLARE  @CUSTOMER_ID           	INT =0   
		 DECLARE  @DATE_OF_LOSS           	DATETIME 
		 DECLARE  @POICY_EFFECTIVE_DATE   	DATETIME  
		 DECLARE  @POICY_EXPIRY_DATE      	DATETIME	 
		 DECLARE  @TODAY_DATE				DATETIME=GETDATE()
		 DECLARE  @LEADER_POLICY_NUMBER 	NVARCHAR(10)
		 DECLARE  @LEADER_ENDORSEMENT_NUMBER INT 
		 DECLARE  @POLICY_ID             	INT =0    
		 DECLARE  @POLICY_VERSION_ID     	INT =0   
		 DECLARE  @CLAIM_ID                 INT =0
		 DECLARE  @LOB_ID		           	INT   
		 DECLARE  @SUB_LOB_ID				INT
		 DECLARE  @LEADER_LOB           	INT   
		 DECLARE  @RISK_ID		           	INT    
		 DECLARE  @APP_STATUS            	NVARCHAR(25) 
		 DECLARE  @ACTIVITY_STATUS         	INT    
		 DECLARE  @ACTIVITY_TYPE         	INT    
	     DECLARE  @LOSS_TYPE            	NVARCHAR(50) 
	     DECLARE  @POLICY_STATUS 			VARCHAR(20) 
	    
	     
	      
        ------------------------------------      
	    -- LOOP FOR VALIDATE COVERAGE CODE      
	    ------------------------------------ 
	    DECLARE  @LOOP_COVERAGE_CODE	  INT  
	    DECLARE  @COVERAGE_ID			  INT    
	    DECLARE  @CLAIM_COVERAGE_ID	      INT 
	    DECLARE  @RESERVE_ID			  INT  
	    DECLARE  @RESERVE_TYPE		      INT   
	    DECLARE  @CHK_COUNTER  		      INT   
	    DECLARE  @OUTSTANDING_RESERVE     DECIMAL(28,4)
	    DECLARE  @IMPORT_SERIAL_NO        INT

		 -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES

		DECLARE @ERROR_NUMBER    INT
		DECLARE @ERROR_SEVERITY  INT
		DECLARE @ERROR_STATE     INT
		DECLARE @ERROR_PROCEDURE VARCHAR(512)
		DECLARE @ERROR_LINE  	 INT
		DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)

     BEGIN TRY
		 SET @HAS_ERRORS=0  
		 
		   CREATE TABLE #TEMP_COVERAGES      
				(      
				  ID                  INT  IDENTITY,      
				  IMPORT_SERIAL_NO    INT,      
				  COVERAGE_CODE       INT,
				  OUTSTANDING_RESERVE DECIMAL(25,4),
				  SUM_INSURED         DECIMAL(25,4),
				  RESERVE_TYPE        INT      
				)   
						   
      
	     SELECT TOP 1
	            @RISK_ID				   = INSURED_OBJECT_CLAIMED,
			    @LEADER_CLAIM_NUMBER       = LEADER_CLAIM_NUMBER  ,             
			    @LEADER_POLICY_NUMBER      = LEADER_POLICY_NUMBER  ,       
			    @LEADER_ENDORSEMENT_NUMBER = LEADER_ENDORSEMENT_NUMBER ,  
			    @DATE_OF_LOSS              = CONVERT (date,DATE_OF_LOSS,101),
			    @RISK_ID				   = INSURED_OBJECT_CLAIMED,
			    @LEADER_LOB				   = PRODUCT_LOB,
			    @LOSS_TYPE				   = LOSS_TYPE
	     FROM   MIG_CLAIM_DETAILS
	     WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND MOVEMENT_TYPE=@MOVEMENT_TYPE AND LEADER_CLAIM_NUMBER=@LEADER_CLAIM_NUMBER AND HAS_ERRORS='N' AND IS_DELETED='N'
	    
	     
	     --------------------------------------------------  
		 -- INVALID MOVEMET TYPE
		 -- ERROR TYPE =30 (NVALID MOVEMET TYPE)  
		 --------------------------------------------------        
         IF(@MOVEMENT_TYPE NOT IN(1,2))
         BEGIN         
            SET @HAS_ERRORS=30   
         END
         
         --=======================================================
		 -- FOR MOVEMENT TYPE = 2 (UPDATE CLAIM DETAIL)
		 --=======================================================
         IF(@MOVEMENT_TYPE=2)
         BEGIN
             --------------------------------------------------  
			 -- CHECK WHETHER CLAIM NUMBER EXISTS OR NOT
			 -- ERROR TYPE =19 (CLAIM DOES NOT EXISTS)  
			 --------------------------------------------------   
			SELECT @CLAIM_ID=CLAIM_ID ,
			       @LOB_ID		= CAST(ISNULL(P.POLICY_LOB,0) AS INT),
			       @SUB_LOB_ID  = CAST(ISNULL(P.POLICY_SUBLOB,0) AS INT)
			FROM   CLM_CLAIM_INFO AS C WITH(NOLOCK) INNER JOIN
			     POL_CUSTOMER_POLICY_LIST P WITH(NOLOCK) ON P.CUSTOMER_ID=C.CUSTOMER_ID AND P.POLICY_ID=C.POLICY_ID AND P.POLICY_VERSION_ID=C.POLICY_VERSION_ID
		    WHERE  LEADER_CLAIM_NUMBER=@LEADER_CLAIM_NUMBER
				
			IF(@CLAIM_ID IS NULL OR @CLAIM_ID=0)
			 SET @HAS_ERRORS=19   
			ELSE
			 BEGIN
			  
			  SELECT TOP 1 @ACTIVITY_TYPE   = ACTION_ON_PAYMENT,
			               @ACTIVITY_STATUS = ACTIVITY_STATUS
 			  FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID ORDER BY ACTIVITY_ID DESC
 			  
 			  --------------------------------------------------      
			  -- IF INCOMPLETE ACTVITY EXISTS  
			  --------------------------------------------------     
 			  IF(@ACTIVITY_STATUS=11800 )
 				SET @HAS_ERRORS=28
 			  ---------------------------------------------------------------------------      
			  -- IF LAST COMPLETED ACTIVITY IS CLOSE RESERVE THEN REOPEN RESERVE FIRST
			  --------------------------------------------------------------------------
 			  ELSE IF (@ACTIVITY_TYPE=167)
			    SET @HAS_ERRORS=31 
			  --------------------------------------------------      
			  -- FOR INVALID LOSS TYPE
			  --------------------------------------------------     
			  ELSE IF NOT EXISTS( SELECT ALBA_LOSS_TYPE FROM MIG_CLAIM_LOSS_TYPE_MAPPING M  WITH(NOLOCK) 
			                      WHERE ((ALBA_LOSS_TYPE=@LOSS_TYPE) OR (LEADER_LOSS_DESC1=@LOSS_TYPE)OR (LEADER_LOSS_DESC2=@LOSS_TYPE) OR (LEADER_LOSS_DESC3=@LOSS_TYPE))  )
				 BEGIN
				   SET @HAS_ERRORS=35  
				 END
			  ELSE
			  BEGIN
			  
			   
				          
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
					WHERE  IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID AND 
					       LEADER_CLAIM_NUMBER = @LEADER_CLAIM_NUMBER AND 
					       MOVEMENT_TYPE       = 2 AND
					       HAS_ERRORS          = 'N' 
				)  
						 
			 
				 	      
				 SELECT  @CHK_COUNTER= COUNT(ID) FROM #TEMP_COVERAGES      
			         
		         WHILE(@CHK_COUNTER>0)      
			      BEGIN      
			              
					SELECT @LOOP_COVERAGE_CODE    = COVERAGE_CODE ,
					       @RESERVE_TYPE          = RESERVE_TYPE  ,
					       @OUTSTANDING_RESERVE   = OUTSTANDING_RESERVE,
					       @IMPORT_SERIAL_NO      = IMPORT_SERIAL_NO
					FROM   #TEMP_COVERAGES      
					WHERE ID=@CHK_COUNTER      
					        
					------------------------------------      
					-- RESERVE TYPE 
					-- 1 = COVERAGE (INDEMNITY) RESERVE, 
					-- 2 = EXPENSE RESERVE, 
					-- 3 = PROFESSIONAL EXPENSE 
					------------------------------------    
					IF(@RESERVE_TYPE=1)
					 BEGIN
					 
					   SET @COVERAGE_ID       = NULL
					   SET @CLAIM_COVERAGE_ID = NULL 
					   
			           SELECT @COVERAGE_ID  = C.COV_ID 
			           FROM   MIG_POLICY_COVERAGE_CODE_MAPPING AS MIG WITH(NOLOCK) INNER JOIN 
							  MNT_COVERAGE AS C WITH(NOLOCK) ON CAST(ISNULL(C.CARRIER_COV_CODE,0) AS INT)=MIG.ALBA_COVERAGE_CODE AND C.LOB_ID=@LOB_ID AND C.SUB_LOB_ID=@SUB_LOB_ID
			           WHERE  LEADER_COVERAGE_CODE = @LOOP_COVERAGE_CODE
			          		                 
						-----------------------------------------------------------      
						-- GET ACTUAL CLAIM COVERAGE ID (CREATED ON CLAIM SIDE)
						------------------------------------------------------------
						SELECT @CLAIM_COVERAGE_ID = CLAIM_COV_ID 
						FROM   CLM_PRODUCT_COVERAGES 
						WHERE  CLAIM_ID			  = @CLAIM_ID AND 
							   COVERAGE_CODE_ID   = @COVERAGE_ID 
							   
				             --select @LOOP_COVERAGE_CODE as 'Loov COv',
			              --    @COVERAGE_ID as '@COVERAGE_ID',
			           		 -- @CLAIM_COVERAGE_ID as 'CLM COV ID',	
			           		 -- @LOB_ID as 'lob',
			           		 -- @SUB_LOB_ID as  'SUB LOB'
				         
				          
						IF(@CLAIM_COVERAGE_ID IS NULL OR @CLAIM_COVERAGE_ID<1)      
						  BEGIN
						  
							------------------------------------      
							-- COVERAGE IS NOT FOUND IN CLAIM (INSERT ERROR)
							------------------------------------     
							   SET @HAS_ERRORS=21       
							   
							   UPDATE MIG_CLAIM_DETAILS
							   SET    HAS_ERRORS='Y'  
							   WHERE  IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND 
							          IMPORT_SERIAL_NO =@IMPORT_SERIAL_NO 
							           
							   EXEC   PROC_MIG_INSERT_IMPORT_ERROR_DETAILS               
									  @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,  
									  @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,      
									  @ERROR_SOURCE_FILE     = ''     ,  
									  @ERROR_SOURCE_COLUMN   = ''     ,  
									  @ERROR_SOURCE_COLUMN_VALUE= '' ,  
									  @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO ,    
									  @ERROR_TYPES           = @HAS_ERRORS,      
									  @ACTUAL_RECORD_DATA    = ''  ,  
									  @ERROR_MODE               = 'VE',  -- VALIDATION ERROR   
									  @ERROR_SOURCE_TYPE        = 'CLM'    
									  
							   --SET @CHK_COUNTER=0 -- TO END LOOP
						  END   
					  
					 END   
					       
			        SET @CHK_COUNTER=@CHK_COUNTER-1
			       
			      END--END OF WHILE
			     
			  END
			 
			  
         END 
         
         
         --------------------------------------------------  
		 -- INSERT ERROR AND RETURN
		 --------------------------------------------------   
         IF(@HAS_ERRORS>0 AND  @HAS_ERRORS!=21)			 	 
          BEGIN  
            
           
             UPDATE MIG_CLAIM_DETAILS   
             SET    HAS_ERRORS='Y'  
             WHERE  IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID AND
                    LEADER_CLAIM_NUMBER  = @LEADER_CLAIM_NUMBER AND 
                    MOVEMENT_TYPE		 = @MOVEMENT_TYPE  
               
             
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
				,@HAS_ERRORS  
				,'VE'       
				,'CLM'   
			 FROM  MIG_CLAIM_DETAILS     
             WHERE IMPORT_REQUEST_ID		 = @IMPORT_REQUEST_ID AND  
				   LEADER_CLAIM_NUMBER		 = @LEADER_CLAIM_NUMBER AND
				   MOVEMENT_TYPE		     = @MOVEMENT_TYPE  
            )     
      END  
      
      
         
         END -- END OF IF(@MOVEMENT_TYPE=2) 
      
         --=======================================================
		 -- FOR MOVEMENT TYPE = 1 (INSERT CLAIM DETAIL)
		 --=======================================================
         IF(@MOVEMENT_TYPE=1)
         BEGIN
         
         --------------------------------------------------  
		 -- INVALID LOB FOR ACCEPTED COI LOAD
		 --------------------------------------------------  
         IF EXISTS(SELECT ALBA_SUSEP_LOB_CODE FROM MIG_POLICY_LOB_MAPPING WHERE LEADER_SUSEP_LOB_CODE= @LEADER_LOB AND  ALBA_SUSEP_LOB_CODE NOT IN(111,114,115,116,118,167,171 ,173,196,351,433,435,621,622,654,993))
         BEGIN
			SET @HAS_ERRORS=32  
         END
		 ELSE
         BEGIN
         
			 --------------------------------------------------  
			 -- GET POLICY DETAILS		
			 -- IF ENDORSEMENT IS NOT CREATED ON POLICY		 
			 --------------------------------------------------    
			
			 IF(@LEADER_ENDORSEMENT_NUMBER =0)     
			   BEGIN
			    
				   -------------------------------------------
				   -- IF GIVEN POLICY DOES NOT EXISTS
				   -------------------------------------------
				   IF NOT EXISTS(SELECT * FROM POL_CO_INSURANCE WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER)
				   BEGIN
				   
					     SET @HAS_ERRORS=10
						  
						  UPDATE MIG_CLAIM_DETAILS   
						  SET    HAS_ERRORS='Y'  
						  WHERE  IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID AND
								 LEADER_CLAIM_NUMBER  = @LEADER_CLAIM_NUMBER AND 
								 MOVEMENT_TYPE		  = @MOVEMENT_TYPE AND
								 HAS_ERRORS			  = 'N'
			               
			             
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
							,@HAS_ERRORS  
							,'VE'       
							,'CLM'   
						 FROM  MIG_CLAIM_DETAILS     
						 WHERE IMPORT_REQUEST_ID		 = @IMPORT_REQUEST_ID AND  
							   LEADER_CLAIM_NUMBER		 = @LEADER_CLAIM_NUMBER AND
							   MOVEMENT_TYPE		     = @MOVEMENT_TYPE 
						)                 
							 
					RETURN
					
				   END
				   ELSE
				   BEGIN
				    
				    
					-- FIND POLICY ACTIVE RECORD FOR GIVEN LOSS DATE
							 SELECT TOP 1
									@CUSTOMER_ID		  = COI.CUSTOMER_ID, 
									@POLICY_ID			  = COI.POLICY_ID, 
									@POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,
									@POICY_EFFECTIVE_DATE = POL.POLICY_EFFECTIVE_DATE,
									@POICY_EXPIRY_DATE	  = POL.POLICY_EXPIRATION_DATE,
									@LOB_ID			      = ISNULL(POLICY_LOB,0),
									@SUB_LOB_ID		      = ISNULL(POLICY_SUBLOB,0),
									@APP_STATUS           = POL.APP_STATUS,
									@POLICY_STATUS		  = POL.POLICY_STATUS
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
									@SUB_LOB_ID		      = ISNULL(POLICY_SUBLOB,0),
									@APP_STATUS           = POL.APP_STATUS,
									@POLICY_STATUS		  = POL.POLICY_STATUS
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
						      
								--DECLARE @MAX_VERSION_ID INT=0
						        
								--SELECT @MAX_VERSION_ID=MAX(POLICY_VERSION_ID )
								--FROM POL_CO_INSURANCE 
								--WHERE LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER
						      
								 SELECT @CUSTOMER_ID		  = COI.CUSTOMER_ID, 
										@POLICY_ID			  = COI.POLICY_ID, 
										@POLICY_VERSION_ID    = COI.POLICY_VERSION_ID,
										@POICY_EFFECTIVE_DATE = POL.POLICY_EFFECTIVE_DATE,
										@POICY_EXPIRY_DATE	  = POL.POLICY_EXPIRATION_DATE,
										@LOB_ID			      = ISNULL(POLICY_LOB,0),
										@SUB_LOB_ID		      = ISNULL(POLICY_SUBLOB,0),
										@APP_STATUS           = POL.APP_STATUS,
										@POLICY_STATUS		  = POL.POLICY_STATUS
								 FROM   POL_CO_INSURANCE AS COI INNER JOIN
										POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID				               
								 WHERE  COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND --POL.POLICY_NUMBER<>'' AND POL.POLICY_NUMBER IS NOT NULL AND
									    CONVERT(DATE,POL_VER_EFFECTIVE_DATE,101)<=@DATE_OF_LOSS AND @DATE_OF_LOSS<=CONVERT(DATE,POL_VER_EXPIRATION_DATE,101)
						      
					    END
					    
					    
					END	
				END
			  ELSE
				BEGIN
			    
			 --------------------------------------------------  
			 -- GET POLICY DETAILS		
			 -- IF ENDORSEMENT HAS CREATED ON POLICY		 
			 --------------------------------------------------      
				SELECT  @CUSTOMER_ID		  = COI.CUSTOMER_ID,  
						@POLICY_ID			  = COI.POLICY_ID, 
						@POLICY_VERSION_ID    = COI.POLICY_VERSION_ID, 
						@POICY_EFFECTIVE_DATE = POL.POLICY_EFFECTIVE_DATE,
						@POICY_EXPIRY_DATE	  = POL.POLICY_EXPIRATION_DATE,
						@LOB_ID			      = ISNULL(POLICY_LOB,0),
						@SUB_LOB_ID		      = ISNULL(POLICY_SUBLOB,0),
						@APP_STATUS           = POL.APP_STATUS,
						@POLICY_STATUS		  = POL.POLICY_STATUS
				 FROM   POL_CO_INSURANCE AS COI INNER JOIN
						POL_POLICY_ENDORSEMENTS AS EN ON  COI.CUSTOMER_ID=EN.CUSTOMER_ID AND COI.POLICY_ID=EN.POLICY_ID AND COI.POLICY_VERSION_ID=EN.POLICY_VERSION_ID INNER JOIN
						POL_CUSTOMER_POLICY_LIST AS POL ON COI.CUSTOMER_ID=POL.CUSTOMER_ID AND COI.POLICY_ID=POL.POLICY_ID AND COI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID				               
				 WHERE  COI.LEADER_POLICY_NUMBER=@LEADER_POLICY_NUMBER AND POL.POLICY_STATUS IS NOT NULL AND 
						POL.POLICY_STATUS <>'' AND POL.POLICY_STATUS NOT IN('UISSUE','REJECT','Suspended','APPLICATION') AND 
						POL.POLICY_NUMBER IS NOT NULL AND POL.POLICY_NUMBER<>'' AND
						CAST(EN.CO_ENDORSEMENT_NO AS INT) =@LEADER_ENDORSEMENT_NUMBER
			    
				END
		
		
		--SELECT @CUSTOMER_ID as '@CUSTOMER_ID'	,	 
		--	   @POLICY_ID	as '@POLICY_ID',		 
		--	   @POLICY_VERSION_ID as '@POLICY_VERSION_ID'  ,
		--	   @POICY_EFFECTIVE_DATE as '@POICY_EFFECTIVE_DATE',
		--	   @POICY_EXPIRY_DATE	 as '@POICY_EXPIRY_DATE',
		--	   @LOB_ID			    as '@LOB_ID' ,
		--	   @APP_STATUS         as '@APP_STATUS' ,
		--	   @POLICY_NUMBER	  as '@POLICY_NUMBER'	 
		        
			 --------------------------------------------------  
			 -- POLICY EXISTS DO NOT ESIXTS
			 --------------------------------------------------          
			 IF (@CUSTOMER_ID IS NULL OR @CUSTOMER_ID ='' OR @CUSTOMER_ID=0)        
			 BEGIN      
			 
			   -- POLICY WITH GIVEN ENDORSEMENT DOES NOT EXISTS
			   IF(@LEADER_ENDORSEMENT_NUMBER>0)
			      SET @HAS_ERRORS=26 
			   -- INVALID LOSS DATE   
			   ELSE			   
			      SET @HAS_ERRORS=18     
			   
			 END    
			 --------------------------------------------------  
			 -- APPLICATION EXISTS BUT NBS NOT COMMITED YET
			 -- ERROR TYPE =25  
			 --------------------------------------------------          
			 ELSE IF (@POLICY_STATUS IS NULL OR @POLICY_STATUS ='' or @POLICY_STATUS='UISSUE')        
			 BEGIN                 
			   SET @HAS_ERRORS=25    
			 END                
			 --------------------------------------------------  
			 -- CHECK WHETHER CLAIM NUMBER ALREADY EXISTS
			 -- ERROR TYPE =16 (CLAIM ALREADY EXISTS)  
			 --------------------------------------------------           
			 ELSE IF EXISTS(SELECT TOP 1 LEADER_CLAIM_NUMBER FROM CLM_CLAIM_INFO WHERE LEADER_CLAIM_NUMBER=@LEADER_CLAIM_NUMBER)    
			 BEGIN
				SET @HAS_ERRORS=16   
			 END
			 --------------------------------------------------  
			 -- VALIDATE LOSS DATE
			 -- ERROR TYPE =17 (Date of loss should not be future date.)  
			 --------------------------------------------------           
			 ELSE IF(CONVERT (DATE,@DATE_OF_LOSS,101)> CONVERT (DATE, @TODAY_DATE,101))
			 BEGIN
				SET @HAS_ERRORS=17   
			 END
			 --------------------------------------------------  
			 -- VALIDATE LOSS DATE
			 -- ERROR TYPE =18 (Date of loss should be in between policy effective and expiration date.)  
			 --------------------------------------------------           
			 ELSE IF(@DATE_OF_LOSS<CONVERT (DATE,@POICY_EFFECTIVE_DATE,101) OR @DATE_OF_LOSS>CONVERT(DATE,@POICY_EXPIRY_DATE,101))
			 BEGIN
				SET @HAS_ERRORS=18   
			 END
			 --------------------------------------------------  
			 -- IF RISK IS NOT PROVIDED IN FILE
			 -- ERROR TYPE =22 (Risk (INSURED OBJECT CLAIMED) is not provided in file)  
			 --------------------------------------------------           
			 ELSE IF(@RISK_ID IS NULL OR @RISK_ID<1)
			 BEGIN
				SET @HAS_ERRORS=22   
			 END 
			 --------------------------------------------------      
			 -- FOR INVALID LOSS TYPE
			 --------------------------------------------------     
			 ELSE IF NOT EXISTS( SELECT ALBA_LOSS_TYPE FROM MIG_CLAIM_LOSS_TYPE_MAPPING M  WITH(NOLOCK) 
			                      WHERE ((ALBA_LOSS_TYPE=@LOSS_TYPE) OR (LEADER_LOSS_DESC1=@LOSS_TYPE)OR (LEADER_LOSS_DESC2=@LOSS_TYPE) OR (LEADER_LOSS_DESC3=@LOSS_TYPE)) )
			 BEGIN
				   SET @HAS_ERRORS=35  
			 END
			 ELSE
			 BEGIN
	             
				 -----------------------------------------------------------------  
				 -- HERE WE ARE USING 58 CLAIM ADJUSTER 
				 -- ERROR TYPE =27 (CLAIM ADJUSTER IS DEFINED FOR GIVEN PRODUCT)  
				 -----------------------------------------------------------------        
	            
				IF NOT EXISTS(  SELECT A.ADJUSTER_ID FROM CLM_ADJUSTER  A LEFT OUTER JOIN
								CLM_ADJUSTER_AUTHORITY  B on A.ADJUSTER_ID=B.ADJUSTER_ID
								WHERE ADJUSTER_CODE='58' AND B.LOB_ID=@LOB_ID AND A.IS_ACTIVE='Y'
							 )
				 BEGIN
				   SET @HAS_ERRORS=27   
				 END
				 ELSE
				 BEGIN
	                  
				--------------------------------------------------  
				-- IF RISK IS NOT FOUND AT POLICY SIDE
				-- ERROR TYPE =20 (Risk is not found in policy)  
				--------------------------------------------------           
					 ---------------------------------------------------------
					 -- 9  : All Risks and Named Perils   
					 -- 26 : Engeneering Risks 
					 ---------------------------------------------------------  
					 IF(@LOB_ID IN(9,26))
					 BEGIN      
					   IF NOT EXISTS( SELECT  CUSTOMER_ID  FROM    POL_PERILS    P WITH(NOLOCK) 
									  WHERE (P.CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                
									 )
						BEGIN
						 SET @HAS_ERRORS=20    
						END 
						
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
					       
						IF NOT EXISTS(  SELECT P.PRODUCT_RISK_ID FROM  POL_PRODUCT_LOCATION_INFO  P WITH(NOLOCK)
										WHERE ( CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')          
									 )
						BEGIN
						 SET @HAS_ERRORS=20    
						END  
					
					 END           
					 ---------------------------------------------------------                       
					  -- 13 : MARITIME
					 ---------------------------------------------------------           
					  ELSE IF(@LOB_ID=13) 
					  BEGIN      
					       
						IF NOT EXISTS(  SELECT  CUSTOMER_ID  FROM    POL_MARITIME M WITH(NOLOCK) 
										WHERE  (  CO_RISK_ID=@RISK_ID AND M.CUSTOMER_ID=@CUSTOMER_ID AND M.POLICY_ID=@POLICY_ID AND M.POLICY_VERSION_ID= @POLICY_VERSION_ID AND M.IS_ACTIVE='Y')           
									 )
						BEGIN
						 SET @HAS_ERRORS=20    
						END  
					            
					  END           
					 ---------------------------------------------------------
					 -- 21 : Group Personal Accident for Passenger 
					 -- 34 : Group Life 
					 -- 15 : Individual Personal Accident
					 -- 33 : Mortgage
					 ---------------------------------------------------------    
					  ELSE IF(@LOB_ID IN (21,34,15,33))
					   BEGIN      
					      
						         
						IF NOT EXISTS(  SELECT CO_RISK_ID FROM   POL_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)
								WHERE (CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                
									 )
						BEGIN
						 SET @HAS_ERRORS=20    
						END  
						 
						 
					  END    
					  
					 --------------------------------------------------------- 
					  -- 22 : Personal Accident for Passengers 
					 ---------------------------------------------------------    
					  ELSE IF(@LOB_ID =22)    
					   BEGIN      
						 IF NOT EXISTS( SELECT CO_RISK_ID FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO P WITH(NOLOCK)  
										WHERE ( CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')
									 )
						 BEGIN
						  SET @HAS_ERRORS=20    
						 END  
					        
							
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
					    
					    
						IF NOT EXISTS(  SELECT CO_RISK_ID  FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK) 
									  WHERE  (  CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                
									)
						 BEGIN
						  SET @HAS_ERRORS=20    
						 END  
					   
					  
					  END         
					        
					        
					 --------------------------------------------------------- 
					  --  20 : National Cargo Transport
					  --  23 : International Cargo Transport
					 ---------------------------------------------------------               
					ELSE IF(@LOB_ID IN (20,23))    
					   BEGIN      
					        
						 IF NOT EXISTS( SELECT  CO_RISK_ID FROM    POL_COMMODITY_INFO  P WITH(NOLOCK)
										WHERE  ( CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                
									)
						 BEGIN
						  SET @HAS_ERRORS=20    
						 END  
						 
					
					 END      


					 --------------------------------------------------------- 
					  -- 30 : Dpvat(Cat. 3 e 4)
					  -- 36 : DPVAT(Cat.1,2,9 e 10)
					 ---------------------------------------------------------     
					  ELSE IF(@LOB_ID IN (30,36))
					   BEGIN      
					         
						IF NOT EXISTS(  SELECT CO_RISK_ID  FROM   POL_CIVIL_TRANSPORT_VEHICLES  P WITH(NOLOCK) 
										WHERE  ( CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                
									)
						 BEGIN
						  SET @HAS_ERRORS=20    
						 END   
						 
						  
					  END       
					  
					 --------------------------------------------------------- 
					  -- 35 : Rural Lien
					 ---------------------------------------------------------     
					  ELSE IF(@LOB_ID IN (35))
					   BEGIN      
					          
						 IF NOT EXISTS(   SELECT  CO_RISK_ID FROM   POL_PENHOR_RURAL_INFO  P WITH(NOLOCK)
						  WHERE  ( CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                
									  )
						 BEGIN
							 SET @HAS_ERRORS=20    
						 END    
						  
					  END       
					  
					  --------------------------------------------------------- 
					  -- 37 : Rental Security
					 ---------------------------------------------------------     
					  ELSE IF(@LOB_ID IN (37))
					   BEGIN      
						 IF NOT EXISTS( SELECT CO_RISK_ID   FROM   POL_PENHOR_RURAL_INFO  P WITH(NOLOCK) 
										WHERE  ( CO_RISK_ID=@RISK_ID AND P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND P.IS_ACTIVE='Y')                
									  )
						 BEGIN
							 SET @HAS_ERRORS=20    
						 END    
						 
						  
					  END       
					  
					 --------------------------------------------------  
					 -- VALIDATE COVERAGE CODE
					 --------------------------------------------------           
					 IF(@HAS_ERRORS=0)
					 BEGIN	   
				          
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
										
					      
					   SELECT  @CHK_COUNTER= COUNT(ID) FROM #TEMP_COVERAGES      
					         
					 WHILE(@CHK_COUNTER>0)      
					   BEGIN      
					        
					        SET @LOOP_COVERAGE_CODE = NULL
					        SET @COVERAGE_ID        = NULL
					        SET @RESERVE_TYPE       = NULL
					        
					       					         
							SELECT @LOOP_COVERAGE_CODE    = COVERAGE_CODE ,
							       @RESERVE_TYPE          = RESERVE_TYPE  ,
							       @OUTSTANDING_RESERVE   = OUTSTANDING_RESERVE
							FROM   #TEMP_COVERAGES      
							WHERE ID=@CHK_COUNTER      
					        
					        
					       
					               
							------------------------------------      
							-- RESERVE TYPE (1 = COVERAGE (INDEMNITY) RESERVE, 2 = EXPENSE RESERVE, 3 = PROFESSIONAL EXPENSE  )
							------------------------------------    
					        SET @HAS_ERRORS =0
							IF(@RESERVE_TYPE=1)
							 BEGIN
							 
					           SELECT @COVERAGE_ID=C.COV_ID 
					           FROM   MIG_POLICY_COVERAGE_CODE_MAPPING AS MIG INNER JOIN 
									  MNT_COVERAGE AS C ON CAST(ISNULL(C.CARRIER_COV_CODE,0) AS INT)=MIG.ALBA_COVERAGE_CODE AND C.LOB_ID=@LOB_ID AND C.SUB_LOB_ID=@SUB_LOB_ID
					           WHERE  LEADER_COVERAGE_CODE=@LOOP_COVERAGE_CODE  
					         
							   IF(@COVERAGE_ID IS NULL OR @COVERAGE_ID<1)      
							    BEGIN      
								  
								   ------------------------------------      
								   -- INVALID COVERAGE CODE
								   ------------------------------------     
								   SET @HAS_ERRORS = 24  
								   SET @CHK_COUNTER =0  -- TO END LOOP 
								 END   
						     END  
							            
					   SET @CHK_COUNTER=@CHK_COUNTER-1
					   
					 END -- END OF WHILE
					 
					  
					   
	                END -- END OF  IF(@HAS_ERRORS=0)
				END -- END OF ELSE PART
	            
			 END						     
			 
		  END-- END OF INVALID LOB FOR ACCEPTED COI LOAD 
	
		  IF(@HAS_ERRORS>0 )			 	 
          BEGIN  
            
           
             UPDATE MIG_CLAIM_DETAILS   
             SET    HAS_ERRORS='Y'  
             WHERE  IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID AND
                    LEADER_CLAIM_NUMBER  = @LEADER_CLAIM_NUMBER AND 
                    MOVEMENT_TYPE		 = @MOVEMENT_TYPE 
               
             
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
				,@HAS_ERRORS  
				,'VE'       
				,'CLM'   
			 FROM  MIG_CLAIM_DETAILS     
             WHERE IMPORT_REQUEST_ID		 = @IMPORT_REQUEST_ID AND  
				   LEADER_CLAIM_NUMBER		 = @LEADER_CLAIM_NUMBER AND
				   MOVEMENT_TYPE		     = @MOVEMENT_TYPE 
            )                 
           END    	  
		END	-- END OF FOR MOVEMENT TYPE = 1  			      

     DROP TABLE #TEMP_COVERAGES					 
  
      
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

