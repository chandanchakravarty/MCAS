IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateVehicleInformationOfCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateVehicleInformationOfCustomer]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateVehicleInformation
Created by      : Nidhi
Date            : 5/4/2005
Purpose         : To update the record in APP_VEHICLE table
Revison History :
Used In         :    
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateVehicleInformationOfCustomer
(
@CUSTOMER_ID   	int,
@VEHICLE_ID    	smallint,
@INSURED_VEH_NUMBER  int,
@VEHICLE_YEAR int,
@MAKE nvarchar(150),
@MODEL  nvarchar(150),
@VIN  nvarchar(150),
@BODY_TYPE  nvarchar(150),
@GRG_ADD1  nvarchar(150),
@GRG_ADD2  nvarchar(150),
@GRG_CITY  nvarchar(80),
@GRG_COUNTRY      nvarchar(10),
@GRG_STATE  nvarchar(10),
@GRG_ZIP  nvarchar(20),
@REGISTERED_STATE  nvarchar(20),
@TERRITORY  nvarchar(20),
@CLASS  nvarchar(150),
@REGN_PLATE_NUMBER nvarchar(150),
@ST_AMT_TYPE   nvarchar(10),
@AMOUNT real=null,
@SYMBOL int =null,
@VEHICLE_AGE decimal=null,
@IS_ACTIVE		nchar(1),
@MODIFIED_BY    	int,
@LAST_UPDATED_DATETIME  datetime
)
AS
BEGIN
	UPDATE CLT_CUSTOMER_VEHICLES
	SET 		
		INSURED_VEH_NUMBER=@INSURED_VEH_NUMBER,
		VEHICLE_YEAR=@VEHICLE_YEAR,
		MAKE=@MAKE,
		MODEL=@MODEL,
		VIN=@VIN,
		BODY_TYPE=@BODY_TYPE,
		GRG_ADD1=@GRG_ADD1,
		GRG_ADD2=@GRG_ADD2,
		GRG_CITY=@GRG_CITY,
		GRG_COUNTRY=@GRG_COUNTRY,
		GRG_STATE=@GRG_STATE,
		GRG_ZIP=@GRG_ZIP,
		REGISTERED_STATE=@REGISTERED_STATE,
		TERRITORY=@TERRITORY,
		CLASS=@CLASS,
		REGN_PLATE_NUMBER=@REGN_PLATE_NUMBER,
		ST_AMT_TYPE=@ST_AMT_TYPE,
		AMOUNT=@AMOUNT,
		SYMBOL=@SYMBOL,
		VEHICLE_AGE=@VEHICLE_AGE,
		IS_ACTIVE		=@IS_ACTIVE,
		MODIFIED_BY		=@MODIFIED_BY,
		LAST_UPDATED_DATETIME	=@LAST_UPDATED_DATETIME
	WHERE
		CUSTOMER_ID 	=@CUSTOMER_ID AND		
		VEHICLE_ID		=@VEHICLE_ID
END


GO

