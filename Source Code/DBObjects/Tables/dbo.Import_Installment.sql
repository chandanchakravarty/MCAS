IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Import_Installment]') AND type in (N'U'))
DROP TABLE [dbo].[Import_Installment]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Import_Installment](
	[PolicyNo] [nvarchar] (20) NULL,
	[EndorsementNo] [nvarchar] (4) NULL,
	[InstallmentNo] [nvarchar] (2) NULL,
	[BarCodeNumber] [nvarchar] (50) NULL,
	[InvoiceNumber] [nvarchar] (30) NULL,
	[InstallmentIssuanceDate] [nvarchar] (8) NULL,
	[InstallmentDueDate] [nvarchar] (8) NULL,
	[InstallmentAmount] [nvarchar] (15) NULL,
	[InstallmentIssuanceCost] [nvarchar] (13) NULL,
	[InstallmentTax] [nvarchar] (16) NULL,
	[InstallmentInsterest] [nvarchar] (13) NULL,
	[InstallmentStatus] [nvarchar] (4) NULL
) ON [PRIMARY]
GO

