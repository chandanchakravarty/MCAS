IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAviationPolicyVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAviationPolicyVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*========================================================================      
Proc Name       : dbo.Proc_InsertAviationPolicyVehicleInfo                                
Created by      : Pravesh K Chandel                           
Date            : 14 Jan 2009
Purpose        :Insert Pol Aviation Vehicle Info
Revison History :                                
Used In        : Brics
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/  
-- DROP proc dbo.Proc_InsertAviationPolicyVehicleInfo                               
CREATE PROC dbo.Proc_InsertAviationPolicyVehicleInfo                                
(                                
 @CUSTOMER_ID     int,                                
 @POLICY_ID     int ,                                
 @POLICY_VERSION_ID     smallint,                                
 @VEHICLE_ID     smallint output,                                
 @INSURED_VEH_NUMBER     smallint,                                
@USE_VEHICLE	nvarchar(10),
@COVG_PERIMETER	int,
@REG_NUMBER	nvarchar(10),
@SERIAL_NUMBER	nvarchar(20),
@VEHICLE_YEAR	varchar(4),
@MAKE	nvarchar(10),
@MAKE_OTHER	nvarchar(50),
@MODEL	nvarchar(10),
@MODEL_OTHER	nvarchar(50),
@CERTIFICATION	nvarchar(30),
@REGISTER	nvarchar(30),
@ENGINE_TYPE	nvarchar(10),
@WING_TYPE	nvarchar(10),
@CREW	nvarchar(20),
@PAX	nvarchar(20),
@REMARKS	nvarchar(500),
@IS_ACTIVE	nchar ='Y',
@CREATED_BY	int,
@CREATED_DATETIME	datetime
)                          
AS                                
BEGIN                                

                             
DECLARE @TEMP_INSURED_VEHICLE_NUMBER INT                              
DECLARE @STATE_ID Int          
          
--SELECT  @STATE_ID = STATE_ID          
--FROM POL_CUSTOMER_POLICY_LIST          WITH(NOLOCK)
--WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
-- APP_ID = @APP_ID AND          
-- APP_VERSION_ID = @APP_VERSION_ID          
                            
SELECT @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 from POL_AVIATION_VEHICLES WITH(NOLOCK) 
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and POLICY_ID=@POLICY_ID                                
                              

SELECT  @TEMP_INSURED_VEHICLE_NUMBER = (isnull(MAX(INSURED_VEH_NUMBER),0)) + 1 FROM POL_AVIATION_VEHICLES WITH(NOLOCK) 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID              
                                

INSERT INTO POL_AVIATION_VEHICLES                                 
(                                
CUSTOMER_ID,
POLICY_ID,
POLICY_VERSION_ID,
VEHICLE_ID,
INSURED_VEH_NUMBER,
USE_VEHICLE,
COVG_PERIMETER,
REG_NUMBER,
SERIAL_NUMBER,
VEHICLE_YEAR,
MAKE,
MAKE_OTHER,
MODEL,
MODEL_OTHER,
CERTIFICATION,
REGISTER,
ENGINE_TYPE,
WING_TYPE,
CREW,
PAX,
REMARKS,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME
)                                
VALUES                                
(                                
@CUSTOMER_ID,
@POLICY_ID,
@POLICY_VERSION_ID,
@VEHICLE_ID,
@TEMP_INSURED_VEHICLE_NUMBER, --@INSURED_VEH_NUMBER,
@USE_VEHICLE,
@COVG_PERIMETER,
@REG_NUMBER,
@SERIAL_NUMBER,
@VEHICLE_YEAR,
@MAKE,
@MAKE_OTHER,
@MODEL,
@MODEL_OTHER,
@CERTIFICATION,
@REGISTER,
@ENGINE_TYPE,
@WING_TYPE,
@CREW,
@PAX,
@REMARKS,
'Y',--@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME
)                
            
                                
END    





GO

