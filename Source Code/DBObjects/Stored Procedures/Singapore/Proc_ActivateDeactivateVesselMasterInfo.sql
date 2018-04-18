  /*----------------------------------------------------------  
Proc Name       : dbo.Proc_ActivateDeactivateVesselMasterInfo  
Created by      :  Abhishek Goel  
Date                :  14/03/2012  
Purpose         :  Activate/Deactivate Vessel Master info 
Revison History :  
Used In         :    Singapore Demo  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateVesselMasterInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateVesselMasterInfo]
GO
CREATE   PROC dbo.Proc_ActivateDeactivateVesselMasterInfo  
(  
@VESSEL_ID int,
@IS_ACTIVE  Char(2)     
)  
AS  
BEGIN  
UPDATE MNT_VESSEL_MASTER  
 SET IS_ACTIVE = @IS_ACTIVE  
 WHERE VESSEL_ID= @VESSEL_ID  
END  
  