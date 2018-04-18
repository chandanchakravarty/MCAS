IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLM_CATASTROPHE_EVENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLM_CATASTROPHE_EVENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : Dbo.Proc_ActivateDeactivateCLM_CATASTROPHE_EVENT        
Created by      : Sumit Chhabra              
Date            : 10/07/2006              
Purpose         : Activate/ Deactivate of data in table CLM_CATASTROPHE_EVENT        
Revison History :              
Used In                   : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC dbo.Proc_ActivateDeactivateCLM_CATASTROPHE_EVENT        
(        
 @CODE INT,    
 @IS_ACTIVE VARCHAR(2)
)              
AS              
BEGIN              
              
  UPDATE  CLM_CATASTROPHE_EVENT         
	  SET IS_ACTIVE=@IS_ACTIVE             
  	WHERE CATASTROPHE_EVENT_ID=@CODE   

END        
      
    
  



GO

