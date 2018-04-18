    
 /*----------------------------------------------------------                
Proc Name       : dbo.[Proc_FetchLobList]             
CREATED BY  : Naveen Pujari           
CREATED DATE : August 18, 2011             
Purpose         :            
Revison History :                
Used In         : Ebix Advantage Web                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/          
--DROP PROC dbo.[Proc_FetchLobList]         
CREATE PROC [dbo].[Proc_FetchLobList]               
          
AS                
BEGIN         
  select LOB_ID,SUSEP_LOB_CODE from MNT_LOB_MASTER  with(nolock)  
                 
END                
                
                
              
            
          
     
      