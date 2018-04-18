IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReinsuranceCompaniesInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReinsuranceCompaniesInDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetReinsuranceCompaniesInDropDown
Created by      : Ajit Singh Chahal
Date            : 15 sept,2005
Purpose    	: to fill rein comp in drop down
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc Dbo.Proc_GetReinsuranceCompaniesInDropDown 
create   prOC Dbo.Proc_GetReinsuranceCompaniesInDropDown 
as
begin
select Rein_Comapany_ID,Rein_Comapany_NAME
from MNT_Rein_Comapany_LIST 
 order by Rein_Comapany_NAME
end






GO

