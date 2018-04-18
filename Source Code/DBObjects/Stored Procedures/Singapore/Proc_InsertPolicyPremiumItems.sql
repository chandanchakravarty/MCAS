IF  EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[Proc_InsertPolicyPremiumItems]') AND TYPE IN (N'P', N'PC'))
DROP PROC Proc_InsertPolicyPremiumItems   
GO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO 
    
     
/*                         
----------------------------------------------------------                                              
Proc Name       : dbo.Proc_InsertPolicyPremiumItems                                    
Created by      : LALIT CHAUHAN                                  
Date            : 05/20/2010                                              
Purpose         : INSERT FIRST TIME PREMIUM INSTALLMENT DETALS.                                              
Revison History :                                              
Used In        : Ebix Advantage                                              
------------------------------------------------------------                                              
Date     Review By          Comments                                              
------   ------------       -------------------------                              
DROP proc Proc_InsertPolicyPremiumItems        
exec Proc_InsertPolicyPremiumItems 2156,473,2,300,0,0,0,300,78,0,398,'01/01/2010',14855,150,0,0,0,150,150,300,0,0              
*/                            
                                        
Create PROC [dbo].Proc_InsertPolicyPremiumItems                            
 (                              
 @CUSTOMER_ID   INT,                     -- ID OF CUSTOMER WHOSE  POLICY PREMIUM BILL INSTALLMENT WILL BE POSTED                               
 @POLICY_ID  INT,                        -- POLICY IDENTIFICATION NUMBER                              
 @POLICY_VERSION_ID SMALLINT,            -- POLICY VERSION IDENTIFICATION NUMBER                              
 @TOTAL_PREMIUM DECIMAL(25,2),           -- POLICY PREMIUM AMOUNT                          
 @TOTAL_INTEREST_AMOUNT DECIMAL(25,2),   -- POLICY INTEREST AMOUNT                          
 @TOTAL_FEES DECIMAL(25,2),              -- POLICY FEES                          
 @TOTAL_TAXES DECIMAL(25,2),             -- POLICY TAXES                            
 @TOTAL_AMOUNT DECIMAL(25,2),            -- POLIY TOTAL AMOUNT SUM OF ALL AMOUNT(PREMIUM AMOUNT + INTEREST AMOUNT + FEES + TAXES)                          
 @PLAN_ID INT,                           -- PLAN ID              
 @RETVAL  INT OUT,       -- PROC RETURN VALUE                         
 @CREATED_BY INT,                        -- CRETEDBY ID                    
 @CREATED_DATETIME DATETIME,             -- CREATED DATE TIME                   
 @PRM_DIST_TYPE INT = null ,                 
 @TOTAL_TRAN_PREMIUM DECIMAL(25,2) = null,              
 @TOTAL_TRAN_INTEREST_AMOUNT DECIMAL(25,2) = null,              
 @TOTAL_TRAN_FEES DECIMAL(25,2)= null,              
 @TOTAL_TRAN_TAXES decimal (25,2) = null,              
 @TOTAL_TRAN_AMOUNT decimal (25,2) = null,              
 @TOTAL_CHANGE_INFORCE_PRM DECIMAL(25,2) = null,               
 @TOTAL_INFO_PRM  DECIMAL(25,2) = null,              
 @TOTAL_STATE_FEES DECIMAL(25,2)=null,              
 @TOTAL_TRAN_STATE_FEES DECIMAL(25,2)=null ,  
 --Added By Kuldeep For TFS3408(New added fields in POL_BILLING_DETAILS)  
 @TOTAL_AFTER_GST DECIMAL(25,2)=NULL,              
 @TOTAL_BEFORE_GST DECIMAL(25,2)=NULL,  
 @GROSS_COMMISSION DECIMAL(25,2)=NULL,  
 @GST_ON_COMMISSION DECIMAL(25,2)=NULL,  
 @TOTAL_COMM_AFTER_GST DECIMAL(25,2)=NULL  
 )                              
AS                              
BEGIN                   
 set   @TOTAL_TRAN_PREMIUM=@TOTAL_PREMIUM             
 set @TOTAL_TRAN_INTEREST_AMOUNT=@TOTAL_INTEREST_AMOUNT            
 set @TOTAL_TRAN_FEES = @TOTAL_FEES              
 set @TOTAL_TRAN_TAXES =@TOTAL_TAXES             
 set @TOTAL_TRAN_AMOUNT =@TOTAL_AMOUNT              
 
  
   DECLARE                             
    @PLAN_DESCRIPTION NVARCHAR(150),                            
    @POLICY_EFFECTIVE_DATE DATETIME,                            
    @PLAN_TYPE  NCHAR(50),                            
    @NO_OF_PAYMENTS  SMALLINT,                            
    @MONTHS_BETWEEN  SMALLINT,                            
    @PERCENT_BREAKDOWN1  DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN2  DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN3  DECIMAL(7,4),                             
    @PERCENT_BREAKDOWN4  DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN5  DECIMAL(7,4),                             
    @PERCENT_BREAKDOWN6  DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN7  DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN8  DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN9  DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN10 DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN11 DECIMAL(7,4),                            
    @PERCENT_BREAKDOWN12 DECIMAL(7,4),                            
    @MODE_OF_PAYMENT INT ,                            
    @MODE_OF_DOWN_PAYMENT INT,                            
    @CURRENT_TERM smallint,                            
    @PROCESS_ID INT=0,                          
    @TRAN_TYPE NVARCHAR(50)='',                            
    @IS_ACTIVE_PLAN NCHAR(2),                            
    @INSTALLMENTS_IN_DOWN_PAYMENT INT ,                    
                              
    ----ACT_POLICY_INSTALLMENT_DETAILS TABLE VARIABLE----------                            
    @LOB_ID INT ,                      
    @INSTALLMENT_AMOUNT DECIMAL(25,2),                            
    @INSTALLMENT_EFFECTIVE_DATE DATETIME,                            
    @RELEASED_STATUS NCHAR(2) ='Y',                            
    @ROW_ID INT,                            
    @INSTALLMENT_NO INT,                            
    @RISK_ID INT,                        
    @RISK_TYPE NVARCHAR(15),                    
                           
    @INSTALLMENT_INTEREST_AMOUNT DECIMAL(25,2)=0,                            
    @INSTALLMENT_FEE DECIMAL(25,2)=0,                            
    @INSTALLMENT_TAXES DECIMAL(25,2)=0,                            
    @INSTALLMENT_TOTAL DECIMAL(25,2)=0,                            
    @PERCENTAG_OF_PREMIUM DECIMAL(9,4)=0,                            
    @CALCULATED_INS_EFF_DATE DATETIME  ,                          
    @INSTALLMENT_PLAN INT,                        
    @SUM_INSTALLMENT_AMOUNT DECIMAL(25,2)=0,                        
    @SUM_INSTALLMENT_INTEREST_AMOUNT DECIMAL(25,2)=0,                        
    @SUM_INSTALLMENT_FEE DECIMAL(25,2)=0,                        
    @SUM_INSTALLMENT_TAXES DECIMAL(25,2)=0,                  
    --Transaction Field                  
    @DATA_TOTAL_TRAN_PREMIUM DECIMAL(25,2) = 0,                  
    @DATA_TOTAL_TRAN_INTEREST_AMOUNT DECIMAL(25,2) = 0,                  
    @DATA_TOTAL_TRAN_FEES  DECIMAL(25,2) = 0,                  
    @DATA_TOTAL_TRAN_TAXES  DECIMAL(25,2) = 0,                  
    @DATA_TOTAL_TRAN_AMOUNT  DECIMAL(25,2) = 0,                  
                      
    @DETAILS_TRAN_FEES DECIMAL(25,2)=0,                  
    @DETAILS_TRAN_INTEREST_AMOUNT DECIMAL(25,2)=0,                  
    @DETAILS_TRAN_PREMIUM  DECIMAL(25,2)=0,                  
    @DETAILS_TRAN_TAXES  DECIMAL(25,2)=0,                  
    @DETAILS_TRAN_TOTAL DECIMAL(25,2)=0,                  
    @PREV_VERSION SMALLINT ,                  
    @PREV_TOTAL_PREMIUM DECIMAL(25,2)=0,                  
    @DETAILS_TRAN_PREMIUM_SUM DECIMAL(25,2)=0,                
    @DETAILS_TRAN_FEES_SUM DECIMAL(25,2)=0,                
    @DETAILS_TRAN_INTEREST_AMOUNT_SUM DECIMAL(25,2)=0,                
    @DETAILS_TRAN_TAXES_SUM DECIMAL(25,2)=0,                
                      
    @REMAINING_PART DECIMAL(24,2),                      
    @TRAN_TYPE_NBS_INPREOGRESS INT = 24,         ---- DECLARE CONSTANT PROCESS ID FOR PROCESS AS NEW BUSINESS IN PROGRESS AS TABLE POL_PROCESS_MASTER                  
    @TRAN_TYPE_NBS_COMMIT INT = 25,                                       ---- PRODESS ID FOR NBS COMMIT                  
    @TRAN_TYPE_REN_INPREOGRESS INT = 5,         ---- FOR RENEWAL IN PROGRESS                  
    @TRAN_TYPE_REN_COMMIT INT = 18,                                       ---- FOR RENEWAL COMMIT                  
    @TRAN_TYPE_END_INPREOGRESS INT = 3,                                   ---- FOR ENDORSMENT                   
    @TRAN_TYPE_END_COMMIT INT = 14,                      ---- FOR ENDORSMENT COMMIT                   
    @TRAN_TYPE_CAN_INPROGRESS INT = 2,             
    @TRAN_TYPE_CAN_COMMIT INT = 12,          
    @TRAN_TYPE_REWRIT_INPROGRESS INT = 31,          
             
    @END_TOTAL_PREMIUM DECIMAL(25,2)=0,                
    @PREV_FEES DECIMAL(25,2)=0,                
    @PREV_TAXES DECIMAL(25,2)=0,                
    @PREV_INTEREST DECIMAL(25,2)=0,                
    @END_FEES DECIMAL(25,2)=0,                
    @END_TAXES DECIMAL(25,2)=0,                
    @END_INTEREST DECIMAL(25,2)=0,                
    @RELEASED_INSTALL_NO INT=0,                
    @UPDATE_INSTALL_DATA_FLAG INT=0,              
    @REMAIN_INSTALL INT,              
    @BASE_DATE_DOWN_PAYMENT INT,              
    @DOWN_PAYMENT_PLAN   SMALLINT,              
    @BDATE_INSTALL_NXT_DOWNPYMT INT,              
    @DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT SMALLINT,              
    @DUE_DAYS_DOWNPYMT SMALLINT,              
    @DAYS_SUBSEQUENT_INSTALLMENTS SMALLINT,              
    @APP_SUBMISSION_DATE DATETIME,              
    @POLICY_ISSUANCE_DATE  DATETIME,@SELECTED_PLAN_ID INT,              
    @POLICY_STATUS NVARCHAR(8),@SUBSEQUENT_INSTALLMENTS_OPTION NVARCHAR(50),              
    @END_EFFECTIVE_DATE DATETIME,              
    @END_EXPI_DATE DATETIME,@MODE_OF_DOWNPAY  INT,@PAYMENT_MODE INT,              
    @END_EFFEVTIVE_MONTHS INT,@END_EFFEVTIVE_DAYS FLOAT,              
    @FEES_DIST_TYPE INT = 1,              
    @PRIMARY_APPLICANT_ID INT,@POLICY_EXPIRY_DATE DATETIME,        
   
 --Added By Kuldeep                 
 @INSTALLMENT_COMM decimal(18,2)=0  
    /* Endorsment premium distribution options              
  --14673 Adjust in First Installment              
  --14674 Adjust in Last Installment              
  --14672 Equally Distribution in Installments               
  --14675 Separate Installment              
   */                
                
    SELECT @PRIMARY_APPLICANT_ID = APPLICANT_ID FROM POL_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND IS_PRIMARY_APPLICANT = 1              
                  
     SELECT                
     @SELECTED_PLAN_ID = POL.INSTALL_PLAN_ID,              
     @CURRENT_TERM = POL.CURRENT_TERM,                             --select CURRENT TERM                          
     @MODE_OF_DOWN_PAYMENT = POL.DOWN_PAY_MODE ,   --select DOWN PAYMENT MODE                           
     @LOB_ID = POL.POLICY_LOB,                                     --select POLICY LOB                           
     @RISK_TYPE = LOBMASTER.LOB_CODE,                              --select RISK TYPE FROM LOB MASTER                          
     @APP_SUBMISSION_DATE = CONVERT(DATETIME,CONVERT(VARCHAR(10),POL.CREATED_DATETIME,111)),-- datepart(date,POL.CREATED_DATETIME,              
     @POLICY_EFFECTIVE_DATE = CONVERT(DATETIME,CONVERT(VARCHAR(10),POL.POLICY_EFFECTIVE_DATE,111)),--POL.POLICY_EFFECTIVE_DATE ,          --select POLICY EFFECTIVE DATE              
     @POLICY_STATUS =               
   CASE               
    WHEN POL.APP_STATUS='COMPLETE'               
     THEN POL.POLICY_STATUS               
    ELSE POL.APP_STATUS              
   END      ,        
     @POLICY_EXPIRY_DATE = ISNULL(POL.POLICY_EXPIRATION_DATE,POL.APP_EXPIRATION_DATE)        
                  
     FROM POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)                      
    LEFT OUTER JOIN                            
     MNT_LOB_MASTER LOBMASTER WITH(NOLOCK) ON                            
     LOBMASTER.LOB_ID=POL.POLICY_LOB                             
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                          
   --END                        
                     
                              
      IF @@ERROR <> 0                                  ----IF ANY ERROR ACCORD DURING THIS PROCESS                  
  GOTO ERRHANDLER                
                      
              
                  
    SELECT @PROCESS_ID = PROCESS_ID FROM POL_POLICY_PROCESS  WITH(NOLOCK)              --Slect policy Process Id from POL_POLICY_PROCESS                  
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND  NEW_POLICY_ID = @POLICY_ID AND  NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID                              
                 AND UPPER(PROCESS_STATUS) <> 'ROLLBACK'          
           IF @PLAN_ID = 0                                 ---IF SELECTED PLAN ID IS 0 THEN IT SHOWS MESSAGE POLICY IS AGENCY BILL                  
    BEGIN                          
     GOTO PLANIDMESSAGE                       
    END                   
                 
     SELECT                             
     @PLAN_DESCRIPTION=PLAND.PLAN_DESCRIPTION,                                      -- select selected PLAN description from ACT_INSTALL_PLAN_DETAIL                           
     @PLAN_TYPE=PLAND.PLAN_TYPE,             -- select PLAN Type                          
     @NO_OF_PAYMENTS=PLAND.NO_OF_PAYMENTS,                                          -- select No of Payment                           
     @MONTHS_BETWEEN =PLAND.MONTHS_BETWEEN,       
     @PERCENT_BREAKDOWN1 =PLAND.PERCENT_BREAKDOWN1,                            
     @PERCENT_BREAKDOWN2=PLAND.PERCENT_BREAKDOWN2,                            
     @PERCENT_BREAKDOWN3=PLAND.PERCENT_BREAKDOWN3,                            
     @PERCENT_BREAKDOWN4=PLAND.PERCENT_BREAKDOWN4,                            
     @PERCENT_BREAKDOWN5=PLAND.PERCENT_BREAKDOWN5,                            
     @PERCENT_BREAKDOWN6=PLAND.PERCENT_BREAKDOWN6,                            
     @PERCENT_BREAKDOWN7=PLAND.PERCENT_BREAKDOWN7,         
     @PERCENT_BREAKDOWN8=PLAND.PERCENT_BREAKDOWN8,                            
     @PERCENT_BREAKDOWN9=PLAND.PERCENT_BREAKDOWN9,                            
     @PERCENT_BREAKDOWN10=PLAND.PERCENT_BREAKDOWN10,                            
     @PERCENT_BREAKDOWN11=PLAND.PERCENT_BREAKDOWN11,                            
     @PERCENT_BREAKDOWN12=PLAND.PERCENT_BREAKDOWN12,                             
     @MODE_OF_PAYMENT=ISNULL(PLAND.PLAN_PAYMENT_MODE,0),                            
     @IS_ACTIVE_PLAN=PLAND.IS_ACTIVE,                            
     @INSTALLMENTS_IN_DOWN_PAYMENT=ISNULL(PLAND.NO_INS_DOWNPAY,0),              
     @BASE_DATE_DOWN_PAYMENT=ISNULL(BASE_DATE_DOWNPAYMENT,0),         ---select base down payment date              
     @DOWN_PAYMENT_PLAN=ISNULL(DOWN_PAYMENT_PLAN,0) ,                                          --select if downpayment installment,(0+N or 1+N)  o for no downpaymebt and 1 for 1 installment in downpayment              
     @BDATE_INSTALL_NXT_DOWNPYMT=ISNULL(BDATE_INSTALL_NXT_DOWNPYMT,0),              
     @DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT=ISNULL(DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT,0),              
     @DUE_DAYS_DOWNPYMT=ISNULL(DUE_DAYS_DOWNPYMT,0),              
     @DAYS_SUBSEQUENT_INSTALLMENTS=ISNULL(DAYS_SUBSEQUENT_INSTALLMENTS,0),              
     @SUBSEQUENT_INSTALLMENTS_OPTION=ISNULL(SUBSEQUENT_INSTALLMENTS_OPTION,0),              
  @MODE_OF_DOWNPAY=  MODE_OF_DOWNPAY              
                   
                   
     FROM ACT_INSTALL_PLAN_DETAIL PLAND WITH(NOLOCK)                          
      WHERE IDEN_PLAN_ID=@PLAN_ID                 
                   
                   
     IF @@ERROR <> 0                            
  GOTO ERRHANDLER                  
              
       
     IF(@PROCESS_ID = @TRAN_TYPE_NBS_INPREOGRESS OR @PROCESS_ID=@TRAN_TYPE_NBS_COMMIT)                      
   SELECT @TRAN_TYPE='NBS'                      
     ELSE  IF(@PROCESS_ID=@TRAN_TYPE_REN_INPREOGRESS OR @PROCESS_ID=@TRAN_TYPE_REN_COMMIT)                      
   SELECT @TRAN_TYPE='REN'                      
     ELSE  IF(@PROCESS_ID=@TRAN_TYPE_END_INPREOGRESS OR @PROCESS_ID=@TRAN_TYPE_END_COMMIT)                      
   SELECT @TRAN_TYPE='END'                
     ELSE  IF(@PROCESS_ID= @TRAN_TYPE_CAN_INPROGRESS OR @PROCESS_ID=@TRAN_TYPE_CAN_COMMIT) ---cancellation installment will be genrated like as endorsement        
   SELECT @TRAN_TYPE='CAN'                           
  ELSE  IF(@PROCESS_ID= @TRAN_TYPE_REWRIT_INPROGRESS) ---cancellation installment will be genrated like as endorsement        
    SELECT @TRAN_TYPE='REW'        
     ELSE IF (@PROCESS_ID = '')                        ----if Policy Process Id null or blank then it consider as New Business                  
   SELECT @TRAN_TYPE='NBS'            
                 
                
                             
                          
                          
    IF(@PLAN_ID <> 0 )  --if Plan has been selected              
       BEGIN     
            
    IF EXISTS (SELECT RELEASED_STATUS from ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RELEASED_STATUS = 'Y')                
    BEGIN                
     SELECT @RELEASED_INSTALL_NO = COUNT(ISNULL(RELEASED_STATUS,0)) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RELEASED_STATUS = 'Y'              
                 
     IF(@NO_OF_PAYMENTS <= @RELEASED_INSTALL_NO)                 --- If no of released installment is greater then payment number to be done in selected plan id.                
      BEGIN              
     SELECT @PRM_DIST_TYPE = 14675               
     -- GOTO errorMsgINSTALLMENTNO                
      END              
      SELECT * INTO #temp_INSTALLMENT_DETAILS FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RELEASED_STATUS = 'Y'               
      SELECT @UPDATE_INSTALL_DATA_FLAG = 1;                     
    END                
    ELSE               
     /*   if policy any installment have paid after that plan change then it               
    also check selected plan no of payments inn that plan .              
    */              
     BEGIN              
                    
         SELECT @RELEASED_INSTALL_NO = COUNT(ISNULL(RELEASED_STATUS,0)) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND RELEASED_STATUS = 'Y'           
   
               
     IF(@NO_OF_PAYMENTS <= @RELEASED_INSTALL_NO)              
        BEGIN              
        SELECT @PRM_DIST_TYPE = 14675               
       --GOTO errorMsgINSTALLMENTNO                  
        END              
                 
                 
   DELETE FROM  POL_INSTALLMENT_BOLETO where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
     
   DELETE FROM ACT_POLICY_CO_BILLING_DETAILS  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
   
   DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS_HISTORY   where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                

   DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
   
   DELETE FROM ACT_POLICY_INSTALL_PLAN_DATA where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
        
       DELETE FROM POL_INSTALLMENT_BOLETO  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID= @POLICY_VERSION_ID                
  
         --kuldeep on july 9,20112            
      -- DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS_HISTORY  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID= @POLICY_VERSION_ID                
                     
       --DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID= @POLICY_VERSION_ID                
   IF @@ERROR <> 0    
    GOTO ERRHANDLER                       
        DELETE FROM ACT_POLICY_INSTALL_PLAN_DATA  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                         
     
   IF @@ERROR <> 0                            
    GOTO ERRHANDLER                       
                     
        --if previously installment genrated with premium.now premium is and all amount entered is 0 then system delete the genrated installment        
        --IF(ISNULL(@TOTAL_PREMIUM,0)+ ISNULL(@TOTAL_INTEREST_AMOUNT,0)+ISNULL(@TOTAL_FEES,0)+ISNULL(@TOTAL_TAXES,0) = 0)      
        --Changed by Kuldeep    
        IF(ISNULL(@TOTAL_AMOUNT,0) = 0)        
   BEGIN   
   --kuldeep 09-jul-2012     
    DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID= @POLICY_VERSION_ID                
     
    DELETE FROM ACT_POLICY_INSTALL_PLAN_DATA  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                      

    RETURN          
   END         
    END               
        END                     
                                   
      --SELECT @TOTAL_AMOUNT = @TOTAL_PREMIUM + @TOTAL_INTEREST_AMOUNT + @TOTAL_FEES + @TOTAL_TAXES            ----   calculate total amount with add TOTAL PREMIUM+TAXES+FEES+INTEREST                     
              
                  
     IF (@TRAN_TYPE in ('NBS','REN','REW'))                  
       BEGIN               
             /* in new business TOTAL_CHANGE_INFORCE_PRM will same as total policy premium */              
            
       INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA               --insert policy premium details according to the selected plan                              
       (                              
       CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,PLAN_ID,                            
       APP_ID,APP_VERSION_ID,PLAN_DESCRIPTION,PLAN_TYPE,                            
       NO_OF_PAYMENTS,MONTHS_BETWEEN,PERCENT_BREAKDOWN1,                           
       PERCENT_BREAKDOWN2,PERCENT_BREAKDOWN3,PERCENT_BREAKDOWN4,                            
       PERCENT_BREAKDOWN5,PERCENT_BREAKDOWN6,PERCENT_BREAKDOWN7,                            
       PERCENT_BREAKDOWN8,PERCENT_BREAKDOWN9,PERCENT_BREAKDOWN10,                            
       PERCENT_BREAKDOWN11,PERCENT_BREAKDOWN12,MODE_OF_DOWN_PAYMENT,                            
       INSTALLMENTS_IN_DOWN_PAYMENT,MODE_OF_PAYMENT,CURRENT_TERM,                        
       IS_ACTIVE_PLAN,TOTAL_PREMIUM,TOTAL_INTEREST_AMOUNT,                            
       TOTAL_FEES,TOTAL_TAXES,TOTAL_AMOUNT,TRAN_TYPE,TOTAL_TRAN_PREMIUM                
       ,TOTAL_TRAN_INTEREST_AMOUNT,TOTAL_TRAN_FEES,TOTAL_TRAN_TAXES,TOTAL_TRAN_AMOUNT,TOTAL_CHANGE_INFORCE_PRM,TOTAL_INFO_PRM,              
       CREATED_BY,CREATED_DATETIME ,PRM_DIST_TYPE ,TOTAL_STATE_FEES,TOTAL_TRAN_STATE_FEES                          
        )VALUES(                            
       @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@PLAN_ID,                            
       @POLICY_ID,@POLICY_VERSION_ID,@PLAN_DESCRIPTION,@PLAN_TYPE,                            
       @NO_OF_PAYMENTS,@MONTHS_BETWEEN,@PERCENT_BREAKDOWN1,                            
       @PERCENT_BREAKDOWN2,@PERCENT_BREAKDOWN3,@PERCENT_BREAKDOWN4,                            
       @PERCENT_BREAKDOWN5,@PERCENT_BREAKDOWN6,@PERCENT_BREAKDOWN7,                            
       @PERCENT_BREAKDOWN8,@PERCENT_BREAKDOWN9,@PERCENT_BREAKDOWN10,                            
       @PERCENT_BREAKDOWN11,@PERCENT_BREAKDOWN12,@MODE_OF_DOWNPAY,                            
       @INSTALLMENTS_IN_DOWN_PAYMENT,@MODE_OF_PAYMENT,@CURRENT_TERM,                            
       @IS_ACTIVE_PLAN,@TOTAL_PREMIUM,@TOTAL_INTEREST_AMOUNT,                            
       @TOTAL_FEES,@TOTAL_TAXES,@TOTAL_AMOUNT,@TRAN_TYPE,                
       @TOTAL_PREMIUM,@TOTAL_INTEREST_AMOUNT,@TOTAL_FEES,@TOTAL_TAXES,@TOTAL_AMOUNT,@TOTAL_PREMIUM,@TOTAL_PREMIUM,              
       @CREATED_BY,@CREATED_DATETIME   ,@PRM_DIST_TYPE  ,@TOTAL_STATE_FEES, @TOTAL_STATE_FEES                   
       )  
                 
        --ADDED BY kULDEEP ON 8-FEB-2012 FOR TFS 3408  
        EXEC INSERT_POL_BILLING_DETAILS @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@TOTAL_PREMIUM,@TOTAL_INTEREST_AMOUNT,@TOTAL_BEFORE_GST,@TOTAL_TAXES,@TOTAL_AFTER_GST,@GROSS_COMMISSION,@GST_ON_COMMISSION,@TOTAL_COMM_AFTER_GST,@TOTAL_AMOUNT,@CREATED_BY,
@CREATED_DATETIME  
             
   
     END                  
   ELSE IF(@TRAN_TYPE IN ('END','CAN'))                  
       BEGIN               
      
       SELECT @PREV_VERSION = POLICY_VERSION_ID,              
    @END_EFFECTIVE_DATE = ISNULL(EFFECTIVE_DATETIME,@POLICY_EFFECTIVE_DATE), ---If Endorsment effective date is null the policy effective date will be consider as endorsment effective date              
    @END_EXPI_DATE=EXPIRY_DATE              
       FROM POL_POLICY_PROCESS  WITH(NOLOCK)                                
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND  NEW_POLICY_ID = @POLICY_ID AND  NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID                                             
           AND UPPER(PROCESS_STATUS) <> 'ROLLBACK'                   
          SELECT @END_EFFEVTIVE_DAYS =ISNULL(DATEDIFF(DAY,@END_EFFECTIVE_DATE,@END_EXPI_DATE),0)              
                      
    IF EXISTS (SELECT CUSTOMER_ID FROM ACT_POLICY_INSTALL_PLAN_DATA WITH(NOLOCK) WHERE  CUSTOMER_ID=@CUSTOMER_ID                
     AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @PREV_VERSION )                
     BEGIN                

     SELECT  @PREV_TOTAL_PREMIUM  = TOTAL_PREMIUM ,                         
     @PREV_FEES= ISNULL(INSTALL_DATA.TOTAL_FEES,0) ,                 
     @PREV_TAXES= ISNULL(INSTALL_DATA.TOTAL_TAXES,0),                
     @PREV_INTEREST= ISNULL(INSTALL_DATA.TOTAL_INTEREST_AMOUNT,0)              
     FROM ACT_POLICY_INSTALL_PLAN_DATA INSTALL_DATA WITH(NOLOCK) WHERE  INSTALL_DATA.CUSTOMER_ID=@CUSTOMER_ID                
      AND INSTALL_DATA.POLICY_ID = @POLICY_ID AND INSTALL_DATA.POLICY_VERSION_ID = @PREV_VERSION                
                               
     END                
      ELSE                
     BEGIN                      
      SELECT @PREV_TOTAL_PREMIUM = SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES COV WITH(NOLOCK) WHERE COV.CUSTOMER_ID=@CUSTOMER_ID                
       AND COV.POLICY_ID = @POLICY_ID AND COV.POLICY_VERSION_ID = @PREV_VERSION                
     END                  
                          
                        
       SELECT @DATA_TOTAL_TRAN_PREMIUM = ROUND((@TOTAL_PREMIUM - @PREV_TOTAL_PREMIUM),2)                
                              
    SELECT @END_FEES = @TOTAL_FEES - @PREV_FEES;                
    SELECT @END_TAXES = @TOTAL_TAXES - @PREV_TAXES;                
    SELECT @END_INTEREST = @TOTAL_INTEREST_AMOUNT  - @PREV_INTEREST;                
                  
    IF(@END_EFFEVTIVE_DAYS<>0)              
     BEGIN              
         SELECT @DATA_TOTAL_TRAN_PREMIUM =ROUND((@DATA_TOTAL_TRAN_PREMIUM * @END_EFFEVTIVE_DAYS/365),2)--round amount for 0 place              
     END              
    --END HERE              
                  
                     
                                    
       SELECT @DATA_TOTAL_TRAN_INTEREST_AMOUNT = @TOTAL_INTEREST_AMOUNT                
       SELECT @DATA_TOTAL_TRAN_FEES = @TOTAL_FEES;                
       SELECT @DATA_TOTAL_TRAN_TAXES = @TOTAL_TAXES;                 
                     
                 
                      
    SELECT @DATA_TOTAL_TRAN_AMOUNT = ROUND((@DATA_TOTAL_TRAN_PREMIUM + @END_FEES + @END_TAXES + @END_INTEREST),2)              
                  
                  
                 --SELECT @TOTAL_AMOUNT =  @TOTAL_PREMIUM + @TOTAL_INTEREST_AMOUNT + @TOTAL_TAXES + @TOTAL_FEES             Comment by Kuldeep as @total_amount is  already calculated in parameters  
                   
                  /* in new business TOTAL_CHANGE_INFORCE_PRM will total total change in premium */              
               
    INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA               --insert policy premium details according to the selected plan                              
    (                              
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,PLAN_ID,                            
    APP_ID,APP_VERSION_ID,PLAN_DESCRIPTION,PLAN_TYPE,                            
    NO_OF_PAYMENTS,MONTHS_BETWEEN,PERCENT_BREAKDOWN1,                            
    PERCENT_BREAKDOWN2,PERCENT_BREAKDOWN3,PERCENT_BREAKDOWN4,                            
    PERCENT_BREAKDOWN5,PERCENT_BREAKDOWN6,PERCENT_BREAKDOWN7,                            
    PERCENT_BREAKDOWN8,PERCENT_BREAKDOWN9,PERCENT_BREAKDOWN10,                            
    PERCENT_BREAKDOWN11,PERCENT_BREAKDOWN12,MODE_OF_DOWN_PAYMENT,                            
    INSTALLMENTS_IN_DOWN_PAYMENT,MODE_OF_PAYMENT,CURRENT_TERM,                            
    IS_ACTIVE_PLAN,TOTAL_PREMIUM,TOTAL_INTEREST_AMOUNT,                            
    TOTAL_FEES,TOTAL_TAXES,TOTAL_AMOUNT,TRAN_TYPE,TOTAL_TRAN_PREMIUM,                  
    TOTAL_TRAN_FEES,TOTAL_TRAN_INTEREST_AMOUNT,TOTAL_TRAN_TAXES,                  
    TOTAL_TRAN_AMOUNT,TOTAL_CHANGE_INFORCE_PRM,PRM_DIST_TYPE,TOTAL_INFO_PRM,              
    CREATED_BY,CREATED_DATETIME,TOTAL_STATE_FEES,TOTAL_TRAN_STATE_FEES              
     )VALUES(                            
    @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@PLAN_ID,                            
    @POLICY_ID,@POLICY_VERSION_ID,@PLAN_DESCRIPTION,@PLAN_TYPE,                            
    @NO_OF_PAYMENTS,@MONTHS_BETWEEN,@PERCENT_BREAKDOWN1,                            
    @PERCENT_BREAKDOWN2,@PERCENT_BREAKDOWN3,@PERCENT_BREAKDOWN4,                            
    @PERCENT_BREAKDOWN5,@PERCENT_BREAKDOWN6,@PERCENT_BREAKDOWN7,                            
    @PERCENT_BREAKDOWN8,@PERCENT_BREAKDOWN9,@PERCENT_BREAKDOWN10,                            
    @PERCENT_BREAKDOWN11,@PERCENT_BREAKDOWN12,@MODE_OF_DOWNPAY,                            
    @INSTALLMENTS_IN_DOWN_PAYMENT,@MODE_OF_PAYMENT,@CURRENT_TERM,                            
    @IS_ACTIVE_PLAN,@TOTAL_PREMIUM,@TOTAL_INTEREST_AMOUNT,                            
    @TOTAL_FEES,@TOTAL_TAXES,@TOTAL_AMOUNT,@TRAN_TYPE,@TOTAL_TRAN_PREMIUM,                  
    @TOTAL_TRAN_FEES,@TOTAL_TRAN_INTEREST_AMOUNT,@TOTAL_TRAN_TAXES,                  
    @TOTAL_TRAN_AMOUNT,@TOTAL_CHANGE_INFORCE_PRM,@PRM_DIST_TYPE,@TOTAL_INFO_PRM,              
    @CREATED_BY,@CREATED_DATETIME ,@TOTAL_STATE_FEES,@TOTAL_TRAN_STATE_FEES                         
      )                        
               
     --ADDED BY kULDEEP ON 8-FEB-2012 FOR TFS 3408  
        EXEC INSERT_POL_BILLING_DETAILS @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@TOTAL_PREMIUM,@TOTAL_INTEREST_AMOUNT,@TOTAL_BEFORE_GST,@TOTAL_TAXES,@TOTAL_AFTER_GST,@GROSS_COMMISSION,@GST_ON_COMMISSION,@TOTAL_COMM_AFTER_GST,@TOTAL_AMOUNT,@CREATED_BY,
@CREATED_DATETIME  
                                  
    IF @@ERROR <> 0                            
    GOTO ERRHANDLER                          
                                 
                  
              
       END ---End of else area for endorsment insert in ACT_INSTALL_PLAN_DATA              
                     
       /*Update Selected Plan in POL_CUSTOMER_POLICY_LIST */                  
       --Start              
                
       IF(@SELECTED_PLAN_ID <> @PLAN_ID)              
     BEGIN              
      UPDATE POL_CUSTOMER_POLICY_LIST SET INSTALL_PLAN_ID=@PLAN_ID,DOWN_PAY_MODE=@MODE_OF_DOWNPAY  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID              
     IF @@ERROR <> 0                            
       GOTO ERRHANDLER             
     END              
       --END Update Selected Plan in POL_CUSTOMER_POLICY_LIST              
                     
                     
    /*Select released installments amount sum */           
    --After that remaining amount will break in to installments              
    SELECT @RELEASED_INSTALL_NO = COUNT(RELEASED_STATUS),                 
       @SUM_INSTALLMENT_AMOUNT = SUM(ISNULL(INSTALLMENT_AMOUNT,0)),              
       @SUM_INSTALLMENT_INTEREST_AMOUNT = SUM(ISNULL(INTEREST_AMOUNT,0)),              
       @SUM_INSTALLMENT_FEE = SUM(ISNULL(FEE,0)),              
       @SUM_INSTALLMENT_TAXES = SUM(ISNULL(TAXES,0)),              
       @DETAILS_TRAN_PREMIUM_SUM = SUM(ISNULL(TRAN_PREMIUM_AMOUNT,0)),              
       @DETAILS_TRAN_FEES_SUM = SUM(ISNULL(TRAN_FEE,0)),               
       @DETAILS_TRAN_INTEREST_AMOUNT_SUM = SUM(ISNULL(TRAN_INTEREST_AMOUNT,0)) ,              
       @DETAILS_TRAN_TAXES_SUM = SUM(ISNULL(TRAN_TAXES,0))              
    FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID                 
      AND POLICY_ID=@POLICY_ID AND  POLICY_VERSION_ID = @POLICY_VERSION_ID AND RELEASED_STATUS = 'Y'                      
       IF @@ERROR <> 0                            
       GOTO ERRHANDLER                  
    --end here              
               
  /* 1. if policy satus is complete then policy installment date consider from policy complete date                
     2. if policy status is application date consider from application created date              
  */              
              
  --14448  --Application Submission              
  --14449  --Policy Issuance              
  --14450  --Policy Effective              
  /*calculating inslallment date according to selected plan id  */              
               
   DECLARE @INSTALLMENT_NUMBER INT =  0      --- installment no                
   IF(@RELEASED_INSTALL_NO <> 0)                
   SELECT @INSTALLMENT_NUMBER = @RELEASED_INSTALL_NO + 1                
   ELSE                
     SELECT @INSTALLMENT_NUMBER =  1       --- If any installment is not paid then installemnt number no sarts with Installment # 1.                
     SELECT @REMAIN_INSTALL =@NO_OF_PAYMENTS - @RELEASED_INSTALL_NO               
              
IF(@TRAN_TYPE in ('NBS','REN','REW')) --if user select genrate end installment according to installment plan             
 BEGIN         
         
     IF(@NO_OF_PAYMENTS > 0)                                     --- check selected plan no of payment                          
  BEGIN                            
   WHILE(@NO_OF_PAYMENTS >= @INSTALLMENT_NUMBER)            --- Loop for insert every assign installment detail which                   
       BEGIN                         
      SELECT @PERCENTAG_OF_PREMIUM =                       --- select installment percentage break down                           
      CASE @INSTALLMENT_NUMBER                              
       WHEN 1 THEN                              
       @PERCENT_BREAKDOWN1                              
       WHEN 2 THEN                              
       @PERCENT_BREAKDOWN2                                   
       WHEN 3 THEN                              
       @PERCENT_BREAKDOWN3                              
       WHEN 4 THEN                              
       @PERCENT_BREAKDOWN4                              
       WHEN 5 THEN                              
       @PERCENT_BREAKDOWN5                  
       WHEN 6 THEN                              
       @PERCENT_BREAKDOWN6                              
       WHEN 7 THEN                              
       @PERCENT_BREAKDOWN7                              
       WHEN 8 THEN                              
       @PERCENT_BREAKDOWN8                              
       WHEN 9 THEN                              
       @PERCENT_BREAKDOWN9                              
       WHEN 10 THEN                   
       @PERCENT_BREAKDOWN10                              
       WHEN 11 THEN                              
       @PERCENT_BREAKDOWN11                              
       WHEN 12 THEN                              
       @PERCENT_BREAKDOWN12                              
       END                
                               
     IF(@TRAN_TYPE IN ('NBS','REN','REW'))        
      BEGIN                  
        SELECT @DETAILS_TRAN_FEES =  0,               
        @DETAILS_TRAN_INTEREST_AMOUNT = 0   ,               
        @DETAILS_TRAN_PREMIUM = 0    ,              
        @DETAILS_TRAN_TAXES = 0    ,              
        @DETAILS_TRAN_TOTAL=0;                
                      
                
   /******/              
      SELECT @INSTALLMENT_AMOUNT = ROUND(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_PREMIUM), 2)        ---calculate installment amount form  total premium according to installment percent                               
      SELECT @SUM_INSTALLMENT_AMOUNT = isnull(@SUM_INSTALLMENT_AMOUNT,0) + @INSTALLMENT_AMOUNT --+ @DETAILS_TRAN_PREMIUM;              -- Get all genrated installment premium SUM for check if it greater or less then total premium                        
                                       
      SELECT @INSTALLMENT_INTEREST_AMOUNT= ROUND(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_INTEREST_AMOUNT), 2)   --calculate installment Interest amount form  total Interest amount according to installment percent                          
      SELECT @SUM_INSTALLMENT_INTEREST_AMOUNT = isnull(@SUM_INSTALLMENT_INTEREST_AMOUNT,0) + @INSTALLMENT_INTEREST_AMOUNT --+ @DETAILS_TRAN_INTEREST_AMOUNT;                        
                                                  
         IF(@FEES_DIST_TYPE = 1)     --Adjust total fees in first installment                     
        BEGIN              
            IF (@INSTALLMENT_NUMBER = 1)              
            BEGIN              
          SELECT @INSTALLMENT_FEE = @TOTAL_FEES              
          SELECT @SUM_INSTALLMENT_FEE = @INSTALLMENT_FEE              
         END              
         ELSE               
        SELECT @INSTALLMENT_FEE = 0                          
        END              
          ELSE IF(@FEES_DIST_TYPE = 2) --distribute equally fees in all installments              
        BEGIN              
        SELECT @INSTALLMENT_FEE      =  ROUND(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_FEES), 2)                   --calculate installment Fees form  total policy Fees according to installment percent                          
        SELECT @SUM_INSTALLMENT_FEE =isnull(@SUM_INSTALLMENT_FEE,0)+ @INSTALLMENT_FEE --+ @DETAILS_TRAN_FEES;                
        END                 
       ELSE              
         BEGIN              
          SELECT @INSTALLMENT_FEE = 0                 
          SELECT @SUM_INSTALLMENT_FEE = 0;               
         END              
                           
                           
                                 
      SELECT @INSTALLMENT_TAXES     = ROUND(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_TAXES), 2)                  --calculate installment taxes form  total policy taxes according to installment percent                           
      SELECT @SUM_INSTALLMENT_TAXES = ISNULL(@SUM_INSTALLMENT_TAXES,0) + @INSTALLMENT_TAXES --+ @DETAILS_TRAN_TAXES                
                                 
  --Added By Kuldeep  
  SELECT @INSTALLMENT_COMM     = ROUND(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_COMM_AFTER_GST), 2)                  --calculate installment taxes form  total policy taxes according to installment percent                           
                                       
                                       
      IF(@NO_OF_PAYMENTS = @INSTALLMENT_NUMBER)                        
       BEGIN                  
                         
          ---1. FOR CALCULATE DIFFERENCE OF INSTALLMENT PREMIUM ACCORDING TO PERCENTAGE BREAKDOWN                        
                       
        SELECT @REMAINING_PART = 0;                    
          IF (@TOTAL_PREMIUM > @SUM_INSTALLMENT_AMOUNT)                        
        BEGIN                        
         SELECT @REMAINING_PART = @TOTAL_PREMIUM - @SUM_INSTALLMENT_AMOUNT;                        
         SELECT @INSTALLMENT_AMOUNT = @INSTALLMENT_AMOUNT + @REMAINING_PART                        
        END           
          ELSE IF(@TOTAL_PREMIUM < @SUM_INSTALLMENT_AMOUNT)                        
        BEGIN                        
         SELECT @REMAINING_PART = @SUM_INSTALLMENT_AMOUNT - @TOTAL_PREMIUM;                        
         SELECT @INSTALLMENT_AMOUNT = @INSTALLMENT_AMOUNT - @REMAINING_PART                        
        END                        
                                              
          ----End 1.                        
                                             
          ---2. FOR CALCULATE DIFFERENCE OF INSTALLMENT INTEREST AMOUNT ACCORDING TO PERCENTAGE BREAKDOWN                        
          SELECT @REMAINING_PART = 0                          
          IF (@TOTAL_INTEREST_AMOUNT > @SUM_INSTALLMENT_INTEREST_AMOUNT)                        
        BEGIN                                     
        SELECT @REMAINING_PART = @TOTAL_INTEREST_AMOUNT - @SUM_INSTALLMENT_INTEREST_AMOUNT;                        
        SELECT @INSTALLMENT_INTEREST_AMOUNT = @INSTALLMENT_INTEREST_AMOUNT + @REMAINING_PART                                     
        END                      
          ELSE IF(@TOTAL_INTEREST_AMOUNT < @SUM_INSTALLMENT_INTEREST_AMOUNT)                   
          BEGIN                        
    SELECT @REMAINING_PART = @SUM_INSTALLMENT_INTEREST_AMOUNT - @TOTAL_INTEREST_AMOUNT;                        
        SELECT @INSTALLMENT_INTEREST_AMOUNT = @INSTALLMENT_INTEREST_AMOUNT - @REMAINING_PART                        
          END                        
          --End 2.                        
                                             
          ---3. FOR CALCULATE DIFFERENCE OF INSTALLMENT FEE ACCORDING TO PERCENTAGE BREAKDOWN                        
          SELECT @REMAINING_PART = 0                                 
          IF (@TOTAL_FEES > @SUM_INSTALLMENT_FEE)                        
        BEGIN                                   
         SELECT @REMAINING_PART = @TOTAL_FEES - @SUM_INSTALLMENT_FEE;                        
         SELECT @INSTALLMENT_FEE = @INSTALLMENT_FEE + @REMAINING_PART                 
        END                        
          ELSE IF(@TOTAL_FEES < @SUM_INSTALLMENT_FEE)                        
        BEGIN                   
        SELECT @REMAINING_PART = @SUM_INSTALLMENT_FEE - @TOTAL_FEES;                        
        SELECT @INSTALLMENT_FEE = @INSTALLMENT_FEE - @REMAINING_PART                        
        END                        
                                              
        ---End 3.                        
                                              
         ---4. FOR CALCULATE DIFFERENCE OF INSTALLMENT Taxes ACCORDING TO PERCENTAGE BREAKDOWN                        
          SELECT @REMAINING_PART = 0                          
          IF (@TOTAL_TAXES > @SUM_INSTALLMENT_TAXES)                        
        BEGIN                                
        SELECT @REMAINING_PART = @TOTAL_TAXES - @SUM_INSTALLMENT_TAXES;                        
        SELECT @INSTALLMENT_TAXES = @INSTALLMENT_TAXES + @REMAINING_PART                        
        END                        
          ELSE IF(@TOTAL_TAXES < @SUM_INSTALLMENT_TAXES)                        
        BEGIN                        
         SELECT @REMAINING_PART = @SUM_INSTALLMENT_TAXES - @TOTAL_TAXES;                        
         SELECT @INSTALLMENT_TAXES = @INSTALLMENT_TAXES - @REMAINING_PART                        
        END                        
                                              
        ---End 4.                        
                                           
       END                         
                  
     IF @@ERROR <> 0                            
     GOTO ERRHANDLER                    
                               
     SELECT  @INSTALLMENT_TOTAL = @INSTALLMENT_AMOUNT + @INSTALLMENT_INTEREST_AMOUNT + @INSTALLMENT_FEE + @INSTALLMENT_TAXES -@INSTALLMENT_COMM  --calculate Total from Premium,taxes,fees,and intrest amount                         
     SELECT  @INSTALLMENT_TOTAL= ROUND(@INSTALLMENT_TOTAL,2)                  
                   
      END                  
     /* get installment date according to selected plan id from user created function*/              
                              
     SELECT @CALCULATED_INS_EFF_DATE=dbo.func_GetInstallmentEffectiveDate(@PLAN_ID,@INSTALLMENT_NUMBER,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,null,@TRAN_TYPE,NULL)               
                      
        IF (@DOWN_PAYMENT_PLAN = 1 AND @INSTALLMENT_NUMBER = 1)              
       SELECT @PAYMENT_MODE = @MODE_OF_DOWNPAY              
     ELSE                
            SELECT @PAYMENT_MODE = @MODE_OF_PAYMENT              
                      
                    SELECT @DETAILS_TRAN_TOTAL  = ROUND((@DETAILS_TRAN_PREMIUM + @DETAILS_TRAN_FEES + @DETAILS_TRAN_INTEREST_AMOUNT + @DETAILS_TRAN_TAXES),2);                 
                                   
     SELECT  @INSTALLMENT_TOTAL = @INSTALLMENT_AMOUNT + @INSTALLMENT_INTEREST_AMOUNT + @INSTALLMENT_FEE + @INSTALLMENT_TAXES -@INSTALLMENT_COMM   --calculate Total from Premium,taxes,fees,and intrest amount                         
     SELECT  @INSTALLMENT_TOTAL= ROUND(@INSTALLMENT_TOTAL,2)                 
                      
       INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS(                            
       CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,                            
       APP_ID,APP_VERSION_ID,                           
       INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,                          
       INSTALLMENT_NO,RISK_ID,RISK_TYPE,PAYMENT_MODE,                            
       CURRENT_TERM,PERCENTAG_OF_PREMIUM,INSTALLMENT_AMOUNT,              
       INTEREST_AMOUNT,                          
       FEE,TAXES,TOTAL,CREATED_BY,CREATED_DATETIME,              
       TRAN_PREMIUM_AMOUNT                    
       ,TRAN_FEE,TRAN_TAXES,TRAN_INTEREST_AMOUNT,TRAN_TOTAL,CO_APPLICANT_ID               
       )values(                            
       @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,                            
       @POLICY_ID,@POLICY_VERSION_ID,              
       @CALCULATED_INS_EFF_DATE,'N',                            
       @INSTALLMENT_NUMBER,@RISK_ID,@RISK_TYPE,@PAYMENT_MODE,              
       @CURRENT_TERM,@PERCENTAG_OF_PREMIUM,@INSTALLMENT_AMOUNT,              
       @INSTALLMENT_INTEREST_AMOUNT,                            
       @INSTALLMENT_FEE,@INSTALLMENT_TAXES,@INSTALLMENT_TOTAL ,@CREATED_BY,@CREATED_DATETIME,               
       @INSTALLMENT_AMOUNT,            
       @INSTALLMENT_FEE,@INSTALLMENT_TAXES,@INSTALLMENT_INTEREST_AMOUNT,@INSTALLMENT_TOTAL            
       --changed By Lalit for NBS premium ,tran premium should be same as premium in NBS            
       --@DETAILS_TRAN_PREMIUM                  
       --,@DETAILS_TRAN_FEES,@DETAILS_TRAN_TAXES,@DETAILS_TRAN_INTEREST_AMOUNT,@DETAILS_TRAN_TOTAL            
       ,@PRIMARY_APPLICANT_ID                  
       )                       
                                  
                 
                 
     IF @@ERROR <> 0                            
       GOTO ERRHANDLER                            
                                             
     SELECT @INSTALLMENT_NUMBER = @INSTALLMENT_NUMBER + 1        --------increment for installment no                          
                                  
       END     ---End Of While Loop               
   END    ---End If  install>0     24              
     END --end tran_type = NBS                   
ELSE IF(@TRAN_TYPE IN ('END','CAN'))--Kuldeep AND @PRM_DIST_TYPE <> 14855)              
 BEGIN         
            
  IF (@TOTAL_TRAN_PREMIUM <> 0)              
    BEGIN              
       
                 
      DECLARE @NBS_VERSION INT = 0 ,              
      @NO_INSTALLMENT  INT = 0,              
      @END_INS_EFF_DATE DATETIME,              
      @NBS_RELEASED_PAY INT = 0,              
      @END_INSTALLMENT_COUNTER INT = 0              
                    
    IF EXISTS(SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND NEW_POLICY_ID=@POLICY_ID AND PROCESS_ID=25)              
      BEGIN              
    SELECT @NBS_VERSION = NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND NEW_POLICY_ID=@POLICY_ID AND PROCESS_ID=25              
                 
      END              
                    
      SELECT @NO_INSTALLMENT = MAX(ISNULL(INSTALLMENT_NO,0)) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID --AND POLICY_VERSION_ID=@NBS_VERSION              
                    
                  
     IF(@NO_INSTALLMENT IS NULL OR @NO_INSTALLMENT = NULL)               
      BEGIN              
    SELECT @NO_INSTALLMENT = @NO_OF_PAYMENTS               
    SELECT @PRM_DIST_TYPE = 14675              
    SELECT @INSTALLMENT_NUMBER = 0              
       END              
    --  SELECT   @NO_OF_PAYMENTS , @NO_INSTALLMENT               
    --  IF(@NO_OF_PAYMENTS >= @NO_INSTALLMENT AND @PRM_DIST_TYPE <> 14675)               
    --BEGIN              
    --SELECT @PRM_DIST_TYPE = 14675              
    --UPDATE ACT_POLICY_INSTALL_PLAN_DATA SET PRM_DIST_TYPE = @PRM_DIST_TYPE WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID              
    -- END               
     SELECT @END_INS_EFF_DATE = CASE                  
    WHEN  COMPLETED_DATETIME >EFFECTIVE_DATETIME               
    THEN              
     COMPLETED_DATETIME              
    ELSE              
     EFFECTIVE_DATETIME              
    END              
                  
    FROM POL_POLICY_PROCESS where CUSTOMER_ID=@CUSTOMER_ID and NEW_POLICY_ID=@POLICY_ID and NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID              
                        
          SELECT  @END_INS_EFF_DATE = ISNULL(@END_INS_EFF_DATE,@POLICY_EFFECTIVE_DATE)              
              
    SELECT @REMAIN_INSTALL = COUNT(distinct INSTALLMENT_NO) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)  WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID=@POLICY_ID   AND RELEASED_STATUS='N' AND INSTALLMENT_EFFECTIVE_DATE >= @END_INS_EFF_DATE          
   
   
                
     SELECT @NBS_RELEASED_PAY =  @NO_INSTALLMENT - @REMAIN_INSTALL ;              
        SELECT @INSTALLMENT_NUMBER =  @NBS_RELEASED_PAY + 1              
                       
                      
       /*If Endorsment effective date grater from last installment to be payed               
       or last installment payed before endorsment launched for that              
       it create a seperate installment for that endorsment premium*/              
                     
    IF NOT EXISTS(SELECT INSTALLMENT_EFFECTIVE_DATE FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND INSTALLMENT_NO = @NO_INSTALLMENT AND (INSTALLMENT_EFFECTIVE_DATE >= @END_INS_EFF_DATE     
  
   
      
        
        
           
     OR RELEASED_STATUS='Y'))                
             BEGIN              
                           
              SELECT @PRM_DIST_TYPE = 14675              
              SELECT @INSTALLMENT_NUMBER = 0              
              UPDATE ACT_POLICY_INSTALL_PLAN_DATA SET PRM_DIST_TYPE = @PRM_DIST_TYPE WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                           
                           
             END                
                                
        /* Endorsment premium distribution options              
    --14672 Equally Distribution in Installments               
    --14673 Adjust in First Installment              
    --14674 Adjust in Last Installment                  
    --14675 Separate Installment              
    -- */                
                 
    WHILE(@NO_INSTALLMENT >= @INSTALLMENT_NUMBER)                 
    BEGIN                   
      SELECT @END_INSTALLMENT_COUNTER = @END_INSTALLMENT_COUNTER + 1              
    IF(@PRM_DIST_TYPE = 14672 OR @PRM_DIST_TYPE = NULL OR @PRM_DIST_TYPE IS NULL Or @PRM_DIST_TYPE = 0) --If Prm distributed equally               
     BEGIN               
                  
        SELECT @CALCULATED_INS_EFF_DATE = INSTALLMENT_EFFECTIVE_DATE FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)  WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND INSTALLMENT_NO =@INSTALLMENT_NUMBER               
                       
      SELECT @DETAILS_TRAN_PREMIUM = ROUND((@TOTAL_TRAN_PREMIUM/@REMAIN_INSTALL), 2)  --                 
      SELECT @DETAILS_TRAN_PREMIUM_SUM = ISNULL(@DETAILS_TRAN_PREMIUM_SUM,0) + @DETAILS_TRAN_PREMIUM               
                    
      --SELECT @DETAILS_TRAN_FEES =  ROUND((@TOTAL_TRAN_FEES/@REMAIN_INSTALL), 2)   --                
      --SELECT @DETAILS_TRAN_FEES_SUM = ISNULL(@DETAILS_TRAN_FEES_SUM,0) + @DETAILS_TRAN_FEES                  
                            
      SELECT @DETAILS_TRAN_INTEREST_AMOUNT = ROUND((@TOTAL_TRAN_INTEREST_AMOUNT / @REMAIN_INSTALL), 2);                  
      SELECT @DETAILS_TRAN_INTEREST_AMOUNT_SUM = ISNULL(@DETAILS_TRAN_INTEREST_AMOUNT_SUM,0) + @DETAILS_TRAN_INTEREST_AMOUNT                  
                          
      SELECT @DETAILS_TRAN_TAXES = ROUND((@TOTAL_TRAN_TAXES / @REMAIN_INSTALL ), 2);                      
      SELECT @DETAILS_TRAN_TAXES_SUM = ISNULL(@DETAILS_TRAN_TAXES_SUM,0) + @DETAILS_TRAN_TAXES                 
              
     END              
    ELSE IF(@PRM_DIST_TYPE = 14673) --if prm adjust in first installment              
     BEGIN              
                   
     SELECT TOP 1 @CALCULATED_INS_EFF_DATE = INSTALLMENT_EFFECTIVE_DATE ,            
      @INSTALLMENT_NUMBER = INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS            
       WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND INSTALLMENT_EFFECTIVE_DATE >= @END_INS_EFF_DATE  ORDER BY INSTALLMENT_EFFECTIVE_DATE ,INSTALLMENT_NO asc                      
                      
          IF (@CALCULATED_INS_EFF_DATE IS NULL)              
         SELECT  @CALCULATED_INS_EFF_DATE = @END_INS_EFF_DATE              
                      
      SELECT @DETAILS_TRAN_PREMIUM = @TOTAL_TRAN_PREMIUM                   
      SELECT @DETAILS_TRAN_PREMIUM_SUM = @DETAILS_TRAN_PREMIUM;              
                    
      --SELECT @DETAILS_TRAN_FEES =  @TOTAL_TRAN_FEES               
      --SELECT @DETAILS_TRAN_FEES_SUM = @DETAILS_TRAN_FEES;              
                            
      SELECT @DETAILS_TRAN_INTEREST_AMOUNT = @TOTAL_TRAN_INTEREST_AMOUNT;                       
            SELECT @DETAILS_TRAN_INTEREST_AMOUNT_SUM = @DETAILS_TRAN_INTEREST_AMOUNT;                          
                        
      SELECT @DETAILS_TRAN_TAXES = @TOTAL_TRAN_TAXES;                      
      SELECT @DETAILS_TRAN_TAXES_SUM = @DETAILS_TRAN_TAXES;              
                   
     END                   
                           
             ELSE IF(@PRM_DIST_TYPE = 14674) --if prm adjust in last installment          
     BEGIN              
                   
      SELECT TOP 1 @CALCULATED_INS_EFF_DATE = INSTALLMENT_EFFECTIVE_DATE , @INSTALLMENT_NUMBER = INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)            
        WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID  AND INSTALLMENT_EFFECTIVE_DATE >= @END_INS_EFF_DATE  ORDER BY INSTALLMENT_EFFECTIVE_DATE desc              
              
      IF (@CALCULATED_INS_EFF_DATE IS NULL)              
         SELECT  @CALCULATED_INS_EFF_DATE = @END_INS_EFF_DATE              
              
      SELECT @DETAILS_TRAN_PREMIUM = @TOTAL_TRAN_PREMIUM                   
      SELECT @DETAILS_TRAN_PREMIUM_SUM = @DETAILS_TRAN_PREMIUM;              
                    
      --SELECT @DETAILS_TRAN_FEES =  @TOTAL_TRAN_FEES               
      --SELECT @DETAILS_TRAN_FEES_SUM = @DETAILS_TRAN_FEES;              
                            
      SELECT @DETAILS_TRAN_INTEREST_AMOUNT = @TOTAL_TRAN_INTEREST_AMOUNT;                       
            SELECT @DETAILS_TRAN_INTEREST_AMOUNT_SUM = @DETAILS_TRAN_INTEREST_AMOUNT;                          
                        
      SELECT @DETAILS_TRAN_TAXES = @TOTAL_TRAN_TAXES;                      
      SELECT @DETAILS_TRAN_TAXES_SUM = @DETAILS_TRAN_TAXES;              
              
      SELECT @REMAIN_INSTALL = @INSTALLMENT_NUMBER;              
     END                 
              ELSE IF(@PRM_DIST_TYPE = 14675) --if prm adjust in seprate installment              
     BEGIN              
                   
      SELECT  @INSTALLMENT_NUMBER= MAX(distinct INSTALLMENT_NO)                     
       FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)                     
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID               
                    
                    
      SELECT @CALCULATED_INS_EFF_DATE =INSTALLMENT_EFFECTIVE_DATE              
       FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)                     
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND INSTALLMENT_NO = @INSTALLMENT_NUMBER                 
                    
      IF(@INSTALLMENT_NUMBER IS NULL)              
        SELECT @INSTALLMENT_NUMBER = @NO_INSTALLMENT              
                      
        IF (@CALCULATED_INS_EFF_DATE IS NULL)              
        SELECT  @CALCULATED_INS_EFF_DATE = @END_INS_EFF_DATE              
                       
         SELECT @INSTALLMENT_NUMBER = @INSTALLMENT_NUMBER + 1              
                       
      SELECT @DETAILS_TRAN_PREMIUM = @TOTAL_TRAN_PREMIUM                   
      SELECT @DETAILS_TRAN_PREMIUM_SUM = @DETAILS_TRAN_PREMIUM;              
                  
      --SELECT @DETAILS_TRAN_FEES =  @TOTAL_TRAN_FEES               
      --SELECT @DETAILS_TRAN_FEES_SUM = @DETAILS_TRAN_FEES;              
                            
      SELECT @DETAILS_TRAN_INTEREST_AMOUNT = @TOTAL_TRAN_INTEREST_AMOUNT;                       
            SELECT @DETAILS_TRAN_INTEREST_AMOUNT_SUM = @DETAILS_TRAN_INTEREST_AMOUNT;                          
                        
      SELECT @DETAILS_TRAN_TAXES = @TOTAL_TRAN_TAXES;                      
      SELECT @DETAILS_TRAN_TAXES_SUM = @DETAILS_TRAN_TAXES;              
                    
      SELECT @CALCULATED_INS_EFF_DATE=dbo.func_GetInstallmentEffectiveDate(@PLAN_ID,@INSTALLMENT_NUMBER,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@CALCULATED_INS_EFF_DATE,@TRAN_TYPE,NULL)               
      if(@CALCULATED_INS_EFF_DATE < @END_INS_EFF_DATE)              
       BEGIN              
       SELECT @CALCULATED_INS_EFF_DATE=dbo.func_GetInstallmentEffectiveDate(@PLAN_ID,@INSTALLMENT_NUMBER,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@END_INS_EFF_DATE,@TRAN_TYPE,NULL)               
       END              
                     
                        IF(@CALCULATED_INS_EFF_DATE > @END_EXPI_DATE )              
                          SELECT  @CALCULATED_INS_EFF_DATE  = @END_EXPI_DATE ;              
      SELECT @REMAIN_INSTALL = @INSTALLMENT_NUMBER;              
     END                
               
                          
                IF(@FEES_DIST_TYPE = 1)   --adjust in first installment               
       BEGIN              
        IF(@END_INSTALLMENT_COUNTER = 1)              
         BEGIN              
          SELECT @DETAILS_TRAN_FEES = @TOTAL_TRAN_FEES                
          SELECT @SUM_INSTALLMENT_FEE = @TOTAL_TRAN_FEES               
         END              
        ELSE              
         BEGIN              
        SELECT @DETAILS_TRAN_FEES = 0              
         END               
       END              
     ELSE IF(@FEES_DIST_TYPE = 2)  --equally distribute in all installments              
      BEGIN              
       SELECT @DETAILS_TRAN_FEES =  ROUND((@TOTAL_TRAN_FEES/@REMAIN_INSTALL), 2) --                  
       SELECT @DETAILS_TRAN_FEES_SUM = ISNULL(@DETAILS_TRAN_FEES_SUM,0) + @DETAILS_TRAN_FEES                   
      END          
     ELSE  --Else adjust total fees in last installment                
      BEGIN               
       /* Here we set installment fees 0 for all installment,few line below logic               
          check when last installment to be insert then it will automatically check if inserted              
          installment fees sum is less then from total transaction fees applied on policy/endorsment              
          then it shifts difference in last installment.here 0 is used for all innstallments so it shift              
          total fees in last installment              
       */              
       SELECT @DETAILS_TRAN_FEES = 0                   
       SELECT @DETAILS_TRAN_FEES_SUM = 0;               
      END               
              
                          
                          
     --1 . calculate  if sum of all installment premium amount is greater or less than total tran premium amount                
     IF (@NO_INSTALLMENT =  @INSTALLMENT_NUMBER)              
      BEGIN                    
       IF(@DETAILS_TRAN_PREMIUM_SUM > @TOTAL_TRAN_PREMIUM)                
          BEGIN                             
         SELECT @REMAINING_PART = @DETAILS_TRAN_PREMIUM_SUM - @TOTAL_TRAN_PREMIUM ;                           
         SELECT  @DETAILS_TRAN_PREMIUM = @DETAILS_TRAN_PREMIUM - @REMAINING_PART;                 
          END                
       ELSE IF (@DETAILS_TRAN_PREMIUM_SUM < @TOTAL_TRAN_PREMIUM)                
         BEGIN                
           SELECT @REMAINING_PART = @TOTAL_TRAN_PREMIUM - @DETAILS_TRAN_PREMIUM_SUM ;                
           SELECT  @DETAILS_TRAN_PREMIUM = @DETAILS_TRAN_PREMIUM + @REMAINING_PART;                
         END                
      --End 1.                
                            
      SELECT @REMAINING_PART = 0 ;                
      --2 . calculate  if sum of all installment fee amount is greater than total tran fee amount                
       IF(@DETAILS_TRAN_FEES_SUM > @TOTAL_TRAN_FEES)                
          BEGIN                
         SELECT @REMAINING_PART = @DETAILS_TRAN_FEES_SUM - @TOTAL_TRAN_FEES ;                
         SELECT  @DETAILS_TRAN_FEES = @DETAILS_TRAN_FEES - @REMAINING_PART;                
          END                
       ELSE IF (@DETAILS_TRAN_FEES_SUM < @TOTAL_TRAN_FEES)                
         BEGIN                
           SELECT @REMAINING_PART = @TOTAL_TRAN_FEES - @DETAILS_TRAN_FEES_SUM ;                
           SELECT  @DETAILS_TRAN_FEES = @DETAILS_TRAN_FEES + @REMAINING_PART;                
         END                
                             
      --end 2.                
      SELECT @REMAINING_PART = 0 ;                
      --3 . calculate  if sum of all installment interest amount is greater than total tran interest amount                       
      IF(@DETAILS_TRAN_INTEREST_AMOUNT_SUM > @TOTAL_TRAN_INTEREST_AMOUNT)                      
          BEGIN                
         SELECT @REMAINING_PART = @DETAILS_TRAN_INTEREST_AMOUNT_SUM - @TOTAL_TRAN_INTEREST_AMOUNT ;                
         SELECT  @DETAILS_TRAN_INTEREST_AMOUNT = @DETAILS_TRAN_INTEREST_AMOUNT - @REMAINING_PART;                
          END                
       ELSE IF (@DETAILS_TRAN_INTEREST_AMOUNT_SUM < @TOTAL_TRAN_INTEREST_AMOUNT)                
         BEGIN                
  SELECT @REMAINING_PART = @TOTAL_TRAN_INTEREST_AMOUNT - @DETAILS_TRAN_INTEREST_AMOUNT_SUM ;                
           SELECT  @DETAILS_TRAN_INTEREST_AMOUNT = @DETAILS_TRAN_INTEREST_AMOUNT + @REMAINING_PART;                
         END                
                             
      --end 3.                
      SELECT @REMAINING_PART = 0 ;                
      --4 . calculate  if sum of all installment taxes amount is greater than total tran taxes amount                
      IF(@DETAILS_TRAN_TAXES_SUM > @TOTAL_TRAN_TAXES)               
         BEGIN                 
        SELECT @REMAINING_PART = @DETAILS_TRAN_TAXES_SUM - @TOTAL_TRAN_TAXES ;       
        SELECT  @DETAILS_TRAN_TAXES = @DETAILS_TRAN_TAXES - @REMAINING_PART;                
         END                
      ELSE IF (@DETAILS_TRAN_TAXES_SUM < @TOTAL_TRAN_TAXES)                
        BEGIN                              
          SELECT @REMAINING_PART = @TOTAL_TRAN_TAXES - @DETAILS_TRAN_TAXES_SUM ;                
          SELECT @DETAILS_TRAN_TAXES = @DETAILS_TRAN_TAXES + @REMAINING_PART;                
        END                
      END              
     --end 4.                
---     END --End Endorsment if begin                
                   
    --IF policy status is endorsment then instalment amount and other amount are same               
    --as transaction amount              
                  
    SELECT @INSTALLMENT_AMOUNT = @DETAILS_TRAN_PREMIUM,              
     @INSTALLMENT_INTEREST_AMOUNT = @DETAILS_TRAN_INTEREST_AMOUNT,                            
     @INSTALLMENT_FEE = @DETAILS_TRAN_FEES,              
     @INSTALLMENT_TAXES =@DETAILS_TRAN_TAXES,              
     @INSTALLMENT_TOTAL = @DETAILS_TRAN_TOTAL               
                      
                    SELECT @DETAILS_TRAN_TOTAL  = ROUND((@DETAILS_TRAN_PREMIUM + @DETAILS_TRAN_FEES + @DETAILS_TRAN_INTEREST_AMOUNT + @DETAILS_TRAN_TAXES),2);                      
     SELECT  @INSTALLMENT_TOTAL = ROUND((@INSTALLMENT_AMOUNT + @INSTALLMENT_INTEREST_AMOUNT + @INSTALLMENT_FEE + @INSTALLMENT_TAXES),2)   --calculate Total from Premium,taxes,fees,and intrest amount                         
             
      INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS(                            
       CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,                            
       APP_ID,APP_VERSION_ID,                           
       INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,                          
       INSTALLMENT_NO,RISK_ID,RISK_TYPE,PAYMENT_MODE,                            
       CURRENT_TERM,PERCENTAG_OF_PREMIUM,INSTALLMENT_AMOUNT,              
       INTEREST_AMOUNT,                          
       FEE,TAXES,TOTAL,CREATED_BY,CREATED_DATETIME,              
       TRAN_PREMIUM_AMOUNT                    
       ,TRAN_FEE,TRAN_TAXES,TRAN_INTEREST_AMOUNT,TRAN_TOTAL,CO_APPLICANT_ID                  
       )values(                       
       @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,   --here                         
       @POLICY_ID,@POLICY_VERSION_ID,              
       @CALCULATED_INS_EFF_DATE,'N',                            
       @INSTALLMENT_NUMBER,@RISK_ID,@RISK_TYPE,@MODE_OF_PAYMENT,              
       @CURRENT_TERM,@PERCENTAG_OF_PREMIUM,@INSTALLMENT_AMOUNT,              
       @INSTALLMENT_INTEREST_AMOUNT,                            
       @INSTALLMENT_FEE,@INSTALLMENT_TAXES,@INSTALLMENT_TOTAL ,@CREATED_BY,@CREATED_DATETIME,               
       @DETAILS_TRAN_PREMIUM                  
       ,@DETAILS_TRAN_FEES,@DETAILS_TRAN_TAXES,@DETAILS_TRAN_INTEREST_AMOUNT,@DETAILS_TRAN_TOTAL,@PRIMARY_APPLICANT_ID                  
       )               
                 
       IF @@ERROR <> 0                            
       GOTO ERRHANDLER                
                     
       IF(@PRM_DIST_TYPE = 14673) --if              
       SELECT @INSTALLMENT_NUMBER =   @NO_INSTALLMENT + 1              
       ELSE              
        SELECT @INSTALLMENT_NUMBER = @INSTALLMENT_NUMBER + 1              
                      
      END                            
       END              
    END              
ELSE --IF (@TRAN_TYPE IN ('END','CAN'))--kuldeep AND @PRM_DIST_TYPE = 14855)               
     BEGIN        
  
     --SELECT 'LALIT'        
      SELECT * INTO #tempINSTALLDETAILS  FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE 1 = 2        
      SELECT * INTO #tempPLAN_DETAILS  FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK)        
      WHERE IDEN_PLAN_ID = @PLAN_ID        
              
      DECLARE @MAX_INSTALLMENT_NO INT,@INDEX INT = 1        
              
      --SELECT @NO_OF_PAYMENTS --=  NO_OF_PAYMENTS FROM #tempPLAN_DETAILS WITH(NOLOCK)        
  
   --SELECT * FROM #tempPLAN_DETAILS        
           
   SELECT @MAX_INSTALLMENT_NO =  MAX(ISNULL(INSTALLMENT_NO,0))         
   FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)        
   WHERE CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID=@POLICY_ID        
   and POLICY_VERSION_ID < @POLICY_VERSION_ID        
           
           
           
   SELECT @INSTALLMENT_NUMBER = 0        
              
         WHILE(@INDEX <= @NO_OF_PAYMENTS)        
   BEGIN         
           
   SELECT @INSTALLMENT_NUMBER = @INSTALLMENT_NUMBER + 1        
           
   SELECT @PERCENTAG_OF_PREMIUM =          
    CASE @INDEX                              
     WHEN 1 THEN                              
     @PERCENT_BREAKDOWN1                              
     WHEN 2 THEN                             
     @PERCENT_BREAKDOWN2                                   
     WHEN 3 THEN                              
     @PERCENT_BREAKDOWN3                              
     WHEN 4 THEN                              
     @PERCENT_BREAKDOWN4                              
     WHEN 5 THEN                              
     @PERCENT_BREAKDOWN5                  
     WHEN 6 THEN                              
     @PERCENT_BREAKDOWN6                              
     WHEN 7 THEN                              
     @PERCENT_BREAKDOWN7                              
     WHEN 8 THEN                              
     @PERCENT_BREAKDOWN8                              
     WHEN 9 THEN                              
     @PERCENT_BREAKDOWN9                              
     WHEN 10 THEN                   
     @PERCENT_BREAKDOWN10                              
     WHEN 11 THEN                              
     @PERCENT_BREAKDOWN11                              
     WHEN 12 THEN                              
     @PERCENT_BREAKDOWN12                              
     END                 
             
             
             
   IF(@INDEX = 1)        
   SELECT @INSTALLMENT_FEE = @TOTAL_TRAN_FEES        
   ELSE        
    SELECT @INSTALLMENT_FEE = 0;        
           
           
   --SELECT @PERCENTAG_OF_PREMIUM        
           
   SELECT @INSTALLMENT_AMOUNT = ROUND(ISNULL(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_TRAN_PREMIUM),0),2)        
   SELECT @INSTALLMENT_INTEREST_AMOUNT = ROUND(ISNULL(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_TRAN_INTEREST_AMOUNT),0),2)        
   SELECT @INSTALLMENT_TAXES =  ROUND(ISNULL(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_TRAN_TAXES),0),2)    
   SELECT @INSTALLMENT_COMM=    ROUND(ISNULL(((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_COMM_AFTER_GST),0),2)   
   SELECT @INSTALLMENT_TOTAL = ROUND(@INSTALLMENT_AMOUNT+@INSTALLMENT_INTEREST_AMOUNT+@INSTALLMENT_TAXES+ @INSTALLMENT_FEE-@INSTALLMENT_COMM,2)        
           
            
   --SELECT @INSTALLMENT_AMOUNT,@INSTALLMENT_INTEREST_AMOUNT,@INSTALLMENT_FEE,        
   --@INSTALLMENT_TAXES,        
   --@INSTALLMENT_TOTAL --= ROUND(@INSTALLMENT_AMOUNT+@INSTALLMENT_INTEREST_AMOUNT+@INSTALLMENT_TAXES+ @INSTALLMENT_FEE,2)        
   --SELECT @INSTALLMENT_NUMBER,@INSTALLMENT_AMOUNT        
           
   SELECT @CALCULATED_INS_EFF_DATE = dbo.func_GetInstallmentEffectiveDate        
   (@PLAN_ID,@INDEX,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,ISNULL(@END_EFFECTIVE_DATE,@POLICY_EFFECTIVE_DATE),'END',@PRM_DIST_TYPE)        
     --itrack   # 919    
  --IF(@CALCULATED_INS_EFF_DATE > @POLICY_EXPIRY_DATE)        
  -- SELECT  @CALCULATED_INS_EFF_DATE = @POLICY_EXPIRY_DATE        
           
  --SELECT @CALCULATED_INS_EFF_DATE,@PRIMARY_APPLICANT_ID        
                   
   INSERT INTO #tempINSTALLDETAILS(        
       CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,                            
       APP_ID,APP_VERSION_ID,                           
       INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,                          
       INSTALLMENT_NO,RISK_ID,RISK_TYPE,PAYMENT_MODE,                            
       CURRENT_TERM,PERCENTAG_OF_PREMIUM,INSTALLMENT_AMOUNT,              
       INTEREST_AMOUNT,                          
       FEE,TAXES,TOTAL,CREATED_BY,CREATED_DATETIME,              
       TRAN_PREMIUM_AMOUNT                    
       ,TRAN_FEE,TRAN_TAXES,TRAN_INTEREST_AMOUNT,TRAN_TOTAL,CO_APPLICANT_ID,PAID_AMOUNT                  
       )values(                            
       @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,   --here                         
       @POLICY_ID,@POLICY_VERSION_ID,              
       @CALCULATED_INS_EFF_DATE,'N',                            
       @INSTALLMENT_NUMBER,@RISK_ID,@RISK_TYPE        
       ,@MODE_OF_PAYMENT,              
       @CURRENT_TERM,@PERCENTAG_OF_PREMIUM,        
       @INSTALLMENT_AMOUNT,              
       @INSTALLMENT_INTEREST_AMOUNT,                            
       @INSTALLMENT_FEE,@INSTALLMENT_TAXES,@INSTALLMENT_TOTAL ,@CREATED_BY,@CREATED_DATETIME,               
       @INSTALLMENT_AMOUNT                  
       ,@INSTALLMENT_FEE,@INSTALLMENT_TAXES,@INSTALLMENT_INTEREST_AMOUNT,        
       @INSTALLMENT_TOTAL,@PRIMARY_APPLICANT_ID,0                  
       )     
     
  IF @@ERROR <> 0                            
     GOTO ERRHANDLER                
           
           
   SELECT @INDEX = @INDEX + 1        
   END        
             
  SELECT @SUM_INSTALLMENT_AMOUNT =  SUM(INSTALLMENT_AMOUNT)        
  ,@SUM_INSTALLMENT_FEE =  SUM(FEE)        
  ,@SUM_INSTALLMENT_TAXES =  SUM(TAXES)        
  ,@SUM_INSTALLMENT_INTEREST_AMOUNT =  SUM(INTEREST_AMOUNT)        
  FROM #tempINSTALLDETAILS         
          
         
  IF(@SUM_INSTALLMENT_AMOUNT <> @TOTAL_TRAN_PREMIUM)        
  SELECT @DETAILS_TRAN_PREMIUM =  @TOTAL_TRAN_PREMIUM - @SUM_INSTALLMENT_AMOUNT        
          
        
          
     IF(@SUM_INSTALLMENT_TAXES <> @TOTAL_TRAN_TAXES)        
  SELECT @DETAILS_TRAN_TAXES = @TOTAL_TRAN_TAXES - @SUM_INSTALLMENT_TAXES         
          
  IF(@SUM_INSTALLMENT_INTEREST_AMOUNT <> @TOTAL_TRAN_INTEREST_AMOUNT)        
  SELECT @DETAILS_TRAN_INTEREST_AMOUNT =  @TOTAL_TRAN_INTEREST_AMOUNT - @SUM_INSTALLMENT_INTEREST_AMOUNT        
          
  --SELECT @DETAILS_TRAN_PREMIUM        
          
          
  UPDATE #tempINSTALLDETAILS SET         
  --SELECT         
    INSTALLMENT_AMOUNT = INSTALLMENT_AMOUNT + @DETAILS_TRAN_PREMIUM ,              
       INTEREST_AMOUNT = INTEREST_AMOUNT + @DETAILS_TRAN_INTEREST_AMOUNT,                          
       TAXES = TAXES+ @DETAILS_TRAN_TAXES,        
       TOTAL = TOTAL + @DETAILS_TRAN_PREMIUM +  @DETAILS_TRAN_INTEREST_AMOUNT + @DETAILS_TRAN_TAXES,        
       TRAN_PREMIUM_AMOUNT = TRAN_PREMIUM_AMOUNT + @DETAILS_TRAN_PREMIUM ,         
       TRAN_TAXES = TRAN_TAXES+ @DETAILS_TRAN_TAXES,        
       TRAN_INTEREST_AMOUNT = TRAN_INTEREST_AMOUNT + @DETAILS_TRAN_INTEREST_AMOUNT,           
       TRAN_TOTAL = TRAN_TOTAL  + @DETAILS_TRAN_PREMIUM +  @DETAILS_TRAN_INTEREST_AMOUNT + @DETAILS_TRAN_TAXES        
 --FROM #tempINSTALLDETAILS        
  WHERE INSTALLMENT_NO  = (SELECT MAX(INSTALLMENT_NO) FROM  #tempINSTALLDETAILS)        
          
   IF @@ERROR <> 0                            
     GOTO ERRHANDLER                

      INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS(        
       CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,                            
       APP_ID,APP_VERSION_ID,                           
       INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,                          
       INSTALLMENT_NO,RISK_ID,RISK_TYPE,PAYMENT_MODE,                            
       CURRENT_TERM,PERCENTAG_OF_PREMIUM,INSTALLMENT_AMOUNT,              
       INTEREST_AMOUNT,                          
       FEE,TAXES,TOTAL,CREATED_BY,CREATED_DATETIME,              
       TRAN_PREMIUM_AMOUNT                    
       ,TRAN_FEE,TRAN_TAXES,TRAN_INTEREST_AMOUNT,TRAN_TOTAL,CO_APPLICANT_ID  ,PAID_AMOUNT                
       )SELECT                            
       CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,                            
       APP_ID,APP_VERSION_ID,                           
       INSTALLMENT_EFFECTIVE_DATE,RELEASED_STATUS,             
       INSTALLMENT_NO,RISK_ID,RISK_TYPE,PAYMENT_MODE,                            
      CURRENT_TERM,PERCENTAG_OF_PREMIUM,INSTALLMENT_AMOUNT,              
       INTEREST_AMOUNT,                          
       FEE,TAXES,TOTAL,CREATED_BY,CREATED_DATETIME,              
       TRAN_PREMIUM_AMOUNT                    
       ,TRAN_FEE,TRAN_TAXES,TRAN_INTEREST_AMOUNT,TRAN_TOTAL,CO_APPLICANT_ID ,PAID_AMOUNT            
       FROM  #tempINSTALLDETAILS ORDER BY  INSTALLMENT_NO ASC        
  
   IF @@ERROR <> 0                            
    GOTO ERRHANDLER                    
             
            
  DROP TABLE #tempINSTALLDETAILS        
             
     END              
        
         /*    
             
         1. IF (installemnt effective) > (Policy  exp date - 30)        
     itrack # 659    
             
         2. itrack # 919.if installment expiry date is greater than policy expiry date then     
   subtract 1 month from installment effective date    
       
   3. July 05 2011. installment due date can be greater than policy expiry date.    
     itrack # 919 Note(10)    
         UPDATE        
         ACT_POLICY_INSTALLMENT_DETAILS SET         
         INSTALLMENT_EFFECTIVE_DATE =  DATEADD(MONTH,-1,INSTALLMENT_EFFECTIVE_DATE)     
         WHERE CUSTOMER_ID =@CUSTOMER_ID        
         AND POLICY_ID  = @POLICY_ID AND         
         POLICY_VERSION_ID = @POLICY_VERSION_ID        
         AND INSTALLMENT_EFFECTIVE_DATE > DATEADD(day,-30,@POLICY_EXPIRY_DATE)        
         */      
                 
                       
    EXEC Proc_InsertInstallmentExpireDate @CUSTOMER_ID, @POLICY_ID , @POLICY_VERSION_ID             
    EXEC Proc_AddDefaultClause  @CUSTOMER_ID, @POLICY_ID , @POLICY_VERSION_ID  ,@CREATED_BY          
    IF @@ERROR <> 0                            
       GOTO ERRHANDLER                
                   
    RETURN 1      --For Successfull Execute Procedure                 
     ---Start GoTo Definitions                
     ERRHANDLER:                    
    BEGIN                 
                      
         RAISERROR('An error occurred while generating billing installments',10,1)                
              
      RETURN -3                
    END                          
     PLANIDMESSAGE:                     
    BEGIN                 
               --------------- IF PLAN IS NOT FOUND Apllication is under agency bill                                   
      RETURN -2                    
    END                  
     errorMsgINSTALLMENTNO:                     
      BEGIN               --------------- in case of endorsment if number of released installment is greater then selected  --plan id Payment no then                 
                
      RETURN -4                    
    END                  
END        
      
--GO      
-- DECLARE @CUSTOMER_ID   INT = 28607,                     
-- @POLICY_ID  INT = 103,                       
-- @POLICY_VERSION_ID SMALLINT = 2,           
-- @TOTAL_PREMIUM DECIMAL(25,2) = 2500,          
-- @TOTAL_INTEREST_AMOUNT DECIMAL(25,2) = 0,      
-- @TOTAL_FEES DECIMAL(25,2) = 0,                 
-- @TOTAL_TAXES DECIMAL(25,2)  = 0     ,      
-- @TOTAL_AMOUNT DECIMAL(25,2) = 2500,        
-- @PLAN_ID INT = 7,       
-- @RETVAL  INT = NULL ,      
-- @CREATED_BY INT = 398,      
-- @CREATED_DATETIME DATETIME = getdate(),       
-- @PRM_DIST_TYPE INT = null ,                 
-- @TOTAL_TRAN_PREMIUM DECIMAL(25,2) = null,              
-- @TOTAL_TRAN_INTEREST_AMOUNT DECIMAL(25,2) = null,              
-- @TOTAL_TRAN_FEES DECIMAL(25,2)= null,              
-- @TOTAL_TRAN_TAXES decimal (25,2) = null,              
-- @TOTAL_TRAN_AMOUNT decimal (25,2) = null,              
-- @TOTAL_CHANGE_INFORCE_PRM DECIMAL(25,2) = null,               
-- @TOTAL_INFO_PRM  DECIMAL(25,2) = null,              
-- @TOTAL_STATE_FEES DECIMAL(25,2)=null,              
-- @TOTAL_TRAN_STATE_FEES DECIMAL(25,2)=null ,
 
-- @TOTAL_AFTER_GST DECIMAL(25,2)=2700,              
-- @TOTAL_BEFORE_GST DECIMAL(25,2)=2600,  
-- @GROSS_COMMISSION DECIMAL(25,2)=120,  
-- @GST_ON_COMMISSION DECIMAL(25,2)=70,  
-- @TOTAL_COMM_AFTER_GST DECIMAL(25,2)=70  
               

--begin tran      
--EXEC Proc_InsertPolicyPremiumItems   @CUSTOMER_ID ,      
-- @POLICY_ID,      
-- @POLICY_VERSION_ID,      
-- @TOTAL_PREMIUM,      
-- @TOTAL_INTEREST_AMOUNT,      
-- @TOTAL_FEES,      
-- @TOTAL_TAXES,      
-- @TOTAL_AMOUNT,      
-- @PLAN_ID,      
-- @RETVAL,      
-- @CREATED_BY,      
-- @CREATED_DATETIME,      
-- @PRM_DIST_TYPE,      
-- @TOTAL_TRAN_PREMIUM,      
-- @TOTAL_TRAN_INTEREST_AMOUNT,      
-- @TOTAL_TRAN_FEES,      
-- @TOTAL_TRAN_TAXES,      
-- @TOTAL_TRAN_AMOUNT,      
-- @TOTAL_CHANGE_INFORCE_PRM,      
-- @TOTAL_INFO_PRM,      
-- @TOTAL_STATE_FEES,      
-- @TOTAL_TRAN_STATE_FEES,
-- @TOTAL_AFTER_GST,
-- @TOTAL_BEFORE_GST,
-- @GROSS_COMMISSION,
-- @GST_ON_COMMISSION,
-- @TOTAL_COMM_AFTER_GST      
-- SELECT INSTALLMENT_AMOUNT,* FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID      
--ROLLBACK TRAN 