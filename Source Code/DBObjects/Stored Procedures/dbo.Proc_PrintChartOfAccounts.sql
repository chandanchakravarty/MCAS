IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PrintChartOfAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PrintChartOfAccounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--Created by ajit
--MODIFIED BY : Agniswar
--DATE MODIFIED : 04 Nov 2011

CREATE  proc Proc_PrintChartOfAccounts  
as  
begin  
select t1.ACCOUNT_ID,t1.FISCAL_ID,t1.GL_ID,ltrim(rtrim(t1.ACC_LEVEL_TYPE)) as ACC_LEVEL_TYPE,
t1.ACC_NUMBER,t1.ACC_DISP_NUMBER  ,case 
when t1.acc_parent_id is null then t1.ACC_DESCRIPTION  
else isnull(t3.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION 
end as ACC_DESCRIPTION,ACC_TYPE_DESC,t1.ACC_TOTALS_LEVEL,  
case t1.ACC_CASH_ACC_TYPE when 'O' then 'Operating' when 'T' then 'Trust' when 'C' then 'Checking' else '-' end as 'CashType',  
case t1.IS_ACTIVE when 'Y' then 'YES' when 'N' then 'NO' end as Active  
from ACT_GL_ACCOUNTS t1--,ACT_TYPE_MASTER t2  
inner join  ACT_TYPE_MASTER t2 
on t1.ACC_TYPE_ID = t2.ACC_TYPE_ID
LEFT OUTER JOIN  ACT_GL_ACCOUNTS t3 
ON t3.account_id = t1.acc_parent_id 
where  t1.ACC_TYPE_ID = t2.ACC_TYPE_ID  
order by ACC_DISP_NUMBER  
end  






GO

