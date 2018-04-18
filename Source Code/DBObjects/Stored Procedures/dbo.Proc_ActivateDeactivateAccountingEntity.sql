IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAccountingEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAccountingEntity]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_ActivateDeactivateAccountingEntity
Created by      : Gaurav
Date            : 5/4/2005
Purpose         : To Activate / Deactivate Accounting Entity
Revison History : Reviewd by Rajan on 10/05/2005
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC dbo.Proc_ActivateDeactivateAccountingEntity
(
@CODE  		NUMERIC(9) ,
@Is_Active 	Char(1)			
)
AS
BEGIN
UPDATE MNT_ACCOUNTING_ENTITY_LIST
	SET 
		Is_Active	= @Is_Active
	WHERE
		REC_ID 		= @CODE
END



GO

