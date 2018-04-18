IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_BUDGET_PLAN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_BUDGET_PLAN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : dbo.ACT_BUDGET_PLAN  
Created by      : Manoj Rathore
Date            : 6/22/2007 
Purpose     	:To insert records of Budget Plan entity.  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_InsertACT_BUDGET_PLAN  

CREATE PROC dbo.Proc_InsertACT_BUDGET_PLAN  
(  
	@IDEN_ROW_ID     int out,  
	@BUDGET_CATEGORY_ID int,
        @GL_ID     int,
	@FISCAL_ID INT =NULL,	
	@JAN_BUDGET decimal(18,2),
	@FEB_BUDGET decimal(18,2),
	@MARCH_BUDGET decimal(18,2),
	@APRIL_BUDGET decimal(18,2),
	@MAY_BUDGET   decimal(18,2),
	@JUNE_BUDGET  decimal(18,2),
	@JULY_BUDGET  decimal(18,2),
	@AUG_BUDGET   decimal(18,2),
	@SEPT_BUDGET  decimal(18,2),
	@OCT_BUDGET   decimal(18,2),
	@NOV_BUDGET   decimal(18,2),
	@DEC_BUDGET decimal(18,2),
	@ACCOUNT_ID int = null
)  
AS  
BEGIN  
	

	--IF EXISTS(SELECT BUDGET_CATEGORY_ID FROM ACT_BUDGET_PLAN WHERE BUDGET_CATEGORY_ID=@BUDGET_CATEGORY_ID and GL_ID=@GL_ID 
		--	and FISCAL_ID=@FISCAL_ID)
		 -- RETURN -2
IF EXISTS(SELECT ACCOUNT_ID FROM ACT_BUDGET_PLAN WHERE ACCOUNT_ID=@ACCOUNT_ID and GL_ID=@GL_ID 
			and FISCAL_ID=@FISCAL_ID)
		  RETURN -2

/* if (@BUDGET_CATEGORY_ID=1)	
begin	
IF EXISTS(SELECT BUDGET_CATEGORY_ID FROM ACT_BUDGET_PLAN WHERE BUDGET_CATEGORY_ID=1 
--		and GL_ID=@GL_ID and FISCAL_ID=@FISCAL_ID
			)
		  RETURN -2
end */
	SELECT @IDEN_ROW_ID= isnull(max(IDEN_ROW_ID)+1,1) from ACT_BUDGET_PLAN 

	INSERT INTO ACT_BUDGET_PLAN  
	(  
		--IDEN_ROW_ID,  
		BUDGET_CATEGORY_ID,
		GL_ID,
		FISCAL_ID,
		JAN_BUDGET,
		FEB_BUDGET,
		MARCH_BUDGET,
		APRIL_BUDGET,
		MAY_BUDGET,
		JUNE_BUDGET,
		JULY_BUDGET,
		AUG_BUDGET,
		SEPT_BUDGET,
		OCT_BUDGET,
		NOV_BUDGET,
		DEC_BUDGET,
		ACCOUNT_ID

	)  
	VALUES  
	(  
		--@IDEN_ROW_ID,
		@BUDGET_CATEGORY_ID,  
		@GL_ID,
		@FISCAL_ID,
		@JAN_BUDGET,
		@FEB_BUDGET,
		@MARCH_BUDGET,
		@APRIL_BUDGET,
		@MAY_BUDGET,
		@JUNE_BUDGET,
		@JULY_BUDGET,
		@AUG_BUDGET,
		@SEPT_BUDGET,
		@OCT_BUDGET,
		@NOV_BUDGET,
		@DEC_BUDGET,
		@ACCOUNT_ID

	)  
	RETURN 1
END  


















GO

