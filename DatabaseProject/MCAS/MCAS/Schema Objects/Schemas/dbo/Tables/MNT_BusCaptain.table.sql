CREATE TABLE [dbo].[MNT_BusCaptain](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[BusCaptainCode] [nvarchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BusCaptainName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NRICPassportNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[Nationality] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateJoined] [datetime] NULL,
	[DateResigned] [datetime] NULL,
	[UploadFileRefNo] [nvarchar](100) NULL,
 CONSTRAINT [PK_MNT_BusCaptain] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


