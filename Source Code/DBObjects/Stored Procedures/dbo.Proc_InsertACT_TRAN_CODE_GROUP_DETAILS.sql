IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_TRAN_CODE_GROUP_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_TRAN_CODE_GROUP_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.ACT_TRAN_CODE_GROUP_DETAILS
Created by      : Ajit Chahal Chahal
Date            : 6/9/2005
Purpose    	  :To insert records 
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertACT_TRAN_CODE_GROUP_DETAILS
(
@DETAIL_ID     smallint out,
@TRAN_GROUP_ID     smallint,
@TRAN_ID     smallint,
@DEF_SEQ     smallint,
@IS_ACTIVE     nchar(2),
@CREATED_BY     int,
@CREATED_DATETIME     datetime,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
select  @DETAIL_ID=isnull(Max(DETAIL_ID),0)+1 from ACT_TRAN_CODE_GROUP_DETAILS
INSERT INTO ACT_TRAN_CODE_GROUP_DETAILS
(
DETAIL_ID,
TRAN_GROUP_ID,
TRAN_ID,
DEF_SEQ,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME
)
VALUES
(
@DETAIL_ID,
@TRAN_GROUP_ID,
@TRAN_ID,
@DEF_SEQ,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
@MODIFIED_BY,
@LAST_UPDATED_DATETIME
)
END






GO

