IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBalanceSheet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBalanceSheet]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name        : dbo.Proc_GetBalanceSheet              
Created by       : Sukhveer Singh              
Date             : 19/09/2006              
Purpose          : Procedure to Get Assets and Liabilities (Balance Sheet).               
Revison History  :              
Used In          : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--DROP PROC dbo.Proc_GetBalanceSheet        
              
CREATE PROC dbo.Proc_GetBalanceSheet          
(              
/*Input parameters*/        
@GLID INT = NULL,        
@YEARFROM INT = NULL,      -- Valid year format YYYY        
@YEARTO INT = NULL,             
@MMONTH INT = NULL,    -- Valid month 1 to 12         
        
/*Memory Variables*/        
@MONTHNAME VARCHAR(8) = NULL,        
@QUERY VARCHAR(8000)= NULL,         
@SUBQUERY VARCHAR(8000)= NULL,      
@VIEW_QUERY VARCHAR(8000) = NULL        
          
)              
AS              
BEGIN              
              
select @MONTHNAME = CONVERT(VARCHAR,@MMONTH) + '/01/00'        
select @MONTHNAME = UPPER(SUBSTRING(DATENAME (MONTH, @MONTHNAME),1,3))        
        
/*Sub query to calculate MTD/YTD based on passed month parameter*/        
SET @SUBQUERY = 'SELECT GL_ID,ACCOUNT_ID,FISCAL_START_DATE,FISCAL_END_DATE,SUM(YEAR_'+ @MONTHNAME + '_MTD) AS YEAR_MTD, SUM(YEAR_' + @MONTHNAME + '_YTD) AS YEAR_YTD FROM ACT_GENERAL_LEDGER_TOTALS'         
        
        
DECLARE @WHERE VARCHAR(2000)              
SET @WHERE = ''        
        
        
IF NOT @GLID IS NULL              
BEGIN              
  SET @WHERE = 'GL_ID =' + CONVERT(VARCHAR,@GLID)              
END        
        
IF NOT @YEARFROM IS NULL  AND NOT @YEARTO IS NULL             
BEGIN              
 if ltrim(rtrim(@WHERE)) = ''          
  SET @WHERE = 'FISCAL_START_YEAR >= '+ CONVERT(VARCHAR,@YEARFROM) + ' AND FISCAL_END_YEAR <= ' + CONVERT(VARCHAR,@YEARTO)              
 else          
  SET @WHERE = @WHERE  + ' AND FISCAL_START_YEAR >= '+ CONVERT(VARCHAR,@YEARFROM) + ' AND FISCAL_END_YEAR <= ' + CONVERT(VARCHAR,@YEARTO)        
END         
        
IF(@WHERE <>'')            
BEGIN              
  SET @SUBQUERY = @SUBQUERY + ' WHERE ' + @WHERE              
END        
        
SET @SUBQUERY = @SUBQUERY + ' GROUP BY GL_ID,ACCOUNT_ID,FISCAL_START_DATE,FISCAL_END_DATE'        
        
SET @QUERY = 'GA.ACC_DISP_NUMBER AS ACC_NUMBER,GA.ACC_TYPE_ID AS ACC_TYPE,        
TM.ACC_TYPE_DESC,SUB1.YEAR_MTD,SUB2.PRIOR_YEAR_MTD,ISNULL(SUB1.YEAR_MTD,0) - ISNULL(SUB2.PRIOR_YEAR_MTD,0) AS VARIANCE_MTD,        
CHNG_MTD = CASE        
WHEN SUB1.YEAR_MTD IS NULL AND SUB2.PRIOR_YEAR_MTD IS NULL THEN CAST(0 as decimal(10,2))        
WHEN SUB1.YEAR_MTD IS NOT NULL AND SUB2.PRIOR_YEAR_MTD IS NULL THEN CAST(100 as decimal(10,2))        
ELSE CAST((SUB1.YEAR_MTD - SUB2.PRIOR_YEAR_MTD)* 100/(CASE WHEN SUB2.PRIOR_YEAR_MTD  =0 THEN 1 ELSE SUB2.PRIOR_YEAR_MTD  END) as decimal(10,2))       
END,        
SUB1.YEAR_YTD,SUB2.PRIOR_YEAR_YTD,ISNULL(SUB1.YEAR_YTD,0) - ISNULL(SUB2.PRIOR_YEAR_YTD,0) AS VARIANCE_YTD,        
CHNG_YTD = CASE        
WHEN SUB1.YEAR_YTD IS NULL AND SUB2.PRIOR_YEAR_YTD IS NULL THEN CAST(0 as decimal(10,2))        
WHEN SUB1.YEAR_YTD IS NOT NULL AND SUB2.PRIOR_YEAR_YTD IS NULL THEN CAST(100 as decimal(10,2))        
ELSE CAST((SUB1.YEAR_YTD - SUB2.PRIOR_YEAR_YTD)* 100/(CASE WHEN SUB2.PRIOR_YEAR_YTD  =0 THEN 1 ELSE SUB2.PRIOR_YEAR_YTD  END) as decimal(10,2))       
END,        
GL.LEDGER_NAME AS LEDGER_NAME,GA.ACC_DESCRIPTION AS ACC_DESC        
FROM (' + @SUBQUERY +') SUB1'        
        
IF NOT @YEARFROM IS NULL  AND NOT @YEARTO IS NULL        
BEGIN         
        
 SET @QUERY = @QUERY + ' LEFT OUTER JOIN (SELECT GL_ID,ACCOUNT_ID,SUM(YEAR_'+ @MONTHNAME + '_MTD) AS PRIOR_YEAR_MTD, SUM(YEAR_' + @MONTHNAME + '_YTD) AS PRIOR_YEAR_YTD FROM ACT_GENERAL_LEDGER_TOTALS WHERE FISCAL_START_YEAR >= ' + CONVERT(VARCHAR,@YEARFROM
  
    
-1) +' AND FISCAL_END_YEAR <= ' + CONVERT(VARCHAR,@YEARTO-1) +' GROUP BY GL_ID,ACCOUNT_ID) SUB2 ON SUB1.GL_ID = SUB2.GL_ID AND SUB1.ACCOUNT_ID = SUB2.ACCOUNT_ID'        
END        
        
SET @QUERY = @QUERY +' LEFT OUTER JOIN ACT_GENERAL_LEDGER GL ON SUB1.GL_ID = GL.GL_ID AND SUB1.FISCAL_START_DATE = GL.FISCAL_BEGIN_DATE AND SUB1.FISCAL_END_DATE = GL.FISCAL_END_DATE        
LEFT OUTER JOIN ACT_GL_ACCOUNTS GA ON SUB1.GL_ID = GA.GL_ID AND SUB1.ACCOUNT_ID = GA.ACCOUNT_ID        
LEFT OUTER JOIN ACT_TYPE_MASTER TM ON GA.ACC_TYPE_ID = TM.ACC_TYPE_ID'      
      
SET @VIEW_QUERY = 'SELECT SUB1.GL_ID AS LEDGER_ID,GL.EQU_TRANSFER AS ACCOUNT_ID,'+ @QUERY       
      
SET @QUERY = 'SELECT SUB1.GL_ID AS LEDGER_ID,SUB1.ACCOUNT_ID,' + @QUERY +' WHERE TM.ACC_TYPE_ID IN (1,2,3)'      
      
SET @VIEW_QUERY = 'CREATE VIEW VIW_BALANCE_SHEET AS ' + @VIEW_QUERY + ' WHERE TM.ACC_TYPE_ID IN (4,5)'      
        
IF EXISTS (SELECT name FROM sysobjects WHERE UPPER(name) = 'VIW_BALANCE_SHEET' AND type = 'V')      
DROP VIEW VIW_BALANCE_SHEET      
      
EXECUTE(@VIEW_QUERY)      
--Print @VIEW_QUERY    
      
SET @QUERY = @QUERY + ' UNION ALL'      
SET @QUERY = @QUERY + ' SELECT GLA.GL_ID AS LEDGER_ID,VBS.ACCOUNT_ID AS ACCOUNT_ID,GLA.ACC_DISP_NUMBER AS ACC_NUMBER,      
GLA.ACC_TYPE_ID AS ACC_TYPE,TYM.ACC_TYPE_DESC,VBS.YEAR_MTD,VBS.PRIOR_YEAR_MTD,VBS.VARIANCE_MTD,NULL AS CHNG_MTD,VBS.YEAR_YTD,      
VBS.PRIOR_YEAR_YTD,VBS.VARIANCE_YTD,NULL AS CHNG_YTD,VBS.LEDGER_NAME AS LEDGER_NAME,GLA.ACC_DESCRIPTION AS ACC_DESC      
FROM (SELECT ACCOUNT_ID,LEDGER_NAME,SUM(YEAR_MTD) AS YEAR_MTD,SUM(PRIOR_YEAR_MTD) AS PRIOR_YEAR_MTD,SUM(VARIANCE_MTD) AS VARIANCE_MTD,      
SUM(YEAR_YTD) AS YEAR_YTD,SUM(PRIOR_YEAR_YTD) AS PRIOR_YEAR_YTD,SUM(VARIANCE_YTD) AS VARIANCE_YTD from VIW_BALANCE_SHEET  GROUP BY ACCOUNT_ID,LEDGER_NAME) VBS      
LEFT OUTER JOIN ACT_GL_ACCOUNTS GLA ON VBS.ACCOUNT_ID = GLA.ACCOUNT_ID      
LEFT OUTER JOIN ACT_TYPE_MASTER TYM ON GLA.ACC_TYPE_ID = TYM.ACC_TYPE_ID  ORDER BY ACC_TYPE,ACC_NUMBER'      
      
--Print @QUERY       
EXECUTE(@QUERY)      
              
END              
      
--dbo.Proc_GetBalanceSheet      
      
--sp_helptext Proc_GetBalanceSheet 1, 2006,2006,1      
  



GO

