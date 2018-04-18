IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteWaterCraftUmbrellaAppCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteWaterCraftUmbrellaAppCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--sp_helptext Proc_DeleteWaterCraftAppCoverages




/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteAppCoverages
Created by      : Gaurav 
Date            : 13th June,2005
Purpose    	  :to delete records from APP_WATERCRAFT_COVERAGE_INFO
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/                                                                                                                                                                                                        
                                                  

CREATE PROCEDURE dbo.Proc_DeleteWaterCraftUmbrellaAppCoverages 
(
@CUSTOMER_ID     int,
@APP_ID     int,
@APP_VERSION_ID     smallint,
@BOAT_ID     smallint,
@COVERAGE_ID int,
@COVERAGE_TYPE VARCHAR(50))
AS
BEGIN
print @COVERAGE_TYPE
IF(@COVERAGE_TYPE='APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO' or @COVERAGE_TYPE='app_umbrella_watercraft_coverage_info')
	begin              
		delete from APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO   where COVERAGE_ID=@COVERAGE_ID and CUSTOMER_ID = @CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID = @APP_VERSION_ID and BOAT_ID = @BOAT_ID
	end

end















GO

