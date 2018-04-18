CREATE PROCEDURE [dbo].[Proc_UpdateVehicleListing]
	@Vehicleid [int],
	@BusNo [nvarchar](50),
	@ChasisNo [nvarchar](50),
	@Make [nvarchar](50),
	@Model [nvarchar](50),
	@Type [nvarchar](50),
	@Aircon [nvarchar](50),
	@Axle [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;                  
  BEGIN                  
  declare @match int              
  select @match=count(*) from MNT_VehicleListingMaster where VehicleMasterId=@Vehicleid    
  if(@match>0)    
  BEGIN    
  Update MNT_VehicleListingMaster set VehicleRegNo=@BusNo,VehicleMakeCode=@Make,VehicleModelCode=@Model,VehicleClassCode=@ChasisNo,Type=@Type,Aircon=@Aircon,Axle=@Axle where VehicleMasterId=@Vehicleid 
   
  END    
  if(@match=0)    
  BEGIN    
  insert into MNT_VehicleListingMaster(VehicleRegNo,VehicleMakeCode,VehicleModelCode,VehicleClassCode,Type,Aircon,Axle) values (@BusNo,@Make,@Model,@ChasisNo,@Type,@Aircon,@Axle)    
  END    
  END


