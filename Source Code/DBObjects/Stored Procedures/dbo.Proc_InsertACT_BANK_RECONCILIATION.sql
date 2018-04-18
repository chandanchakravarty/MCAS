IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_BANK_RECONCILIATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_BANK_RECONCILIATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name       : dbo.ACT_BANK_RECONCILIATION
Created by      : Ajit Singh Chahal
Date            : 7/8/2005
Purpose    	  :To insert records of bank reconciliation entity.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_InsertACT_BANK_RECONCILIATION
CREATE PROC [dbo].[Proc_InsertACT_BANK_RECONCILIATION]
(
@AC_RECONCILIATION_ID     int out,
@ACCOUNT_ID     int,
@DIV_ID     smallint,
@DEPT_ID     smallint,
@PC_ID     smallint,
@STATEMENT_DATE     datetime,
@STARTING_BALANCE     decimal(18,2),
@ENDING_BALANCE     decimal(18,2),
@BANK_CHARGES_CREDITS     decimal(18,2),
@LAST_RECONCILED     datetime,
@IS_ACTIVE     nchar(2),
@CREATED_BY     int,
@CREATED_DATETIME     datetime,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
--Checks if a reconciliation is pending for selected account, if it is then return -1
if exists (select AC_RECONCILIATION_ID from ACT_BANK_RECONCILIATION where ACCOUNT_ID = @ACCOUNT_ID and isnull(IS_COMMITED,'N')='N')
	return -2

declare @START_STATEMENT_DATE     datetime
select  @AC_RECONCILIATION_ID=isnull(Max(AC_RECONCILIATION_ID),0)+1 from ACT_BANK_RECONCILIATION
select  top 1 @START_STATEMENT_DATE = STATEMENT_DATE + 1 from ACT_BANK_RECONCILIATION where ACCOUNT_ID = @ACCOUNT_ID order by STATEMENT_DATE
select @LAST_RECONCILED=DATE_COMMITED from ACT_BANK_RECONCILIATION where ACCOUNT_ID=@ACCOUNT_ID and IS_COMMITED='Y' and DATE_COMMITED=(select max(DATE_COMMITED) from ACT_BANK_RECONCILIATION where ACCOUNT_ID=@ACCOUNT_ID and IS_COMMITED='Y')
INSERT INTO ACT_BANK_RECONCILIATION
(
AC_RECONCILIATION_ID,
ACCOUNT_ID,
DIV_ID,
DEPT_ID,
PC_ID,
START_STATEMENT_DATE,
STATEMENT_DATE,
STARTING_BALANCE,
ENDING_BALANCE,
BANK_CHARGES_CREDITS,
LAST_RECONCILED,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME
)
VALUES
(
@AC_RECONCILIATION_ID,
@ACCOUNT_ID,
@DIV_ID,
@DEPT_ID,
@PC_ID,
@START_STATEMENT_DATE,
@STATEMENT_DATE,
@STARTING_BALANCE,
@ENDING_BALANCE,
@BANK_CHARGES_CREDITS,
@LAST_RECONCILED,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
NULL,
NULL)
END




























GO

