IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_QQ_INVOICE_PARTICULAR_MARINE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_QQ_INVOICE_PARTICULAR_MARINE]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------    
--Proc Name          : dbo.[Proc_Get_QQ_INVOICE_PARTICULAR_MARINE]    
--Created by         :     Kuldeep Saxena         
--Date               :  17-Mar-2012           
--------------------------------------------------------    
--Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc dbo.[Proc_Get_QQ_INVOICE_PARTICULAR_MARINE]          
CREATE  PROCEDURE [dbo].[Proc_Get_QQ_INVOICE_PARTICULAR_MARINE]          
 (
 @ID INT
 )         
AS           
BEGIN          
Select 
    ID,
	CUSTOMER_ID,
	QUOTE_ID,
	CUSTOMER_TYPE,
	COMPANY_NAME ,
	BUSINESS_TYPE,
	OPEN_COVER_NO,
	INVOICE_TYPE,
	INVOICE_AMOUNT,
	CURRENCY_TYPE,
	BILLING_CURRENCY,
	MARK_UP_RATE_PERC,
	DATE_OF_QUOTATION  
 FROM QQ_INVOICE_PARTICULAR_MARINE  where ID=@ID
End

