IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_VendorInvoiceDistribution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_VendorInvoiceDistribution]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC [dbo].[rpt_VendorInvoiceDistribution]        
CREATE PROC [dbo].[rpt_VendorInvoiceDistribution]        
@FromDate	DateTime = '01/01/1900',
@ToDate		DateTime = '12/31/3000',
@VendorId	varchar(8000) = Null
as

DECLARE @sql VARCHAR(8000) 

Begin
/* By Rajan for Vendor Invoice Distribution */

IF (isnull(@VendorId, '0') = '0')

	BEGIN
		select @sql = 'SELECT ISNULL(MVL.COMPANY_NAME,'''') + ''-'' + ISNULL(MVL.VENDOR_CODE,'''') + ''-'' + ISNULL(MVL.VENDOR_ACC_NUMBER,'''') AS VENDOR ,AVI.TRANSACTION_DATE, 
		AVI.INVOICE_NUM, AGA.ACC_DESCRIPTION, AGA.ACC_DISP_NUMBER, AD.DISTRIBUTION_AMOUNT, AD.NOTE, AVI.VENDOR_ID
		FROM ACT_VENDOR_INVOICES AVI 
		INNER JOIN ACT_DISTRIBUTION_DETAILS AD ON 
		AVI.INVOICE_ID = AD.GROUP_ID
		AND AD.GROUP_TYPE = ''VEN''			
		INNER JOIN MNT_VENDOR_LIST MVL ON AVI.VENDOR_ID = MVL.VENDOR_ID
		INNER JOIN ACT_GL_ACCOUNTS AGA ON AD.ACCOUNT_ID = AGA.ACCOUNT_ID
		WHERE ISNULL(AVI.IS_COMMITTED, ''N'') = ''Y'''	 
	
		IF NOT @FromDate IS NULL AND NOT @ToDate IS NULL 
		begin
			SELECT @sql = @sql + ' AND AVI.TRANSACTION_DATE >= '''  + CONVERT(VARCHAR, @FromDate,101) + ''' AND AVI.TRANSACTION_DATE <=  '''
			+ Convert(varchar,@ToDate,101) + ''''
		end
		-- Added by Asfa(23-June-2008) - iTrack #4373
		ELSE IF NOT @FromDate IS NULL AND @ToDate IS NULL 
		begin
			SELECT @sql = @sql + ' AND AVI.TRANSACTION_DATE >= '''  + CONVERT(VARCHAR, @FromDate,101) + ''''
		end
		ELSE IF @FromDate IS NULL AND NOT @ToDate IS NULL 
		begin
			SELECT @sql = @sql + ' AND AVI.TRANSACTION_DATE <=  ''' + Convert(varchar,@ToDate,101) + ''''
		end

		SELECT @sql = @sql + ' ORDER BY AVI.TRANSACTION_DATE, MVL.VENDOR_FNAME, MVL.VENDOR_LNAME'
	END

ELSE

	BEGIN
		select @sql = 'SELECT ISNULL(MVL.COMPANY_NAME,'''') + ''-'' + ISNULL(MVL.VENDOR_CODE,'''') + ''-'' + ISNULL(MVL.VENDOR_ACC_NUMBER,'''') AS VENDOR ,AVI.TRANSACTION_DATE, 
		AVI.INVOICE_NUM, AGA.ACC_DESCRIPTION, AGA.ACC_DISP_NUMBER, AD.DISTRIBUTION_AMOUNT, AD.NOTE, AVI.VENDOR_ID
		FROM ACT_VENDOR_INVOICES AVI 
		INNER JOIN ACT_DISTRIBUTION_DETAILS AD ON 
		AVI.INVOICE_ID = AD.GROUP_ID
		AND AD.GROUP_TYPE = ''VEN''
		INNER JOIN MNT_VENDOR_LIST MVL ON AVI.VENDOR_ID = MVL.VENDOR_ID
		INNER JOIN ACT_GL_ACCOUNTS AGA ON AD.ACCOUNT_ID = AGA.ACCOUNT_ID
		WHERE ISNULL(AVI.IS_COMMITTED, ''N'') = ''Y''
		AND AVI.VENDOR_ID IN (' + @VendorId + ')'
	
		IF NOT @FromDate IS NULL AND NOT @ToDate IS NULL 
		begin
			SELECT @sql = @sql + ' AND AVI.TRANSACTION_DATE >= '''  + CONVERT(VARCHAR, @FromDate,101) + ''' AND AVI.TRANSACTION_DATE <=  '''
			+ Convert(varchar,@ToDate,101) + ''''
		end
		-- Added by Asfa(23-June-2008) - iTrack #4373
		ELSE IF NOT @FromDate IS NULL AND @ToDate IS NULL 
		begin
			SELECT @sql = @sql + ' AND AVI.TRANSACTION_DATE >= '''  + CONVERT(VARCHAR, @FromDate,101) + ''''
		end
		ELSE IF @FromDate IS NULL AND NOT @ToDate IS NULL 
		begin
			SELECT @sql = @sql + ' AND AVI.TRANSACTION_DATE <=  ''' + Convert(varchar,@ToDate,101) + ''''
		end
	
		SELECT @sql = @sql + ' ORDER BY AVI.TRANSACTION_DATE, MVL.VENDOR_FNAME, MVL.VENDOR_LNAME'		
	END
/*
SELECT * FROM MNT_VENDOR_LIST
select * from act_vendor_invoices where isnull(is_committed, 'N') ='Y' and invoice_id = 11
SELECT * FROM ACT_DISTRIBUTION_DETAILS WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = 11
exec rpt_VendorInvoiceDistribution  @VendorId= 0
exec rpt_VendorInvoiceDistribution  @VendorId= 11
*/

--rpt_VendorInvoiceDistribution '','','89,45'
--rpt_VendorInvoiceDistribution Null,Null,'89,45'
--rpt_VendorInvoiceDistribution Null,Null,Null
--rpt_VendorInvoiceDistribution '','',Null

PRINT(@sql)         
EXEC(@sql)
End  



GO

