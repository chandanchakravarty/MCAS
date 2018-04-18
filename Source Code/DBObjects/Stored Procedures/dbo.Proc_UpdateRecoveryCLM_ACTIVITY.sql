IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRecoveryCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRecoveryCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_UpdateRecoveryCLM_ACTIVITY                                
Created by      : Sumit Chhabra                        
Date            : 21/07/2006                                
Purpose        : To update recovery amount at clm_activity             
Revison History :                                
Used In  : Wolverine                                
            
------------------------------------------------------------                                
MODIFIED BY  : Asfa Praveen            
Date  : 14-Sept-2007            
Purpose  : Modify Activities result            
------------------------------------------------------------                                          
Date     Review By          Comments                                
Reviewed By : Anurag Verma            
Reviewed On : 16-07-2007       
  
MODIFIED BY  : Praveen kasana             
Date  : 12-March-2008            
Purpose  : Based on discussion with Rhonda ,  
   Activities Salavage (Check recd),   
  SUbrogation check recd   
  and Credit to Paid loss,   
  currently lower the resinurance reserve amount, that should not happen.       
  
  
MODIFIED BY  : Praveen kasana             
Date   : 13-March-2008            
Purpose   : Itrack #3585  
------   ------------       -------------------------*/                                
--DROP PROC dbo.Proc_UpdateRecoveryCLM_ACTIVITY                                
CREATE PROC [dbo].[Proc_UpdateRecoveryCLM_ACTIVITY]                                
(                                
 @CLAIM_ID     int,                                
 @ACTIVITY_ID  int            
)                                
AS                                
BEGIN                                          
declare @TOTAL_RECOVERY decimal(20,2)         
DECLARE @ACTION_ON_PAYMENT INT          
DECLARE @SALVAGE_CHECK_RECEIVED INT          
DECLARE @SUBROGATION_CHECK_RECEIVED INT          
DECLARE @LOSS_REINSURANCE_RECOVERED_PAID_LOSS INT       
DECLARE @OTHER_EXPENSE_REINSURANCE_RECOVERED INT       
DECLARE @LEGAL_EXPENSE_REINSURANCE_RECOVERED INT      
DECLARE @ADJUSTMENT_EXPENSE_REINSURANCE_RECOVERED INT       
DECLARE @SALVAGE_EXPENSE_REINSURANCE_RECOVERED INT             
DECLARE @SUBROGATION_EXPENSE_REINSURANCE_RECOVERED INT     
-- DECLARE @CREDIT_TO_PAID_LOSS INT    
-- ASFA (29-APR-2008)   
-- DECLARE @VOID_CREDIT_TO_PAID_LOSS INT    
DECLARE @OLD_CLAIM_RESERVE_AMOUNT DECIMAL(20,2)            
DECLARE @OLD_CLAIM_RI_RESERVE DECIMAL(20,2)   
DECLARE @OLD_CLAIM_PAYMENT_AMOUNT DECIMAL(20,2)   
DECLARE @CREDIT_TO_LEGAL_EXPENSE INT  
DECLARE @CREDIT_TO_OTHER_EXPENSE INT  
DECLARE @CREDIT_TO_ADJUSTER_EXPENSE INT  
DECLARE @VOID_CREDIT_TO_EXPENSE INT  
DECLARE @VOID_TO_ADJUSTER_EXPENSE INT      
DECLARE @VOID_TO_LEGAL_EXPENSE INT      
       
  
SET @CREDIT_TO_ADJUSTER_EXPENSE = 207  
SET @VOID_CREDIT_TO_EXPENSE = 241  
SET @CREDIT_TO_LEGAL_EXPENSE = 203  
SET @CREDIT_TO_OTHER_EXPENSE = 204
-- Modified Mohit Agarwal 25 Sep 2008 
SET @VOID_TO_ADJUSTER_EXPENSE = 254      
SET @VOID_TO_LEGAL_EXPENSE = 255      
SET @SALVAGE_CHECK_RECEIVED = 189        
SET @SUBROGATION_CHECK_RECEIVED = 190        
SET @LOSS_REINSURANCE_RECOVERED_PAID_LOSS  = 182        
SET @OTHER_EXPENSE_REINSURANCE_RECOVERED  = 183          
SET @LEGAL_EXPENSE_REINSURANCE_RECOVERED  = 184            
SET @ADJUSTMENT_EXPENSE_REINSURANCE_RECOVERED  = 185      
SET @SALVAGE_EXPENSE_REINSURANCE_RECOVERED  = 186      
SET @SUBROGATION_EXPENSE_REINSURANCE_RECOVERED  = 187      
-- SET @CREDIT_TO_PAID_LOSS  = 188  
-- ASFA (29-APR-2008)  
-- SET @VOID_CREDIT_TO_PAID_LOSS = 240  
SET @OLD_CLAIM_RESERVE_AMOUNT = 0             
SET @OLD_CLAIM_RI_RESERVE = 0     
      
        
        
--Calculate total recovery                                                          
 SELECT @TOTAL_RECOVERY = ISNULL(SUM(AMOUNT),0) FROM CLM_ACTIVITY_RECOVERY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                                                               




  
  
  
  
  
  
     
        
-- Commented by Asfa (14-Sept-2007) in order to correct Activities as per email sent by Gagan.       
-- UPDATE CLM_ACTIVITY SET CLAIM_RI_RESERVE=ISNULL(CLAIM_RI_RESERVE,0)-ISNULL(@TOTAL_RECOVERY,0), RECOVERY=ISNULL(@TOTAL_RECOVERY,0) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                    





   
  
  
  
  
  
   
  
 SELECT TOP 1 @OLD_CLAIM_RESERVE_AMOUNT = ISNULL(CLAIM_RESERVE_AMOUNT,0),@OLD_CLAIM_RI_RESERVE=ISNULL(CLAIM_RI_RESERVE,0),  
  @OLD_CLAIM_PAYMENT_AMOUNT = ISNULL(CLAIM_PAYMENT_AMOUNT,0)  
  FROM CLM_ACTIVITY WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID< @ACTIVITY_ID AND IS_ACTIVE ='Y'  
  ORDER BY ACTIVITY_ID DESC      
        
 SELECT @ACTION_ON_PAYMENT= ACTION_ON_PAYMENT FROM CLM_ACTIVITY WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID         
-- ASFA (29-APR-2008)   
-- @ACTION_ON_PAYMENT = @VOID_CREDIT_TO_PAID_LOSS OR @ACTION_ON_PAYMENT = @CREDIT_TO_PAID_LOSS OR   
IF (@ACTION_ON_PAYMENT = @SALVAGE_CHECK_RECEIVED OR   
 @ACTION_ON_PAYMENT = @SUBROGATION_CHECK_RECEIVED OR @ACTION_ON_PAYMENT = @CREDIT_TO_ADJUSTER_EXPENSE  OR @ACTION_ON_PAYMENT = @VOID_CREDIT_TO_EXPENSE OR   
 @ACTION_ON_PAYMENT = @CREDIT_TO_LEGAL_EXPENSE OR @ACTION_ON_PAYMENT = @CREDIT_TO_OTHER_EXPENSE
 OR @ACTION_ON_PAYMENT = @VOID_TO_ADJUSTER_EXPENSE OR @ACTION_ON_PAYMENT = @VOID_TO_LEGAL_EXPENSE)        
 BEGIN        
   SET @TOTAL_RECOVERY = -(@TOTAL_RECOVERY)        
 END  
  
IF(@ACTION_ON_PAYMENT = @CREDIT_TO_LEGAL_EXPENSE  OR @ACTION_ON_PAYMENT = @VOID_CREDIT_TO_EXPENSE OR @ACTION_ON_PAYMENT = @CREDIT_TO_ADJUSTER_EXPENSE OR @ACTION_ON_PAYMENT = @CREDIT_TO_OTHER_EXPENSE
	OR @ACTION_ON_PAYMENT = @VOID_TO_ADJUSTER_EXPENSE OR @ACTION_ON_PAYMENT = @VOID_TO_LEGAL_EXPENSE)  
BEGIN  
  UPDATE CLM_ACTIVITY SET EXPENSES = ISNULL(@TOTAL_RECOVERY,0)         
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  
END  
ELSE IF(@ACTION_ON_PAYMENT = @LOSS_REINSURANCE_RECOVERED_PAID_LOSS OR @ACTION_ON_PAYMENT = @SALVAGE_EXPENSE_REINSURANCE_RECOVERED OR @ACTION_ON_PAYMENT = @SUBROGATION_EXPENSE_REINSURANCE_RECOVERED)        
BEGIN      
 IF(@ACTION_ON_PAYMENT = @LOSS_REINSURANCE_RECOVERED_PAID_LOSS AND @OLD_CLAIM_RI_RESERVE != 0)  
 BEGIN  
select @OLD_CLAIM_RI_RESERVE - ISNULL(@TOTAL_RECOVERY,0)  
  UPDATE CLM_ACTIVITY   
 SET   
  LOSS_REINSURANCE_RECOVERED = ISNULL(@TOTAL_RECOVERY,0)  
        --By kasana   
  --CLAIM_RI_RESERVE= @OLD_CLAIM_RI_RESERVE - ISNULL(@TOTAL_RECOVERY,0)   
 --RI_RESERVE = ISNULL(@TOTAL_RECOVERY,0) --Added ForItrack Issue #6144 To handle Reinsurance Reserve.
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID       
 END  
 ELSE  
 BEGIN  
 UPDATE CLM_ACTIVITY SET LOSS_REINSURANCE_RECOVERED = ISNULL(@TOTAL_RECOVERY,0) 
   --RI_RESERVE = ISNULL(@TOTAL_RECOVERY,0) --Added ForItrack Issue #6144 To handle Reinsurance Reserve. 
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID       
 END  
END      
ELSE IF(@ACTION_ON_PAYMENT = @OTHER_EXPENSE_REINSURANCE_RECOVERED OR @ACTION_ON_PAYMENT = @LEGAL_EXPENSE_REINSURANCE_RECOVERED OR @ACTION_ON_PAYMENT = @ADJUSTMENT_EXPENSE_REINSURANCE_RECOVERED)      
BEGIN      
 UPDATE CLM_ACTIVITY SET EXPENSE_REINSURANCE_RECOVERED = ISNULL(@TOTAL_RECOVERY,0)         
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID        
END    
/*  
ELSE IF(@ACTION_ON_PAYMENT = @CREDIT_TO_PAID_LOSS)      
BEGIN      
 UPDATE CLM_ACTIVITY SET RECOVERY = ISNULL(-@TOTAL_RECOVERY,0), RESERVE_AMOUNT = ISNULL(@TOTAL_RECOVERY,0)    
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID        
END       
*/  
ELSE       
BEGIN      
    
  
 UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=ISNULL(@OLD_CLAIM_RESERVE_AMOUNT,0) + 
ISNULL(@TOTAL_RECOVERY,0) , RECOVERY=ISNULL(@TOTAL_RECOVERY,0)         
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                 
 IF(@OLD_CLAIM_RI_RESERVE != 0)     
 BEGIN  
 --Itrack 3585  
-- ASFA (29-APR-2008)   
-- ,@VOID_CREDIT_TO_PAID_LOSS,@CREDIT_TO_PAID_LOSS  
    IF(@ACTION_ON_PAYMENT NOT IN (@SALVAGE_CHECK_RECEIVED ,@SUBROGATION_CHECK_RECEIVED))  
 BEGIN  
  UPDATE CLM_ACTIVITY SET CLAIM_RI_RESERVE= @OLD_CLAIM_RI_RESERVE + ISNULL(@TOTAL_RECOVERY,0)  
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                               
   END   
 END  
 --UPDATE CLM_ACTIVITY SET CLAIM_RI_RESERVE=ISNULL(CLAIM_RI_RESERVE,0) + ISNULL(@TOTAL_RECOVERY,0), RECOVERY=ISNULL(@TOTAL_RECOVERY,0)         
 --WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                               
-- UPDATE CLM_CLAIM_INFO SET RECOVERIES=ISNULL(RECOVERIES,0) + ISNULL(@TOTAL_RECOVERY,0) WHERE CLAIM_ID=@CLAIM_ID       
END     
  
    
END  
  
  
  





GO

