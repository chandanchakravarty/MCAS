IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGLAccountsInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGLAccountsInDropDown]
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
Modified by	: Pravesh K Chandel 
Modified Date	:16 Aug 2007
Purpose		: Add more Condition to filter
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_GetGLAccountsInDropDown 
create  prOC [dbo].[Proc_GetGLAccountsInDropDown] 
(
@GL_ID int,
@accountTypeID int
)
as
begin
select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION,*

from ACT_GL_ACCOUNTS 

where GL_ID=@GL_ID and ACC_TYPE_ID=@accountTypeID and ACC_LEVEL_TYPE='14457' and IS_ACTIVE = 'Y' --Changed by Aditya for TFS BUG # 1845

order by ACC_DISP_NUMBER

end





GO

