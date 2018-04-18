IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXML_ACT_BANK_RECONCILIATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXML_ACT_BANK_RECONCILIATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name       : dbo.ACT_CHECK_INFORMATION  
Created by      : Ajit Singh Chahal  
Date            : 6/30/2005  
Purpose       :To insert records in bank reconciliation.  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_GetXML_ACT_BANK_RECONCILIATION 39  
CREATE PROC [dbo].[Proc_GetXML_ACT_BANK_RECONCILIATION]  
(  
@AC_RECONCILIATION_ID INT  
)  
AS  
BEGIN  
-- SELECT  
-- (select distinct LEDGER_NAME from ACT_GENERAL_LEDGER)as GL_NAME,  
-- ACCOUNT_ID,  
-- convert(varchar,STATEMENT_DATE,101) as STATEMENT_DATE,  
-- STARTING_BALANCE,ENDING_BALANCE,BANK_CHARGES_CREDITS,AC_RECONCILIATION_ID  
-- FROM ACT_BANK_RECONCILIATION BR  
-- WHERE AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID  
  
SELECT  
(select distinct LEDGER_NAME from ACT_GENERAL_LEDGER)as GL_NAME,  
ACCOUNT_ID,  
convert(varchar,STATEMENT_DATE,101) as STATEMENT_DATE,  
STARTING_BALANCE,ENDING_BALANCE,BANK_CHARGES_CREDITS,BR.AC_RECONCILIATION_ID AS AC_RECONCILIATION_ID,  
UF.[FILE_ID],UF.[FILE_NAME] as UPLOAD_FILE,

CASE WHEN ISNULL(UF.[FILE_NAME],'') = '' THEN NULL
ELSE

(SELECT COUNT(REF_FILE_ID) FROM ACT_BANK_RECON_CHECK_FILE CL WHERE 
CL.REF_FILE_ID = UF.[FILE_ID]) 

END 
AS REF_FILE_ID_COUNT 

FROM ACT_BANK_RECONCILIATION BR  
LEFT JOIN ACT_BANK_RECON_UPLOAD_FILE UF ON UF.AC_RECONCILIATION_ID = BR.AC_RECONCILIATION_ID  
WHERE BR.AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID  
--and  
--UF.FILE_ID IN  
--(SELECT TOP 1 MAX(ACT_BANK_RECON_UPLOAD_FILE.FILE_ID) FROM ACT_BANK_RECON_UPLOAD_FILE)  
  
  
  
  
END  








  
  
  
  
  








GO

