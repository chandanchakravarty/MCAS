IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateACT_BUDGET_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateACT_BUDGET_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_ActivateDeactivateACT_BUDGET_CATEGORY  
Created by      : Vijay Arora  
Date            : Oct. 04 2005  
Purpose         : To update the record in ACT_BUDGET_CATEGORY table  
Revison History :  
Used In         : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
create PROC dbo.Proc_ActivateDeactivateACT_BUDGET_CATEGORY  
(  
@CODE int,  
@IS_ACTIVE  char(1)     
)  
AS  
BEGIN  
UPDATE ACT_BUDGET_CATEGORY  
 SET IS_ACTIVE = @IS_ACTIVE  
 WHERE CATEGEORY_ID = @CODE  
END



GO

