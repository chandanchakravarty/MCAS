---------------------------------------------------------------      
--Proc Name          : dbo.[Proc_Get_MNT_VESSEL_MASTER]  
--Created by         : Ruchika Chauhan          
--Date               : 19-March-2012             
--------------------------------------------------------      
--Date     Review By          Comments              
------   ------------       -------------------------*/             
-- drop proc dbo.[Proc_Get_MNT_PORT_MASTER]            
CREATE  PROCEDURE [dbo].[Proc_Get_MNT_VESSEL_MASTER]                
AS             
BEGIN            
SELECT VESSEL_ID, VESSEL_NAME FROM MNT_VESSEL_MASTER
END
  
  
--exec Proc_Get_MNT_VESSEL_MASTER




