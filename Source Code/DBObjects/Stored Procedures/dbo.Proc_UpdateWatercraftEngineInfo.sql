IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateWatercraftEngineInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateWatercraftEngineInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Object:  Stored Procedure dbo.Proc_UpdateWatercraftEngineInfo    Script Date: 5/24/2006 10:44:30 AM ******/
--// DO
/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateWatercraftEngineInfo
Created by      : Nidhi
Date            : 5/18/2005
Purpose    	  :Update Engine Info
Revison History :
Used In 	      : Wolverine

Modified By	: Anurag Verma
Modified On	: Oct 11,2005
Purpose		: Removing Fuel_type,limit_desired,deductible,premium,current_value and adding insuring_value in query 


------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_UpdateWatercraftEngineInfo
CREATE     PROC dbo.Proc_UpdateWatercraftEngineInfo
(
@CUSTOMER_ID     int,
@APP_ID     int,
@APP_VERSION_ID     smallint,
@ENGINE_ID     smallint,
@ENGINE_NO     nvarchar(20),
@YEAR     int,
@MAKE     nvarchar(75),
@MODEL     nvarchar(75),
@SERIAL_NO     nvarchar(75),
@HORSEPOWER     nvarchar(5),
@INSURING_VALUE     decimal(9,2)=null,
@ASSOCIATED_BOAT     smallint,
@FUEL_TYPE int,
@OTHER     nvarchar(100),
@MODIFIED_BY int,
@LAST_UPDATED_DATETIME datetime

)
AS
BEGIN
Declare @Count int
	Set @Count= (SELECT count(ENGINE_NO) FROM APP_WATERCRAFT_ENGINE_INFO
	 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and  ENGINE_ID<>@ENGINE_ID and ENGINE_NO=@ENGINE_NO)


if (@Count=0)
	 BEGIN
	update  APP_WATERCRAFT_ENGINE_INFO
	set 
	ENGINE_NO=@ENGINE_NO,
	YEAR=@YEAR,
	MAKE=@MAKE,
	MODEL=@MODEL,
	SERIAL_NO=@SERIAL_NO,
	HORSEPOWER=@HORSEPOWER,
	INSURING_VALUE=@INSURING_VALUE,
	ASSOCIATED_BOAT=@ASSOCIATED_BOAT,
	FUEL_TYPE=@FUEL_TYPE,
	OTHER=@OTHER,
	MODIFIED_BY=@MODIFIED_BY,
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
	where 
	CUSTOMER_ID=@CUSTOMER_ID AND
	APP_ID=@APP_ID AND
	APP_VERSION_ID =@APP_VERSION_ID AND
	ENGINE_ID =@ENGINE_ID

END
else

begin
	select @ENGINE_ID =0  	
	return -1
end
end








GO

