IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUmbrellaOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUmbrellaOperatorInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_UpdateUmbrellaOperatorInfo      
Created by      : Sumit Chhabra  
Date            : 24/10/2005      
Purpose        :update the umbrella operator information      
    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_UpdateUmbrellaOperatorInfo      
(      
@CUSTOMER_ID     int,      
@APP_ID     int,      
@APP_VERSION_ID     smallint,      
@DRIVER_ID     smallint ,      
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
@DRIVER_SEX   nchar(2),
@DRIVER_DRIV_LIC     nvarchar(60),      
@DRIVER_LIC_STATE     nvarchar(10),      
@DRIVER_COST_GAURAD_AUX     int =null,      
@MODIFIED_BY     int,      
@LAST_UPDATED_DATETIME    datetime =null,      
@EXPERIENCE_CREDIT decimal(9,2),      
@vehicle_id int,      
@percent_driven decimal(9,2),    
@APP_VEHICLE_PRIN_OCC_ID  INT    
)      
AS      
BEGIN      
      
update  APP_UMBRELLA_OPERATOR_INFO      
set      
      
DRIVER_FNAME=@DRIVER_FNAME,      
DRIVER_MNAME=@DRIVER_MNAME,      
DRIVER_LNAME=@DRIVER_LNAME,      
DRIVER_CODE=@DRIVER_CODE,      
DRIVER_SUFFIX=@DRIVER_SUFFIX,      
DRIVER_ADD1=@DRIVER_ADD1,      
DRIVER_ADD2=@DRIVER_ADD2,      
DRIVER_CITY=@DRIVER_CITY,      
DRIVER_STATE=@DRIVER_STATE,      
DRIVER_ZIP=@DRIVER_ZIP,      
DRIVER_COUNTRY=@DRIVER_COUNTRY,      
DRIVER_DOB=@DRIVER_DOB,      
DRIVER_SSN=@DRIVER_SSN,      
DRIVER_SEX=@DRIVER_SEX,      
DRIVER_DRIV_LIC=@DRIVER_DRIV_LIC,      
DRIVER_LIC_STATE=@DRIVER_LIC_STATE,      
DRIVER_COST_GAURAD_AUX=@DRIVER_COST_GAURAD_AUX,      
MODIFIED_BY=@MODIFIED_BY,      
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME   ,      
EXPERIENCE_CREDIT=@EXPERIENCE_CREDIT ,      
vehicle_id=@vehicle_id,      
percent_driven=@percent_driven,    
APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID      
      
where DRIVER_ID=@DRIVER_ID AND CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID      
END    
    
  



GO

