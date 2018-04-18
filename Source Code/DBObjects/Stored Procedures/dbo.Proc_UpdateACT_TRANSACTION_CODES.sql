IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_TRANSACTION_CODES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_TRANSACTION_CODES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.ACT_TRANSACTION_CODES
Created by      : Ajit Singh Chahal
Date            : 6/7/2005
Purpose    	: To insert records in Transaction codes.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateACT_TRANSACTION_CODES
(
@TRAN_ID     smallint,
@CATEGOTY_CODE     varchar(5),
@TRAN_TYPE     nvarchar(10),
@TRAN_CODE     nvarchar(10),
@DISPLAY_DESCRIPTION     nvarchar(300),
@PRINT_DESCRIPTION     nvarchar(300),
@DEF_AMT_CALC_TYPE     char(1),
@DEF_AMT     decimal(9,2),
@AGENCY_COMM_APPLIES     nchar(2),
@GL_INCOME_ACC     int,
@IS_DEF_NEGATIVE     nchar(2),
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN

update ACT_TRANSACTION_CODES
set 
CATEGOTY_CODE= @CATEGOTY_CODE,
TRAN_TYPE= @TRAN_TYPE,
TRAN_CODE= @TRAN_CODE,
DISPLAY_DESCRIPTION=@DISPLAY_DESCRIPTION ,
PRINT_DESCRIPTION= @PRINT_DESCRIPTION,
DEF_AMT_CALC_TYPE= @DEF_AMT_CALC_TYPE,
DEF_AMT= @DEF_AMT,
AGENCY_COMM_APPLIES= @AGENCY_COMM_APPLIES,
GL_INCOME_ACC= @GL_INCOME_ACC,
IS_DEF_NEGATIVE= @IS_DEF_NEGATIVE,
MODIFIED_BY= @MODIFIED_BY,
LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
where TRAN_ID= @TRAN_ID
END






GO

