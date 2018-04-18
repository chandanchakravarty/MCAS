IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGLName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGLName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.ACT_BANK_RECONCILIATION
Created by      : Ajit Singh Chahal
Date            : 7/8/2005
Purpose    	: To get legder name
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetGLName
as
begin
select distinct LEDGER_NAME from ACT_GENERAL_LEDGER 
end




GO

