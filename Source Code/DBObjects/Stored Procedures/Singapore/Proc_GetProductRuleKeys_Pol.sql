    
/* ===========================================================================================================                                                                                            
Proc Name                : dbo.Proc_GetProductRuleKeys_Pol                                                                                                                                         
Created by               : Pravesh K Chandel                                                                                                                                                          
Date                     : 14 June. 2010                                                                                                                                          
Purpose                  : To get the policy /Product keys        
Revison History          :                                                                                                                                                          
Used In                  : EbixAdvantage          
===========================================================================================================                                                                                            
Date     Review By          Comments                                                                                                                                                          
==========================================================================================================                                                                               
drop proc dbo.Proc_GetProductRuleKeys_Pol 2156,430,2          
*/                                                                                           
ALTER proc [dbo].[Proc_GetProductRuleKeys_Pol]                                                                                                                                               
(         
 @CUSTOMER_ID    int,                                                                                                                                                          
 @POLICY_ID    int,                                                                                                                                                          
 @POLICY_VERSION_ID   int                                                                                                                  
)                                                                                                                                                          
AS                                                                                                                                           
BEGIN            
DECLARE @COUNT_LEADER INT ,@RISK INT,@POLICY_LOB INT                 
 SELECT @COUNT_LEADER = ISNULL(COUNT(*),0) FROM POL_CO_INSURANCE WITH(NOLOCK)      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND       
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND LEADER_FOLLOWER = 14548       
                                                                                              
      
  SELECT @POLICY_LOB = POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND     
 POLICY_VERSION_ID = @POLICY_VERSION_ID    
     
     
 EXEC Proc_GetPolicyRiskCount @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@POLICY_LOB,@RISK OUT    
    
 --table 0 POlicy Table          
 SELECT          
         
   POLICY_LOB as LOB_ID,        
   POLICY_SUBLOB as SUB_LOB_ID,        
   APP_EFFECTIVE_DATE,        
   CO_INSURANCE as COINSURANCES,        
   CO_INSURANCE as CO_INSURANCE  ,      
   @COUNT_LEADER AS LEADER_COUNT,      
     ISNULL(@RISK,0) AS RISK    
 FROM  POL_CUSTOMER_POLICY_LIST APPLICATION WITH(NOLOCK)           
 WHERE APPLICATION.CUSTOMER_ID  =@CUSTOMER_ID         
 and APPLICATION.POLICY_ID  =@POLICY_ID         
 and APPLICATION.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                      
       END               
        