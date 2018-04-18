IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateQuickQuoteXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateQuickQuoteXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : Proc_UpdateQuickQuoteXml
Created by      : Deepak
Date            : 8/17/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateQuickQuoteXml
(
	@CUSTOMER_ID     int,
	@QQ_ID		 Int,
	@QQ_XML		TEXT
)
AS

BEGIN
	UPDATE CLT_QUICKQUOTE_LIST
	SET QQ_XML=@QQ_XML,
	    QQ_XML_TIME = GetDate()
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID 
END








GO

