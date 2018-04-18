    
 /*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateIOFDetails             
Created by      : Aditya Goel              
Date            : 04/11/2011              
Purpose         : To update record in MNT_LOB_MASTER              
Revison History :              
Used In         :  Ebix Advantage web          
------------------------------------------------------------              
Date     Review By          Comments              
DROP PROC Dbo.Proc_UpdateIOFDetails           
------   ------------       -------------------------*/              
              
CREATE  PROC [dbo].[Proc_UpdateIOFDetails]              
(   
@LOB_ID INT,           
@IOF_PERCENTAGE   DECIMAL(12,4)       
     
)              
AS              
              
BEGIN              
              
  UPDATE MNT_LOB_MASTER SET              
                
   IOF_PERCENTAGE   = @IOF_PERCENTAGE    
   WHERE LOB_ID = @LOB_ID           
           
END       
            
          