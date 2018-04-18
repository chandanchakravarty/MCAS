IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_TRAN_CODE_GROUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_TRAN_CODE_GROUP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateACT_TRAN_CODE_GROUP
Created by      : Ajit Singh Chahal
Date            : 6/8/2005
Purpose    	  :To update records in transaction code group.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Proc_UpdateACT_TRAN_CODE_GROUP
(
@TRAN_GROUP_ID     smallint,
@COUNTRY_ID     int,
@STATE_ID     smallint,
@LOB_ID     smallint,
@SUB_LOB_ID     smallint,
@CLASS_RISK     int,
@TERM     char(1),
@POLICY_TYPE     char(1),
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
update ACT_TRAN_CODE_GROUP
set
COUNTRY_ID= @COUNTRY_ID,
STATE_ID= @STATE_ID,
LOB_ID= @LOB_ID,
SUB_LOB_ID= @SUB_LOB_ID,
CLASS_RISK= @CLASS_RISK,
TERM=@TERM ,
POLICY_TYPE= @POLICY_TYPE,
NEW_BUSINESS    =@NEW_BUSINESS  ,
CHANGE_IN_NEW_BUSINESS    = @CHANGE_IN_NEW_BUSINESS,
RENEWAL    =@RENEWAL ,
CHANGE_IN_RENEWAL    = @CHANGE_IN_RENEWAL,
REINSTATE_SAME_TERM    = @REINSTATE_SAME_TERM,
REINSTATE_NEW_TERM    = @REINSTATE_NEW_TERM,
CANCELLATION     = @CANCELLATION,
MODIFIED_BY= @MODIFIED_BY,
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
where TRAN_GROUP_ID= @TRAN_GROUP_ID
end





GO

