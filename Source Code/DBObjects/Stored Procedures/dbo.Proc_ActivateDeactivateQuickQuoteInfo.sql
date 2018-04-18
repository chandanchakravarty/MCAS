IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateQuickQuoteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateQuickQuoteInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Proc_ActivateDeactivateQuickQuoteInfo
Created by      : Deepak
Date            : 8/17/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_ActivateDeactivateQuickQuoteInfo
(
	@CUSTOMER_ID     int,
	@QQ_ID		 Int
)
AS
DECLARE @ACTIVE_STATUS VARCHAR(5)
BEGIN


	UPDATE CLT_QUICKQUOTE_LIST
	SET IS_ACTIVE = CASE ISNULL(IS_ACTIVE,'') 
				WHEN 'Y' THEN 'N'
				ELSE 'Y'
			END
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID 

END





GO

