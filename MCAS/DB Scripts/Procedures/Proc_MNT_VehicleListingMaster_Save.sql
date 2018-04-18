
/****** Object:  StoredProcedure [dbo].[Proc_MNT_VehicleListingMaster_Save]    Script Date: 08/21/2014 18:10:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_VehicleListingMaster_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_VehicleListingMaster_Save]
GO


/****** Object:  StoredProcedure [dbo].[Proc_MNT_VehicleListingMaster_Save]    Script Date: 08/21/2014 18:10:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_MNT_VehicleListingMaster_Save]
@p_VehicleRegNo nvarchar(50),
@p_VehicleMakeCode nvarchar(50),
@p_VehicleModelCode nvarchar(50),
@p_VehicleClassCode nvarchar(50),
@p_ModelDescription nvarchar(500),
@p_BusCaptainCode nvarchar(50)  
AS
BEGIN
	Insert into MNT_VehicleListingMaster (VehicleRegNo,VehicleMakeCode,VehicleModelCode,VehicleClassCode,ModelDescription,BusCaptainCode)
	values(@p_VehicleRegNo,@p_VehicleMakeCode,@p_VehicleModelCode,@p_VehicleClassCode,@p_ModelDescription,@p_BusCaptainCode)

END


GO


