CREATE TABLE [dbo].[MNT_Claim](
	[ClaimCode] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Product_Code] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [pk_TM_Claim] PRIMARY KEY CLUSTERED 
(
	[ClaimCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


