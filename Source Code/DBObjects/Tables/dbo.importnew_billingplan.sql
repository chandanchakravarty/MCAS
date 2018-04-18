IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[importnew_billingplan]') AND type in (N'U'))
DROP TABLE [dbo].[importnew_billingplan]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[importnew_billingplan](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[CoApplicant ID] [nvarchar] (255) NULL,
	[InstallmentNo] [nvarchar] (255) NULL,
	[BarCodeNumber] [nvarchar] (255) NULL,
	[InvoiceNumber] [nvarchar] (255) NULL,
	[InstallmentIssuanceDate] [nvarchar] (255) NULL,
	[InstallmentDueDate] [nvarchar] (255) NULL,
	[InstallmentAmount] [nvarchar] (255) NULL,
	[InstallmentIssuanceCost] [nvarchar] (255) NULL,
	[InstallmentTax] [nvarchar] (255) NULL,
	[InstallmentInsterest] [nvarchar] (255) NULL,
	[InstallmentStatus] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

