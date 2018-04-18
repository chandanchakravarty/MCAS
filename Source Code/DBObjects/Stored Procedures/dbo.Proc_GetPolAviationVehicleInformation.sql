IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolAviationVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolAviationVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name          :  dbo.Proc_GetPolAviationVehicleInformation                                
Created by         : Pravesh K Chandel
Date               : 14 Jan 2010
Purpose            : To get the Aviation vehicle  information  from Pol_aviation_vehicles table                                
Revison History :                               
Used In            :   Brics
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                           
-- drop proc  dbo.Proc_GetPolAviationVehicleInformation                      
CREATE PROC dbo.Proc_GetPolAviationVehicleInformation         
(                                
@CUSTOMERID  int,                                
@POLICY_ID  int,                                
@POLICY_VERSION_ID int,                                
@VEHICLE_ID  int                                
)                                
AS                                
BEGIN                                
                                
SELECT    
ISNULL(PAV.INSURED_VEH_NUMBER, 0) AS INSURED_VEH_NUMBER, 
PAV.VEHICLE_YEAR,                                 
PAV.MAKE AS MAKE, 
ISNULL(PAV.MAKE_OTHER, '') AS MAKE_OTHER, 
PAV.MODEL AS MODEL,
ISNULL(PAV.MODEL_OTHER, '') AS MODEL_OTHER,
ISNULL(PAV.USE_VEHICLE, 0) AS USE_VEHICLE,                                            
ISNULL(PAV.IS_ACTIVE, 'Y') AS IS_ACTIVE,                
COVG_PERIMETER,
REG_NUMBER,
SERIAL_NUMBER,
CERTIFICATION,
REGISTER,
ENGINE_TYPE,
WING_TYPE,
CREW,
PAX,
REMARKS
FROM POL_AVIATION_VEHICLES PAV WITH(NOLOCK)
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON PAV.CUSTOMER_ID = PCPL.CUSTOMER_ID 
			AND PAV.POLICY_ID			= PCPL.POLICY_ID 
			AND PAV.POLICY_VERSION_ID	= PCPL.POLICY_VERSION_ID              

WHERE   PAV.CUSTOMER_ID		= @CUSTOMERID  
	AND PAV.POLICY_ID		=@POLICY_ID 
	AND PAV.POLICY_VERSION_ID=@POLICY_VERSION_ID 
	AND PAV.VEHICLE_ID		= @VEHICLE_ID          
      
END    
      
     

GO

