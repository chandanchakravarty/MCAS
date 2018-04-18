IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQQRatingReportStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQQRatingReportStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name       : Proc_GetQQRatingReportStatus
Created by      : Praveen 
Date            : 7 Dec 2009
Purpose    	    :Evaluation
Revison History :
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- DROP Proc Proc_GetQQRatingReportStatus
CREATE PROC Proc_GetQQRatingReportStatus
(
	@CUSTOMER_ID     int,
	@QQ_ID		 Int
)
AS

BEGIN
DECLARE @QQ_RATING_REPORT CHAR(1)
SET @QQ_RATING_REPORT = '1'

IF(SELECT  QQ_RATING_REPORT FROM CLT_QUICKQUOTE_LIST with(nolock)
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND QQ_ID = @QQ_ID ) IS NULL
BEGIN
	SET @QQ_RATING_REPORT = '0'
END

select @QQ_RATING_REPORT as QQ_RATING_REPORT
END






GO

