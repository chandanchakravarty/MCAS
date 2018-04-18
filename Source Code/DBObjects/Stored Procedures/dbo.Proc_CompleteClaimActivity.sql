IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CompleteClaimActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CompleteClaimActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran                                                                
--drop proc dbo.Proc_CompleteClaimActivity                                                                       
--
--go                                                                
/*----------------------------------------------------------                                                                                                                                        
Proc Name       : dbo.Proc_CompleteClaimActivity                                                                                                                                  
Created by      : Sumit Chhabra                                                                                                                                      
Date            : 27/04/2006                                                                                                                                        
Purpose         : Commit Activity                                                                                                                   
Created by      : Sumit Chhabra                                                                                                                                       
Revison History :                                                                                                                                        
Used In        : Wolverine                                                                                                                                        
------------------------------------------------------------                                                                                                                                        
Modified By  : Asfa Praveen                                                                              
Date   : 30/Aug/2007                                                                              
Purpose   : To correct use of Adjuster_ID column                                                                            
                                                                    
Modified By  : Praveen  Kasana                                                                            
Date   : 13/March/2008                                                                              
Purpose   : Itrack #3585                                                                         
------------------------------------------------------------                                                                                                                                              
Date     Review By          Comments                                                                                                                                        
------   ------------       -------------------------*/                                                                                  
--drop proc dbo.Proc_CompleteClaimActivity                                                                            
                                                                                    
CREATE PROC [dbo].[Proc_CompleteClaimActivity]                                                                    
 

(                                                                                                                                                                                                                    
  @CLAIM_ID     int,                                                                                               
  @ACTIVITY_ID  int,                                                                                               
  @USER_ID int = null,     


  @ADJUSTER_ID_PARAM int = null,                                                          
-- @WOLVERINE_USER1 INT = null,    -- @WOLVERINE_USER2 INT = null                                   
  @NOTIFY_USER int = null output,
  @ACCOUNTING_SUPPRESSED int                                                                               
                                                                                  
)                                                                       
AS                                 
BEGIN                                      
                                                                         
--begin tran                                                           
DECLARE @CLAIM_NUMBER VARCHAR(10)                                                                 
                                      
DECLARE @DIV_ID int                                                                        
DECLARE @DEPT_ID int                                                             
DECLARE @PC_ID int                                                                        
DECLARE @DEBIT_ACCOUNT_ID int                                                                        
DECLARE @CREDIT_ACCOUNT_ID int                                                                        
DECLARE @OLD_CLAIM_RESERVE_AMOUNT DECIMAL(25,2)                                                              
DECLARE @CLOSE_RESERVE INT                                                                      
DECLARE @MCCA_ATTACHMENT_POINT DECIMAL(25,2)                                                                                                                      
DECLARE @MCCA_APPLIES DECIMAL(25,2)                                                                        
DECLARE @ATTACHMENT_POINT DECIMAL(25,2)                                                                                   
DECLARE @PRIMARY_EXCESS VARCHAR                                                                     
DECLARE @COVERAGE_ID INT                                                                      
DECLARE @REINSURANCE_CARRIER INT                                                                      
DECLARE @RI_RESERVE DECIMAL(25,2)                                                                      
DECLARE @VEHICLE_ID INT                                                                      
DECLARE @CREATED_BY INT                                                                      
DECLARE @POLICY_LIMITS DECIMAL(25,2)                                                                      
DECLARE @RETENTION_LIMITS DECIMAL(25,2)                                                                      
DECLARE @CRACCTS varchar(800)                                                                     
DECLARE @DRACCTS varchar(800)                                                                      
DECLARE @TRANSACTION_ID INT                                                                      
DECLARE @TEMP_ERROR_CODE INT                                                                                 
DECLARE @RETURN_VALUE INT                                                                        
DECLARE @ADJUSTER_ID  INT                                                                                                        
DECLARE @UNPAID_PREMIUM DECIMAL(25,2)                                                                             

DECLARE @RESERVE_AMOUNT DECIMAL(25,2)                             
                            
DECLARE @CLAIM_ITEM_STATUS VARCHAR(10)                                                  
                                              
DECLARE @RESERVE_LIMIT DECIMAL(25,2)                                                            
                                              
DECLARE @USER_LOGGED_RESERVE_LIMIT DECIMAL(25,2)--ADDED FOR ITRACK ISSUE 6101 ON 16 JULY 09            
DECLARE @PAYMENT_AMOUNT DECIMAL(25,2)                   
DECLARE @PAYMENT_LIMIT DECIMAL(25,2)  
 
DECLARE @USER_LOGGED_PAYMENT_LIMIT DECIMAL(25,2) --ADDED FOR ITRACK ISSUE 6101 ON 16 JULY 09               
DECLARE @NOTIFY_LIMIT DECIMAL(25,2)                                                              

 DECLARE @USER_LOGGED_NOTIFY_LIMIT DECIMAL(25,2)  --ADDED FOR ITRACK ISSUE 6101 ON 17 JULY 09                                                 
                                     
DECLARE @LOB_ID INT                                                                                
DECLARE @CUSTOMER_ID INT                                    
DECLARE @POLICY_ID INT                                                                                                             
DECLARE @POLICY_VERSION_ID INT                                                                                    

DECLARE @AWAITING_AUTHORIZATION INT                                               
                                              
DECLARE @RESERVE_LIMIT_TYPE_ID INT                                   
DECLARE @PAYMENT_LIMIT_TYPE_ID INT                                                                               

 DECLARE @LEGAL_EXPENSE INT                      
                        
                                                       
DECLARE @ADJUSTMENT_EXPENSE INT                                                                                   

DECLARE @SALVAGE_EXPENSE INT                                      
DECLARE @SUBROGATION_EXPENSE INT                                                                      
DECLARE @OTHER_EXPENSE INT                                                                                                       
DECLARE @RESERVE_AUTHORITY INT                                                                                                         
DECLARE @PAYMENT_AUTHORITY INT                                                                                                 
DECLARE @RECOVERY_SALVAGE INT                                                                                                                                      
DECLARE @RECOVERY_SUBROGATION INT                                                              
                            
 DECLARE @TOTAL_RECOVERY DECIMAL(25,2)                                                                                      
DECLARE @TOTAL_BREAKDOWN_AMOUNT DECIMAL(25,2)                                                    
                                              
DECLARE @SALVAGE_EXPENSE_SUM DECIMAL(25,2)                                                                                     
DECLARE @SALVAGE_RECOVERY_SUM DECIMAL(25,2)                                                     
                                      
DECLARE @TOTAL_PAYMENTS DECIMAL(25,2)                                                
                                              
DECLARE @TOTAL_EXPENSE DECIMAL(25,2)                                                                           
DECLARE @TOTAL_CLAIM_PAYMENTS DECIMAL(25,2)                                                                                            
DECLARE @TOTAL_OUTSTANDING DECIMAL(25,2)                                                                                            
DECLARE @OUTSTANDING DECIMAL(25,2)                                                                  
DECLARE @REDUCE_RESERVE_BY_PAYMENT_ACTION INT                                                              
DECLARE @ACTION_PAYMENTS DECIMAL(25,2)                                                                                      
DECLARE @TOTAL_RI_RESERVE DECIMAL(25,2)                                                                          
DECLARE @TOTAL_PAYEE_AMOUNT  DECIMAL(25,2)                                                 
DECLARE @ACTIVITY_REASON INT                                                                                         
DECLARE @ACTIVITY_STATUS INT        
DECLARE @ACTIVITY_COMPLETE INT                 
-- DECLARE @TOTAL_RESERVE INT             
 DECLARE @TOTAL_RESERVE DECIMAL(25,2)                                                                      
 --ACTIVITY REASON VARIABLES                 
 DECLARE @RESERVE_UPDATE INT                                                                                      
 DECLARE @EXPENSE_PAYMENT INT                                                                                   
 DECLARE @CLAIM_PAYMENT INT                                                                                                                                
 DECLARE @RECOVERY INT                                                                                                                  
 DECLARE @REINSURANCE INT                                                                                                                                 
 DECLARE @NEW_RESERVE INT                                                                                     
 DECLARE @CLAIM_MODULE INT                                                                                         
 DECLARE @ACTION_ON_PAYMENT INT                                                                   
 DECLARE @SALVAGE_REINSURANCE_RETURNED INT                                                
 DECLARE @SUBROGATION_REINSURANCE_RETURNED INT                                                                                                                                 
 DECLARE @PAID_LOSS_FINAL int                                       
 DECLARE @PAID_LOSS_PARTIAL int                                                               
 DECLARE @SYSTEM_USER_ID INT                                          
 DECLARE @LAST_UPDATED_DATETIME DATETIME                                                                       
 DECLARE @SYSTEM_ACTIVITY_ID INT                                                                       
 DECLARE @LOSS_DATE DATETIME                                                                                                                              
 DECLARE @RESERVE_UPDATE_ACTIVITY INT                                                                      
 DECLARE @DUMMY_POLICY_ID INT                                                                     
 DECLARE @ACTIVITY_INCOMPLETE INT                                                                     
                                                                      
 DECLARE @LOSS_REINSURANCE_RECOVERED_PAID_LOSS INT                                                                      
 SET @LOSS_REINSURANCE_RECOVERED_PAID_LOSS = 182                                                                      
                                                                      
 DECLARE @CHANGE_RESERVE_UPDATE_ACTIVITY INT                                                                      
 SET @CHANGE_RESERVE_UPDATE_ACTIVITY = 170       

 --Done for Itrack Issue 6835 on 17 Dec 09
 DECLARE @CURRENT_CLAIM_RESERVE_AMOUNT DECIMAL(25,2)                                                            
 DECLARE @CURRENT_CLAIM_RI_RESERVE_AMOUNT DECIMAL(25,2)                                                                     
 DECLARE @RI_RESERVE_AMOUNT DECIMAL(25,2)                                                                     
 DECLARE @NEW_REINSURANCE_RESERVE  INT     
 DECLARE @CHANGE_REINSURANCE_RESERVE  INT
 DECLARE @RE_OPEN_REINSURANCE_RESERVE  INT
 DECLARE @ACTUAL_RISK_ID INT
 DECLARE @ACTUAL_RISK_TYPE VARCHAR(10)
                                                             
SET @CLOSE_RESERVE = 167                                                                      
set @RESERVE_UPDATE_ACTIVITY = 205                                                                      
SET @PAID_LOSS_FINAL = 181                                                            
SET @PAID_LOSS_PARTIAL = 180        
SET @SALVAGE_REINSURANCE_RETURNED  = 178       
SET @SUBROGATION_REINSURANCE_RETURNED  = 179         
set @NOTIFY_USER = 0                                                                                  
set @CLAIM_MODULE = 5 --Claims Module ID for Diary Entry     
set @RETURN_VALUE = 1         
set @RESERVE_UPDATE = 11773                                                                                                                                           
set @EXPENSE_PAYMENT = 11774                                                                         
set @CLAIM_PAYMENT = 11775                                                                                                                                                                                
set @RECOVERY = 11776                                                                              
set @REINSURANCE = 11777                                                                                                                                                  
set @NEW_RESERVE = 165     
--Done for Itrack Issue 6835 on 17 Dec 09                                                                   
SET @NEW_REINSURANCE_RESERVE = 169
SET @CHANGE_REINSURANCE_RESERVE = 170                                                                                                                                  
SET @RE_OPEN_REINSURANCE_RESERVE = 172                                                                                                                                  
                                                                                                                                  
set @CLAIM_ITEM_STATUS = 'DP'                                                                                                           
                                                 
                                                                           
set @AWAITING_AUTHORIZATION = 11803 --lookup unique id for Awaiting Authorization Activity Status                                                                                                                           
set @RESERVE_LIMIT_TYPE_ID = 17 ---diary type id for reserve limit exceeded                        
set @PAYMENT_LIMIT_TYPE_ID = 18 ---diary type id for payment limit exceeded                                                                    
set @ACTIVITY_COMPLETE = 11801 --lookup unique id for Complete Activity Status                                                                       
--Added By Praveen                                              
set @ACTIVITY_INCOMPLETE = 11800 --lookup unique id for InComplete Activity Status                                                                                                         
SET @OTHER_EXPENSE  = 81                                                                                                                       
SET @LEGAL_EXPENSE = 82                                                                                                                         
SET @SALVAGE_EXPENSE = 83                                                                                      
SET @SUBROGATION_EXPENSE = 84                                                                                                                                                                          
SET @ADJUSTMENT_EXPENSE = 85                                                                                                             
SET @RECOVERY_SALVAGE = 63                                                                                        
SET @RECOVERY_SUBROGATION = 64                                                                                                                                                            
SET @REDUCE_RESERVE_BY_PAYMENT_ACTION=11785                                   
               
SELECT @DUMMY_POLICY_ID = DUMMY_POLICY_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID                            
                     
IF @DUMMY_POLICY_ID IS NULL                                            
BEGIN       
  --Find lob_id,customer_id,policy_id,policy_version_id of the current claim being worked upon                                                                                                      
  SELECT @LOB_ID=POLICY_LOB,@CUSTOMER_ID=PCPL.CUSTOMER_ID,@POLICY_ID=PCPL.POLICY_ID,                                           
  @POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID,                       
  -- @ADJUSTER_ID=CA.ADJUSTER_CODE, //Commented by Asfa 30-Aug-2007                                                                              
  @ADJUSTER_ID=CA.ADJUSTER_ID,           
  @LOSS_DATE = CCI.LOSS_DATE                                                                
  FROM POL_CUSTOMER_POLICY_LIST PCPL JOIN CLM_CLAIM_INFO CCI ON                                                   

  PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND PCPL.POLICY_ID = CCI.POLICY_ID AND                                               
 PCPL.POLICY_VERSION_ID =CCI.POLICY_VERSION_ID AND CCI.CLAIM_ID=@CLAIM_ID       
 LEFT OUTER JOIN CLM_ADJUSTER CA ON CCI.ADJUSTER_ID = CA.ADJUSTER_ID                                                                                       
END                                                                      
ELSE                                                                      
BEGIN                                                                      
  SELECT @LOB_ID=CCI.LOB_ID , @ADJUSTER_ID=CA.ADJUSTER_ID,@LOSS_DATE = CCI.LOSS_DATE                              

  FROM CLM_DUMMY_POLICY CDP JOIN CLM_CLAIM_INFO CCI                                                                       
  ON CCI.CLAIM_ID=CDP.CLAIM_ID                                                                                   
  LEFT OUTER JOIN CLM_ADJUSTER CA ON CCI.ADJUSTER_ID = CA.ADJUSTER_ID                                                      
  WHERE CCI.CLAIM_ID=@CLAIM_ID                                                                       
END                                                                       
-- Commented by Asfa 30-Aug-2007                                                                               
-- LEFT OUTER JOIN CLM_ADJUSTER CA ON CCI.ADJUSTER_CODE = CA.ADJUSTER_CODE                                                                                                                                                                 
--When we want an activity be completed by currently logged in user and not from system assigned adjuster, set                                      
--the ajduster id to current user                                                                                              
                                                                 
 IF @ADJUSTER_ID_PARAM IS NOT NULL AND @ADJUSTER_ID_PARAM<>0                                                                                    
 SET @ADJUSTER_ID = @ADJUSTER_ID_PARAM                                                                    
                                                                        
 /*Following code to check for payment of premium has been commented for now                                                                                           
 --Check whether the premium has been paid for the policy                                                                      
 SELECT @UNPAID_PREMIUM = ISNULL(SUM(TOTAL_DUE-TOTAL_PAID),0) FROM ACT_CUSTOMER_OPEN_ITEMS                                                                                        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                               
 AND ITEM_STATUS = @CLAIM_ITEM_STATUS                      
   
 if(@UNPAID_PREMIUM>0) --Premuim has not been paid yet, return                                                                
  return 2                                         */               
         
--Fetch reserve amount, payment amount and activity reason for the current claim and activity                                               
SELECT @RESERVE_AMOUNT=ISNULL(CLAIM_RESERVE_AMOUNT,0),@PAYMENT_AMOUNT=ISNULL(PAYMENT_AMOUNT,0),@RI_RESERVE_AMOUNT=CLAIM_RI_RESERVE, --Done for Itrack Issue 6835 on 17 Dec 09                 
@ACTIVITY_REASON=ISNULL(ACTIVITY_REASON,0),@ACTIVITY_STATUS=ISNULL(ACTIVITY_STATUS,0),                                                                                  
@ACTION_ON_PAYMENT=ISNULL(ACTION_ON_PAYMENT,0)                 
FROM CLM_ACTIVITY  WITH (NOLOCK)  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  

--Done for Itrack Issue 6835 on 17 Dec 09
SELECT @CURRENT_CLAIM_RESERVE_AMOUNT = SUM(ISNULL(OUTSTANDING,0)),@CURRENT_CLAIM_RI_RESERVE_AMOUNT = SUM(ISNULL(RI_RESERVE,0)) FROM CLM_ACTIVITY_RESERVE 
    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=0 

IF(@ACTION_ON_PAYMENT = @NEW_RESERVE)
BEGIN
	SET @RESERVE_AMOUNT = @CURRENT_CLAIM_RESERVE_AMOUNT
	SET @RI_RESERVE_AMOUNT = @CURRENT_CLAIM_RI_RESERVE_AMOUNT
END

IF(@ACTION_ON_PAYMENT = @NEW_REINSURANCE_RESERVE OR @ACTION_ON_PAYMENT = @CHANGE_REINSURANCE_RESERVE OR @ACTION_ON_PAYMENT = @RE_OPEN_REINSURANCE_RESERVE)
BEGIN
	SET @RESERVE_AMOUNT = @RI_RESERVE_AMOUNT
END
 --Added till here
                                                                                                                                             
 --No valid value for Activity Reason being found, lets return                                                                                                                                                    
IF (@ACTIVITY_REASON IS NULL OR @ACTIVITY_REASON=0)                                                                                       
BEGIN                                                                                  
 -- ROLLBACK TRAN                                                                                                                                                      
 SET @RETURN_VALUE = -1                                                                                                                                   
 RETURN @RETURN_VALUE                                                                                                                                                
END                                                                                                                                   
--------------------------------------------Recovery Activity Starts here---------------------------------                                                                                                   
--ACTIVITY_REASON is Recovery                                                                                                                                                                                        
--Update the status of the recovery fields at claim info table, update status of activity as completed, and return                                                                                                               
IF(@ACTIVITY_REASON=@RECOVERY)                                                                                                                         
BEGIN                                                                                 
 --Check for existence of at-least one payee for the given payment activity..                                                      
 --Return if there are no payees                    
                                                                 
 IF((SELECT COUNT(CLAIM_ID) FROM CLM_ACTIVITY_RECOVERY_PAYER  WITH (NOLOCK)                     
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND IS_ACTIVE='Y')<1)                                      
 BEGIN                                                                                       
   --    ROLLBACK TRAN               
   SET @RETURN_VALUE = 6                                 
   RETURN @RETURN_VALUE                                                           
  END                                  
                                                                    
   --Calculate total recovery       
    SELECT @TOTAL_RECOVERY = ISNULL(SUM(AMOUNT),0) FROM CLM_ACTIVITY_RECOVERY  WITH (NOLOCK)                                        
    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                  
                                                       
 -- Commented by Asfa (17-Sept-2007) in order to correct Activities as per email sent by Gagan.                                       
 -- UPDATE CLM_ACTIVITY SET RECOVERY=@TOTAL_RECOVERY,ACTIVITY_STATUS = @ACTIVITY_COMPLETE WHERE CLAIM_ID=@                                
 --CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                                                                                    
 -- UPDATE CLM_ACTIVITY SET RECOVERY= -(@TOTAL_RECOVERY),ACTIVITY_STATUS = @ACTIVITY_COMPLETE WHERE CLAIM_ID=@                                          
 --CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                                                                                        
 -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                     
                                                                
  UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(), ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED                          

  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                     






                                                              
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                    
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
                                                                 
  SELECT @TOTAL_RECOVERY = ISNULL(SUM(RECOVERY),0) FROM CLM_ACTIVITY WITH (NOLOCK)                                

  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_REASON=@ACTIVITY_REASON                                                                              






   
                                                              
     --update various fields at claim_info as follows                                                                                                                     
  UPDATE CLM_CLAIM_INFO SET RECOVERIES = ISNULL(@TOTAL_RECOVERY,0),MODIFIED_BY=@ADJUSTER_ID, LAST_UPDATED_DATETIME=GetDate()            
  WHERE CLAIM_ID=@CLAIM_ID                                                                                             
                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                         
 
END                                                                                                            
--------------------------------------------Recovery Activity Ends here---------------------------------                                                       
         
--------------------------------------------Expense Payment Activity Starts here---------------------------------          
--ACTIVITY_REASON is Expense Payment                              
--Look for payment amount, if not there find the previous record.              
--Check the authority limits of current adjuster. If not within limits, find another adjuster and add a diary entry for him/her                                                              
--With the new/current adjuster having the appropriate limits, update data at claim activity and claim info tables           

IF(@ACTIVITY_REASON=@EXPENSE_PAYMENT)             
BEGIN                                             
  --Check for existence of at-least one payee for the given payment activity..    
  --Return if there are no payees                               
  IF((SELECT COUNT(CLAIM_ID) FROM CLM_PAYEE  WITH (NOLOCK)  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                 
     AND IS_ACTIVE='Y')<1)                                                                        
  BEGIN                                                              
   SET @RETURN_VALUE = 6                    
   RETURN @RETURN_VALUE                                                                                                    
  END 
                                                 
  DECLARE @ADDITIONAL_EXPENSE DECIMAL(25,2)                                                                                                      
  SELECT @ADDITIONAL_EXPENSE = ISNULL(ADDITIONAL_EXPENSE,0) FROM CLM_ACTIVITY_EXPENSE WITH (NOLOCK)                                                 
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                            
                                                  
  SELECT @TOTAL_EXPENSE = ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE WITH (NOLOCK)                                                  
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                                          
  SET @TOTAL_EXPENSE = ISNULL(@TOTAL_EXPENSE,0) + ISNULL(@ADDITIONAL_EXPENSE,0)                                                                                                                               
  --Check that the payment breakdown for the claim activity equals total payment for the activity                                                                 
  --SELECT @TOTAL_BREAKDOWN_AMOUNT=ISNULL(SUM(PAID_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE_BREAKDOWN WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                 

--Check that the payee's payment breakdown for the claim activity equals the total payment for the activity                                       
  SELECT @TOTAL_PAYEE_AMOUNT=ISNULL(SUM(AMOUNT),0) FROM CLM_PAYEE WITH (NOLOCK)                           
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                      
 --When the payment breakdown for payee or payment is less than total payment, return                                                                                                                           
            
  IF (@TOTAL_EXPENSE IS NULL OR @TOTAL_PAYEE_AMOUNT<@TOTAL_EXPENSE)                                                

 BEGIN                                  
   --   ROLLBACK TRAN                                                
   SET @RETURN_VALUE = 3                                                                                      
   RETURN @RETURN_VALUE                                                                                  

   END                            
                        
  --Raghav: For iTrack 5751, if Total of coverage breakup is not equal to expense amount return   
  IF (ISNULL(@TOTAL_EXPENSE,0) <> ISNULL(@TOTAL_PAYEE_AMOUNT,0))     
   BEGIN                                                                                                     
 --   ROLLBACK TRAN                                                                                                                     
   SET @RETURN_VALUE = 9                                                                                      
   RETURN @RETURN_VALUE                        
  END                                  
             
  --Commented for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed     only when there is suitable Ajuster/User which the limit to complete the activity                            
 -- IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                                                       
 -- BEGIN                                                                                                       
 --  --Update the status of activity as complete                                                                                                   
 --  -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                                            
 --  UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                                                                   
 --  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                                             
 -- SELECT @TEMP_ERROR_CODE = @@ERROR                                                                          
 --                                                                
 --  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                                                                  
 --  UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                                  
 --                                               
 --  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
 --  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
 --                                                                
 --  SET @RETURN_VALUE = 1                                                                                         
 -- END                                       
 -- ELSE                                                                             
 -- BEGIN                                                                                                             
  --Fetch Payment Limit assigned to current adjuster/ user                              
                
/* --Commented for Itrack Issue 6101 on 16 Sept 09 --- No need to check adjuster limit                
--  SELECT @PAYMENT_LIMIT = ISNULL(PAYMENT_LIMIT,0),@NOTIFY_LIMIT=ISNULL(NOTIFY_AMOUNT,0)                          --  FROM CLM_ADJUSTER_AUTHORITY CAA JOIN CLM_AUTHORITY_LIMIT CAL  
  
 ON CAA.LIMIT_ID = CAL.LIMIT_ID LEFT JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID                 
--    --where CA.ADJUSTER_CODE= @ADJUSTER_ID   //Commented by Asfa 30-Aug-2007             
--    WHERE CA.ADJUSTER_ID=@ADJUSTER_ID AND CAA.LOB_ID=@LOB_ID AND CAA.IS_ACTIVE='Y'  */                                               
                                               
  --Added for Itrack Issue6101 on 16 July --- Check for user limit         
  SELECT @USER_LOGGED_PAYMENT_LIMIT = ISNULL(PAYMENT_LIMIT,0),                                                    
  @USER_LOGGED_NOTIFY_LIMIT=ISNULL(NOTIFY_AMOUNT,0)                     
  FROM CLM_ADJUSTER_AUTHORITY CAA WITH (NOLOCK) JOIN CLM_AUTHORITY_LIMIT CAL                               
  ON CAA.LIMIT_ID = CAL.LIMIT_ID             
  LEFT JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID AND CA.IS_ACTIVE='Y'                   
  LEFT OUTER JOIN MNT_USER_LIST MUL ON CA.USER_ID=MUL.USER_ID  
  --AND                           
  --Done for Itrack Issue 6285 on 25 Aug 2009  
  --Commented for Itrack Issue 6542 on 15 Oct 09                      
  --ISNULL(CA.ADJUSTER_CODE,'0') = ISNULL(MUL.ADJUSTER_CODE,'0')                          
  WHERE MUL.USER_ID=@USER_ID AND (MUL.USER_TYPE_ID=46 OR MUL.USER_SYSTEM_ID = 'W001')                           
  AND CAA.LOB_ID=@LOB_ID AND CAA.IS_ACTIVE='Y'                                                            
                                                           
   --Check whether the current adjuster is within limits to authorise payment transaction of the given amount         --CURRENT ADJUSTER IS WITHIN LIMITS TO AUTHORIZE                                                                           
 --Commented for Itrack Issue 6101 on 16 Sept 09 --- No need to check adjuster limit                     
-- IF((@TOTAL_EXPENSE <= @PAYMENT_LIMIT) )                                          
-- BEGIN                                     
--   --Moved here for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed                                           
--   -- only when there is suitable Ajuster/User which the limit to complete the activity -30 July 09 --                                            
--   --Update the status of activity as complete                                                   
--  IF(@ACTION_ON_PAYMENT = @SALVAGE_REINSURANCE_RETURNED OR @ACTION_ON_PAYMENT = @SUBROGATION_REINSURANCE_RETURNED)  BEGIN                                               
--   IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                                                   
--   BEGIN                                                                                                       
--     --Update the status of activity as complete                                                                                                               
--     -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                                            
--     UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                                                  
--     WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                                                                      
--     SELECT @TEMP_ERROR_CODE = @@ERROR                      
--                                            
--     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM               
--     UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                                  
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                    
--     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
--                      
--     SET @RETURN_VALUE = 1                                                                              
--   END    
--   ELSE                                                                             
--   BEGIN                                                                          
--   -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                    
--   UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                            WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID 
--   END                             END                                                            
--   -----------------------end of else of @SALVAGE_REINSURANCE_RETURNED---------------------------------------                                            
--  ELSE                                          
--  BEGIN    
--   --Moved here for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed    only when there is suitable Ajuster/User which the limit to complete the activity- 30 July 09          

--   IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                                                                      BEGIN                                                                                                      
--    --Update the status of activity as complete                                                                          
--    -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                                 
--    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                            WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                      
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                          
--                                                                 
--    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                       
--    UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                   
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
--    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
--                                                                 
--    SET @RETURN_VALUE = 1                                                                                         
--   END                                                                                                                       
--   ELSE                    
--   BEGIN                                                              
--  -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                            
--    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                            WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                            END                  




















                          
--   --UPDATE CLM_ACTIVITY SET EXPENSES=@TOTAL_EXPENSE,ACTIVITY_STATUS = @ACTIVITY_COMPLETE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID      
--  END                                                                               
--            
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                        
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                             
--   --When the activity is being completed, check whether the activity is crossing the notification limit                                                                   
--  IF(@TOTAL_EXPENSE>@NOTIFY_LIMIT)                                                      
--  BEGIN                                 
--    --Diary entry will be made at page level now using the Diary Setup under Maintenance                                            
--    --ADD DIARY ENTRY                             
--    /*INSERT INTO TODOLIST                                               
--    (          
--    RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                                                               
--    POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                                                    
--    FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                                                                                          
--    FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                                                                                                          
--    )                          
--    VALUES                                           
--    (                                                                                   
--    NULL,GETDATE(),DATEADD(DAY,7,GETDATE()),@PAYMENT_LIMIT_TYPE_ID,                                                                                             
--    NULL,'Notification limit for Activity exceeded','Y',                                                                     
--    NULL,'M',@WOLVERINE_USER1,@USER_ID,NULL,NULL,'Notification limit for the Activity has been exceeded',NULL,NULL,@CLAIM_ID,NULL,NULL,NULL,                                                                        
--    @CUSTOMER_ID,NULL,NULL,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE                   
--    )                                                       
--                                                                
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                                                          
--    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
--    --ADD DIARY ENTRY                                                             
--    INSERT INTO TODOLIST          
--    (                                                                                                  
--    RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                                                                                                          
--    POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                                  
--    FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                              
--    FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                      
--    )                                                                                                          
--    VALUES                                                             
--(      
--    NULL,GETDATE(),DATEADD(DAY,7,GETDATE()),@PAYMENT_LIMIT_TYPE_ID,                                                     
--    NULL,'Notification limit for Activity exceeded','Y',                                      
--    NULL,'M',@WOLVERINE_USER2,@USER_ID,NULL,NULL,'Notification limit for the Activity has been exceeded',NULL,NULL,@CLAIM_ID,NULL,NULL,NULL,                                                                                                          
--    @CUSTOMER_ID,NULL,NULL,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE                           
--    )       */                                                                                                                            
--    SET @NOTIFY_USER = 1                                                                     
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                        
--    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
--  END                      
--  set @RETURN_VALUE = 1                                   
-- END                         
--  --CURRENT ADJUSTER IS WITHIN LIMITS TO AUTHORIZE                                                   
  --Added for Itrack Issue 6101 on 16 July --- Check for user limit                                                           
            
--Commented for Itrack Issue 6101 on 16 Sept 09--- Check only for logged in user limit             
--  ELSE IF((@TOTAL_EXPENSE <= @USER_LOGGED_PAYMENT_LIMIT) )   
       
  IF((@TOTAL_EXPENSE <= @USER_LOGGED_PAYMENT_LIMIT) )                                          
  BEGIN                                      
                                                                                                                                            
--Moved here for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed      --only when there is suitable Ajuster/User which the limit to complete the activity   
  
    
     
--Update the status of activity as complete                                                                            
   IF(@ACTION_ON_PAYMENT = @SALVAGE_REINSURANCE_RETURNED OR                                    
   @ACTION_ON_PAYMENT = @SUBROGATION_REINSURANCE_RETURNED)                                    
   BEGIN                                                   
    IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                                                   
	BEGIN                                                                                            
     --Update the status of activity as complete                                                                       -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                              
	UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(), ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED                           

    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                  
                    
                                          
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                          
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                              
     UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                                  
     SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                             
         
     SET @RETURN_VALUE = 1                                                                                
END                                                                                                                       
    ELSE                                             
    BEGIN                                                    
    -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                    
     UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(), ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                  
    END                                                    
   END                                                          
   -----------------------end of else of @SALVAGE_REINSURANCE_RETURNED---------------------------------------                                    
  ELSE   
  BEGIN   
 --Moved here for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed  only when there is suitable Ajuster/User which the limit to complete the activity - 30 July 09         
                       
 
IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                     
    BEGIN     
     --Update the status of activity as complete                                         
   -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                           
     UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(), ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED                            WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                  




                                                









	SELECT @TEMP_ERROR_CODE = @@ERROR                                                      
     IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                         
                                                             
     UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                 
	 SELECT @TEMP_ERROR_CODE = @@ERROR                                                        
	 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
                                                                 
     SET @RETURN_VALUE = 1                                                                                         
   END                                
   ELSE                                                                             
   BEGIN                                                                     
    -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                  
    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(), ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED	
	WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                           









   END                    

    
                                                          
    --UPDATE CLM_ACTIVITY SET EXPENSES=@TOTAL_EXPENSE,ACTIVITY_STATUS = @ACTIVITY_COMPLETE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                 
  END                                                                               
       
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                   
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                           
  --When the activity is being completed, check whether the activity is crossing the logged user notification limit                                
  IF(@TOTAL_EXPENSE>@USER_LOGGED_NOTIFY_LIMIT)                                                                                  
  BEGIN                                                
    SET @NOTIFY_USER = 1                                                                                  
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                                  
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                                                                                          
  END                                                                                        
   SET @RETURN_VALUE = 1                                                  
  END       
  ELSE                  
   BEGIN                
   --When the current adjuster is not within limits to authorize transaction, first check if there are adjusters                                                                                       
   --meeting the limits..Make a diary entry if there are adjusters       
                    
   /*IF EXISTS(SELECT DISTINCT CA.ADJUSTER_ID FROM CLM_ADJUSTER CA WITH (NOLOCK)  JOIN CLM_ADJUSTER_AUTHORITY CAA                                                                    
   --CA.ADJUSTER_CODE FROM CLM_ADJUSTER CA JOIN CLM_ADJUSTER_AUTHORITY CAA //Commented by Asfa 30-Aug-2007                                                                       
   ON CA.ADJUSTER_ID=CAA.ADJUSTER_ID AND CAA.LOB_ID=@LOB_ID AND CA.IS_ACTIVE='Y' AND CAA.IS_ACTIVE='Y' AND CAA.LIMIT_ID =                                                      
   (                        
   SELECT  TOP 1 LIMIT_ID FROM CLM_AUTHORITY_LIMIT   WITH (NOLOCK)                                                                                          
   WHERE PAYMENT_LIMIT>@TOTAL_EXPENSE AND IS_ACTIVE='Y' ORDER BY PAYMENT_LIMIT))                                                                                                              
   BEGIN    */                                       
     --Add a diary entry for the new found adjusters                                                                          
   /* Done for Itrack Issue 6542 on 16 Oct 09                                     
    INSERT into TODOLIST           
    (                                                                                                    
    RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                                                                                                                
    POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                                                              
    FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                                                                
    FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID)                                                                            
    SELECT DISTINCT                                                                                            
    null,GetDate(),GetDate(),@PAYMENT_LIMIT_TYPE_ID                                                         
    ,null,--'Payment Limit Exceeded'  
 --Done for Itrack Issue 6101 on 17 Sept 09  
 '/Reserve exceeds Users limit of Authority','Y',                                                                  null,'M',                         
    --   CA.ADJUSTER_CODE,           
    CA.USER_ID,                                                                      
    @ADJUSTER_ID,null,null,--'Payment Limit Exceeded'  
 --Done for Itrack Issue 6101 on 17 Sept 09  
 'Activity could not be completed as the Payment/Reserve exceeds the Users limit of Authority',  
null,null,@CLAIM_ID,@ACTIVITY_ID,null,null,                                                            
    @CUSTOMER_ID,null,null,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE                                                                                 
    FROM CLM_ADJUSTER CA  WITH (NOLOCK) JOIN CLM_ADJUSTER_AUTHORITY CAA                                                                                   
    ON CA.ADJUSTER_ID=CAA.ADJUSTER_ID AND CAA.LOB_ID=@LOB_ID AND CA.IS_ACTIVE='Y' AND CAA.IS_ACTIVE='Y' AND CAA.LIMIT_ID =           
    (         
    SELECT  TOP 1 LIMIT_ID FROM CLM_AUTHORITY_LIMIT    WITH (NOLOCK)                                
    WHERE PAYMENT_LIMIT>@TOTAL_EXPENSE AND IS_ACTIVE='Y' ORDER BY PAYMENT_LIMIT) */                       
                     
                                
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                           
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM        
                                                
	             
    --Change the status of the activity as Awaiting Authorization --Done for Itrack Issue 6547 on 12 Oct 09
	IF(@ACTION_ON_PAYMENT NOT IN (243,245,249,250,251,252,253))
	BEGIN     
	 
     UPDATE CLM_ACTIVITY SET EXPENSES=@TOTAL_EXPENSE,ACTIVITY_STATUS = @ACTIVITY_INCOMPLETE, ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED 
     WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                           

    END
	ELSE  
	BEGIN
	  UPDATE CLM_ACTIVITY SET EXPENSES=@TOTAL_EXPENSE,ACTIVITY_STATUS = @AWAITING_AUTHORIZATION, ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED 
      WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID       
	END                                                      
                                                                
    SELECT @TEMP_ERROR_CODE = @@ERROR 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                                 
   set @RETURN_VALUE = 4                                                                                                                                          
  /*  END                                         
   ELSE --WE DO NOT HAVE ANY ADJUSTER SATISFYING THE GIVEN CONDITIONS...                             
   BEGIN              
    --  ROLLBACK TRAN                                                                                        
    SET @RETURN_VALUE=5 --SETTING THE RETURN VALUE TO 5 TO INDICATE THAT NO ADJUSTER HAS BEEN FOUND                     */               
    RETURN @RETURN_VALUE --RETURN FROM THE PROCEDURE                                                                                       
   --END                                                                                                                        
                                                                
  --END                                                 
                                                                 
  SELECT @TOTAL_EXPENSE=ISNULL(SUM(EXPENSES),0) FROM CLM_ACTIVITY WITH (NOLOCK)                           
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_REASON=@EXPENSE_PAYMENT                                                                                      
                             
 --update various fields at claim_info as follows                                       
  UPDATE CLM_CLAIM_INFO SET PAID_EXPENSE = ISNULL(@TOTAL_EXPENSE,0),MODIFIED_BY=@ADJUSTER_ID,LAST_UPDATED_DATETIME=GetDate() WHERE CLAIM_ID=@CLAIM_ID                                                                         
                                        
                                                                
  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                                                      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                               
                                                  
 END                                                                                                                       
                 
end                                                                    
   
--------------------------------------------Expense Payment Activity Ends here---------------------------------                                                    
                                                                    
--------------------------------------------Reinsurance Activity Starts here---------------------------------                                    
                
      
                    
                     
--Reinsurance Activity will be treated similar to Reserve Update                                          
                                                                                                
--ACTIVITY_REASON is Reinsurance 
--Update the status of the activity as complete and lets leave the proc                                                       
IF(@ACTIVITY_REASON=@REINSURANCE)                                
                            
BEGIN                                                                                                    
                                                                 
  SELECT @TOTAL_RI_RESERVE=ISNULL(SUM(RI_RESERVE),0) FROM CLM_ACTIVITY WITH (NOLOCK)  WHERE CLAIM_ID=@CLAIM_ID                                                                                                  
  UPDATE CLM_CLAIM_INFO SET RESINSURANCE_RESERVE = ISNULL(@TOTAL_RI_RESERVE,0) WHERE CLAIM_ID=@CLAIM_ID                                                                                     
  SELECT @TEMP_ERROR_CODE = @@ERROR           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
                                                            
END                                                                                               
                                                                     
--------------------------------------------Reinsurance Activity Ends here---------------------------------                                                                                                        
--------------------------------------------New Reserve Activity Starts here---------------------------------                                                                                   
--ACTIVITY_REASON IS RESERVE UPDATE AND ACTION ON PAYMENT IS NEW RESERVE                                                     
IF(@ACTIVITY_REASON = @RESERVE_UPDATE AND @ACTION_ON_PAYMENT=@NEW_RESERVE)                                                                    
BEGIN                                                               
                                                                
 --Get the sum of total outstandings for the claim from reserve table                                                                                   
 SELECT @OUTSTANDING = ISNULL(SUM(OUTSTANDING),0) FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK)                           
 WHERE CLAIM_ID=@CLAIM_ID                   
 --update various fields at claim_info as follows                                                                                                                                          
 UPDATE CLM_CLAIM_INFO SET OUTSTANDING_RESERVE = ISNULL(@OUTSTANDING,0),MODIFIED_BY=@ADJUSTER_ID,LAST_UPDATED_DATETIME=GetDate() WHERE CLAIM_ID=@CLAIM_ID                





                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                       
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                  
                                                                    
END                                                                            
   
--------------------------------------------Reinsurance Activity Ends here---------------------------------                        
   
--------------------------------------------Claim Payment Activity Ends here---------------------------------                                                                   
            -- select @ACTIVITY_REASON,     @CLAIM_PAYMENT    ,@LOSS_REINSURANCE_RECOVERED_PAID_LOSS    ,@ACTION_ON_PAYMENT                                
--###############Added by Praveen kasana 14 March 2008####################3                                                                      
                                                                
IF(@ACTION_ON_PAYMENT = @LOSS_REINSURANCE_RECOVERED_PAID_LOSS)                                                                      
                      
BEGIN                                        
--print 'hello'                                                            
 SELECT @SYSTEM_USER_ID = USER_ID FROM MNT_USER_LIST WITH (NOLOCK)  WHERE USER_LOGIN_ID = 'SYSTEM'                                                                      
 SELECT @LAST_UPDATED_DATETIME = GetDate()    
 EXEC Proc_InsertCLM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID OUTPUT,11773,'CHANGE REINSURANCE RESERVE',@SYSTEM_USER_ID,11801,0,@CHANGE_RESERVE_UPDATE_ACTIVITY,@ACCOUNTING_SUPPRESSED                                                                
                                                                   
 /*                                                                    
 SELECT CAR.RESERVE_ID, COVERAGE_ID,CAR.OUTSTANDING - CAP.AMOUNT AS OUTSTANDING,                                                                      
 REINSURANCE_CARRIER,RI_RESERVE,CAR.VEHICLE_ID,POLICY_LIMITS,RETENTION_LIMITS                                                                      
 FROM CLM_ACTIVITY_RESERVE CAR  WITH (NOLOCK)                           
 JOIN CLM_ACTIVITY_RECOVERY CAP ON CAR.RESERVE_ID = CAP.RESERVE_ID                
 AND CAR.CLAIM_ID = CAP.CLAIM_ID                                                                      
 WHERE CAR.CLAIM_ID=756                            
 */                                                        
 -- select * from clm_activity_recovery where claim_id = 756 and activity_id = 8                                        
                                                                    
    SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0)+1 FROM CLM_ACTIVITY_RESERVE  WITH (NOLOCK)          
       
    WHERE CLAIM_ID=@CLAIM_ID                              
                            
-------------------------Done for Itrack 6087 on 4 Aug 09----------------------------------------------------        --Sets the Debit and Credit value if there is only one Account.Sets the Debit and Credit value to null                             
   --if there are multiple accounts                            
  IF EXISTS(SELECT SELECTEDDEBITLEDGERS                            
   FROM CLM_TYPE_DETAIL WHERE TYPE_ID = 8 and DETAIL_TYPE_ID = @CHANGE_RESERVE_UPDATE_ACTIVITY AND                            
    dbo.piece(SELECTEDDEBITLEDGERS,',',2) is null                            
     )                            
 BEGIN                             
  SELECT @DRACCTS =  ACCOUNT_ID FROM ACT_GL_ACCOUNTS WITH (NOLOCK)       
  WHERE ACC_NUMBER IN (SELECT SELECTEDDEBITLEDGERS FROM CLM_TYPE_DETAIL WITH (NOLOCK)                              
       WHERE DETAIL_TYPE_ID = @CHANGE_RESERVE_UPDATE_ACTIVITY)                                 
                              
 END                   
                            
 IF EXISTS(SELECT SELECTEDCREDITLEDGERS                            
     FROM CLM_TYPE_DETAIL WHERE TYPE_ID = 8 and DETAIL_TYPE_ID = @CHANGE_RESERVE_UPDATE_ACTIVITY AND                            
     dbo.piece(SELECTEDCREDITLEDGERS,',',2) is null                       
    )                            
 BEGIN                                                               
  SELECT @CRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS WITH (NOLOCK)                              
  WHERE ACC_NUMBER=(SELECT SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL WITH (NOLOCK)                     
       WHERE DETAIL_TYPE_ID = @CHANGE_RESERVE_UPDATE_ACTIVITY)                                                                      
 END        
                             
 ------------------------------------------End 6087-------------------------------------------                                                               
                                               
 DECLARE CUR CURSOR                                                             
                                        
 FOR SELECT COVERAGE_ID,PRIMARY_EXCESS,MCCA_ATTACHMENT_POINT,MCCA_APPLIES,ATTACHMENT_POINT,CAR.OUTSTANDING,  REINSURANCE_CARRIER,isnull(RI_RESERVE,0) - CAP.AMOUNT as RI_RESERVE,CAR.VEHICLE_ID,POLICY_LIMITS,RETENTION_LIMITS,
 CAR.ACTUAL_RISK_ID,CAR.ACTUAL_RISK_TYPE
 FROM CLM_ACTIVITY_RESERVE CAR WITH (NOLOCK) JOIN CLM_ACTIVITY_recovery CAP             
 ON CAR.RESERVE_ID = CAP.RESERVE_ID     AND CAR.CLAIM_ID = CAP.CLAIM_ID                                           

 WHERE CAR.CLAIM_ID=@CLAIM_ID                            
                                                                    
 OPEN CUR                                                                      
 FETCH NEXT FROM CUR                                                                       
 INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE                                        

    
 WHILE @@FETCH_STATUS = 0                                                                      
 BEGIN                                                                      
  /*                                                                     
  if isnull(@RI_RESERVE , 0) <> 0          
  begin                                                                     
  select @RI_RESERVE                                                                    
  end                                                                     
  */                                                 
  EXEC Proc_InsertCLM_ACTIVITY_RESERVE @CLAIM_ID ,@SYSTEM_ACTIVITY_ID,@COVERAGE_ID,@PRIMARY_EXCESS,                          
  @MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,                         
 
  @SYSTEM_USER_ID,@LAST_UPDATED_DATETIME, @VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,                          
  @CHANGE_RESERVE_UPDATE_ACTIVITY,@CRACCTS,@DRACCTS, @TRANSACTION_ID,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE                   
     --select @RI_RESERVE as clm_reserve                                                                
                              
  FETCH NEXT FROM CUR                                                                       
  INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE                       
 END                   
 CLOSE CUR                  
 DEALLOCATE CUR                          
 SET @RESERVE_AMOUNT = 0            
                                                          
 SELECT @RI_RESERVE = - ISNULL(SUM(AMOUNT),0) FROM CLM_ACTIVITY_recovery  WITH (NOLOCK)                           

 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  and is_active = 'Y'               






EXEC Proc_UpdateActivityReserve @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@RESERVE_AMOUNT,NULL,NULL,0,@RI_RESERVE,NULL,                          
 @LAST_UPDATED_DATETIME         
 --select @CLAIM_ID as cid ,@SYSTEM_ACTIVITY_ID as sai,@RESERVE_AMOUNT,                                                                
   --@RI_RESERVE as rr,@LAST_UPDATED_DATETIME  as ll          

  --Done for Itrack Issue 6101 on 12 Oct 09 
   
  /*UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @AWAITING_AUTHORIZATION                           
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @SYSTEM_ACTIVITY_ID*/
      
  UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_INCOMPLETE                           
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @SYSTEM_ACTIVITY_ID                               
      
                                                                
END                                                                     
          
-------------------------------------------------------------------end Praveen kasana   
---#################33###############                                                                      
--activity reason is Claim Payment                                                                               
                                                                
IF (@ACTIVITY_REASON=@CLAIM_PAYMENT)                                                                                                                   
BEGIN                                                                                 
 --Check for existence of at-least one payee for the given payment activity..                                                            
 --Return if there are no payees                                          
                                                      
 IF((SELECT COUNT(CLAIM_ID) FROM CLM_PAYEE  WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND IS_ACTIVE='Y')<1)                                                                        
 BEGIN                                                                                                                                 
  --   ROLLBACK TRAN                                                                                                                                                      
  SET @RETURN_VALUE = 6                          
  return @RETURN_VALUE                                                                                                                                   
 END                                            
                                                                                                                                            
 SELECT @TOTAL_CLAIM_PAYMENTS = ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_PAYMENT  WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID             


--Check that the payment breakdown for the claim activity equals total payment for the activity                                                      
 --Payment breakdown check is being removed as the page will not be used                                                            
 --  SELECT @TOTAL_BREAKDOWN_AMOUNT=ISNULL(SUM(PAID_AMOUNT),0) FROM CLM_ACTIVITY_PAYMENT_BREAKDOWN WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                   
 --Check that the payee's payment breakdown for the claim activity equals the total payment for the activity                                                
 SELECT @TOTAL_PAYEE_AMOUNT=ISNULL(SUM(AMOUNT),0) FROM CLM_PAYEE  WITH (NOLOCK)                           
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                              
 --When the payment breakdown for payee or payment is less than total payment, return                                                                                                                                               
 --IF (@TOTAL_CLAIM_PAYMENTS IS NULL  OR @TOTAL_BREAKDOWN_AMOUNT<@TOTAL_CLAIM_PAYMENTS OR @TOTAL_PAYEE_AMOUNT<@TOTAL_CLAIM_PAYMENTS)                                                                                                                  
 --Above check is modified to exlude payment breakdown data (page not to be used anymore)               
 IF (@TOTAL_CLAIM_PAYMENTS IS NULL  OR  @TOTAL_PAYEE_AMOUNT<@TOTAL_CLAIM_PAYMENTS)             
 BEGIN                                                                             
  --  ROLLBACK TRAN                                                                                                                                 
  SET @RETURN_VALUE = 3                                                                                
  return @RETURN_VALUE                             
 END                                                                                        
                                                                                   
 -- Added FOR Itrack Issue #5751.                                                                     
 IF (ISNULL(@TOTAL_CLAIM_PAYMENTS,0) <> ISNULL(@TOTAL_PAYEE_AMOUNT,0) )                     
 BEGIN                                                                                        
  SET @RETURN_VALUE = 9                                                   
  RETURN @RETURN_VALUE                                                                      
 END                                                                    
 --Commented for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed  only when there is suitable Ajuster/User which the limit to complete the activity - 30 July 09                     

-- IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                                                                              
-- BEGIN                                                                    
--                                                 
--  --Update the status of activity as complete                                                                                   
--  -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                    
--  UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                                                                     
--WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                    
--                                         
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                         
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                             
--                                                          
--  UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                   
--                                                                
--  EXEC PROC_COMPLETECLAIMPAYMENT   @CLAIM_ID ,                                                                          
--  @ACTIVITY_ID ,                                                         
--  @USER_ID  ,                                                                                  
--  @ADJUSTER_ID_PARAM ,                                                                    
--  @NOTIFY_USER                                                                    
--    
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                           
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                
--                       
--  set @RETURN_VALUE = 1                                                                                 
--                                                                 
-- END                                            
-- ELSE    
-- BEGIN                                                                             
  --Fetch Payment Limit assigned to current adjuster/ user                                                                       
  --Commented for Itrack Issue 6101 on 16 Sept 09 --- Check for user limit                                         
/*--  SELECT @PAYMENT_LIMIT = ISNULL(PAYMENT_LIMIT,0),@NOTIFY_LIMIT=ISNULL(NOTIFY_AMOUNT,0) FROM                        CLM_ADJUSTER_AUTHORITY CAA  WITH (NOLOCK) JOIN CLM_AUTHORITY_LIMIT CAL ON CAA.LIMIT_ID = CAL.LIMIT_ID	
	
	 LEFT JOIN CLM_ADJUSTER  CA ON  CA.ADJUSTER_ID = CAA.ADJUSTER_ID                WHERE CA.ADJUSTER_ID=@ADJUSTER_ID           */                
                                           
  --CA.ADJUSTER_CODE=@ADJUSTER_ID //Commented by Asfa 30-Aug-2007                                                   AND CAA.LOB_ID=@LOB_ID  AND CAA.IS_ACTIVE='Y'                                                                     

--Check whether the current adjuster is within limits to authorise payment transaction of the given amount          
  -- Commented if by Mohit Agarwal ITrack 4693 15-Sep-08                
  --if (1=1)                              
                                 
                                                      
   --Added for Itrack Issue 6101 on 16 July --- Check for user limit                                                            
   SELECT @USER_LOGGED_PAYMENT_LIMIT = ISNULL(PAYMENT_LIMIT,0),@USER_LOGGED_NOTIFY_LIMIT=ISNULL(NOTIFY_AMOUNT,0)  
   FROM CLM_ADJUSTER_AUTHORITY CAA WITH (NOLOCK) JOIN CLM_AUTHORITY_LIMIT CAL ON CAA.LIMIT_ID = CAL.LIMIT_ID                   
 LEFT JOIN CLM_ADJUSTER CA ON  CA.ADJUSTER_ID = CAA.ADJUSTER_ID  AND CA.IS_ACTIVE='Y'                              LEFT OUTER JOIN MNT_USER_LIST MUL ON CA.USER_ID=MUL.USER_ID 
	--AND                           
   --Done for Itrack Issue 6285 on 25 Aug 2009  
	--Commented for Itrack Issue 6542 on 15 Oct 09                         
   --ISNULL(CA.ADJUSTER_CODE,'0') = ISNULL(MUL.ADJUSTER_CODE,'0')                            
   WHERE MUL.USER_ID=@USER_ID AND (USER_TYPE_ID=46 OR USER_SYSTEM_ID='W001')                                         --CA.ADJUSTER_CODE=@ADJUSTER_ID //Commented by Asfa 30-Aug-2007                     
   AND CAA.LOB_ID=@LOB_ID  AND CAA.IS_ACTIVE='Y'            
 --Commented for Itrack Issue 6101 on 16 Sept 09--- No need to check adjuster limit                                
                                                          
--   IF(@PAYMENT_AMOUNT <= @PAYMENT_LIMIT) --CURRENT ADJUSTER IS WITHIN LIMITS TO AUTHORIZE                                          
--   BEGIN                                                    
--    --Moved here for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed  only when there is suitable Ajuster/User which the limit to complete the activity - 30 July 09           

--   IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                             
--   BEGIN                                                                    
--    --Update the status of activity as complete                                                          
--    -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                         
--    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                                                  
--    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                    
--                                                                          
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
--              
--              
--    UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                               
--    EXEC PROC_COMPLETECLAIMPAYMENT   @CLAIM_ID ,  @ACTIVITY_ID,@USER_ID,               
--    @ADJUSTER_ID_PARAM,                            
--    @NOTIFY_USER                                                                    
--          
--    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                     
--    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                  
--                                
--    set @RETURN_VALUE = 1                                                                                 
--                                                                   
--  END                                                
--  ELSE                                                                                        
--  BEGIN                                                                  
--   EXEC PROC_COMPLETECLAIMPAYMENT   @CLAIM_ID ,                                                          
--   @ACTIVITY_ID ,                                                   
--   @USER_ID ,               
--   @ADJUSTER_ID_PARAM ,                                                                                 
--   @NOTIFY_USER                                                     
--     END                                                                           
   /*                                                                  
   --Update the status of activity as complete                                                                                               
   -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                    
   UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                                         
   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                                            
                                   
   ------------------------------------------------                                                    
                                                            
   -- Added by Asfa (22-Oct-2007)                                                                      
   /*                                                                      
   Q - For Paid loss-final activity, Outstanding amount sets to zero(0) as soon as we hit the Save button. Should not it be done at Complete activity event?                                
Ans - It should be like all other activities and should be done on the Complete Activity event                                                                 
   */              
   if(@ACTION_ON_PAYMENT=@PAID_LOSS_PARTIAL)                                            
   BEGIN                                                       
 SELECT @SYSTEM_USER_ID = USER_ID FROM MNT_USER_LIST WITH (NOLOCK)  WHERE USER_LOGIN_ID = 'SYSTEM'                  
   SELECT @LAST_UPDATED_DATETIME = GetDate()                                                                      
                                                                
   --  EXEC Proc_InsertCLM_ACTIVITY @CLAIM_ID,@ACTIVITY_ID,@ACTIVITY_REASON,@REASON_DESCRIPTION,@CREATED_BY,@ACTIVITY_STATUS,@RESERVE_TRAN_CODE,@ACTION_ON_PAYMENT                                                                        
   EXEC Proc_InsertCLM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID OUTPUT,11773,'RESERVE UPDATE',@SYSTEM_USER_ID,11801,0,@RESERVE_UPDATE_ACTIVITY                                                                      
                                                                
   -- Added by Asfa (25-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                    
   UPDATE CLM_ACTIVITY SET COMPLETED_DATE=GETDATE()          
   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID                                            
                            
   SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0)+1 FROM CLM_ACTIVITY_RESERVE  WITH (NOLOCK)                                                                           
   WHERE CLAIM_ID=@CLAIM_ID                                                
                         
   --    SELECT @DRACCTS=SELECTEDDEBITLEDGERS, @CRACCTS=SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @RESERVE_UPDATE_ACTIVITY -- RESERVE UPDATE                                                                      
   SELECT @DRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS WITH (NOLOCK)  WHERE ACC_NUMBER=(SELECT SELECTEDDEBITLEDGERS FROM CLM_TYPE_DETAIL WITH (NOLOCK)  WHERE DETAIL_TYPE_ID = @RESERVE_UPDATE_ACTIVITY)                                                          


















 SELECT @CRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS WITH (NOLOCK)  WHERE ACC_NUMBER=(SELECT SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL WITH (NOLOCK)  WHERE DETAIL_TYPE_ID = @RESERVE_UPDATE_ACTIVITY)                               

DECLARE CUR CURSOR                                                        
   FOR SELECT COVERAGE_ID,PRIMARY_EXCESS,MCCA_ATTACHMENT_POINT,MCCA_APPLIES,ATTACHMENT_POINT,CAR.OUTSTANDING - CAP.PAYMENT_AMOUNT AS OUTSTANDING,                                                                      
   REINSURANCE_CARRIER,RI_RESERVE,CAR.VEHICLE_ID,POLICY_LIMITS,RETENTION_LIMITS                                                   
   FROM CLM_ACTIVITY_RESERVE CAR  WITH (NOLOCK) JOIN CLM_ACTIVITY_PAYMENT CAP ON CAR.RESERVE_ID = CAP.RESERVE_ID        
   AND CAR.CLAIM_ID = CAP.CLAIM_ID                                                                      
   WHERE CAR.CLAIM_ID=@CLAIM_ID                                            
   OPEN CUR                               
   FETCH NEXT FROM CUR                                                                       
   INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,                   
   @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,                  
   @RETENTION_LIMITS                                                                      
                                    
 
   WHILE @@FETCH_STATUS = 0                                                
   BEGIN                           
                                                                
   EXEC Proc_InsertCLM_ACTIVITY_RESERVE @CLAIM_ID ,@SYSTEM_ACTIVITY_ID,@COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,                                                                      
   @MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@SYSTEM_USER_ID,@LAST_UPDATED_DATETIME,                                                                      
 @VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,@RESERVE_UPDATE_ACTIVITY,@CRACCTS,@DRACCTS, @TRANSACTION_ID                                                                      
                                                                
                            
   FETCH NEXT FROM CUR                                                                       
   INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,                                                                      
   @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,                 
   @RETENTION_LIMITS                                                                
                                                                
END                                                                      
   CLOSE CUR                                                                      
   DEALLOCATE CUR        
                   
   --  EXEC Proc_UpdateActivityReserve @CLAIM_ID,@ACTIVITY_ID,@RESERVE_AMOUNT,@EXPENSES,@RECOVERY,@PAYMENT_AMOUNT,@RI_RESERVE,@MODIFIED_BY,@LAST_UPDATED_DATETIME                                                   
   SET @RESERVE_AMOUNT = -@TOTAL_CLAIM_PAYMENTS                                                                       
   EXEC Proc_UpdateActivityReserve @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@RESERVE_AMOUNT,NULL,NULL,0,0,NULL,@LAST_UPDATED_DATETIME                                                                      
                                                                
   SELECT @CLAIM_NUMBER = CLAIM_NUMBER FROM CLM_CLAIM_INFO  WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID                
   SELECT TOP 1 @DIV_ID=DIV_ID,@DEPT_ID=DEPT_ID,@PC_ID=PC_ID FROM ACT_ACCOUNTS_POSTING_DETAILS  WITH (NOLOCK) WHERE SOURCE_ROW_ID=@CLAIM_ID AND SOURCE_NUM=@CLAIM_NUMBER                                                                      
   --  SELECT @DEBIT_ACCOUNT_ID=CRACCTS, @CREDIT_ACCOUNT_ID=DRACCTS FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                    

 --  EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@ACTIVITY_ID,@USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DEBIT_ACCOUNT_ID,@CREDIT_ACCOUNT_ID,                                                                      
   EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@SYSTEM_USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DRACCTS,@CRACCTS                                                                      
                                                              
   END                                                                      
                                                             
   if(@ACTION_ON_PAYMENT=@PAID_LOSS_FINAL)                                               
   BEGIN                                         
                                
   SELECT @SYSTEM_USER_ID = USER_ID FROM MNT_USER_LIST  WITH (NOLOCK) WHERE USER_LOGIN_ID = 'SYSTEM'                        
   SELECT @LAST_UPDATED_DATETIME = GetDate()                                                                      
    
   --  EXEC Proc_InsertCLM_ACTIVITY @CLAIM_ID,@ACTIVITY_ID,@ACTIVITY_REASON,@REASON_DESCRIPTION,@CREATED_BY,@ACTIVITY_STATUS,@RESERVE_TRAN_CODE,@ACTION_ON_PAYMENT                                                      
   EXEC Proc_InsertCLM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID OUTPUT,11773,'CLOSE RESERVE',@SYSTEM_USER_ID,11801,0,@CLOSE_RESERVE                                                                      
                                                                
   -- Added by Asfa (25-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                    
   UPDATE CLM_ACTIVITY SET COMPLETED_DATE=GETDATE()                                                                     
   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID                                                    
      
   SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0)+1 FROM CLM_ACTIVITY_RESERVE  WITH (NOLOCK)                                                                             
   WHERE CLAIM_ID=@CLAIM_ID                                                                        
                                                                
   --    SELECT @DRACCTS=SELECTEDDEBITLEDGERS, @CRACCTS=SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE -- CLOSE RESERVE                                                                      
   SELECT @DRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS  WITH (NOLOCK) WHERE ACC_NUMBER=(SELECT SELECTEDDEBITLEDGERS FROM CLM_TYPE_DETAIL  WITH (NOLOCK) WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE)                                                                 
  SELECT @CRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS WITH (NOLOCK) WHERE ACC_NUMBER=(SELECT SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL  WITH (NOLOCK) WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE)                                         

                                                             
   DECLARE CUR CURSOR                              
   FOR SELECT COVERAGE_ID,PRIMARY_EXCESS,MCCA_ATTACHMENT_POINT,MCCA_APPLIES,ATTACHMENT_POINT,        
   CAR.OUTSTANDING,REINSURANCE_CARRIER,RI_RESERVE,CAR.VEHICLE_ID,POLICY_LIMITS,RETENTION_LIMITS                                                  
   FROM CLM_ACTIVITY_RESERVE CAR  WITH (NOLOCK) JOIN CLM_ACTIVITY_PAYMENT CAP ON CAR.RESERVE_ID = CAP.RESERVE_ID    
   AND CAR.CLAIM_ID = CAP.CLAIM_ID                                                                      
   WHERE CAR.CLAIM_ID=@CLAIM_ID AND CAP.ACTIVITY_ID=@ACTIVITY_ID                                                                 
   OPEN CUR                                                                      
   FETCH NEXT FROM CUR                                                                       
   INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,                                                                      
   @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,                                                                      
   @RETENTION_LIMITS                                                 
                       
                                                                
   WHILE @@FETCH_STATUS = 0                        
   BEGIN                                               
                                  
   EXEC Proc_InsertCLM_ACTIVITY_RESERVE @CLAIM_ID ,@SYSTEM_ACTIVITY_ID,@COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,                                 
@MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@SYSTEM_USER_ID,@LAST_UPDATED_DATETIME,                                 
   @VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,@CLOSE_RESERVE,@CRACCTS,@DRACCTS, @TRANSACTION_ID                                                                      
                                                                
                
   FETCH NEXT FROM CUR                                                                       
   INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,                                                                      
   @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,        
   @RETENTION_LIMITS                                                     
                                                                
   END                                                 
   CLOSE CUR                                                                      
   DEALLOCATE CUR                                                                      
                                                                
   --  EXEC Proc_UpdateActivityReserve @CLAIM_ID,@ACTIVITY_ID,@RESERVE_AMOUNT,@EXPENSES,@RECOVERY,@PAYMENT_AMOUNT,@RI_RESERVE,@MODIFIED_BY,@LAST_UPDATED_DATETIME                                           
   SELECT @RESERVE_AMOUNT = -SUM(ISNULL(OUTSTANDING,0))                                     
   FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK) WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID             
                                                                
EXEC Proc_UpdateActivityReserve @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@RESERVE_AMOUNT,NULL,NULL,0,0,NULL,@LAST_UPDATED_DATETIME                                                                      
                                                                
   SELECT @CLAIM_NUMBER = CLAIM_NUMBER FROM CLM_CLAIM_INFO  WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID                       
   SELECT TOP 1 @DIV_ID=DIV_ID,@DEPT_ID=DEPT_ID,@PC_ID=PC_ID FROM ACT_ACCOUNTS_POSTING_DETAILS WITH (NOLOCK)  WHERE SOURCE_ROW_ID=@CLAIM_ID AND SOURCE_NUM=@CLAIM_NUMBER                                                                      
   -- SELECT @DEBIT_ACCOUNT_ID=CRACCTS, @CREDIT_ACCOUNT_ID=DRACCTS FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                               
   --  EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@ACTIVITY_ID,@USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DEBIT_ACCOUNT_ID,@CREDIT_ACCOUNT_ID,       
   EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@SYSTEM_USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DRACCTS,@CRACCTS                                 
                                                                
   UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=0 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID                                                                      
   END                                                                      
                  
                                           
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                                                   
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                         
                                                                
                                                          
   --When the activity is being completed, check whether the activity is crossing the notification limit                                                                 
   if(@PAYMENT_AMOUNT>@NOTIFY_LIMIT)                                    
   begin                                                                 
   --Diary entry will be made at page level now using the Diary Setup under Maintenance                                            
   --ADD DIARY ENTRY                                  
   */  /*INSERT INTO TODOLIST 
   (                                                                                          
   RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                                     
   POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                          
   FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                                                    
   FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                                                                                                          
   )                                                                             
   VALUES                           
   (                                                                              
   NULL,GETDATE(),DATEADD(DAY,7,GETDATE()),@PAYMENT_LIMIT_TYPE_ID,                                    
   NULL,'Notification limit for Activity exceeded','Y',                              
   NULL,'M',@WOLVERINE_USER1,@USER_ID,NULL,NULL,'Notification limit for the Activity has been exceeded',NULL,NULL,@CLAIM_ID,NULL,NULL,NULL,                                                               
   @CUSTOMER_ID,NULL,NULL,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE                    
   )                    
                                                                
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                     
   --ADD DIARY ENTRY                                               
   INSERT INTO TODOLIST                                              
   (                                                                                                          
   RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                                                      
   POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                                           
   FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                                                                               
   FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                                        
   )                                                                                                          
   VALUES                                                                                  
   (                                                                
   NULL,GETDATE(),DATEADD(DAY,7,GETDATE()),@PAYMENT_LIMIT_TYPE_ID,                                                                                                          
   NULL,'Notification limit for Activity exceeded','Y',                                                  
   NULL,'M',@WOLVERINE_USER2,@USER_ID,NULL,NULL,'Notification limit for the Activity has been exceeded',NULL,NULL,@CLAIM_ID,NULL,NULL,NULL,                                                                                 
   @CUSTOMER_ID,NULL,NULL,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE         
   )  */ /*                                                                                
SET @NOTIFY_USER = 1                                                                                  
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                       
   end         
                                     
                                                                
   set @RETURN_VALUE = 1                                            
   */                                         
  --END                                                             
  --Added for Itrack Issue 6101 on 16 July --- Check for user limit                                                            
  --ELSE IF(@PAYMENT_AMOUNT <= @USER_LOGGED_PAYMENT_LIMIT)            
  IF(@PAYMENT_AMOUNT <= @USER_LOGGED_PAYMENT_LIMIT) --CURRENT ADJUSTER IS WITHIN LIMITS TO AUTHORIZE                                     
  BEGIN  
  --Moved here for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed  only when there is suitable Ajuster/User which the limit to complete the activity - 30 July 09                      

IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                                                                              
   BEGIN       
	                                                 
    --Update the status of activity as complete                                                                                   
  -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                              
    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(),ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED                     
    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                    
                                                                          
    SELECT @TEMP_ERROR_CODE = @@ERROR                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                             
                   
    UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                               
    EXEC PROC_COMPLETECLAIMPAYMENT   @CLAIM_ID ,                                                     
	@ACTIVITY_ID ,                                                  
    
	@USER_ID  , 

	@ADJUSTER_ID_PARAM ,                                                                      
    @NOTIFY_USER,
	@ACCOUNTING_SUPPRESSED                                                                  
                                                                 
    SELECT @TEMP_ERROR_CODE = @@ERROR         
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                            
                                           
   SET @RETURN_VALUE = 1                                                                                 
       
  END                                              
  ELSE                                                                                        
  BEGIN                  
   EXEC PROC_COMPLETECLAIMPAYMENT   @CLAIM_ID ,                                              
   @ACTIVITY_ID ,                                                            
	@USER_ID ,                         
 @ADJUSTER_ID_PARAM ,                                            
   @NOTIFY_USER,
	@ACCOUNTING_SUPPRESSED             
     END                                  
                                                       
  END                                                                      
  ELSE                                                                      
  BEGIN                                            
                                                                                     
   --When the current adjuster is not within limits to authorize transaction, first check if there are adjusters                                 
   --meeting the limits..Make a diary entry if there are adjusters                                                           
                                                                
   --When the current adjuster is not within limits to authorize transaction, first check if there are adjusters    
   --meeting the limits..Make a diary entry if there are adjusters                                                                    
  /* IF EXISTS(SELECT DISTINCT CA.ADJUSTER_ID FROM CLM_ADJUSTER CA  WITH (NOLOCK) JOIN CLM_ADJUSTER_AUTHORITY CAA          
   --CA.ADJUSTER_CODE FROM CLM_ADJUSTER CA JOIN CLM_ADJUSTER_AUTHORITY CAA //Commented by Asfa 30-Aug-2007                                        
   ON CA.ADJUSTER_ID=CAA.ADJUSTER_ID AND CAA.LOB_ID=@LOB_ID AND CA.IS_ACTIVE='Y' AND CAA.IS_ACTIVE='Y'             
   AND CAA.LIMIT_ID =                                                                                                    
   (                                                                                  
   SELECT  TOP 1 LIMIT_ID FROM CLM_AUTHORITY_LIMIT  WITH (NOLOCK)                                                                          
   WHERE PAYMENT_LIMIT>@PAYMENT_AMOUNT AND IS_ACTIVE='Y' ORDER BY PAYMENT_LIMIT))                                                                                                                           
   BEGIN  */                           
    --Add a diary entry for the new found adjusters          
/* Done for Itrack Issue 6542 on 16 Oct 09
    INSERT into TODOLIST                                                                                                                                           
    (                                                                                                                  RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,                           
  TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                            
  FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                          
 )                                                                                     
    SELECT DISTINCT                                                                        
    null,GetDate(),GetDate(),@PAYMENT_LIMIT_TYPE_ID,null,--'Payment Limit Exceeded'  
 --Done for Itrack Issue 6101 on 17 Sept 09  
 'Payment exceeds Users limit of Authority','Y',            null,'M',                                     
    --  CA.ADJUSTER_CODE,                           
    CA.USER_ID,@ADJUSTER_ID,null,null,--'Payment Limit Exceeded'  
 --Done for Itrack Issue 6101 on 17 Sept 09  
 'Activity could not be completed as the Payment/Reserve exceeds the Users limit of Authority',null,null,@CLAIM_ID,@ACTIVITY_ID,null,null,            @CUSTOMER_ID,null,null,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE  
 
 FROM CLM_ADJUSTER CA WITH (NOLOCK) JOIN CLM_ADJUSTER_AUTHORITY CAA                                                ON CA.ADJUSTER_ID=CAA.ADJUSTER_ID AND CAA.LOB_ID=@LOB_ID AND CA.IS_ACTIVE='Y' AND CAA.IS_ACTIVE='Y'             
 AND CAA.LIMIT_ID =                       
    (                                                              
     SELECT  TOP 1 LIMIT_ID FROM CLM_AUTHORITY_LIMIT  WITH (NOLOCK)                                                 WHERE PAYMENT_LIMIT>@PAYMENT_AMOUNT AND IS_ACTIVE='Y' ORDER BY PAYMENT_LIMIT                          
    )        */                                                                                    
                                                                
    SELECT @TEMP_ERROR_CODE = @@ERROR                                           
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                       
                                                                
    --Change the status of the activity as Awaiting Authorization 
	
	--Done for Itrack Issue 6101 on 12 Oct 09     
    
	/*UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @AWAITING_AUTHORIZATION                           
	WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID*/   
    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_INCOMPLETE                           
	WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                    
          
    SELECT @TEMP_ERROR_CODE = @@ERROR                                     
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
    set @RETURN_VALUE = 4                                                                                                                                                       
  /* END                                                                                           
   ELSE --WE DO NOT HAVE ANY ADJUSTER SATISFYING THE GIVEN CONDITIONS...                                  
     BEGIN                           
    SET @RETURN_VALUE=5 --SETTING THE RETURN VALUE TO 5 TO INDICATE THAT NO ADJUSTER HAS BEEN FOUND                       */                                                                    
    RETURN @RETURN_VALUE --RETURN FROM THE PROCEDURE                         
  -- END                                                       
                                                                
  --END                                                                                              
 END                                                                                                                              
   
 SELECT @TOTAL_CLAIM_PAYMENTS = ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY  WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_REASON=@CLAIM_PAYMENT                     
 --update various fields at claim_info as follows                                                                                                                                    
 UPDATE CLM_CLAIM_INFO SET PAID_LOSS = ISNULL(@TOTAL_CLAIM_PAYMENTS,0),MODIFIED_BY=@ADJUSTER_ID,       
 LAST_UPDATED_DATETIME=GetDate() WHERE CLAIM_ID=@CLAIM_ID                                                                                                          
             
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                               
END                                                               
                       
--------------------------------------------Claim Payment Activity Ends here---------------------------------                                                                                                                       
                         
--------------------------------------------Reserve Update Activity Ends here---------------------------------               
                                                      
--ACTIVITY_REASON is Reserve Update                           
--Look for reserve update, if not there find the previous record.         
--Check the authority limits of current adjuster. If not within limits, find another adjuster and add a diary entry for him/her                                                                                                           
                                   
--With the new/current adjuster having the appropriate limits, update data at claim activity and claim info tables                                                                                          
IF(@ACTIVITY_REASON=@RESERVE_UPDATE OR @ACTIVITY_REASON=@REINSURANCE)                 
BEGIN                                        
 SELECT @TOTAL_RESERVE = ISNULL(SUM(OUTSTANDING),0) FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK)              
 WHERE CLAIM_ID=@CLAIM_ID                                                            
-- if(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                 
-- begin                                                                                                                      
--  --Update the status of activity as complete                                           
--  -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                   
--  UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                                                             
--  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                        
--                                                       
--                                                                
--  SELECT @TEMP_ERROR_CODE = @@ERROR                                                                  
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                
--                                                                
--  UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                          
--                                                                
--                                         
--  SELECT @TEMP_ERROR_CODE = @@ERROR          
--  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                 
--                                                                
--  set @RETURN_VALUE = 1                                                                   
-- end                                              
-- else                                            
-- begin                                                                                                                      
  --Fetch Reserve Limit assigned to current adjuster/ user                                                                      
 --Commented for Itrack Issue 6101 on 16 Sept 09 --- No need to check adjuster limit                    
 /*    
 SELECT @RESERVE_LIMIT = ISNULL(RESERVE_LIMIT,0),@NOTIFY_LIMIT=ISNULL(NOTIFY_AMOUNT,0)                           
 FROM CLM_ADJUSTER_AUTHORITY CAA WITH (NOLOCK) JOIN CLM_AUTHORITY_LIMIT CAL              
 ON CAA.LIMIT_ID = CAL.LIMIT_ID LEFT JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID                      WHERE CA.ADJUSTER_ID=@ADJUSTER_ID 
 CA.ADJUSTER_CODE=@ADJUSTER_ID //Commented by Asfa 30-Aug-2007      
    AND CAA.LOB_ID=@LOB_ID  AND CAA.IS_ACTIVE='Y'  */                                                            
                                                          
   --Added for Itrack Issue 6101 on 16 July --- Check for user limit                                                            
 SELECT @USER_LOGGED_RESERVE_LIMIT = ISNULL(RESERVE_LIMIT,0),@USER_LOGGED_NOTIFY_LIMIT=ISNULL(NOTIFY_AMOUNT,0)     
 FROM CLM_ADJUSTER_AUTHORITY CAA WITH (NOLOCK) JOIN CLM_AUTHORITY_LIMIT CAL       
 ON CAA.LIMIT_ID = CAL.LIMIT_ID                
 LEFT JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID AND CA.IS_ACTIVE='Y'                             
 LEFT OUTER JOIN MNT_USER_LIST MUL ON CA.USER_ID=MUL.USER_ID                          
 --Done for Itrack Issue 6285 on 25 Aug 2009   
 --Commented for Itrack Issue 6542 on 15 Oct 09                          
 --AND isnull(CA.ADJUSTER_CODE,'0') = isnull(MUL.ADJUSTER_CODE,'0')                                       
 WHERE MUL.USER_ID=@USER_ID AND (MUL.USER_TYPE_ID=46 OR MUL.USER_SYSTEM_ID = 'W001')                               --CA.ADJUSTER_CODE=@ADJUSTER_ID //Commented by Asfa 30-Aug-2007                                                                
AND CAA.LOB_ID=@LOB_ID  AND CAA.IS_ACTIVE='Y'              
  --Check whether the current adjuster is within limits to authorise reserve transaction of the given amount                     
 --Commented for Itrack Issue 6101 on 16 Sept 09 --- No need to check adjuster limit                     
                                                             
/*if((@RESERVE_AMOUNT <= @RESERVE_LIMIT)) --Current adjuster/logged in user is within limits to authorize                                                                 
  begin                                                   
   IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)             
   BEGIN                     
   UPDATE THE STATUS OF ACTIVITY AS COMPLETE                                                             
   MODIFIED BY ASFA (15-JULY-2008) - AS PER MAIL SENT BY RAJAN SIR DATED 12-JULY-2008                                                                    
   UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                            WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                           

  SELECT @TEMP_ERROR_CODE = @@ERROR      
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                            
                                                                 
   UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                          
                                                                 
                                                                 
   SELECT @TEMP_ERROR_CODE = @@ERROR                                  
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                       
                                                                 
   SET @RETURN_VALUE = 1                                                                                                                
   END                       
   ELSE                                   
   BEGIN                                   
    --Update the status of activity as complete                                                                                                                 
    -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008                                                                       
    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE()                         
    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                            
                         
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                                                      
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                  
                                                                
  --When the activity is being completed, check whether the activity is crossing the notification limit                                                                      
    if(@RESERVE_AMOUNT>@NOTIFY_LIMIT)  
    begin                                                 
    --Diary entry will be made at page level now using the Diary Setup under Maintenance                                       
    --ADD DIARY ENTRY                                                                                                          
    /*INSERT INTO TODOLIST                                                                                               
    (                                                                                                          
    RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                                                                        
    POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,           
    FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                                                                                          
   FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                                                                          
    )                                                                                       
    VALUES                                                                                                     
    (               
    NULL,GETDATE(),DATEADD(DAY,7,GETDATE()),@RESERVE_LIMIT_TYPE_ID,                                                                     
    NULL,'Notification limit for Activity exceeded','Y',                                                            
    NULL,'M',@WOLVERINE_USER1,@USER_ID,NULL,NULL,'Notification limit for the Activity has been exceeded',NULL,NULL,@CLAIM_ID,NULL,NULL,NULL,                                                                                     
    @CUSTOMER_ID,NULL,NULL,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE                                                                                                          
    )                                                                                         
                                              
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                       
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                             
--ADD DIARY ENTRY                                                                                    
    INSERT INTO TODOLIST              
    ( 
    RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,                                                                                                          
    POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                                                                                          
    FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                     
    FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID                                                                                            
    )                                              
    VALUES                                                                            
    (              
    NULL,GETDATE(),DATEADD(DAY,7,GETDATE()),@RESERVE_LIMIT_TYPE_ID,                                                           
    NULL,'Notification limit for Activity exceeded','Y',                                                                                                          
    NULL,'M',@WOLVERINE_USER2,@USER_ID,NULL,NULL,'Notification limit for the Activity has been exceeded',NULL,NULL,@CLAIM_ID,NULL,NULL,NULL,                                  
    @CUSTOMER_ID,NULL,NULL,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE                                       
    )     */                         
    SET @NOTIFY_USER = 1                                                                                    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                               
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                          
   END                                        
                                                                                            
   SET @RETURN_VALUE = 1                        
  END                                                     
  END     */                                                      
--Added for Itrack Issue 6101 on 16 July --- Check for user limit            
 --Commented for Itrack Issue 6101 on 16 Sept 09 --- No need to check adjuster limit                     
--  else if((@RESERVE_AMOUNT <= @USER_LOGGED_RESERVE_LIMIT)) --Current adjuster/logged in user is within limits to            
  
IF((@RESERVE_AMOUNT <= @USER_LOGGED_RESERVE_LIMIT)) --CURRENT ADJUSTER/LOGGED IN USER IS WITHIN LIMITS TO AUTHORIZE                                             
  BEGIN  
                                  
 --Moved here for Itrack 6101 and moved in below condition so that Activity in Authorisation queue is completed  only when there is suitable Ajuster/User which the limit to complete the activity - 30 July 09                                    
   IF(@ACTIVITY_STATUS=@AWAITING_AUTHORIZATION)                                                                                                                  
   BEGIN                                                             
   --UPDATE THE STATUS OF ACTIVITY AS COMPLETE                                               
   -- MODIFIED BY ASFA (15-JULY-2008) - AS PER MAIL SENT BY RAJAN SIR DATED 12-JULY-2008  
   -- Done for Itrack Issue 7169 on 12 Aug 2010
   -- When Paid Loss, Final is reversed and the Followup Re-open activity is Incomplete and we go to complete the re-open activity,
   -- the activity should be visible as a reversed activity(in Purple color),i.e. is ACCOUNTING_SUPPRESSED should be 1
   -- which currently becomes 0 when we complete the activity as suppress accounting checkbox is not visible for re-open activities 
   --Commented as this being now handled from page logic
	--   SELECT @ACCOUNTING_SUPPRESSED = ACCOUNTING_SUPPRESSED FROM CLM_ACTIVITY WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID                                                                 
   UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(), ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED
   WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                    
                                             
   SELECT @TEMP_ERROR_CODE = @@ERROR                                                                  
   IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                            
                                                                 
   UPDATE TODOLIST SET LISTOPEN='N' WHERE CLAIMID=@CLAIM_ID AND CLAIMMOVEMENTID=@ACTIVITY_ID                                                                                          
   SELECT @TEMP_ERROR_CODE = @@ERROR                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                       
    SET @RETURN_VALUE = 1                                                                                 
   END                       
   ELSE                                   
   BEGIN                                                            
    --Update the status of activity as complete                                             
    -- Modified by Asfa (15-July--2008) - As per mail sent by Rajan sir dated 12-july-2008
	 -- Done for Itrack Issue 7169 on 12 Aug 2010
   -- When Paid Loss, Final is reversed and the Followup Re-open activity is Incomplete and we go to complete the re-open activity,
   -- the activity should be visible as a reversed activity(in Purple color),i.e. is ACCOUNTING_SUPPRESSED should be 1
   -- which currently becomes 0 when we complete the activity as suppress accounting checkbox is not visible for re-open activities 
	--Commented as this being now handled from page logic
    --SELECT @ACCOUNTING_SUPPRESSED = ACCOUNTING_SUPPRESSED FROM CLM_ACTIVITY WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID  
	UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(), ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED      
    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                             

	SELECT @TEMP_ERROR_CODE = @@ERROR                          
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM        
                                               
 --When the activity is being completed, check whether the activity is crossing the logged user notification limit                                                      
    IF(@RESERVE_AMOUNT>@USER_LOGGED_NOTIFY_LIMIT)                  
    BEGIN                    
                                                                    
    SET @NOTIFY_USER = 1              
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                    
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                         
                                                                      
   END                                                                   
   SET @RETURN_VALUE = 1                                                                                                           
  END                                                    
  END --6101                
  ELSE                                                                                                                                                                     
  BEGIN                        
   --When the current adjuster is not within limits to authorize transaction, first check if there are adjusters   
   --meeting the limits..Make a diary entry if there are adjusters                                            
 --Commented for Irack Issue 6101 on 17 Sept 09                
   /*IF EXISTS(SELECT DISTINCT CA.ADJUSTER_ID FROM CLM_ADJUSTER CA  WITH (NOLOCK) JOIN CLM_ADJUSTER_AUTHORITY CAA                  
   --CA.ADJUSTER_CODE FROM CLM_ADJUSTER CA JOIN CLM_ADJUSTER_AUTHORITY CAA //Commented by Asfa 30-Aug-2007                                  
   ON CA.ADJUSTER_ID=CAA.ADJUSTER_ID AND CAA.LOB_ID=@LOB_ID AND CA.IS_ACTIVE='Y' AND CAA.IS_ACTIVE='Y'             
   AND CAA.LIMIT_ID =                                                                                  
   (                                                                                                   
    SELECT  TOP 1 LIMIT_ID FROM CLM_AUTHORITY_LIMIT  WITH (NOLOCK)                              
    WHERE RESERVE_LIMIT>@RESERVE_AMOUNT AND IS_ACTIVE='Y' ORDER BY RESERVE_LIMIT))                                                            
   begin      */                                                                      
    --Add a diary entry for the new found adjusters                                                                   
	/* Done for Itrack Issue 6542 on 16 Oct 09                                              
    INSERT into TODOLIST                                                                                                                                                                         
    (       
    RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,        TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,            FROMENTITYID,
	CUSTOMER_ID,
	APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,MODULE_ID)            
  SELECT DISTINCT                                                               
    null,GetDate(),GetDate(),@RESERVE_LIMIT_TYPE_ID,null,--'Reserves Limit Exceeded'       
 --Done for Itrack Issue 6101 on 17 Sept 09     
 'Reserve exceeds Users limit of Authority','Y', null,'M',                                                    
    --   CA.ADJUSTER_CODE,                                          
   CA.USER_ID,                                                                     
    @ADJUSTER_ID,null,null,--'Reserves Limit Exceeded'    
--Done for Itrack Issue 6101 on 17 Sept 09        
 'Activity could not be completed as the Reserve exceeds the Users limit of Authority',null,null,          
 @CLAIM_ID,@ACTIVITY_ID,null,null,                                                                                 @CUSTOMER_ID,null,null,@POLICY_ID,@POLICY_VERSION_ID,@CLAIM_MODULE                                 
    FROM CLM_ADJUSTER CA  WITH (NOLOCK) JOIN CLM_ADJUSTER_AUTHORITY CAA                                               ON CA.ADJUSTER_ID=CAA.ADJUSTER_ID AND CAA.LOB_ID=@LOB_ID AND CA.IS_ACTIVE='Y' AND CAA.IS_ACTIVE='Y'            
   AND CAA.LIMIT_ID =                                                                                                (                                                                  
     SELECT  TOP 1 LIMIT_ID FROM CLM_AUTHORITY_LIMIT   WITH (NOLOCK)                                                   WHERE RESERVE_LIMIT>@RESERVE_AMOUNT AND IS_ACTIVE='Y' ORDER BY RESERVE_LIMIT)       */                            
	
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                           
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                     
   

 --Change the status of the activity as Awaiting Authorization                                                        --Done for Itrack Issue 6101 on 12 Oct 09
    
	/*UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @AWAITING_AUTHORIZATION WHERE CLAIM_ID=@CLAIM_ID             
    AND ACTIVITY_ID=@ACTIVITY_ID */         
    UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_INCOMPLETE WHERE CLAIM_ID=@CLAIM_ID             
 AND ACTIVITY_ID=@ACTIVITY_ID     
                                                                        
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                      
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
                                      
    SET @RETURN_VALUE = 4                        
--   END                                                                    
--   ELSE --WE DO NOT HAVE ANY ADJUSTER SATISFYING THE GIVEN CONDITIONS...      
--   BEGIN                                                                                        
--    --  ROLLBACK TRAN                                                    
    --SET @RETURN_VALUE=5 --SETTING THE RETURN VALUE TO 5 TO INDICATE THAT NO ADJUSTER HAS BEEN FOUND                                            
    RETURN @RETURN_VALUE --RETURN FROM THE PROCEDURE                                                                                       
  -- END                                                                                          
  END                                                                                    
  -- END   6101                                                                 
  --Get the sum of all claim payments whose action on payment is marked as Reduce Reserve by Payment Amount                              
  SELECT @ACTION_PAYMENTS=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_PAYMENT  WITH (NOLOCK)         
           WHERE CLAIM_ID=@CLAIM_ID AND   ACTION_ON_PAYMENT = @REDUCE_RESERVE_BY_PAYMENT_ACTION                                                      







                                                                      
  --Get the sum of total outstandings for the claim from reserve table                                                                                              
  SELECT @OUTSTANDING = ISNULL(SUM(OUTSTANDING),0) FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK)             
  WHERE CLAIM_ID=@CLAIM_ID                                  
                                       
  SET @TOTAL_OUTSTANDING = ISNULL((@OUTSTANDING - @ACTION_PAYMENTS),0)                                                                            
                                                                                                 
  --update various fields at claim_info as follows                                                                                  
  UPDATE CLM_CLAIM_INFO SET OUTSTANDING_RESERVE = ISNULL(@TOTAL_OUTSTANDING,0),MODIFIED_BY=@ADJUSTER_ID, LAST_UPDATED_DATETIME=GetDate() WHERE CLAIM_ID=@CLAIM_ID                                                           
     
  SELECT @TEMP_ERROR_CODE = @@ERROR     
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
                                                                                 
 end                                 
  
--------------------------------------------Reserve Update Activity Ends here---------------------------------    
          
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                 
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                                    
                              
                                                                       
-- COMMIT TRAN                                              
                                                 
 PROBLEM:                          
 IF (@TEMP_ERROR_CODE <> 0)                                           
  BEGIN                                                                                                                                                    
--     ROLLBACK TRAN                     
   SET @RETURN_VALUE = -1                                                        
   return @RETURN_VALUE                                                        
  END    
 ELSE                                                                                                                  
   RETURN @RETURN_VALUE                                                                          
 
 END 
                                                                
                                     
--go                                    
--declare @NOTIFY int                      
----exec  Proc_CompleteClaimActivity 999,2,279,0, @NOTIFY out                                                                 
--exec  Proc_CompleteClaimActivity 2974,6,334,0 , @NOTIFY out,1             
----select @NOTIFY as NOTIFY                                                               
--rollback tran


GO

