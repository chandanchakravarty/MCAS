  /*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetIOFDetails      
Created by           : Aditya Goel    
Date                    : 04/11/2011      
Purpose               :       
Revison History :      
Used In                :   Ebix Advantage web      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--DROP PROC Dbo.Proc_GetIOFDetails   
  
CREATE   PROCEDURE Proc_GetIOFDetails      
@LOB_ID INT  
AS      
BEGIN      
SELECT LOB_ID,LOB_DESC,IOF_PERCENTAGE FROM MNT_LOB_MASTER WITH(NOLOCK)          
WHERE LOB_ID = @LOB_ID  AND IS_ACTIVE = 'Y'    
END      
  
  