IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateWaterCraftUmbrellaCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateWaterCraftUmbrellaCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateWaterCraftUmbrellaCoverages  
Created by      : Gaurav  
Date            : 6/14/2005  
Purpose     : TO UPDATE RECORDS IN   
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROC Dbo.Proc_UpdateWaterCraftUmbrellaCoverages  
(  
@CUSTOMER_ID     int,  
@APP_ID     int,  
@APP_VERSION_ID     smallint,  
@COVERAGE_ID     int ,  
@BOAT_ID smallint,  
@COVERAGE_CODE     nvarchar(50),  
@LIMIT     decimal(9),  
@DEDUCTIBLE     decimal(9),  
  
@WRITTEN_PREMIUM     decimal(9),  
@FULL_TERM_PREMIUM     decimal(9),  
@IS_ACTIVE  nchar(1),  
@MODIFIED_BY  int,  
@LAST_UPDATED_DATETIME DateTime  
)  
AS  
BEGIN  
/*
Update APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
set   
CUSTOMER_ID=@CUSTOMER_ID ,  
APP_ID=@APP_ID ,  
APP_VERSION_ID=@APP_VERSION_ID ,  
BOAT_ID=@BOAT_ID ,  
COVERAGE_CODE=@COVERAGE_CODE ,  
LIMIT=@LIMIT ,  
DEDUCTIBLE=@DEDUCTIBLE ,  
WRITTEN_PREMIUM=@WRITTEN_PREMIUM ,  
FULL_TERM_PREMIUM=@FULL_TERM_PREMIUM ,  
IS_ACTIVE =@IS_ACTIVE,  
MODIFIED_BY=@MODIFIED_BY,  
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME  
 where COVERAGE_ID=@COVERAGE_ID and CUSTOMER_ID = @CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID = @APP_VERSION_ID and BOAT_ID = @BOAT_ID  
*/
return 1
end  
  
  
  
  
  
--sp_helptext Proc_UpdateVehicleCoverages  
  
  
  
  
  
  
  



GO

