IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePriorPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePriorPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivatePriorPolicy
Created by      : Vijay    
Date            : 29 April,2005    
Purpose         : To Activate/Deactivate the record of driver
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_ActivateDeactivatePriorPolicy
(    
 	@CODE     	numeric(9),    
 	@IS_ACTIVE  	Char(1)    
)    
AS    
BEGIN    
	UPDATE APP_PRIOR_CARRIER_INFO
	SET     
  		IS_ACTIVE 	= @IS_ACTIVE   
	WHERE    
    		APP_PRIOR_CARRIER_INFO_ID   	= @CODE
    
END





GO

