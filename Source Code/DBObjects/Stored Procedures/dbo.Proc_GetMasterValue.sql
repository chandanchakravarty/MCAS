    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_GetMasterValue]              
Created by      : SNEHA          
Date            : 24/10/2011                      
Purpose         :INSERT RECORDS IN MNT_MASTER_VALUE TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_GetMasterValue]        
      
*/  

CREATE PROC dbo.Proc_GetMasterValue  
(  
@TYPE_UNIQUE_ID INT
)  
AS  
BEGIN  
        
 SELECT   TYPE_UNIQUE_ID,TYPE_ID,LOSS_TYPE,CODE,NAME,DESCRIPTION,RECOVERY_TYPE,IS_ACTIVE
 FROM MNT_MASTER_VALUE WITH(NOLOCK)   
 WHERE TYPE_UNIQUE_ID=@TYPE_UNIQUE_ID             
  
END  
 