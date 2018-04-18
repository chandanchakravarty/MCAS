﻿CREATE TABLE [dbo].[STG_MNTVehicleListingMaster](
	[VehicleMasterId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleRegNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleMakeCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleModelCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleClassCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModelDescription] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BusCaptainCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UploadFileRefNo] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Processed] [bit] NULL,
 CONSTRAINT [PK_STG_MNTVehicleListingMaster] PRIMARY KEY CLUSTERED 
(
	[VehicleMasterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


