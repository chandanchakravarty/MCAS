IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyWatercraftDriverInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyWatercraftDriverInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name        : dbo.Proc_GetPolicyWatercraftDriverInfo                  
Created by        : Vijay Arora              
Date              : 24-11-2005              
Purpose         : Retrieving data from POL_WATERCRAFT_DRIVER_INFO                  
Revison History  :                  
Used In          : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--drop proc Proc_GetPolicyWatercraftDriverInfo              
CREATE PROC dbo.Proc_GetPolicyWatercraftDriverInfo                  
@CUSTOMER_ID INT,                  
@POLICY_ID INT,                  
@POLICY_VERSION_ID INT,                  
@DRIVER_ID INT                  
AS                  
BEGIN                  
SELECT                  
DRIVER_FNAME,                  
DRIVER_MNAME,                  
DRIVER_LNAME,                  
DRIVER_CODE,                  
DRIVER_SUFFIX,                  
DRIVER_ADD1,                  
DRIVER_ADD2,                  
DRIVER_CITY,                  
DRIVER_STATE,                  
DRIVER_ZIP,                  
DRIVER_COUNTRY,                  
CONVERT(VARCHAR(10),DRIVER_DOB,101) AS DRIVER_DOB,                  
DRIVER_SSN,                  
DRIVER_SEX,                  
DRIVER_DRIV_LIC,                  
DRIVER_LIC_STATE,                  
DRIVER_COST_GAURAD_AUX,                  
VEHICLE_ID,              
IS_ACTIVE,                  
CEILING(PERCENT_DRIVEN)PERCENT_DRIVEN,                
ISNULL(APP_VEHICLE_PRIN_OCC_ID,0) AS APP_VEHICLE_PRIN_OCC_ID,              
WAT_SAFETY_COURSE,              
CERT_COAST_GUARD,            
ISNULL(APP_REC_VEHICLE_PRIN_OCC_ID,0) AS APP_REC_VEHICLE_PRIN_OCC_ID,            
REC_VEH_ID,          
CONVERT(VARCHAR(10),DATE_ORDERED,101) AS DATE_ORDERED,                  
MVR_ORDERED,          
VIOLATIONS,      
MARITAL_STATUS,    
MVR_CLASS,      
MVR_LIC_CLASS,      
MVR_LIC_RESTR,      
MVR_DRIV_LIC_APPL,     
MVR_REMARKS,  
MVR_STATUS,
DRIVER_DRIV_TYPE
                                          
  
FROM POL_WATERCRAFT_DRIVER_DETAILS                   
WHERE POLICY_ID=@POLICY_ID AND                   
POLICY_VERSION_ID=@POLICY_VERSION_ID                  
AND CUSTOMER_ID=@CUSTOMER_ID AND                  
DRIVER_ID=@DRIVER_ID                  
END                
        
      
    
  



GO

