IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CompleteClaimPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CompleteClaimPayment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.Proc_CompleteClaimPayment
--go
/*----------------------------------------------------------                                                                          
Proc Name       : dbo.Proc_CompleteClaimPayment                                                                    
Created by      : Mohit Agarwal                                                                        
Date            : 15/09/2008                                                                          
Purpose         : Commit Claims Payment Activity for Paid Loss Partial and Final                                                   
Revison History :                                                                          
Used In        : Wolverine                                                                          
                                                                            
Date     Review By          Comments                                                                          
------   ------------       -------------------------*/                    
--drop proc dbo.Proc_CompleteClaimPayment              
                      
CREATE PROC [dbo].[Proc_CompleteClaimPayment]                                                                                                                                                      
(                                                                                                                                                      
 @CLAIM_ID     int,                                                                                                                                                      
 @ACTIVITY_ID  int,                                                                        
 @USER_ID int = null,                    
 @ADJUSTER_ID_PARAM int = null,                    
 -- @WOLVERINE_USER1 INT = null,                                                                                                                                    
 -- @WOLVERINE_USER2 INT = null                    
 @NOTIFY_USER int = null output ,
 @ACCOUNTING_SUPPRESSED int                   
)                                                                                                                                                      
AS                                                                                                                                                      
BEGIN                               
                              
 --begin tran        
 DECLARE @CLAIM_NUMBER VARCHAR(10)        
 DECLARE @DIV_ID int          
 DECLARE @DEPT_ID int          
 DECLARE @PC_ID int          
  
 DECLARE @MCCA_ATTACHMENT_POINT DECIMAL(25,2)                                                        
 DECLARE @MCCA_APPLIES DECIMAL(25,2)          
 DECLARE @ATTACHMENT_POINT DECIMAL(25,2)                                                        
 DECLARE @PRIMARY_EXCESS VARCHAR                                                        
 DECLARE @COVERAGE_ID INT        
 DECLARE @REINSURANCE_CARRIER INT        
 DECLARE @RI_RESERVE DECIMAL(25,2)        
 DECLARE @VEHICLE_ID INT        
 DECLARE @POLICY_LIMITS DECIMAL(25,2)        
 DECLARE @RETENTION_LIMITS DECIMAL(25,2)        
 DECLARE @CRACCTS int       
 DECLARE @DRACCTS int        
  
 DECLARE @TRANSACTION_ID INT        
 DECLARE @TEMP_ERROR_CODE INT                                  
 declare @RETURN_VALUE int          
 declare @RESERVE_AMOUNT decimal(25,2)                                                                                                          
 declare @PAYMENT_AMOUNT decimal(25,2)          
 declare @NOTIFY_LIMIT decimal(25,2)                                                                              
 declare @TOTAL_CLAIM_PAYMENTS decimal(25,2)           
 declare @OUTSTANDING decimal(25,2)                        
 declare @ACTION_ON_PAYMENT int       
 DECLARE @SYSTEM_USER_ID INT        
 DECLARE @LAST_UPDATED_DATETIME DATETIME         
 DECLARE @SYSTEM_ACTIVITY_ID INT         
 declare @RESERVE_UPDATE_ACTIVITY int        
 declare @ACTIVITY_COMPLETE int                                                                                                                            
 DECLARE @CLOSE_RESERVE INT        
 DECLARE @PAID_LOSS_FINAL int                                                      
 DECLARE @PAID_LOSS_PARTIAL int        
 declare @ACTIVITY_REASON int                                                        
 declare @ACTIVITY_STATUS int  
 DECLARE @ACTUAL_RISK_ID INT
 DECLARE @ACTUAL_RISK_TYPE VARCHAR(10)
                                                  
     
 SET @CLOSE_RESERVE = 167       
 set @RESERVE_UPDATE_ACTIVITY = 205        
 SET @PAID_LOSS_FINAL = 181               
 SET @PAID_LOSS_PARTIAL = 180        
 set @NOTIFY_USER = 0                    
 set @RETURN_VALUE = 1                                                          
  
  
 set @ACTIVITY_COMPLETE = 11801 --lookup unique id for Complete Activity Status         
 --Added By Praveen       
  
 SELECT @RESERVE_AMOUNT=ISNULL(CLAIM_RESERVE_AMOUNT,0),@PAYMENT_AMOUNT=ISNULL(PAYMENT_AMOUNT,0),                                                                            
 @ACTIVITY_REASON=ISNULL(ACTIVITY_REASON,0),@ACTIVITY_STATUS=ISNULL(ACTIVITY_STATUS,0),                    
 @ACTION_ON_PAYMENT=ISNULL(ACTION_ON_PAYMENT,0)                    
 FROM CLM_ACTIVITY  WITH (NOLOCK)  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID                                                                        
                                           
 --No valid value for Activity Reason being found, lets return                                                                                                                          
 if (@ACTIVITY_REASON is null or @ACTIVITY_REASON=0)                                                          
 BEGIN                        
  --   ROLLBACK TRAN                                                                                        
  SET @RETURN_VALUE = -1                                                                     
  return @RETURN_VALUE                                                                                        
 end                                                                        
                                                                                                                             
                                                                                 
 --Update the status of activity as complete                                     
 -- Modified by Asfa (15-July-2008) - As per mail sent by Rajan sir dated 12-july-2008      
 UPDATE CLM_ACTIVITY SET ACTIVITY_STATUS = @ACTIVITY_COMPLETE, COMPLETED_DATE=GETDATE(),ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED       
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
  UPDATE CLM_ACTIVITY SET COMPLETED_DATE=GETDATE(),ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED       
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID                  
  
  SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0)+1 FROM CLM_ACTIVITY_RESERVE  WITH (NOLOCK)               
  WHERE CLAIM_ID=@CLAIM_ID          
  
  --    SELECT @DRACCTS=SELECTEDDEBITLEDGERS, @CRACCTS=SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @RESERVE_UPDATE_ACTIVITY -- RESERVE UPDATE     
  --Added for Itrack Issue 6372 on 29 Sept 09     
  SELECT @DRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS WITH (NOLOCK)  WHERE ACC_DISP_NUMBER=(SELECT SELECTEDDEBITLEDGERS FROM CLM_TYPE_DETAIL WITH (NOLOCK)  WHERE DETAIL_TYPE_ID = @RESERVE_UPDATE_ACTIVITY)        
  SELECT @CRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS WITH (NOLOCK)  WHERE ACC_DISP_NUMBER=(SELECT SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL WITH (NOLOCK)  WHERE DETAIL_TYPE_ID = @RESERVE_UPDATE_ACTIVITY)        
  
  DECLARE CUR CURSOR        
  FOR SELECT COVERAGE_ID,PRIMARY_EXCESS,MCCA_ATTACHMENT_POINT,MCCA_APPLIES,ATTACHMENT_POINT,  
  CAR.OUTSTANDING - CAP.PAYMENT_AMOUNT AS OUTSTANDING,        
  REINSURANCE_CARRIER,RI_RESERVE,CAR.VEHICLE_ID,POLICY_LIMITS,RETENTION_LIMITS,CAR.ACTUAL_RISK_ID,CAR.ACTUAL_RISK_TYPE        
  FROM CLM_ACTIVITY_RESERVE CAR  WITH (NOLOCK) JOIN CLM_ACTIVITY_PAYMENT CAP ON CAR.RESERVE_ID = CAP.RESERVE_ID         
  AND CAR.CLAIM_ID = CAP.CLAIM_ID        
  WHERE CAR.CLAIM_ID=@CLAIM_ID      
  --Ravindra(11-25-2008):   
  AND CAP.ACTIVITY_ID = @ACTIVITY_ID   
  
  OPEN CUR        
  
  FETCH NEXT FROM CUR         
  INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,        
  @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,        
  @RETENTION_LIMITS,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE        
  
  
  WHILE @@FETCH_STATUS = 0        
  BEGIN        
         
   EXEC Proc_InsertCLM_ACTIVITY_RESERVE @CLAIM_ID ,@SYSTEM_ACTIVITY_ID,@COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,        
   @MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@SYSTEM_USER_ID,@LAST_UPDATED_DATETIME,        
   @VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,@RESERVE_UPDATE_ACTIVITY,@CRACCTS,@DRACCTS, @TRANSACTION_ID,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE        
  
  
   FETCH NEXT FROM CUR         
   INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,        
   @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,        
   @RETENTION_LIMITS,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE        
         
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
  IF(@ACCOUNTING_SUPPRESSED = 0)
  BEGIN
	EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@SYSTEM_USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DRACCTS,@CRACCTS        
  END
 END        
        
 if(@ACTION_ON_PAYMENT=@PAID_LOSS_FINAL)       
 BEGIN             
  
  SELECT @SYSTEM_USER_ID = USER_ID FROM MNT_USER_LIST  WITH (NOLOCK) WHERE USER_LOGIN_ID = 'SYSTEM'        
  SELECT @LAST_UPDATED_DATETIME = GetDate()        
  
  --  EXEC Proc_InsertCLM_ACTIVITY @CLAIM_ID,@ACTIVITY_ID,@ACTIVITY_REASON,@REASON_DESCRIPTION,@CREATED_BY,@ACTIVITY_STATUS,@RESERVE_TRAN_CODE,@ACTION_ON_PAYMENT          
  EXEC Proc_InsertCLM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID OUTPUT,11773,'CLOSE RESERVE',@SYSTEM_USER_ID,11801,0,@CLOSE_RESERVE        
  
  -- Added by Asfa (25-July-2008) - As per mail sent by Rajan sir dated 12-july-2008      
  UPDATE CLM_ACTIVITY SET COMPLETED_DATE=GETDATE(),ACCOUNTING_SUPPRESSED = @ACCOUNTING_SUPPRESSED        
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID                              
  
  SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0)+1 FROM CLM_ACTIVITY_RESERVE  WITH (NOLOCK)               
  WHERE CLAIM_ID=@CLAIM_ID          
  
  --    SELECT @DRACCTS=SELECTEDDEBITLEDGERS, @CRACCTS=SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE -- CLOSE RESERVE   
  --Added for Itrack Issue 6327 on 29 Sept 09       
  SELECT @DRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS  WITH (NOLOCK) WHERE ACC_DISP_NUMBER=(SELECT SELECTEDDEBITLEDGERS FROM CLM_TYPE_DETAIL  WITH (NOLOCK) WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE)        
  SELECT @CRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS  WITH (NOLOCK) WHERE ACC_DISP_NUMBER=(SELECT SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL  WITH (NOLOCK) WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE)        
  
  
  DECLARE CUR CURSOR        
  FOR SELECT COVERAGE_ID,PRIMARY_EXCESS,MCCA_ATTACHMENT_POINT,MCCA_APPLIES,ATTACHMENT_POINT,        
  CAR.OUTSTANDING,REINSURANCE_CARRIER,RI_RESERVE,CAR.VEHICLE_ID,POLICY_LIMITS,RETENTION_LIMITS,CAR.ACTUAL_RISK_ID,CAR.ACTUAL_RISK_TYPE        
  FROM CLM_ACTIVITY_RESERVE CAR  WITH (NOLOCK) JOIN CLM_ACTIVITY_PAYMENT CAP ON CAR.RESERVE_ID = CAP.RESERVE_ID         
  AND CAR.CLAIM_ID = CAP.CLAIM_ID        
  WHERE CAR.CLAIM_ID=@CLAIM_ID AND CAP.ACTIVITY_ID=@ACTIVITY_ID         
  --Ravindra(11-25-2008):   
  AND CAP.ACTIVITY_ID = @ACTIVITY_ID   
  
  OPEN CUR        
  FETCH NEXT FROM CUR         
  INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,        
  @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,        
  @RETENTION_LIMITS,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE        
  
          
  WHILE @@FETCH_STATUS = 0        
  BEGIN        
  
   EXEC Proc_InsertCLM_ACTIVITY_RESERVE @CLAIM_ID ,@SYSTEM_ACTIVITY_ID,@COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,        
   @MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@SYSTEM_USER_ID,@LAST_UPDATED_DATETIME,    
   @VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,@CLOSE_RESERVE,@CRACCTS,@DRACCTS, @TRANSACTION_ID,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE        
  
  
   FETCH NEXT FROM CUR         
   INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,        
   @OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,        
   @RETENTION_LIMITS,@ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE        
  
  END        
  CLOSE CUR        
  DEALLOCATE CUR        
        
  --  EXEC Proc_UpdateActivityReserve @CLAIM_ID,@ACTIVITY_ID,@RESERVE_AMOUNT,@EXPENSES,@RECOVERY,@PAYMENT_AMOUNT,@RI_RESERVE,@MODIFIED_BY,@LAST_UPDATED_DATETIME         
  SELECT @RESERVE_AMOUNT = -SUM(ISNULL(OUTSTANDING,0))         
  FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK) WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID        
  
  EXEC Proc_UpdateActivityReserve @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@RESERVE_AMOUNT,NULL,NULL,0,0,NULL,@LAST_UPDATED_DATETIME        
  
  SELECT @CLAIM_NUMBER = CLAIM_NUMBER FROM CLM_CLAIM_INFO  WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID        
  SELECT TOP 1 @DIV_ID=DIV_ID,@DEPT_ID=DEPT_ID,@PC_ID=PC_ID FROM ACT_ACCOUNTS_POSTING_DETAILS WITH (NOLOCK)  WHERE SOURCE_ROW_ID=@CLAIM_ID AND SOURCE_NUM=@CLAIM_NUMBER        
  --  SELECT @DEBIT_ACCOUNT_ID=CRACCTS, @CREDIT_ACCOUNT_ID=DRACCTS FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID   
  --  EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@ACTIVITY_ID,@USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DEBIT_ACCOUNT_ID,@CREDIT_ACCOUNT_ID,        
 
 IF(@ACCOUNTING_SUPPRESSED = 0)
 BEGIN
  EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@SYSTEM_USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DRACCTS,@CRACCTS        
 END

  UPDATE CLM_ACTIVITY_RESERVE SET OUTSTANDING=0 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID        
 END        
    
           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                            
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM       
  
    
 --When the activity is being completed, check whether the activity is crossing the notification limit                    
 if(@PAYMENT_AMOUNT>@NOTIFY_LIMIT)                    
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
  )  */                    
  SET @NOTIFY_USER = 1                    
  SELECT @TEMP_ERROR_CODE = @@ERROR           
  IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM 
 end         
  
                       
 set @RETURN_VALUE = 1       
  
 PROBLEM:                                            
 IF (@TEMP_ERROR_CODE <> 0)                                       
 BEGIN                                                                                      
  --     ROLLBACK TRAN                                                                                        
  SET @RETURN_VALUE = -1             
  return @RETURN_VALUE                     
 END                                                                                    
 ELSE                                 
  RETURN @RETURN_VALUE                                         
end   

--go
--declare @NOTIFY int 
--exec Proc_CompleteClaimPayment 2989,2,null,null,@NOTIFY out,0
--rollback tran

GO

