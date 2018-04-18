CREATE TABLE [dbo].[MNT_ZoneMaster](
	[ZoneCode] [int] IDENTITY(1,1) NOT NULL,
	[ZoneType] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ZoneDesc] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MstZone] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[isActive] [bit] NULL CONSTRAINT [DF_TM_ZoneMaster_isActive]  DEFAULT ((1)),
	[ZonePer] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[LloydsCountryCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


