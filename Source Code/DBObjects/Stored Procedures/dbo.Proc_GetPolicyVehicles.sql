IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyVehicles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyVehicles]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------          
Proc Name           : Dbo.Proc_GetPolicyVehicles  
Created by            : Amar  
Date                    : 02 May 2006  
Purpose               : To get all the vehicles for policy  
Revison History  :          
Used In                :   Wolverine          
------   ------------       -------------------------*/          
--drop proc Proc_GetPolicyVehicles  
CREATE PROC Dbo.Proc_GetPolicyVehicles 
(          
	@CLAIM_ID  int,
        @INSURED_VEHICLE_ID int = NULL 
)          
AS          
BEGIN         
	IF (@INSURED_VEHICLE_ID IS NULL)
		 SELECT  VIN + ' - ' + VEHICLE_YEAR + ' - ' + MAKE + ' - ' + MODEL AS VIN_NUMBER,
		 INSURED_VEHICLE_ID AS VEHICLE_ID, VEHICLE_YEAR, MAKE, MODEL, VIN, BODY_TYPE  
		 FROM  CLM_INSURED_VEHICLE 
		 WHERE CLAIM_ID = @CLAIM_ID  
	ELSE
		 SELECT  VIN + ' - ' + VEHICLE_YEAR + ' - ' + MAKE + ' - ' + MODEL AS VIN_NUMBER,
		 INSURED_VEHICLE_ID AS VEHICLE_ID, VEHICLE_YEAR, MAKE, MODEL, VIN, BODY_TYPE  
		 FROM  CLM_INSURED_VEHICLE 
		 WHERE CLAIM_ID = @CLAIM_ID AND  INSURED_VEHICLE_ID = @INSURED_VEHICLE_ID	
END          
    


GO

