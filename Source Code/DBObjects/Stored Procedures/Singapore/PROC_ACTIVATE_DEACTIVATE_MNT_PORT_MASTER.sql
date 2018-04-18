--------------------------------------------------------------    
--Proc Name          : dbo.PROC_ACTIVATE_DEACTIVATE_MNT_PORT_MASTER 
--Created by         : Avijit Goswami         
--Date               : 20/01/2012     
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[PROC_ACTIVATE_DEACTIVATE_MNT_PORT_MASTER ]          
create  PROCEDURE [dbo].[PROC_ACTIVATE_DEACTIVATE_MNT_PORT_MASTER]          
(           
  @PORT_CODE int,    
  @IS_ACTIVE Char(1)    
)            
AS           
BEGIN          
UPDATE  MNT_PORT_MASTER   
 SET                   
    IS_ACTIVE = @IS_ACTIVE                 
 WHERE                  
 PORT_CODE=@PORT_CODE    
End