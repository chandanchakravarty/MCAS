IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUmbrellaOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUmbrellaOperatorInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_InsertUmbrellaOperatorInfo        
Created by      : Sumit Chhabra    
Date            : 24/10/2005        
Purpose       :Evaluation        
Modified by      : Sumit Chhabra    
Date            : 11/11/2005        
Purpose       :Check for duplicate record has been done away with  
Revison History :        
Used In        : Wolverine        
    
       
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_InsertUmbrellaOperatorInfo        
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
@DRIVER_DOB     datetime =null,        
@DRIVER_SSN     nvarchar(22),        
@DRIVER_SEX  nchar(2),
@DRIVER_DRIV_LIC     nvarchar(60),        
@DRIVER_LIC_STATE     nvarchar(10),        
@DRIVER_COST_GAURAD_AUX     int =null,        
@CREATED_BY     int,        
@CREATED_DATETIME     datetime =null,        
@EXPERIENCE_CREDIT DECIMAL(9,2)=NULL,        
@VEHICLE_ID INT=NULL,        
@PERCENT_DRIVEN DECIMAL(9,2)=NULL,      
@APP_VEHICLE_PRIN_OCC_ID  INT      
)        
AS        
BEGIN        
/*  
Check for duplicate record has been done away with  
If Not Exists        
(        
SELECT DRIVER_CODE FROM APP_UMBRELLA_OPERATOR_INFO         
WHERE DRIVER_CODE=@DRIVER_CODE        
AND CUSTOMER_ID = @CUSTOMER_ID AND        
  APP_ID = @APP_ID AND        
  APP_VERSION_ID = @APP_VERSION_ID)        
        
BEGIN*/  
select @DRIVER_ID=isnull(max(DRIVER_ID),0)+1         
from APP_UMBRELLA_OPERATOR_INFO         
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID ;        
INSERT INTO APP_UMBRELLA_OPERATOR_INFO        
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
APP_VEHICLE_PRIN_OCC_ID        
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
@APP_VEHICLE_PRIN_OCC_ID        
)        
END        
        
        
--END      
  



GO

