/*---------------------------------------------------------------  
Proc Name          : dbo.[Proc_Active_Deactive_MasterValue]  
Created by      : SNEHA          
Date            : 24/10/2011                      
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/         
-- drop proc dbo.[Proc_Active_Deactive_MasterValue]        
CREATE  PROCEDURE [dbo].[Proc_Active_Deactive_MasterValue]        
(         
  @TYPE_UNIQUE_ID int,  
  @IS_ACTIVE nChar(2)  
  
)          
AS         
BEGIN        
UPDATE  MNT_MASTER_VALUE      
 SET                 
    IS_ACTIVE = @IS_ACTIVE               
 WHERE                
    TYPE_UNIQUE_ID=@TYPE_UNIQUE_ID 
    
End