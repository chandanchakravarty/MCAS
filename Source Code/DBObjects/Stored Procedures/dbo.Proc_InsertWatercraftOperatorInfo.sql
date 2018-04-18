IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertWatercraftOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertWatercraftOperatorInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.WatercraftOperatorInfo                
Created by      : nid hi                
Date            : 5/18/2005                
Purpose       :Evaluation                
Revison History :                
Used In        : Wolverine                
                
Modified By : Anurag verma                
Modified On : 22/09/2005                
Purpose : Merging screen of watercraft driver details screen with driver discount and assigned vehicle               
              
Modified By : Vijay Arora              
Modified On : 17-10-2005              
Purpose : Added field named APP_VEHICLE_PRIN_OCC_ID              
            
Modified By : Sumit Chhabra            
Modified On : 11-11-2005              
Purpose : Check for duplicate driver code has been done away with            
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_InsertWatercraftOperatorInfo                
CREATE PROC dbo.Proc_InsertWatercraftOperatorInfo                
(                
@CUSTOMER_ID     int,                
@APP_ID     int,                
@APP_VERSION_ID     smallint,                
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
@MARITAL_STATUS     nchar(1),                
@VIOLATIONS         int,                
@MVR_ORDERED         int,                
@DATE_ORDERED     datetime =null,                
@DRIVER_DOB     datetime =null,                
@DRIVER_SSN     nvarchar(88),                
@DRIVER_SEX   nchar(2),          
@DRIVER_DRIV_LIC     nvarchar(60),                
@DRIVER_LIC_STATE     nvarchar(10),                
@DRIVER_COST_GAURAD_AUX     int =null,                
@CREATED_BY     int,                
@CREATED_DATETIME     datetime =null,                
@EXPERIENCE_CREDIT DECIMAL(9,2)=NULL,                
@VEHICLE_ID INT=NULL,                
@PERCENT_DRIVEN DECIMAL(9,2)=NULL,              
@APP_VEHICLE_PRIN_OCC_ID  INT,          
@WAT_SAFETY_COURSE INT,          
@CERT_COAST_GUARD INT,        
@APP_REC_VEHICLE_PRIN_OCC_ID int=null,        
@REC_VEH_ID  smallint=null,      
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
/*            
Check for duplicate driver code has been done away with               
If Not Exists                
(                
SELECT DRIVER_CODE FROM APP_WATERCRAFT_DRIVER_DETAILS                 
WHERE DRIVER_CODE=@DRIVER_CODE                
AND CUSTOMER_ID = @CUSTOMER_ID AND                
  APP_ID = @APP_ID AND                
  APP_VERSION_ID = @APP_VERSION_ID)                
                
BEGIN*/            
select @DRIVER_ID=isnull(max(DRIVER_ID),0)+1                 
from APP_WATERCRAFT_DRIVER_DETAILS                 
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID ;                
INSERT INTO APP_WATERCRAFT_DRIVER_DETAILS                
(                
CUSTOMER_ID,                
APP_ID,                
APP_VERSION_ID,                
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
MARITAL_STATUS,        
VIOLATIONS,        
MVR_ORDERED,        
DATE_ORDERED,                
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
@APP_ID,                
@APP_VERSION_ID,                
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
@MARITAL_STATUS,        
@VIOLATIONS,        
@MVR_ORDERED,        
@DATE_ORDERED,                
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
@MVR_CLASS,        
@MVR_LIC_CLASS,        
@MVR_LIC_RESTR,        
@MVR_DRIV_LIC_APPL,    
@MVR_REMARKS,    
@MVR_STATUS,  
@DRIVER_DRIV_TYPE          
)                
END                
                
                
--END              
              
        
        
        
        
        
        
        
        
        
        
      
    
  
  


GO

