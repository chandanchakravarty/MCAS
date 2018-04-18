IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[import_installment_paid]') AND type in (N'U'))
DROP TABLE [dbo].[import_installment_paid]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[import_installment_paid](
	[PolicyNumber] [varchar] (20) NULL,
	[EndorsementNumber] [varchar] (4) NULL,
	[InstallmentNumber] [varchar] (2) NULL,
	[BankCode] [varchar] (4) NULL,
	[BankAccountNumber] [varchar] (30) NULL,
	[InvoiceNumber] [varchar] (30) NULL,
	[InstallmentAmount] [varchar] (15) NULL,
	[PenaltyAmount] [varchar] (15) NULL,
	[DiscountAmount] [varchar] (15) NULL,
	[ReceivedDate] [varchar] (8) NULL,
	[ReceivedAmount] [varchar] (17) NULL
) ON [PRIMARY]
GO

