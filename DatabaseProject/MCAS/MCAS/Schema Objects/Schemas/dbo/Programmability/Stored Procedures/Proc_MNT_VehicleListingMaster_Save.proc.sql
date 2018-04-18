CREATE PROCEDURE [dbo].[Proc_MNT_VehicleListingMaster_Save]
	@p_VehicleRegNo [nvarchar](50),
	@p_VehicleMakeCode [nvarchar](50),
	@p_VehicleModelCode [nvarchar](50),
	@p_VehicleClassCode [nvarchar](50),
	@p_ModelDescription [nvarchar](500),
	@p_BusCaptainCode [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
	Insert into MNT_VehicleListingMaster (VehicleRegNo,VehicleMakeCode,VehicleModelCode,VehicleClassCode,ModelDescription,BusCaptainCode)
	values(@p_VehicleRegNo,@p_VehicleMakeCode,@p_VehicleModelCode,@p_VehicleClassCode,@p_ModelDescription,@p_BusCaptainCode)

END


