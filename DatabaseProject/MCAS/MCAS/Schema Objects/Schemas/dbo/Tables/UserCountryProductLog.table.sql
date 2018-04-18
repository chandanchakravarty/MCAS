CREATE TABLE [dbo].[UserCountryProductLog](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PreviousCountry] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CurrentCountry] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[CountryCode] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TranStatus] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


