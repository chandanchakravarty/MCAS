IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_INSTALL_PARAMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_INSTALL_PARAMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_GetACT_INSTALL_PARAMS
Created by      Vijay Joshi
Date            : 6/6/2005
Purpose   :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetACT_INSTALL_PARAMS

AS
BEGIN
	SELECT INSTALL_DAYS_IN_ADVANCE, INSTALL_NOTIFY_ACCOUNTEXE,
		INSTALL_NOTIFY_CSR, INSTALL_NOTIFY_UNDERWRITER,
		INSTALL_NOTIFY_OTHER_USERS, IS_ACTIVE,
		MODIFIED_BY, LAST_UPDATED_DATETIME
	FROM ACT_INSTALL_PARAMS
END






GO

