IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateACT_GL_ACCOUNTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateACT_GL_ACCOUNTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       :  dbo.Proc_ActivateDeactivateGLAccounts  
Created by      :  Ajit Singh Chahal  
Date            :  5/18/2005  
Purpose         :  To activate/deactivate the record in ACT_GL_ACCOUNTS table  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
create  PROC dbo.Proc_ActivateDeactivateACT_GL_ACCOUNTS  
(  
@CODE  int,  
@IS_ACTIVE  Char(1)     
)  
AS  
BEGIN  
UPDATE ACT_GL_ACCOUNTS  
 SET   
  IS_ACTIVE = @IS_ACTIVE  
 WHERE  
  ACCOUNT_ID = @CODE  
END  
  
  
  
  
  



GO

