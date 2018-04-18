IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_GENERAL_LEDGER_ACCOUNTS_NOTIN_POSTING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_GENERAL_LEDGER_ACCOUNTS_NOTIN_POSTING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : Proc_GetACT_GENERAL_LEDGER_POSTING_INFO    
Created by      : Vijay Joshi    
Date            : 14 june, 2005    
Purpose     : Get accounts from ACT_GENERAL_LEDGER which are not in posting interface    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
drop proc Proc_GetACT_GENERAL_LEDGER_ACCOUNTS_NOTIN_POSTING  
------   ------------       -------------------------*/    
---drop proc dbo.Proc_GetACT_GENERAL_LEDGER_ACCOUNTS_NOTIN_POSTING  1,2,'Y'
CREATE  Procedure dbo.Proc_GetACT_GENERAL_LEDGER_ACCOUNTS_NOTIN_POSTING    
(    
 @GL_ID int,    
 @FISCAL_ID int,  
 @ACC_JOURNAL_ENTRY char(1) =null   
)    
As    
BEGIN    
if  @ACC_JOURNAL_ENTRY is null  
begin  
/*SELECT ACCOUNT_ID, IsNull(ACC_DESCRIPTION,'')+ ' : ' + IsNull(ACC_DISP_NUMBER,'')As ACC_DESCRIPTION    
 FROM ACT_GL_ACCOUNTS    
 WHERE NOT exists  (    
  SELECT FISCAL_ID    
   FROM ACT_GENERAL_LEDGER     
   WHERE FISCAL_ID = @FISCAL_ID AND GL_ID = @GL_ID AND    
   (AST_UNCOLL_PRM_AGENCY = ACCOUNT_ID OR    
    AST_UNCOLL_PRM_CUSTOMER = ACCOUNT_ID OR    
    LIB_TAX_PAYB = ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = ACCOUNT_ID))    
  AND Upper(ACC_JOURNAL_ENTRY) = 'Y' AND Upper(IS_ACTIVE) = 'Y'    
  and ACC_LEVEL_TYPE='AS'    
 ORDER BY ACC_DISP_NUMBER    */
 SELECT t1.account_id, 
 case when t1.acc_parent_id is null   
  then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')    
  else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')  
  end as ACC_DESCRIPTION  
 FROM ACT_GL_ACCOUNTS t1   
 LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id   
WHERE NOT exists  (    
  SELECT FISCAL_ID    
   FROM ACT_GENERAL_LEDGER    
   WHERE FISCAL_ID = @FISCAL_ID AND GL_ID = @GL_ID AND    
   (AST_UNCOLL_PRM_AGENCY = t1.ACCOUNT_ID OR    
    AST_UNCOLL_PRM_CUSTOMER = t1.ACCOUNT_ID OR    
    LIB_TAX_PAYB = t1.ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = t1.ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = t1.ACCOUNT_ID))    
  AND Upper(t1.ACC_JOURNAL_ENTRY) = 'Y' AND Upper(t1.IS_ACTIVE) = 'Y'    
  and t1.ACC_LEVEL_TYPE='AS'    
 ORDER BY t1.ACC_DISP_NUMBER   
end  
else  
/*SELECT ACCOUNT_ID,   IsNull(ACC_DESCRIPTION,'')+ ' : ' + IsNull(ACC_DISP_NUMBER,'')As ACC_DESCRIPTION    
 FROM ACT_GL_ACCOUNTS    
 WHERE NOT exists  (    
  SELECT FISCAL_ID    
   FROM ACT_GENERAL_LEDGER     
   WHERE FISCAL_ID = @FISCAL_ID AND GL_ID = @GL_ID AND    
   (AST_UNCOLL_PRM_AGENCY = ACCOUNT_ID OR    
    AST_UNCOLL_PRM_CUSTOMER = ACCOUNT_ID OR    
    LIB_TAX_PAYB = ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = ACCOUNT_ID))    
  AND Upper(ACC_JOURNAL_ENTRY) = 'Y' AND Upper(IS_ACTIVE) = 'Y'    
  and ACC_LEVEL_TYPE='AS'    
  and ACC_JOURNAL_ENTRY='Y'  
 ORDER BY ACC_DISP_NUMBER    */
SELECT t1.account_id, 
 case when t1.acc_parent_id is null   
  then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')    
  else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'')  
  end as ACC_DESCRIPTION  
 FROM ACT_GL_ACCOUNTS t1   
 LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id   
WHERE NOT exists  (    
  SELECT FISCAL_ID    
   FROM ACT_GENERAL_LEDGER     
   WHERE FISCAL_ID = @FISCAL_ID AND GL_ID = @GL_ID AND    
   (AST_UNCOLL_PRM_AGENCY = t1.ACCOUNT_ID OR    
    AST_UNCOLL_PRM_CUSTOMER = t1.ACCOUNT_ID OR    
    LIB_TAX_PAYB = t1.ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = t1.ACCOUNT_ID OR    
    LIB_VENDOR_PAYB = t1.ACCOUNT_ID))    
  AND Upper(t1.ACC_JOURNAL_ENTRY) = 'Y' AND Upper(t1.IS_ACTIVE) = 'Y'    
  and t1.ACC_LEVEL_TYPE='AS'    
  and t1.ACC_JOURNAL_ENTRY='Y'  
 ORDER BY t1.ACC_DISP_NUMBER   
  
  
END    
    
    
 
  






GO

