IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionCodesXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionCodesXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTransactionCodesXML
Created by      : Ajit Singh Chahal
Date            : 6/7/2005
Purpose    	: To get record for xml.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetTransactionCodesXML
@TRAN_ID int
as
begin
select
 t1.CATEGOTY_CODE, --when T' then 'Accounting' else 'Non-Accounting' end as CATEGOTY_CODE,
 t1.TRAN_TYPE/*
when 'pre' then 'Premium Transactions' 
when 'fee' then 'Fees Transactions' 
when 'rec' then 'Reciepts' 
when 'pay' then 'Payments' 
when 'dis' then 'Discounts' 
when 'pnc' then 'Premium Notice Codes' 
when 'pas' then 'Past Due Codes' 
when 'pri' then 'Print Codes' 
when 'can' then 'Cancellation Codes' 
end
as TRAN_TYPE*/
,
t1.TRAN_CODE,t1.DISPLAY_DESCRIPTION,t1.PRINT_DESCRIPTION,t1.DEF_AMT_CALC_TYPE,t1.DEF_AMT,t1.AGENCY_COMM_APPLIES,t1.GL_INCOME_ACC,t1.IS_DEF_NEGATIVE,t1.TRAN_ID,t1.IS_ACTIVE
from ACT_TRANSACTION_CODES t1
where TRAN_ID  = @TRAN_ID
end

select ACCOUNT_ID,convert(varchar,ACCOUNT_ID)+': '+ACC_DESCRIPTION as ACC_DESCRIPTION from ACT_GL_ACCOUNTS where ACC_TYPE_ID=4



GO

