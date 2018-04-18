IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[importnew_reinsurance]') AND type in (N'U'))
DROP TABLE [dbo].[importnew_reinsurance]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[importnew_reinsurance](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[Reinsurance Type] [nvarchar] (255) NULL,
	[ReinsuranceContractCode] [nvarchar] (255) NULL,
	[ReinsurerId] [nvarchar] (255) NULL,
	[ReinsuranceShareRate] [nvarchar] (255) NULL,
	[ReinsuranceFee] [nvarchar] (255) NULL
) ON [PRIMARY]
GO

