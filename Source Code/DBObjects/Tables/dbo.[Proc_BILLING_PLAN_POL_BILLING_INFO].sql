
/*---------------------------------------------------------------  
Proc Name          : dbo.[Proc_BILLING_PLAN_POL_BILLING_INFO]  
Created by      : SNEHA          
Date            : 23/11/2011                      
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/         
-- drop proc dbo.[Proc_BILLING_PLAN_POL_BILLING_INFO] 


CREATE PROC dbo.[Proc_BILLING_PLAN_POL_BILLING_INFO]   

AS
BEGIN
SELECT IDEN_PLAN_ID, PLAN_DESCRIPTION  FROM ACT_INSTALL_PLAN_DETAIL where IS_ACTIVE='y'
END



