IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionCodeGroupXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionCodeGroupXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTransactionCodeGroupXML
Created by      : Ajit Singh Chahal
Date            : 6/8/2005
Purpose    	  :To record for xml.
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Proc_GetTransactionCodeGroupXML
(
@TRAN_GROUP_ID     smallint
)
AS
BEGIN
select  TRAN_GROUP_ID,
COUNTRY_ID,
STATE_ID,
LOB_ID,
SUB_LOB_ID,
CLASS_RISK,
TERM,
POLICY_TYPE,
NEW_BUSINESS,
CHANGE_IN_NEW_BUSINESS,
RENEWAL    ,
CHANGE_IN_RENEWAL,
REINSTATE_SAME_TERM ,
REINSTATE_NEW_TERM  ,
CANCELLATION     ,
IS_ACTIVE from ACT_TRAN_CODE_GROUP
where TRAN_GROUP_ID= @TRAN_GROUP_ID
end



GO

