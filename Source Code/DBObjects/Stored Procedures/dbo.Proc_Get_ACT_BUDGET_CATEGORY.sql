IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_ACT_BUDGET_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_ACT_BUDGET_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_Get_ACT_BUDGET_CATEGORY
Created by      : Vijay Arora
Date            : 10/3/2005
Purpose    	  :To get the xml listing from table name ACT_BUDGET_CATEGORY
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_Get_ACT_BUDGET_CATEGORY
(
@CATEGEORY_ID     int
)
AS
BEGIN
select CATEGEORY_ID,
CATEGEORY_CODE,
CATEGORY_DEPARTEMENT_NAME,
RESPONSIBLE_EMPLOYEE_NAME,
MODIFIED_BY,
LAST_UPDATED_DATETIME
from  ACT_BUDGET_CATEGORY
where 	CATEGEORY_ID = @CATEGEORY_ID
END



GO

