IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyUmbrellaVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyUmbrellaVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name          : Dbo.Proc_GetPolicyUmbrellaVehicleInformation      
Created by         : Sumit Chhabra      
Date               : 03-20-2006            
Purpose            : To get the vehicle  information  from POL_UMBRELLA_VEHICLE_INFO table                  
Revison History :                  
Used In            :   Wolverine                  
 */      
--DROP PROC Proc_GetPolicyUmbrellaVehicleInformation 1199,79,1,2         
CREATE PROC dbo.Proc_GetPolicyUmbrellaVehicleInformation                  
(                  
@CUSTOMERID  int,                  
@POLICYID  int,                  
@POLICYVERSIONID int,                  
@VEHICLEID  int                  
)                  
AS                  
BEGIN                  
                  
SELECT        
  ISNULL(PUVI.INSURED_VEH_NUMBER, 0) AS INSURED_VEH_NUMBER, PUVI.VEHICLE_YEAR,                   
  ISNULL(PUVI.MAKE, '') AS MAKE, ISNULL(PUVI.MODEL, '') AS MODEL, ISNULL(PUVI.VIN, '') AS VIN,                   
  ISNULL(PUVI.BODY_TYPE, '') AS BODY_TYPE, ISNULL(PUVI.GRG_ADD1, '') AS GRG_ADD1,                   
  ISNULL(PUVI.GRG_ADD2, '') AS GRG_ADD2, ISNULL(PUVI.GRG_CITY, '') AS GRG_CITY,                   
  ISNULL(PUVI.GRG_COUNTRY, '') AS GRG_COUNTRY, ISNULL(PUVI.GRG_STATE, '') AS GRG_STATE,                   
  ISNULL(PUVI.GRG_ZIP, '') AS GRG_ZIP, ISNULL(PUVI.REGISTERED_STATE, '') AS REGISTERED_STATE,                   
  ISNULL(PUVI.TERRITORY, '') AS TERRITORY, ISNULL(PUVI.CLASS, '') AS CLASS,                   
  ISNULL(PUVI.REGN_PLATE_NUMBER, '') AS REGN_PLATE_NUMBER, PUVI.ST_AMT_TYPE,      
  --VEHICLE_TYPE,                   
  PUVI.AMOUNT, PUVI.SYMBOL, PUVI.VEHICLE_AGE, PCPL.POLICY_LOB AS APP_LOB,      
  --PUVI.NATURE_OF_INTEREST,                  
        
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
  CASE WHEN PUVI.MOTORCYCLE_TYPE IS NOT NULL THEN PUVI.MOTORCYCLE_TYPE
       WHEN PUVI.VEHICLE_TYPE_PER IS NOT NULL THEN PUVI.VEHICLE_TYPE_PER
       WHEN PUVI.VEHICLE_TYPE_COM IS NOT NULL THEN PUVI.VEHICLE_TYPE_COM
       end
 AS MOTORCYCLE_TYPE,
                 
        
  --isnull(UNDERINS_MOTOR_INJURY_COVE,'') as UNDERINS_MOTOR_INJURY_COVE,                  
  --isnull(UNINS_MOTOR_INJURY_COVE,'') as UNINS_MOTOR_INJURY_COVE,                  
  --isnull(UNINS_PROPERTY_DAMAGE_COVE,'') as UNINS_PROPERTY_DAMAGE_COVE,                
  ISNULL(PUVI.USE_VEHICLE, 0) AS USE_VEHICLE,                
  ISNULL(PUVI.CLASS_PER, 0) AS APP_VEHICLE_PERCLASS_ID,                
  ISNULL(PUVI.CLASS_COM, 0) AS APP_VEHICLE_COMCLASS_ID,                
  ISNULL(PUVI.VEHICLE_TYPE_PER, 0) AS APP_VEHICLE_PERTYPE_ID,                
  ISNULL(PUVI.VEHICLE_TYPE_COM, 0) AS APP_VEHICLE_COMTYPE_ID,         
--  ISNULL(PUVI.SAFETY_BELT ,'') AS SAFETY_BELT ,        
  ISNULL(PUVI.IS_ACTIVE,'') AS IS_ACTIVE,    
  ISNULL(PUVI.IS_EXCLUDED,'') AS IS_EXCLUDED ,     
    ISNULL(PUVI.OTHER_POLICY,0) AS OTHER_POLICY   
                  
            
                
 FROM         POL_UMBRELLA_VEHICLE_INFO PUVI INNER JOIN                  
                      POL_CUSTOMER_POLICY_LIST PCPL ON PUVI.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PUVI.POLICY_ID = PCPL.POLICY_ID AND                   
                      PUVI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID                  
WHERE     (PUVI.CUSTOMER_ID = @CUSTOMERID)   and (PUVI.POLICY_ID=@POLICYID) AND (PUVI.POLICY_VERSION_ID=@POLICYVERSIONID) AND (PUVI.VEHICLE_ID= @VEHICLEID);                  
END              
    


GO

