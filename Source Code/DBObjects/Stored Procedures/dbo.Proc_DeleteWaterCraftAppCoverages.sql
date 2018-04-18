IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteWaterCraftAppCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteWaterCraftAppCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



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

CREATE PROCEDURE dbo.Proc_DeleteWaterCraftAppCoverages 
(
@CUSTOMER_ID     int,
@APP_ID     int,
@APP_VERSION_ID     smallint,
@BOAT_ID     smallint,
@COVERAGE_ID int,
@COVERAGE_TYPE VARCHAR(30))
AS
BEGIN
print @COVERAGE_TYPE
IF(@COVERAGE_TYPE='APP_WATERCRAFT_COVERAGE_INFO' or @COVERAGE_TYPE='app_watercraft_coverage_info')
	begin              
		delete from APP_WATERCRAFT_COVERAGE_INFO   where COVERAGE_ID=@COVERAGE_ID and CUSTOMER_ID = @CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID = @APP_VERSION_ID and BOAT_ID = @BOAT_ID
	end

end








GO

