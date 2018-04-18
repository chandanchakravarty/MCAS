/****** Object:  Table [dbo].[MNT_VehicleListingMaster]    Script Date: 06/10/2014 14:26:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_VehicleListingMaster](
	[VehicleMasterId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleRegNo] [nvarchar](50) NULL,
	[VehicleMakeCode] [nvarchar](50) NULL,
	[VehicleModelCode] [nvarchar](50) NULL,
	[VehicleClassCode] [nvarchar](50) NULL,
	[ModelDescription] [nvarchar](500) NULL,
	[BusCaptainCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_MNT_VehicleListingMaster] PRIMARY KEY CLUSTERED 
(
	[VehicleMasterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


