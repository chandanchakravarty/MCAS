IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAssignedDriversForVehicleForPol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAssignedDriversForVehicleForPol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Proc_GetAssignedDriversForVehicleForPol]
(                      
 @CUSTOMER_ID int,                      
 @POLICY_ID int,                      
 @POLICY_VERSION_ID int                      
)                          
AS                               
BEGIN                                
  DECLARE @STATE SMALLINT 
  DECLARE @APPEFFECTIVEDATE DATETIME 
                   
SELECT @STATE = STATE_ID, @APPEFFECTIVEDATE=APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
IF (@STATE=14)
BEGIN
 SELECT                 
  A.DRIVER_ID, A.VEHICLE_ID,           
  SUBSTRING(V.LOOKUP_VALUE_CODE,charindex('^',V.LOOKUP_VALUE_CODE)+1, len(V.LOOKUP_VALUE_CODE)) AS VEHICLEDRIVEDAS,  
	dbo.piece(V.LOOKUP_VALUE_CODE,'^',1) AS VEHICLEDRIVEDASCODE,                           
--  YEAR(GETDATE())-YEAR(D.DRIVER_DOB) AS DRIVER_AGE,        
	--dbo.piece(DATEDIFF(DAY,D.DRIVER_DOB,@APPEFFECTIVEDATE)/365.2425,'.',1)  AS DRIVER_AGE,
	dbo.GetAge(D.DRIVER_DOB,@APPEFFECTIVEDATE) AS DRIVER_AGE,
--dbo.GetAge(DRIVER_DOB,GetDate())  AS DRIVER_AGE,
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
 END AS COLLEGESTUDENT,
 ISNULL(HAVE_CAR,0) AS HAVE_CAR  
 FROM                 
  POL_DRIVER_DETAILS D  with(nolock)               
 JOIN                 
  POL_DRIVER_ASSIGNED_VEHICLE A  with(nolock)                     
 ON                 
  A.CUSTOMER_ID = D.CUSTOMER_ID AND                
  A.POLICY_ID = D.POLICY_ID AND                
  A.POLICY_VERSION_ID = D.POLICY_VERSION_ID AND                
  A.DRIVER_ID = D.DRIVER_ID                
 LEFT OUTER JOIN              
 MNT_LOOKUP_VALUES V  with(nolock)                   
 ON              
 A.APP_VEHICLE_PRIN_OCC_ID = V.LOOKUP_UNIQUE_ID              
 WHERE                
  D.CUSTOMER_ID = @CUSTOMER_ID AND                
  D.POLICY_ID = @POLICY_ID AND                
  D.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                
  ISNULL(D.IS_ACTIVE,'Y')='Y' AND
  D.DRIVER_DRIV_TYPE=11603--Licensed Driver Type
   and ( A.APP_VEHICLE_PRIN_OCC_ID not in (11925,11926,11931)  )  
 END 
ELSE
BEGIN 

 SELECT                 
  A.DRIVER_ID, A.VEHICLE_ID,           
  SUBSTRING(V.LOOKUP_VALUE_CODE,charindex('^',V.LOOKUP_VALUE_CODE)+1, len(V.LOOKUP_VALUE_CODE)) AS VEHICLEDRIVEDAS,  
dbo.piece(V.LOOKUP_VALUE_CODE,'^',1) AS VEHICLEDRIVEDASCODE,                 
--  YEAR(GETDATE())-YEAR(D.DRIVER_DOB) AS DRIVER_AGE,        
 -- dbo.GetAge(DRIVER_DOB,GetDate())  AS DRIVER_AGE,
--dbo.piece(DATEDIFF(DAY,D.DRIVER_DOB,@APPEFFECTIVEDATE)/365.2425,'.',1)  AS DRIVER_AGE,
dbo.GetAge(D.DRIVER_DOB,@APPEFFECTIVEDATE) AS DRIVER_AGE,
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
 END AS COLLEGESTUDENT,
 ISNULL(HAVE_CAR,0) AS HAVE_CAR  
 FROM                 
  POL_DRIVER_DETAILS D  with(nolock)               
 JOIN                 
  POL_DRIVER_ASSIGNED_VEHICLE A  with(nolock)                     
 ON                 
  A.CUSTOMER_ID = D.CUSTOMER_ID AND                
  A.POLICY_ID = D.POLICY_ID AND                
  A.POLICY_VERSION_ID = D.POLICY_VERSION_ID AND                
  A.DRIVER_ID = D.DRIVER_ID                
 LEFT OUTER JOIN              
 MNT_LOOKUP_VALUES V  with(nolock)                   
 ON              
 A.APP_VEHICLE_PRIN_OCC_ID = V.LOOKUP_UNIQUE_ID              
 WHERE                
  D.CUSTOMER_ID = @CUSTOMER_ID AND                
  D.POLICY_ID = @POLICY_ID AND                
  D.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                
  ISNULL(D.IS_ACTIVE,'Y')='Y' AND
  D.DRIVER_DRIV_TYPE=11603--Licensed Driver Type
	and ( A.APP_VEHICLE_PRIN_OCC_ID not in (11925,11926,11931)  )   
END               
End













GO

