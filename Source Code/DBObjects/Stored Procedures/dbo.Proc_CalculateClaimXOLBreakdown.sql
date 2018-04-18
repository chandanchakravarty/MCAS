IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CalculateClaimXOLBreakdown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CalculateClaimXOLBreakdown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*----------------------------------------------------------                                                        
Proc Name             : Dbo.Proc_CalculateClaimXOLBreakdown                                                        
Created by            : Santosh Kumar Gautam                                                       
Date                  : 01 April 2011                                                       
Purpose               : To calculate XOL breakdown                                                   
Revison History       :                                                        
Used In               : Claim                                            
------------------------------------------------------------                                                        
Date     Review By          Comments                           
                  
drop Proc Proc_CalculateClaimXOLBreakdown  710,3,'04/01/2011',406                                              
------   ------------       -------------------------*/                                                        
--                           
                            
--                         
                      
CREATE PROCEDURE [dbo].[Proc_CalculateClaimXOLBreakdown]                            
                             
 @CLAIM_ID            INT                    
,@ACTIVITY_ID         INT    
,@CREATED_DATE        DATETIME=NULL
,@CREATED_BY          INT     =NULL
,@IS_VOIDED_ACTIVITY  CHAR(1) ='N' 
,@VOID_ACTIVITY_ID    int =-1  


AS                            
BEGIN      

        SET @VOID_ACTIVITY_ID=ISNULL(@VOID_ACTIVITY_ID,0)
   
		DECLARE @ACTIVITY_PAYMENT_AMT     DECIMAL(18,2) =0
		DECLARE @TOTAL_PAYMENT_AMT        DECIMAL(18,2) =0
		DECLARE @TEMP_PAYMENT_AMT         DECIMAL(18,2) =0
		DECLARE @XOL_AGGREGATE_LIMIT      DECIMAL(18,2) =0
		DECLARE @XOL_USED_AGGREGATE_LIMIT DECIMAL(18,2) =0
		DECLARE @XOL_DEDUCTION_AMT		  DECIMAL(18,2) =0
		DECLARE @APPLIED_PAYMENT_AMT      DECIMAL(18,2) =0
		DECLARE @USED_CLAIM_ID            NVARCHAR(1024) =''

		DECLARE @CONTRACT_ID			INT	   =0 
		DECLARE @LOB_ID               	INT	   =0 
		DECLARE @BREAKDOWN_ID           INT	   =0
		DECLARE @ROW_COUNT              INT	   =0
		DECLARE @TEMP_CLAIM_ID          INT	   =0
		DECLARE @TEMP_ACTIVITY_ID       INT	   =0    
		DECLARE @XOL_ADDED_CLAIM_COUNT  INT    =0
		DECLARE @IS_CLAIM_CAL_DONE	    CHAR(1)=0
		DECLARE @XOL_CLAIM_LIMIT		INT    =0

  ---============================================================================================================================
  -- XOL SCREEN HAS MINIMUM NUMBERS OF CLAIM IF CURRENT CLAIM ( IS GREATER OF EQUAL THEN ONLY FOLLOWING CALCULATION WILL BE DONE
  -- TO REDUCE THE AGGREGATE LIMIT OF XOL
  -- AGGREGATE LIMIT DECREASED BY TOTAL PAYMENT OF CURRENT ACTIVITY
  ---============================================================================================================================
      
      
     
      
       -- GET MIN LIMIT OF CLAIM AND IS_CLAIM_CAL_DONE FLAG (Y=YES, N=NO)
	   SELECT @CONTRACT_ID				= CATASTROPHE_EVENT_CODE,
	          @LOB_ID					= C.LOB_ID,
	          @XOL_CLAIM_LIMIT			= ISNULL(MIN_CLAIM_LIMIT,0), 
	          @IS_CLAIM_CAL_DONE		= ISNULL(IS_CLAIM_CAL_DONE,'N'),
	          @XOL_AGGREGATE_LIMIT      = AGGREGATE_LIMIT,
	          @XOL_USED_AGGREGATE_LIMIT = USED_AGGREGATE_LIMIT,
	          @XOL_DEDUCTION_AMT		= LOSS_DEDUCTION	          
	   FROM CLM_CLAIM_INFO C INNER JOIN
	        MNT_XOL_INFORMATION M ON C.CATASTROPHE_EVENT_CODE=M.CONTRACT_ID AND C.LOB_ID=M.LOB_ID
	   WHERE CLAIM_ID=@CLAIM_ID AND M.IS_ACTIVE='Y'
	  
	   IF (@CONTRACT_ID IS NOT NULL AND @CONTRACT_ID>0)
	   BEGIN	  
	         -- GET TOTAL CLAIMS ADDED FOR THIS CONTRACT
	         SELECT @XOL_ADDED_CLAIM_COUNT=COUNT(CLAIM_ID)
	   	  	 FROM CLM_CLAIM_INFO 
	   	  	 WHERE CATASTROPHE_EVENT_CODE=@CONTRACT_ID AND IS_ACTIVE='Y'    
	  
	   	  -----------------------------------------------------------------------------------------------------------
	   	  --- WHEN XOL CALCULATION BEING CALCULATE FIRST TIME
	   	  -----------------------------------------------------------------------------------------------------------
	   	  IF(@IS_CLAIM_CAL_DONE='N')
	   	  BEGIN	   	  
	   	   
	   	   SET @IS_CLAIM_CAL_DONE='Y'
	   	   
	   	     ---------------------------------------------------------------------------------------
	   	     --- IF ADDED CLAIM ARE LESS THEN THE DEFINED CLAIM NUMBER THEN NO ACTION WILL PERFORMED
	   	     ---------------------------------------------------------------------------------------
	   	     IF(@XOL_ADDED_CLAIM_COUNT<@XOL_CLAIM_LIMIT)
	   	        RETURN
	   	     
	   	       
	   	   
	   			 CREATE TABLE #TEMP_TABLE
	   			 (
					ROW_ID INT IDENTITY,
					CLAIM_ID INT,
					ACTIVITY_ID INT,
					ACT_PAYMENT_AMT DECIMAL(18,2)DEFAULT 0
				  )
	   	  
	   	       -- THEN ADD THE TOTAL PAYMENT AMOUNT OF ALL THE CLAIM FOR THIS XOL
			   -- GET TOTAL PAYMENT OF ALL CLAIM FOR THIS XOL
			   -- LAST ACTIVITY CONTAINS TOTAL PAYMENT AMOUNT
	           -- SELECT CLAIM AND ACTIVITY ID ALONG TOTAL PAYMENT OF THAT ACTIVITY
				 INSERT INTO #TEMP_TABLE
							 (
							   CLAIM_ID,
							   ACTIVITY_ID,
							   ACT_PAYMENT_AMT   					  
							 )
							 (
									 SELECT T.CLAIM_ID, T.ACTIVITY_ID,SUM(TOTAL_PAYMENT_AMOUNT)
									 FROM CLM_ACTIVITY_RESERVE S INNER JOIN 
									  (														  
											  SELECT MAX(A.ACTIVITY_ID) AS ACTIVITY_ID ,A.CLAIM_ID    
											  FROM     CLM_ACTIVITY A 
											  WHERE    ACTIVITY_STATUS=11801 AND -- FOR LAST COMPLETED ACTIVITY
													   CLAIM_ID IN (SELECT CLAIM_ID FROM CLM_CLAIM_INFO WHERE CATASTROPHE_EVENT_CODE=@CONTRACT_ID AND IS_ACTIVE='Y')-- TO GET ALL CLAIM FOR THIS XOL
											  GROUP BY CLAIM_ID  
									  ) T
									  ON T.CLAIM_ID=S.CLAIM_ID AND T.ACTIVITY_ID=S.ACTIVITY_ID							  
									  GROUP BY T.CLAIM_ID, T.ACTIVITY_ID
							 )
							
	   					 -- GET TOTAL RECORD FROM TEMP TABLE
	   					 SELECT @ROW_COUNT=COUNT(ROW_ID) FROM #TEMP_TABLE
	   						   					
	   					 WHILE (@ROW_COUNT>0)
	   					 BEGIN
	   					 
	   					    --- FETCH CLAIM, ACTIVITY_ID AND ACTIVITY PAYMENT AMOUNT
	   					    SELECT @TEMP_CLAIM_ID    = CLAIM_ID, 
	   					           @TEMP_ACTIVITY_ID = ACTIVITY_ID,
	   					           @TEMP_PAYMENT_AMT = ACT_PAYMENT_AMT
	   					    FROM   #TEMP_TABLE
	   					    WHERE  (ROW_ID=@ROW_COUNT)
	   					    
	   					    --- CALCULATE TOTAL PAYMENT AMOUNT
	   					    SET @TOTAL_PAYMENT_AMT   += @TEMP_PAYMENT_AMT
	   					    SET @USED_CLAIM_ID        = CAST(@TEMP_CLAIM_ID AS NVARCHAR(50))+','+@USED_CLAIM_ID 
	   					    
	   					    --- IF CURRENT ACTIVITY ID IS EQUAL TO TEMP ACTIVITY ID THEN SET @
	   					    IF(@CLAIM_ID=@CLAIM_ID AND @ACTIVITY_ID=@TEMP_ACTIVITY_ID)
	   					       SET @ACTIVITY_PAYMENT_AMT=@TEMP_PAYMENT_AMT
	   					       
	   					    SET @ROW_COUNT=@ROW_COUNT-1
	   					 
	   					 END	   					 
	   			
	   			
	   			       --- IF TOTAL PAYMENT AMOUNT IS LESS THEN LOSS DEDUCTION AMOUNT THEN NO NEED TO INSERT
	   					 IF(@XOL_DEDUCTION_AMT>=@TOTAL_PAYMENT_AMT)
	   					   RETURN
	   					 ELSE
	   					   SET @APPLIED_PAYMENT_AMT=@TOTAL_PAYMENT_AMT-@XOL_DEDUCTION_AMT -- CALCULATE ACTUAL AMOUNT TO APPLIED
	   	  END	
	   	  ELSE
	   	  -----------------------------------------------------------------------------------------------------------
	   	  --- WHEN XOL CALCULATION IS NOT CALCULATING FIRST TIME
	   	  -----------------------------------------------------------------------------------------------------------
	   	  BEGIN
	   	  
	   	      --- APPLIED LOSS DEDUCTION CREATE ONLY FIRST TIME SO HERE ITS VALUE SET TO ZERO
	   	      SET @XOL_DEDUCTION_AMT=0
	   	      
	   	      -- GET TOTAL AMOUNT FOR THIS ACTIVITY ONLY
	   	      -- VOID ACTIVITY IS PROVIDED THEN GET THE TOTAL OF VOIDED ACTIVITY ELSE TOTAL OF NORMAL ACTIVITY
	   	      SELECT @TOTAL_PAYMENT_AMT = SUM(PAYMENT_AMOUNT)
			  FROM   CLM_ACTIVITY_RESERVE 
			  WHERE  CLAIM_ID=@CLAIM_ID AND 
			        ACTIVITY_ID= CASE WHEN @VOID_ACTIVITY_ID>0 THEN @VOID_ACTIVITY_ID ELSE @ACTIVITY_ID END		
			  
			  -- HERE ACTIVITY AMOUNT WOULD BE EQUAL TO TOTAL PAYMENT AMOUNT 
			  SET @ACTIVITY_PAYMENT_AMT = @TOTAL_PAYMENT_AMT
			  			  
			  -- HERE APPLIED PAYMENT AMOUNT WOULD BE EQUAL TO TOTAL PAYMENT AMOUNT 
			  SET @APPLIED_PAYMENT_AMT = @TOTAL_PAYMENT_AMT
			  
			  SET @USED_CLAIM_ID=CAST(@CLAIM_ID AS NVARCHAR(50))
	   	    	   	  
	   	     -- CALCULATION IS AREADY DONE SO ITS VALUE SET TO YES
	   	      SET @IS_CLAIM_CAL_DONE='Y'
	   	      
	   	  END
	   	  		   
	   		
	   		--------------------------------------------------------------------------------------
	   		--- IF VOID ACTIVITY IS PROVIDED IT MEANS WE NEED TO REVERT ENTRIES FOR THIS ACTIVITY
	   		--------------------------------------------------------------------------------------
	   		IF(@VOID_ACTIVITY_ID>0)
	   		   BEGIN
	   		   
	   		    SET @TOTAL_PAYMENT_AMT    = -1*@TOTAL_PAYMENT_AMT
	   		    SET @APPLIED_PAYMENT_AMT  = -1*@APPLIED_PAYMENT_AMT
	   		    SET @ACTIVITY_PAYMENT_AMT = -1*@ACTIVITY_PAYMENT_AMT
	   		    
	   		   END
	   		
	   		       SELECT @BREAKDOWN_ID=(ISNULL(MAX(BREAKDOWN_ID),0)+1)  FROM [dbo].[CLM_XOL_BREAKDOWN]              

	   			 -- INSERT RECORD INTO CLM_XOL_BREAKDOWN
	   			    INSERT INTO [CLM_XOL_BREAKDOWN]
	   			    (
						 [CLAIM_ID]				           
						,[ACTIVITY_ID]   		  
						,[BREAKDOWN_ID]  		  
						,[CONTRACT_ID]   		  
						,[XOL_AGGREGATE_LIMIT]	  
						,[XOL_USED_AGGREGATE_LIMIT]
						,[APPLIED_LOSS_DEDUCTION]  
						,[TOTAL_PAYMENT_AMT]       
						,[APPLIED_PAYMENT_AMT]	  
						,[ACTIVITY_PAYMENT_AMT]    
						,[USED_CLAIM_ID]            
						,[IS_ACTIVE]				  
						,[CREATED_BY]			  
						,[CREATED_DATETIME]		  						
	   			    )	 
	   			    VALUES
	   			    (
	   				     @CLAIM_ID
						,@ACTIVITY_ID   		 
						,@BREAKDOWN_ID  		  
						,@CONTRACT_ID
						,@XOL_AGGREGATE_LIMIT
						,@XOL_USED_AGGREGATE_LIMIT						
						,@XOL_DEDUCTION_AMT      -- [APPLIED_LOSS_DEDUCTION]
						,@TOTAL_PAYMENT_AMT      -- [TOTAL_PAYMENT_AMT]  
						,@APPLIED_PAYMENT_AMT
						,@ACTIVITY_PAYMENT_AMT   -- [ACTIVITY_PAYMENT_AMT] 
						,@USED_CLAIM_ID           
						,'Y'				  
						,@CREATED_BY			  
						,@CREATED_DATE		  
							
	   			    )
	   				   
	   	  	 
              		
	   		  -- UPDATE XOL INFORMATION
	   	      UPDATE MNT_XOL_INFORMATION 
			  SET    USED_AGGREGATE_LIMIT = ISNULL(USED_AGGREGATE_LIMIT,0)+@APPLIED_PAYMENT_AMT,
				     IS_CLAIM_CAL_DONE    = @IS_CLAIM_CAL_DONE
			  WHERE  LOB_ID=@LOB_ID AND CONTRACT_ID=@CONTRACT_ID
	   
	   	  END
	   	  
	   	  
	END
GO

