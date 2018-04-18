  
 /*----------------------------------------------------------        
Proc Name       : dbo.Proc_Get_MNT_SYSTEM_PARAMS        
Created by      : Aditya Goel 
Date            : 04/11/2011      
Purpose         : To fetch record in MNT_SYSTEM_PARAMS        
Revison History :        
Used In         : Ebix Advantage web  
    
------------------------------------------------------------        
Date     Review By          Comments        
--Drop Proc dbo.Proc_Get_MNT_SYSTEM_PARAMS     
------   ------------       -------------------------*/           
CREATE PROCEDURE [dbo].[Proc_Get_MNT_SYSTEM_PARAMS]                                
                                
AS                                
BEGIN                                
select MAXIMUM_LIMIT,
FEES_PERCENTAGE
  
 from MNT_SYSTEM_PARAMS with(nolock)                                
END       
    
    
    