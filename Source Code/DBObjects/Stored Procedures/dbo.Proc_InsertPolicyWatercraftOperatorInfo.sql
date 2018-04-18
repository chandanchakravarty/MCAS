IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyWatercraftOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyWatercraftOperatorInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_InsertPolicyWatercraftOperatorInfo                      
Created by      : Vijay Arora                
Date            : 24-11-2005                
Purpose        : Added the Policy WaterCraft Operator Information.                
Revison History :                      
Used In         : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
-- drop proc dbo.Proc_InsertPolicyWatercraftOperatorInfo                      
CREATE PROC dbo.Proc_InsertPolicyWatercraftOperatorInfo                      
(                      
@CUSTOMER_ID     int,                      
@POLICY_ID     int,                      
@POLICY_VERSION_ID     smallint,                      
@DRIVER_ID     smallint output,                      
@DRIVER_FNAME     nvarchar(150),                      
@DRIVER_MNAME     nvarchar(25),                      
@DRIVER_LNAME     nvarchar(150),                      
@DRIVER_CODE     nvarchar(40),                      
@DRIVER_SUFFIX     nvarchar(20),                      
@DRIVER_ADD1     nvarchar(140),                      
@DRIVER_ADD2     nvarchar(140),                      
@DRIVER_CITY     nvarchar(80),                      
@DRIVER_STATE     nvarchar(10),                      
@DRIVER_ZIP     varchar(11),                      
@DRIVER_COUNTRY     nchar(10),                      
@DRIVER_DOB     datetime =null,                      
@DRIVER_SSN     nvarchar(88),                      
@DRIVER_SEX  nchar(1),              
@DRIVER_DRIV_LIC     nvarchar(60),                      
@DRIVER_LIC_STATE     nvarchar(10),                      
@DRIVER_COST_GAURAD_AUX     int =null,                      
@CREATED_BY     int,                      
@CREATED_DATETIME     datetime =null,                      
@EXPERIENCE_CREDIT DECIMAL(9,2)=NULL,                      
@VEHICLE_ID INT=NULL,                      
@PERCENT_DRIVEN DECIMAL(9,2)=NULL,                    
@APP_VEHICLE_PRIN_OCC_ID  INT,              
@WAT_SAFETY_COURSE  INT,              
@CERT_COAST_GUARD  INT,            
@APP_REC_VEHICLE_PRIN_OCC_ID INT = NULL,            
@REC_VEH_ID SMALLINT = NULL,          
@DATE_ORDERED datetime=null,          
@MVR_ORDERED int=null,          
@VIOLATIONS  int=null,        
@MARITAL_STATUS nvarchar(1)=null,      
@MVR_CLASS varchar(50),          
@MVR_LIC_CLASS varchar(50),          
@MVR_LIC_RESTR varchar(50),          
@MVR_DRIV_LIC_APPL varchar(50),          
@MVR_REMARKS varchar(250),    
@MVR_STATUS varchar(10),  
@DRIVER_DRIV_TYPE int = null                                                          
          
)                      
AS                      
BEGIN                   
                
SELECT @DRIVER_ID=isnull(MAX(DRIVER_ID),0)+1                       
FROM POL_WATERCRAFT_DRIVER_DETAILS                       
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID ;                      
INSERT INTO POL_WATERCRAFT_DRIVER_DETAILS                      
(                      
CUSTOMER_ID,                      
POLICY_ID,                      
POLICY_VERSION_ID,                      
DRIVER_ID,                      
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
DRIVER_DOB,                      
DRIVER_SSN,                      
DRIVER_SEX,                      
DRIVER_DRIV_LIC,                      
DRIVER_LIC_STATE,                      
DRIVER_COST_GAURAD_AUX,                      
IS_ACTIVE,    
CREATED_BY,                      
CREATED_DATETIME,                      
EXPERIENCE_CREDIT,                      
VEHICLE_ID,                      
PERCENT_DRIVEN,                    
APP_VEHICLE_PRIN_OCC_ID,        
WAT_SAFETY_COURSE,              
CERT_COAST_GUARD,            
APP_REC_VEHICLE_PRIN_OCC_ID,            
REC_VEH_ID,          
DATE_ORDERED,          
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
)        
VALUES                      
(                      
@CUSTOMER_ID,                      
@POLICY_ID,                      
@POLICY_VERSION_ID,                      
@DRIVER_ID,                      
@DRIVER_FNAME,                      
@DRIVER_MNAME,                      
@DRIVER_LNAME,                      
@DRIVER_CODE,                      
@DRIVER_SUFFIX,                      
@DRIVER_ADD1,                      
@DRIVER_ADD2,                      
@DRIVER_CITY,                      
@DRIVER_STATE,                      
@DRIVER_ZIP,                      
@DRIVER_COUNTRY,                      
@DRIVER_DOB,                      
@DRIVER_SSN,                      
@DRIVER_SEX,                      
@DRIVER_DRIV_LIC,                      
@DRIVER_LIC_STATE,                      
@DRIVER_COST_GAURAD_AUX,                      
'Y',            
@CREATED_BY,                      
@CREATED_DATETIME,                      
@EXPERIENCE_CREDIT ,                      
@VEHICLE_ID,                      
@PERCENT_DRIVEN,                    
@APP_VEHICLE_PRIN_OCC_ID,              
@WAT_SAFETY_COURSE,              
@CERT_COAST_GUARD,            
@APP_REC_VEHICLE_PRIN_OCC_ID,            
@REC_VEH_ID,          
@DATE_ORDERED,          
@MVR_ORDERED,          
@VIOLATIONS,        
@MARITAL_STATUS,      
@MVR_CLASS,          
@MVR_LIC_CLASS,          
@MVR_LIC_RESTR,          
@MVR_DRIV_LIC_APPL,    
@MVR_REMARKS,    
@MVR_STATUS,  
@DRIVER_DRIV_TYPE  
)                      
END                      
        
        
      
    
  
  


GO

