IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name          : Dbo.Proc_GetPolicyVehicleInformation                    
Created by         : Vijay Arora                    
Date               : 03-11-2005                    
Purpose            : To get the vehicle  information  from pol_vehicles table                          
Revison History :                          
Used In            :   Wolverine                          
                
Modified BY     : Shafi                          
Date            : 16-03-2006                
Purpose         : Add the Field For Seat Belt                         
Used In         : Wolverine   

Modified BY     : Lalit Chauhan	                          
Date            : 05-10-2010
Purpose         : Get Count of Vehical in POL_VEHICAL OF POLICY
Used In         : Ebix Advantage Web  
                  
------------------------------------------------------------                          
Date      Review By           Comments                          
09-03-2006 Swastika Gaur  IS_ACTIVE included                  
------    ------------       -------------------------*/                          
--  DROP PROC dbo.Proc_GetPolicyVehicleInformation                  
CREATE PROC [dbo].[Proc_GetPolicyVehicleInformation]                          
(                          
@CUSTOMERID  int,                          
@POLICYID  int,                          
@POLICYVERSIONID int,                          
@VEHICLEID  int                          
)                          
AS                          
BEGIN                          
  DECLARE @VEHICLE_COUNT INT= NULL
    
    SELECT @VEHICLE_COUNT = ISNULL(COUNT(VEHICLE_ID),0) FROM POL_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND IS_ACTIVE= 'Y'
         
                          
SELECT  ISNULL(POL_VEHICLES.INSURED_VEH_NUMBER, 0) AS INSURED_VEH_NUMBER, POL_VEHICLES.VEHICLE_YEAR,                           
                      ISNULL(POL_VEHICLES.MAKE, '') AS MAKE, ISNULL(POL_VEHICLES.MODEL, '') AS MODEL, ISNULL(POL_VEHICLES.VIN, '') AS VIN,                           
                      ISNULL(POL_VEHICLES.BODY_TYPE, '') AS BODY_TYPE, ISNULL(POL_VEHICLES.GRG_ADD1, '') AS GRG_ADD1,                           
                      ISNULL(POL_VEHICLES.GRG_ADD2, '') AS GRG_ADD2, ISNULL(POL_VEHICLES.GRG_CITY, '') AS GRG_CITY,                           
                      ISNULL(POL_VEHICLES.GRG_COUNTRY, '') AS GRG_COUNTRY, ISNULL(POL_VEHICLES.GRG_STATE, '') AS GRG_STATE,                           
                      ISNULL(POL_VEHICLES.GRG_ZIP, '') AS GRG_ZIP, ISNULL(POL_VEHICLES.REGISTERED_STATE, '') AS REGISTERED_STATE,                           
                      ISNULL(POL_VEHICLES.TERRITORY, '') AS TERRITORY, ISNULL(POL_VEHICLES.CLASS, '') AS CLASS,                           
                      ISNULL(POL_VEHICLES.REGN_PLATE_NUMBER, '') AS REGN_PLATE_NUMBER, POL_VEHICLES.ST_AMT_TYPE,VEHICLE_TYPE,                           
                      POL_VEHICLES.AMOUNT, POL_VEHICLES.SYMBOL, POL_VEHICLES.VEHICLE_AGE, POL_CUSTOMER_POLICY_LIST.POLICY_LOB AS APP_LOB,POL_VEHICLES.NATURE_OF_INTEREST,                          
                         
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
                      
ISNULL(POL_VEHICLES.APP_USE_VEHICLE_ID, 0) AS APP_USE_VEHICLE_ID,                        
ISNULL(POL_VEHICLES.APP_VEHICLE_PERCLASS_ID, 0) AS APP_VEHICLE_PERCLASS_ID,                        
ISNULL(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID, 0) AS APP_VEHICLE_COMCLASS_ID,               
ISNULL(POL_VEHICLES.APP_VEHICLE_PERTYPE_ID, 0) AS APP_VEHICLE_PERTYPE_ID,                        
ISNULL(POL_VEHICLES.APP_VEHICLE_COMTYPE_ID, 0) AS APP_VEHICLE_COMTYPE_ID,                 
--ISNULL(POL_VEHICLES.SAFETY_BELT ,'') AS SAFETY_BELT ,                
ISNULL(POL_VEHICLES.IS_ACTIVE,'') AS IS_ACTIVE,                
BUSS_PERM_RESI,SNOWPLOW_CONDS,CAR_POOL,AUTO_POL_NO,            
RADIUS_OF_USE,            
ISNULL(TRANSPORT_CHEMICAL,'') TRANSPORT_CHEMICAL,            
ISNULL(COVERED_BY_WC_INSU,'') COVERED_BY_WC_INSU,            
--POL_VEHICLES.CLASS_DESCRIPTION AS COMMCLASS           
CAST(LOOKUP_UNIQUE_ID AS VARCHAR) + '~' + 'CA' + LOOKUP_FRAME_OR_MASONRY AS COMMCLASS      ,  
--'' as COMMCLASS      
IS_SUSPENDED  ,
@VEHICLE_COUNT AS VEHICLE_COUNT                      
  
 FROM         POL_VEHICLES INNER JOIN                          
                      POL_CUSTOMER_POLICY_LIST ON POL_VEHICLES.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID AND POL_VEHICLES.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID AND                           
                  POL_VEHICLES.POLICY_VERSION_ID = POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID                          
LEFT OUTER JOIN MNT_LOOKUP_VALUES  ON MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID = isnull(POL_VEHICLES.CLASS_DESCRIPTION,1)              
 AND MNT_LOOKUP_VALUES.IS_ACTIVE = 'Y'           
WHERE     (POL_VEHICLES.CUSTOMER_ID = @CUSTOMERID)   and (POL_VEHICLES.POLICY_ID=@POLICYID) AND (POL_VEHICLES.POLICY_VERSION_ID=@POLICYVERSIONID) AND (POL_VEHICLES.VEHICLE_ID= @VEHICLEID)      
--AND LOOKUP_ID = 1286     
    
END                        
                        
                      
      
                    
                  
                  
                  
              
                
                
              
            
          
        
      
    
    
    
    
  
  
  
GO

