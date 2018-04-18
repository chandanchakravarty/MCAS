IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateGeneralCoverageLimit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateGeneralCoverageLimit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc  
Created by      : Gaurav  
Date            : 8/18/2005  
Purpose       :Evaluation  
Revison History :  
Used In        : Wolverine  
---------------------------------------------------------  
Modified By   : Mohit  
Date              : 09/16/2005  
Purpose        : Adding and removing fields as in database table.  

Modified By   	: Vijay Arora  
Date            : 09-03-2006
Purpose        	: Add the Insured Type and Correct the type of Coverage Type, Coverage Form and Free Trade Zone
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_UpdateGeneralCoverageLimit  
(  
@CUSTOMER_ID     int,  
@APP_ID     int,  
@APP_VERSION_ID     smallint,  
@COVERAGE_ID     numeric(9),  
@LOCATION_ID     int,  
@CLASS_CODE     numeric(9),  
@BUSINESS_DESCRIPTION     varchar(150),  
@COVERAGE_TYPE     int,  
@COVERAGE_FORM     int,  
@RETRO_DATE     datetime,  
@EXTENDED_REPORTING_DATE     datetime,  
@FREE_TRADE_ZONE     int,  
@PERSONAL_ADVERTISING_INJURY    decimal(18,2),  
@PRODUCTS_COMPLETED_OPERATIONS    decimal(18,2),  
@PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE     decimal(18,2),  
@EMPLOYEE_BENEFITS    decimal(18,2),  
@LIMIT_IDENTIFIER     varchar(10),  
@IS_ACTIVE     nchar(1),  
@MODIFIED_BY     smallint,  
@LAST_UPDATED_DATETIME     datetime,  
@EXPOSURE decimal(18,2),  
@ADDITIONAL_EXPOSURE decimal(18,2),  
@RATING_BASE int,  
@RATE numeric,
@INSURED_TYPE int  
--@TERRITORY     smallint,  
--@EACH_OCCURRRENCE    decimal(18,2),  
--@DAMAGE_TO_PREMISES     decimal(18,2),  
--@MEDICAL_EXPENSE     decimal(18,2),  
--@GENERAL_AGGREGATE    decimal(18,2),  
--@FIRE_DAMAGE_LEGAL    decimal(18,2),  
)  
AS  
BEGIN  
Update  APP_GENERAL_COVERAGE_LIMIT_INFO  
set  
CUSTOMER_ID = @CUSTOMER_ID,  
APP_ID  = @APP_ID,  
APP_VERSION_ID  = @APP_VERSION_ID,  
COVERAGE_ID  = @COVERAGE_ID,  
LOCATION_ID  =  @LOCATION_ID,  
CLASS_CODE  =  @CLASS_CODE,  
BUSINESS_DESCRIPTION  =  @BUSINESS_DESCRIPTION,  
COVERAGE_TYPE  =  @COVERAGE_TYPE,  
COVERAGE_FORM  = @COVERAGE_FORM,  
RETRO_DATE  =  @RETRO_DATE,  
EXTENDED_REPORTING_DATE  =  @EXTENDED_REPORTING_DATE,  
FREE_TRADE_ZONE  = @FREE_TRADE_ZONE,  
PERSONAL_ADVERTISING_INJURY  =  @PERSONAL_ADVERTISING_INJURY,  
PRODUCTS_COMPLETED_OPERATIONS  =  @PRODUCTS_COMPLETED_OPERATIONS,  
PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE  =  @PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE,  
EMPLOYEE_BENEFITS  =  @EMPLOYEE_BENEFITS,  
LIMIT_IDENTIFIER  =  @LIMIT_IDENTIFIER,  
IS_ACTIVE     =@IS_ACTIVE,  
MODIFIED_BY  =  @MODIFIED_BY,  
LAST_UPDATED_DATETIME  =  @LAST_UPDATED_DATETIME,  
EXPOSURE=@EXPOSURE,  
ADDITIONAL_EXPOSURE=@ADDITIONAL_EXPOSURE,  
RATING_BASE=@RATING_BASE,  
RATE=@RATE,
INSURED_TYPE = @INSURED_TYPE  
--TERRITORY  =  @TERRITORY,    
--EACH_OCCURRRENCE  = @EACH_OCCURRRENCE,  
--DAMAGE_TO_PREMISES  =  @DAMAGE_TO_PREMISES,  
--MEDICAL_EXPENSE  =  @MEDICAL_EXPENSE,  
--GENERAL_AGGREGATE  = @GENERAL_AGGREGATE,  
--FIRE_DAMAGE_LEGAL  =  @FIRE_DAMAGE_LEGAL,  
where  CUSTOMER_ID = @CUSTOMER_ID           AND  
  
APP_ID = @APP_ID AND  
APP_VERSION_ID = @APP_VERSION_ID    AND  
COVERAGE_ID=@COVERAGE_ID     AND  
LOCATION_ID=@LOCATION_ID      
  
END



GO

