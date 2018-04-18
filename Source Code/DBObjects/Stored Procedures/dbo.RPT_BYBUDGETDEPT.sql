IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RPT_BYBUDGETDEPT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RPT_BYBUDGETDEPT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------          
PROC NAME        : DBO.RPT_BYBUDGETDEPT           
CREATED BY       : ARUN DHINGRA          
DATE             : 21/11/2007          
PURPOSE          : PROCEDURE TO SHOW BUDGET CATEGORY REPORT by Dept.           
REVISON HISTORY  :          
USED IN          : WOLVERINE          
------------------------------------------------------------          
DATE     REVIEW BY          COMMENTS          
------   ------------       -------------------------*/          
  
-- DROP PROC DBO.RPT_BYBUDGETDEPT   
-- DBO.RPT_BYBUDGETDEPT null,Null,NULL,NULL,10  
-- DBO.RPT_BYBUDGETDEPT 7,Null,2007,2007,10
  
CREATE PROC DBO.RPT_BYBUDGETDEPT         
(          
@BCID INT = NULL,    
@FISCALID INT = NULL,   
@YEARFROM INT = NULL,  
@YEARTO INT = NULL,      
@MONTH INT = NULL
)          
  
AS          
  
BEGIN    
  
 DECLARE @QUERY VARCHAR(8000)  
 SET @QUERY =''     
   
 DECLARE @SUBQUERY VARCHAR(8000)  
 SET @SUBQUERY =''  
   
 DECLARE @WHERE VARCHAR(8000)          
 SET @WHERE = ''    
   
 IF NOT @BCID IS NULL          
 BEGIN          
   SET @WHERE = ' WHERE A.ACC_LEVEL_TYPE = ''AS'' AND A.WOLVERINE_USER_ID =' + CONVERT(VARCHAR,@BCID)          
 END    
   
 IF NOT @FISCALID IS NULL         
 BEGIN          
  IF LTRIM(RTRIM(@WHERE)) = ''      
   SET @WHERE = ' WHERE B.FISCAL_ID = ' + CONVERT(VARCHAR,@FISCALID)  
  ELSE      
   SET @WHERE = @WHERE  + ' AND B.FISCAL_ID = ' + CONVERT(VARCHAR,@FISCALID)  
 END    
   
 IF NOT @YEARFROM IS NULL         
 BEGIN          
  IF LTRIM(RTRIM(@SUBQUERY)) = ''      
   SET @SUBQUERY = ' WHERE FISCAL_START_YEAR = ' + CONVERT(VARCHAR,@YEARFROM)  
  ELSE      
   SET @SUBQUERY = @SUBQUERY  + ' AND FISCAL_START_YEAR = ' + CONVERT(VARCHAR,@YEARFROM)  
 END    
   
 IF NOT @YEARTO IS NULL         
 BEGIN          
  IF LTRIM(RTRIM(@SUBQUERY)) = ''      
   SET @SUBQUERY = ' WHERE FISCAL_END_YEAR = ' + CONVERT(VARCHAR,@YEARTO)  
  ELSE      
   SET @SUBQUERY = @SUBQUERY  + ' AND FISCAL_END_YEAR = ' + CONVERT(VARCHAR,@YEARTO)  
 END   
   
 SET @QUERY = 'SELECT (ISNULL(USER_FNAME,'''') + '' '' + ISNULL(USER_LNAME,'''')) as WOLVERINE_USERS,C.USER_ID,B.FISCAL_ID,A.ACC_DESCRIPTION as ACC_DESCRIPTION_OLD,case when A.acc_parent_id is null   
  then A.ACC_DESCRIPTION + '' : '' +  isnull(A.ACC_DISP_NUMBER,'''')    
  else  isnull(A1.acc_description,'''') + '' - '' + A.ACC_DESCRIPTION  + '' : '' + isnull(A.ACC_DISP_NUMBER,'''')  
  end as ACC_DESCRIPTION,    
      (JAN_BUDGET +FEB_BUDGET +MARCH_BUDGET+APRIL_BUDGET+MAY_BUDGET+JUNE_BUDGET+JULY_BUDGET+AUG_BUDGET+SEPT_BUDGET+OCT_BUDGET+NOV_BUDGET+DEC_BUDGET) AS BUDGETAMOUNT,  
   CASE '+CAST(@MONTH  AS VARCHAR) +'
   WHEN 1 THEN  
   ISNULL(JAN_BUDGET,0)  
   WHEN 2 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET),0)  
   WHEN 3 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET),0)  
   WHEN 4 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET + APRIL_BUDGET),0)  
   WHEN 5 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET + APRIL_BUDGET + MAY_BUDGET),0)  
   WHEN 6 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET + APRIL_BUDGET + MAY_BUDGET + JUNE_BUDGET),0)  
   WHEN 7 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET + APRIL_BUDGET + MAY_BUDGET + JUNE_BUDGET + JULY_BUDGET),0)  
   WHEN 8 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET+ APRIL_BUDGET+ MAY_BUDGET+ JUNE_BUDGET+ JULY_BUDGET+ AUG_BUDGET),0)  
   WHEN 9 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET+ APRIL_BUDGET+ MAY_BUDGET+ JUNE_BUDGET+ JULY_BUDGET+ AUG_BUDGET+ SEPT_BUDGET),0)  
   WHEN 10 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET+ APRIL_BUDGET+ MAY_BUDGET+ JUNE_BUDGET+ JULY_BUDGET+ AUG_BUDGET+ SEPT_BUDGET+ OCT_BUDGET),0)  
   WHEN 11 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET+ APRIL_BUDGET+ MAY_BUDGET+ JUNE_BUDGET+ JULY_BUDGET+ AUG_BUDGET+ SEPT_BUDGET+ OCT_BUDGET+ NOV_BUDGET),0)  
   WHEN 12 THEN  
   ISNULL((JAN_BUDGET + FEB_BUDGET + MARCH_BUDGET+ APRIL_BUDGET+ MAY_BUDGET+ JUNE_BUDGET+ JULY_BUDGET+ AUG_BUDGET+ SEPT_BUDGET+ OCT_BUDGET+ NOV_BUDGET+DEC_BUDGET),0)  
   END   
   AS BUDGETUPTOAMOUNT,     
   ISNULL(
   CASE '+CAST(@MONTH  AS VARCHAR) +' 
   WHEN 1 THEN  
   ISNULL(YEAR_JAN_YTD,0)  
   WHEN 2 THEN  
   ISNULL(YEAR_FEB_YTD,0)  
   WHEN 3 THEN  
   ISNULL(YEAR_MAR_YTD,0)  
   WHEN 4 THEN  
   ISNULL(YEAR_APR_YTD,0)  
   WHEN 5 THEN  
   ISNULL(YEAR_MAY_YTD,0)  
   WHEN 6 THEN  
   ISNULL(YEAR_JUN_YTD,0)  
   WHEN 7 THEN  
   ISNULL(YEAR_JUL_YTD,0)  
   WHEN 8 THEN  
   ISNULL(YEAR_AUG_YTD,0)  
   WHEN 9 THEN  
   ISNULL(YEAR_SEP_YTD,0)  
   WHEN 10 THEN  
   ISNULL(YEAR_OCT_YTD,0)  
   WHEN 11 THEN  
   ISNULL(YEAR_NOV_YTD,0)  
   WHEN 12 THEN  
   ISNULL(YEAR_DEC_YTD,0)  
   END  ,0 )
   AS ACTUAL_AMT,
   YEAR(E.FISCAL_BEGIN_DATE) AS FISCAL_BEGIN_YEAR,
   YEAR(E.FISCAL_END_DATE) AS FISCAL_END_YEAR  
   
   FROM ACT_GL_ACCOUNTS A  
   INNER JOIN ACT_BUDGET_PLAN B  
   ON A.ACCOUNT_ID = B.ACCOUNT_ID   
   INNER JOIN MNT_USER_LIST C  
   ON A.WOLVERINE_USER_ID = C.USER_ID
   INNER JOIN ACT_GENERAL_LEDGER E ON E.FISCAL_ID = B.FISCAL_ID
   LEFT OUTER JOIN ACT_GENERAL_LEDGER_TOTALS D 
   ON D.ACCOUNT_ID=B.ACCOUNT_ID 
   AND E.FISCAL_BEGIN_DATE = D.FISCAL_START_DATE
   AND E.FISCAL_END_DATE  = D.FISCAL_END_DATE 
   LEFT OUTER JOIN  ACT_GL_ACCOUNTS A1 ON A1.ACCOUNT_ID = A.ACC_PARENT_ID '
  
   IF(@WHERE <>'')        
   BEGIN          
     SET @QUERY = @QUERY + @WHERE          
   END   
     
 --  PRINT @QUERY       
   EXECUTE(@QUERY)   
     
END   
  
-- FROM ACT_GL_ACCOUNTS A  
--    INNER JOIN ACT_BUDGET_PLAN B  
--    ON A.ACCOUNT_ID = B.ACCOUNT_ID   
--    INNER JOIN ACT_BUDGET_CATEGORY C  
--    ON A.BUDGET_CATEGORY_ID = C.CATEGEORY_ID
--    INNER JOIN ACT_GENERAL_LEDGER E ON E.FISCAL_ID = B.FISCAL_ID	
--    INNER JOIN ACT_GENERAL_LEDGER_TOTALS D 
-- 	ON D.ACCOUNT_ID=B.ACCOUNT_ID 
-- 	AND E.FISCAL_BEGIN_DATE = D.FISCAL_START_DATE
-- 	AND E.FISCAL_END_DATE  = D.FISCAL_END_DATE 
--   WHERE A.BUDGET_CATEGORY_ID =7
















GO

