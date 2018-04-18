IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftEngineInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftEngineInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetWatercraftEngineInfo
Created by           : Nidhi
Date                    : 17/05/2005
Purpose               : To get the vehicle  information  from app_watercraft_info  table
Revison History :
Used In                :   Wolverine

Modified By	: Anurag Verma
Modified On	: Oct 11,2005
Purpose		: Removing Fuel_type,limit_desired,deductible,premium,current_value and adding insuring_value in query 
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- DROP PROC dbo.Proc_GetWatercraftEngineInfo
CREATE  PROC dbo.Proc_GetWatercraftEngineInfo
(
	@CUSTOMERID 	int,
	@APPID		int,
	@APPVERSIONID	int,
	@ENGINE_ID	int
)
AS
BEGIN
	SELECT  
	ENGINE_NO,
	YEAR,
	MAKE,
	MODEL,
	SERIAL_NO,
	HORSEPOWER,
	INSURING_VALUE,
	ASSOCIATED_BOAT,
	FUEL_TYPE,
	OTHER,
	--Added by Shanker - 21 Aug 2006 
	Isnull(IS_ACTIVE,'Y')as IS_ACTIVE
	--End of addition by Shanker - 21 Aug 2006 
	FROM        app_watercraft_engine_info
	WHERE     (CUSTOMER_ID = @CUSTOMERID)   and (APP_ID=@APPID) AND (APP_VERSION_ID=@APPVERSIONID) AND (ENGINE_ID= @ENGINE_ID);
END







GO

