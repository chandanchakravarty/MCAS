IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_EXPERT_SERVICE_PROVIDERS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_EXPERT_SERVICE_PROVIDERS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
                  
Proc Name       : Proc_DeleteCLM_EXPERT_SERVICE_PROVIDERS  
Created by      : Sumit Chhabra  
Date            : 21/04/2006                  
Purpose         : Delete Expert Service Provider data from CLM_EXPERT_SERVICE_PROVIDERS  
Revison History :                  
Used In                   : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE PROC Dbo.Proc_DeleteCLM_EXPERT_SERVICE_PROVIDERS                  
(                  
 @EXPERT_SERVICE_ID int              
)                  
AS                  
BEGIN                
	DELETE FROM CLM_EXPERT_SERVICE_PROVIDERS  WHERE EXPERT_SERVICE_ID=@EXPERT_SERVICE_ID
  
   
END        



GO

