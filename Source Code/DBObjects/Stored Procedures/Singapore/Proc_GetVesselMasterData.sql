  
 /*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetVesselMasterData  
Created by           : Abhishek Gorl
Date                    : 14/03/2012  
Purpose               :   
Revison History :  
Used In                :   Singapore Demo    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVesselMasterData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVesselMasterData]
GO
CREATE   PROCEDURE dbo.Proc_GetVesselMasterData  
@VESSEL_ID     int
AS  
BEGIN  
select * from MNT_VESSEL_MASTER WHERE VESSEL_ID =@VESSEL_ID  order by VESSEL_NAME  
END  
  
  