IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateReserveCLM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateReserveCLM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_UpdateReserveCLM_ACTIVITY                                      
Created by      : Sumit Chhabra                              
Date            : 06/07/2006                                      
Purpose       : To update reserve amount at clm_activity with differnce of sum of payments and last recorded reserve amount                              
Revison History :                                      
Used In  : Wolverine                                      
------------------------------------------------------------                                      
MODIFIED BY  : Asfa Praveen                  
Date  : 13-Sept-2007                  
Purpose  : Modify Activities result.                  
------------------------------------------------------------                        
Date     Review By          Comments                                      
------   ------------       -------------------------*/                   
--DROP PROC dbo.Proc_UpdateReserveCLM_ACTIVITY                                                         
CREATE PROC [dbo].[Proc_UpdateReserveCLM_ACTIVITY]                                      
(                                      
 @CLAIM_ID     int,                                      
 @ACTIVITY_ID  int,                          
 @PAYMENT_ACTION int                          
-- @PAYEE_PARTIES_ID varchar(200)                              
)                                      
AS                                      
BEGIN                                                
      
DECLARE @CREDIT_TO_PAID_LOSS INT                                  
DECLARE @VOID_CREDIT_TO_PAID_LOSS INT                                  
declare @PAYMENT_AMOUNT decimal(20,2)                     
DECLARE @CLAIM_PAYMENT_AMOUNT DECIMAL(20,2)                
DECLARE @ADJUSTMENT_EXPENSE int               
DECLARE @SUBROGATION_EXPENSE int               
DECLARE @OTHER_EXPENSE int            
DECLARE @LEGAL_EXPENSE INT            
DECLARE @SALVAGE_EXPENSE INT      
      
DECLARE @CREDIT_TO_SUBROGATION_EXPENSE INT      
DECLARE @VOID_CREDIT_TO_SUBROGATION_EXPENSE INT      
DECLARE @CREDIT_TO_SALVAGE_EXPENSE INT      
DECLARE @VOID_CREDIT_TO_SALVAGE_EXPENSE INT      
         
DECLARE @SALVAGE_REINSURANCE_RETURNED INT         
DECLARE @SUBROGATION_REINSURANCE_RETURNED INT       
DECLARE @OLD_CLAIM_RESERVE_AMOUNT DECIMAL(20,2)                
DECLARE @OLD_CLAIM_RI_RESERVE DECIMAL(20,2)          
                    
DECLARE @PAID_LOSS_PARTIAL int                      
DECLARE @PAID_LOSS_FINAL int                      
                    
DECLARE @REINSURANCE_RETURNED_ADJUSTER_EXPENSE INT      
DECLARE @REINSURANCE_RETURNED_LEGAL_EXPENSE INT      
DECLARE @REINSURANCE_RETURNED_OTHER_EXPENSE INT      
      
DECLARE @VOID_TO_REINSURANCE_RETURNED_ADJUSTER_EXPENSE INT      
DECLARE @VOID_TO_REINSURANCE_RETURNED_OTHER_EXPENSE INT      
DECLARE @VOID_TO_REINSURANCE_RETURNED_LEGAL_EXPENSE INT      
DECLARE @VOID_TO_REINSURANCE_RETURNED_PAID_LOSS INT      
DECLARE @VOID_TO_REINSURANCE_RETURNED_SALVAGE_SUB_EXPENSE INT      
      
      
SET @CREDIT_TO_PAID_LOSS = 188      
SET @VOID_CREDIT_TO_PAID_LOSS = 240      
SET @PAID_LOSS_PARTIAL = 180                      
SET @PAID_LOSS_FINAL = 181                 
SET @ADJUSTMENT_EXPENSE = 175                     
SET @SUBROGATION_EXPENSE = 177             
SET @OTHER_EXPENSE = 173            
SET @LEGAL_EXPENSE = 174            
SET @SALVAGE_EXPENSE = 176       
      
SET @CREDIT_TO_SUBROGATION_EXPENSE = 242      
SET @VOID_CREDIT_TO_SUBROGATION_EXPENSE = 243      
SET @CREDIT_TO_SALVAGE_EXPENSE = 244      
SET @VOID_CREDIT_TO_SALVAGE_EXPENSE = 245      
        
SET @SALVAGE_REINSURANCE_RETURNED  = 178        
SET @REINSURANCE_RETURNED_ADJUSTER_EXPENSE = 246        
SET @REINSURANCE_RETURNED_LEGAL_EXPENSE = 247        
SET @REINSURANCE_RETURNED_OTHER_EXPENSE = 248     
--@SUBROGATION_REINSURANCE_RETURNED Will BE Treated as a REINSURANCE RETURNED PAID LOSS
SET @SUBROGATION_REINSURANCE_RETURNED  = 179       
      
SET @VOID_TO_REINSURANCE_RETURNED_ADJUSTER_EXPENSE = 249      
SET @VOID_TO_REINSURANCE_RETURNED_OTHER_EXPENSE = 250      
SET @VOID_TO_REINSURANCE_RETURNED_LEGAL_EXPENSE = 251      
SET @VOID_TO_REINSURANCE_RETURNED_PAID_LOSS = 252      
SET @VOID_TO_REINSURANCE_RETURNED_SALVAGE_SUB_EXPENSE = 253      
      
SET @OLD_CLAIM_RESERVE_AMOUNT = 0                 
SET @OLD_CLAIM_RI_RESERVE = 0         
              
  SELECT TOP 1 @OLD_CLAIM_RESERVE_AMOUNT = ISNULL(CLAIM_RESERVE_AMOUNT,0),@OLD_CLAIM_RI_RESERVE=ISNULL(CLAIM_RI_RESERVE,0)      
  FROM CLM_ACTIVITY WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID< @ACTIVITY_ID AND IS_ACTIVE ='Y'      
  ORDER BY ACTIVITY_ID DESC                     
                          
 --find sum of payments recorded against the current claim and activity                              
 SELECT @PAYMENT_AMOUNT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
                
-- SELECT @CLAIM_PAYMENT_AMOUNT = ISNULL(CLAIM_PAYMENT_AMOUNT,0) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
 SELECT TOP 1 @CLAIM_PAYMENT_AMOUNT = ISNULL(CLAIM_PAYMENT_AMOUNT,0) FROM CLM_ACTIVITY       
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID< @ACTIVITY_ID AND IS_ACTIVE ='Y'      
 ORDER BY ACTIVITY_ID DESC                   
                  
                  
-- Commented by Asfa (13-Sept-2007) in order to correct Activities as per email sent by Gagan.                  
/*                           
if (@PAYMENT_ACTION = @PAID_LOSS_PARTIAL)                    
begin                                  
 --update clm_activity with the differnce of sum of payments and last recorded reserve amount                              
 UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=(CLAIM_RESERVE_AMOUNT - @PAYMENT_AMOUNT),PAYMENT_AMOUNT=@PAYMENT_AMOUNT,CLAIM_PAYMENT_AMOUNT=(@CLAIM_PAYMENT_AMOUNT + @PAYMENT_AMOUNT) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                    





   
    
      
       
       
       
      
 --update clm_claim_info with the differnce of sum of payments and last recorded reserve amount                              
 UPDATE CLM_CLAIM_INFO SET OUTSTANDING_RESERVE=(OUTSTANDING_RESERVE - @PAYMENT_AMOUNT),PAID_LOSS=@PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID                    
end                          
else if (@PAYMENT_ACTION = @PAID_LOSS_FINAL ) */                   
      
-- ASFA (29-APR-2008)                  
if (@PAYMENT_ACTION = @VOID_CREDIT_TO_PAID_LOSS OR @PAYMENT_ACTION = @CREDIT_TO_PAID_LOSS)                    
begin        
set @PAYMENT_AMOUNT = -@PAYMENT_AMOUNT                                
 --update clm_activity with the differnce of sum of payments and last recorded reserve amount                            
-- UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=(CLAIM_RESERVE_AMOUNT - @PAYMENT_AMOUNT), PAYMENT_AMOUNT=@PAYMENT_AMOUNT,CLAIM_PAYMENT_AMOUNT=(@CLAIM_PAYMENT_AMOUNT + @PAYMENT_AMOUNT) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID           
 UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=@OLD_CLAIM_RESERVE_AMOUNT, 
RESERVE_AMOUNT = @PAYMENT_AMOUNT, PAYMENT_AMOUNT=@PAYMENT_AMOUNT,
CLAIM_PAYMENT_AMOUNT=(@CLAIM_PAYMENT_AMOUNT + @PAYMENT_AMOUNT) 
WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID 

  
    
      
      
      
      
      
      
 --update clm_claim_info with the differnce of sum of payments and last recorded reserve amount                              
-- UPDATE CLM_CLAIM_INFO SET PAID_LOSS=@PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID                    
end                          
else if (@PAYMENT_ACTION = @PAID_LOSS_PARTIAL)                    
begin                                  
 --update clm_activity with the differnce of sum of payments and last recorded reserve amount                            
-- UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=(CLAIM_RESERVE_AMOUNT - @PAYMENT_AMOUNT), PAYMENT_AMOUNT=@PAYMENT_AMOUNT,CLAIM_PAYMENT_AMOUNT=(@CLAIM_PAYMENT_AMOUNT + @PAYMENT_AMOUNT) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID           
 UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=@OLD_CLAIM_RESERVE_AMOUNT, 
RESERVE_AMOUNT = @PAYMENT_AMOUNT, 
PAYMENT_AMOUNT=@PAYMENT_AMOUNT,
CLAIM_PAYMENT_AMOUNT=(@CLAIM_PAYMENT_AMOUNT + @PAYMENT_AMOUNT) 
WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID 

  
    
      
      
      
      
      
      
 --update clm_claim_info with the differnce of sum of payments and last recorded reserve amount                              
-- UPDATE CLM_CLAIM_INFO SET PAID_LOSS=@PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID                    
end            
else if (@PAYMENT_ACTION = @PAID_LOSS_FINAL )                  
begin                          
 --update clm_activity with the sum of payments  ..WHEN OPTION IS CLOSE TO ZERO                        
-- Commented by Asfa (18-Oct-2007) #2727      
-- UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=0,PAYMENT_AMOUNT=@PAYMENT_AMOUNT,CLAIM_PAYMENT_AMOUNT=0 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
 UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=@OLD_CLAIM_RESERVE_AMOUNT,RESERVE_AMOUNT=@PAYMENT_AMOUNT, PAYMENT_AMOUNT=@PAYMENT_AMOUNT,      
 CLAIM_PAYMENT_AMOUNT= (@CLAIM_PAYMENT_AMOUNT + @PAYMENT_AMOUNT)       
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
                    
 --update clm_claim_info with the sum of payments  ..WHEN OPTION IS CLOSE TO ZERO                        
--UPDATE CLM_CLAIM_INFO SET OUTSTANDING_RESERVE=0,PAID_LOSS=@PAYMENT_AMOUNT WHERE CLAIM_ID=@CLAIM_ID                    
end                                 
-- Added by Asfa (14-Sept-2007) in order to correct Activities as per email sent by Gagan.                   
ELSE IF(@PAYMENT_ACTION=@ADJUSTMENT_EXPENSE OR @PAYMENT_ACTION=@OTHER_EXPENSE OR @PAYMENT_ACTION=@LEGAL_EXPENSE)                          
 BEGIN                        
  UPDATE CLM_ACTIVITY SET EXPENSES = (SELECT SUM(ISNULL(PAYMENT_AMOUNT,0)) FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID)                   
  WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID                
 END                               
                                     
ELSE IF(@PAYMENT_ACTION=@SUBROGATION_EXPENSE OR @PAYMENT_ACTION=@SALVAGE_EXPENSE OR @PAYMENT_ACTION=@CREDIT_TO_SUBROGATION_EXPENSE OR @PAYMENT_ACTION=@VOID_CREDIT_TO_SUBROGATION_EXPENSE OR @PAYMENT_ACTION=@CREDIT_TO_SALVAGE_EXPENSE       
  OR @PAYMENT_ACTION=@VOID_CREDIT_TO_SALVAGE_EXPENSE)                          
 BEGIN                        
  SELECT @PAYMENT_AMOUNT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
  IF(@PAYMENT_ACTION=@CREDIT_TO_SUBROGATION_EXPENSE OR @PAYMENT_ACTION=@VOID_CREDIT_TO_SUBROGATION_EXPENSE OR @PAYMENT_ACTION=@CREDIT_TO_SALVAGE_EXPENSE OR @PAYMENT_ACTION=@VOID_CREDIT_TO_SALVAGE_EXPENSE)      
  BEGIN      
 SET @PAYMENT_AMOUNT= -@PAYMENT_AMOUNT       
  END      
  UPDATE CLM_ACTIVITY SET CLAIM_RESERVE_AMOUNT=(ISNULL(@OLD_CLAIM_RESERVE_AMOUNT,0) + @PAYMENT_AMOUNT) ,RECOVERY = @PAYMENT_AMOUNT       
--(SELECT SUM(ISNULL(PAYMENT_AMOUNT,0)) FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID)      
      
      
      
  WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID                
 END        
        
ELSE IF(@PAYMENT_ACTION = @SALVAGE_REINSURANCE_RETURNED OR @PAYMENT_ACTION = @SUBROGATION_REINSURANCE_RETURNED)          
BEGIN        
 
--Negative Posting LOSS_REINSURANCE_RECOVERED For Itrack Issue 6144. 
SELECT @PAYMENT_AMOUNT=ISNULL(SUM(-PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE 
WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
  UPDATE CLM_ACTIVITY SET LOSS_REINSURANCE_RECOVERED = ISNULL(@PAYMENT_AMOUNT,0) 
    
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID         
END        
--Begin---- Modified by Mohit Agarwal 22-Aug-08 Itrack #4664    
ELSE IF(@PAYMENT_ACTION = @REINSURANCE_RETURNED_ADJUSTER_EXPENSE OR @PAYMENT_ACTION = @REINSURANCE_RETURNED_LEGAL_EXPENSE       
OR @PAYMENT_ACTION = @REINSURANCE_RETURNED_OTHER_EXPENSE)          
BEGIN        
  SELECT @PAYMENT_AMOUNT=ISNULL(SUM(-PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
  UPDATE CLM_ACTIVITY SET EXPENSE_REINSURANCE_RECOVERED = ISNULL(@PAYMENT_AMOUNT,0)           
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID         
END        
--End---- Modified by Mohit Agarwal 22-Aug-08 Itrack #4664    
      
ELSE IF(@PAYMENT_ACTION = @VOID_TO_REINSURANCE_RETURNED_PAID_LOSS      
  OR @PAYMENT_ACTION = @VOID_TO_REINSURANCE_RETURNED_SALVAGE_SUB_EXPENSE)          

BEGIN        
  SELECT @PAYMENT_AMOUNT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
UPDATE CLM_ACTIVITY SET LOSS_REINSURANCE_RECOVERED = ISNULL(@PAYMENT_AMOUNT,0)           
WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID         
END        
    
--Begin---- Modified by Mohit Agarwal 22-Aug-08 Itrack #4664      
ELSE IF(@PAYMENT_ACTION = @VOID_TO_REINSURANCE_RETURNED_ADJUSTER_EXPENSE       
  OR @PAYMENT_ACTION = @VOID_TO_REINSURANCE_RETURNED_OTHER_EXPENSE       
  OR @PAYMENT_ACTION = @VOID_TO_REINSURANCE_RETURNED_LEGAL_EXPENSE)          
BEGIN        
  SELECT @PAYMENT_AMOUNT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
  UPDATE CLM_ACTIVITY SET EXPENSE_REINSURANCE_RECOVERED = ISNULL(@PAYMENT_AMOUNT,0)           
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID         
END        
                              
--End---- Modified by Mohit Agarwal 22-Aug-08 Itrack #4664    
      
END      
      
      
      
      
    







GO

