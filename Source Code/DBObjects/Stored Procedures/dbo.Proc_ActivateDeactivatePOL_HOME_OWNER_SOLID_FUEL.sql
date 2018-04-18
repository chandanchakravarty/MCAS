IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePOL_HOME_OWNER_SOLID_FUEL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePOL_HOME_OWNER_SOLID_FUEL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : Dbo.Proc_ActivateDeactivatePOL_HOME_OWNER_SOLID_FUEL
Created by      : Anurag Verma   
Date            : 11/18/2005      
Purpose         : Activate/ Deactivate of data in table POL_HOME_OWNER_SOLID_FUEL
Revison History :      
Used In                   : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_ActivateDeactivatePOL_HOME_OWNER_SOLID_FUEL
(      
 @FUEL_ID INT,
 @IS_ACTIVE      nchar(1)   ,
 @CUSTOMER_ID  int,
 @POL_ID INT,
 @POL_VERSION_ID INT
)      
AS      
BEGIN      
      
 UPDATE  POL_HOME_OWNER_SOLID_FUEL
 SET        
  IS_ACTIVE   =  @IS_ACTIVE     
 WHERE          
 FUEL_ID = @FUEL_ID AND
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID 
END






GO

