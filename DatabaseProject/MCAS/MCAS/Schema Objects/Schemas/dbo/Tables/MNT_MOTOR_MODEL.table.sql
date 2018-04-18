CREATE TABLE [dbo].[MNT_MOTOR_MODEL](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[ModelCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ModelName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MakeCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MotorBody] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CC] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LC] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubClassCode] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[status] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoOfPassenger] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleClassCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_MOTOR_MODEL] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


