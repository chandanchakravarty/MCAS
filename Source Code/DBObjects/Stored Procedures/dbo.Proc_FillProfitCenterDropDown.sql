IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillProfitCenterDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillProfitCenterDropDown]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillProfitCenterDropDown
Created by      : Gaurav
Date            : 4/15/2005
Purpose         : To select  record in MNT_PROFIT_CENTER_LIST
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_FillProfitCenterDropDown

AS

BEGIN

SELECT 	PC_ID , PC_NAME 	FROM 	MNT_PROFIT_CENTER_LIST where Is_Active = 'Y' ORDER BY PC_NAME ASC

END


GO

