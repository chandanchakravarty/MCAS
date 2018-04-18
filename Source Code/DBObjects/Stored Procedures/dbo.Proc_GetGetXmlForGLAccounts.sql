IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGetXmlForGLAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGetXmlForGLAccounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------    
Proc Name       : Proc_GetGetXmlForGLAccounts    
Created by      : Ajit Singh Chahal    
Date            : 8/8/2005    
Purpose     : To get xml for GL account page controls..    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_GetGetXmlForGLAccounts    
CREATE PROC [dbo].[Proc_GetGetXmlForGLAccounts]    
(    
@ACCOUNT_ID int     
)    
AS    
BEGIN    
select ACC_TYPE_DESC,ACCOUNT_ID,CATEGORY_TYPE,GROUP_TYPE,t1.ACC_TYPE_ID,ACC_PARENT_ID,GL_ID    
--floor(ACC_NUMBER) as ACC_NUMBER    
,ACC_LEVEL_TYPE,ACC_DESCRIPTION,ACC_TOTALS_LEVEL,    
case when ACC_JOURNAL_ENTRY='Y' then '10963' when ACC_JOURNAL_ENTRY='N' then '10964' ELSE '' END ACC_JOURNAL_ENTRY,    
ACC_CASH_ACCOUNT,ACC_CASH_ACC_TYPE,IS_ACTIVE,    
case when ACC_CASH_DEF_TYPE=1 OR ACC_CASH_DEF_TYPE=3 then 1 end as ACC_CASH_DEF_TYPE_CASH,    
case when ACC_CASH_DEF_TYPE=2 OR ACC_CASH_DEF_TYPE=3 then 1 end as ACC_CASH_DEF_TYPE_CHECK,    
case when len(convert(varchar,floor(ACC_NUMBER)))=1 then '0'+convert(varchar,floor((ACC_NUMBER))) else convert(varchar,floor(ACC_NUMBER)) end as ACC_NUMBER,    
 ACC_RELATES_TO_TYPE , BUDGET_CATEGORY_ID,WOLVERINE_USER_ID  
    
from ACT_GL_ACCOUNTS t1,ACT_TYPE_MASTER t2    
    
where  ACCOUNT_ID=@ACCOUNT_ID and t1.ACC_TYPE_ID = t2.ACC_TYPE_ID    
    
END    
    
    
    
    
    
  
  
  
  

GO

