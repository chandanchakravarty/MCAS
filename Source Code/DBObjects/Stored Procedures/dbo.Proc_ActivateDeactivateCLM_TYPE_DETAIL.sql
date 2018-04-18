IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLM_TYPE_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLM_TYPE_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_ActivateDeactivateCLM_TYPE_DETAIL  
Created by      : Vijay Arora  
Date            : 5/18/2006  
Purpose    			: To activate/deactivate the values at table CLM_TYPE_DETAIL  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_ActivateDeactivateCLM_TYPE_DETAIL  
(  
 @DETAIL_TYPE_ID  int,
 @IS_ACTIVE char(1)  
)  
AS  
BEGIN  
	UPDATE CLM_TYPE_DETAIL SET IS_ACTIVE=@IS_ACTIVE WHERE DETAIL_TYPE_ID=@DETAIL_TYPE_ID
END  
  



GO

