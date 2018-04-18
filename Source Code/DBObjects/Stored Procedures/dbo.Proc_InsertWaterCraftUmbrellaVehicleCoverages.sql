IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertWaterCraftUmbrellaVehicleCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertWaterCraftUmbrellaVehicleCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertWaterCraftVehicleCoverages  
Created by      : Gaurav  
Date            : 6/14/2005  
Purpose       :Evaluation  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_InsertWaterCraftUmbrellaVehicleCoverages  
(  
@CUSTOMER_ID     int,  
@APP_ID     int,  
@APP_VERSION_ID     smallint,  
@COVERAGE_ID     int output,  
@BOAT_ID smallint,  
@COVERAGE_CODE     nvarchar(50),  
@LIMIT     decimal(9),  
@DEDUCTIBLE     decimal(9),  
  
@WRITTEN_PREMIUM     decimal(9),  
@FULL_TERM_PREMIUM     decimal(9),  
@IS_ACTIVE  nchar(1),  
@CREATED_BY int,  
@CREATED_DATETIME DateTime  
)  
AS  
BEGIN  

/*
select  @COVERAGE_ID=isnull(Max(COVERAGE_ID),0)+1 from APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
where CUSTOMER_ID = @CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID = @APP_VERSION_ID and BOAT_ID = @BOAT_ID  
INSERT INTO APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
(  
CUSTOMER_ID,  
APP_ID,  
APP_VERSION_ID,  
COVERAGE_ID,  
BOAT_ID,  
COVERAGE_CODE,  
LIMIT,  
DEDUCTIBLE,  
WRITTEN_PREMIUM,  
FULL_TERM_PREMIUM,  
IS_ACTIVE ,  
CREATED_BY ,  
CREATED_DATETIME  
)  
VALUES  
(  
@CUSTOMER_ID,  
@APP_ID,  
@APP_VERSION_ID,  
@COVERAGE_ID,  
@BOAT_ID,  
@COVERAGE_CODE,  
  
@LIMIT,  
  
@DEDUCTIBLE,  
  
@WRITTEN_PREMIUM,  
@FULL_TERM_PREMIUM,  
@IS_ACTIVE ,  
@CREATED_BY ,  
@CREATED_DATETIME   
) 
*/
return 1 
END  
  
  

  
  
  
  
  
  
  



GO

