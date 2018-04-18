IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEditXML_ACT_CURRENT_DEPOSIT_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEditXML_ACT_CURRENT_DEPOSIT_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc
Created by      : Ebix
Date            : 6/24/2005
Purpose    	: To get xml for edit recodes of ACT_CURRENT_DEPOSIT_LINE_ITEMS
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_GetEditXML_ACT_CURRENT_DEPOSIT_LINE_ITEMS
(
@CD_LINE_ITEM_ID int
)
as
begin
-- ************************ Start: Calculating applied status **************************
declare @count int,
	@sum decimal,
	@recAmount decimal,
	@appliedStatus varchar(30),
	@GROUP_TYPE varchar(5)

	select @GROUP_TYPE='DEP'

	select @count=count(*),@sum=sum(DISTRIBUTION_AMOUNT)
	from ACT_DISTRIBUTION_DETAILS
	where GROUP_ID = @CD_LINE_ITEM_ID and GROUP_TYPE = @GROUP_TYPE
	

print @count
if @count=0
	set @appliedStatus = 'No'
else
begin
	select @recAmount=RECEIPT_AMOUNT from ACT_CURRENT_DEPOSIT_LINE_ITEMS where CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID
	if(@sum=@recAmount)
		set @appliedStatus = 'Yes-Fully'
	else if(@sum<@recAmount)
		set @appliedStatus = 'Yes-Partial'
	else if(@sum>@recAmount)
		set @appliedStatus = 'Yes-Discrepancy'
end
-- ************************ End: Calculating applied status
	select CD_LINE_ITEM_ID     ,
	LINE_ITEM_INTERNAL_NUMBER     ,
	convert(varchar,DIV_ID)+'_'+ convert(varchar,DEPT_ID) +'_'+ convert(varchar,PC_ID)  as AccountingEntity,
	ACC_DESCRIPTION as ACCOUNT_ID     ,
	DEPOSIT_TYPE    ,
	BANK_NAME     ,
	CHECK_NUM     ,
	RECEIPT_AMOUNT     ,
	PAYOR_TYPE    ,
	case PAYOR_TYPE 
	when 'AGN' then (select AGENCY_DISPLAY_NAME FROM MNT_AGENCY_LIST where AGENCY_ID = ACT_CURRENT_DEPOSIT_LINE_ITEMS.RECEIPT_FROM_ID) 
	when 'CUS' then (select CUSTOMER_FIRST_NAME+' '+CUSTOMER_LAST_NAME from CLT_CUSTOMER_LIST where CUSTOMER_ID = ACT_CURRENT_DEPOSIT_LINE_ITEMS.RECEIPT_FROM_ID) 
	when 'TAX' then (select TAX_NAME from MNT_TAX_ENTITY_LIST where TAX_ID = ACT_CURRENT_DEPOSIT_LINE_ITEMS.RECEIPT_FROM_ID)
	when 'VEN' then (select VENDOR_FNAME+' '+VENDOR_LNAME from MNT_VENDOR_LIST where VENDOR_ID = ACT_CURRENT_DEPOSIT_LINE_ITEMS.RECEIPT_FROM_ID)
	when 'OTH' then RECEIPT_FROM_NAME
	when 'MOR' then (select HOLDER_NAME from MNT_HOLDER_INTEREST_LIST where HOLDER_ID = ACT_CURRENT_DEPOSIT_LINE_ITEMS.RECEIPT_FROM_ID) end
	 as RECEIPT_FROM_ID     ,
	RECEIPT_FROM_ID as RECEIPT_FROM_ID_HID,
	LINE_ITEM_DESCRIPTION    ,
	POLICY_ID     ,
	(select CUSTOMER_FIRST_NAME+' '+CUSTOMER_LAST_NAME from CLT_CUSTOMER_LIST where CUSTOMER_ID = ACT_CURRENT_DEPOSIT_LINE_ITEMS.REF_CUSTOMER_ID) as REF_CUSTOMER_ID ,
	REF_CUSTOMER_ID as REF_CUSTOMER_ID_HID,
	@appliedStatus as AppliedStatus
	from ACT_CURRENT_DEPOSIT_LINE_ITEMS inner join ACT_GL_ACCOUNTS on ACT_CURRENT_DEPOSIT_LINE_ITEMS.ACCOUNT_ID = ACT_GL_ACCOUNTS.ACCOUNT_ID
	where CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID
end









GO

