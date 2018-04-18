IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAppSolidFuel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAppSolidFuel]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
          
Proc Name       : Proc_DeleteAppSolidFuel          
Created by      : Swastika Gaur          
Date            : 5th Apr'06          
Purpose         : Delete Solid Fuel Info, Fire Protection, Chimney/Stove Details          
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Dbo.Proc_DeleteAppSolidFuel          
(          
 @CUSTOMER_ID INT,    
 @APP_ID INT,    
 @APP_VERSION_ID INT,    
 @FUEL_ID smallint   
  
)          
AS          
BEGIN     
     
 
 -- Delete Chimney/Stovepipe Info
 DELETE FROM APP_HOME_OWNER_CHIMNEY_STOVE
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND FUEL_ID=@FUEL_ID 

 -- Delete Fire Protection Info
 DELETE FROM APP_HOME_OWNER_FIRE_PROT_CLEAN
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND FUEL_ID=@FUEL_ID 

-- Delete Solid Fuel Info
 DELETE FROM APP_HOME_OWNER_SOLID_FUEL
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND FUEL_ID=@FUEL_ID 


 
END         
      
   




GO

