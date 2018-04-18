IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteRatingReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteRatingReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : Proc_GetQuickQuoteRatingReport
Created by      : Deepak
Date            : 8/17/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetQuickQuoteRatingReport
(
	@CUSTOMER_ID     	int,
	@QQ_ID		 	Int
)
AS

BEGIN
	SELECT QQ_RATING_REPORT 
	FROM CLT_QUICKQUOTE_LIST 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID 
END

/*
sp_help CLT_QUICKQUOTE_LIST
*/




GO

