CREATE TABLE [dbo].[CLM_ReportedClaimBackup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[tableName] [varchar](100) NULL,
	[Tabledata] [xml] NULL,
	[CreatedDateTime] Datetime
) ON [PRIMARY]

GO


