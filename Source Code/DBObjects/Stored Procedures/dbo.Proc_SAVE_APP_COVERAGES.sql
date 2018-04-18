IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_APP_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_APP_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_SAVE_VEHICLE_COVERAGES  
Created by      : Pradeep  
Date            : 5/18/2005  
Purpose     :Inserts a record in APP_COVERAGES  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  CREATE
           PROC Dbo.Proc_SAVE_APP_COVERAGES  
(  
 @CUSTOMER_ID     int,  
 @APP_ID     int,  
 @APP_VERSION_ID     smallint,  
 @VEHICLE_ID smallint,  
 @COVERAGE_ID int,  
 @COVERAGE_CODE_ID int,  
 @LIMIT_1 Decimal(18,2),  
 @LIMIT_2 Decimal(18,2),  
 @LIMIT_1_TYPE NVarChar(5),  
 @LIMIT_2_TYPE NVarChar(5),  
 @DEDUCTIBLE_1 DECIMAL(18,2),  
 @DEDUCTIBLE_2 DECIMAL(18,2),  
 @DEDUCTIBLE_1_TYPE NVarChar(5),  
 @DEDUCTIBLE_2_TYPE NVarChar(5),  
 @WRITTEN_PREMIUM DECIMAL(18,2),  
 @FULL_TERM_PREMIUM DECIMAL(18,2)  
)  
AS  
  
DECLARE @COVERAGE_ID_MAX smallint  
BEGIN  
   
 IF ( @COVERAGE_ID = -1 )  
 BEGIN  
    
  select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 from APP_VEHICLE_COVERAGES  
  where CUSTOMER_ID = @CUSTOMER_ID and   
   APP_ID=@APP_ID and   
   APP_VERSION_ID = @APP_VERSION_ID   
   and VEHICLE_ID = @VEHICLE_ID  
  
  INSERT INTO APP_COVERAGES  
  (  
   CUSTOMER_ID,  
   APP_ID,  
   APP_VERSION_ID,  
   COVERAGE_ID,  
   COVERAGE_CODE_ID,  
   LIMIT_1_TYPE,  
   LIMIT_2_TYPE,  
   DEDUCTIBLE_1_TYPE,  
   DEDUCTIBLE_2_TYPE,  
   LIMIT_1,  
   LIMIT_2,  
   DEDUCTIBLE_1,  
   DEDUCTIBLE_2,   
   WRITTEN_PREMIUM,  
   FULL_TERM_PREMIUM  
  )  
  VALUES  
  (  
   @CUSTOMER_ID,  
   @APP_ID,  
   @APP_VERSION_ID,  
   @COVERAGE_ID_MAX,  
   @COVERAGE_CODE_ID,  
   @LIMIT_1_TYPE,  
   @LIMIT_2_TYPE,  
   @DEDUCTIBLE_1_TYPE,  
   @DEDUCTIBLE_2_TYPE,  
   @LIMIT_1,  
   @LIMIT_2,  
   @DEDUCTIBLE_1,  
   @DEDUCTIBLE_2,   
   @WRITTEN_PREMIUM,  
   @FULL_TERM_PREMIUM  
  )  
 RETURN 1  
  
 END   
   
 --Update  
 UPDATE APP_COVERAGES  
 SET  
  CUSTOMER_ID = @CUSTOMER_ID,  
  APP_ID =  @APP_ID,  
  APP_VERSION_ID = @APP_VERSION_ID,  
  COVERAGE_CODE_ID = @COVERAGE_CODE_ID,  
  LIMIT_1_TYPE = @LIMIT_1_TYPE,  
  LIMIT_2_TYPE = @LIMIT_2_TYPE,  
  LIMIT_1 = @LIMIT_1,  
  LIMIT_2 = @LIMIT_2,  
  DEDUCTIBLE_1_TYPE = @DEDUCTIBLE_1_TYPE,  
  DEDUCTIBLE_2_TYPE = @DEDUCTIBLE_2_TYPE,  
  DEDUCTIBLE_1 = @DEDUCTIBLE_1,  
  DEDUCTIBLE_2 = @DEDUCTIBLE_2,  
  WRITTEN_PREMIUM = @WRITTEN_PREMIUM,   
  FULL_TERM_PREMIUM = @FULL_TERM_PREMIUM  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID AND  
  COVERAGE_ID = @COVERAGE_ID  
    
END  
  
  
  
  
  
  
  
  
  
  
  
  
  
  



GO

