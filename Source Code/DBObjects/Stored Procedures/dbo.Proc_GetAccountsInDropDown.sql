IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAccountsInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAccountsInDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertACT_GL_ACCOUNTS  
Created by      : Ajit Singh Chahal  
Date            : 5/18/2005  
Purpose     : to parent a/cs in drop down  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc Proc_GetAccountsInDropDown
create   prOC Dbo.Proc_GetAccountsInDropDown   
(  
@accountTypeID int  
)  
as  
begin  
select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION  
from ACT_GL_ACCOUNTS   
where ACC_TYPE_ID=@accountTypeID and ACC_LEVEL_TYPE='AS' and IS_ACTIVE ='Y'

order by ACC_DISP_NUMBER  
end  
  
  
  
  
  
  
  
  
  
  
  
  



GO

