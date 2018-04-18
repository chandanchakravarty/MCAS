IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_DISTINCT_APP_VEHICLES_FOR_UMBRELLA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_DISTINCT_APP_VEHICLES_FOR_UMBRELLA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                  
Proc Name   : dbo.PROC_GET_DISTINCT_APP_VEHICLES_FOR_UMBRELLA                        
Created by  : Sumit Chhabra              
Date        : 11 Oc,2006                        
Purpose     : Fetch vehicles for umbrella sch.               
Revison History  :                                        
 ------------------------------------------------------------                                              
Date     Review By          Comments                                            
                                   
------   ------------       -------------------------*/             
            
-- DROP PROC  dbo.PROC_GET_DISTINCT_APP_VEHICLES_FOR_UMBRELLA                            
CREATE PROC dbo.PROC_GET_DISTINCT_APP_VEHICLES_FOR_UMBRELLA                           
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID smallint    
AS                            
BEGIN                                
    
                              
 /* SELECT                             
 INSURED_VEH_NUMBER,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,    
 REGISTERED_STATE,TERRITORY,CLASS,REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,    
 IS_NEW_USED,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,    
 AIR_BAG,ANTI_LOCK_BRAKES,DEACTIVATE_REACTIVATE_DATE,VEHICLE_CC,/*MOTORCYCLE_TYPE,*/*/
/*USE_VEHICLE,    
 CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,UNINS_MOTOR_INJURY_COVE,UNINS_PROPERTY_DAMAGE_COVE,    
 UNDERINS_MOTOR_INJURY_COVE,SAFETY_BELT,    
    
 CASE  WHEN APP_LIST.APP_LOB='3' THEN 11957 -- MOTORCYCLE    
       WHEN APP_LIST.APP_LOB='1' THEN 11958 -- MOTOR HOME    
       WHEN APP_LIST.APP_LOB='2' THEN 11956 -- AUTOMOBILE    
 ELSE 11959 END AS MOTORCYCLE_TYPE, --OTHER    
  
CASE  WHEN APP_LIST.APP_LOB='3' THEN 'MOTORCYCLE'    
       WHEN APP_LIST.APP_LOB='1' THEN 'MOTOR HOME'    
       WHEN APP_LIST.APP_LOB='2' THEN 'AUTOMOBILE'    
 ELSE 'OTHER' END AS MOTORCYCLE_TYPE_DESC   
    
  INTO #TEMP_VEHICLES                            
  FROM APP_VEHICLES                            
  INNER JOIN APP_LIST ON APP_VEHICLES.CUSTOMER_ID = APP_LIST.CUSTOMER_ID                            
  AND APP_VEHICLES.APP_ID = APP_LIST.APP_ID                             
  AND APP_VEHICLES.APP_VERSION_ID = APP_LIST.APP_VERSION_ID                             
  INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES ON APP_LIST.CUSTOMER_ID = APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID                            
  AND APP_LIST.APP_NUMBER =                           
  LTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                        
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                              
  AND APP_LIST.APP_VERSION_ID=                          
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                        
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                          
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                        
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                        
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                        
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))         
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST S ON        
 APP_VEHICLES.REGISTERED_STATE = S.STATE_ID        
        
  WHERE APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID                         
  AND APP_UMBRELLA_UNDERLYING_POLICIES.APP_ID = @APP_ID                             
  AND APP_UMBRELLA_UNDERLYING_POLICIES.APP_VERSION_ID = @APP_VERSION_ID                             
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,0)=0                      
  AND APP_VEHICLES.IS_ACTIVE='Y'               
                        
  UNION                            
                              
  SELECT                             
 INSURED_VEH_NUMBER,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,    
 REGISTERED_STATE,TERRITORY,CLASS,REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,    
 IS_NEW_USED,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,    
 AIR_BAG,ANTI_LOCK_BRAKES,DEACTIVATE_REACTIVATE_DATE,VEHICLE_CC,/*MOTORCYCLE_TYPE,*/    
 --USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM, */   
 /*APP_USE_VEHICLE_ID AS USE_VEHICLE,    
 APP_VEHICLE_PERCLASS_ID AS CLASS_PER,    
 APP_VEHICLE_COMCLASS_ID AS CLASS_COM,    
 APP_VEHICLE_PERTYPE_ID AS VEHICLE_TYPE_PER,    
 APP_VEHICLE_COMTYPE_ID AS VEHICLE_TYPE_COM,    
 UNINS_MOTOR_INJURY_COVE,UNINS_PROPERTY_DAMAGE_COVE,    
 UNDERINS_MOTOR_INJURY_COVE,SAFETY_BELT ,    
    
 CASE WHEN POL_CUSTOMER_POLICY_LIST.POLICY_LOB='3' THEN 11957 -- MOTORCYCLE    
   WHEN POL_CUSTOMER_POLICY_LIST.POLICY_LOB='1' THEN 11958 -- MOTOR HOME    
   WHEN POL_CUSTOMER_POLICY_LIST.POLICY_LOB='2' THEN 11956 -- AUTOMOBILE    
 ELSE 11959 END AS MOTORCYCLE_TYPE, --OTHER   
  
CASE WHEN POL_CUSTOMER_POLICY_LIST.POLICY_LOB='3' THEN 'MOTORCYCLE'  
   WHEN POL_CUSTOMER_POLICY_LIST.POLICY_LOB='1' THEN 'MOTOR HOME'  
   WHEN POL_CUSTOMER_POLICY_LIST.POLICY_LOB='2' THEN 'AUTOMOBILE'    
 ELSE 'OTHER' END AS MOTORCYCLE_TYPE_DESC --OTHER     
    
  FROM POL_VEHICLES                            
  INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_VEHICLES.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                            
  AND POL_VEHICLES.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID                             
  AND POL_VEHICLES.POLICY_VERSION_ID = POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID                             
  INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                         
  = APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID             
  AND POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER =                         
  LTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                        
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                              
  AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=                        
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                        
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                        
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                        
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                        
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                        
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))           
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST S ON        
 POL_VEHICLES.REGISTERED_STATE = S.STATE_ID                           
  WHERE APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID AND                         
  APP_UMBRELLA_UNDERLYING_POLICIES.APP_ID = @APP_ID                             
  AND APP_UMBRELLA_UNDERLYING_POLICIES.APP_VERSION_ID = @APP_VERSION_ID                             
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,@APP_VERSION_ID)=@APP_VERSION_ID                    
  AND POL_VEHICLES.IS_ACTIVE='Y'                           
                    
SELECT          
 INSURED_VEH_NUMBER,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,    
 REGISTERED_STATE,TERRITORY,CLASS,REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,    
 IS_NEW_USED,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,    
 AIR_BAG,ANTI_LOCK_BRAKES,DEACTIVATE_REACTIVATE_DATE,VEHICLE_CC,MOTORCYCLE_TYPE,USE_VEHICLE,    
 CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,UNINS_MOTOR_INJURY_COVE,UNINS_PROPERTY_DAMAGE_COVE,    
 UNDERINS_MOTOR_INJURY_COVE,SAFETY_BELT,MOTORCYCLE_TYPE_DESC        
  FROM #TEMP_VEHICLES                            
--  GROUP BY LOC_ADD1, LOC_ADD2  */   

SELECT t1.VEHICLE_ID,t1.INSURED_VEH_NUMBER,t1.VEHICLE_YEAR,t1.MAKE,t1.MODEL,
CASE WHEN t1.MOTORCYCLE_TYPE IS NULL THEN t1.VEHICLE_TYPE_PER ELSE t1.MOTORCYCLE_TYPE END AS VEHICLE_TYPE, 
LV.LOOKUP_VALUE_DESC AS MOTORCYCLE_TYPE_DESC
FROM APP_UMBRELLA_VEHICLE_INFO t1 
left join mnt_lookup_values l1 on t1.vehicle_type_per  = l1.lookup_unique_id 
LEFT OUTER JOIN MNT_LOOKUP_VALUES LV ON 
(CASE WHEN t1.MOTORCYCLE_TYPE IS NULL THEN t1.VEHICLE_TYPE_PER ELSE t1.MOTORCYCLE_TYPE END) = LV.LOOKUP_UNIQUE_ID 
LEFT OUTER JOIN APP_LIST AL ON  T1.CUSTOMER_ID = AL.CUSTOMER_ID AND T1.APP_ID = AL.APP_ID 
AND T1.APP_VERSION_ID = AL.APP_VERSION_ID                                               

WHERE t1.CUSTOMER_ID= @CUSTOMER_ID AND t1.APP_ID= @APP_ID AND t1.APP_VERSION_ID =@APP_VERSION_ID     
END    
    
    
    
  



GO

