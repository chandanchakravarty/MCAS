IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactiveCLM_AUTHORITY_LIMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactiveCLM_AUTHORITY_LIMIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name       : Proc_ActivateDeactiveCLM_AUTHORITY_LIMIT      
Created by      : Vijay Arora   
Date            : 14-06-2006             
Purpose         : Activate Deactivate the record.
Revison History :                      
Used In         : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
CREATE PROC Dbo.Proc_ActivateDeactiveCLM_AUTHORITY_LIMIT                      
(                      
 @LIMIT_ID int,
 @IS_ACTIVE char(1)      
                  
)                      
AS                      
BEGIN       
            
 UPDATE CLM_AUTHORITY_LIMIT SET       
  IS_ACTIVE = @IS_ACTIVE
 WHERE      
  LIMIT_ID=@LIMIT_ID      
END            
      
      
    



GO

