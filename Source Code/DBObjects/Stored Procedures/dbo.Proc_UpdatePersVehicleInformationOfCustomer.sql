IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePersVehicleInformationOfCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePersVehicleInformationOfCustomer]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdatePersVehicleInformationOfCustomer
Created by      : Nidhi	
Date            : 05/05/2005
Purpose         : To update the record in CLT_CUSTOMER_VEHICLE table
Revison History :
Used In         :   wolvorine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdatePersVehicleInformationOfCustomer
(
@CUSTOMER_ID   		int,
@VEHICLE_ID    		smallint,
@IS_OWN_LEASE     	nvarchar(10),
@PURCHASE_DATE     	datetime,
@IS_NEW_USED     	nchar(1),
@AMOUNT_COST_NEW     	real,
@MILES_TO_WORK     	nvarchar(5),
@VEHICLE_USE     	nvarchar(5),
@VEH_PERFORMANCE     	nvarchar(5),
@MULTI_CAR     		nvarchar(5),
@ANNUAL_MILEAGE     	real,
@PASSIVE_SEAT_BELT     	nvarchar(5),
@AIR_BAG		nvarchar(5),
@ANTI_LOCK_BRAKES	nvarchar(5),
@P_SURCHARGES		real,
@IS_ACTIVE		nchar(1),
@MODIFIED_BY    	int,
@LAST_UPDATED_DATETIME  datetime
)
AS
BEGIN
	UPDATE clt_customer_vehicles
	SET 		
		IS_OWN_LEASE		=@IS_OWN_LEASE,
		PURCHASE_DATE		=@PURCHASE_DATE,
		IS_NEW_USED		=@IS_NEW_USED,
		AMOUNT_COST_NEW		=@AMOUNT_COST_NEW,
		MILES_TO_WORK		=@MILES_TO_WORK,
		VEHICLE_USE		=@VEHICLE_USE,
		VEH_PERFORMANCE		=@VEH_PERFORMANCE,
		MULTI_CAR		=@MULTI_CAR,
		ANNUAL_MILEAGE		=@ANNUAL_MILEAGE,
		PASSIVE_SEAT_BELT	=@PASSIVE_SEAT_BELT,
		AIR_BAG			=@AIR_BAG,
		ANTI_LOCK_BRAKES	=@ANTI_LOCK_BRAKES,
		P_SURCHARGES		=@P_SURCHARGES,
		IS_ACTIVE		=@IS_ACTIVE,
		MODIFIED_BY		=@MODIFIED_BY,
		LAST_UPDATED_DATETIME	=@LAST_UPDATED_DATETIME
	WHERE
		CUSTOMER_ID 		=@CUSTOMER_ID AND		
		VEHICLE_ID		=@VEHICLE_ID
END


GO

