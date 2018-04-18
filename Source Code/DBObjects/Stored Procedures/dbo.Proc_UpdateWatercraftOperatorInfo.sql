IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateWatercraftOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateWatercraftOperatorInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateWatercraftOperatorInfo              
Created by      : nid hi              
Date            : 5/18/2005              
Purpose       :update the watercraft operator information              
Revison History :              
Used In        : Wolverine              
              
Modified By : Anurag verma              
Modified On : 22/09/2005              
Purpose : Removing fields for title,marital_status,home_phone and driver_license and adding experience_credit field              
            
Modified By : Vijay Arora            
Modified On : 17-10-2005              
Purpose : Added Field named APP_VEHICLE_PRIN_OCC_ID            
            
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
 -- drop proc dbo.Proc_UpdateWatercraftOperatorInfo              
CREATE PROC dbo.Proc_UpdateWatercraftOperatorInfo              
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
@MARITAL_STATUS     nchar(1),              
@DRIVER_DOB     datetime =null,              
@VIOLATIONS     int,              
@MVR_ORDERED     int,              
@DATE_ORDERED     datetime =null,              
@DRIVER_SSN     nvarchar(88),              
@DRIVER_SEX   nchar(2),          
@DRIVER_DRIV_LIC     nvarchar(60),              
@DRIVER_LIC_STATE     nvarchar(10),              
@DRIVER_COST_GAURAD_AUX     int =null,              
@MODIFIED_BY     int,              
@LAST_UPDATED_DATETIME    datetime =null,              
@EXPERIENCE_CREDIT decimal(9,2),              
@vehicle_id int,              
@percent_driven decimal(9,2),            
@APP_VEHICLE_PRIN_OCC_ID  INT,          
@WAT_SAFETY_COURSE INT,          
@CERT_COAST_GUARD INT,        
@APP_REC_VEHICLE_PRIN_OCC_ID int = null,        
@REC_VEH_ID smallint = null,      
@MVR_CLASS varchar(50),        
@MVR_LIC_CLASS varchar(50),        
@MVR_LIC_RESTR varchar(50),        
@MVR_DRIV_LIC_APPL varchar(50),        
@MVR_REMARKS varchar(250),    
@MVR_STATUS varchar(10),  
@DRIVER_DRIV_TYPE int  = null                                                                  
          
)              
AS              
BEGIN              
--delete MVR Informaton  if driver name was changed                            
declare @OLD_DRIVER_FNAME VARCHAR(30)        
declare @OLD_DRIVER_MNAME VARCHAR(30)        
declare @OLD_DRIVER_LNAME VARCHAR(30)        
SELECT  @OLD_DRIVER_FNAME=DRIVER_FNAME,@OLD_DRIVER_MNAME=isnull(DRIVER_MNAME,''),@OLD_DRIVER_LNAME=DRIVER_LNAME         
   FROM APP_WATERCRAFT_DRIVER_DETAILS WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                            
    APP_ID   = @APP_ID AND  APP_VERSION_ID  = @APP_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID                      
if (@OLD_DRIVER_FNAME<>@DRIVER_FNAME or @OLD_DRIVER_MNAME<> isnull(@DRIVER_MNAME,'') or @OLD_DRIVER_LNAME<>@DRIVER_LNAME )        
   delete from app_mvr_information WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                           
    APP_ID   = @APP_ID AND  APP_VERSION_ID  = @APP_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID         
--end here            
              
update  APP_WATERCRAFT_DRIVER_DETAILS              
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
MARITAL_STATUS=@MARITAL_STATUS,        
VIOLATIONS=@VIOLATIONS,        
MVR_ORDERED=@MVR_ORDERED,        
DATE_ORDERED=@DATE_ORDERED,             
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
APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID,          
WAT_SAFETY_COURSE = @WAT_SAFETY_COURSE,          
CERT_COAST_GUARD = @CERT_COAST_GUARD,        
APP_REC_VEHICLE_PRIN_OCC_ID = @APP_REC_VEHICLE_PRIN_OCC_ID,        
REC_VEH_ID=@REC_VEH_ID,      
MVR_CLASS = @MVR_CLASS,        
MVR_LIC_CLASS = @MVR_LIC_CLASS,        
MVR_LIC_RESTR = @MVR_LIC_RESTR,        
MVR_DRIV_LIC_APPL = @MVR_DRIV_LIC_APPL,    
MVR_REMARKS = @MVR_REMARKS,    
MVR_STATUS = @MVR_STATUS,  
DRIVER_DRIV_TYPE =@DRIVER_DRIV_TYPE           
              
where DRIVER_ID=@DRIVER_ID AND CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID              
END     
  
  


GO

