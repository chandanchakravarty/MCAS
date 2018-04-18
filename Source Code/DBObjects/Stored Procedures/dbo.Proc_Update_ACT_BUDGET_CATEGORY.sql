IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_ACT_BUDGET_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_ACT_BUDGET_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_Update_ACT_BUDGET_CATEGORY
Created by      : Vijay Arora
Date            : 10/3/2005
Purpose    	  :To update the budget category in table name ACT_BUDGET_CATEGORY
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_Update_ACT_BUDGET_CATEGORY
(
@CATEGEORY_ID     int,
@CATEGEORY_CODE     int,
@CATEGORY_DEPARTEMENT_NAME     varchar(50),
@RESPONSIBLE_EMPLOYEE_NAME     varchar(50),
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
	IF NOT EXISTS (SELECT CATEGEORY_CODE FROM ACT_BUDGET_CATEGORY WHERE CATEGEORY_CODE = @CATEGEORY_CODE AND @CATEGEORY_ID != CATEGEORY_ID )
		BEGIN
			UPDATE  ACT_BUDGET_CATEGORY 
			SET
				CATEGEORY_CODE  = @CATEGEORY_CODE,
				CATEGORY_DEPARTEMENT_NAME  = @CATEGORY_DEPARTEMENT_NAME,
				RESPONSIBLE_EMPLOYEE_NAME  =  @RESPONSIBLE_EMPLOYEE_NAME,
				MODIFIED_BY  =  @MODIFIED_BY,
				LAST_UPDATED_DATETIME  =  @LAST_UPDATED_DATETIME
			WHERE	
				CATEGEORY_ID = @CATEGEORY_ID
		END
	
END


GO

