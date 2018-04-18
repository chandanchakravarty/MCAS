IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateACT_REG_COMM_SETUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateACT_REG_COMM_SETUP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       :  dbo.Proc_ActivateDeactivateACT_REG_COMM_SETUP  
Created by      :  Ajit Singh Chahal  
Date            :  5/18/2005  
Purpose         :  To activate/deactivate the record in ACT_GL_ACCOUNTS table  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
create  PROC dbo.Proc_ActivateDeactivateACT_REG_COMM_SETUP  
(  
@CODE  int,  
@IS_ACTIVE  Char(1)     
)  
AS  
BEGIN  
UPDATE ACT_REG_COMM_SETUP  
 SET   
	IS_ACTIVE = @IS_ACTIVE  
 WHERE  
	COMM_ID = @CODE  
END  
  




GO

