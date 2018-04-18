/*----------------------------------------------------------                                                    
Proc Name             : Dbo.[Proc_GetRuleCollectionInformation]                                                    
Created by            : Aditya Goel                                                   
Date                  : 22/09/2011                                                   
Purpose               : To get Rule Collection information details          
Revison History       :                                                    
Used In               : Maintenance module          
------------------------------------------------------------                                                    
Date     Review By          Comments                       
              
drop Proc [Proc_GetRuleCollectionInformation]  6                                         
------   ------------       -------------------------*/            
          
CREATE PROC [dbo].[Proc_GetRuleCollectionInformation]         
          
@RULE_COLLECTION_ID   int            
          
AS                                                                                      
BEGIN               
            
    SELECT RULE_COLLECTION_ID,
    RULE_COLLECTION_CODE,
    LOB_ID,
    COUNTRY_ID,
    STATE_ID,
    SUB_LOB_ID,
    PRODUCT_ID,
    APPLICABLE_TO,
    EFFECTIVE_FROM         
    ,EFFECTIVE_TO        
     ,RULE_XML_PATH,
     VALIDATION_ORDER,
     VALIDATE_NEXT_IF_FAILED       
      ,IS_ACTIVE        
      ,CREATED_BY        
      ,CREATED_DATETIME       
      ,MODIFIED_BY       
      ,LAST_UPDATED_DATETIME        
  FROM [dbo].[MNT_RULE_COLLECTION_DETAILS] WITH(NOLOCK)        
  WHERE (RULE_COLLECTION_ID=@RULE_COLLECTION_ID AND IS_ACTIVE='1')          
        
            
END            
          
          