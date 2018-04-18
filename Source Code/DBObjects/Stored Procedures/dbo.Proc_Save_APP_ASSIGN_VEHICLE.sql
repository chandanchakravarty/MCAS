IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_APP_ASSIGN_VEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_APP_ASSIGN_VEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_Save_APP_ASSIGN_VEHICLE
Created by      : Pradeep
Date            : 29/7/2005
Purpose    	:Insert record
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_Save_APP_ASSIGN_VEHICLE
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@DRIVER_ID     smallint,
	@VEHICLE_ID     smallint,
	@INSURED_DRIVER_NO     smallint,
	@PERCENT_DRIVEN     smallint,
	@MODIFIED_BY int,
	@CREATED_BY int  
)
AS
BEGIN

IF EXISTS ( 
	SELECT * FROM APP_ASSIGN_VEHICLE
	WHERE CUSTOMER_ID = 	@CUSTOMER_ID AND
	APP_ID = @APP_ID AND
	APP_VERSION_ID = @APP_VERSION_ID AND
	DRIVER_ID = @DRIVER_ID AND
	VEHICLE_ID = @VEHICLE_ID
)
BEGIN
	 UPDATE APP_ASSIGN_VEHICLE  
 	SET  VEHICLE_ID = @VEHICLE_ID,   
                INSURED_DRIVER_NO = @INSURED_DRIVER_NO,  
                 PERCENT_DRIVEN = @PERCENT_DRIVEN,
		MODIFIED_BY = @MODIFIED_BY,
		LAST_UPDATED_DATETIME= GetDate()  
    
 WHERE  
                CUSTOMER_ID=@Customer_Id and  
                DRIVER_ID=@DRIVER_ID and  
         	APP_ID=@APP_ID AND  
                APP_VERSION_ID=@APP_VERSION_ID  
END
ELSE
BEGIN
	INSERT INTO APP_ASSIGN_VEHICLE
	(
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		DRIVER_ID,
		VEHICLE_ID,
		INSURED_DRIVER_NO,
		PERCENT_DRIVEN,
		CREATED_BY,
		CREATED_DATETIME
	)
	VALUES
	(
		@CUSTOMER_ID,
		@APP_ID,
		@APP_VERSION_ID,
		@DRIVER_ID,
		@VEHICLE_ID,
		@INSURED_DRIVER_NO,
		@PERCENT_DRIVEN,
		@CREATED_BY,
		GetDate()
	)
END
END



GO

