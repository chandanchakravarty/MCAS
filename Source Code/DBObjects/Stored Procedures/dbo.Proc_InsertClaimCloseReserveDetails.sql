IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertClaimCloseReserveDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertClaimCloseReserveDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP PROC dbo.Proc_InsertClaimCloseReserveDetails
--go
/*----------------------------------------------------------                                                                                        
Proc Name       : dbo.Proc_InsertClaimCloseReserveDetails                                                                            
Created by      : Asfa Praveen                                                                                      
Date            : 05/Dec/2007    
Purpose         : Insert data in CLM_ACTIVITY_RESERVE table for Close Reserve Activity                                                                   
Revison History :                                                                                        
Used In         : Wolverine                                                                                        
----------------------------------------------------------------------            
Date     Review By          Comments                                                                                        
------   ------------       -------------------------*/                                                                                        
--DROP PROC dbo.Proc_InsertClaimCloseReserveDetails                
CREATE PROC [dbo].[Proc_InsertClaimCloseReserveDetails]                                                                              
(    
 @CLAIM_ID Int ,    
 @USER_ID Int = NULL     
)    
AS                                                                                        
BEGIN    
    
    
BEGIN TRAN                                         
DECLARE @TEMP_ERROR_CODE INT         
    
    
    
    
  DECLARE @SYSTEM_USER_ID INT    
  DECLARE @LAST_UPDATED_DATETIME DATETIME    
  DECLARE @SYSTEM_ACTIVITY_ID INT     
  DECLARE @CLOSE_RESERVE INT    
  DECLARE @TRANSACTION_ID INT    
  DECLARE @CRACCTS INT    
  DECLARE @DRACCTS INT    
  DECLARE @COVERAGE_ID INT    
  DECLARE @PRIMARY_EXCESS VARCHAR    
  DECLARE @MCCA_ATTACHMENT_POINT DECIMAL    
  DECLARE @MCCA_APPLIES DECIMAL      
  DECLARE @ATTACHMENT_POINT DECIMAL    
  DECLARE @OUTSTANDING DECIMAL(18,2)    
  DECLARE @REINSURANCE_CARRIER INT    
  DECLARE @RI_RESERVE DECIMAL    
  DECLARE @VEHICLE_ID INT    
  DECLARE @POLICY_LIMITS DECIMAL    
  DECLARE @RETENTION_LIMITS DECIMAL    
  DECLARE @RESERVE_AMOUNT DECIMAL(18,2)    
  DECLARE @CLAIM_NUMBER VARCHAR(10)    
  DECLARE @DIV_ID INT    
  DECLARE @DEPT_ID INT      
  DECLARE @PC_ID INT    
  
	DECLARE @ACTUAL_RISK_ID INT
	DECLARE @ACTUAL_RISK_TYPE VARCHAR(15)
    
    
    
    
  SET @CLOSE_RESERVE = 167    
  SET @OUTSTANDING = 0     
      
 SELECT @RESERVE_AMOUNT = -SUM(ISNULL(OUTSTANDING,0))     
    FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK) WHERE CLAIM_ID= @CLAIM_ID AND ACTIVITY_ID=(SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK) WHERE CLAIM_ID = @CLAIM_ID AND IS_ACTIVE = 'Y' ORDER BY ACTIVITY_ID DESC)    
        
    SELECT @SYSTEM_USER_ID = USER_ID FROM MNT_USER_LIST  WITH (NOLOCK) WHERE USER_LOGIN_ID = 'SYSTEM'    
    SELECT @LAST_UPDATED_DATETIME = GETDATE()    
        
 --Ravindra if system generated close actibity use SYSTEM user else user who is closing the claim     
 IF(@USER_ID IS NULL )     
  SET @USER_ID = @SYSTEM_USER_ID    
    
    EXEC PROC_INSERTCLM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID OUTPUT,11773,'CLOSE RESERVE',@USER_ID,11801,0,
@CLOSE_RESERVE    


    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM         
    
 -- Added by Asfa (29-July-2008) - As per mail sent by Rajan sir dated 12-july-2008    
 UPDATE CLM_ACTIVITY SET COMPLETED_DATE=GETDATE()     
 WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@SYSTEM_ACTIVITY_ID     
    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM     
      
    SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0)+1 FROM CLM_ACTIVITY_RESERVE  WITH (NOLOCK)           
    WHERE CLAIM_ID=@CLAIM_ID      

    ----ACC_NUMBER Replace By ACC_DISP_NUMBER For Itrack Issue #6372/6274.  
    SELECT @DRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS  WITH (NOLOCK) WHERE ACC_DISP_NUMBER=(SELECT SELECTEDDEBITLEDGERS FROM CLM_TYPE_DETAIL  WITH (NOLOCK) WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE)    
    SELECT @CRACCTS=ACCOUNT_ID FROM ACT_GL_ACCOUNTS  WITH (NOLOCK) WHERE ACC_DISP_NUMBER=(SELECT SELECTEDCREDITLEDGERS FROM CLM_TYPE_DETAIL  WITH (NOLOCK) WHERE DETAIL_TYPE_ID = @CLOSE_RESERVE)    


    
 DECLARE CUR CURSOR    
    FOR SELECT COVERAGE_ID,PRIMARY_EXCESS,MCCA_ATTACHMENT_POINT,MCCA_APPLIES,ATTACHMENT_POINT,    
      REINSURANCE_CARRIER,RI_RESERVE,VEHICLE_ID,POLICY_LIMITS,RETENTION_LIMITS,
      ACTUAL_RISK_ID,ACTUAL_RISK_TYPE   
      FROM CLM_ACTIVITY_RESERVE     
      WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=(SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK) WHERE CLAIM_ID = @CLAIM_ID AND IS_ACTIVE = 'Y' ORDER BY ACTIVITY_ID DESC)   
	  ORDER BY RESERVE_ID 
 OPEN CUR    
 FETCH NEXT FROM CUR     
 INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,    
      @REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,
      @ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE 
     
     
 WHILE @@FETCH_STATUS = 0    
 BEGIN    
     
 EXEC PROC_INSERTCLM_ACTIVITY_RESERVE @CLAIM_ID ,@SYSTEM_ACTIVITY_ID,@COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,    
          @MCCA_APPLIES,@ATTACHMENT_POINT,@OUTSTANDING,@REINSURANCE_CARRIER,@RI_RESERVE,@USER_ID,@LAST_UPDATED_DATETIME,    
		  @VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,@CLOSE_RESERVE,@CRACCTS,@DRACCTS, @TRANSACTION_ID,
		  @ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE
    
     
     
     
 FETCH NEXT FROM CUR     
 INTO @COVERAGE_ID,@PRIMARY_EXCESS,@MCCA_ATTACHMENT_POINT,@MCCA_APPLIES,@ATTACHMENT_POINT,    
      @REINSURANCE_CARRIER,@RI_RESERVE,@VEHICLE_ID,@POLICY_LIMITS,@RETENTION_LIMITS,
      @ACTUAL_RISK_ID,@ACTUAL_RISK_TYPE    
     
 END    
 CLOSE CUR    
 DEALLOCATE CUR    
    
 EXEC PROC_UPDATEACTIVITYRESERVE @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@RESERVE_AMOUNT,NULL,NULL,0,0,NULL,@LAST_UPDATED_DATETIME    
    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
    
      
     
 SELECT @CLAIM_NUMBER = CLAIM_NUMBER FROM CLM_CLAIM_INFO  WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID    
        
 SELECT TOP 1 @DIV_ID=DIV_ID,@DEPT_ID=DEPT_ID,@PC_ID=PC_ID FROM ACT_ACCOUNTS_POSTING_DETAILS WITH (NOLOCK)  WHERE SOURCE_ROW_ID=@CLAIM_ID AND SOURCE_NUM=@CLAIM_NUMBER    
    EXEC PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY @CLAIM_ID,@SYSTEM_ACTIVITY_ID,@USER_ID,@DIV_ID,@DEPT_ID,@PC_ID,@DRACCTS,@CRACCTS    
    
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                        
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      
    
    
SELECT @SYSTEM_ACTIVITY_ID AS ACTIVITY_ID                    
  
  
   -- Added By Praveen    
  COMMIT TRAN                        
   RETURN 1                           
                                            
  PROBLEM:                                                
   ROLLBACK TRAN  
   RETURN 0    
  
END                    

--go
--exec Proc_InsertClaimCloseReserveDetails 2184,334
--rollback tran

GO

