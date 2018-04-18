IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetParentAccountsInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetParentAccountsInDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertACT_GL_ACCOUNTS  
Created by      : Ajit Singh Chahal  
Date            : 5/18/2005  
Purpose     : to insert records in ACT_GL_ACCOUNTS  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop prOC Dbo.Proc_GetParentAccountsInDropDown   
create  prOC Dbo.Proc_GetParentAccountsInDropDown as  
begin  
select   
ACCOUNT_ID,  
--convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION   
ACC_DISP_NUMBER+': '+ACC_DESCRIPTION as ACC_DESCRIPTION   
from ACT_GL_ACCOUNTS  
--where ACC_PARENT_ID is null and ACC_LEVEL_TYPE='AS' order by ACC_DISP_NUMBER  
where ACC_PARENT_ID is null and ACC_LEVEL_TYPE='14457' order by ACC_DISP_NUMBER  -- Condition already changed for ACC_LEVEL_TYPE
end  
  
  
-- exec Proc_GetParentAccountsInDropDown
  
  
  
  
  
  
  
  
  



GO

    
    
    
    
    
  
  