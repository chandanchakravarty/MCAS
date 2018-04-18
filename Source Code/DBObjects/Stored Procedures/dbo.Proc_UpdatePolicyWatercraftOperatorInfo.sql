IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyWatercraftOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyWatercraftOperatorInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_UpdatePolicyWatercraftOperatorInfo                  
Created by      : Vijay Arora              
Date            : 24-11-2005              
Purpose        :update the policy watercraft operator information                  
Revison History :                  
Used In         : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/               
-- DROP PROC dbo.Proc_UpdatePolicyWatercraftOperatorInfo                     
CREATE PROC dbo.Proc_UpdatePolicyWatercraftOperatorInfo                  
(                  
@CUSTOMER_ID     int,                  
@POLICY_ID     int,                  
@POLICY_VERSION_ID     smallint,                  
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
@DRIVER_SSN     nvarchar(88),                  
@DRIVER_SEX   nchar(1),            
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
                  
--delete MVR Informaton  if driver name was changed                          
declare @OLD_DRIVER_FNAME VARCHAR(30)      
declare @OLD_DRIVER_MNAME VARCHAR(30)      
declare @OLD_DRIVER_LNAME VARCHAR(30)      
SELECT  @OLD_DRIVER_FNAME=DRIVER_FNAME,@OLD_DRIVER_MNAME=isnull(DRIVER_MNAME,''),@OLD_DRIVER_LNAME=DRIVER_LNAME       
   FROM POL_WATERCRAFT_DRIVER_DETAILS WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                          
   POLICY_ID   = @POLICY_ID AND  POLICY_VERSION_ID  = @POLICY_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID                   
if (@OLD_DRIVER_FNAME<>@DRIVER_FNAME or @OLD_DRIVER_MNAME<> isnull(@DRIVER_MNAME,'') or @OLD_DRIVER_LNAME<>@DRIVER_LNAME )      
   delete from POL_mvr_information WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                         
    POLICY_ID   = @POLICY_ID AND  POLICY_VERSION_ID  = @POLICY_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID       
--end here          
      
update  POL_WATERCRAFT_DRIVER_DETAILS                  
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
APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID,            
WAT_SAFETY_COURSE = @WAT_SAFETY_COURSE,                 
CERT_COAST_GUARD = @CERT_COAST_GUARD,          
APP_REC_VEHICLE_PRIN_OCC_ID=@APP_REC_VEHICLE_PRIN_OCC_ID,          
REC_VEH_ID=@REC_VEH_ID,        
DATE_ORDERED=@DATE_ORDERED,        
MVR_ORDERED=@MVR_ORDERED,        
VIOLATIONS=@VIOLATIONS,      
MARITAL_STATUS=@MARITAL_STATUS,    
MVR_CLASS = @MVR_CLASS,        
MVR_LIC_CLASS = @MVR_LIC_CLASS,        
MVR_LIC_RESTR = @MVR_LIC_RESTR,        
MVR_DRIV_LIC_APPL = @MVR_DRIV_LIC_APPL,  
MVR_REMARKS = @MVR_REMARKS,  
MVR_STATUS = @MVR_STATUS,
DRIVER_DRIV_TYPE = @DRIVER_DRIV_TYPE                                         
                              
where DRIVER_ID=@DRIVER_ID AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                  
END                
      
      
      
      
 




GO

