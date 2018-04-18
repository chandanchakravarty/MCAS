IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEntity_Open_Items_For_Check]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEntity_Open_Items_For_Check]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_GetCustomer_Open_Items
Created by      : Vijay Joshi
Date            : 1/July/2005
Purpose    	: Retreives the open items of specified customer
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
-----------------------------------------------------------*/
CREATE PROCEDURE Proc_GetEntity_Open_Items_For_Check
(
	@EntityId int,--customer/vendor/tax/agency/claim
	@CD_LINE_ITEM_ID int,
	@PAGE_SIZE int,
	@CURRENT_PAGE_INDEX int,
	@CHECK_TYPE_ID int
)
AS
BEGIN
	Declare @EntityName varchar(50)
	declare @sql varchar(5000)
	--Customer
	--"2474" /*Return Premium Checks*/
	--"9936" :/*Return Suspense Checks*/
	--"9935" /*Return Over Payments/*/--
	if @CHECK_TYPE_ID=  2474 or @CHECK_TYPE_ID=  9936 or @CHECK_TYPE_ID=  9935 
		set @EntityName = 'CUSTOMER'
	--Agency
	--Agency Commission Checks
	if @CHECK_TYPE_ID=  2472
		set @EntityName = 'AGENCY'
	--Vendor
	if @CHECK_TYPE_ID=  9938
		set @EntityName = 'VENDOR'
	--Tax
	if @CHECK_TYPE_ID=  9939
		set @EntityName = 'TAX'
	--Claim DEFFRED TILL DISCUSSION
	--if @CHECK_TYPE_ID=  9937
		--@EntityName = 'ACT_CLAIM_OPEN_ITEMS'

	DECLARE @STARTINDEX INT, @ENDPAGEINDEX INT 

	SET @STARTINDEX =  ((@CURRENT_PAGE_INDEX - 1 ) * @PAGE_SIZE) + 1
	SET @ENDPAGEINDEX = @STARTINDEX + @PAGE_SIZE

	set @sql='SELECT COUNT(1)
	FROM ACT_'+@EntityName+'_OPEN_ITEMS
	WHERE ISNULL(TOTAL_DUE,0) > ISNULL(TOTAL_PAID,0) AND '+@EntityName+'_ID = '+convert(varchar,@EntityId)
	exec(@sql) 
	
	CREATE TABLE #ACT_CHECK_OPEN_ITEMS_TEMP
	(
		[IDENT_COL] 	INT IDENTITY(1,1),
		UPDATED_FROM  	VARCHAR(50),
		SOURCE_ROW_ID 	INT,
		SOURCE_NUM	VARCHAR(50),
		SOURCE_TRAN_DATE VARCHAR(20),
		SOURCE_EFF_DATE	DATETIME, 
		POSTING_DATE	DATETIME,
		TOTAL_DUE	DECIMAL(18,2),
		TOTAL_PAID	DECIMAL(18,2),
		AGENCY_ID	INT,
		CUSTOMER_ID	INT,
		POLICY_ID	INT,
		POLICY_VERSION_ID INT,
		BALANCE 	DECIMAL(18,2),
		RECON_AMOUNT	DECIMAL(18,2),
		IDEN_ROW_NO	INT,
		DISPLAY_UPDATED_FROM	VARCHAR(50),
		GROUP_ID	INT
	)
	
	
	BEGIN
	set @sql='INSERT INTO #ACT_CHECK_OPEN_ITEMS_TEMP
		(
			DISPLAY_UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,
			POSTING_DATE, TOTAL_DUE, TOTAL_PAID,  CUSTOMER_ID,'
	if  @CHECK_TYPE_ID=  2474 or @CHECK_TYPE_ID=  9936 or @CHECK_TYPE_ID=  9935 or @CHECK_TYPE_ID=  2472 
		set @sql =@sql + 'POLICY_ID, POLICY_VERSION_ID,AGENCY_ID,'
	set @sql =@sql + 'BALANCE, RECON_AMOUNT,
			IDEN_ROW_NO, UPDATED_FROM, GROUP_ID
		)
		SELECT 
			CASE UPDATED_FROM WHEN ''J'' THEN ''JOURNAL'' WHEN ''D'' THEN ''DEPOSIT'' ELSE '''' END, SOURCE_ROW_ID, 
			SOURCE_NUM, CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) , SOURCE_EFF_DATE,
			POSTING_DATE, TOTAL_DUE, TOTAL_PAID,  '+@EntityName+'_ID,'
	if  @CHECK_TYPE_ID=  2474 or @CHECK_TYPE_ID=  9936 or @CHECK_TYPE_ID=  9935 or @CHECK_TYPE_ID=  2472
		set @sql =@sql +  'POLICY_ID, POLICY_VERSION_ID,AGENCY_ID,'
	set @sql =@sql + 'ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0) AS BALANCE, RGD.RECON_AMOUNT,
			RGD.IDEN_ROW_NO, UPDATED_FROM, RG.GROUP_ID
		FROM ACT_' +@EntityName+ '_OPEN_ITEMS
		LEFT JOIN ACT_'+@EntityName+'_RECON_GROUP_DETAILS RGD ON ITEM_TYPE = ACT_' +@EntityName+ '_OPEN_ITEMS.UPDATED_FROM AND 
			ITEM_REFERENCE_ID = ACT_' +@EntityName+ '_OPEN_ITEMS.SOURCE_ROW_ID
		LEFT JOIN ACT_RECONCILIATION_GROUPS RG ON RG.GROUP_ID = RGD.GROUP_ID
		WHERE ISNULL(TOTAL_DUE,0) <> ISNULL(TOTAL_PAID,0) AND 
			ACT_' +@EntityName+ '_OPEN_ITEMS.'+@EntityName+'_ID = '+convert(varchar,@EntityId)+' AND
			(
			(
			ACT_' +@EntityName+ '_OPEN_ITEMS.SOURCE_ROW_ID = '+convert(varchar,@CD_LINE_ITEM_ID)+' AND
			ACT_' +@EntityName+ '_OPEN_ITEMS.UPDATED_FROM = ''D''
			)
			OR
			ACT_' +@EntityName+ '_OPEN_ITEMS.UPDATED_FROM <> ''D''
			)'
	END

	exec(@sql)

	SELECT * FROM #ACT_CHECK_OPEN_ITEMS_TEMP
	WHERE IDENT_COL >= @STARTINDEX AND
       	IDENT_COL <  @ENDPAGEINDEX	


	DROP TABLE #ACT_CHECK_OPEN_ITEMS_TEMP
END







GO

