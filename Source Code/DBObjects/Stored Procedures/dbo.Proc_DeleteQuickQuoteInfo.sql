IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteQuickQuoteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteQuickQuoteInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_DeleteQuickQuoteInfo
Created by      : Deepak
Date            : 8/17/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_DeleteQuickQuoteInfo
(
	@CUSTOMER_ID     int,
	@QQ_ID		 Int
)
AS
BEGIN
	DELETE FROM CLT_QUICKQUOTE_LIST
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID 
END



GO

