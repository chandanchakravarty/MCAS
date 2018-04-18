  
---------------------------------------------------------------      
--Proc Name          : dbo.[Proc_Get_COUNTRY_FROM_MNT_PORT_MASTER]   
--Created by         : Ruchika Chauhan          
--Date               : 17-March-2012             
--------------------------------------------------------      
--Date     Review By          Comments              
------   ------------       -------------------------*/             
-- drop proc dbo.[Proc_Get_MNT_PORT_MASTER]            
CREATE  PROCEDURE [dbo].[Proc_Get_COUNTRY_FROM_MNT_PORT_MASTER]                
AS             
BEGIN            
SELECT PORT_CODE,COUNTRY FROM MNT_PORT_MASTER
END
  
  
--exec Proc_Get_COUNTRY_FROM_MNT_PORT_MASTER

--SELECT * FROM MNT_PORT_MASTER
