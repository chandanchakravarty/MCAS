--------------------------------------------------------------    
--Proc Name          : dbo.ACTIVATE_DEACTIVATE_MNT_CURRENCY_MASTER   
--Created by         : Avijit Goswami         
--Date               : 20/01/2012     
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[ACTIVATE_DEACTIVATE_MNT_CURRENCY_MASTER]          
create  PROCEDURE [dbo].[ACTIVATE_DEACTIVATE_MNT_CURRENCY_MASTER]          
(           
  @CURRENCY_ID int,    
  @IS_ACTIVE Char(1)    
)            
AS           
BEGIN          
UPDATE  MNT_CURRENCY_MASTER  
 SET                   
    IS_ACTIVE = @IS_ACTIVE                 
 WHERE                  
 CURRENCY_ID=@CURRENCY_ID      
End