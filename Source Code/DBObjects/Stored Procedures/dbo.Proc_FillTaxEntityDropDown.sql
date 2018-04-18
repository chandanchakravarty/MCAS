IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillTaxEntityDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillTaxEntityDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_FillTaxEntityDropDown
Created by      : 	Ajit Singh Chahal
Date                : 	04/20/2005
Purpose         : 	To fill drop down of tax Entity
Revison History :
Used In         :  	 Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
create     PROCEDURE [dbo].[Proc_FillTaxEntityDropDown] AS
begin

select TAX_ID,TAX_NAME from MNT_TAX_ENTITY_LIST where IS_ACTIVE = 'Y' order by TAX_NAME

End







GO

