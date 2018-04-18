IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTrialBalance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTrialBalance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name        : dbo.Proc_GetTrialBalance        
Created by       : Sukhveer Singh        
Date             : 01/09/2006        
Purpose          : Procedure to get MTD/YTD trail balance calculations.         
Revison History  :        
Used In          : Wolverine        

exec Proc_GetTrialBalance   1  , 2008 , 2008 , 3 

------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
  
-- DROP PROC Proc_GetTrialBalance        
CREATE PROC dbo.Proc_GetTrialBalance        
(        
/*Input parameters*/  
@GLID INT = NULL,  
@YEARFROM INT = NULL,      -- Valid year format YYYY  
@YEARTO INT = NULL,       
@MMONTH INT = NULL,    -- Valid month 1 to 12   
  
/*Memory Variables*/  
@MONTHNAME VARCHAR(8) = NULL,  
@QUERY VARCHAR(8000)= NULL,   
@SUBQUERY VARCHAR(8000)= NULL  
    
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
  
/*
SET @QUERY = 'SELECT SUB1.GL_ID AS LEDGER_ID,SUB1.ACCOUNT_ID AS ACC_ID,GA.ACC_DISP_NUMBER AS ACC_NUMBER,SUB1.YEAR_MTD AS YEAR_MTD,SUB1.YEAR_YTD AS YEAR_YTD,GL.LEDGER_NAME AS LEDGER_NAME,
case 
when GA.acc_parent_id is null 
then GA.ACC_DESCRIPTION  
else (select ACC_DESCRIPTION  from ACT_GL_ACCOUNTS with(nolock) where ACCOUNT_ID=GA.acc_parent_id) + '' - ''+  GA.ACC_DESCRIPTION 
end as ACC_DESCRIPTION
 
FROM (' + @SUBQUERY +') SUB1  
INNER JOIN ACT_GENERAL_LEDGER GL ON SUB1.GL_ID = GL.GL_ID AND SUB1.FISCAL_START_DATE = GL.FISCAL_BEGIN_DATE AND SUB1.FISCAL_END_DATE = GL.FISCAL_END_DATE  
INNER  JOIN ACT_GL_ACCOUNTS GA ON SUB1.GL_ID = GA.GL_ID AND SUB1.ACCOUNT_ID = GA.ACCOUNT_ID
LEFT OUTER JOIN  ACT_GL_ACCOUNTS t3 ON t3.account_id = ga.acc_parent_id  
ORDER BY ACC_NUMBER'  
*/
 

SET @QUERY = 'SELECT GA.GL_ID AS LEDGER_ID,GA.ACCOUNT_ID AS ACC_ID,GA.ACC_DISP_NUMBER AS ACC_NUMBER,
SUB1.YEAR_MTD  AS YEAR_MTD, SUB1.YEAR_YTD AS YEAR_YTD,
GL.LEDGER_NAME AS LEDGER_NAME,
case 
when GA.acc_parent_id is null 
then GA.ACC_DESCRIPTION  
else (select ACC_DESCRIPTION  from ACT_GL_ACCOUNTS with(nolock) where ACCOUNT_ID=GA.acc_parent_id) + '' - ''+  GA.ACC_DESCRIPTION 
end as ACC_DESCRIPTION	
FROM ACT_GL_ACCOUNTS GA  
INNER JOIN ACT_GENERAL_LEDGER GL
ON GA.GL_ID = GL.GL_ID AND YEAR(GL.FISCAL_BEGIN_DATE) >= ' 
+ CONVERT(VARCHAR,@YEARFROM)      
+ '  AND YEAR(GL.FISCAL_END_DATE) = ' + CONVERT(VARCHAR,@YEARTO) 
+ ' LEFT JOIN  (' + @SUBQUERY +') SUB1  
ON SUB1.GL_ID = GA.GL_ID AND SUB1.ACCOUNT_ID = GA.ACCOUNT_ID

ORDER BY ACC_NUMBER'  


--print @QUERY     
EXECUTE(@QUERY)        
END        





GO

