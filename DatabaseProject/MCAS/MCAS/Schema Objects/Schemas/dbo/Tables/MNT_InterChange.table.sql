CREATE TABLE [dbo].[MNT_InterChange](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InterchangeName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address1] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address2] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address3] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PostalCode] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EffectiveFrom] [datetime] NOT NULL,
	[EffectiveTo] [datetime] NULL,
	[Remarks] [nvarchar](800) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_InterChange] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


