IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAssignedDriversForVehicleForApp_Motor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAssignedDriversForVehicleForApp_Motor]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                  
Proc Name   : dbo.Proc_GetAssignedDriversForVehicleForApp                  
Created by  : Praveen Kasana                        
Date        : 1 may,2009                        
Purpose     : Get the Assigned Driver ID for a vehicle for application                  
                                     
 ------------------------------------------------------------                                              
Date     Review By          Comments                                            
                                   
------   ------------       -------------------------*/    
--drop proc dbo.Proc_GetAssignedDriversForVehicleForApp_Motor 197,85,1                                
CREATE PROCEDURE [dbo].Proc_GetAssignedDriversForVehicleForApp_Motor                       
(                        
 @CUSTOMER_ID int,                        
 @APP_ID int,                        
 @APP_VERSION_ID int                        
)                            
AS                                 
BEGIN   
DECLARE @STATE SMALLINT  
DECLARE @APPEFFECTIVEDATE DATETIME  
                                 
  SELECT @STATE=STATE_ID,@APPEFFECTIVEDATE=APP_EFFECTIVE_DATE FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
   
IF (@STATE=14)  
BEGIN     
 SELECT                   
  A.DRIVER_ID, A.VEHICLE_ID,             
  SUBSTRING(V.LOOKUP_VALUE_CODE,charindex('^',V.LOOKUP_VALUE_CODE)+1, len(V.LOOKUP_VALUE_CODE)) AS VEHICLEDRIVEDAS,  
  dbo.piece(V.LOOKUP_VALUE_CODE,'^',1) AS VEHICLEDRIVEDASCODE,                     
 --dbo.piece(DATEDIFF(DAY,D.DRIVER_DOB,@APPEFFECTIVEDATE)/365.2425,'.',1) AS DRIVER_AGE,    
	dbo.GetAge(D.DRIVER_DOB,@APPEFFECTIVEDATE) AS DRIVER_AGE,      
 convert(char,D.DRIVER_DOB,101) BIRTHDATE,        
 CASE(D.DRIVER_SEX)        
  WHEN 'M' THEN 'MALE'        
  WHEN 'F' THEN 'FEMALE'        
  ELSE ''        
 END AS GENDER        
  
 FROM                   
  APP_DRIVER_DETAILS D  WITH (NOLOCK)                       
 JOIN                   
  APP_DRIVER_ASSIGNED_VEHICLE A WITH (NOLOCK)                       
 ON                   
  A.CUSTOMER_ID = D.CUSTOMER_ID AND                  
  A.APP_ID = D.APP_ID AND                  
  A.APP_VERSION_ID = D.APP_VERSION_ID AND                  
  A.DRIVER_ID = D.DRIVER_ID                  
 LEFT OUTER JOIN                
 MNT_LOOKUP_VALUES V  WITH (NOLOCK)                      
 ON                
 A.APP_VEHICLE_PRIN_OCC_ID = V.LOOKUP_UNIQUE_ID       
      
 WHERE                  
  D.CUSTOMER_ID = @CUSTOMER_ID AND                  
  D.APP_ID = @APP_ID AND                  
  D.APP_VERSION_ID = @APP_VERSION_ID AND                  
  ISNULL(D.IS_ACTIVE,'Y')='Y'--  AND  
  --D.DRIVER_DRIV_TYPE = 11603 --Driver Type is Licensed      
  and ( A.APP_VEHICLE_PRIN_OCC_ID not in (11925,11926,11931)  )    
  
 END  
ELSE  
BEGIN  
SELECT                   
  A.DRIVER_ID, A.VEHICLE_ID,             
  SUBSTRING(V.LOOKUP_VALUE_CODE,charindex('^',V.LOOKUP_VALUE_CODE)+1, len(V.LOOKUP_VALUE_CODE)) AS VEHICLEDRIVEDAS,    
  dbo.piece(V.LOOKUP_VALUE_CODE,'^',1) AS VEHICLEDRIVEDASCODE,           
 -- dbo.piece(DATEDIFF(DAY,D.DRIVER_DOB,@APPEFFECTIVEDATE)/365.2425,'.',1) AS DRIVER_AGE,          
	dbo.GetAge(DRIVER_DOB,@APPEFFECTIVEDATE) AS DRIVER_AGE,
  --dbo.GetAge(DRIVER_DOB,GetDate()) as DRIVER_AGE,  
  convert(char,D.DRIVER_DOB,101) BIRTHDATE,        
 CASE(D.DRIVER_SEX)        
  WHEN 'M' THEN 'MALE'        
  WHEN 'F' THEN 'FEMALE'        
  ELSE ''        
 END AS GENDER      
  
 FROM                   
  APP_DRIVER_DETAILS D  WITH (NOLOCK)                       
 JOIN                   
  APP_DRIVER_ASSIGNED_VEHICLE A WITH (NOLOCK)                       
 ON                   
  A.CUSTOMER_ID = D.CUSTOMER_ID AND                  
  A.APP_ID = D.APP_ID AND                  
  A.APP_VERSION_ID = D.APP_VERSION_ID AND                  
  A.DRIVER_ID = D.DRIVER_ID                  
 LEFT OUTER JOIN                
 MNT_LOOKUP_VALUES V  WITH (NOLOCK)                      
 ON                
 A.APP_VEHICLE_PRIN_OCC_ID = V.LOOKUP_UNIQUE_ID             
WHERE                  
  D.CUSTOMER_ID = @CUSTOMER_ID AND                  
  D.APP_ID = @APP_ID AND                  
  D.APP_VERSION_ID = @APP_VERSION_ID AND                  
  ISNULL(D.IS_ACTIVE,'Y')='Y'  --AND  
  --D.DRIVER_DRIV_TYPE = 11603 --Driver Type is Licensed    
 and ( A.APP_VEHICLE_PRIN_OCC_ID not in (11925,11926,11931))     
END                 
End    
  

  
  


GO

