  
 /*----------------------------------------------------------            
Proc Name   : DBO.PROC_SELECTACTIVITYTYPE         
Created by  : Abhinav Agarwal            
Date        : 20 October-2010      
Purpose     :             
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/     
-- DROP PROC DBO.PROC_SELECTACTIVITYTYPE  
CREATE  PROC [dbo].[PROC_SELECTACTIVITYTYPE]             
(              
 @TYPE INT = NULL         
 )              
AS              
BEGIN              
    IF(@TYPE IS NOT NULL AND @TYPE <> '')  
  SELECT DISTINCT ACTIVITY_ID,ACTIVITY_DESC,TYPE FROM MNT_ACTIVITY_MASTER(NOLOCK) WHERE TYPE=@TYPE AND IS_ACTIVE <> 'N'           
 ELSE  
  SELECT DISTINCT ACTIVITY_ID,ACTIVITY_DESC,TYPE FROM MNT_ACTIVITY_MASTER where IS_ACTIVE <> 'N'       
 END         
    