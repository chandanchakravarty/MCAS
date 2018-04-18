IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillFinanceCompanyDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillFinanceCompanyDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillFinanceCompanyDropDown
Created by      : 	Ajit Singh Chahal
Date                : 	04/20/2005
Purpose         : 	To fill drop down of finance company names
Revison History :
Used In         :  	 Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
create     PROCEDURE [dbo].[Proc_FillFinanceCompanyDropDown] AS
begin

select COMPANY_ID,COMPANY_NAME from MNT_FINANCE_COMPANY_LIST where IS_ACTIVE = 'Y' order by COMPANY_NAME

End







GO

