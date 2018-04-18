IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyReserveDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyReserveDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                
Proc Name             : Dbo.Proc_CopyReserveDetails                                                                
Created by            : Santosh Kumar Gautam                                                               
Date                  : 09 Nov 2010                                                               
Purpose               : TO copy records of last completed activity to a new activity                                                           
Revison History       :                                                                
Used In               : CLAIM                                                             
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc Proc_CopyReserveDetails                  
------   ------------       -------------------------*/                                                                
--                                   
                               
CREATE PROCEDURE [dbo].[Proc_CopyReserveDetails]                                    
                                     
 @CLAIM_ID            INT                            
,@ACTIVITY_ID         INT                          
,@FACTOR              DECIMAL(8,4)  =1     
AS                                    
BEGIN  

 -- GET PRVIOUS CLAIM TYPE COMPLETED ACTIVITY ID                   
        DECLARE @TEMP_ACTIVITY_ID INT =0                  
        DECLARE @MAX_RESERVE_ID INT =0                  
        DECLARE @MAX_TRANSACTION_ID INT =0         
        DECLARE @HAS_PAYMENT_ACTIVITY INT =0                
                          
         SELECT TOP 1 @TEMP_ACTIVITY_ID=ACTIVITY_ID                    
         FROM  CLM_ACTIVITY WITH(NOLOCK)                    
         WHERE (ACTIVITY_STATUS=11801 -- COMPLETED ACTIVITY                    
                AND CLAIM_ID=@CLAIM_ID                     
                AND IS_ACTIVE='Y'         
               )                     
        ORDER BY ACTIVITY_ID DESC                    
                       
                   
         SELECT @MAX_RESERVE_ID =MAX([RESERVE_ID]),@MAX_TRANSACTION_ID=ISNULL(MAX(TRANSACTION_ID),1) FROM [CLM_ACTIVITY_RESERVE] WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID              
      
      ------------------------------------------------------------------------------------------------
      -- ADDED BY SANTOSH GAUTAM ON 06 APRIL 2011 FOR ITRACK : 977
      -- DEDUCTICLE AMOUNT SHOULD BE COPY FOR FIRST PAYMENT ONLY. IF FIRST PAYMENT(PARTIAL OR FULL)
      -- COMPLETED THEN NO NEED TO COPY DEDUCTIBLE AMOUNT FURTHER.
      ------------------------------------------------------------------------------------------------     
         SELECT @HAS_PAYMENT_ACTIVITY=ISNULL(CLAIM_ID,0)
         FROM CLM_ACTIVITY 
         WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' 
               AND ACTION_ON_PAYMENT IN(180,181) -- FOR PAYMENT ACTIVITY
               AND ACTIVITY_STATUS=11801
           
                   
     -- COPY THE RECORD OF PRVIOUS CLAIM TYPE COMPLETED ACTIVITY                  
     -- WITH [OUTSTANDING_TRAN]=0, [RI_RESERVE_TRAN]=0, [CO_RESERVE_TRAN]=0                  
        INSERT INTO [dbo].[CLM_ACTIVITY_RESERVE]                 
       (                                   
		   [CLAIM_ID]                             
		  ,[RESERVE_ID]                    
		  ,[ACTUAL_RISK_ID]                    
		  ,[ACTIVITY_ID]                    
		  ,[COVERAGE_ID]                       
		  ,[OUTSTANDING]          
		  ,[PREV_OUTSTANDING]          
		  ,[TOTAL_PAYMENT_AMOUNT]          
		  ,[TOTAL_RECOVERY_AMOUNT]          
		  ,[OUTSTANDING_TRAN]           
		  ,[PAYMENT_AMOUNT]          
		  ,[RECOVERY_AMOUNT]          
		  ,[TRANSACTION_ID]              
		  ,[IS_ACTIVE]                    
		  ,[CREATED_BY]                    
		  ,[CREATED_DATETIME]  
		  ,[DEDUCTIBLE_1]   
		  --,[ADJUSTED_AMOUNT] 
		  ,PERSONAL_INJURY
		  ,VICTIM_ID                              
                               
      )                        
      (                
		   SELECT                  
		   CLAIM_ID                    
		  ,@MAX_RESERVE_ID+row_number() over(order by CLAIM_ID,RESERVE_ID asc)                     
		  ,ACTUAL_RISK_ID                    
		  ,@ACTIVITY_ID   -- NEW ACTIVITY ID            
		  ,COVERAGE_ID                               
		  ,OUTSTANDING * @FACTOR             
		  ,OUTSTANDING    --PREV_OUTSTANDING          
		  ,[TOTAL_PAYMENT_AMOUNT]          
		  ,[TOTAL_RECOVERY_AMOUNT]          
		  ,(OUTSTANDING* @FACTOR)-OUTSTANDING  --[OUTSTANDING_TRAN]        
		  ,0 --[PAYMENT_AMOUNT]           
		  ,0 --[RECOVERY_AMOUNT]           
		  ,@MAX_TRANSACTION_ID+row_number() over(order by CLAIM_ID,RESERVE_ID asc)  -- [TRANSACTION_ID]          
		  ,'Y'               
		  ,CREATED_BY                    
		  ,CREATED_DATETIME 
		  ,CASE WHEN @HAS_PAYMENT_ACTIVITY>0 THEN 0 ELSE [DEDUCTIBLE_1]  END ---[DEDUCTIBLE_1]
		  --,[ADJUSTED_AMOUNT] 
		  ,PERSONAL_INJURY
		  ,VICTIM_ID                    
		FROM [CLM_ACTIVITY_RESERVE]  WITH(NOLOCK)                 
		WHERE (CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@TEMP_ACTIVITY_ID AND IS_ACTIVE='Y')    
    )              
  
  END                         
GO

