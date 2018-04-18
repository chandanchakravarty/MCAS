IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_TYPE_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_TYPE_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_DeleteCLM_TYPE_DETAIL  
Created by      : Vijay Arora  
Date            : 4/20/2006  
Purpose     : To Delete the Record from CLM_TYPE_DETAIL  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_DeleteCLM_TYPE_DETAIL  
(  
 @DETAIL_TYPE_ID int  
)  
AS  
BEGIN  
	IF EXISTS (SELECT DETAIL_TYPE_ID FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID=@DETAIL_TYPE_ID)
		 DELETE FROM CLM_TYPE_DETAIL WHERE DETAIL_TYPE_ID=@DETAIL_TYPE_ID   
END  


GO

