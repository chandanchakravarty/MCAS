IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS    
Created by      : Vijay      
Date            : 23 June,2005        
Purpose         : Selects records from act_current_deposits_line_items and of specified deposit number    
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------   */    
--  Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS 927,18,1,'CUST'    
-- drop proc dbo.Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS    
CREATE PROCEDURE dbo.Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS     
(    
 @DEPOSIT_ID int,    
 @PAGE_SIZE int,    
 @CURRENT_PAGE_INDEX int,    
 @DEPOSIT_TYPE nvarchar(5) = null      
    
)    
    
As    
BEGIN    
DECLARE @STARTINDEX int     
DECLARE @ENDPAGEINDEX int     
DECLARE @DISTRIBUTION_STATUS VARCHAR(100)    
    
     
 DECLARE @DEPOSIT_TYPE_CUSTOMER Varchar(5),    
  @DEPOSIT_TYPE_AGENCY Varchar(5),    
  @DEPOSIT_TYPE_CLAIM Varchar(5),    
  @DEPOSIT_TYPE_REINSURANCE Varchar(5),    
  @DEPOSIT_TYPE_MISC Varchar(5)    
    
 SET @DEPOSIT_TYPE_CUSTOMER = 'CUST'    
 SET @DEPOSIT_TYPE_AGENCY = 'AGN'    
 SET @DEPOSIT_TYPE_CLAIM = 'CLAM'    
 SET @DEPOSIT_TYPE_REINSURANCE = 'RINS'    
 SET @DEPOSIT_TYPE_MISC ='MISC'    
    
SET @STARTINDEX =  ((@CURRENT_PAGE_INDEX - 1 ) * @PAGE_SIZE) + 1    
SET @ENDPAGEINDEX = @STARTINDEX + @PAGE_SIZE    
    
SELECT COUNT(1)    
FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS    
WHERE DEPOSIT_ID = @DEPOSIT_ID     
    
    
CREATE TABLE #LINE_ITEM_DETAILS    
(    
 [IDENT_COL] Int Identity(1,1),    
 CD_LINE_ITEM_ID int,     
 DEPOSIT_ID int,     
 LINE_ITEM_INTERNAL_NUMBER int,      
 DIV_ID smallint,     
 DEPT_ID smallint,     
 PC_ID smallint,     
 ACCOUNT_ID int,     
 DEPOSIT_TYPE nvarchar(5),     
 BANK_NAME nvarchar(255),     
 CHECK_NUM nvarchar(25),     
 RECEIPT_NUM nvarchar(25),     
 RECEIPT_AMOUNT decimal(18,2),     
 PAYOR_TYPE nvarchar(5),     
 RECEIPT_FROM_ID int,     
 RECEIPT_FROM_CODE nvarchar(14),     
 RECEIPT_FROM_NAME nvarchar(510),     
 BILL_TYPE char(1),     
 LINE_ITEM_DESCRIPTION nvarchar(510),     
 CUSTOMER_ID int,     
 POLICY_ID smallint,     
 POLICY_VERSION_ID smallint,     
 IN_RECON nchar(2),     
 AVAILABLE_BALANCE decimal(18,2),     
 REF_CUSTOMER_ID int,     
 IS_BNK_RECONCILED nchar(10),      
 IS_ACTIVE nchar(1),     
 CREATED_BY int,     
 CREATED_DATETIME datetime,     
 MODIFIED_BY int,     
 LAST_UPDATED_DATETIME datetime,    
 POLICY_NUMBER nvarchar(150),    
 CLAIM_NUMBER varchar(50),    
 STATUS varchar(500),    
 DISTRIBUTION_STATUS VARCHAR(100),    
 PAGE_ID VARCHAR(100)    
    
)    
    
INSERT INTO #LINE_ITEM_DETAILS    
(    
 CD_LINE_ITEM_ID, DEPOSIT_ID, LINE_ITEM_INTERNAL_NUMBER, DIV_ID, DEPT_ID, PC_ID, ACCOUNT_ID,     
 DEPOSIT_TYPE, BANK_NAME, CHECK_NUM, RECEIPT_NUM, RECEIPT_AMOUNT, PAYOR_TYPE, RECEIPT_FROM_ID,     
 RECEIPT_FROM_CODE, RECEIPT_FROM_NAME, BILL_TYPE, LINE_ITEM_DESCRIPTION, CUSTOMER_ID, POLICY_ID,     
 POLICY_VERSION_ID, IN_RECON, AVAILABLE_BALANCE, REF_CUSTOMER_ID, IS_BNK_RECONCILED, IS_ACTIVE,     
 CREATED_BY, CREATED_DATETIME, MODIFIED_BY, LAST_UPDATED_DATETIME, POLICY_NUMBER, CLAIM_NUMBER, STATUS,    
 DISTRIBUTION_STATUS,PAGE_ID    
)    
SELECT CDLI.CD_LINE_ITEM_ID, CDLI.DEPOSIT_ID, CDLI.LINE_ITEM_INTERNAL_NUMBER, CDLI.DIV_ID, CDLI.DEPT_ID, CDLI.PC_ID, CDLI.ACCOUNT_ID,     
 CDLI.DEPOSIT_TYPE, CDLI.BANK_NAME, CDLI.CHECK_NUM, CDLI.RECEIPT_NUM, CDLI.RECEIPT_AMOUNT, CDLI.PAYOR_TYPE, CDLI.RECEIPT_FROM_ID,     
 CDLI.RECEIPT_FROM_CODE, CDLI.RECEIPT_FROM_NAME, CDLI.BILL_TYPE, CDLI.LINE_ITEM_DESCRIPTION, CDLI.CUSTOMER_ID, CDLI.POLICY_ID,     
 CDLI.POLICY_VERSION_ID, CDLI.IN_RECON, CDLI.AVAILABLE_BALANCE, CDLI.REF_CUSTOMER_ID, CDLI.IS_BNK_RECONCILED, CDLI.IS_ACTIVE,     
 CDLI.CREATED_BY, CDLI.CREATED_DATETIME, CDLI.MODIFIED_BY, CDLI.LAST_UPDATED_DATETIME, CDLI.POLICY_NUMBER, CDLI.CLAIM_NUMBER,     
 CASE IsNull(POLICY_ACCOUNT_STATUS,0) WHEN 0 THEN '' ELSE 'Status of policy is <U>' + LOOKUP.LOOKUP_VALUE_DESC + '</U>. Please make sure whether you want to have it or not.' END STATUS,    
 CASE ISNULL(T1.DISTAMT,-1)     
  WHEN -1.00 THEN -- record is null    
   'Not Distributed'    
  ELSE    
    CASE (ISNULL(T1.DISTAMT,0) - ISNULL(CDLI.RECEIPT_AMOUNT,0))    
    WHEN  0 THEN 'Fully Distributed' ELSE 'Partially Distributed'    
    END     
    END DISTRIBUTION_STATUS,PAGE_ID    
FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI    
LEFT OUTER JOIN     
(SELECT GROUP_ID, SUM(ACD.DISTRIBUTION_AMOUNT) AS DISTAMT FROM ACT_DISTRIBUTION_DETAILS ACD     
WHERE ACD.GROUP_TYPE = 'DEP' GROUP BY GROUP_ID ) T1 ON T1. GROUP_ID = CDLI.CD_LINE_ITEM_ID    
LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.POLICY_ID = CDLI.POLICY_ID AND PCPL.POLICY_VERSION_ID = CDLI.POLICY_VERSION_ID AND PCPL.CUSTOMER_ID = CDLI.CUSTOMER_ID    
LEFT JOIN MNT_LOOKUP_VALUES LOOKUP ON LOOKUP.LOOKUP_UNIQUE_ID = PCPL.POLICY_ACCOUNT_STATUS    
WHERE DEPOSIT_ID = @DEPOSIT_ID     
AND DEPOSIT_TYPE = @DEPOSIT_TYPE    
ORDER BY CDLI.CD_LINE_ITEM_ID    
    
SELECT * FROM #LINE_ITEM_DETAILS    
    
    
-- Commented: Swastika : Implementation has been changed to accomodate changes for multiple pages(iTrack - 2185)    
--  SELECT * FROM #LINE_ITEM_DETAILS    
--   WHERE IDENT_COL >= @STARTINDEX AND    
--   IDENT_COL <  @ENDPAGEINDEX     
    
    
    
    
--- Added by ravindra to fetch the tape total     
    
SELECT CASE @DEPOSIT_TYPE     
 WHEN @DEPOSIT_TYPE_CUSTOMER Then ISNULL(TAPE_TOTAL_CUST,0)    
 WHEN @DEPOSIT_TYPE_CLAIM Then ISNULL(TAPE_TOTAL_CLM,0)    
 WHEN @DEPOSIT_TYPE_REINSURANCE Then ISNULL(TAPE_TOTAL_CLM,0)    
 WHEN @DEPOSIT_TYPE_MISC Then ISNULL(TAPE_TOTAL_MISC,0)    
 END AS TAPE_TOTAL     
FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID     
    
-- SWASTIKA : Implementation has been changed to fetch individual Tape Totals for individual pages(iTrack - 2185)    
SELECT DISTINCT PAGE_ID,SUM(RECEIPT_AMOUNT) AS TAPE_TOTAL FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WHERE  DEPOSIT_ID = @DEPOSIT_ID     
AND DEPOSIT_TYPE = @DEPOSIT_TYPE    
GROUP BY PAGE_ID  
      
DROP TABLE #LINE_ITEM_DETAILS    
    
    
END    
   
  
  




GO

