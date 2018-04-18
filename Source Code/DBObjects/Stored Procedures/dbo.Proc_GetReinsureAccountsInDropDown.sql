IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReinsureAccountsInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReinsureAccountsInDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetReinsureAccountsInDropDown
Created by      : Shafee
Date            : 4/1/2005
Purpose     :     
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_GetReinsureAccountsInDropDown   
  
as  
begin  
--Table Income
select convert(varchar,GL_ID)+'^'+convert(varchar,ACCOUNT_ID)  as AccountID,
convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION 
from ACT_GL_ACCOUNTS
where ACC_RELATES_TO_TYPE = 11199  and ACC_TYPE_ID =4 And GL_ID=1

--Table Liablity

select convert(varchar,GL_ID)+'^'+convert(varchar,ACCOUNT_ID)  as AccountID,
convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION 
from ACT_GL_ACCOUNTS
where ACC_RELATES_TO_TYPE = 11199  and ACC_TYPE_ID =2 And GL_ID=1

--Table Expense

select convert(varchar,GL_ID)+'^'+convert(varchar,ACCOUNT_ID)  as AccountID,
convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION 
from ACT_GL_ACCOUNTS
where ACC_RELATES_TO_TYPE = 11199  and ACC_TYPE_ID =5 And GL_ID=1

--Table Asset

select convert(varchar,GL_ID)+'^'+convert(varchar,ACCOUNT_ID)  as AccountID,
convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION 
from ACT_GL_ACCOUNTS
where ACC_RELATES_TO_TYPE = 11199  and ACC_TYPE_ID =1 And GL_ID=1

End

GO

