CREATE TABLE [dbo].[MNT_Motor_Body](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[BodyCode] [nvarchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BodyDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BodyValue] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubClassCode] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[status] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_Motor_Body] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


