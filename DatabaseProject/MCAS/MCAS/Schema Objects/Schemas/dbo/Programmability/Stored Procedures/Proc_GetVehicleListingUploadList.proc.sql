CREATE PROCEDURE [dbo].[Proc_GetVehicleListingUploadList]
	@query [nvarchar](max)
WITH EXECUTE AS CALLER
AS
BEGIN  
  
IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL DROP TABLE #TempTable  
SET FMTONLY OFF;  
create table #TempTable  
(  
VehicleRegNo nvarchar(100),  
VehicleClassCode nvarchar(50),  
VehicleMakeCode nvarchar(50),
VehicleModelCode nvarchar(50),
[Type]  nvarchar(50),
Aircon nvarchar(50),
Axle nvarchar(50)
)  
exec (@query)  
  
SELECT VehicleRegNo, VehicleClassCode, VehicleMakeCode,VehicleModelCode,[Type],Aircon,Axle from #TempTable  
  
END


