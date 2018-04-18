CREATE TABLE [dbo].[ExcelFilesLog](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleRegNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleMakeCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleModelCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleClassCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModelDescription] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BusCaptainCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorColumn] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileCreateDate] [datetime] NULL,
 CONSTRAINT [PK_ExcelFilesLog] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK__ExcelFile__FileI__0F0E1094] FOREIGN KEY([FileId])
REFERENCES [dbo].[MNT_VehicleListingMaster] ([VehicleMasterId])
)


