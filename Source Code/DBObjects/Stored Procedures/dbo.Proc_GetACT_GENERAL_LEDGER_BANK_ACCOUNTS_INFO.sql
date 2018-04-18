IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_GENERAL_LEDGER_BANK_ACCOUNTS_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_GENERAL_LEDGER_BANK_ACCOUNTS_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------    
Proc Name       : Proc_GetACT_GENERAL_LEDGER_BANK_ACCOUNTS_INFO    
Created by      : Vijay Joshi    
Date            : 27 june, 2005    
Purpose     : Get accounts from ACT_GENERAL_LEDGER which are of type cash and bank information    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
  
drop proc dbo.Proc_GetACT_GENERAL_LEDGER_BANK_ACCOUNTS_INFO    
------   ------------       -------------------------*/    
CREATE Procedure [dbo].[Proc_GetACT_GENERAL_LEDGER_BANK_ACCOUNTS_INFO]    
(    
 @FISCAL_ID int    
)    
As    
BEGIN    
--  SELECT     
--   GL_ACCOUNTS.FISCAL_ID, GL_ACCOUNTS.ACCOUNT_ID,     
--   IsNull(GL_ACCOUNTS.ACC_DISP_NUMBER,'') + ' - ' + IsNull(GL_ACCOUNTS.ACC_DESCRIPTION,'') As ACC_DESCRIPTION,    
--   Convert(varchar,GL_ACCOUNTS.ACCOUNT_ID)     
--   AS EXTRA_INFO    
--       
--  FROM     
--   ACT_GL_ACCOUNTS GL_ACCOUNTS    
--  LEFT JOIN ACT_BANK_INFORMATION BANK_INFO ON    
--   GL_ACCOUNTS.ACCOUNT_ID = BANK_INFO.ACCOUNT_ID    
--  WHERE Upper(ACC_CASH_ACCOUNT) = 'Y'     
--  ORDER BY ACC_DISP_NUMBER    
  
  
    
--SELECT t1.FISCAL_ID, t1.ACCOUNT_ID,t1.acc_parent_id,  
--case when t1.acc_parent_id is null   
-- then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')    
-- else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')  
-- end as ACC_DESCRIPTION,Convert(varchar,t1.ACCOUNT_ID)    AS EXTRA_INFO    
  
--FROM ACT_GL_ACCOUNTS t1   
--LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id   
--LEFT JOIN ACT_BANK_INFORMATION BANK_INFO ON    
--  t1.ACCOUNT_ID = BANK_INFO.ACCOUNT_ID    
-- WHERE Upper(t1.ACC_CASH_ACCOUNT) = 'Y'      
--ORDER BY t1.ACC_DESCRIPTION    
  
SELECT BANK_ID , BANK_NAME +' : ' +BANK_NUMBER  AS ACC_DESCRIPTION FROM ACT_BANK_INFORMATION WITH( NOLOCK)  
 WHERE ISNULL(BANK_TYPE,14932) = 14932
  
END    
    
  

GO

