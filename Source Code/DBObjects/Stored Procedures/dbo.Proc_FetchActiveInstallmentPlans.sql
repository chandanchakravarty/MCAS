IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchActiveInstallmentPlans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchActiveInstallmentPlans]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_FetchActiveInstallmentPlans        
Created by      : Vijay Joshi      
Date            : 31-03-2006      
Purpose         : To select  record in act_install_plan_detail        
Revison History :        
Used In         :            
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop PROC Dbo.Proc_FetchActiveInstallmentPlans    
CREATE PROC dbo.Proc_FetchActiveInstallmentPlans        
@PLAN_ID INT = NULL      
AS        
       
BEGIN        
select         
        
IDEN_PLAN_ID as INSTALL_PLAN_ID,        
PLAN_CODE,        
PLAN_DESCRIPTION,        
PLAN_TYPE,        
NO_OF_PAYMENTS,        
MONTHS_BETWEEN,        
PERCENT_BREAKDOWN1,        
PERCENT_BREAKDOWN2,        
PERCENT_BREAKDOWN3,        
PERCENT_BREAKDOWN4,        
PERCENT_BREAKDOWN5,        
PERCENT_BREAKDOWN6,        
PERCENT_BREAKDOWN7,        
PERCENT_BREAKDOWN8,        
PERCENT_BREAKDOWN9,        
PERCENT_BREAKDOWN10,        
PERCENT_BREAKDOWN11,        
PERCENT_BREAKDOWN12,        
ACT.IS_ACTIVE IS_ACTIVE,        
CREATED_BY,        
CREATED_DATETIME,        
MODIFIED_BY,        
ACT.LAST_UPDATED_DATETIME  LAST_UPDATED_DATETIME 
--added by RP        
, isnull(PLAN_DESCRIPTION,' ') + ' (' + isnull(PLAN_CODE,' ') + ')' + ' - ' +  
isnull((select LOOKUP_VALUE_DESC from mnt_lookup_values where LOOKUP_UNIQUE_ID=PLAN_PAYMENT_MODE),'')
 as BILLING_PLAN  

--End of addition by RP  
--added by pravesh on 4 dec 2006
,isnull(APPLABLE_POLTERM,'-1') APPLABLE_POLTERM,
MODE_OF_DOWNPAY,
MODE_OF_DOWNPAY1,
MODE_OF_DOWNPAY2,
mnt.LOOKUP_VALUE_DESC LOOKUP_VALUE_DESC,
mnt1.LOOKUP_VALUE_DESC LOOKUP_VALUE_DESC1,
mnt2.LOOKUP_VALUE_DESC LOOKUP_VALUE_DESC2
--end here  
from act_install_plan_detail ACT
left join mnt_lookup_values mnt on mnt.LOOKUP_UNIQUE_ID=ACT.MODE_OF_DOWNPAY
left join mnt_lookup_values mnt1 on mnt1.LOOKUP_UNIQUE_ID=ACT.MODE_OF_DOWNPAY1         
left join mnt_lookup_values mnt2 on mnt2.LOOKUP_UNIQUE_ID=ACT.MODE_OF_DOWNPAY2


where IDEN_PLAN_ID = @PLAN_ID OR      
isnull(ACT.IS_ACTIVE,'N') = 'Y'      
order by plan_code        
        
END        
    
  
  
  
  
  







GO

