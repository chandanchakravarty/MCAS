IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_TRAN_CODE_GROUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_TRAN_CODE_GROUP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertACT_TRAN_CODE_GROUP
Created by      : Ajit Singh Chahal
Date            : 6/8/2005
Purpose    	  :To insert records in transaction code group.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertACT_TRAN_CODE_GROUP
(
@TRAN_GROUP_ID     smallint out,
@COUNTRY_ID     int,
@STATE_ID     smallint,
@LOB_ID     smallint,
@SUB_LOB_ID     smallint,
@CLASS_RISK     int,
@TERM     char(1),
@POLICY_TYPE     char(1),
@IS_ACTIVE     nchar(2),
@CREATED_BY     int,
@CREATED_DATETIME     datetime,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime,
@NEW_BUSINESS     char(1),
@CHANGE_IN_NEW_BUSINESS     char(1),
@RENEWAL     char(1),
@CHANGE_IN_RENEWAL     char(1),
@REINSTATE_SAME_TERM     char(1),
@REINSTATE_NEW_TERM     char(1),
@CANCELLATION     char(1)
)
AS
BEGIN
select  @TRAN_GROUP_ID=isnull(Max(TRAN_GROUP_ID),0)+1 from ACT_TRAN_CODE_GROUP
INSERT INTO ACT_TRAN_CODE_GROUP
(
TRAN_GROUP_ID,
COUNTRY_ID,
STATE_ID,
LOB_ID,
SUB_LOB_ID,
CLASS_RISK,
TERM,
POLICY_TYPE,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME,
NEW_BUSINESS,
CHANGE_IN_NEW_BUSINESS,
RENEWAL,
CHANGE_IN_RENEWAL,
REINSTATE_SAME_TERM,
REINSTATE_NEW_TERM,
CANCELLATION
)
VALUES
(
@TRAN_GROUP_ID,
@COUNTRY_ID,
@STATE_ID,
@LOB_ID,
@SUB_LOB_ID,
@CLASS_RISK,
@TERM,
@POLICY_TYPE,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
@MODIFIED_BY,
@LAST_UPDATED_DATETIME,
@NEW_BUSINESS,
@CHANGE_IN_NEW_BUSINESS,
@RENEWAL,
@CHANGE_IN_RENEWAL,
@REINSTATE_SAME_TERM,
@REINSTATE_NEW_TERM,
@CANCELLATION
)
END




GO

