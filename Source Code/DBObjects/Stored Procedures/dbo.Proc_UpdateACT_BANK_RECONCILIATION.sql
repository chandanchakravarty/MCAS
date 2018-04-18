IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_BANK_RECONCILIATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_BANK_RECONCILIATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.ACT_BANK_RECONCILIATION
Created by      : Ajit Singh Chahal
Date            : 7/8/2005
Purpose    	: To insert update of bank reconciliation entity.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_UpdateACT_BANK_RECONCILIATION
CREATE  PROC dbo.Proc_UpdateACT_BANK_RECONCILIATION
(
@AC_RECONCILIATION_ID     int ,
@ACCOUNT_ID     int,
@DIV_ID     smallint,
@DEPT_ID     smallint,
@PC_ID     smallint,
@STATEMENT_DATE     datetime,
@STARTING_BALANCE     decimal(18,2),
@ENDING_BALANCE     decimal(18,2),
@BANK_CHARGES_CREDITS     decimal(18,2),
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
--Checks if a reconciliation is pending for selected account, if it is then return -1
if exists (select AC_RECONCILIATION_ID from ACT_BANK_RECONCILIATION where ACCOUNT_ID = @ACCOUNT_ID and isnull(IS_COMMITED,'N')='N' and AC_RECONCILIATION_ID<>@AC_RECONCILIATION_ID)
	return -2
update ACT_BANK_RECONCILIATION
set 
ACCOUNT_ID = @ACCOUNT_ID,
DIV_ID = @DIV_ID,
DEPT_ID = @DEPT_ID,
PC_ID = @PC_ID,
STATEMENT_DATE = @STATEMENT_DATE,
STARTING_BALANCE = @STARTING_BALANCE,
ENDING_BALANCE = @ENDING_BALANCE,
BANK_CHARGES_CREDITS = @BANK_CHARGES_CREDITS,
MODIFIED_BY =@MODIFIED_BY ,
LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
where AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID
END
















GO

