  
 /*----------------------------------------------------------            
Proc Name       : dbo.Proc_Updatefees           
Created by      : Aditya Goel            
Date            : 04/11/2011            
Purpose         : To update record in MNT_SYSTEM_PARAMS            
Revison History :            
Used In         :  Ebix Advantage web        
------------------------------------------------------------            
Date     Review By          Comments            
DROP PROC Dbo.Proc_Updatefees         
------   ------------       -------------------------*/            
            
CREATE  PROC [dbo].[Proc_Updatefees]            
(            
@MAXIMUM_LIMIT   DECIMAL(12,4),  --Changed by Aditya for TFS BUG # 2425            
@FEES_PERCENTAGE DECIMAL(12,4)   --Changed by Aditya for TFS BUG # 2425               
   
)            
AS            
            
BEGIN            
            
  UPDATE MNT_SYSTEM_PARAMS SET            
              
   MAXIMUM_LIMIT   = @MAXIMUM_LIMIT ,            
   FEES_PERCENTAGE   = @FEES_PERCENTAGE              
         
END     
          
        
      