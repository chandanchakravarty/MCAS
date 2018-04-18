IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriversForPolicyMVR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriversForPolicyMVR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*********************************************************************      
Proc Name           : Dbo.Proc_GetDriversForPolicyMVR      
Created by          : Anurag Verma           
Date                    : 08/11/2005            
Purpose                : To get the drivers details for policy MVR Information screen        
Revison History  :            
Used In                 :   Wolverine    
  
Modified By     : Shafi                                    
Modified Date   : 09-02-2006                                
Purpose         : Fetch Driver Details From Home  
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/   
--drop proc Proc_GetDriversForPolicyMVR          
          
CREATE PROC Dbo.Proc_GetDriversForPolicyMVR        
@CUSTOMERID  INT,            
@POLID  INT,            
@POLVERSIONID INT,           
@DRIVERID INT,      
@CALLEDFROM varchar(10)       
AS     
BEGIN            
 IF UPPER(@CALLEDFROM)='WAT' or UPPER(@CALLEDFROM)='HOME'      
  BEGIN      
    SELECT ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_MNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,        
     DRIVER_CODE FROM POL_WATERCRAFT_DRIVER_DETAILS        
     WHERE    CUSTOMER_ID = @CUSTOMERID   AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@POLVERSIONID     AND DRIVER_ID=@DRIVERID        
  END      
 ELSE IF UPPER(@CALLEDFROM)='PPA' OR UPPER(@CALLEDFROM)='MOT'      
  BEGIN      
    SELECT ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_MNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,        
    DRIVER_CODE FROM POL_DRIVER_DETAILS        
    WHERE    CUSTOMER_ID = @CUSTOMERID   AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@POLVERSIONID     AND DRIVER_ID=@DRIVERID        
  END     
	ELSE IF UPPER(@CALLEDFROM)='UMB'
  BEGIN      
    SELECT ISNULL(DRIVER_FNAME,'') + ' ' + ISNULL(DRIVER_MNAME,'') + ' ' + ISNULL(DRIVER_LNAME,'') AS DRIVER_NAME,        
    DRIVER_CODE FROM POL_UMBRELLA_DRIVER_DETAILS        
    WHERE    CUSTOMER_ID = @CUSTOMERID   AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@POLVERSIONID     AND DRIVER_ID=@DRIVERID        
  END       
 ELSE      
  RETURN -1        
END      
      


GO

