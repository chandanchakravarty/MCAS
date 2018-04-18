alter PROCEDURE dbo.Proc_GetBillingIDs_Pol        
(        
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID int        
)            
AS                 
BEGIN                  
   
 SELECT   isnull(PLAN_ID,0),isnull(GROSS_PREMIUM,''),ISNULL(NET_AMOUNT,'')         
 FROM       POL_BILLING_DETAILS         
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID       
    
 ORDER BY   PLAN_ID                    
End  