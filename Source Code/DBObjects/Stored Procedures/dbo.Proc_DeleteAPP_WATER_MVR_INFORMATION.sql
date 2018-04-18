IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAPP_WATER_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAPP_WATER_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
        
Proc Name       : Proc_DeleteAPP_WATER_MVR_INFORMATION        
Created by      : Sumit Chhabra        
Date            : 10/03/2005        
Purpose         : Delete of Driver APP_WATER_MVR_INFORMATION
Revison History :        
Used In                   : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_DeleteAPP_WATER_MVR_INFORMATION        
(        
 @CUSTOMER_ID INT,  
 @APP_ID INT,  
 @APP_VERSION_ID INT,  
 @DRIVER_ID INT,  
 @APP_WATER_MVR_ID INT  
)        
AS        
BEGIN        
 DELETE FROM APP_WATER_MVR_INFORMATION WHERE   
 CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND   
  DRIVER_ID=@DRIVER_ID AND APP_WATER_MVR_ID=@APP_WATER_MVR_ID    
END       
    
  



GO

