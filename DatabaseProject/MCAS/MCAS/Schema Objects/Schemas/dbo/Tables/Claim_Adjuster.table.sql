CREATE TABLE [dbo].[Claim_Adjuster](
	[AdjusterRefNo] [int] IDENTITY(1,1) NOT NULL,
	[AdjusterCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimantRefNo] [nvarchar](21) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdjusterType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdjusterSource] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SurveyLocation] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PIC] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdjusterNo] [tinyint] NULL CONSTRAINT [DF_Claim_Adjuster_AdjusterNo]  DEFAULT ((1)),
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Claim_Adjuster_CreatedDate]  DEFAULT (getdate())
)


