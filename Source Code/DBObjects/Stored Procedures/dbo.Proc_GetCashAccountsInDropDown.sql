IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCashAccountsInDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCashAccountsInDropDown]
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
drop proc dbo.Proc_GetCashAccountsInDropDown 
------   ------------       -------------------------*/    
CREATE   prOC [dbo].[Proc_GetCashAccountsInDropDown](    
@Relates_TO_Type int)    
as    
begin    
if @Relates_TO_Type>0    

--  select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION     
--  from ACT_GL_ACCOUNTS     
--  where ACC_TYPE_ID=1 and ACC_LEVEL_TYPE='AS' and ACC_CASH_ACCOUNT='Y' and ACC_RELATES_TO_TYPE = @Relates_TO_Type    
--  order by ACC_DISP_NUMBER     
SELECT t1.account_id,
case when t1.acc_parent_id is null 
	then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')  
	else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')
	end as ACC_DESCRIPTION
FROM ACT_GL_ACCOUNTS t1 
LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id 
WHERE t1.ACC_TYPE_ID=1 and t1.ACC_LEVEL_TYPE='14457' and t1.ACC_CASH_ACCOUNT='Y' AND t1.ACC_RELATES_TO_TYPE = @Relates_TO_Type and t1.IS_ACTIVE = 'Y' --Changed by Aditya for TFS BUG # 1845
ORDER BY t1.account_id  

else    
--  select ACCOUNT_ID,convert(varchar,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION     
--  from ACT_GL_ACCOUNTS     
--  where ACC_TYPE_ID=1 and ACC_LEVEL_TYPE='AS' and ACC_CASH_ACCOUNT='Y'     
--  order by ACC_DISP_NUMBER    

SELECT t1.account_id,
case when t1.acc_parent_id is null 
	then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')  
	else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')
	end as ACC_DESCRIPTION
FROM ACT_GL_ACCOUNTS t1 
LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id 
WHERE t1.ACC_TYPE_ID=1 and t1.ACC_LEVEL_TYPE='14457' and t1.ACC_CASH_ACCOUNT='Y' and t1.IS_ACTIVE = 'Y' --Changed by Aditya for TFS BUG # 1845
ORDER BY t1.account_id  
end    
    



GO

