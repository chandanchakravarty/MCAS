IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateSystemParams]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateSystemParams]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------          
Proc Name       : dbo.Proc_UpdateSystemParams          
Created by      : Gaurav          
Date            : 3/16/2005          
Purpose         : To update record in MNT_SYSTEM_PARAMS          
Revison History :          
Used In         :  Ebix Advantage web  
Modified by  :Pradeep Kushwaha  
Date   :-30-06-2010  
Purpose   : Add new Fields @NOTIFY_RECVE_INSURED int   
Modified by     :Abhinav Agarwal   
Date   :-09-09-2010  
Purpose   : Add new Fields @BASE_CURRENCY int   
------------------------------------------------------------          
Date     Review By          Comments          
DROP PROC Dbo.Proc_UpdateSystemParams       
------   ------------       -------------------------*/          
          
CREATE  PROC [dbo].[Proc_UpdateSystemParams]          
(          
@Bad_Login_Attempt   smallint  ,          
@Renewal_For_Alert   smallint  ,          
@Pending_Quote_For_Alert  smallint  ,          
@Quoted_Quote_For_Alert   smallint  ,          
@Number_Days_Expire   smallint  ,          
@Number_Days_Pending_NTU  smallint  ,          
@Number_Days_Expire_Quote  smallint  ,          
@Default_Policy_Term   smallint  ,          
@Graph_Logo_Allow   nchar(3)  ,          
@Installment_Fees   decimal(18,2)   ,          
@Non_Sufficient_Fund_Fees   decimal(18,2)  ,          
@Reinstatement_Fees   decimal(18,2)  ,          
@Employee_Discount   decimal(18,2)  ,          
@Print_Following   varchar(50) ,          
@Claim_Number    numeric,          
@SYS_INSURANCE_SCORE_VALIDITY  VARCHAR(4)  ,        
@SYS_Min_Install_Plan decimal(18,2),        
@SYS_AmtUnder_Payment decimal(18,2),        
@SYS_MinDays_Premium smallint,        
@SYS_MinDays_Cancel smallint,        
@SYS_Post_Phone smallint,        
@SYS_Post_Cancel smallint,      
@SYS_INDICATE_POL_AS VARCHAR(10),    
@POSTAGE_FEE int,    
@RESTR_DELIV_FEE int,    
@CERTIFIED_FEE int,    
@RET_RECEIPT_FEE int ,       
@NOTIFY_RECVE_INSURED int,  
@BASE_CURRENCY int,  
@DAYS_FOR_BOLETO_EXPIRATION smallint  
)          
AS          
          
BEGIN          
          
  UPDATE MNT_SYSTEM_PARAMS SET          
            
   SYS_BAD_LOGON_ATTMPT   = @Bad_Login_Attempt  ,          
   SYS_RENEWAL_FOR_ALERT   = @Renewal_For_Alert  ,          
   SYS_PENDING_QUOTE_FOR_ALERT  = @Pending_Quote_For_Alert ,          
   SYS_QUOTED_QUOTE_FOR_ALERT  = @Quoted_Quote_For_Alert ,          
   SYS_NUM_DAYS_EXPIRE   = @Number_Days_Expire  ,          
   SYS_NUM_DAYS_PEN_TO_NTU   = @Number_Days_Pending_NTU ,          
   SYS_NUM_DAYS_EXPIRE_QUOTE  = @Number_Days_Expire_Quote ,          
   SYS_DEFAULT_POL_TERM   = @Default_Policy_Term  ,          
          
   SYS_GRAPH_LOGO_ALLOW   = @Graph_Logo_Allow  ,          
   SYS_INSTALLMENT_FEES   = @Installment_Fees  ,          
   SYS_NON_SUFFICIENT_FUND_FEES  = @Non_Sufficient_Fund_Fees ,          
   SYS_REINSTATEMENT_FEES   = @Reinstatement_Fees  ,          
   SYS_EMPLOYEE_DISCOUNT   = @Employee_Discount  ,          
   SYS_PRINT_FOLLOWING   = @Print_Following  ,          
   SYS_CLAIM_NO    = @Claim_Number   ,          
   SYS_INSURANCE_SCORE_VALIDITY  = @SYS_INSURANCE_SCORE_VALIDITY ,        
   SYS_Min_Install_Plan   =@SYS_Min_Install_Plan,        
   SYS_AmtUnder_Payment  =@SYS_AmtUnder_Payment,        
   SYS_MinDays_Premium =  @SYS_MinDays_Premium,        
   SYS_MinDays_Cancel  =  @SYS_MinDays_Cancel,        
   SYS_Post_Phone      =  @SYS_Post_Phone,        
   SYS_Post_Cancel     =  @SYS_Post_Cancel ,      
   SYS_INDICATE_POL_AS = @SYS_INDICATE_POL_AS,    
   POSTAGE_FEE = @POSTAGE_FEE,    
   RESTR_DELIV_FEE = @RESTR_DELIV_FEE,    
   CERTIFIED_FEE = @CERTIFIED_FEE,    
   RET_RECEIPT_FEE = @RET_RECEIPT_FEE ,  
   NOTIFY_RECVE_INSURED=@NOTIFY_RECVE_INSURED,  
   BASE_CURRENCY=@BASE_CURRENCY,  
   DAYS_FOR_BOLETO_EXPIRATION= @DAYS_FOR_BOLETO_EXPIRATION       
END          
        
      
    
GO

