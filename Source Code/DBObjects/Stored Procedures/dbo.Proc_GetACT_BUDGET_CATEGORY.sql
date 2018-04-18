IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_BUDGET_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_BUDGET_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetACT_BUDGET_CATEGORY
Created by      : Vijay Arora
Date            : 10/4/2005
Purpose    	:To get the particular budget category
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetACT_BUDGET_CATEGORY
(
@CATEGEORY_ID     int
)
AS
BEGIN
select CATEGEORY_ID,
CATEGEORY_CODE,
CATEGORY_DEPARTEMENT_NAME,
RESPONSIBLE_EMPLOYEE_NAME,
ISNULL(MODIFIED_BY,'') AS MODIFIED_BY,
ISNULL(CONVERT(VARCHAR(15),LAST_UPDATED_DATETIME,103),'') AS LAST_UPDATED_DATETIME,
ISNULL(IS_ACTIVE,'N') AS IS_ACTIVE
from  ACT_BUDGET_CATEGORY
where 	CATEGEORY_ID = @CATEGEORY_ID
END

GO

