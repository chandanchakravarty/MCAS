IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDropDownACT_BUDGET_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDropDownACT_BUDGET_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetAllACT_BUDGET_CATEGORY  
Created by      : Vijay Arora  
Date            : Oct. 05 2005  
Purpose         : To get all the active budget categories from ACT_BUDGET_CATEGORY table  
Revison History :  
Used In         : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- Drop PROC dbo.Proc_GetDropDownACT_BUDGET_CATEGORY 
CREATE PROC dbo.Proc_GetDropDownACT_BUDGET_CATEGORY  
AS  
BEGIN  
--SELECT CATEGEORY_ID,CATEGORY_DEPARTEMENT_NAME FROM ACT_BUDGET_CATEGORY WHERE IS_ACTIVE = 'Y' 
/*SELECT t1.account_id,t1.BUDGET_CATEGORY_ID,t3.CATEGEORY_ID,t3.CATEGORY_DEPARTEMENT_NAME,   
  isnull(t1.ACC_DISP_NUMBER,'')+ ' - ' +  t1.ACC_DESCRIPTION + ' / ' +isnull(t3.CATEGORY_DEPARTEMENT_NAME,'')
  as ACC_DESCRIPTION    
 FROM ACT_GL_ACCOUNTS t1 inner join ACT_BUDGET_CATEGORY t3 on t1.BUDGET_CATEGORY_ID=t3.categEory_ID     
 LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id 
 where t1.ACC_PARENT_ID is null and t1.ACC_LEVEL_TYPE='AS' and t3.categEory_ID <>0 order by t1.ACC_DISP_NUMBER*/

 SELECT T1.ACCOUNT_ID,t1.ACC_DISP_NUMBER as ACC_NUMBER,  
 case when t1.acc_parent_id is null   
  then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'') + ' / '   + isnull(t3.CATEGORY_DEPARTEMENT_NAME,'')
  else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')  + ' / '   + isnull(t3.CATEGORY_DEPARTEMENT_NAME,'')
  end as CATEGORY_DEPARTEMENT_NAME  
 FROM ACT_GL_ACCOUNTS t1   
 INNER JOIN ACT_BUDGET_CATEGORY T3 ON T1.BUDGET_CATEGORY_ID=T3.CATEGEORY_ID  
 LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id   
 WHERE t1.ACC_LEVEL_TYPE='AS'     and  t3.IS_ACTIVE = 'Y'   
 ORDER BY CATEGORY_DEPARTEMENT_NAME  

END  











GO

