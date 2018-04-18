IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CreateSystemGeneratedClaimActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CreateSystemGeneratedClaimActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
   
 /*----------------------------------------------------------                                                  
Proc Name             : Dbo.Proc_CreateSystemGeneratedClaimActivity                                                  
Created by            : Santosh Kumar Gautam                                                 
Date                  : 23 Dec 2010                                             
Purpose               : To void an activity     
Revison History       :                                                  
Used In               : claim module        
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc Proc_CreateSystemGeneratedClaimActivity                                
------   ------------       -------------------------*/                                                  
              
CREATE PROCEDURE [dbo].[Proc_CreateSystemGeneratedClaimActivity]   
      
 @CLAIM_ID              int,      
 @ACTIVITY_ID           int ,      
 @ACTIVITY_REASON       int,  
 @ACTION_ON_PAYMENT     int,  
 @VOID_ACTIVITY_ID      int =-1,    
 @FACTOR                DECIMAL(8,4)  =1,      
 @MODIFIED_BY           int,  
 @LAST_UPDATED_DATETIME     datetime  

   
               
AS                      
BEGIN               
         
 DECLARE  @VOID_ACTIVITY_REASON     int    
 DECLARE  @VOID_ACTION_ON_PAYMENT   int=0    
  
   
   ------------------------------------------------------------        
   -- GET DETAILS OF VOID ACTIVITY  
   ------------------------------------------------------------     
  IF(@VOID_ACTIVITY_ID>0)  
   BEGIN  
     
    SELECT @VOID_ACTIVITY_REASON=ACTIVITY_REASON,  
           @VOID_ACTION_ON_PAYMENT=ACTION_ON_PAYMENT      
    FROM   CLM_ACTIVITY WITH(NOLOCK)  
    WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@VOID_ACTIVITY_ID  
     
   END   
    
    
   ------------------------------------------------------------        
   -- COPY THE DATA OF LAST COMPLETED ACTIVITY    
   ------------------------------------------------------------     
   -- WE COPY THE DATA OF PREVIOUS ACTIVITY FOR PAYMENT OR RECOVERY ACTIVITY 
   -- BUT THIS PROC CAN BE CALLED FROM MANY PLACES INSTEAD OF ACTIVITY PAGE SO FOLLOWING CONDTION IS NECCESSARY OTHERWISE 
    IF NOT EXISTS( SELECT ACTIVITY_ID FROM CLM_ACTIVITY_RESERVE WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID)     
       EXEC [Proc_CopyReserveDetails] @CLAIM_ID,@ACTIVITY_ID,@FACTOR
        
        
   ------------------------------------------------------------        
   -- UPDATE THE VOID PARTIAL PAYMENT ACTIVITY      
   ----------------------------------------------------------     
       
   IF(@VOID_ACTIVITY_REASON=11775 AND @VOID_ACTION_ON_PAYMENT IN(180,181))        
    BEGIN        
		 UPDATE [CLM_ACTIVITY_RESERVE]                
		 SET          
		 [PAYMENT_AMOUNT]        = -1*T.PAYMENT_AMOUNT        -- TO NEGATE VOIDED ACTIVITY          
		,[OUTSTANDING_TRAN]      = -1*(-1*T.PAYMENT_AMOUNT)      -- OUTSTANDING_TRAN IS DIFFRENCE OF OUSTANDING AND PREV OUTSTANDING AMOUNT          
		,[OUTSTANDING]           = PREV_OUTSTANDING-(-1*T.PAYMENT_AMOUNT)       -- DEDUCT THE PAYMENT AMOUNT FOR PREVIOUS OUTSTANDING AMOUNT       
		,[TOTAL_PAYMENT_AMOUNT]  = [TOTAL_PAYMENT_AMOUNT]+(-1*T.PAYMENT_AMOUNT) -- ADD PAYMENT AMOUNT IN TOTAL PAYMENT AMOUNT   
		,[MODIFIED_BY]           = @MODIFIED_BY    
		,[LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME    
		 FROM CLM_ACTIVITY_RESERVE C INNER JOIN            
		 (             
		 SELECT PAYMENT_AMOUNT, ACTUAL_RISK_ID ,COVERAGE_ID    
		 FROM   CLM_ACTIVITY_RESERVE  WITH (NOLOCK)            
		 WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@VOID_ACTIVITY_ID     
		)T            
	   ON     C.COVERAGE_ID=T.COVERAGE_ID                      
	   WHERE (C.CLAIM_ID=@CLAIM_ID AND C.ACTIVITY_ID=@ACTIVITY_ID AND C.ACTUAL_RISK_ID=T.ACTUAL_RISK_ID )            
    END        
  
        
  -- ------------------------------------------------------------        
  -- -- UPDATE THE VOID RECOVERY ACTIVITY      
  -- ------------------------------------------------------------       
    IF(@VOID_ACTIVITY_REASON=11776)     
    BEGIN        
	   UPDATE [CLM_ACTIVITY_RESERVE]                
	   SET         
	      [RECOVERY_AMOUNT]       = (-1*T.[RECOVERY_AMOUNT])  
		 ,[TOTAL_RECOVERY_AMOUNT] = [TOTAL_RECOVERY_AMOUNT]+(-1*T.[RECOVERY_AMOUNT])      
		 ,[MODIFIED_BY]           = @MODIFIED_BY                
		 ,[LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME      
		 FROM CLM_ACTIVITY_RESERVE C  WITH (NOLOCK)  INNER JOIN            
		 (             
		SELECT [RECOVERY_AMOUNT], ACTUAL_RISK_ID ,COVERAGE_ID    
		FROM   CLM_ACTIVITY_RESERVE WITH (NOLOCK)           
		WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@VOID_ACTIVITY_ID     
	   )T        
	   ON   C.COVERAGE_ID=T.COVERAGE_ID                                  
       WHERE (C.CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND C.ACTUAL_RISK_ID=T.ACTUAL_RISK_ID  )            
    END        
       
     
   ------------------------------------------------------------        
   -- CALCULATE BREAKDOWN  
   ------------------------------------------------------------       
     EXEC [Proc_CalculateBreakdown] @CLAIM_ID,@ACTIVITY_ID
      
              
      
   ------------------------------------------------------------          
   -- COMPLETE ACTIVITY    
   ------------------------------------------------------------         
    EXEC [Proc_CompleteClaimActivities] 
     @CLAIM_ID            =@CLAIM_ID                 
	,@ACTIVITY_ID         =@ACTIVITY_ID    
	,@ACTIVITY_REASON     =@ACTIVITY_REASON              
	,@ACTION_ON_PAYMENT   =@ACTION_ON_PAYMENT
	,@IS_VOIDED_ACTIVITY  ='Y' 
	,@VOID_ACTIVITY_ID    =@VOID_ACTIVITY_ID
	,@COMPETED_DATE       =@LAST_UPDATED_DATETIME
	,@COMPETED_BY         =@MODIFIED_BY
	,@TEXT_ID             =NULL
	,@TEXT_DESCRIPTION    =NULL
	,@REASON_DESCRIPTION  =''

      
      
   ------------------------------------------------------------        
   -- UPDATE VOIDED ACTIVITY FLAGS  
   ------------------------------------------------------------       
    IF(@VOID_ACTIVITY_ID>0)  
   BEGIN  
     
     UPDATE CLM_ACTIVITY   
     SET IS_VOIDED_REVERSED_ACTIVITY=10963, -- FOR YES  
         VOIDED_DATE=@LAST_UPDATED_DATETIME  
     WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@VOID_ACTIVITY_ID       
     
   END  
          
END                      
    
    
    
  
  
GO

