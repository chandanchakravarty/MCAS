IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriversForMVR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriversForMVR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*Proc Name           : Dbo.Proc_GetDriversForMVR                
Created by          : Sumit Chhabra               
Date                    : 26/10/2005                
Purpose                : To get the drivers details for MVR Information screen            
Modified by          : Sumit Chhabra               
Date                    : 10/11/2005                
Purpose                : Added the LOB motorcycle   and Homeowner  and umbrella    
Revison History  :                
Used In                 :   Wolverine                  
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
  
CREATE PROC Dbo.Proc_GetDriversForMVR            
@CUSTOMERID  INT,                
@APPID  INT,                
@APPVERSIONID INT,               
@DRIVERID INT,          
@CALLEDFROM varchar(10)  
--@OPERATOR INT  
AS                
BEGIN                
    
 IF UPPER(@CALLEDFROM)='WAT' or UPPER(@CALLEDFROM)='HOME'    
  BEGIN          
    SELECT ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_MNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,            
     DRIVER_CODE FROM APP_WATERCRAFT_DRIVER_DETAILS            
     WHERE    CUSTOMER_ID = @CUSTOMERID   AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID     AND DRIVER_ID=@DRIVERID            
  END          
 ELSE IF UPPER(@CALLEDFROM)='PPA' or UPPER(@CALLEDFROM)='MOT'        
  BEGIN          
    SELECT ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_MNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,            
    DRIVER_CODE FROM APP_DRIVER_DETAILS            
    WHERE    CUSTOMER_ID = @CUSTOMERID   AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID     AND DRIVER_ID=@DRIVERID            
  END     
 /*ELSE  IF (UPPER(@CALLEDFROM)='UMB' and @OPERATOR=1)  
   BEGIN          
    SELECT ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_MNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,            
    DRIVER_CODE FROM APP_UMBRELLA_OPERATOR_INFO            
    WHERE    CUSTOMER_ID = @CUSTOMERID   AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID     AND DRIVER_ID=@DRIVERID            
  END*/     
 ELSE  IF (UPPER(@CALLEDFROM)='UMB')  
   BEGIN          
    SELECT ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_MNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,            
    DRIVER_CODE FROM APP_UMBRELLA_DRIVER_DETAILS            
    WHERE    CUSTOMER_ID = @CUSTOMERID   AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID     AND DRIVER_ID=@DRIVERID            
  END     
 ELSE    
  RETURN -1            
END          
  


GO

