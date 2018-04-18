IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCompany]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCompany
Created by      : Priya
Date            : 16 Jun,2005
Purpose         : To delete record from Finance Company table
Revison History :
Used In         :   wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCompany
(

	@CompanyId	INT
)
AS
BEGIN
	DELETE FROM MNT_FINANCE_COMPANY_LIST
	WHERE COMPANY_ID = @CompanyId
END






GO

