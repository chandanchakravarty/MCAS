      
    
 /*----------------------------------------------------------                          
Proc Name       : dbo.Proc_Fetch_Division                  
Created by      : Naveen                      
Date            : 18 Aug 2010                       
Purpose       : Return the Query                         
Revison History :                          
Used In   : Ebix Advantage Web                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/              
-- drop procedure Proc_Fetch_Division                          
CREATE PROC [dbo].Proc_Fetch_Division                          
                        
AS                          
BEGIN                          
  SELECT DIV_ID,DIV_NAME ,DIV_CODE                  
  FROM MNT_DIV_LIST  with(nolock)                       
              
END        
    