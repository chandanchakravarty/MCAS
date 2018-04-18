IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_CHECK_ACT_RECON_GROUP_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_CHECK_ACT_RECON_GROUP_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_DeleteACT_CUSTOMER_RECON_GROUP_DETAILS
Created by      : Vijay
Date            : 07/05/2005
Purpose    	: delete the values in ACT_CUSTOMER_RECON_GROUP_DETAILS
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_Delete_CHECK_ACT_RECON_GROUP_DETAILS
(
	@IDEN_ROW_NO    	int,
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
set  @sql='DELETE FROM ACT_'+@EntityName+'_RECON_GROUP_DETAILS
	WHERE IDEN_ROW_NO = @IDEN_ROW_NO'
END







GO

