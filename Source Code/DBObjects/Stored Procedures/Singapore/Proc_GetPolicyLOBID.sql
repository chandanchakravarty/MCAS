/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetPolicyLOBID    
Created by      : Vijay Arora      
Date            : 04-11-2005       
Purpose         : It will get the Policy LOB Id.    
Revison History :          
Used In         : Wolverine            
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
ALTER PROC [dbo].[Proc_GetPolicyLOBID]      
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID SMALLINT    
AS      
BEGIN      
SELECT POLICY_LOB,CONVERT(NVARCHAR(20),ISNULL(APP_EFFECTIVE_DATE,''),101) AS APPEFFECTIVEDATE,ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') AS STATENAME  
FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)     
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK)   
ON   
POL_CUSTOMER_POLICY_LIST.STATE_ID = 0  
     
WHERE CUSTOMER_ID = @CUSTOMER_ID    
AND POLICY_ID = @POLICY_ID    
AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
END      
    
  
  
  
  
  
  
  
  