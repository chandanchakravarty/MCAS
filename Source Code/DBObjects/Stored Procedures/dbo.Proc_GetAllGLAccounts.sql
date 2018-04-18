IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAllGLAccounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAllGLAccounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetAllGLAccounts    
Created by      : Swastika    
Date            : 20th Jul'1007    
Purpose      : Get GL Accounts with Acc Desc Before AccNo (Investment Cash : 14300.00)    
Related/Similiar Procs  : Proc_GetAllAccountsInDropDown, Proc_GetGLAccounts    
    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- DROP PROC dbo.Proc_GetAllGLAccounts     
CREATE   PROC dbo.Proc_GetAllGLAccounts     
AS    
BEGIN    
--  SELECT ACCOUNT_ID,ACC_DESCRIPTION + ' : ' + CONVERT(VARCHAR,ACC_DISP_NUMBER) AS ACC_DESCRIPTION    
--  FROM ACT_GL_ACCOUNTS     
--  WHERE ACC_LEVEL_TYPE='AS'     
--  ORDER BY ACC_DESCRIPTION 
	SELECT t1.account_id,t1.ACC_DISP_NUMBER as ACC_NUMBER,
	case when t1.acc_parent_id is null 
		then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')  
		else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')
		end as ACC_DESCRIPTION
	FROM ACT_GL_ACCOUNTS t1 
	LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id 
	WHERE t1.ACC_LEVEL_TYPE='AS'     
	ORDER BY ACC_DESCRIPTION   
END    
   



GO

