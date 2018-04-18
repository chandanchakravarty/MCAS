IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGLAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGLAccounts]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetGLAccounts
Created by           : Mohit Gupta
Date                    : 
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetGLAccounts
AS
BEGIN
select ACCOUNT_ID,ACC_DESCRIPTION from ACT_GL_ACCOUNTS
END


GO

