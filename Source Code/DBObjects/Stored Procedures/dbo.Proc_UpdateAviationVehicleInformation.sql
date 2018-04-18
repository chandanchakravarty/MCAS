IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAviationVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAviationVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_UpdateAviationVehicleInformation                                
Created by      : Pravesh K Chandel
Date            : 12 Jan 2010                            
Purpose         : To update the record in APP_VEHICLE table                                
Revison History :                                
Used In         :   BRICS
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
--drop proc Proc_UpdateAviationVehicleInformation
CREATE PROC dbo.Proc_UpdateAviationVehicleInformation                                
(                                
 @CUSTOMER_ID    int,                                
 @APP_ID          int,                                
 @APP_VERSION_ID  int,                                
 @VEHICLE_ID     smallint,                                
 @INSURED_VEH_NUMBER  int,                                
@USE_VEHICLE	nvarchar(10),
@VEHICLE_YEAR	varchar(4),
@MAKE	nvarchar(10),
@MAKE_OTHER	nvarchar(50),
@MODEL	nvarchar(10),
@MODEL_OTHER	nvarchar(50),
@COVG_PERIMETER	int,
@REG_NUMBER	nvarchar(10),
@SERIAL_NUMBER	nvarchar(20),
@CERTIFICATION	nvarchar(30),
@REGISTER	nvarchar(30),
@ENGINE_TYPE	nvarchar(10),
@WING_TYPE	nvarchar(10),
@CREW	nvarchar(20),
@PAX	nvarchar(20),
@REMARKS	nvarchar(500),
@IS_ACTIVE	nchar ='Y',
@MODIFIED_BY     int,                                
@LAST_UPDATED_DATETIME  datetime                   
)                                
AS                                
BEGIN                                
 
                         
 UPDATE APP_AVIATION_VEHICLES                                
 SET                                   
 --INSURED_VEH_NUMBER=@INSURED_VEH_NUMBER,                                
VEHICLE_YEAR=@VEHICLE_YEAR,            
MAKE		=@MAKE,                                
MODEL		=@MODEL,          
USE_VEHICLE	=@USE_VEHICLE,
COVG_PERIMETER=@COVG_PERIMETER,
REG_NUMBER	= @REG_NUMBER,
SERIAL_NUMBER	= @SERIAL_NUMBER,
MAKE_OTHER		= @MAKE_OTHER,
MODEL_OTHER		= @MODEL_OTHER,
CERTIFICATION	= @CERTIFICATION,
REGISTER		= @REGISTER,
ENGINE_TYPE		= @ENGINE_TYPE,
WING_TYPE		= @WING_TYPE,
CREW			= @CREW,
PAX				= @PAX,
REMARKS			= @REMARKS,
MODIFIED_BY		= @MODIFIED_BY,
LAST_UPDATED_DATETIME= @LAST_UPDATED_DATETIME
                                  
 WHERE                
  CUSTOMER_ID  =@CUSTOMER_ID AND                  
  APP_ID   =@APP_ID AND                                
  APP_VERSION_ID =@APP_VERSION_ID AND                                
  VEHICLE_ID  =@VEHICLE_ID                                
                         
END                              
                              
             
          

GO

