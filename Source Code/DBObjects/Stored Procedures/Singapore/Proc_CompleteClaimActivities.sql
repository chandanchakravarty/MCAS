                  
 /*----------------------------------------------------------                                                          
Proc Name             : Dbo.Proc_CompleteClaimActivities                                                          
Created by            : Santosh Kumar Gautam                                                         
Date                  : 3 Dec 2010                                                         
Purpose               : To retrieve the reserve of claim coverages                                                       
Revison History       :                                                          
Used In               : To fill dropdown at risk information page.(CLAIM module)                                                          
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc Proc_CompleteClaimActivities                                                  
------   ------------       -------------------------*/                                                          
--                             
                              
--                           
 --Proc_CompleteClaimActivities 1224,1,11836,165,'N',-1,'2012-03-01',915,0,'','',3,0   
 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CompleteClaimActivities]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CompleteClaimActivities]
GO                      
CREATE PROCEDURE [dbo].[Proc_CompleteClaimActivities]                              
                               
 @CLAIM_ID            INT                      
,@ACTIVITY_ID         INT      
,@ACTIVITY_REASON     INT                
,@ACTION_ON_PAYMENT   INT  
,@IS_VOIDED_ACTIVITY  CHAR(1) ='N'   
,@VOID_ACTIVITY_ID    int =-1    
,@COMPETED_DATE       DATETIME=NULL  
,@COMPETED_BY         INT     =NULL  
,@TEXT_ID             INT     =NULL  
,@TEXT_DESCRIPTION    VARCHAR(2000)=NULL  
,@REASON_DESCRIPTION  VARCHAR(2000)=''  
,@LANG_ID     INT =1  
,@IS_ACC_COI_FLG   INT =0  
  
  
  
  
AS                              
BEGIN                  
   
 DECLARE @IS_LITIGATION_FILED    INT =0     
 DECLARE @CLAIM_STATUS           INT =0   
 DECLARE @LOB_ID                 INT =0      
 DECLARE @USER_ID               INT =0      
 DECLARE @CONTRACT_ID       INT =0     
 DECLARE @USER_LOGGED_RESERVE_LIMIT DECIMAL(18,2) =0     
 DECLARE @USER_LOGGED_PAYMENT_LIMIT DECIMAL(18,2) =0     
 DECLARE @USER_LOGGED_NOTIFY_LIMIT DECIMAL(18,2) =0     
 DECLARE @USER_PREV_PAID_AMOUNT     DECIMAL(18,2) =0   
 DECLARE @CUSTOMER_ID    INT =0   
 DECLARE @POLICY_ID     INT =0    
 DECLARE @POLICY_VERSION_ID         INT =0   
      
SET @USER_ID=@COMPETED_BY  
--================================================================================  
-- FETCH CLAIM RELATED FIELDS AND PREVIOUS PAID AMOUNT AND TOTAL RESERVE AMOUNT  
--================================================================================  
 SELECT @IS_LITIGATION_FILED   = LITIGATION_FILE ,  
        @CLAIM_STATUS     = CLAIM_STATUS_UNDER ,   
        @LOB_ID       = LOB_ID ,         
        @USER_PREV_PAID_AMOUNT = ISNULL(PAID_LOSS,0)+ISNULL(PAID_EXPENSE,0)  ,  
        @CUSTOMER_ID     = CUSTOMER_ID,  
        @POLICY_ID      = POLICY_ID,  
        @POLICY_VERSION_ID     = POLICY_VERSION_ID     
 FROM   CLM_CLAIM_INFO WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID    
  
  
  
  
IF @COMPETED_DATE IS NULL  
  SET @COMPETED_DATE =GETDATE()  
  
IF(@IS_ACC_COI_FLG=0)  
BEGIN  
 -- PAYEE OR RECOVERY INFORMATION IS NOT PROVIDED THEN NOT ALLOWED TO COMPLETED ACTIVITY  
 IF(@IS_VOIDED_ACTIVITY ='N' AND @ACTIVITY_REASON IN(11775,11776))  
  BEGIN  
       
    IF NOT EXISTS( SELECT CLAIM_ID FROM CLM_PAYEE WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID)      
     RETURN -9     
      
    
  END           
END        
 --PRINT @USER_LOGGED_RESERVE_LIMIT  
 -- PRINT @USER_LOGGED_NOTIFY_LIMIT
 --  PRINT @USER_LOGGED_PAYMENT_LIMIT
 
--================================================================================  
-- FETCH LOGGED USER AUTHORITY  
--================================================================================  
 SELECT  @USER_LOGGED_RESERVE_LIMIT= ISNULL(RESERVE_LIMIT,0),  
         @USER_LOGGED_NOTIFY_LIMIT=ISNULL(NOTIFY_AMOUNT,0)    ,  
         @USER_LOGGED_PAYMENT_LIMIT=ISNULL(PAYMENT_LIMIT,0)                  
 FROM CLM_ADJUSTER_AUTHORITY CAA WITH (NOLOCK)   
 JOIN CLM_AUTHORITY_LIMIT CAL  WITH (NOLOCK)  ON CAA.LIMIT_ID = CAL.LIMIT_ID                    
 LEFT JOIN CLM_ADJUSTER CA WITH (NOLOCK)  ON CA.ADJUSTER_ID = CAA.ADJUSTER_ID AND CA.IS_ACTIVE='Y'                                 
 LEFT OUTER JOIN MNT_USER_LIST MUL ON CA.USER_ID=MUL.USER_ID                                                                      
 WHERE MUL.USER_ID=@USER_ID AND CAA.LOB_ID=@LOB_ID  AND CAA.IS_ACTIVE='Y'   
  
       
          
 DECLARE @OUTSTANDING_TRAN  DECIMAL(18,2),            
   @OUTSTANDING  DECIMAL(18,2),            
   @RI_RESERVE_TRAN DECIMAL(18,2),            
   @RI_RESERVE  DECIMAL(18,2),            
   @CO_RESERVE_TRAN DECIMAL(18,2),            
   @CO_RESERVE  DECIMAL(18,2)  ,      
   @TOTAL_PAYMENT_AMOUNT DECIMAL(18,2)  ,      
         
   @PAYMENT_LOSS_AMOUNT  DECIMAL(18,2),      
   @TOTAL_PAYMENT_LOSS_AMOUNT  DECIMAL(18,2) ,      
   @PAYMENT_EXP_AMOUNT  DECIMAL(18,2),      
   @TOTAL_PAYMENT_EXP_AMOUNT  DECIMAL(18,2)   ,    
   @RECOVERY  DECIMAL(18,2),      
   @TOTAL_RECOVERY  DECIMAL(18,2) ,    
   @RI_NET_PAID_RESERVE  DECIMAL(18,2)   ,    
   @COI_NET_PAID_RESERVE  DECIMAL(18,2)         
        
        
 SELECT         
  @OUTSTANDING_TRAN=ISNULL(SUM(OUTSTANDING_TRAN ),0),            
  @OUTSTANDING=ISNULL(SUM(OUTSTANDING),0),            
  @RI_RESERVE_TRAN=ISNULL(SUM(RI_RESERVE_TRAN),0) ,            
  @RI_RESERVE=ISNULL(SUM(RI_RESERVE),0) ,            
  @CO_RESERVE_TRAN=ISNULL(SUM(CO_RESERVE_TRAN),0) ,            
  @CO_RESERVE=ISNULL(SUM(CO_RESERVE),0) ,             
  @RECOVERY=ISNULL(SUM(RECOVERY_AMOUNT),0) ,    
  @TOTAL_RECOVERY=ISNULL(SUM(TOTAL_RECOVERY_AMOUNT),0) ,    
  @TOTAL_PAYMENT_AMOUNT=ISNULL(SUM(PAYMENT_AMOUNT),0) ,   
  @PAYMENT_LOSS_AMOUNT=ISNULL(SUM( CASE WHEN CLM_PRODUCT_COVERAGES.IS_RISK_COVERAGE='Y' THEN PAYMENT_AMOUNT ELSE 0 END),0),            
  @TOTAL_PAYMENT_LOSS_AMOUNT=ISNULL(SUM( CASE WHEN CLM_PRODUCT_COVERAGES.IS_RISK_COVERAGE='Y' THEN TOTAL_PAYMENT_AMOUNT ELSE 0 END),0),                
  @PAYMENT_EXP_AMOUNT=ISNULL(SUM( CASE WHEN CLM_PRODUCT_COVERAGES.IS_RISK_COVERAGE='N' THEN PAYMENT_AMOUNT ELSE 0 END),0),            
  @TOTAL_PAYMENT_EXP_AMOUNT=ISNULL(SUM( CASE WHEN CLM_PRODUCT_COVERAGES.IS_RISK_COVERAGE='N' THEN TOTAL_PAYMENT_AMOUNT ELSE 0 END),0)      
 FROM CLM_ACTIVITY_RESERVE  WITH(NOLOCK)    INNER JOIN      
     CLM_PRODUCT_COVERAGES WITH (NOLOCK) ON CLM_PRODUCT_COVERAGES.CLAIM_COV_ID= CLM_ACTIVITY_RESERVE.COVERAGE_ID       
     AND CLM_ACTIVITY_RESERVE.CLAIM_ID=CLM_PRODUCT_COVERAGES.CLAIM_ID        
 WHERE (CLM_ACTIVITY_RESERVE.CLAIM_ID=@CLAIM_ID AND CLM_ACTIVITY_RESERVE.ACTIVITY_ID=@ACTIVITY_ID)            
       
  
--PRINT @OUTSTANDING
--PRINT @TOTAL_PAYMENT_AMOUNT
--================================================================================  
-- FOR PARTIAL/FULL PAYMENT ACTIVITY   
-- VALIDATE AUTHORITY  
--================================================================================     
PRINT @ACTION_ON_PAYMENT   
IF(@ACTION_ON_PAYMENT IN (180,181) )  
BEGIN  
   
 --- GET TOTAL PAYMENT AMOUNT INCLUDING THIS ACTIVITY  
  SET @USER_PREV_PAID_AMOUNT=@USER_PREV_PAID_AMOUNT+@TOTAL_PAYMENT_AMOUNT  
    
  -- IF TOTAL PAYMENT AMOUNT IS EXCEEDED FROM NOTIFY AMOUNT THEN MAKE AN DIARY ENTRY  
  IF(@USER_PREV_PAID_AMOUNT>@USER_LOGGED_NOTIFY_LIMIT)  
    BEGIN  
      
        DECLARE @FOLLOWUPDATE DATETIME  
        DECLARE @SUBJECTLINE  NVARCHAR(256)='Notification limit for Activity exceeded'     
        DECLARE @NOTE    NVARCHAR(256)='Notification limit for the Activity has been exceeded'  
        SET @FOLLOWUPDATE=DATEADD(DAY,7,@COMPETED_DATE)  
          
        IF @LANG_ID=2  
          BEGIN  
           SET @SUBJECTLINE='Notificação limite para a atividade ultrapassado'  
           SET @NOTE='Notificação limite para a atividade tenha sido excedido'  
          END  
                   
  EXEC Proc_InsertDiary     
   @RECBYSYSTEM       = NULL,  
   @RECDATE           = @COMPETED_DATE ,       
   @FOLLOWUPDATE      = @FOLLOWUPDATE,        
   @LISTTYPEID        = 34,--Adjuster Limit Notification    
   @POLICYBROKERID    = NULL,       
   @SUBJECTLINE       = @SUBJECTLINE,       
   @LISTOPEN   ='Y',      
   @PRIORITY   ='M',        
   @TOUSERID          = @USER_ID,        
   @FROMUSERID        = @USER_ID,       
   @NOTE    = @NOTE,   
   @CLAIMID   = @CLAIM_ID,    
   @CUSTOMER_ID       = @CUSTOMER_ID,       
   @POLICY_ID         = @POLICY_ID,        
   @POLICY_VERSION_ID = @POLICY_VERSION_ID,      
   @MODULE_ID      = 5, -- CLAIM MODULE    
   @SYSTEMFOLLOWUPID  = NULL,     
   @STARTTIME         = NULL,        
   @ENDTIME           = NULL,  
   @PROPOSALVERSION   = NULL,        
   @QUOTEID   = NULL,     
   @CLAIMMOVEMENTID   = NULL,        
   @TOENTITYID        = NULL,        
   @FROMENTITYID  = NULL,        
   @LISTID            = NULL,    
   @APP_ID            = NULL,        
         @APP_VERSION_ID    = NULL,    
         @RULES_VERIFIED    = NULL,    
   @PROCESS_ROW_ID    = NULL    
    END  
      

  IF(@USER_PREV_PAID_AMOUNT < @USER_LOGGED_PAYMENT_LIMIT AND @IS_ACC_COI_FLG=0)  
     RETURN -10    
  
END  
  
--================================================================================  
-- FOR CREATE RESERVE/CHANGE RESERVE/RE-OPEN RESERVE  
-- VALIDATE AUTHORITY  
--================================================================================   

IF(@ACTION_ON_PAYMENT IN (165,166,168)AND @IS_ACC_COI_FLG=0)  
BEGIN  
  PRINT @OUTSTANDING
  PRINT @USER_LOGGED_RESERVE_LIMIT
 --- GET TOTAL RESERVE AMOUNT INCLUDING THIS ACTIVITY   
  IF(@OUTSTANDING < @USER_LOGGED_RESERVE_LIMIT)  
     BEGIN  
          
        -- IF IT IS FIRST ACTIVITY(RESERVE CREATTION ACTIVITY) THEN DELETE IT BECAUSE   
        -- ACTIVITY WILL CREATE AGAIN   
        IF(@ACTION_ON_PAYMENT =165)  
           EXEC [Proc_DeleteClaimActivity] @CLAIM_ID,@ACTIVITY_ID  
          
       RETURN -11    
       
     END  
  
END  
       
   
   -- CLAIM HAS LITIGATION_FILE IS YES THEN IS_LEGAL MUST BE YES OTHERWISE NO    
       
   SELECT @COI_NET_PAID_RESERVE=SUM(CASE WHEN COMP_TYPE='CO' THEN PAYMENT_AMT+RECOVERY_AMT ELSE 0 END) ,    
          @RI_NET_PAID_RESERVE=SUM(CASE WHEN COMP_TYPE='RI' THEN PAYMENT_AMT+RECOVERY_AMT ELSE 0 END)     
   FROM   CLM_ACTIVITY_CO_RI_BREAKDOWN   WITH (NOLOCK)  
   WHERE  CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID<=@ACTIVITY_ID AND IS_ACTIVE='Y'    
             
   UPDATE CLM_ACTIVITY               
   SET  ACTIVITY_STATUS       = 11801 --COMPLETED              
      , RESERVE_AMOUNT        = @OUTSTANDING_TRAN            
      , CLAIM_RESERVE_AMOUNT  = @OUTSTANDING            
      , RI_RESERVE            = @RI_RESERVE_TRAN                  
      , CLAIM_RI_RESERVE      = @RI_RESERVE            
      , CO_TRAN_RESERVE_AMT   = @CO_RESERVE_TRAN            
      , CO_TOTAL_RESERVE_AMT  = @CO_RESERVE       
      , PAYMENT_AMOUNT    = @PAYMENT_LOSS_AMOUNT       
      , EXPENSES              = @PAYMENT_EXP_AMOUNT      
      , CLAIM_PAYMENT_AMOUNT  = @TOTAL_PAYMENT_LOSS_AMOUNT        
      , TOTAL_EXPENSE         = @TOTAL_PAYMENT_EXP_AMOUNT      
      , [RECOVERY]     = @RECOVERY    
      , [TOTAL_RECOVERY]      = @TOTAL_RECOVERY    
      , RI_NET_PAID_RESERVE   = ISNULL(@RI_NET_PAID_RESERVE,0)    
      , COI_NET_PAID_RESERVE  = ISNULL(@COI_NET_PAID_RESERVE,0)    
      , IS_LEGAL              = CASE WHEN @IS_LITIGATION_FILED=10963 THEN 'Y' ELSE 'N' END    
      , LAST_UPDATED_DATETIME = @COMPETED_DATE  
      , MODIFIED_BY           = @COMPETED_BY   
      , CLAIM_STATUS    = @CLAIM_STATUS  
      , TEXT_ID               = @TEXT_ID   
      , TEXT_DESCRIPTION      = @TEXT_DESCRIPTION   
      , REASON_DESCRIPTION     = @REASON_DESCRIPTION  
      , COMPLETED_DATE   = @COMPETED_DATE  
   WHERE (CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND IS_ACTIVE='Y')        
               
      
 UPDATE CLM_CLAIM_INFO             
 SET    OUTSTANDING_RESERVE  = @OUTSTANDING, --OUTSTANDING’             
        RESINSURANCE_RESERVE = @RI_RESERVE,  --RI_Reserve’             
        CO_TOTAL_RESERVE_AMT = @CO_RESERVE, --CO_Reserve’             
        PAID_LOSS    = @TOTAL_PAYMENT_LOSS_AMOUNT,       
        PAID_EXPENSE   = @TOTAL_PAYMENT_EXP_AMOUNT  ,    
        RECOVERY_OUTSTANDING = @TOTAL_RECOVERY    
 WHERE (CLAIM_ID=@CLAIM_ID)            
     
  EXEC Proc_ClaimPosting   
  @CLAIM_ID      =@CLAIM_ID,  
  @ACTIVITY_ID   =@ACTIVITY_ID,  
  @DATE_COMMITED =@COMPETED_DATE,  
  @COMMITTED_BY  =@COMPETED_BY  
    
  ------------------------------------------------------------------------------  
  -- ADDED BY SANTOSH KUMAR GAUTAM ON 22 MARCH 2011  
  -- TO REDUCE THE AGGREGATE LIMIT OF XOL  
  -- AGGREGATE LIMIT DECREASED BY TOTAL PAYMENT OF CURRENT ACTIVITY  
  ------------------------------------------------------------------------------  
    
  -- FOR PARTIAL/FULL PAYMENT ACTIVITY OR WHEN PAYMENT AMOUNT IS NEGATE( WHEN  @VOID_ACTIVITY_ID>0)  
 IF(@ACTION_ON_PAYMENT IN (180,181) OR @VOID_ACTIVITY_ID>0)  
 BEGIN   
        
     EXEC Proc_CalculateClaimXOLBreakdown   
   @CLAIM_ID       =  @CLAIM_ID  
  ,@ACTIVITY_ID    =  @ACTIVITY_ID     
  ,@CREATED_DATE   =  @COMPETED_DATE    
  ,@CREATED_BY     =  @COMPETED_BY     
  ,@VOID_ACTIVITY_ID  =  @VOID_ACTIVITY_ID   
    
  END  
    
  ------------------------------------------------------------------------------  
  -- FOR PRODUCT FACULTATIVE LIALIBLITY AND CIVIL LIALIBITY TRANSPORTATION  
  -- WHEN A FULL PAYMENT IS DONE AGAINST A VICTIM, THE VICTIM STATUS UNDER VICTIM TAB SHOULD BE AUTOMATICALLY UPDATED TO CLOSED.  
  ------------------------------------------------------------------------------  
 IF(@ACTION_ON_PAYMENT =181 AND @LOB_ID IN(17,18,22) )   
 BEGIN   
      
     UPDATE CLM_VICTIM_INFO SET [STATUS]=90 -- CLOSED   
     WHERE CLAIM_ID=@CLAIM_ID AND VICTIM_ID IN (  
       SELECT VICTIM_ID FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK)  
       WHERE CLAIM_ID =@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND PAYMENT_AMOUNT <>0  
      )  
    
  END  
    
  RETURN 1              
                      
                      
                    
END 