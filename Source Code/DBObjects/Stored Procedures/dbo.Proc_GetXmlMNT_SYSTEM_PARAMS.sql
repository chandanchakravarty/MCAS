IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXmlMNT_SYSTEM_PARAMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXmlMNT_SYSTEM_PARAMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetXmlMNT_SYSTEM_PARAMS      
Created by      : Mohit Agarwal/Manoj Rathore      
Date            : 10-Jul-2007      
Purpose         : To fetch record in MNT_SYSTEM_PARAMS      
Revison History :      
Used In         :	Ebix Advantage web
Modified by		:	Pradeep Kushwaha
Date			:	30-06-2010
Purpose			:	Add new Fields @NOTIFY_RECVE_INSURED int
Modified by		:	Abhinav Agarwal
Date			:	9-09-2010
Purpose			:	Add new Fields @BASE_CURRENCY int    
------------------------------------------------------------      
Date     Review By          Comments      
--Drop Proc dbo.Proc_GetXmlMNT_SYSTEM_PARAMS   
------   ------------       -------------------------*/         
CREATE PROCEDURE [dbo].[Proc_GetXmlMNT_SYSTEM_PARAMS]                              
                              
AS                              
BEGIN                              
select SYS_BAD_LOGON_ATTMPT,  
SYS_RENEWAL_FOR_ALERT,  
SYS_PENDING_QUOTE_FOR_ALERT,  
SYS_QUOTED_QUOTE_FOR_ALERT,  
SYS_NUM_DAYS_EXPIRE,  
SYS_NUM_DAYS_PEN_TO_NTU,  
SYS_NUM_DAYS_EXPIRE_QUOTE,  
SYS_DEFAULT_POL_TERM,  
SYS_INDICATE_POL_AS,  
SYS_GRAPH_LOGO_ALLOW,  
SYS_INSTALLMENT_FEES,  
SYS_NON_SUFFICIENT_FUND_FEES,  
SYS_REINSTATEMENT_FEES,  
SYS_EMPLOYEE_DISCOUNT,  
SYS_PRINT_FOLLOWING,  
SYS_CLAIM_NO,  
SYS_INSURANCE_SCORE_VALIDITY,  
SYS_MIN_INSTALLMENT_AMOUNT,  
SYS_MIN_INSTALL_PLAN,  
SYS_AMTUNDER_PAYMENT,  
SYS_MINDAYS_PREMIUM,  
SYS_MINDAYS_CANCEL,  
SYS_POST_PHONE,  
SYS_POST_CANCEL,  
SYS_MIN_FLAT_AMOUNT_FEE,  
SYS_NON_RENEW_DAYS,  
SYS_INSURANCE_SCORE_FETCH_INTERVAL,  
AGENCY_COMP_COMM,  
SYS_COMPLETE_APP_COMM_APPLICABILITY,  
SYS_PROPERTY_INS_COMM_APPLICABILITY ,
NOTIFY_RECVE_INSURED,
BASE_CURRENCY,
DAYS_FOR_BOLETO_EXPIRATION

 from MNT_SYSTEM_PARAMS with(nolock)                              
END     
  
  
  
GO

