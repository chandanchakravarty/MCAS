IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAllAccountsInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAllAccountsInDropDown]
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
Reviewd  :      :Praveen kasana
Modified :      : Accountd Description     
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc Dbo.Proc_GetAllAccountsInDropDown 
create   prOC dbo.Proc_GetAllAccountsInDropDown 
as
begin
-- select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION
-- from ACT_GL_ACCOUNTS 
-- where ACC_LEVEL_TYPE='AS' 
-- order by ACC_DISP_NUMBER

SELECT T1.ACCOUNT_ID,  
CASE WHEN T1.ACC_PARENT_ID IS NULL   
 THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
 END AS ACC_DESCRIPTION  
FROM ACT_GL_ACCOUNTS T1   
LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
WHERE T1.ACC_TYPE_ID=1 AND T1.ACC_LEVEL_TYPE='AS' AND T1.ACC_CASH_ACCOUNT='Y'   
ORDER BY T1.ACCOUNT_ID    
end






















GO

