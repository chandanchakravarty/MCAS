IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Vendor_Open_Items]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Vendor_Open_Items]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : Proc_Get_Vendor_Open_Items
Created by      : Ravindra 
Date            : 12-29-2006
Purpose     	: Retreives the open items of specified vendor
Revison History :  
Used In  	: Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
-----------------------------------------------------------*/  

--drop proc dbo.Proc_Get_Vendor_Open_Items
CREATE PROCEDURE dbo.Proc_Get_Vendor_Open_Items
(  
 @ENTITY_ID int,  
 @RECON_GROUP_ID int
 --,@PAGE_SIZE int = null,  
 --@CURRENT_PAGE_INDEX int= null  
)  
AS  
BEGIN  
-- DECLARE @STARTINDEX int, @ENDPAGEINDEX int   
  
 
 SELECT Count(1)  
 FROM ACT_VENDOR_OPEN_ITEMS  
 WHERE IsNull(TOTAL_DUE,0) > IsNull(TOTAL_PAID,0) AND VENDOR_ID = @ENTITY_ID   
   
 CREATE TABLE #ACT_VENDOR_OPEN_ITEMS_TEMP  
 (  
  [IDENT_COL]  Int Identity(1,1),  
  UPDATED_FROM   varchar(50),  
  ITEM_REF_ID   int,  
  SOURCE_NUM varchar(50),  
  SOURCE_TRAN_DATE varchar(20),  
  SOURCE_EFF_DATE varchar(20),   
  POSTING_DATE varchar(20),  
  TOTAL_DUE decimal(18,2),  
  TOTAL_PAID decimal(18,2),  
  AGENCY_ID int,  
  CUSTOMER_ID int,  
  POLICY_ID int,  
  POLICY_VERSION_ID int,  
  BALANCE  decimal(18,2),  
  RECON_AMOUNT decimal(18,2),  
  IDEN_ROW_NO int,  
  DISPLAY_UPDATED_FROM varchar(50),  
  POLICY_NUMBER VARCHAR(15),  
  TRANSACTION_TYPE VARCHAR(100)   
 )  
	INSERT INTO #ACT_VENDOR_OPEN_ITEMS_TEMP  
	(  
	DISPLAY_UPDATED_FROM, 
	ITEM_REF_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,  
	POSTING_DATE, TOTAL_DUE, TOTAL_PAID, 
	AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,  
	BALANCE, RECON_AMOUNT,  
	IDEN_ROW_NO, UPDATED_FROM, 
	POLICY_NUMBER,  
	TRANSACTION_TYPE  
	)  
	SELECT   
	CASE ACOI.UPDATED_FROM WHEN 'J' THEN 'JOURNAL' 
		WHEN 'I' THEN 'DEPOSIT' 
		WHEN 'C' THEN 'CHECK'
		ELSE '' END, 
	ACOI.IDEN_ROW_ID,ACOI.SOURCE_NUM, CONVERT(VARCHAR,ACOI.SOURCE_TRAN_DATE,101) , CONVERT(VARCHAR(20),ACOI.SOURCE_EFF_DATE, 101),  
	ACOI.POSTING_DATE, ACOI.TOTAL_DUE, ACOI.TOTAL_PAID, 
	null , null , null , null ,
	ISNULL(ACOI.TOTAL_DUE,0) - ISNULL(ACOI.TOTAL_PAID,0) AS BALANCE, RGD.RECON_AMOUNT,  
	RGD.IDEN_ROW_NO, ACOI.UPDATED_FROM, 
	Null,
	CASE ACOI.UPDATED_FROM   
	WHEN 'I' THEN 'Invoice'  
	WHEN 'J' THEN 'Journal Entry'  
	WHEN 'C' THEN 'Check'  
	END  
	FROM ACT_VENDOR_OPEN_ITEMS  ACOI
	LEFT JOIN ACT_VENDOR_RECON_GROUP_DETAILS RGD ON 
	RGD.ITEM_REFERENCE_ID = ACOI.IDEN_ROW_ID  
	AND RGD.GROUP_ID = @RECON_GROUP_ID
	WHERE ISNULL(TOTAL_DUE,0) <> ISNULL(TOTAL_PAID,0) AND   
	ACOI.VENDOR_ID = @ENTITY_ID 
	ORDER BY ISNULL(RGD.IDEN_ROW_NO,2147483647),SOURCE_EFF_DATE
  
  
 SELECT * FROM #ACT_VENDOR_OPEN_ITEMS_TEMP  
  
 DROP TABLE #ACT_VENDOR_OPEN_ITEMS_TEMP  

END  
  










GO

