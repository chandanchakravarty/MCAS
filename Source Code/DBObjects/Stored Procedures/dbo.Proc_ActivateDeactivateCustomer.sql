IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateCustomer
Created by      : Vijay    
Date            : 25 Mar,2005    
Purpose         : To Activate/Deactivate the record in Customer table    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_ActivateDeactivateCustomer
(    
 	@CODE     	numeric(9),    
 	@IS_ACTIVE  	Char(1)    
)    
AS    
BEGIN    
	UPDATE CLT_CUSTOMER_LIST    
	SET     
  		Is_Active 	= @IS_ACTIVE   
	WHERE    
    		CUSTOMER_ID   	= @CODE    
    
END


GO

