IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateTaxEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateTaxEntity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateTaxEntity
Created by      : Priya  
Date            : 14 Apr,2005    
Purpose         : To Activate/Deactivate the record in Tax Entity table    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_ActivateDeactivateTaxEntity
(    
 	@Code     	numeric(4),    
 	@IS_ACTIVE  	Char(1)    
)    
AS    
BEGIN    
	UPDATE MNT_TAX_ENTITY_LIST
	SET     
  		Is_Active 	= @IS_ACTIVE   
	WHERE    
    		TAX_ID   	= @CODE    
    
END





GO

