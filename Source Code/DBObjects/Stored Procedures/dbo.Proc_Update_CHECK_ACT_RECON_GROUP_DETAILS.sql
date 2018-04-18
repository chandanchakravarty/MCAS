IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_CHECK_ACT_RECON_GROUP_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_CHECK_ACT_RECON_GROUP_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_UpdateACT_CUSTOMER_RECON_GROUP_DETAILS
Created by      : Vijay
Date            : 07/05/2005
Purpose    	: Update the values in ACT_CUSTOMER_RECON_GROUP_DETAILS
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_Update_CHECK_ACT_RECON_GROUP_DETAILS
(
	@IDEN_ROW_NO    	int	OUTPUT,
	@GROUP_ID     		int,
	@ITEM_TYPE     		nvarchar(10),
	@ITEM_REFERENCE_ID     	int,
	@SUB_LEDGER_TYPE	nvarchar(10),
	@RECON_AMOUNT  		decimal(9),
	@NOTE     		nvarchar(510),
	@DIV_ID     		smallint,
	@DEPT_ID     		smallint,
	@PC_ID     		smallint,
	@CHECK_TYPE_ID 		int	

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

SET @sql='UPDATE ACT_'+@EntityName+'_RECON_GROUP_DETAILS
	SET
		GROUP_ID = '+convert(varchar,@GROUP_ID)+',
		ITEM_TYPE = '''+@ITEM_TYPE+''',
		ITEM_REFERENCE_ID = '+convert(varchar,@ITEM_REFERENCE_ID)+',
		SUB_LEDGER_TYPE = '''+convert(varchar,@SUB_LEDGER_TYPE)+''',
		RECON_AMOUNT = '+convert(varchar,@RECON_AMOUNT)+',
		NOTE = '''+convert(varchar,@NOTE)+''','
		if(@DIV_ID is null)
			set @sql=@sql+ 'DIV_ID=null,'
		else
			set @sql=@sql+'DIV_ID = '+convert(varchar,@DIV_ID)+','
		if(@DEPT_ID is null)
			set @sql=@sql+'DEPT_ID=null,'
		else
			set @sql=@sql+'DEPT_ID = '+convert(varchar,@DEPT_ID)+','
		if(@PC_ID is null)
			set @sql=@sql+'PC_ID=null '
		else
			set @sql=@sql+'PC_ID = '+convert(varchar,@PC_ID)
set @sql=@sql+	' WHERE IDEN_ROW_NO = '+convert(varchar,@IDEN_ROW_NO)
print @sql
exec(@sql)
END




GO

