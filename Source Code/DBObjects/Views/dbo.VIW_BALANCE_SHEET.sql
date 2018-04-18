IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VIW_BALANCE_SHEET]'))
DROP VIEW [dbo].[VIW_BALANCE_SHEET]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW VIW_BALANCE_SHEET AS SELECT SUB1.GL_ID AS LEDGER_ID,SUB1.ACCOUNT_ID,GA.ACC_DISP_NUMBER AS ACC_NUMBER,GA.ACC_TYPE_ID AS ACC_TYPE,  
TM.ACC_TYPE_DESC,SUB1.YEAR_MTD,SUB2.PRIOR_YEAR_MTD,ISNULL(SUB1.YEAR_MTD,0) - ISNULL(SUB2.PRIOR_YEAR_MTD,0) AS VARIANCE_MTD,  
CHNG_MTD = CASE  
WHEN SUB1.YEAR_MTD IS NULL AND SUB2.PRIOR_YEAR_MTD IS NULL THEN CAST(0 as decimal(10,2))  
WHEN SUB1.YEAR_MTD IS NOT NULL AND SUB2.PRIOR_YEAR_MTD IS NULL THEN CAST(100 as decimal(10,2))  
ELSE CAST((SUB1.YEAR_MTD - SUB2.PRIOR_YEAR_MTD)* 100/SUB2.PRIOR_YEAR_MTD as decimal(10,2)) 
END,  
SUB1.YEAR_YTD,SUB2.PRIOR_YEAR_YTD,ISNULL(SUB1.YEAR_YTD,0) - ISNULL(SUB2.PRIOR_YEAR_YTD,0) AS VARIANCE_YTD,  
CHNG_YTD = CASE  
WHEN SUB1.YEAR_YTD IS NULL AND SUB2.PRIOR_YEAR_YTD IS NULL THEN CAST(0 as decimal(10,2))  
WHEN SUB1.YEAR_YTD IS NOT NULL AND SUB2.PRIOR_YEAR_YTD IS NULL THEN CAST(100 as decimal(10,2))  
ELSE CAST((SUB1.YEAR_YTD - SUB2.PRIOR_YEAR_YTD)* 100/SUB2.PRIOR_YEAR_YTD as decimal(10,2)) 
END,  
GL.LEDGER_NAME AS LEDGER_NAME,GA.ACC_DESCRIPTION AS ACC_DESC  
FROM (SELECT GL_ID,ACCOUNT_ID,FISCAL_START_DATE,FISCAL_END_DATE,SUM(YEAR_SEP_MTD) AS YEAR_MTD, SUM(YEAR_SEP_YTD) AS YEAR_YTD FROM ACT_GENERAL_LEDGER_TOTALS WHERE GL_ID =1 AND FISCAL_START_YEAR >= 2006 AND FISCAL_END_YEAR <= 2006 GROUP BY GL_ID,ACCOUNT_ID,FISCAL_START_DATE,FISCAL_END_DATE) SUB1 LEFT OUTER JOIN (SELECT GL_ID,ACCOUNT_ID,SUM(YEAR_SEP_MTD) AS PRIOR_YEAR_MTD, SUM(YEAR_SEP_YTD) AS PRIOR_YEAR_YTD FROM ACT_GENERAL_LEDGER_TOTALS WHERE FISCAL_START_YEAR >= 2005 AND FISCAL_END_YEAR <= 2005 GROUP BY GL_ID,ACCOUNT_ID) SUB2 ON SUB1.GL_ID = SUB2.GL_ID AND SUB1.ACCOUNT_ID = SUB2.ACCOUNT_ID LEFT OUTER JOIN ACT_GENERAL_LEDGER GL ON SUB1.GL_ID = GL.GL_ID AND SUB1.FISCAL_START_DATE = GL.FISCAL_BEGIN_DATE AND SUB1.FISCAL_END_DATE = GL.FISCAL_END_DATE  
LEFT OUTER JOIN ACT_GL_ACCOUNTS GA ON SUB1.GL_ID = GA.GL_ID AND SUB1.ACCOUNT_ID = GA.ACCOUNT_ID  
LEFT OUTER JOIN ACT_TYPE_MASTER TM ON GA.ACC_TYPE_ID = TM.ACC_TYPE_ID WHERE TM.ACC_TYPE_ID IN (4,5)
GO

