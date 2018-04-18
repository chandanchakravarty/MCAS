/*----------------------------------------------------------                      
Proc Name    : dbo.Proc_GetPolEffectiveDate      
Created by    : Swastika            
Date          : 20 October,2005            
Purpose       : Get the Effective date of the Policy for Watercraft      
Revison History  :                            
Modified by : Pravesh K Chandel    
Modified Date : 12 Jan 09    
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/              
-- drop proc dbo.Proc_GetPolEffectiveDate 1692,118,4             
ALTER PROCEDURE dbo.Proc_GetPolEffectiveDate            
(            
 @CUSTOMER_ID int,            
 @POLICY_ID int,            
 @POLICY_VERSION_ID int            
)                
AS                     
BEGIN                      
            
-- SELECT DATEPART(YEAR,APP_EFFECTIVE_DATE) AS APP_EFFECTIVE_DATE            
 SELECT CONVERT(VARCHAR,APP_EFFECTIVE_DATE,103) AS APP_EFFECTIVE_DATE  
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID --and IS_ACTIVE='Y'             
       
End      
    
    
    