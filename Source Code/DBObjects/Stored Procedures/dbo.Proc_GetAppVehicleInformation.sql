IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name          : Dbo.Proc_GetAppInformation                                
Created by           : Nidhi                                
Date                    : 27/04/2005                                
Purpose               : To get the vehicle  information  from app_vehicle table                                
Revison History :                                
Used In                :   Wolverine                                
                                
Modified By : Anurag Verma                                
Modfied On : 20/09/2005                                
Purpose : Personal vehicle info screen is merged with vehicle info                                
                              
Modified By : Vijay Arora                              
Modfied On : 10-10-2005                              
Purpose : To set the vehicle use on personal and commercial                              
                            
Modified By : Mohit Gupta                            
Modfied On : 21-10-2005                              
Purpose : Changing field names as changed in respective table.                            
                          
Modified By : Sumit Chhabra                          
Modfied On : 21-11-2005                              
Purpose : Added the column name is_active to be fetched also                          
                            
Reviewed By	:	Anurag Verma
Reviewed On	:	06-07-2007                            
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                           
-- drop proc  dbo.Proc_GetAppVehicleInformation                             
CREATE PROC dbo.Proc_GetAppVehicleInformation         
(                                
@CUSTOMERID  int,                                
@APPID  int,                                
@APPVERSIONID int,                                
@VEHICLEID  int                                
                                
)                                
AS                                
BEGIN                                
                                
                                
SELECT     ISNULL(APP_VEHICLES.INSURED_VEH_NUMBER, 0) AS INSURED_VEH_NUMBER, APP_VEHICLES.VEHICLE_YEAR,                                 
                      ISNULL(APP_VEHICLES.MAKE, '') AS MAKE, ISNULL(APP_VEHICLES.MODEL, '') AS MODEL, ISNULL(APP_VEHICLES.VIN, '') AS VIN,                                 
                      ISNULL(APP_VEHICLES.BODY_TYPE, '') AS BODY_TYPE, ISNULL(APP_VEHICLES.GRG_ADD1, '') AS GRG_ADD1,                                 
                      ISNULL(APP_VEHICLES.GRG_ADD2, '') AS GRG_ADD2, ISNULL(APP_VEHICLES.GRG_CITY, '') AS GRG_CITY,                                 
                      ISNULL(APP_VEHICLES.GRG_COUNTRY, '') AS GRG_COUNTRY, ISNULL(APP_VEHICLES.GRG_STATE, '') AS GRG_STATE,                                 
                      ISNULL(APP_VEHICLES.GRG_ZIP, '') AS GRG_ZIP, ISNULL(APP_VEHICLES.REGISTERED_STATE, '') AS REGISTERED_STATE,                                 
                      ISNULL(APP_VEHICLES.TERRITORY, '') AS TERRITORY, ISNULL(APP_VEHICLES.CLASS, '') AS CLASS,                                 
                      ISNULL(APP_VEHICLES.REGN_PLATE_NUMBER, '') AS REGN_PLATE_NUMBER, APP_VEHICLES.ST_AMT_TYPE,VEHICLE_TYPE,                                 
                      APP_VEHICLES.AMOUNT, APP_VEHICLES.SYMBOL, APP_VEHICLES.VEHICLE_AGE, APP_LIST.APP_LOB,APP_VEHICLES.NATURE_OF_INTEREST,                                
                               
isnull(IS_OWN_LEASE,'') as IS_OWN_LEASE,                                
isnull(CONVERT(varchar(10), PURCHASE_DATE,101) ,'') as PURCHASE_DATE,                                
isnull(IS_NEW_USED,'') AS IS_NEW_USED,              
isnull(MILES_TO_WORK,'') AS  MILES_TO_WORK,                  
isnull(VEHICLE_USE,'') AS VEHICLE_USE,                                
isnull(VEH_PERFORMANCE,'') AS VEH_PERFORMANCE,                                
isnull(MULTI_CAR,'')  AS MULTI_CAR,                              
isnull(Convert(varchar,ANNUAL_MILEAGE),'') AS ANNUAL_MILEAGE,                          
isnull(PASSIVE_SEAT_BELT,'')  AS PASSIVE_SEAT_BELT,                                
isnull(AIR_BAG,'') as AIR_BAG,                                
isnull(ANTI_LOCK_BRAKES,'') as ANTI_LOCK_BRAKES,                                
                                
--isnull(UNDERINS_MOTOR_INJURY_COVE,'') as UNDERINS_MOTOR_INJURY_COVE,                                
--isnull(UNINS_MOTOR_INJURY_COVE,'') as UNINS_MOTOR_INJURY_COVE,                                
--isnull(UNINS_PROPERTY_DAMAGE_COVE,'') as UNINS_PROPERTY_DAMAGE_COVE,                   
                     
--commented by mohit on 21-10-2005                   
--ISNULL(APP_VEHICLES.APP_USE_VEHICLE_ID, 0) AS APP_USE_VEHICLE_ID,                              
--ISNULL(APP_VEHICLES.APP_VEHICLE_PERCLASS_ID, 0) AS APP_VEHICLE_PERCLASS_ID,                              
--ISNULL(APP_VEHICLES.APP_VEHICLE_COMCLASS_ID, 0) AS APP_VEHICLE_COMCLASS_ID,                              
--ISNULL(APP_VEHICLES.APP_VEHICLE_PERTYPE_ID, 0) AS APP_VEHICLE_PERTYPE_ID,                              
--ISNULL(APP_VEHICLES.APP_VEHICLE_COMTYPE_ID, 0) AS APP_VEHICLE_COMTYPE_ID                                
                            
-- added by mohit on 21-10-2005 as field name are changed for the respective table.                            
ISNULL(APP_VEHICLES.USE_VEHICLE, 0) AS USE_VEHICLE,                                            
/*ISNULL(APP_VEHICLES.CLASS_PER, '') AS CLASS_PER,                            
ISNULL(APP_VEHICLES.CLASS_COM, '') AS CLASS_COM,                            
ISNULL(APP_VEHICLES.VEHICLE_TYPE_PER, '') AS VEHICLE_TYPE_PER,                            
ISNULL(APP_VEHICLES.VEHICLE_TYPE_COM, '') AS VEHICLE_TYPE_COM,          */                
APP_VEHICLES.CLASS_PER,                
APP_VEHICLES.CLASS_COM,                
APP_VEHICLES.VEHICLE_TYPE_PER,                
APP_VEHICLES.VEHICLE_TYPE_COM,                
ISNULL(APP_VEHICLES.IS_ACTIVE, 'Y') AS IS_ACTIVE,                
APP_VEHICLES.BUSS_PERM_RESI,                
APP_VEHICLES.SNOWPLOW_CONDS,                
APP_VEHICLES.CAR_POOL,                
--ISNULL(APP_VEHICLES.SAFETY_BELT ,'') AS SAFETY_BELT,              
APP_VEHICLES.AUTO_POL_NO,            
APP_VEHICLES.RADIUS_OF_USE,            
APP_VEHICLES.TRANSPORT_CHEMICAL,            
APP_VEHICLES.COVERED_BY_WC_INSU,                                                
-- APP_VEHICLES.CLASS_DESCRIPTION AS CLASS_DESCRIPTION,            
-- CAST(LOOKUP_UNIQUE_ID AS VARCHAR) + '~' + 'CA' + LOOKUP_FRAME_OR_MASONRY AS COMMCLASS          
APP_VEHICLES.CLASS_DESCRIPTION AS CLASS_DESCRIPTION1,            
CAST(LOOKUP_UNIQUE_ID AS VARCHAR) + '~' + 'CA' + LOOKUP_FRAME_OR_MASONRY AS CLASS_DESCRIPTION ,
IS_SUSPENDED  
--'' as COMMCLASS        
FROM         APP_VEHICLES INNER JOIN                                
                      APP_LIST ON APP_VEHICLES.CUSTOMER_ID = APP_LIST.CUSTOMER_ID AND APP_VEHICLES.APP_ID = APP_LIST.APP_ID AND                                 
                      APP_VEHICLES.APP_VERSION_ID = APP_LIST.APP_VERSION_ID              
LEFT  JOIN MNT_LOOKUP_VALUES  ON MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID = APP_VEHICLES.CLASS_DESCRIPTION      
 AND MNT_LOOKUP_VALUES.IS_ACTIVE = 'Y'           
WHERE     (APP_VEHICLES.CUSTOMER_ID = @CUSTOMERID)   and (APP_VEHICLES.APP_ID=@APPID) AND (APP_VEHICLES.APP_VERSION_ID=@APPVERSIONID) AND (APP_VEHICLES.VEHICLE_ID= @VEHICLEID)          
       
--AND LOOKUP_ID = 1286      
      
END                              
      
     
      
      
    
  






GO

