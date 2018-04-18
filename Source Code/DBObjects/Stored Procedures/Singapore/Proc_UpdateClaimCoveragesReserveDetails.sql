---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
      
 /*----------------------------------------------------------                                                                        
Proc Name             : Dbo.Proc_UpdateClaimCoveragesReserveDetails                                                                        
Created by            : Santosh Kumar Gautam                                                                       
Date                  : 01 Dec 2010                                                                       
Purpose               : To insert the claim reserve details                              
Revison History       :                                                                        
Used In               : claim module                              
------------------------------------------------------------                                                                        
Date     Review By          Comments                                           
                                  
drop Proc Proc_UpdateClaimCoveragesReserveDetails               
exec Proc_UpdateClaimCoveragesReserveDetails 12,2,1,151,1543,1.0,0,0,406,'2010-12-02 19:46:41.020'                                                          
------   ------------       -------------------------*/                                                                        
--                                          
          
Alter PROCEDURE [dbo].[Proc_UpdateClaimCoveragesReserveDetails]                                            
               
 @RESERVE_ID int output              
,@ACTIVITY_ID int                
,@RISK_ID int                            
,@CLAIM_ID int              
,@COVERAGE_ID int              
,@ACTIVITY_TYPE int =11836      
,@ACTION_ON_PAYMENT int=0  
,@OUTSTANDING decimal(18,2)              
,@RI_RESERVE decimal(18,2)              
,@CO_RESERVE decimal(18,2)              
,@PAYMENT_AMOUNT  decimal(18,2)    
,@RECOVERY_AMOUNT  decimal(18,2)  
,@DEDUCTIBLE_1 decimal(18,2)    
,@ADJUSTED_AMOUNT decimal(18,2)    
,@PERSONAL_INJURY char(1)      
,@MODIFIED_BY int              
,@LAST_UPDATED_DATETIME datetime              
                                                                                           
                                            
AS                                            
BEGIN                                 
                
    -- FOR RESEARVE TYPE      
    IF(@ACTIVITY_TYPE IN(11836,11773,0) )      
    BEGIN          
  UPDATE [dbo].[CLM_ACTIVITY_RESERVE]              
  SET                    
   [OUTSTANDING] = @OUTSTANDING                        
  ,[OUTSTANDING_TRAN]=(@OUTSTANDING-PREV_OUTSTANDING)            
  ,[MODIFIED_BY] = @MODIFIED_BY              
  ,[LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME                   
  WHERE (CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND RESERVE_ID=@RESERVE_ID AND ACTUAL_RISK_ID=@RISK_ID AND COVERAGE_ID=@COVERAGE_ID)              
  END      
          
    -- FOR PARTIAL PAYMENT ACTIVITY      
    IF(@ACTIVITY_TYPE=11775 AND @ACTION_ON_PAYMENT=180)      
    BEGIN      
     UPDATE [dbo].[CLM_ACTIVITY_RESERVE]              
     SET        
     [DEDUCTIBLE_1]       = @DEDUCTIBLE_1         
    ,[ADJUSTED_AMOUNT]    = @ADJUSTED_AMOUNT         
    ,[PAYMENT_AMOUNT]     = @PAYMENT_AMOUNT          
    ,[OUTSTANDING_TRAN]   =(@PAYMENT_AMOUNT*-1)         
    ,[OUTSTANDING]        = PREV_OUTSTANDING-@PAYMENT_AMOUNT      
    ,[TOTAL_PAYMENT_AMOUNT]  =([TOTAL_PAYMENT_AMOUNT]-PAYMENT_AMOUNT)+@PAYMENT_AMOUNT      
    ,[MODIFIED_BY]           = @MODIFIED_BY              
       ,[LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME    
       ,[PERSONAL_INJURY]       = @PERSONAL_INJURY           
       WHERE (CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND RESERVE_ID=@RESERVE_ID AND ACTUAL_RISK_ID=@RISK_ID AND COVERAGE_ID=@COVERAGE_ID)          
    END      
      
      
     -- FOR FULL PAYMENT ACTIVITY      
    IF(@ACTIVITY_TYPE=11775 AND @ACTION_ON_PAYMENT=181)      
    BEGIN      
     UPDATE [dbo].[CLM_ACTIVITY_RESERVE]              
     SET        
        [DEDUCTIBLE_1]       = @DEDUCTIBLE_1         
    ,[ADJUSTED_AMOUNT]    = @ADJUSTED_AMOUNT    
    ,[PAYMENT_AMOUNT]     = @PAYMENT_AMOUNT          
    ,[OUTSTANDING_TRAN]   = CASE  WHEN  @PAYMENT_AMOUNT=0.00 THEN 0  ELSE  PREV_OUTSTANDING*-1 END   
    ,[OUTSTANDING]        = CASE  WHEN  @PAYMENT_AMOUNT=0.00 THEN PREV_OUTSTANDING ELSE 0  END  
    ,[TOTAL_PAYMENT_AMOUNT]  =([TOTAL_PAYMENT_AMOUNT]-PAYMENT_AMOUNT)+@PAYMENT_AMOUNT      
    ,[MODIFIED_BY]           = @MODIFIED_BY              
       ,[LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME      
       ,[PERSONAL_INJURY]       = @PERSONAL_INJURY                     
       WHERE (CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND RESERVE_ID=@RESERVE_ID AND ACTUAL_RISK_ID=@RISK_ID AND COVERAGE_ID=@COVERAGE_ID)          
    END      
      
      -- FOR RECOVERY ACTIVITY      
    IF(@ACTIVITY_TYPE=11776)   
    BEGIN      
     UPDATE [dbo].[CLM_ACTIVITY_RESERVE]              
     SET       
        [RECOVERY_AMOUNT] =@RECOVERY_AMOUNT  
       ,[TOTAL_RECOVERY_AMOUNT]=([TOTAL_RECOVERY_AMOUNT]-[RECOVERY_AMOUNT])+@RECOVERY_AMOUNT    
    ,[MODIFIED_BY]    = @MODIFIED_BY              
       ,[LAST_UPDATED_DATETIME]= @LAST_UPDATED_DATETIME                   
       WHERE (CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND RESERVE_ID=@RESERVE_ID AND ACTUAL_RISK_ID=@RISK_ID AND COVERAGE_ID=@COVERAGE_ID)          
    END      
                                
END   
  