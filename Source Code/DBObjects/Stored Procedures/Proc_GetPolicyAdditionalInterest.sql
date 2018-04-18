--Proc_GetPolicyAdditionalInterest 8,2,1

CREATE PROC Dbo.Proc_GetPolicyAdditionalInterest 
(      
 @CUSTOMER_ID int,      
 @POLICY_ID  int,      
 @POLICY_VERSION_ID int
)      
AS      
BEGIN      
      
 
  SELECT  
  ADD_INT_ID  ,  
  MEMO,      
  NATURE_OF_INTEREST,      
  RANK,      
  LOAN_REF_NUMBER,      
  IS_ACTIVE,      
  HOLDER_ID,      
  HOLDER_NAME,      
  HOLDER_ADD1,      
  HOLDER_ADD2,      
  HOLDER_CITY,      
  HOLDER_COUNTRY,      
  HOLDER_STATE,      
  HOLDER_ZIP      
        
  FROM  POL_GENERAL_HOLDER_INTEREST             
  WHERE       
  CUSTOMER_ID  = @CUSTOMER_ID and      
  POLICY_ID   = @POLICY_ID and      
  POLICY_VERSION_ID = @POLICY_VERSION_ID
 
      
END      