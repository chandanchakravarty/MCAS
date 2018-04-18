IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDefaultAccountForTranCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDefaultAccountForTranCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetDefaultAccountForTranCode    
Created by      : Swastika Gaur 
Date            :     
Purpose         :To fetch default Account ID on basis of Transation Code    
Revison History :    
Used In  : Wolverine    
----------------------    
AB Commission--		"AB-COMM"
AB Premium--		"AB-PREM"
DB Commission--		"DB-COMM"
Other--				"OTHER"
Billing Fee--		"BILL-FEE"
DB Premium--		"DB-PREM"
Late Fee--			"LATE-FEE"
NSF--				"NSF"
Reinstatement Fee--	"REINS-FEE"
------   ------------       -------------------------*/    
-- DROP PROC dbo.Proc_GetDefaultAccountForTranCode    
CREATE PROC dbo.Proc_GetDefaultAccountForTranCode    
(    
 @TRAN_CODE nvarchar(10)   
)    
AS    
BEGIN    
	IF(@TRAN_CODE ='AB-COMM') 
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'AB-COMM',AGA.ACCOUNT_ID
	    	FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.LIB_COMM_PAYB_AGENCY_BILL*/
 		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'AB-COMM' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.LIB_COMM_PAYB_AGENCY_BILL

	END

	IF(@TRAN_CODE='AB-PREM') 
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'AB-PREM',AGA.ACCOUNT_ID
		FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.AST_UNCOLL_PRM_AGENCY*/
		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'AB-PREM' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.AST_UNCOLL_PRM_AGENCY
	END

	IF(@TRAN_CODE='DB-COMM')
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'DB-COMM',AGA.ACCOUNT_ID
		FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.LIB_COMM_PAYB_DIRECT_BILL*/
		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'DB-COMM' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.LIB_COMM_PAYB_DIRECT_BILL
	END

	IF(@TRAN_CODE='BILL-FEE')
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'BILL-FEE',AGA.ACCOUNT_ID
		FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.INC_INSTALLMENT_FEES	*/
		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'BILL-FEE' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.INC_INSTALLMENT_FEES
		
	END

	IF(@TRAN_CODE='DB-PREM')
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'DB-PREM',AGA.ACCOUNT_ID
		FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.AST_UNCOLL_PRM_CUSTOMER*/
		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'DB-PREM' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.AST_UNCOLL_PRM_CUSTOMER

	END

	IF(@TRAN_CODE='LATE-FEE')
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'LATE-FEE',AGA.ACCOUNT_ID
		FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.INC_LATE_FEES*/
		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'LATE-FEE' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.INC_LATE_FEES
	END

	IF(@TRAN_CODE='NSF')
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'NSF',AGA.ACCOUNT_ID
		FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.INC_NON_SUFFICIENT_FUND_FEES*/
		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'NSF' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.INC_NON_SUFFICIENT_FUND_FEES
		
	END

	IF(@TRAN_CODE='REINS-FEE')
	BEGIN
		/*SELECT DISTINCT AGA.ACC_DISP_NUMBER + ' - ' + AGA.ACC_DESCRIPTION AS 'REINS-FEE',AGA.ACCOUNT_ID
		FROM ACT_GENERAL_LEDGER AGL INNER JOIN ACT_GL_ACCOUNTS AGA
		ON AGA.ACCOUNT_ID = AGL.INC_RE_INSTATEMENT_FEES*/
		SELECT DISTINCT
		CASE WHEN T1.ACC_PARENT_ID IS NULL   
		THEN T1.ACC_DESCRIPTION + ' : ' +  ISNULL(T1.ACC_DISP_NUMBER,'')    
		ELSE  ISNULL(T2.ACC_DESCRIPTION,'') + ' - ' + T1.ACC_DESCRIPTION  + ' : ' + ISNULL(T1.ACC_DISP_NUMBER,'')  
		END AS 'REINS-FEE' ,T1.ACCOUNT_ID
		FROM ACT_GL_ACCOUNTS T1   
		LEFT OUTER JOIN  ACT_GL_ACCOUNTS T2 ON T2.ACCOUNT_ID = T1.ACC_PARENT_ID   
		INNER JOIN ACT_GENERAL_LEDGER AGL
		ON T1.ACCOUNT_ID = AGL.INC_RE_INSTATEMENT_FEES

	END
END










GO

