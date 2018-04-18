CREATE TYPE [dbo].[VehicleMasterType] AS TABLE(
	[Id] [int] NULL,
	[VehicleRegNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleClassCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleMakeCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VehicleModelCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Type] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Aircon] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Axle] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


