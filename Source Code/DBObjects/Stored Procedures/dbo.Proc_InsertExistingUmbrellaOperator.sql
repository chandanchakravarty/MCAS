IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertExistingUmbrellaOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertExistingUmbrellaOperator]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name          : Dbo.Proc_InsertExistingUmbrellaOperator        
Created by           : Shafi  
Date                    : 12/13/2005        
Purpose               :   Insert Operator and Generate Operator Code      
Revison History :        
Used In                :   Wolverine          
        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE  PROCEDURE Proc_InsertExistingUmbrellaOperator        
(        
 @TO_CUSTOMER_ID int,        
 @TO_APP_ID int,        
 @TO_APP_VERSION_ID int,        
 @FROM_CUSTOMER_ID int,        
 @FROM_APP_ID int,        
 @FROM_APP_VERSION_ID int,        
 @FROM_DRIVER_ID int,        
 @CREATED_BY_USER_ID  int           
)        
AS        
BEGIN        
Declare @To_Driver_Id int            
SELECT  @To_Driver_Id = ISNULL(MAX(DRIVER_ID),0) + 1        
FROM     APP_UMBRELLA_OPERATOR_INFO         
WHERE  CUSTOMER_ID=@TO_CUSTOMER_ID        
AND         APP_ID=@TO_APP_ID        
AND         APP_VERSION_ID=@TO_APP_VERSION_ID           
        
        
DECLARE @DRIVERCODE VARCHAR(20)        
declare @LASTCHAR  VARCHAR(10)         
declare @FIRSTCHAR  VARCHAR(10)         
        
        
    
  
    
SET @DRIVERCODE=CONVERT(INT,RAND()*50)          
          
SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM APP_UMBRELLA_OPERATOR_INFO     
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID          
    AND         APP_ID=@FROM_APP_ID          
    AND         APP_VERSION_ID=@FROM_APP_VERSION_ID          
    AND         DRIVER_ID= @FROM_DRIVER_ID         
SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)     
    
if(@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_APP_ID=@FROM_APP_ID and  @TO_APP_VERSION_ID=@FROM_APP_VERSION_ID)
begin    
INSERT INTO APP_UMBRELLA_OPERATOR_INFO        
(        
CUSTOMER_ID, APP_ID,APP_VERSION_ID,  
Driver_Id,DRIVER_FNAME,DRIVER_MNAME,  
DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  
DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,  
DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,  
DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,  
EXPERIENCE_CREDIT,VEHICLE_ID,PERCENT_DRIVEN,APP_VEHICLE_PRIN_OCC_ID,  
IS_ACTIVE,CREATED_BY,CREATED_DATETIME    
  
  
  
  
      
)        
SELECT        
@TO_CUSTOMER_ID, @TO_APP_ID,@TO_APP_VERSION_ID,  
@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,  
DRIVER_LNAME,@DRIVERCODE,DRIVER_SUFFIX,  
DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,  
DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,  
DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,  
EXPERIENCE_CREDIT,VEHICLE_ID,PERCENT_DRIVEN,APP_VEHICLE_PRIN_OCC_ID,  
'Y',@CREATED_BY_USER_ID,GETDATE()        
      
FROM    APP_UMBRELLA_OPERATOR_INFO        
WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID        
AND         APP_ID=@FROM_APP_ID        
AND         APP_VERSION_ID=@FROM_APP_VERSION_ID        
AND         DRIVER_ID= @FROM_DRIVER_ID        
end
else
begin 

INSERT INTO APP_UMBRELLA_OPERATOR_INFO        
(        
CUSTOMER_ID, APP_ID,APP_VERSION_ID,  
Driver_Id,DRIVER_FNAME,DRIVER_MNAME,  
DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  
DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,  
DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,  
DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,  
EXPERIENCE_CREDIT,PERCENT_DRIVEN,
IS_ACTIVE,CREATED_BY,CREATED_DATETIME    
  
  
  
  
      
)        
SELECT        
@TO_CUSTOMER_ID, @TO_APP_ID,@TO_APP_VERSION_ID,  
@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,  
DRIVER_LNAME,@DRIVERCODE,DRIVER_SUFFIX,  
DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,  
DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,  
DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,  
DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,  
EXPERIENCE_CREDIT,PERCENT_DRIVEN,
'Y',@CREATED_BY_USER_ID,GETDATE()        
      
FROM    APP_UMBRELLA_OPERATOR_INFO        
WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID        
AND         APP_ID=@FROM_APP_ID        
AND         APP_VERSION_ID=@FROM_APP_VERSION_ID        
AND         DRIVER_ID= @FROM_DRIVER_ID        



end
END        
        
        
      
    
  
  



GO

