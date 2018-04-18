IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateQuickQuoteRatingReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateQuickQuoteRatingReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : Proc_UpdateQuickQuoteRatingReport
Created by      : Deepak
Date            : 8/17/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROCEDURE Dbo.Proc_UpdateQuickQuoteRatingReport
(
	@CUSTOMER_ID     	int,
	@QQ_ID		 	Int,
	@QQ_RATING_REPORT 	text
)
AS

BEGIN
	UPDATE CLT_QUICKQUOTE_LIST
	SET QQ_RATING_REPORT=@QQ_RATING_REPORT,
	    QQ_RATING_TIME = GetDate()
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID 
END

/*
sp_help CLT_QUICKQUOTE_LIST
*/








GO

