IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAssignedDriversForVehicleForApp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAssignedDriversForVehicleForApp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name   : dbo.Proc_GetAssignedDriversForVehicleForApp                
Created by  : nidhi                      
Date        : 07 October,2005                      
Purpose     : Get the Assigned Driver ID for a vehicle for application                

Modified by : Sumit Chhabra
Date        : 15 May,2007                      
Purpose     : Choose only those drivers that are licensed..Only licensed drivers are assigned the vehicle

Revison History  :                                      
 ------------------------------------------------------------                                            
Date     Review By          Comments                                          
                                 
------   ------------       -------------------------*/  
--drop proc dbo.Proc_GetAssignedDriversForVehicleForApp                               
CREATE PROCEDURE [dbo].[Proc_GetAssignedDriversForVehicleForApp]                     
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
--  dbo.GetAge(DRIVER_DOB,GetDate()) as DRIVER_AGE,
  convert(char,D.DRIVER_DOB,101) BIRTHDATE,      
 CASE(D.DRIVER_SEX)      
  WHEN 'M' THEN 'MALE'      
  WHEN 'F' THEN 'FEMALE'      
  ELSE ''      
 END AS GENDER,      
/* CASE(D.DRIVER_STUD_DIST_OVER_HUNDRED)    
  WHEN 1 THEN 'TRUE'    
  ELSE 'FALSE'    
 END AS COLLEGESTUDENT*/  
 CASE   
 WHEN (IN_MILITARY = 10963 OR  DRIVER_STUD_DIST_OVER_HUNDRED  =1) THEN 'TRUE'  
 ELSE 'FALSE'  
 END AS COLLEGESTUDENT, ISNULL(HAVE_CAR,0) AS HAVE_CAR
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
  ISNULL(D.IS_ACTIVE,'Y')='Y'  AND
  D.DRIVER_DRIV_TYPE = 11603 --Driver Type is Licensed    
  and ( A.APP_VEHICLE_PRIN_OCC_ID not in (11925,11926,11931)  )  

 END
ELSE
BEGIN
SELECT                 
  A.DRIVER_ID, A.VEHICLE_ID,           
  SUBSTRING(V.LOOKUP_VALUE_CODE,charindex('^',V.LOOKUP_VALUE_CODE)+1, len(V.LOOKUP_VALUE_CODE)) AS VEHICLEDRIVEDAS,  
  dbo.piece(V.LOOKUP_VALUE_CODE,'^',1) AS VEHICLEDRIVEDASCODE,         
 -- dbo.piece(DATEDIFF(DAY,D.DRIVER_DOB,@APPEFFECTIVEDATE)/365.2425,'.',1) AS DRIVER_AGE,        
	dbo.GetAge(D.DRIVER_DOB,@APPEFFECTIVEDATE) AS DRIVER_AGE,
  --dbo.GetAge(DRIVER_DOB,GetDate()) as DRIVER_AGE,
  convert(char,D.DRIVER_DOB,101) BIRTHDATE,      
 CASE(D.DRIVER_SEX)      
  WHEN 'M' THEN 'MALE'      
  WHEN 'F' THEN 'FEMALE'      
  ELSE ''      
 END AS GENDER,      
/* CASE(D.DRIVER_STUD_DIST_OVER_HUNDRED)    
  WHEN 1 THEN 'TRUE'    
  ELSE 'FALSE'    
 END AS COLLEGESTUDENT*/  
 CASE   
 WHEN (IN_MILITARY = 10963 OR  DRIVER_STUD_DIST_OVER_HUNDRED  =1) THEN 'TRUE'  
 ELSE 'FALSE'  
 END AS COLLEGESTUDENT, ISNULL(HAVE_CAR,0) AS HAVE_CAR
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
  ISNULL(D.IS_ACTIVE,'Y')='Y'  AND
  D.DRIVER_DRIV_TYPE = 11603 --Driver Type is Licensed  
 and ( A.APP_VEHICLE_PRIN_OCC_ID not in (11925,11926,11931)  )   
END               
End  


GO

