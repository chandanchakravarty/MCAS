IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CalculateBreakdown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CalculateBreakdown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

         
/*----------------------------------------------------------                                                        
--Proc Name       : dbo.[Proc_CalculateBreakdown]                                                        
--Created by      : Santosh Kumar Gautam                                                    
--Date            : 07 Dec 2010                                                     
--Purpose         :  Copy co insurance and reinsurance details                                                     
--Revison History :                                                        
--Used In         : CLAIM                         
--------   ------------       -------------------------*/                                                        
---- DROP PROC dbo.[Proc_CalculateBreakdown]    1002,  4   
                                                    
CREATE PROC [dbo].[Proc_CalculateBreakdown]                                                                                               
 @CLAIM_ID int ,                        
 @ACTIVITY_ID int 
          
AS                                          
BEGIN            
        
 DECLARE @NetRIPercentage Decimal (8,4)          
 DECLARE @BreakdownID INT=0            
 DECLARE @CLM_RI_APPLIES_FLG INT=0
 DECLARE @COI_TRAN_TYPE INT=0
 DECLARE @ACTION_ON_PAYMENT INT=0
 DECLARE @PAYEE_ID INT=0

 DECLARE @PARTY_TYPE INT=0
 
 DECLARE @COVEAGE_TYPE INT=0
 DECLARE @PREVIOUS_ACTIVITY INT 
 
 SELECT @CLM_RI_APPLIES_FLG=CLM_RI_APPLIES_FLG 
 FROM MNT_SYSTEM_PARAMS  WITH(NOLOCK) 
 
 SELECT @COI_TRAN_TYPE= COI_TRAN_TYPE ,@ACTION_ON_PAYMENT=ACTION_ON_PAYMENT
 FROM   CLM_ACTIVITY WITH(NOLOCK)
 WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                     
                                                      
CREATE TABLE #TEMP_REINSURANCE              
(           
 ID INT IDENTITY,              
 [COMP_PERCENTAGE] DECIMAL(18,4) NULL,                     
 COMP_ID int NULL,               
 [PARTY_TYPE] [nvarchar](2) NULL ,          
 [CLAIM_ID]INT NULL          
)              
       
 DELETE FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN]  WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID      
      
 /*==========================================================================================
 
 == ADDED BY SANTOSH KUMAR GAUTAM ON 30 JUNE 2011 (ITRACK : 1289,961)
 == CASE 1 : COI_TRAN_TYPE=14850 (PARTIAL)
		  => NO NEED TO CALCULATE CO AND RI RECOVERY RESEREVE BECUASE ALBA PAYING HIS SHARE
                  
 == CASE 2 : COI_TRAN_TYPE=14849 (FULL)
		  => CALCULATE CO AND RI RECOVERY RESEREVE
			
 == CASE 3 : COI_TRAN_TYPE=14923 (ALBA AND RI SHARE)
          => CALCULATE RI RECOVERY RESEREVE
          => NO NEED TO CALCULATE CO RECOVERY RESEREVE BECUASE ALBA PAYING HIS SHARE AND RI SHARE
 
 == CASE 4 : COI_TRAN_TYPE=14924 (ALBA AND COI SHARE)
          => CALCULATE CO RECOVERY RESEREVE
          => NO NEED TO CALCULATE RI RECOVERY RESEREVE BECUASE ALBA PAYING HIS SHARE AND CO SHARE
 
 ===========================================================================================*/   
    
 
 --------------------------------------------------      
 --- FOR CO INSURANCE  
 --  CO INSURANCE WILL CALCULATE ONLY WHEN @COI_TRAN_TYPE=FULL    
 --------------------------------------------------     
 
 -- THIS IS VALUES OF TRANSACTION TYPE DROPDOWN IN ACTIVITY SCREEN
 IF(@COI_TRAN_TYPE IN (14849,14923,14924)) 
    BEGIN
    

	 --============================================================================
	-- MODIFIED BY SANTOSH GAUTAM ON 06 DEC 2011 REF ITRACK(1827)
	--============================================================================
	-- THE SAME WAY THE SYSTEM DOES FOR CLAIM PAYMENTS, BY CREATING COINSURANCE 
	-- AND REINSURANCE RESERVE,THE SYSTEM MUST CREATE COINSURANCE AND REINSURANCE 
	-- RESERVE ALSO ON RECOVERING SALVAGE OR SUBROGATION
	--============================================================================ 
	 SELECT  @PREVIOUS_ACTIVITY = ACTIVITY_ID
	 FROM CLM_ACTIVITY_CO_RI_BREAKDOWN
	 WHERE CLAIM_ID= @CLAIM_ID
	       AND ACTIVITY_ID <@ACTIVITY_ID
	       
     --====================================
     -- TYPE OF COVERAGE
     --====================================
     IF(@ACTION_ON_PAYMENT IN(190,192))
      BEGIN     
		 SELECT @COVEAGE_TYPE=COVERAGE_CODE_ID 
		 FROM  CLM_ACTIVITY_RESERVE RES INNER JOIN
			   CLM_PRODUCT_COVERAGES COV ON RES.CLAIM_ID = COV.CLAIM_ID  AND RES.COVERAGE_ID = COV.CLAIM_COV_ID
		 WHERE RES.CLAIM_ID=@CLAIM_ID AND RES.ACTIVITY_ID = @ACTIVITY_ID AND RES.RECOVERY_AMOUNT >0 
	  END    
     
  
             
      --------------------------------------------------      
      -- FOR PAYMENT, RESERVE ACTIVITY TYPE
      --------------------------------------------------    
      IF (@ACTION_ON_PAYMENT NOT IN(190,192))
      BEGIN
		INSERT INTO #TEMP_REINSURANCE          
		 (          
		  COMP_PERCENTAGE,          
		  COMP_ID,          
		  [PARTY_TYPE],          
		  [CLAIM_ID]          
		 )          
		 (          
		    
		  SELECT SUM((CAST( ISNULL(C.PARTY_PERCENTAGE,0) AS DECIMAL(18,4))))  AS COMP_PERCENTAGE,C.SOURCE_PARTY_ID,'CO' AS PARTY_TYPE,D.CLAIM_ID          
		  FROM CLM_PARTIES  C  WITH(NOLOCK)        
		  INNER JOIN          
		  CLM_CLAIM_INFO D WITH(NOLOCK) ON C.CLAIM_ID= D.CLAIM_ID       
		  WHERE (C.IS_ACTIVE='Y' AND 
				 C.CLAIM_ID=@CLAIM_ID AND 
				 C.SOURCE_PARTY_TYPE_ID<>14548 AND -- PARTY SHOULD BE FOLLOWER ( 14548 :LEADER ,14549 :FOLLOWER)
				 D.CO_INSURANCE_TYPE=14548     AND -- ONLY FOR LEADER TYPE(14547:DIRECT, 14548 :LEADER ,14549 :FOLLOWER )    
				 C.PARTY_TYPE_ID=618		   AND	   -- FOR CO-INSURANCE PARTY
				 C.SOURCE_PARTY_ID IS NOT NULL 
				)           
		  GROUP BY C.SOURCE_PARTY_ID,D.CLAIM_ID 		  )

		  
		  
		END	  
	  -------------------------------------------------      
      -- FOR RECOVERY ACTIVITY TYPE
      --------------------------------------------------    
        ELSE
        BEGIN
             
             IF EXISTS(SELECT CLAIM_ID FROM CLM_PAYEE WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID= @ACTIVITY_ID)
              BEGIN
              
              SELECT @PAYEE_ID=P.SOURCE_PARTY_ID, @PARTY_TYPE=P.PARTY_TYPE_ID 
              FROM  CLM_PAYEE C WITH(NOLOCK) INNER JOIN
                    CLM_PARTIES P WITH(NOLOCK) ON C.CLAIM_ID=P.CLAIM_ID and C.PARTY_ID=P.PARTY_ID
              WHERE C.CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID= @ACTIVITY_ID
              
              
              
              
					INSERT INTO #TEMP_REINSURANCE          
					(          
						COMP_PERCENTAGE,          
						COMP_ID,          
						[PARTY_TYPE],          
						[CLAIM_ID]          
					)          
					(          
					 
						SELECT SUM((CAST( ISNULL(C.PARTY_PERCENTAGE,0) AS DECIMAL(18,4))))  AS COMP_PERCENTAGE,C.SOURCE_PARTY_ID,'CO' AS PARTY_TYPE,D.CLAIM_ID          
						FROM CLM_PARTIES     C WITH(NOLOCK)         
						INNER JOIN          
						CLM_CLAIM_INFO D WITH(NOLOCK)   ON C.CLAIM_ID= D.CLAIM_ID
						--FOR RECOVERY ACTIVITY: BREAKDOWN MUST CALCULATED FOR ALL PARTIES (REFER ITRACK:1263)
						--INNER JOIN CLM_PAYEE P ON P.CLAIM_ID=C.CLAIM_ID AND P.ACTIVITY_ID=@ACTIVITY_ID
						--	AND C.PARTY_ID =P.PARTY_ID 
						WHERE (C.IS_ACTIVE='Y' AND 
						 C.CLAIM_ID=@CLAIM_ID AND 
						 C.SOURCE_PARTY_TYPE_ID<>14548 AND -- PARTY SHOULD BE FOLLOWER ( 14548 :LEADER ,14549 :FOLLOWER)
						 D.CO_INSURANCE_TYPE=14548     AND -- ONLY FOR LEADER TYPE(14547:DIRECT, 14548 :LEADER ,14549 :FOLLOWER )    
						 C.PARTY_TYPE_ID=618		   AND	   -- FOR CO-INSURANCE PARTY
						 C.SOURCE_PARTY_ID IS NOT NULL 
						)           
						GROUP BY C.SOURCE_PARTY_ID,D.CLAIM_ID   

					--- COPY RECORDS FROM POL_CO_INSURANCE THOSE ARE NOT A LEADER      
					--- COPY RECORDS ONLY WHEN THE CO_INSURANCE OF POLICY IS LEADER TYPE      

					) 
			  END      
		END 
			       
	-- select * from #TEMP_REINSURANCE
	  SELECT @BreakdownID=(ISNULL(MAX(BREAKDOWN_ID),0)) FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
			               
			    
	--============================================================================
	-- MODIFIED BY SANTOSH GAUTAM ON 06 DEC 2011 REF ITRACK(1827)
	--============================================================================
	-- THE SAME WAY THE SYSTEM DOES FOR CLAIM PAYMENTS, BY CREATING COINSURANCE 
	-- AND REINSURANCE RESERVE,THE SYSTEM MUST CREATE COINSURANCE AND REINSURANCE 
	-- RESERVE ALSO ON RECOVERING SALVAGE OR SUBROGATION
	--============================================================================ 
	
	
	--- FOR RECOVERY ACTIVITY WHEN RECOVERY IS DONE FROM OTHER THE COI AND RI PARTY
		 IF(@ACTION_ON_PAYMENT IN(190,192))
		  BEGIN		   
		     
		     IF(@PARTY_TYPE NOT IN(618,619) AND @COVEAGE_TYPE IN (50019,50021))
		      BEGIN
				 INSERT INTO [dbo].[CLM_ACTIVITY_CO_RI_BREAKDOWN]          
				   (          
					[CLAIM_ID]                            
				   ,[ACTIVITY_ID]          
				   ,[RESERVE_ID]          
				   ,[BREAKDOWN_ID]          
				   ,[COMP_TYPE]          
				   ,[COMP_ID]          
				   ,[COMP_PERCENTAGE]    
				   ,[RESERVE_AMT]          
				   ,[TRAN_RESERVE_AMT]          
				   ,[PAYMENT_AMT]   
				   ,[RECOVERY_AMT]       
				   ,[IS_ACTIVE]          
				   ,[CREATED_BY]          
				   ,[CREATED_DATETIME]          
				   ,[MODIFIED_BY]          
				   ,[LAST_UPDATED_DATETIME]   
				   
				   )               
				   (   
		           
				   SELECT           
					C.CLAIM_ID          
				   ,C.ACTIVITY_ID          
				   ,RESERVE_ID          
				   ,@BreakdownID+row_number() over(order by C.CLAIM_ID,RESERVE_ID asc)                      
				   ,PARTY_TYPE         
				   ,COMP_ID      --<COMP_ID, char(2),>          
				   ,COMP_PERCENTAGE    --<COMP_PERCENTAGE, decimal(6,4),>          
				   , 
				     ISNULL((
				       SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
				       WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
				             AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
								 (
									SELECT TOP 1 RESERVE_ID
									FROM  CLM_ACTIVITY_RESERVE CAR
									WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
									  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
								   )
				        ),0)  
				    -(ISNULL(((C.RECOVERY_AMOUNT  * COMP_PERCENTAGE)/100) ,0)) -- PREVIOUS CO RESERVE-RECOVERY AMOUNT
				  
				   ,-ISNULL(((RECOVERY_AMOUNT  * COMP_PERCENTAGE)/100),0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
				   , ISNULL(((RECOVERY_AMOUNT  * COMP_PERCENTAGE)/100),0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
				   , 0        --<RECOVERY_AMOUNT, decimal(18,2),>       
				   ,'Y'       --<IS_ACTIVE, char(1),>          
				   ,C.CREATED_BY          
				   ,C.CREATED_DATETIME          
				   ,C.MODIFIED_BY          
				   ,C.LAST_UPDATED_DATETIME  
				  
					FROM CLM_ACTIVITY_RESERVE C  WITH(NOLOCK)  INNER JOIN          
						 #TEMP_REINSURANCE T ON C.CLAIM_ID=T.CLAIM_ID   INNER JOIN      
						  CLM_PRODUCT_COVERAGES P  WITH(NOLOCK)  ON P.CLAIM_ID=C.CLAIM_ID AND P.CLAIM_COV_ID=C.COVERAGE_ID      
					WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID 
						  AND P.RI_APPLIES =CASE WHEN @CLM_RI_APPLIES_FLG=1 THEN 'Y' ELSE P.RI_APPLIES END
				   )   
				   
				   
              END              
              ELSE
              BEGIN
              
                INSERT INTO [dbo].[CLM_ACTIVITY_CO_RI_BREAKDOWN]          
			   (          
				[CLAIM_ID]                            
			   ,[ACTIVITY_ID]          
			   ,[RESERVE_ID]          
			   ,[BREAKDOWN_ID]          
			   ,[COMP_TYPE]          
			   ,[COMP_ID]          
			   ,[COMP_PERCENTAGE]          
			   ,[RESERVE_AMT]          
			   ,[TRAN_RESERVE_AMT]          
			   ,[PAYMENT_AMT]    
			   ,[RECOVERY_AMT]          
			   ,[IS_ACTIVE]          
			   ,[CREATED_BY]          
			   ,[CREATED_DATETIME]          
			   ,[MODIFIED_BY]          
			   ,[LAST_UPDATED_DATETIME]       
			       
			   )               
			   (          
				SELECT           
				C.CLAIM_ID   
			   ,C.ACTIVITY_ID          
			   ,RESERVE_ID          
			   ,@BreakdownID+row_number() over(order by C.CLAIM_ID,RESERVE_ID asc)                      
			   ,PARTY_TYPE          
			   ,COMP_ID  --<COMP_ID, char(2),>          
			   ,COMP_PERCENTAGE   --<COMP_PERCENTAGE, decimal(6,4),>          
			   ,CASE WHEN @ACTION_ON_PAYMENT =192 AND COMP_ID=@PAYEE_ID THEN 0 ELSE 
			   
			     ISNULL((
				       SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
				       WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
				             AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
								 (
									SELECT TOP 1 RESERVE_ID
									FROM  CLM_ACTIVITY_RESERVE CAR
									WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
									  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
								   )
				        ),0)  
			    END--<RESERVE_AMT, decimal(18,2),>          
			   ,ISNULL((COMP_PERCENTAGE*OUTSTANDING_TRAN )/100,0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
			   ,ISNULL(( 
			    (CASE WHEN @COI_TRAN_TYPE=14923 THEN 0 ELSE COMP_PERCENTAGE END)
			    *PAYMENT_AMOUNT )/100,0) --<PAYMENT_AMT, decimal(18,2),>  
			   --- RECOVERY AMOUNT SHOULD BE COPIED FOR ONLY FOR PAYEE (REFER ITRACK:1263)         
			   ,CASE WHEN COMP_ID=@PAYEE_ID THEN -ISNULL(RECOVERY_AMOUNT ,0) ELSE 0 END --<RECOVERY_AMOUNT, decimal(18,2),>       
			   ,'Y'  --<IS_ACTIVE, char(1),>          
			   ,C.CREATED_BY          
			   ,C.CREATED_DATETIME          
			   ,C.MODIFIED_BY          
			   ,C.LAST_UPDATED_DATETIME 
			        
				FROM CLM_ACTIVITY_RESERVE C WITH(NOLOCK)   INNER JOIN          
					 #TEMP_REINSURANCE T ON C.CLAIM_ID=T.CLAIM_ID                        
				WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID       
			   )      
              
              END -- END OF  IF(@PARTY_TYPE NOT IN(618,619) AND @COVEAGE_TYPE IN (50019,50021))
              
		  END    --- end of begin 		 IF((@ACTION_ON_PAYMENT IN(190,192)) AND @PARTY_TYPE NOT IN(618,619) AND @COVEAGE_TYPE IN (50019,50021))       
		  ELSE
		  BEGIN
		  
			  INSERT INTO [dbo].[CLM_ACTIVITY_CO_RI_BREAKDOWN]          
			   (          
				[CLAIM_ID]                            
			   ,[ACTIVITY_ID]          
			   ,[RESERVE_ID]          
			   ,[BREAKDOWN_ID]          
			   ,[COMP_TYPE]          
			   ,[COMP_ID]          
			   ,[COMP_PERCENTAGE]          
			   ,[RESERVE_AMT]          
			   ,[TRAN_RESERVE_AMT]          
			   ,[PAYMENT_AMT]    
			   ,[RECOVERY_AMT]          
			   ,[IS_ACTIVE]          
			   ,[CREATED_BY]          
			   ,[CREATED_DATETIME]          
			   ,[MODIFIED_BY]          
			   ,[LAST_UPDATED_DATETIME]   
			       
			   )               
			   (          
				SELECT           
				C.CLAIM_ID   
			   ,C.ACTIVITY_ID          
			   ,RESERVE_ID          
			   ,@BreakdownID+row_number() over(order by C.CLAIM_ID,RESERVE_ID asc)                      
			   ,PARTY_TYPE          
			   ,COMP_ID  --<COMP_ID, char(2),>          
			   ,COMP_PERCENTAGE   --<COMP_PERCENTAGE, decimal(6,4),>          
			   ,CASE WHEN @ACTION_ON_PAYMENT =192 AND COMP_ID=@PAYEE_ID THEN 0 
			         WHEN @ACTION_ON_PAYMENT IN (165,166,168) THEN  ISNULL((COMP_PERCENTAGE*OUTSTANDING )/100,0) 
			         WHEN @ACTION_ON_PAYMENT IN (180,181) THEN 
			          
			           CASE WHEN @COI_TRAN_TYPE<> 14923 THEN
							 (
							   ISNULL((
							   SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
							   WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
									 AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
										 (
											SELECT TOP 1 RESERVE_ID
											FROM  CLM_ACTIVITY_RESERVE CAR
											WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
											  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
										   )
								),0)  
								- ISNULL((COMP_PERCENTAGE*PAYMENT_AMOUNT )/100,0)
							 )
					     ELSE
					        ISNULL((
							   SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
							   WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
									 AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
										 (
											SELECT TOP 1 RESERVE_ID
											FROM  CLM_ACTIVITY_RESERVE CAR
											WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
											  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
										   )
								),0)  
						 END
					     
			         
			         END--<RESERVE_AMT, decimal(18,2),>          
			   ,ISNULL((COMP_PERCENTAGE*OUTSTANDING_TRAN )/100,0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
			   ,ISNULL(( 
			    (CASE WHEN @COI_TRAN_TYPE=14923 THEN 0 ELSE COMP_PERCENTAGE END)
			    *PAYMENT_AMOUNT )/100,0) --<PAYMENT_AMT, decimal(18,2),>  
			   --- RECOVERY AMOUNT SHOULD BE COPIED FOR ONLY FOR PAYEE (REFER ITRACK:1263)         
			   ,0  --<RECOVERY_AMOUNT, decimal(18,2),>       
			   ,'Y'  --<IS_ACTIVE, char(1),>          
			   ,C.CREATED_BY          
			   ,C.CREATED_DATETIME          
			   ,C.MODIFIED_BY          
			   ,C.LAST_UPDATED_DATETIME    
			   
				FROM CLM_ACTIVITY_RESERVE C WITH(NOLOCK)   INNER JOIN          
					 #TEMP_REINSURANCE T ON C.CLAIM_ID=T.CLAIM_ID                        
				WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID       
			   )          
         
          END
          
   
       
 --------------------------------------------------      
 --- FOR RI INSURANCE      
 --------------------------------------------------      
             
 SELECT @NetRIPercentage =  ISNULL((1- (SUM(COMP_PERCENTAGE)/100)),1) from #TEMP_REINSURANCE        
       
 TRUNCATE TABLE #TEMP_REINSURANCE           

 
 --------------------------------------------------      
  -- FOR PAYMENT, RESERVE ACTIVITY TYPE
  --------------------------------------------------    
 IF (@ACTION_ON_PAYMENT NOT IN(190,192))
   BEGIN
   
	INSERT INTO #TEMP_REINSURANCE          
	 (          
	        
	  COMP_PERCENTAGE,          
	  COMP_ID,          
	  [PARTY_TYPE],          
	  [CLAIM_ID]          
	 )          
	 (          
	       
	  SELECT SUM((CAST( ISNULL(C.PARTY_PERCENTAGE,0) AS DECIMAL(18,4))))  AS COMP_PERCENTAGE,C.SOURCE_PARTY_ID AS COMP_ID,'RI' AS PARTY_TYPE,D.CLAIM_ID          
	  FROM CLM_PARTIES  C  WITH(NOLOCK)        
	  INNER JOIN          
	  CLM_CLAIM_INFO D  WITH(NOLOCK)  ON C.CLAIM_ID=D.CLAIM_ID    	 
	  WHERE D.CLAIM_ID=@CLAIM_ID 
	       AND C.PARTY_TYPE_ID=619   -- FOR RE-INSURANCE PARTY   
	       AND C.SOURCE_PARTY_ID IS NOT NULL 
	  GROUP BY C.SOURCE_PARTY_ID,D.CLAIM_ID        
	        
	 )   
	 
	 SELECT * FROM #TEMP_REINSURANCE
   END
   ELSE
   BEGIN
    
      IF EXISTS(SELECT CLAIM_ID FROM CLM_PAYEE WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID= @ACTIVITY_ID)
       BEGIN
       
       DECLARE @RECOVERY_TYPE INT 
      
        SELECT @RECOVERY_TYPE=RECOVERY_TYPE, @PAYEE_ID=P.SOURCE_PARTY_ID, @PARTY_TYPE=P.PARTY_TYPE_ID 
        FROM   CLM_PAYEE C WITH(NOLOCK) INNER JOIN
               CLM_PARTIES P WITH(NOLOCK) ON C.CLAIM_ID=P.CLAIM_ID and C.PARTY_ID=P.PARTY_ID
        WHERE  C.CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID= @ACTIVITY_ID
       
		INSERT INTO #TEMP_REINSURANCE          
		(          
		    
			COMP_PERCENTAGE,          
			COMP_ID,          
			[PARTY_TYPE],          
			[CLAIM_ID]          
		)          
		(          
		   
		   ----------------------------------------------------------------
		   -- IF RECOVERY TYPE IS XOL(14897) THEN PARTY PERCENTAGE WOULD BE ZERO
		   ----------------------------------------------------------------
			SELECT SUM((CAST( ISNULL( (CASE WHEN @RECOVERY_TYPE=14897 THEN 0 ELSE  C.PARTY_PERCENTAGE END),0) AS DECIMAL(18,4))))  AS COMP_PERCENTAGE,
			C.SOURCE_PARTY_ID AS COMP_ID,'RI' AS PARTY_TYPE,D.CLAIM_ID          
			FROM CLM_PARTIES  C   WITH(NOLOCK)      
			INNER JOIN          
			CLM_CLAIM_INFO D WITH(NOLOCK)  ON C.CLAIM_ID=D.CLAIM_ID 
			--------------------------------------------------------------------------------------- 
			--FOR RECOVERY ACTIVITY: BREAKDOWN MUST CALCULATED FOR ALL PARTIES (REFER ITRACK:1263)
			---------------------------------------------------------------------------------------
			--INNER JOIN  			
			--CLM_PAYEE P ON P.CLAIM_ID=C.CLAIM_ID AND P.ACTIVITY_ID=@ACTIVITY_ID	AND C.PARTY_ID =P.PARTY_ID     
			WHERE D.CLAIM_ID=@CLAIM_ID 
			      AND C.PARTY_TYPE_ID=619   -- FOR RE-INSURANCE PARTY   
			      AND  C.SOURCE_PARTY_ID IS NOT NULL 
			GROUP BY C.SOURCE_PARTY_ID,D.CLAIM_ID         
		    
		)   
  END
	 
	 
   END -- END OF  IF (@ACTION_ON_PAYMENT NOT IN(190,192))
       
  SELECT @BreakdownID=(ISNULL(MAX(BREAKDOWN_ID),0)) FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
   
	
	
	--- FOR RECOVERY ACTIVITY WHEN RECOVERY IS DONE FROM OTHER THE COI AND RI PARTY
   IF(@ACTION_ON_PAYMENT IN(190,192))
	 BEGIN		   
		     
     IF(@PARTY_TYPE NOT IN(618,619) AND @COVEAGE_TYPE IN (50019,50021))
      BEGIN
      
	   INSERT INTO [dbo].[CLM_ACTIVITY_CO_RI_BREAKDOWN]          
           (          
            [CLAIM_ID]                            
           ,[ACTIVITY_ID]          
           ,[RESERVE_ID]          
           ,[BREAKDOWN_ID]          
           ,[COMP_TYPE]          
           ,[COMP_ID]          
           ,[COMP_PERCENTAGE]          
           ,[RESERVE_AMT]          
           ,[TRAN_RESERVE_AMT]          
           ,[PAYMENT_AMT]   
           ,[RECOVERY_AMT]       
           ,[IS_ACTIVE]          
           ,[CREATED_BY]          
           ,[CREATED_DATETIME]          
           ,[MODIFIED_BY]          
           ,[LAST_UPDATED_DATETIME]      
           
           )               
           (   
           
           SELECT           
            C.CLAIM_ID          
           ,C.ACTIVITY_ID          
           ,RESERVE_ID          
           ,@BreakdownID+row_number() over(order by C.CLAIM_ID,RESERVE_ID asc)                      
           ,PARTY_TYPE         
           ,COMP_ID      --<COMP_ID, char(2),>          
           ,COMP_PERCENTAGE    --<COMP_PERCENTAGE, decimal(6,4),>          
           , ISNULL((
				       SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
				       WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
				             AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
								 (
									SELECT TOP 1 RESERVE_ID
									FROM  CLM_ACTIVITY_RESERVE CAR
									WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
									  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
								   )
				        ),0)  
            -(ISNULL(((C.RECOVERY_AMOUNT * @NetRIPercentage * COMP_PERCENTAGE)/100) ,0)) -- PREVIOUS RI RESERVE-RECOVERY AMOUNT
            
           ,-ISNULL(((RECOVERY_AMOUNT *@NetRIPercentage * COMP_PERCENTAGE)/100),0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
           , ISNULL(((RECOVERY_AMOUNT *@NetRIPercentage * COMP_PERCENTAGE)/100),0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
		   , 0        --<RECOVERY_AMOUNT, decimal(18,2),>       
           ,'Y'       --<IS_ACTIVE, char(1),>          
           ,C.CREATED_BY          
           ,C.CREATED_DATETIME          
           ,C.MODIFIED_BY          
           ,C.LAST_UPDATED_DATETIME       
           
            FROM CLM_ACTIVITY_RESERVE C  WITH(NOLOCK)  INNER JOIN          
                 #TEMP_REINSURANCE T ON C.CLAIM_ID=T.CLAIM_ID   INNER JOIN      
                  CLM_PRODUCT_COVERAGES P  WITH(NOLOCK)  ON P.CLAIM_ID=C.CLAIM_ID AND P.CLAIM_COV_ID=C.COVERAGE_ID      
            WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID 
                  AND P.RI_APPLIES =CASE WHEN @CLM_RI_APPLIES_FLG=1 THEN 'Y' ELSE P.RI_APPLIES END
           )   
     END 
     ELSE
     BEGIN
          INSERT INTO [dbo].[CLM_ACTIVITY_CO_RI_BREAKDOWN]          
           (          
            [CLAIM_ID]                            
           ,[ACTIVITY_ID]          
           ,[RESERVE_ID]          
           ,[BREAKDOWN_ID]          
           ,[COMP_TYPE]          
           ,[COMP_ID]          
           ,[COMP_PERCENTAGE]          
           ,[RESERVE_AMT]          
           ,[TRAN_RESERVE_AMT]          
           ,[PAYMENT_AMT]   
           ,[RECOVERY_AMT]       
           ,[IS_ACTIVE]          
           ,[CREATED_BY]          
           ,[CREATED_DATETIME]          
           ,[MODIFIED_BY]          
           ,[LAST_UPDATED_DATETIME] 
              
           )               
           (   
           
           SELECT           
            C.CLAIM_ID          
           ,C.ACTIVITY_ID          
           ,RESERVE_ID          
           ,@BreakdownID+row_number() over(order by C.CLAIM_ID,RESERVE_ID asc)                      
           ,PARTY_TYPE         
           ,COMP_ID      --<COMP_ID, char(2),>          
           ,COMP_PERCENTAGE    --<COMP_PERCENTAGE, decimal(6,4),>          
           ,
            CASE WHEN @ACTION_ON_PAYMENT =192 AND COMP_ID=@PAYEE_ID THEN 0 ELSE 
			   
			     ISNULL((
				       SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
				       WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
				             AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
								 (
									SELECT TOP 1 RESERVE_ID
									FROM  CLM_ACTIVITY_RESERVE CAR
									WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
									  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
								   )
				        ),0)  
			    END--<RESERVE_AMT, decimal(18,2),>       
			    
           --CASE WHEN @ACTION_ON_PAYMENT =192 AND COMP_ID=@PAYEE_ID THEN 0 ELSE RI_RESERVE END--<RESERVE_AMT, decimal(18,2),>          
           ,ISNULL(((OUTSTANDING_TRAN *@NetRIPercentage * COMP_PERCENTAGE)/100),0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
           ,ISNULL(((
               PAYMENT_AMOUNT * @NetRIPercentage *
                (CASE WHEN @COI_TRAN_TYPE=14924 THEN 0 ELSE COMP_PERCENTAGE END)
                )/100),0) --<PAYMENT_AMT, decimal(18,2),>          
            --- RECOVERY AMOUNT SHOULD BE COPIED FOR ONLY FOR PAYEE (REFER ITRACK:1263)         
		   ,CASE WHEN COMP_ID=@PAYEE_ID THEN -ISNULL(RECOVERY_AMOUNT ,0) ELSE 0 END --<RECOVERY_AMOUNT, decimal(18,2),>       
           ,'Y'       --<IS_ACTIVE, char(1),>          
           ,C.CREATED_BY          
           ,C.CREATED_DATETIME          
           ,C.MODIFIED_BY          
           ,C.LAST_UPDATED_DATETIME
             
            FROM CLM_ACTIVITY_RESERVE C  WITH(NOLOCK)  INNER JOIN          
                 #TEMP_REINSURANCE T ON C.CLAIM_ID=T.CLAIM_ID   INNER JOIN      
                  CLM_PRODUCT_COVERAGES P  WITH(NOLOCK)  ON P.CLAIM_ID=C.CLAIM_ID AND P.CLAIM_COV_ID=C.COVERAGE_ID      
            WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID 
                  AND P.RI_APPLIES =CASE WHEN @CLM_RI_APPLIES_FLG=1 THEN 'Y' ELSE P.RI_APPLIES END
           )   
           
     END -- END OF  IF(@PARTY_TYPE NOT IN(618,619) AND @COVEAGE_TYPE IN (50019,50021))
          
  END
  ELSE
  BEGIN
    
     INSERT INTO [dbo].[CLM_ACTIVITY_CO_RI_BREAKDOWN]          
           (          
            [CLAIM_ID]                            
           ,[ACTIVITY_ID]          
           ,[RESERVE_ID]          
           ,[BREAKDOWN_ID]          
           ,[COMP_TYPE]          
           ,[COMP_ID]          
           ,[COMP_PERCENTAGE]          
           ,[RESERVE_AMT]          
           ,[TRAN_RESERVE_AMT]          
           ,[PAYMENT_AMT]   
           ,[RECOVERY_AMT]       
           ,[IS_ACTIVE]          
           ,[CREATED_BY]          
           ,[CREATED_DATETIME]          
           ,[MODIFIED_BY]          
           ,[LAST_UPDATED_DATETIME]
               
           )               
           (   
           
           SELECT           
            C.CLAIM_ID          
           ,C.ACTIVITY_ID          
           ,RESERVE_ID          
           ,@BreakdownID+row_number() over(order by C.CLAIM_ID,RESERVE_ID asc)                      
           ,PARTY_TYPE         
           ,COMP_ID      --<COMP_ID, char(2),>          
           ,COMP_PERCENTAGE    --<COMP_PERCENTAGE, decimal(6,4),>          
           , CASE WHEN @ACTION_ON_PAYMENT =192 AND COMP_ID=@PAYEE_ID THEN 0 
			         WHEN @ACTION_ON_PAYMENT IN (165,166,168) THEN  ISNULL((COMP_PERCENTAGE*@NetRIPercentage*OUTSTANDING )/100,0) 
			         WHEN @ACTION_ON_PAYMENT IN (180,181)     THEN 
			            CASE WHEN @COI_TRAN_TYPE<> 14924 THEN
							 (
							   ISNULL((
							   SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
							   WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
									 AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
										 (
											SELECT TOP 1 RESERVE_ID
											FROM  CLM_ACTIVITY_RESERVE CAR
											WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
											  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
										   )
								),0)  
								- ISNULL((COMP_PERCENTAGE* @NetRIPercentage *PAYMENT_AMOUNT )/100,0)
								)
							 ELSE
							 
							   ISNULL((
							   SELECT TOP 1 BR.RESERVE_AMT FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] BR
							   WHERE BR.CLAIM_ID = @CLAIM_ID AND BR.ACTIVITY_ID = @PREVIOUS_ACTIVITY 
									 AND BR.COMP_ID=COMP_ID AND BR.COMP_TYPE=T.PARTY_TYPE AND  BR.RESERVE_ID IN
										 (
											SELECT TOP 1 RESERVE_ID
											FROM  CLM_ACTIVITY_RESERVE CAR
											WHERE CAR.CLAIM_ID= @CLAIM_ID AND CAR.ACTIVITY_ID=@PREVIOUS_ACTIVITY  AND 
											  CAR.COVERAGE_ID=C.COVERAGE_ID AND ISNULL(CAR.VICTIM_ID,0)=ISNULL(C.VICTIM_ID ,0)
										   )
								),0)  
								
							END
				 
			       
			         END--<RESERVE_AMT, decimal(18,2),>    
             
           --CASE WHEN @ACTION_ON_PAYMENT =192 AND COMP_ID=@PAYEE_ID THEN 0 ELSE ISNULL(((OUTSTANDING * @NetRIPercentage * COMP_PERCENTAGE)/100) ,0) END --<RESERVE_AMT, decimal(18,2),>          
           ,ISNULL(((OUTSTANDING_TRAN *@NetRIPercentage * COMP_PERCENTAGE)/100),0)--<TRAN_RESERVE_AMT, decimal(18,2),>          
           ,ISNULL(((
               PAYMENT_AMOUNT * @NetRIPercentage *
                (CASE WHEN @COI_TRAN_TYPE=14924 THEN 0 ELSE COMP_PERCENTAGE END)
                )/100),0) --<PAYMENT_AMT, decimal(18,2),>          
            --- RECOVERY AMOUNT SHOULD BE COPIED FOR ONLY FOR PAYEE (REFER ITRACK:1263)         
		   ,0 --<RECOVERY_AMOUNT, decimal(18,2),>       
           ,'Y'       --<IS_ACTIVE, char(1),>          
           ,C.CREATED_BY          
           ,C.CREATED_DATETIME          
           ,C.MODIFIED_BY          
           ,C.LAST_UPDATED_DATETIME   
           
            FROM CLM_ACTIVITY_RESERVE C  WITH(NOLOCK)  INNER JOIN          
                 #TEMP_REINSURANCE T ON C.CLAIM_ID=T.CLAIM_ID   INNER JOIN      
                  CLM_PRODUCT_COVERAGES P  WITH(NOLOCK)  ON P.CLAIM_ID=C.CLAIM_ID AND P.CLAIM_COV_ID=C.COVERAGE_ID      
            WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID 
                  AND P.RI_APPLIES =CASE WHEN @CLM_RI_APPLIES_FLG=1 THEN 'Y' ELSE P.RI_APPLIES END
           )   
           
    
  END    
  
  
 END       
         
 ------------------------------------------------      
 -- FOR UPDATE RI INSURANCE RELATED COLUMNS      
 ------------------------------------------------       
  UPDATE CLM_ACTIVITY_RESERVE       
  SET RI_RESERVE=T.RESERVE_AMT,      
   RI_RESERVE_TRAN=T.TRAN_RESERVE_AMT      
  FROM CLM_ACTIVITY_RESERVE C INNER JOIN      
   (       
   SELECT SUM(RESERVE_AMT)AS RESERVE_AMT, RESERVE_ID, SUM(TRAN_RESERVE_AMT) AS TRAN_RESERVE_AMT      
   FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] WITH(NOLOCK)     
   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND COMP_TYPE='RI'       
   GROUP BY RESERVE_ID      
   )T      
  ON C.RESERVE_ID=T.RESERVE_ID      
  WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID      
        
 --------------------------------------------------      
 --- FOR UPDATE CO INSURANCE RELATED COLUMNS      
 --------------------------------------------------         
        
  UPDATE CLM_ACTIVITY_RESERVE       
  SET CO_RESERVE=T.RESERVE_AMT,      
   CO_RESERVE_TRAN=T.TRAN_RESERVE_AMT      
  FROM CLM_ACTIVITY_RESERVE C INNER JOIN      
   (       
   SELECT SUM(RESERVE_AMT)AS RESERVE_AMT, RESERVE_ID, SUM(TRAN_RESERVE_AMT) AS TRAN_RESERVE_AMT      
   FROM [CLM_ACTIVITY_CO_RI_BREAKDOWN] WITH(NOLOCK)     
   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND COMP_TYPE='CO'      
   GROUP BY RESERVE_ID      
   )T      
  ON  C.RESERVE_ID=T.RESERVE_ID      
  WHERE C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID      
        
           
          
END              
                                                    
--go                  
--exec Proc_CalculateBreakdown 159,2          
             
--rollback tran                      
----                      
---- 



