CREATE PROCEDURE [dbo].[UpdateDestinationTable]
	@VehicleRegNo [nvarchar](50),
	@VehicleMakeCode [nvarchar](50),
	@VehicleModelCode [nvarchar](50),
	@VehicleClassCode [nvarchar](50),
	@ModelDescription [nvarchar](50),
	@BusCaptainCode [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
    SET NOCOUNT ON;
 
    update MNT_VehicleListingMaster
    set VehicleMakeCode=@VehicleMakeCode,
    VehicleModelCode=@VehicleModelCode,
    VehicleClassCode=@VehicleClassCode,
    ModelDescription=@ModelDescription,
    BusCaptainCode=@BusCaptainCode
    where VehicleRegNo=@VehicleRegNo
END


