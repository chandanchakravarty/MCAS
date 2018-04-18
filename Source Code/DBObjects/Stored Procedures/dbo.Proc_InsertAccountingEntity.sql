IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAccountingEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAccountingEntity]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertAccountingEntity
Created by      : Gaurav
Date            : 4/15/2005
Purpose         : To add record in MNT_ACCOUNTING_ENTITY_LIST table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertAccountingEntity
(
@ENTITY_ID	int,
@ENTITY_TYPE	varchar(5),
@DIVISION_ID	int,
@DEPARTMENT_ID	int,
@PROFIT_CENTER_ID	int,
@Is_Active		nchar(1),
@Created_By		int,
@Created_DateTime	DateTime,

@REC_ID			int 		OUTPUT
)
 AS
BEGIN

Declare @Count int
Set @Count= (SELECT count(DIVISION_ID) FROM MNT_ACCOUNTING_ENTITY_LIST WHERE DIVISION_ID =@DIVISION_ID and DEPARTMENT_ID =@DEPARTMENT_ID and PROFIT_CENTER_ID=@PROFIT_CENTER_ID and ENTITY_ID=@ENTITY_ID and ENTITY_TYPE=@ENTITY_TYPE)

if(@Count=0)
BEGIN
INSERT INTO MNT_ACCOUNTING_ENTITY_LIST	(
				ENTITY_ID,
				ENTITY_TYPE,
				DIVISION_ID,
				DEPARTMENT_ID,
				PROFIT_CENTER_ID,
				Is_Active,
				Created_By,
				Created_DateTime
				
			 	)
	VALUES	(
				@ENTITY_ID,
				@ENTITY_TYPE,
				@DIVISION_ID,
				@DEPARTMENT_ID,
				@PROFIT_CENTER_ID,
				@Is_Active,
				@Created_By,
				@Created_DateTime
				

			)


SELECT @REC_ID=Max(REC_ID)
			FROM MNT_ACCOUNTING_ENTITY_LIST

END

ELSE
BEGIN
/*Record already exist*/
		SELECT @REC_ID = -1

END

END


GO

