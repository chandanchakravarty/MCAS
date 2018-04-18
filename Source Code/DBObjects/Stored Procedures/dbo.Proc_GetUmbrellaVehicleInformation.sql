IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name          : Dbo.Proc_GetUmbrellaVehicleInformation                          
Created by           : Nidhi                          
Date                    : 27/04/2005                          
Purpose               : To get the vehicle  information  from A table                          
Revison History :                          
Used In                :   Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/     
--drop PROC dbo.Proc_GetUmbrellaVehicleInformation                         
CREATE PROC dbo.Proc_GetUmbrellaVehicleInformation                       
(                          
@CUSTOMERID  int,                          
@APPID  int,                          
@APPVERSIONID int,                          
@VEHICLEID  int                          
                          
)                          
AS                          
BEGIN                          
                          
                          
SELECT             
   ISNULL(A.INSURED_VEH_NUMBER, 0) AS INSURED_VEH_NUMBER,                           
   ISNULL(A.VEHICLE_YEAR, 0) AS VEHICLE_YEAR, ISNULL(A.MAKE, '') AS MAKE,                           
   ISNULL(A.MODEL, '') AS MODEL, ISNULL(A.VIN, '') AS VIN,                           
   ISNULL(A.BODY_TYPE, '') AS BODY_TYPE, ISNULL(A.GRG_ADD1, '')                           
   AS GRG_ADD1, ISNULL(A.GRG_ADD2, '') AS GRG_ADD2, ISNULL(A.GRG_CITY,                           
   '') AS GRG_CITY, ISNULL(A.GRG_COUNTRY, '') AS GRG_COUNTRY,                           
   ISNULL(A.GRG_STATE, '') AS GRG_STATE, ISNULL(A.GRG_ZIP, '') AS GRG_ZIP,                           
   ISNULL(A.REGISTERED_STATE, '') AS REGISTERED_STATE,                           
   ISNULL(A.TERRITORY, '') AS TERRITORY, ISNULL(A.CLASS, '') AS CLASS,                           
   ISNULL(A.REGN_PLATE_NUMBER, '') AS REGN_PLATE_NUMBER,                           
   ISNULL(A.ST_AMT_TYPE, 0) AS ST_AMT_TYPE,A.AMOUNT                          
   AS AMOUNT, A.SYMBOL, ISNULL(A.VEHICLE_AGE, 0)                           
   AS VEHICLE_AGE,                         
               --Added by sumit on Oct 25,2005                        
   ISNULL(A.IS_OWN_LEASE,0) AS IS_OWN_LEASE,                         
   isnull(CONVERT(varchar(10), A.PURCHASE_DATE,101) ,'') as PURCHASE_DATE,                          
   ISNULL(A.IS_NEW_USED,0) AS IS_NEW_USED,                        
   ISNULL(A.AMOUNT_COST_NEW,0) AS AMOUNT_COST_NEW,                        
   ISNULL(A.MILES_TO_WORK,'') AS MILES_TO_WORK,                        
   ISNULL(A.VEHICLE_USE,0) AS VEHICLE_USE,                        
   ISNULL(A.VEH_PERFORMANCE,0) AS VEH_PERFORMANCE,                        
   ISNULL(A.MULTI_CAR,0) AS MULTI_CAR,                        
   ISNULL(A.SAFETY_BELT,0) AS SAFETY_BELT,                        
   ISNULL(CONVERT(VARCHAR,ANNUAL_MILEAGE),'') AS ANNUAL_MILEAGE,                
   ISNULL(A.PASSIVE_SEAT_BELT,0) AS PASSIVE_SEAT_BELT,                        
   ISNULL(A.AIR_BAG,0) AS AIR_BAG,                        
   ISNULL(A.ANTI_LOCK_BRAKES,0) AS ANTI_LOCK_BRAKES,                        
   ISNULL(A.P_SURCHARGES,0) AS P_SURCHARGES,                        
   A.DEACTIVATE_REACTIVATE_DATE AS DEACTIVATE_REACTIVATE_DATE,                        
   ISNULL(A.VEHICLE_CC,0) AS VEHICLE_CC,                        
--   ISNULL(A.MOTORCYCLE_TYPE,0) AS MOTORCYCLE_TYPE,                        
   A.IS_ACTIVE AS IS_ACTIVE,                        
   ISNULL(A.USE_VEHICLE,0) AS USE_VEHICLE,                        
   ISNULL(A.CLASS_PER,0) AS CLASS_PER,                        
   ISNULL(A.CLASS_COM,0) AS CLASS_COM,                        
   ISNULL(A.VEHICLE_TYPE_PER,0) AS VEHICLE_TYPE_PER,                        
   ISNULL(A.VEHICLE_TYPE_COM,0) AS VEHICLE_TYPE_COM,                  
   --   ISNULL(A.UNINS_MOTOR_INJURY_COVE,0) AS UNINS_MOTOR_INJURY_COVE,                      
   --   ISNULL(A.UNINS_PROPERTY_DAMAGE_COVE ,0) AS UNINS_PROPERTY_DAMAGE_COVE,                      
   --   ISNULL(A.UNDERINS_MOTOR_INJURY_COVE,0) AS UNDERINS_MOTOR_INJURY_COVE,   
    ISNULL(A.IS_EXCLUDED,0) AS IS_EXCLUDED, 
    ISNULL(A.OTHER_POLICY,0) AS OTHER_POLICY,            
   B.APP_LOB,        
   CASE WHEN A.MOTORCYCLE_TYPE IS NULL THEN A.VEHICLE_TYPE_PER ELSE A.MOTORCYCLE_TYPE END AS MOTORCYCLE_TYPE                          
FROM                   APP_UMBRELLA_VEHICLE_INFO A INNER JOIN                          
                       APP_LIST B ON A.CUSTOMER_ID = B.CUSTOMER_ID AND                           
                       A.APP_ID = B.APP_ID AND                           
                       A.APP_VERSION_ID = B.APP_VERSION_ID                          
WHERE                  (A.CUSTOMER_ID = @CUSTOMERID)   and (A.APP_ID=@APPID) AND (A.APP_VERSION_ID=@APPVERSIONID) AND (A.VEHICLE_ID= @VEHICLEID);                          
END        
    
  




GO

