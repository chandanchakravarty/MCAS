IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOperatorDiscounts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOperatorDiscounts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : Proc_GetOperatorDiscounts
Created by      : Sumit Chhabra
Date            : Nov 29,2005
Purpose      	: Fetches operator level discount information 
Revison History :                
Used In  : Wolverine                
------------------------------------------------------------                
Date        Review By          Comments                
------   ------------       -------------------------*/                
CREATE   PROC Dbo.Proc_GetOperatorDiscounts                
(                
 @CUSTOMER_ID      int,                
 @APP_ID   		smallint,                
 @APP_VERSION_ID  int,                
 @DRIVER_ID       smallint             
)                
AS               
BEGIN                

--determine the equipment type corresponding to associated boat                
select EQUIP_TYPE from APP_WATERCRAFT_EQUIP_DETAILLS
where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID
and associated_boat in
(select vehicle_id from APP_WATERCRAFT_DRIVER_DETAILS where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and 
APP_VERSION_ID=@APP_VERSION_ID and driver_id=@DRIVER_ID)

--determine the fuel type corresponding to customer,app,version and driver
select a.fuel_type  from APP_WATERCRAFT_INFO a join APP_WATERCRAFT_DRIVER_DETAILS d
on a.customer_id=d.customer_id and a.app_id=d.app_id and a.app_version_id=d.app_version_id
and a.boat_id=d.vehicle_id
where 
d.customer_id=@CUSTOMER_ID and d.app_id=@APP_ID and d.app_version_id=@APP_VERSION_ID and 
d.driver_id=@DRIVER_ID

--finding if there are multiple boats for the given customer,application and version
select customer_id from app_watercraft_info where 
customer_id=@CUSTOMER_ID and app_id=@APP_ID and app_version_id=@APP_VERSION_ID 

              
END                


GO

