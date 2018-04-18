IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_HOME_OWNER_CHIMNEY_STOVE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_HOME_OWNER_CHIMNEY_STOVE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_Get_POL_HOME_OWNER_CHIMNEY_STOVE    
Created by      : Vijay Arora
Date            : 18-11-2005
Purpose     	: Fetch records in APP_HOME_OWNER_CHIMNEY_STOVE    
Revison History :    
Used In  	: Wolverine
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_Get_POL_HOME_OWNER_CHIMNEY_STOVE    
(    
 @CUSTOMER_ID     int,    
 @POLICY_ID     int,    
 @POLICY_VERSION_ID     smallint,    
 @FUEL_ID     smallint    
)    
AS    
BEGIN    
 SELECT     
  CUSTOMER_ID,    
  POLICY_ID,    
  POLICY_VERSION_ID,    
  FUEL_ID,    
  IS_STOVE_VENTED,    
  OTHER_DEVICES_ATTACHED,    
  CHIMNEY_CONSTRUCTION,    
  CONSTRUCT_OTHER_DESC,    
  IS_TILE_FLUE_LINING,    
  IS_CHIMNEY_GROUND_UP,    
  CHIMNEY_INST_AFTER_HOUSE_BLT,    
  IS_CHIMNEY_COVERED,    
  DIST_FROM_SMOKE_PIPE,    
  THIMBLE_OR_MATERIAL,    
  STOVE_PIPE_IS,    
  DOES_SMOKE_PIPE_FIT,    
  SMOKE_PIPE_WASTE_HEAT,    
  STOVE_CONN_SECURE,    
  SMOKE_PIPE_PASS,    
  SELECT_PASS,    
  PASS_INCHES,  
  IS_ACTIVE   
 FROM      
  POL_HOME_OWNER_CHIMNEY_STOVE     
 WHERE     
  CUSTOMER_ID  = @CUSTOMER_ID AND    
  POLICY_ID  = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  FUEL_ID  = @FUEL_ID    
END    
    
    
  



GO

