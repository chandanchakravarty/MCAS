IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ResetCheckNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ResetCheckNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.ACT_CHECK_INFORMATION
Created by      : Ajit Singh Chahal
Date            : 8/25/2005
Purpose    	: To manually reset check number series of an account.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_ResetCheckNumber
create proc dbo.Proc_ResetCheckNumber
(@ACCOUNT_ID int)
as
begin
	update ACT_CHECK_INFORMATION set IS_IN_CURRENT_SEQUENCE='N' 
	where ACCOUNT_ID=@ACCOUNT_ID
	AND ISNULL(IS_PRINTED,'N') = 'Y'
end





GO

