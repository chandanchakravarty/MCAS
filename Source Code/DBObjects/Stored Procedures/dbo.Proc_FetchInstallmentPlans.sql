IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchInstallmentPlans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchInstallmentPlans]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 -- drop proc  Dbo.Proc_FetchInstallmentPlans       
CREATE PROC dbo.Proc_FetchInstallmentPlans      
      
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
IS_ACTIVE,      
CREATED_BY,      
CREATED_DATETIME,      
MODIFIED_BY,      
LAST_UPDATED_DATETIME,  
DEFAULT_PLAN      
--added by RP          
, isnull(PLAN_DESCRIPTION,' ') + ' (' + isnull(PLAN_CODE,' ')  + ')' as BILLING_PLAN    
--End of addition by RP    
from act_install_plan_detail with(nolock)       
where isnull(IS_ACTIVE,'N') = 'Y'      
order by plan_code      
      
END      
    
    
  
  
  
GO

