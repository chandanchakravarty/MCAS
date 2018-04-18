IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_ACT_BUDGET_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_ACT_BUDGET_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_Insert_ACT_BUDGET_CATEGORY
Created by      : Vijay Arora
Date            : 10/3/2005
Purpose    	  :To insert the budget category in table name ACT_BUDGET_CATEGORY
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_Insert_ACT_BUDGET_CATEGORY
(
@CATEGEORY_CODE     int,
@CATEGORY_DEPARTEMENT_NAME     varchar(50),
@RESPONSIBLE_EMPLOYEE_NAME     varchar(50),
@CREATED_BY     int,
@CREATED_DATETIME     datetime,
@CATEGEORY_ID     int output
)
AS
BEGIN
IF NOT EXISTS (SELECT CATEGEORY_CODE FROM ACT_BUDGET_CATEGORY WHERE CATEGEORY_CODE = @CATEGEORY_CODE)
	BEGIN
		SELECT @CATEGEORY_ID=isnull(Max(CATEGEORY_ID),0)+1 FROM ACT_BUDGET_CATEGORY

		INSERT INTO ACT_BUDGET_CATEGORY
		(CATEGEORY_ID,CATEGEORY_CODE,CATEGORY_DEPARTEMENT_NAME,RESPONSIBLE_EMPLOYEE_NAME,IS_ACTIVE,CREATED_BY,
		 CREATED_DATETIME
		)
		VALUES
		(@CATEGEORY_ID,@CATEGEORY_CODE,@CATEGORY_DEPARTEMENT_NAME,@RESPONSIBLE_EMPLOYEE_NAME,'Y',@CREATED_BY,
		 @CREATED_DATETIME
		)
	
	END
END



GO

