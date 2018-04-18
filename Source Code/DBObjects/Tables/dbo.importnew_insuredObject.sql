IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[importnew_insuredObject]') AND type in (N'U'))
DROP TABLE [dbo].[importnew_insuredObject]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[importnew_insuredObject](
	[PolicyNo] [nvarchar] (255) NULL,
	[EndorsementNo] [nvarchar] (255) NULL,
	[Item] [nvarchar] (255) NULL,
	[InsuredEffectiveDate] [nvarchar] (255) NULL,
	[InsuredExpireDate] [nvarchar] (255) NULL,
	[LocationAddress] [nvarchar] (255) NULL,
	[LocationNumber] [nvarchar] (255) NULL,
	[LocationComplement] [nvarchar] (255) NULL,
	[LocationDistrict] [nvarchar] (255) NULL,
	[LocationCity] [nvarchar] (255) NULL,
	[LocationState] [nvarchar] (255) NULL,
	[LocationPostalCode] [nvarchar] (255) NULL,
	[LocationCountry] [nvarchar] (255) NULL,
	[LocationContructionType] [nvarchar] (255) NULL,
	[LocationActivity] [nvarchar] (255) NULL,
	[LocationOccupiedAs] [nvarchar] (255) NULL,
	[VehicleBrand] [nvarchar] (255) NULL,
	[VehicleModel] [nvarchar] (255) NULL,
	[VehicleYear] [nvarchar] (255) NULL,
	[VehiclePlate] [nvarchar] (255) NULL,
	[VehicleId] [nvarchar] (255) NULL,
	[VehiclePassengerCapacity] [nvarchar] (255) NULL,
	[VehicleCategory] [nvarchar] (255) NULL,
	[FIPE Code] [nvarchar] (255) NULL,
	[Insured Type] [nvarchar] (255) NULL,
	[InsuredName] [nvarchar] (255) NULL,
	[InsuredId] [nvarchar] (255) NULL,
	[TransportationConveyancetype] [nvarchar] (255) NULL,
	[TransportationDepartingDate] [nvarchar] (255) NULL,
	[TransportationOriginCountry] [nvarchar] (255) NULL,
	[TransportationOriginState] [nvarchar] (255) NULL,
	[TransportationOriginCity] [nvarchar] (255) NULL,
	[TransportationDestinationCountry] [nvarchar] (255) NULL,
	[TransportationDestinationState] [nvarchar] (255) NULL,
	[TransportationDestinationCity] [nvarchar] (255) NULL,
	[Vessel #] [varchar] (MAX) NULL,
	[Name of Vessel] [varchar] (MAX) NULL,
	[Type of Vessel] [varchar] (MAX) NULL,
	[Manufacture Year] [varchar] (MAX) NULL,
	[CODE] [varchar] (MAX) NULL,
	[POSITION_ID] [varchar] (MAX) NULL,
	[REG_ID_ISSUES] [varchar] (MAX) NULL,
	[REG_ID_ORG] [varchar] (MAX) NULL,
	[REG_IDEN] [varchar] (MAX) NULL,
	[Insured Effective Date] [varchar] (MAX) NULL,
	[Insured Expire Date] [varchar] (MAX) NULL,
	[Number of Passengers] [varchar] (MAX) NULL,
	[TransportationInsuredObject] [varchar] (MAX) NULL,
	[VehicleChassis] [varchar] (MAX) NULL
) ON [PRIMARY]
GO

