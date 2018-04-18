IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolSolidFuel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolSolidFuel]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
          
Proc Name       : Proc_DeletePolSolidFuel          
Created by      : Swastika Gaur          
Date            : 5th Apr'06          
Purpose         : Delete Solid Fuel Info, Fire Protection, Chimney/Stove Details          
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
CREATE PROC Dbo.Proc_DeletePolSolidFuel          
(          
 @CUSTOMER_ID INT,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID INT,    
 @FUEL_ID smallint   
  
)          
AS          
BEGIN     
     
 
 -- Delete Chimney/Stovepipe Info
 DELETE FROM POL_HOME_OWNER_CHIMNEY_STOVE
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND FUEL_ID=@FUEL_ID 

 -- Delete Fire Protection Info
 DELETE FROM POL_HOME_OWNER_FIRE_PROT_CLEAN
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND FUEL_ID=@FUEL_ID 

-- Delete Solid Fuel Info
 DELETE FROM POL_HOME_OWNER_SOLID_FUEL
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND FUEL_ID=@FUEL_ID 

 
END         


GO

