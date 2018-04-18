CREATE TABLE [dbo].[MNT_Motor_Class](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleClassCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleClassDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubClassCode] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_Motor_Class] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


