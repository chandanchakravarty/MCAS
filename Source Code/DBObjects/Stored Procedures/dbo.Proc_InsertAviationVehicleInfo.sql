IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAviationVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAviationVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*========================================================================      
Proc Name       : dbo.Proc_InsertAviationVehicleInfo                                
Created by      : Pravesh K Chandel                           
Date            : 11 Jan 2009
Purpose        :Insert  Aviation Vehicle Info
Revison History :                                
Used In        : Brics
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/  
-- DROP proc dbo.Proc_InsertAviationVehicleInfo                               
CREATE PROC dbo.Proc_InsertAviationVehicleInfo                                
(                                
 @CUSTOMER_ID     int,                                
 @APP_ID     int ,                                
 @APP_VERSION_ID     smallint,                                
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
          
SELECT  @STATE_ID = STATE_ID          
FROM APP_LIST          
WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
 APP_ID = @APP_ID AND          
 APP_VERSION_ID = @APP_VERSION_ID          
                            
SELECT @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 from APP_AVIATION_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID                                
                              

SELECT  @TEMP_INSURED_VEHICLE_NUMBER = (isnull(MAX(INSURED_VEH_NUMBER),0)) + 1 FROM APP_AVIATION_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID              
                                

INSERT INTO APP_AVIATION_VEHICLES                                 
(                                
CUSTOMER_ID,
APP_ID,
APP_VERSION_ID,
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
@APP_ID,
@APP_VERSION_ID,
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
		
----Copy Policy level vehicles from any other vehicle if it exists--          
--IF (ISNULL(@VEHICLE_TYPE_PER,'0') NOT IN ('11870','11337','11618')  
--	AND isnull(@VEHICLE_TYPE_COM,0) NOT IN('11341')
--	AND isnull(@IS_SUSPENDED,0) !=10963
--	) 
--BEGIN        
--	EXEC Proc_COPY_POLICY_LEVEL_COVERAGES_APP               
--	 @CUSTOMER_ID,
--	 @APP_ID,
--	 @APP_VERSION_ID,
--	 @VEHICLE_ID
--	------------------   End of policy level 
--END
            
                                
END                        




GO

