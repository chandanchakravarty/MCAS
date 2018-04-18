 /*----------------------------------------------------------                                                                
Proc Name                : Dbo.Proc_GetHORule_LocationInfo_Pol                                                              
Created by               : Ashwani                                                                
Date                     : 02 Mar. 2006              
Purpose                  : To get the location detail for HO policy rules                
Revison History          :                                                                
Used In                  : Wolverine                                                                
------------------------------------------------------------                                                                
Date     Review By          Comments                                                                
------   ------------       -------------------------*/           
--DROP PROC Proc_GetHORule_LocationInfo_Pol 1692,76,4,4429                                                              
alter proc dbo.Proc_GetFireRule_BillingInfo_Pol                
(                                                                
@CUSTOMER_ID    int,                                                                
@POLICY_ID    int,                                                                
@POLICY_VERSION_ID   int,                
@PLAN_ID int            
)                                                                
as                                                                    
begin                                 
           
                 
 if exists (select CUSTOMER_ID from POL_BILLING_DETAILS  with(nolock)                                 
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID)                
 begin                 
  select  *                
 from POL_BILLING_DETAILS    with(nolock)            
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID               
                
 end                

end 