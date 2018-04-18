IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_INSTALL_PLAN_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_INSTALL_PLAN_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : dbo.UpdateACT_INSTALL_PLAN_DETAIL      
Created by      : Vijay Joshi      
Date            : 6/7/2005      
Purpose     : Update record into ACT_INSTALL_PLAN_DETAIL      
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
Modified  by      : Pravesh Chandel      
Date            : 30/11/2006      
Purpose     : Add two morw parameters and for fields @APPLABLE_POLTERM int,       
       and  @PLAN_PAYMENT_MODE int      
      
drop proc dbo.Proc_UpdateACT_INSTALL_PLAN_DETAIL  
 Modified by:sonal
 Date:-08-06-2010
Updating few extra which are added in billing plan  

Modified  by      : Pradeep Kushwaha     
Date              : 30/06/2010      
Purpose			  : Add new Fields @RECVE_NOTIFICATION_DOC int   
    
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_UpdateACT_INSTALL_PLAN_DETAIL      
CREATE PROC [dbo].[Proc_UpdateACT_INSTALL_PLAN_DETAIL]      
(      
 @IDEN_PLAN_ID   int,      
 @PLAN_CODE      nvarchar(20),      
 @PLAN_DESCRIPTION     nvarchar(70),      
 --@PLAN_TYPE      nchar(2),      --commented by lalit dec 30,2010
 @NO_OF_PAYMENTS smallint,      
 @MONTHS_BETWEEN smallint,      
 @PERCENT_BREAKDOWN1 decimal(7,4),      
 @PERCENT_BREAKDOWN2     decimal(7,4),      
 @PERCENT_BREAKDOWN3     decimal(7,4),      
 @PERCENT_BREAKDOWN4     decimal(7,4),      
 @PERCENT_BREAKDOWN5     decimal(7,4),      
 @PERCENT_BREAKDOWN6     decimal(7,4),      
 @PERCENT_BREAKDOWN7     decimal(7,4),      
 @PERCENT_BREAKDOWN8     decimal(7,4),      
 @PERCENT_BREAKDOWN9     decimal(7,4),      
 @PERCENT_BREAKDOWN10    decimal(7,4),      
 @PERCENT_BREAKDOWN11    decimal(7,4),      
 @PERCENT_BREAKDOWN12    decimal(7,4),      
 @MODIFIED_BY       int,      
 @LAST_UPDATED_DATETIME  datetime,      
 @INSTALLMENT_FEES  decimal(18,2),      
 @NON_SUFFICIENT_FUND_FEES decimal(18,2),      
 @REINSTATEMENT_FEES  decimal(18,2),      
 @MIN_INSTALLMENT_AMOUNT decimal(18,2),      
 @AMTUNDER_PAYMENT  decimal(18,2),      
 @MINDAYS_PREMIUM  smallint,      
 @MINDAYS_CANCEL   smallint,      
 @POST_PHONE    smallint,      
 @POST_CANCEL   smallint,      
 @DEFAULT_PLAN   BIT,      
  @PERCENT_BREAKDOWNRP1 decimal(7,4),      
 @PERCENT_BREAKDOWNRP2     decimal(7,4),      
 @PERCENT_BREAKDOWNRP3     decimal(7,4),      
 @PERCENT_BREAKDOWNRP4     decimal(7,4),      
 @PERCENT_BREAKDOWNRP5     decimal(7,4),      
 @PERCENT_BREAKDOWNRP6     decimal(7,4),      
 @PERCENT_BREAKDOWNRP7     decimal(7,4),      
 @PERCENT_BREAKDOWNRP8     decimal(7,4),      
 @PERCENT_BREAKDOWNRP9     decimal(7,4),      
 @PERCENT_BREAKDOWNRP10    decimal(7,4),      
 @PERCENT_BREAKDOWNRP11    decimal(7,4),      
 @PERCENT_BREAKDOWNRP12    decimal(7,4),      
 @LATE_FEES    decimal(9,2),      
 @SERVICE_CHARGE    decimal(9,2) =null,  --as this fields are removed from billing plan page      
 @CONVENIENCE_FEES   decimal(9,2) =null, --as this fields are removed from billing plan page      
 @GRACE_PERIOD    smallint,      
 --added by pravesh on 30 nov 2006      
 @APPLABLE_POLTERM int,       
 @PLAN_PAYMENT_MODE int,      
 @NO_INS_DOWNPAY   int,      
 @MODE_OF_DOWNPAY  int,      
 @NO_INS_DOWNPAY_RENEW  int,      
 @MODE_OF_DOWNPAY_RENEW  int,       
 @MODE_OF_DOWNPAY1  int,      
 @MODE_OF_DOWNPAY2  int,      
 @MODE_OF_DOWNPAY_RENEW1  int,      
 @MODE_OF_DOWNPAY_RENEW2  int,      
 @PAST_DUE_RENEW   numeric(18,0),    
 @DAYS_DUE_PREM_NOTICE_PRINTD int,
@INTREST_RATES decimal(7,4),
 @DAYS_SUBSEQUENT_INSTALLMENTS smallint,
 @SUBSEQUENT_INSTALLMENTS_OPTION nvarchar(10),
 @BASE_DATE_DOWNPAYMENT int,
 @DUE_DAYS_DOWNPYMT int,
 @BDATE_INSTALL_NXT_DOWNPYMT int,
 @DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT int,
 @DOWN_PAYMENT_PLAN int,
 @DOWN_PAYMENT_PLAN_RENEWAL int,
 @APPLICABLE_LOB int,
 @RECVE_NOTIFICATION_DOC int         
       
      
)      
AS      
BEGIN      

 /*Checking whether duplicate code exists or not*/      
 If Not Exists(SELECT PLAN_CODE       
   FROM ACT_INSTALL_PLAN_DETAIL       
   WHERE PLAN_CODE = @PLAN_CODE AND IDEN_PLAN_ID <> @IDEN_PLAN_ID)      
 BEGIN      
  /*Duplicate code does not exists*/      
      
  --If this is the default plan updating flag of all other plans      
  IF @DEFAULT_PLAN = 1      
  UPDATE ACT_INSTALL_PLAN_DETAIL      
  SET DEFAULT_PLAN = 0  
  WHERE APPLABLE_POLTERM IN (SELECT  APPLABLE_POLTERM FROM ACT_INSTALL_PLAN_DETAIL WHERE APPLABLE_POLTERM  = @APPLABLE_POLTERM)  
  
  UPDATE ACT_INSTALL_PLAN_DETAIL      
  SET PLAN_CODE = @PLAN_CODE,       
   PLAN_DESCRIPTION = @PLAN_DESCRIPTION,    
   --PLAN_TYPE = @PLAN_TYPE,   
   NO_OF_PAYMENTS = @NO_OF_PAYMENTS,       
   MONTHS_BETWEEN =@MONTHS_BETWEEN,       
   PERCENT_BREAKDOWN1 = @PERCENT_BREAKDOWN1,      
   PERCENT_BREAKDOWN2 = @PERCENT_BREAKDOWN2,       
   PERCENT_BREAKDOWN3 = @PERCENT_BREAKDOWN3,       
   PERCENT_BREAKDOWN4 = @PERCENT_BREAKDOWN4,      
   PERCENT_BREAKDOWN5 = @PERCENT_BREAKDOWN5,       
   PERCENT_BREAKDOWN6 = @PERCENT_BREAKDOWN6,       
   PERCENT_BREAKDOWN7 = @PERCENT_BREAKDOWN7,      
   PERCENT_BREAKDOWN8 = @PERCENT_BREAKDOWN8,       
   PERCENT_BREAKDOWN9 = @PERCENT_BREAKDOWN9,       
   PERCENT_BREAKDOWN10 = @PERCENT_BREAKDOWN10,      
   PERCENT_BREAKDOWN11 = @PERCENT_BREAKDOWN11,       
   PERCENT_BREAKDOWN12 = @PERCENT_BREAKDOWN12,      
   MODIFIED_BY   = @MODIFIED_BY,       
   LAST_UPDATED_DATETIME  = @LAST_UPDATED_DATETIME,      
   INSTALLMENT_FEES   = @INSTALLMENT_FEES,      
   NON_SUFFICIENT_FUND_FEES= @NON_SUFFICIENT_FUND_FEES,      
   REINSTATEMENT_FEES   = @REINSTATEMENT_FEES,      
   MIN_INSTALLMENT_AMOUNT  = @MIN_INSTALLMENT_AMOUNT,      
   AMTUNDER_PAYMENT   = @AMTUNDER_PAYMENT,      
   MINDAYS_PREMIUM   = @MINDAYS_PREMIUM,      
   MINDAYS_CANCEL    = @MINDAYS_CANCEL,      
   POST_PHONE     = @POST_PHONE,      
   POST_CANCEL    = @POST_CANCEL,      
   DEFAULT_PLAN   = @DEFAULT_PLAN,        
   PERCENT_BREAKDOWNRP1 = @PERCENT_BREAKDOWNRP1 ,      
   PERCENT_BREAKDOWNRP2 = @PERCENT_BREAKDOWNRP2,      
   PERCENT_BREAKDOWNRP3 = @PERCENT_BREAKDOWNRP3 ,       
   PERCENT_BREAKDOWNRP4 = @PERCENT_BREAKDOWNRP4 ,      
   PERCENT_BREAKDOWNRP5 = @PERCENT_BREAKDOWNRP5 ,      
   PERCENT_BREAKDOWNRP6 = @PERCENT_BREAKDOWNRP6 ,       
   PERCENT_BREAKDOWNRP7 = @PERCENT_BREAKDOWNRP7  ,      
   PERCENT_BREAKDOWNRP8 = @PERCENT_BREAKDOWNRP8,      
   PERCENT_BREAKDOWNRP9 = @PERCENT_BREAKDOWNRP9  ,      
   PERCENT_BREAKDOWNRP10= @PERCENT_BREAKDOWNRP10 ,      
   PERCENT_BREAKDOWNRP11 = @PERCENT_BREAKDOWNRP11   ,      
   PERCENT_BREAKDOWNRP12 = @PERCENT_BREAKDOWNRP12 ,      
   LATE_FEES = @LATE_FEES,          
   SERVICE_CHARGE = @SERVICE_CHARGE,          
   CONVENIENCE_FEES = @CONVENIENCE_FEES,      
   GRACE_PERIOD  = @GRACE_PERIOD ,      
   --added by pravesh on 30 nov 2006         
   APPLABLE_POLTERM = @APPLABLE_POLTERM,       
   PLAN_PAYMENT_MODE = @PLAN_PAYMENT_MODE,      
   NO_INS_DOWNPAY  = @NO_INS_DOWNPAY,      
   MODE_OF_DOWNPAY  = @MODE_OF_DOWNPAY,      
   NO_INS_DOWNPAY_RENEW = @NO_INS_DOWNPAY_RENEW,      
   MODE_OF_DOWNPAY_RENEW = @MODE_OF_DOWNPAY_RENEW,      
   MODE_OF_DOWNPAY1 = @MODE_OF_DOWNPAY1,      
   MODE_OF_DOWNPAY2 = @MODE_OF_DOWNPAY2,      
   MODE_OF_DOWNPAY_RENEW1 = @MODE_OF_DOWNPAY_RENEW1,      
   MODE_OF_DOWNPAY_RENEW2 = @MODE_OF_DOWNPAY_RENEW2,      
   PAST_DUE_RENEW  = @PAST_DUE_RENEW,    
   DAYS_DUE_PREM_NOTICE_PRINTD = @DAYS_DUE_PREM_NOTICE_PRINTD, 
   DAYS_SUBSEQUENT_INSTALLMENTS=@DAYS_SUBSEQUENT_INSTALLMENTS,
  SUBSEQUENT_INSTALLMENTS_OPTION=@SUBSEQUENT_INSTALLMENTS_OPTION,
   INTREST_RATES=@INTREST_RATES,
   BASE_DATE_DOWNPAYMENT=@BASE_DATE_DOWNPAYMENT,
   DUE_DAYS_DOWNPYMT=@DUE_DAYS_DOWNPYMT,
   BDATE_INSTALL_NXT_DOWNPYMT=@BDATE_INSTALL_NXT_DOWNPYMT,
   DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT=@DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT,
   DOWN_PAYMENT_PLAN=@DOWN_PAYMENT_PLAN,
   DOWN_PAYMENT_PLAN_RENEWAL=@DOWN_PAYMENT_PLAN_RENEWAL,
   APP_LOB=@APPLICABLE_LOB  ,   
   RECVE_NOTIFICATION_DOC=@RECVE_NOTIFICATION_DOC  
  WHERE      
   IDEN_PLAN_ID = @IDEN_PLAN_ID      
      
 END      
END      
      
      
      
      
      
      
      
      
    
  
  
  
  
  
  
GO

