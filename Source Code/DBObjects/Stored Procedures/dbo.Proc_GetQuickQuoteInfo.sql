IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuickQuoteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuickQuoteInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetQuickQuoteInfo
--
--go
/*----------------------------------------------------------
Proc Name       : Proc_GetQuickQuoteInfo
Created by      : Deepak
Date            : 8/17/2005
Purpose    		:Evaluation
Revison History : 
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_GetQuickQuoteInfo
(
	@CUSTOMER_ID     int,
	@QQ_ID     int
)
AS
BEGIN
	
	SELECT QQ_NUMBER,QQ_TYPE,ISNULL(QQ_XML,'') QQ_XML,ISNULL(QQ_APP_NUMBER,'') QQ_APP_NUMBER,IS_ACTIVE,ISNULL(QQ_STATE,'') QQ_STATE  
	FROM  CLT_QUICKQUOTE_LIST
	WHERE 	CUSTOMER_ID = @CUSTOMER_ID
		AND QQ_ID=@QQ_ID
END

GO

