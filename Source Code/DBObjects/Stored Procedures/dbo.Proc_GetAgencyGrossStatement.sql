IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyGrossStatement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyGrossStatement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN  
--drop proc dbo.Proc_GetAgencyGrossStatement   
--GO  
  
/*----------------------------------------------------------                
Proc Name       : dbo.Proc_GetAgencyGrossStatement              
Created by      : Sukhveer Singh              
Date            : 21-09-2006              
Purpose         : To Fetch the agency summary details of specified agency of specified month/year              
Revison History :                
Used In         : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/             
            
-- drop proc dbo.Proc_GetAgencyGrossStatement          
          
-- Sample parameters          
-- Proc_GetAgencyGrossStatement 209,12,2007 ,'REG'          
-- dbo.Proc_GetAgencyGrossStatement '194,27',07,2006 ,'REG'         
      
-- Proc_GetAgencyGrossStatement 28,1,2008 ,'REG'          
              
CREATE PROC [dbo].[Proc_GetAgencyGrossStatement]              
(              
 @AGENCY_ID  varchar(8000),              
 @MONTH  INT = NULL,            --Valid month format mm            
 @YEAR   INT = NULL  ,   --Valid year format yyyy            
 @COMM_TYPE VARCHAR(20)  ,
 @CALLED_FROM VARCHAR(20) = null          
)              
AS             
DECLARE @QUERY VARCHAR(8000)              
DECLARE @SUBQUERY VARCHAR(8000)          
DECLARE @QUERY_COND VARCHAR(8000)          
             
BEGIN               
             
SELECT @QUERY = 'SELECT DISTINCT (AA.AGENCY_ID) AS AGENCY_ID,      
  AGL.AGENCY_CODE AS AGENCY_CODE,  
  ISNULL(AGL.ACCOUNT_ISVERIFIED1,0) AS ACCOUNT_ISVERIFIED1,      
  AGL.AGENCY_DISPLAY_NAME AS AGENCY_DISPLAY_NAME,            
  AGL.AGENCY_LIC_NUM AS AGENCY_LIC_NUM,      
  AGL.AGENCY_ADD1 AS AGENCY_ADD1,      
  AGL.AGENCY_ADD2 AS AGENCY_ADD2,      
  AGL.AGENCY_CITY AS AGENCY_CITY,            
  AGL.AGENCY_STATE AS AGENCY_STATE,      
  CST.STATE_NAME AS STATE_NAME,      
  AGL.AGENCY_ZIP AS AGENCY_ZIP,      
  AGL.AGENCY_COUNTRY AS AGENCY_COUNTRY,      
  ISNULL(AGL.ALLOWS_EFT,0) AS ALLOWS_EFT,            
  SUB1.PREMIUM_AMOUNT AS PREMIUM_AMOUNT_AB,      
  -SUB1.COMMISSION_AMOUNT AS COMMISSION_AMOUNT_AB,      
  SUB2.PREMIUM_AMOUNT AS PREMIUM_AMOUNT_DB,          
  -SUB2.COMMISSION_AMOUNT AS COMMISSION_AMOUNT_DB       
  FROM ACT_AGENCY_STATEMENT AA'            
          
SELECT @SUBQUERY = ' LEFT OUTER JOIN (      
         
    SELECT  TMP.AGENCY_ID , SUM (PREMIUM_AMOUNT) AS PREMIUM_AMOUNT ,       
    SUM (COMMISSION_AMOUNT) AS COMMISSION_AMOUNT      
    FROM      
    (       
    SELECT AAS.AGENCY_ID,       
    CASE TRAN_TYPE WHEN ''PREM'' THEN ISNULL(DUE_AMOUNT,0) ELSE 0 END  AS PREMIUM_AMOUNT,      
    AAS.COMMISSION_AMOUNT           
    FROM ACT_AGENCY_STATEMENT AAS WHERE AAS.COMM_TYPE = '''  + @COMM_TYPE + ''''            
        
SELECT @QUERY_COND = ''           
      
IF (@AGENCY_ID <> '0')                   
BEGIN                  
 SET @QUERY_COND = @QUERY_COND + ' AND AAS.AGENCY_ID IN (' + @AGENCY_ID + ')'          
END         
            
IF NOT @MONTH IS NULL                  
BEGIN                  
 SET @QUERY_COND = @QUERY_COND + ' AND AAS.MONTH_NUMBER = ' + CONVERT(VARCHAR,@MONTH)          
END            
            
IF NOT @YEAR IS NULL                  
BEGIN                  
 SET @QUERY_COND = @QUERY_COND + ' AND AAS.MONTH_YEAR = ' + CONVERT(VARCHAR,@YEAR)          
END            
           
SELECT @SUBQUERY = @SUBQUERY + ' AND AAS.BILL_TYPE = ''AB'''+ @QUERY_COND + ' ) TMP GROUP BY  TMP.AGENCY_ID) SUB1 ON SUB1.AGENCY_ID = AA.AGENCY_ID'          
      
SELECT @SUBQUERY = @SUBQUERY + '       
  LEFT OUTER JOIN (      
   SELECT  TMP.AGENCY_ID , SUM (PREMIUM_AMOUNT) AS PREMIUM_AMOUNT ,       
   SUM (COMMISSION_AMOUNT) AS COMMISSION_AMOUNT      
   FROM      
   (      
    SELECT AAS.AGENCY_ID,       
    CASE TRAN_CODE WHEN ''PIC'' THEN 0       
     ELSE ISNULL(STAT_FEES_FOR_CALCULATION,0)       
    +  ISNULL(AMOUNT_FOR_CALCULATION,0) END AS PREMIUM_AMOUNT,      
    AAS.COMMISSION_AMOUNT    
    FROM ACT_AGENCY_STATEMENT AAS       
    WHERE AAS.BILL_TYPE = ''DB'' AND AAS.COMM_TYPE = '''  + @COMM_TYPE + ''''        
      
    + @QUERY_COND +' ) TMP GROUP BY  TMP.AGENCY_ID) SUB2 ON SUB2.AGENCY_ID = AA.AGENCY_ID'            
        
--Condition Added For Itrack Issue #4615. 
IF(@CALLED_FROM = 'ASR')
BEGIN
 SELECT @QUERY = @QUERY + @SUBQUERY +' Inner JOIN MNT_AGENCY_LIST AGL ON AGL.AGENCY_ID = AA.AGENCY_ID           
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST CST ON CST.STATE_ID = AGL.AGENCY_STATE'        
    +' WHERE AA.COMM_TYPE = '''  + @COMM_TYPE + '''' + 'AND (ISNULL(SUB1.PREMIUM_AMOUNT,0.00) - ISNULL(SUB1.COMMISSION_AMOUNT,0.00) - ISNULL(SUB2.COMMISSION_AMOUNT,0.00)) <> 0.00 '          
    + ' AND AA.MONTH_NUMBER = ' +  CONVERT(VARCHAR,@MONTH)  + ' AND AA.MONTH_YEAR = '   +  CONVERT(VARCHAR,@YEAR)    
END

ELSE
BEGIN
   SELECT @QUERY = @QUERY + @SUBQUERY +' Inner JOIN MNT_AGENCY_LIST AGL ON AGL.AGENCY_ID = AA.AGENCY_ID           
   LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST CST ON CST.STATE_ID = AGL.AGENCY_STATE'        
    +' WHERE AA.COMM_TYPE = '''  + @COMM_TYPE + ''''           
    + ' AND AA.MONTH_NUMBER = ' +  CONVERT(VARCHAR,@MONTH)  + ' AND AA.MONTH_YEAR = '   +  CONVERT(VARCHAR,@YEAR)    
END

IF (@AGENCY_ID <> '0')          
BEGIN        
 SELECT  @QUERY = @QUERY + ' AND AA.AGENCY_ID IN (' + @AGENCY_ID + ')'         
END        
        
--SELECT @QUERY = @QUERY + ' ORDER BY SUB1.AGENCY_ID'            
SELECT @QUERY = @QUERY + ' ORDER BY AGENCY_DISPLAY_NAME , AGENCY_CODE'                    
print @QUERY                        
EXECUTE(@QUERY)             
              
END              
  
  
--GO  
--EXEC  Proc_GetAgencyGrossStatement 0, 2 ,2009 ,'REG','ASR'  
--ROLLBACK TRAN  
  
  
  
  



GO

