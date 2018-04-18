IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_GetInstallmentEffectiveDate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_GetInstallmentEffectiveDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
    
/*      
select dbo.fun_GetInstallmentEffectiveDate(getdate(),1,1)    
Create By : Lalit Kumar Chauhan     
dated  : 15 July 2010    
purpose  : calculate installment effective date base on installment no    
    
drop function dbo.func_GetInstallmentEffectiveDate    
select [dbo].[func_GetInstallmentEffectiveDate](84,1,2156,756,1,NULL,'NBS',NULL)    
*/    
CREATE function [dbo].[func_GetInstallmentEffectiveDate]    
(    
    
@PLAN_ID INT,    
@INSTALLMENT_NO INT,    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT,    
@BASE_DATE DATETIME = null,    
@TRAN_TYPE NVARCHAR(10) = null,    
@DIST_TYPE INT = NULL   
)RETURNS DATETIME      
AS       
Begin      
    
  DECLARE  @BASE_DATE_DOWN_PAYMENT INT,    
    @DOWN_PAYMENT_PLAN   SMALLINT,    
    @BDATE_INSTALL_NXT_DOWNPYMT INT,    
    @DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT SMALLINT,    
    @DUE_DAYS_DOWNPYMT SMALLINT,    
    @DAYS_SUBSEQUENT_INSTALLMENTS SMALLINT,    
    @SUBSEQUENT_INSTALLMENTS_OPTION NVARCHAR(50),    
    @CALCULATED_INS_EFF_DATE DATETIME,    
    @APPLICATION_SUBMISSION_DATE DATE,    
 @POLICY_ISSUANCE_DATE DATE,    
 @POLICY_EFFECTIVE_DATE DATE,    
 @SUBSEQUENT_INSTALLMENTS_OPTION_TYPE NVARCHAR(10),    
 @FLAG INT,@PROCESS_ID INT,@CURRENT_TERM INT,@APP_NUMBER NVARCHAR(15)  
        
        
        
        
SELECT @BASE_DATE_DOWN_PAYMENT = BASE_DATE_DOWNPAYMENT,    
    @DOWN_PAYMENT_PLAN   = DOWN_PAYMENT_PLAN ,    
    @BDATE_INSTALL_NXT_DOWNPYMT = BDATE_INSTALL_NXT_DOWNPYMT,    
    @DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT = DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT,    
    @DUE_DAYS_DOWNPYMT = DUE_DAYS_DOWNPYMT,    
    @DAYS_SUBSEQUENT_INSTALLMENTS = DAYS_SUBSEQUENT_INSTALLMENTS,    
    @SUBSEQUENT_INSTALLMENTS_OPTION = SUBSEQUENT_INSTALLMENTS_OPTION     
    FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK) WHERE IDEN_PLAN_ID=@PLAN_ID    
    
IF(@TRAN_TYPE IN ('NBS','REN','REW') OR @TRAN_TYPE IS NULL)    
  BEGIN    
      IF(@BASE_DATE IS NULL OR @BASE_DATE = NULL)    
    BEGIN    
     SELECT     
      @APPLICATION_SUBMISSION_DATE = CONVERT(DATETIME,CONVERT(VARCHAR(10),CREATED_DATETIME,111)),    
      @POLICY_EFFECTIVE_DATE = CONVERT(DATETIME,CONVERT(VARCHAR(10),POLICY_EFFECTIVE_DATE,111))  ,  
      @CURRENT_TERM = CURRENT_TERM        
      FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID   
      AND POLICY_ID=@POLICY_ID AND   
      POLICY_VERSION_ID=@POLICY_VERSION_ID    
          
        IF(@TRAN_TYPE='NBS')  
  SET @PROCESS_ID= 25  
      ELSE IF (@TRAN_TYPE in ('REN'))---for renewal policy insttallment should starts from renewal effective date  
  SET @PROCESS_ID=5  
  ELSE IF (@TRAN_TYPE in ('REW'))---for renewal policy insttallment should starts from renewal effective date  
  SET @PROCESS_ID=31     
     
        
      IF EXISTS(SELECT COMPLETED_DATETIME FROM POL_POLICY_PROCESS  WITH(NOLOCK)    
           WHERE CUSTOMER_ID = @CUSTOMER_ID AND  NEW_POLICY_ID = @POLICY_ID AND PROCESS_ID=@PROCESS_ID AND PROCESS_STATUS='COMPLETE')    
        BEGIN    
        SELECT @POLICY_ISSUANCE_DATE = COMPLETED_DATETIME  FROM POL_POLICY_PROCESS  WITH(NOLOCK)    
            WHERE CUSTOMER_ID = @CUSTOMER_ID AND  NEW_POLICY_ID = @POLICY_ID AND   
    PROCESS_ID=@PROCESS_ID   
            AND PROCESS_STATUS='COMPLETE'    
          SELECT  @FLAG =1 ;       
        END    
        ELSE  IF(@PROCESS_ID IN (5,31))--FOR RENEWAL ,REwrite  
     SELECT @POLICY_ISSUANCE_DATE = APP_EFFECTIVE_DATE  FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)    
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID = @POLICY_ID AND   
     POLICY_VERSION_ID = @POLICY_VERSION_ID  
  ELSE  
           BEGIN    
    SELECT @POLICY_ISSUANCE_DATE =  CONVERT(DATETIME,CONVERT(VARCHAR(100),GETDATE(),111))    
         
           END       
    
      IF(@BDATE_INSTALL_NXT_DOWNPYMT = 14448)     
        SELECT @BASE_DATE = @APPLICATION_SUBMISSION_DATE    
      ELSE IF (@BDATE_INSTALL_NXT_DOWNPYMT = 14449)               
       SELECT @BASE_DATE = @POLICY_ISSUANCE_DATE    
      ELSE IF (@BDATE_INSTALL_NXT_DOWNPYMT = 14450)     
       SELECT @BASE_DATE= @POLICY_EFFECTIVE_DATE    
    END    
        
  IF(@INSTALLMENT_NO = 1)     
   BEGIN    
    
    IF @DOWN_PAYMENT_PLAN = 0                    ---If Down Payment is 0 then 1st installment date will be Issuance date + 30    
     BEGIN      
      IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14662')     --IF INSTALLEMNT GAP IS IN DAYS THEN IT CALCULATE NEXT INSTALLMENT FRON DAYS GAP               
        SELECT @CALCULATED_INS_EFF_DATE = DATEADD(DAY,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );      
       ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14664') --IF OPTION SELECTED MONTHS               
        SELECT @CALCULATED_INS_EFF_DATE = DATEADD(MONTH,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );      
       ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14663') --IF OPTION SELECTED WEEK               
        SELECT @CALCULATED_INS_EFF_DATE = DATEADD(WEEK,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );     
       END     
    ELSE    
     BEGIN          
         IF(@BASE_DATE_DOWN_PAYMENT = 14448)     
        SELECT @BASE_DATE = @APPLICATION_SUBMISSION_DATE    
         ELSE IF (@BASE_DATE_DOWN_PAYMENT = 14449)               
        SELECT @BASE_DATE = @POLICY_ISSUANCE_DATE    
         ELSE IF (@BASE_DATE_DOWN_PAYMENT = 14450)     
        SELECT @BASE_DATE= @POLICY_EFFECTIVE_DATE        
       SELECT @CALCULATED_INS_EFF_DATE = DATEADD(DAY, @DUE_DAYS_DOWNPYMT, @BASE_DATE );      
                          
     END    
   END    
  ELSE IF(@INSTALLMENT_NO = 2)           
   BEGIN     
        
           IF(@DOWN_PAYMENT_PLAN = 1 )    
        BEGIN    
         IF(@BASE_DATE_DOWN_PAYMENT = 14448)     
         SELECT @BASE_DATE = @APPLICATION_SUBMISSION_DATE    
          ELSE IF (@BASE_DATE_DOWN_PAYMENT = 14449)               
         SELECT @BASE_DATE = @POLICY_ISSUANCE_DATE    
          ELSE IF (@BASE_DATE_DOWN_PAYMENT = 14450)     
         SELECT @BASE_DATE= @POLICY_EFFECTIVE_DATE      
                 
          SELECT @CALCULATED_INS_EFF_DATE = DATEADD(DAY, @DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT, @BASE_DATE);        
           END       
           ELSE     
         BEGIN     
    
          SELECT @BASE_DATE = INSTALLMENT_EFFECTIVE_DATE FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND INSTALLMENT_NO = 1              
    
       IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14662')     --IF INSTALLEMNT GAP IS IN DAYS THEN IT CALCULATE NEXT INSTALLMENT FRON DAYS GAP             
        SELECT @CALCULATED_INS_EFF_DATE = DATEADD(DAY,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
       ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14664') --IF OPTION SELECTED MONTHS             
        SELECT @CALCULATED_INS_EFF_DATE = DATEADD(MONTH,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
       ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14663') --IF OPTION SELECTED WEEK             
        SELECT @CALCULATED_INS_EFF_DATE = DATEADD(WEEK,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
            
       SELECT @CALCULATED_INS_EFF_DATE= CONVERT(DATETIME,CONVERT(VARCHAR(100),@CALCULATED_INS_EFF_DATE,111))    
           
         END    
   END    
  ELSE    
    
    BEGIN    
        
     SELECT @BASE_DATE = INSTALLMENT_EFFECTIVE_DATE FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND INSTALLMENT_NO = @INSTALLMENT_NO - 1    
    
    END    
    
   IF(@INSTALLMENT_NO <> 1 AND @INSTALLMENT_NO <> 2)    
   BEGIN    
    IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14662')     --IF INSTALLEMNT GAP IS IN DAYS THEN IT CALCULATE NEXT INSTALLMENT FRON DAYS GAP             
     SELECT @CALCULATED_INS_EFF_DATE = DATEADD(DAY,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
    ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14664') --IF OPTION SELECTED MONTHS             
     SELECT @CALCULATED_INS_EFF_DATE = DATEADD(MONTH,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
    ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14663') --IF OPTION SELECTED WEEK             
     SELECT @CALCULATED_INS_EFF_DATE = DATEADD(WEEK,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
    
     SELECT @CALCULATED_INS_EFF_DATE= CONVERT(DATETIME,CONVERT(VARCHAR(100),@CALCULATED_INS_EFF_DATE,111))    
   END    
      
 END    
 ELSE IF(@TRAN_TYPE IN ('END','CAN') AND ISNULL(@DIST_TYPE,0) <> 14855)    
  BEGIN    
        
        
   IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14662')     --IF INSTALLEMNT GAP IS IN DAYS THEN IT CALCULATE NEXT INSTALLMENT FRON DAYS GAP             
     SELECT @CALCULATED_INS_EFF_DATE = DATEADD(DAY,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
   ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14664') --IF OPTION SELECTED MONTHS             
    SELECT @CALCULATED_INS_EFF_DATE = DATEADD(MONTH,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
   ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14663') --IF OPTION SELECTED WEEK             
    SELECT @CALCULATED_INS_EFF_DATE = DATEADD(WEEK,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
        
    SELECT @CALCULATED_INS_EFF_DATE= CONVERT(DATETIME,CONVERT(VARCHAR(100),@CALCULATED_INS_EFF_DATE,111))    
         
  END    
 ELSE  IF(@TRAN_TYPE IN('END','CAN') AND  @DIST_TYPE = 14855) --endosement installment date according to billing plan  
 BEGIN  
    
     --itrack # 919 note #(13)  
  --if Base Date For installments next to Down Payment = "policy issuance date"  
  ---get policy NBS completed date as base date.  
  IF (@BDATE_INSTALL_NXT_DOWNPYMT = 14449)   
  SELECT @BASE_DATE = COMPLETED_DATETIME FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID   
  AND POLICY_ID = @POLICY_ID   AND PROCESS_ID = 25 and PROCESS_STATUS <> 'ROLLBACK'  
    
  IF (@BDATE_INSTALL_NXT_DOWNPYMT = 14448)   
  BEGIN   
  SELECT @APP_NUMBER = APP_NUMBER FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID =   
  @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
    
  SELECT top  1 @BASE_DATE = CREATED_DATETIME  FROM POL_CUSTOMER_POLICY_LIST   
  WHERE APP_NUMBER = @APP_NUMBER ORDER BY POLICY_VERSION_ID ASC  
    
    
  END  
  
 --IF(@INSTALLMENT_NO <> 1)  
 SELECT @DAYS_SUBSEQUENT_INSTALLMENTS = @DAYS_SUBSEQUENT_INSTALLMENTS * @INSTALLMENT_NO  
   
 IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14662')     --IF INSTALLEMNT GAP IS IN DAYS THEN IT CALCULATE NEXT INSTALLMENT FRON DAYS GAP              
     SELECT @CALCULATED_INS_EFF_DATE = DATEADD(DAY,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );        
   ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14664') --IF OPTION SELECTED MONTHS             
    SELECT @CALCULATED_INS_EFF_DATE = DATEADD(MONTH,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
   ELSE IF(@SUBSEQUENT_INSTALLMENTS_OPTION ='14663') --IF OPTION SELECTED WEEK             
    SELECT @CALCULATED_INS_EFF_DATE = DATEADD(WEEK,@DAYS_SUBSEQUENT_INSTALLMENTS, @BASE_DATE );    
        
  
  SELECT @CALCULATED_INS_EFF_DATE= CONVERT(DATETIME,CONVERT(VARCHAR(100),@CALCULATED_INS_EFF_DATE,111))    
  
   
 END  
    
Return @CALCULATED_INS_EFF_DATE    
    
End      
  
GO

