IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateQuickQuoteAppNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateQuickQuoteAppNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_UpdateQuickQuoteAppNumber
Created by      : Deepak
Date            : 8/17/2005
Purpose    	  :Evaluation
Revison History :
RPSINGH 	3 Aug 2006	Added APP and APP Version
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- DROP Proc Proc_UpdateQuickQuoteAppNumber
CREATE PROC Proc_UpdateQuickQuoteAppNumber
(
	@CUSTOMER_ID     int,
	@QQ_ID		 Int,
	@QQ_APP_NUMBER	 varchar(8000)
)
AS

BEGIN
	UPDATE CLT_QUICKQUOTE_LIST
	SET QQ_APP_NUMBER=@QQ_APP_NUMBER,
	APP_ID = (Select APP_ID from APP_LIST Where APP_NUMBER = @QQ_APP_NUMBER),
	APP_VERSION_ID = 1 -- WHEN CREATED FROM QQ APP VERSION IS ALWAYS 1
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID 
END

--select * from CLT_QUICKQUOTE_LIST
-- sp_depends CLT_QUICKQUOTE_LIST




GO

