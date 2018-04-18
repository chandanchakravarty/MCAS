IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_RPTPendingAgencyStatements]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_RPTPendingAgencyStatements]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran  
--drop proc dbo.Proc_RPTPendingAgencyStatements     
--go  
/*---------------------------------------------------------------------------          
CREATE BY    : Arun Dhingra        
CREATE DATETIME : 2 Aug 2007      
PURPOSE     :     
REVIOSN HISTORY          
Revised By  Date  Reason       
praveen : Comm Type Itrack 3812       
    
Proc_RPTPendingAgencyStatements 8,2007,'Wolverine'    
Proc_RPTPendingAgencyStatements 11,2007    
exec Proc_RPTPendingAgencyStatements @MONTH = 0, @YEAR = 0, @AGENCY_NAME = NULL    
----------------------------------------------------------------------------*/          
--drop proc dbo.Proc_RPTPendingAgencyStatements 1,2008,null,"reg"     
CREATE  PROC dbo.Proc_RPTPendingAgencyStatements     
(          
 @MONTH  INT =null,          
 @YEAR  INT  =null,        
 @AGENCY_NAME VARCHAR(100) = null  ,      
 @COMM_TYPE VARCHAR(10) = null        
)          
AS    
      
BEGIN       
SELECT   
 CASE WHEN ISNULL(VW_AGS.AMOUNT,0) - ISNULL(VW_AGS.TOTAL_PAID,0)  > 0 THEN 'Deposit' ELSE 'Check' END AS TYPE,   
 --ISNULL(AMOUNT,0) AS STMT_AMOUNT,
    --Amount Format For Itrack Issue #5426. 
	convert(varchar(30),convert(money,isnull(AMOUNT,0)),1) as STMT_AMOUNT,  
	convert(varchar(30),convert(money,ISNULL(VW_AGS.AMOUNT,0)  - ISNULL(VW_AGS.TOTAL_PAID,0)),1) AS BALANCE_AMOUNT,   
	convert(varchar(30),convert(money,ISNULL(DB_COMMISSION,0)),1) AS COMMISSION_AMOUNT_DB,   
	convert(varchar(30),convert(money,ISNULL(AB_COMMISSION,0)),1) AS COMMISSION_AMOUNT_AB,   
	convert(varchar(30),convert(money,ISNULL(AB_PREMIUM,0)),1)    AS PREMIUM_AMOUNT_AB ,  
    ISNULL(VW_AGS.AGENCY_ID,0) AS AGENCY_ID ,  
    ISNULL(AGN.AGENCY_DISPLAY_NAME,'')  AS AGENCY_DISPLAY_NAME,  
    ISNULL(AGN.AGENCY_CODE,0) AS AGENCY_CODE ,    
    ISNULL(VW_AGS.[MONTH],0) ,  
    ISNULL(VW_AGS.[YEAR],0) ,   
    CONVERT(VARCHAR(30),VW_AGS.MONTH) + ' ' +  CONVERT(VARCHAR(20),VW_AGS.YEAR) AS MONTHYEAR       
  FROM   
 VW_AGENCYSTATEMENTGROSSAMOUNT VW_AGS   
 INNER JOIN MNT_AGENCY_LIST AGN   
 ON VW_AGS.AGENCY_ID = AGN.AGENCY_ID          
 WHERE VW_AGS.MONTH = @MONTH   
 AND VW_AGS.YEAR = @YEAR    
 AND VW_AGS.COMM_TYPE = @COMM_TYPE   
 AND ISNULL(VW_AGS.AMOUNT,0)  - ISNULL(VW_AGS.TOTAL_PAID,0)  <> 0    
 AND AGN.AGENCY_DISPLAY_NAME like +'%' + isnull(@AGENCY_NAME,AGN.AGENCY_DISPLAY_NAME) + '%' 
 order by AGN.AGENCY_DISPLAY_NAME   
--     
--DECLARE @WHEREPARAM VARCHAR(2000)                        
--SET @WHEREPARAM = ''      
--    
-- SELECT 'Check' AS TYPE,    
-- AAS.AGENCY_ID,AGENCY.AGENCY_DISPLAY_NAME,AGENCY.AGENCY_CODE,    
-- AAS.MONTH_NUMBER AS [MONTH],AAS.MONTH_YEAR AS [YEAR],    
-- CONVERT(VARCHAR(30),AAS.MONTH_NUMBER) + ' ' +  CONVERT(VARCHAR(20),AAS.MONTH_YEAR) AS MONTHYEAR,     
-- SUM(ISNULL(AAS.DUE_AMOUNT,0))  * -1 AS STMT_AMOUNT,    
--(SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0))) * -1  AS BALANCE_AMOUNT,  
-- convert(varchar(30),convert(money,VW_AB.AMOUNT),1) COMMISSION_AMOUNT_AB,  
-- convert(varchar(30),convert(money,VW_DB.AMOUNT),1) COMMISSION_AMOUNT_DB,  
-- convert(varchar(30),convert(money,VW_PREM.AMOUNT),1) PREMIUM_AMOUNT_AB  
--    
--  
----convert(varchar(30),convert(money,ACI.CHECK_AMOUNT),1) PAYMENT_AMOUNT,   
--     
--  FROM ACT_AGENCY_STATEMENT AAS   with(nolock)  
--  LEFT JOIN MNT_AGENCY_LIST AGENCY  with(nolock) ON AGENCY.AGENCY_ID = AAS.AGENCY_ID   
--   
--  LEFT OUTER  JOIN (  
--                 SELECT AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--     FROM VW_AGENCYSTATEMENTGROSSAMOUNT with(nolock)  
--     WHERE BILL_TYPE = 'AB' AND TRAN_TYPE = 'COM'  
--                 GROUP BY AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--                 ) VW_AB   
--  
--     ON VW_AB.AGENCY_ID  = AAS.AGENCY_ID AND  
--     VW_AB.YEAR    = AAS.MONTH_YEAR AND   
--     VW_AB.MONTH   = AAS.MONTH_NUMBER    
--  
--  
--  LEFT OUTER  JOIN (  
--                 SELECT AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--     FROM VW_AGENCYSTATEMENTGROSSAMOUNT with(nolock)  
--     where BILL_TYPE = 'DB' and TRAN_TYPE = 'COM'  
--                 GROUP BY AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--                 ) VW_DB   
--  
--     ON VW_DB.AGENCY_ID  = AAS.AGENCY_ID and  
--     VW_DB.YEAR    = AAS.MONTH_YEAR and   
--     VW_DB.MONTH   = AAS.MONTH_NUMBER   
--  
--  
--  LEFT OUTER  JOIN (  
--                 SELECT AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--     FROM VW_AGENCYSTATEMENTGROSSAMOUNT with(nolock)  
--     where BILL_TYPE = 'AB' and TRAN_TYPE = 'PREM'  
--                 GROUP BY AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--                 ) VW_PREM  
--  
--     ON VW_PREM.AGENCY_ID  = AAS.AGENCY_ID and  
--     VW_PREM.YEAR    = AAS.MONTH_YEAR and   
--     VW_PREM.MONTH   = AAS.MONTH_NUMBER   
--  
--   
--   
-- WHERE AAS.MONTH_NUMBER = isnull(@MONTH,AAS.MONTH_NUMBER)      
-- AND AAS.MONTH_YEAR = isnull(@YEAR,AAS.MONTH_YEAR)     
-- AND AGENCY.AGENCY_DISPLAY_NAME like +'%' + isnull(@AGENCY_NAME,AGENCY.AGENCY_DISPLAY_NAME) + '%'    
-- AND ISNULL(AAS.IS_CHECK_CREATED,'N') <> 'Y'    
-- AND AAS.COMM_TYPE = @COMM_TYPE    
-- --AND AAS.COMM_TYPE=@COMM_TYPE     
-- GROUP BY AAS.AGENCY_ID,AAS.MONTH_NUMBER,AAS.MONTH_YEAR,    
-- AGENCY.AGENCY_DISPLAY_NAME,AGENCY.AGENCY_CODE,AGENCY.ALLOWS_EFT ,VW_AB.AMOUNT,VW_DB.AMOUNT ,VW_PREM.AMOUNT  
-- HAVING (SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0)))< 0    
--     
--    
--UNION    
--    
--SELECT  'Deposit' AS TYPE,    
-- AGS.AGENCY_ID,   
-- LTRIM(RTRIM(AGENCY.AGENCY_DISPLAY_NAME)) as AGENCY_DISPLAY_NAME ,AGENCY.AGENCY_CODE,        
-- AGS.MONTH_NUMBER AS [MONTH],        
-- AGS.MONTH_YEAR AS [YEAR],        
-- convert(varchar(30),AGS.MONTH_NUMBER) + ' ' +        
-- convert(varchar(20),AGS.MONTH_YEAR) AS MONTHYEAR ,        
-- sum(ISNULL(AGS.DUE_AMOUNT,0)) AS STMT_AMOUNT,        
-- sum(ISNULL(AGS.DUE_AMOUNT,0)) - sum(ISNULL(AGS.TOTAL_PAID,0)) as BALANCE_AMOUNT,  
--convert(varchar(30),convert(money,VW_AB.AMOUNT),1) COMMISSION_AMOUNT_AB,  
-- convert(varchar(30),convert(money,VW_DB.AMOUNT),1) COMMISSION_AMOUNT_DB,  
-- convert(varchar(30),convert(money,VW_PREM.AMOUNT),1) PREMIUM_AMOUNT_AB     
--     
-- from ACT_AGENCY_STATEMENT AGS   with(nolock)      
-- LEFT JOIN MNT_AGENCY_LIST AGENCY  with(nolock) ON AGENCY.AGENCY_ID = AGS.AGENCY_ID    
--  
-- LEFT OUTER  JOIN (  
--                 SELECT AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--     FROM VW_AGENCYSTATEMENTGROSSAMOUNT with(nolock)  
--     WHERE BILL_TYPE = 'AB' AND TRAN_TYPE = 'COM'  
--                 GROUP BY AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--                 ) VW_AB   
--  
--     ON VW_AB.AGENCY_ID  = AGS.AGENCY_ID AND  
--     VW_AB.YEAR    = AGS.MONTH_YEAR AND   
--     VW_AB.MONTH   = AGS.MONTH_NUMBER    
--  
--  
--  LEFT OUTER  JOIN  (  
--                 SELECT AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--     FROM VW_AGENCYSTATEMENTGROSSAMOUNT with(nolock)  
--     WHERE BILL_TYPE = 'DB' AND TRAN_TYPE = 'COM'  
--                 GROUP BY AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--                 ) VW_DB   
--  
--     ON VW_DB.AGENCY_ID  = AGS.AGENCY_ID AND  
--     VW_DB.YEAR    = AGS.MONTH_YEAR AND   
--     VW_DB.MONTH   = AGS.MONTH_NUMBER   
--  
--  LEFT OUTER  JOIN  (  
--                 SELECT AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--     FROM VW_AGENCYSTATEMENTGROSSAMOUNT with(nolock)  
--     WHERE BILL_TYPE = 'AB' AND TRAN_TYPE = 'PREM'  
--                 GROUP BY AGENCY_ID, MONTH , YEAR ,BILL_TYPE ,TRAN_TYPE , COMM_TYPE, AMOUNT, TOTAL_PAID  
--                 ) VW_PREM   
--  
--     ON VW_PREM.AGENCY_ID  = AGS.AGENCY_ID AND  
--     VW_PREM.YEAR    = AGS.MONTH_YEAR AND   
--     VW_PREM.MONTH   = AGS.MONTH_NUMBER   
--      
-- WHERE AGS.MONTH_NUMBER = isnull(@MONTH,AGS.MONTH_NUMBER)    
-- AND AGS.MONTH_YEAR = isnull(@YEAR,AGS.MONTH_YEAR)       
-- AND AGENCY.AGENCY_DISPLAY_NAME like + '%' + isnull(@AGENCY_NAME,AGENCY.AGENCY_DISPLAY_NAME) + '%'    
-- AND AGS.COMM_TYPE = @COMM_TYPE    
-- --AND CASE when @COMM_TYPE = 'OP' then AGS.ITEM_STATUS = @COMM_TYPE else  AGS.COMM_TYPE = @COMM_TYPE end  
-- GROUP BY AGS.AGENCY_ID,AGENCY.AGENCY_DISPLAY_NAME ,AGENCY.AGENCY_CODE,AGS.MONTH_NUMBER,AGS.MONTH_YEAR ,VW_AB.AMOUNT,VW_DB.AMOUNT,VW_PREM.AMOUNT       
-- HAVING (SUM(ISNULL(AGS.DUE_AMOUNT,0)) - SUM(ISNULL(AGS.TOTAL_PAID,0))) > 0      
-- order by AGENCY.AGENCY_DISPLAY_NAME    
--     
END    
    
--       
--  go  
--exec Proc_RPTPendingAgencyStatements 1,2008,null,"reg"
----@MONTH = 7, @YEAR = 2008, @AGENCY_NAME = '' ,@COMM_TYPE = 'REG'   
--  
--rollback tran  
GO

