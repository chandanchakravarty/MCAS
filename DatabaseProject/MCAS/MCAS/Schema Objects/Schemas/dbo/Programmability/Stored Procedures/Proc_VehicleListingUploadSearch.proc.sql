CREATE PROCEDURE [dbo].[Proc_VehicleListingUploadSearch]  
 @BusNo [nvarchar](100) = null,  
 @ChassisNo [nvarchar](50) = null,  
 @Class [nvarchar](15) = null,  
 @Make [nvarchar](50) = null ,
 @Model [nvarchar](50)= null ,
 @Type  [nvarchar](50)= null 
WITH EXECUTE AS CALLER  
AS  
SET FMTONLY OFF;                
BEGIN         
        
Select x.VehicleMasterId,x.VehicleRegNo,y.VehicleClassDesc,make.MakeName as VehicleMakeCode,
		model.ModelName as VehicleModelCode,x.Type,x.Aircon,x.Axle,x.ModelDescription
from  MNT_VehicleListingMaster x 
left join MNT_Motor_Class y on x.VehicleClassCode=y.VehicleClassCode 
left join MNT_MOTOR_MODEL model on x.VehicleModelCode = model.ModelCode 
left join MNT_Motor_Make make on x.VehicleMakeCode = make.MakeCode  
where 
       
       (@BusNo IS NULL OR (ISNULL(x.VehicleRegNo,'')  like '%'+@BusNo+'%'))        
        and((@ChassisNo IS NULL OR @ChassisNo='') OR (ISNULL(x.ModelDescription,'') like '%'+@ChassisNo+'%'))        
        and((@Class IS NULL OR @Class='') OR (ISNULL(y.VehicleClassDesc,'')  like '%'+@Class+'%'))        
        and((@Make IS NULL OR @Make='') OR (ISNULL(make.MakeName,'')=@Make))  
        and((@Model IS NULL OR @Model='') OR (ISNULL(model.ModelName,'')=@Model))   
        and((@Type IS NULL OR @Type='') OR (ISNULL(x.Type,'')=@Type))   
       
        
END


GO


