IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReinsuranceContractsInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReinsuranceContractsInDropDown]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetReinsuranceContractsInDropDown
Created by      : Nidhi
Date            : 5 jan,2006
Purpose    	: to fill rein contracts  in drop down
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
create   proc  Dbo.Proc_GetReinsuranceContractsInDropDown 
as
begin

select  CONTRACT_NAME_ID,CONTRACT_NAME from MNT_CONTRACT_NAME order by CONTRACT_NAME
end

GO

