IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSystemParams]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSystemParams]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateSystemParams
Created by      : Gaurav
Date            : 3/21/2005
Purpose         : To select  record in MNT_SYSTEM_PARAMS
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_GetSystemParams

AS

BEGIN

		SELECT * FROM MNT_SYSTEM_PARAMS
END


GO

