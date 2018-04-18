IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_CATASTROPHE_EVENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_CATASTROPHE_EVENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateCLM_CATASTROPHE_EVENT
Created by      : Vijay Arora
Date            : 4/24/2006
Purpose    	: To update the data in table named CLM_CATASTROPHE_EVENT
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateCLM_CATASTROPHE_EVENT
(
	@CATASTROPHE_EVENT_ID     int,
	@CATASTROPHE_EVENT_TYPE     int,
	@DATE_FROM     datetime,
	@DATE_TO     datetime,
	@DESCRIPTION     varchar(500),
	@CAT_CODE     varchar(20),
	@MODIFIED_BY     int
)
AS
BEGIN
Update  CLM_CATASTROPHE_EVENT
set
CATASTROPHE_EVENT_TYPE = @CATASTROPHE_EVENT_TYPE,
DATE_FROM = @DATE_FROM,
DATE_TO = @DATE_TO,
DESCRIPTION = @DESCRIPTION,
CAT_CODE = @CAT_CODE,
MODIFIED_BY = @MODIFIED_BY,
LAST_UPDATED_DATETIME = GETDATE()
where 	CATASTROPHE_EVENT_ID = @CATASTROPHE_EVENT_ID
END



GO

