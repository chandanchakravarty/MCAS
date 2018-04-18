    
/*----------------------------------------------------------                                
Proc Name      : dbo.[Proc_DeleteRuleCollection]                                
Created by     : Aditya Goel                         
Date           : 21/12/2010          
Modify by      :   
Date           :                      
Purpose        : Delete data from MNT_RULE_COLLECTION_DETAILS                                                      
Used In        : Ebix Advantage                            
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
--drop proc dbo.[Proc_DeleteRuleCollection] 
      
CREATE PROC [dbo].[Proc_DeleteRuleCollection]        
(         
@RULE_COLLECTION_ID INT                        
)                    
AS                    
BEGIN                    
  DELETE FROM MNT_RULE_COLLECTION_DETAILS  WHERE 
  RULE_COLLECTION_ID = @RULE_COLLECTION_ID
END    
        
        
     
          