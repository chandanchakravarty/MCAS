IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateTransactionGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateTransactionGroups]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_ActivateDeactivateCoverage
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_ActivateDeactivateTransactionGroups
(
	@code int ,
	@Is_Active nchar
)
AS
BEGIN
UPDATE ACT_TRAN_CODE_GROUP  
SET Is_Active= @Is_Active
WHERE TRAN_GROUP_ID= @code
END





GO

