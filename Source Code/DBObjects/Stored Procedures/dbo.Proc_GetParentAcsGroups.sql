IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetParentAcsGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetParentAcsGroups]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertACT_GL_ACCOUNTS
Created by      : Ajit Singh Chahal
Date            : 5/18/2005
Purpose    	: to parent a/cs in drop down
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
create   prOC Dbo.Proc_GetParentAcsGroups
as
begin
select ACCOUNT_ID,ACC_TYPE_ID 
from ACT_GL_ACCOUNTS 
--where ACC_PARENT_ID is null and ACC_LEVEL_TYPE='AS'
where ACC_PARENT_ID is null and ACC_LEVEL_TYPE=14457  --Condition Already changed for ACC_LEVEL_TYPE
end







  
  
  







GO

