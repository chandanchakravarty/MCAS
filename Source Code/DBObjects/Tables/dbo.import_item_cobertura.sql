IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[import_item_cobertura]') AND type in (N'U'))
DROP TABLE [dbo].[import_item_cobertura]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[import_item_cobertura](
	[PolicyNo] [nvarchar] (20) NULL,
	[EndorsementNo] [nvarchar] (4) NULL,
	[Item] [nvarchar] (3) NULL,
	[InsuredObject] [nvarchar] (60) NULL,
	[InsuredEffectiveDate] [nvarchar] (8) NULL,
	[InsuredExpireDate] [nvarchar] (8) NULL,
	[LocationAddress] [nvarchar] (40) NULL,
	[LocationNumber] [nvarchar] (40) NULL,
	[LocationComplement] [nvarchar] (40) NULL,
	[LocationDistrict] [nvarchar] (40) NULL,
	[LocationCity] [nvarchar] (40) NULL,
	[LocationState] [nvarchar] (2) NULL,
	[LocationPostalCode] [nvarchar] (10) NULL,
	[LocationCountry] [nvarchar] (20) NULL,
	[LocationContructionType] [nvarchar] (5) NULL,
	[LocationActivity] [nvarchar] (5) NULL,
	[LocationOccupiedAs] [nvarchar] (5) NULL,
	[duplicatedate1] [nvarchar] (8) NULL,
	[duplicatedate2] [nvarchar] (8) NULL,
	[VehicleBrand] [nvarchar] (50) NULL,
	[VehicleModel] [nvarchar] (50) NULL,
	[VehicleYear] [nvarchar] (5) NULL,
	[VehiclePlate] [nvarchar] (50) NULL,
	[VehicleId] [nvarchar] (50) NULL,
	[VehiclePassengerCapacity] [nvarchar] (50) NULL,
	[VehicleCategory] [nvarchar] (5) NULL,
	[VehiclePlanId] [nvarchar] (5) NULL,
	[InsuredName] [nvarchar] (50) NULL,
	[InsuredId] [nvarchar] (11) NULL,
	[TransportationConveyancetype] [nvarchar] (60) NULL,
	[TransportationDepartingDate] [nvarchar] (8) NULL,
	[TransportationOriginCountry] [nvarchar] (60) NULL,
	[TransportationOriginState] [nvarchar] (50) NULL,
	[TransportationOriginCity] [nvarchar] (50) NULL,
	[TransportationDestinationCountry] [nvarchar] (50) NULL,
	[TransportationDestinationState] [nvarchar] (50) NULL,
	[TransportationDestinationCity] [nvarchar] (50) NULL,
	[Coverage Code] [nvarchar] (5) NULL,
	[Coverage Name] [nvarchar] (60) NULL,
	[Sum Insured] [nvarchar] (15) NULL,
	[Rate] [nvarchar] (15) NULL,
	[Premium] [nvarchar] (15) NULL,
	[DeductibleType] [nvarchar] (4) NULL,
	[DeductibleValue] [nvarchar] (13) NULL,
	[DeductibleMinAmount] [nvarchar] (13) NULL,
	[DeducbtibleDescripition] [nvarchar] (62) NULL
) ON [PRIMARY]
GO

