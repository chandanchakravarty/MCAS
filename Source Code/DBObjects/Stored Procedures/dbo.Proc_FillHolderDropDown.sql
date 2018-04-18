IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillHolderDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillHolderDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillHolderDropDown
Created by      : 	Ajit Singh Chahal
Date                : 	04/20/2005
Purpose         : 	To fill drop down of Holder Names
Revison History :
Used In         :  	 Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
create     PROCEDURE [dbo].[Proc_FillHolderDropDown] AS
begin

select HOLDER_ID,HOLDER_NAME from MNT_HOLDER_INTEREST_LIST where IS_ACTIVE = 'Y' order by HOLDER_NAME

End







GO

