IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAPP_PRIOR_LOSS_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAPP_PRIOR_LOSS_INFO]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------    
Proc Name       : dbo.Proc_ActivateDeactivateAPP_PRIOR_LOSS_INFO
Created by      : ANURAG VERMA  
Date            :     28/4/2005    
Purpose         : To Activate/Deactivate the record in APP_PRIOR_LOSS_INFO table    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_ActivateDeactivateAPP_PRIOR_LOSS_INFO
(    
 	@CODE     	numeric(9),    
 	@IS_ACTIVE  	Char(1)    
)    
AS    
BEGIN    
	UPDATE APP_PRIOR_LOSS_INFO
	SET     
  		Is_Active 	= @IS_ACTIVE   
	WHERE    
    		LOSS_ID   	= @CODE    
    
END


GO

