
/****** Object:  StoredProcedure [dbo].[Proc_UpdateVehicleListing]    Script Date: 02/20/2015 18:17:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateVehicleListing]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateVehicleListing]
GO


/****** Object:  StoredProcedure [dbo].[Proc_UpdateVehicleListing]    Script Date: 02/20/2015 18:17:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Proc_UpdateVehicleListing]    
(    
@Vehicleid int,    
@BusNo nvarchar(50),    
@ChasisNo nvarchar(50),    
@Make nvarchar(50),    
@Model nvarchar(50),    
@Type nvarchar(50),    
@Aircon nvarchar(50),    
@Axle nvarchar(50)    
)     
as    
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
GO


