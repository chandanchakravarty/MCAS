  
--Drop proc Proc_DeleteReinsurance    
ALTER PROC [dbo].[Proc_DeleteReinsurance]    
(        
  @REINSURANCE_ID int,    
  @POLICY_ID int,    
  @POLICY_VERSION_ID int,    
  @CUSTOMER_ID int    
)         
 AS      
 BEGIN      
 Delete from POL_REINSURANCE_INFO where REINSURANCE_ID=@REINSURANCE_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and CUSTOMER_ID=@CUSTOMER_ID     
 END      
     
  