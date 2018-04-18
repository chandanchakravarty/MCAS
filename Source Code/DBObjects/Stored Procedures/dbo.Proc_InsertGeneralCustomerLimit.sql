IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertGeneralCustomerLimit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertGeneralCustomerLimit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertGeneralCustomerLimit  
Created by      :Gaurav  
Date            : 8/18/2005  
Purpose       	:Evaluation  
Revison History :  
Used In        	: Wolverine  
---------------------------------------------------------------  
Modified By   	:  Mohit  
Date            : 09/16/2005  
Purpose         : Commenting droped fields & Adding the added fields in the table.  

Modified By   	:  Vijay Arora
Date            : 09-03-2006
Purpose         : Added the Insured Type and Make Coverage Type, Covarege Form, Free Trade Zone to Int.
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_InsertGeneralCustomerLimit  
(  
@CUSTOMER_ID     int,  
@APP_ID     int,  
@APP_VERSION_ID     smallint,  
@COVERAGE_ID     numeric(9) out,  
@LOCATION_ID     int,  
@CLASS_CODE     numeric(9),  
@BUSINESS_DESCRIPTION     varchar(150),  
@COVERAGE_TYPE    int,  
@COVERAGE_FORM    int,  
@RETRO_DATE     datetime,  
@EXTENDED_REPORTING_DATE     datetime,  
@FREE_TRADE_ZONE     int,  
@PERSONAL_ADVERTISING_INJURY     decimal(18,2),  
@PRODUCTS_COMPLETED_OPERATIONS    decimal(18,2),  
@PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE    decimal(18,2),  
@EMPLOYEE_BENEFITS    decimal(18,2),  
@LIMIT_IDENTIFIER     varchar(10),  
@IS_ACTIVE     nchar(1),  
@CREATED_BY     smallint,  
@CREATED_DATETIME     datetime,  
@EXPOSURE decimal(18,2),  
@ADDITIONAL_EXPOSURE decimal(18,2),  
@RATING_BASE int,  
@RATE numeric,
@INSURED_TYPE int  
--@TERRITORY     smallint,  
--@EACH_OCCURRRENCE     decimal(18,2),  
--@DAMAGE_TO_PREMISES     decimal(18,2),  
--@MEDICAL_EXPENSE     decimal(18,2),  
--@GENERAL_AGGREGATE    decimal(18,2),  
--@FIRE_DAMAGE_LEGAL     decimal(18,2),    
)  
AS  
BEGIN  
  
select @COVERAGE_ID=isnull(Max(@COVERAGE_ID),0)+1 from APP_GENERAL_COVERAGE_LIMIT_INFO  
INSERT INTO APP_GENERAL_COVERAGE_LIMIT_INFO  
(  
CUSTOMER_ID,  
APP_ID,  
APP_VERSION_ID,  
COVERAGE_ID,  
LOCATION_ID,  
CLASS_CODE,  
BUSINESS_DESCRIPTION,  
COVERAGE_TYPE,  
COVERAGE_FORM,  
RETRO_DATE,  
EXTENDED_REPORTING_DATE,  
FREE_TRADE_ZONE,  
PERSONAL_ADVERTISING_INJURY,  
PRODUCTS_COMPLETED_OPERATIONS,  
PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE,  
EMPLOYEE_BENEFITS,  
LIMIT_IDENTIFIER,  
IS_ACTIVE,  
CREATED_BY,  
CREATED_DATETIME,  
EXPOSURE,  
ADDITIONAL_EXPOSURE,  
RATING_BASE,  
RATE,
INSURED_TYPE  
--TERRITORY,  
--EACH_OCCURRRENCE,  
--DAMAGE_TO_PREMISES,  
--MEDICAL_EXPENSE,  
--GENERAL_AGGREGATE,  
--FIRE_DAMAGE_LEGAL,  
)  
VALUES  
(  
@CUSTOMER_ID,  
@APP_ID,  
@APP_VERSION_ID,  
@COVERAGE_ID,  
@LOCATION_ID,  
@CLASS_CODE,  
@BUSINESS_DESCRIPTION,  
@COVERAGE_TYPE,  
@COVERAGE_FORM,  
@RETRO_DATE,  
@EXTENDED_REPORTING_DATE,  
@FREE_TRADE_ZONE,  
@PERSONAL_ADVERTISING_INJURY,  
@PRODUCTS_COMPLETED_OPERATIONS,  
@PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE,  
@EMPLOYEE_BENEFITS,  
@LIMIT_IDENTIFIER,  
@IS_ACTIVE,  
@CREATED_BY,  
@CREATED_DATETIME,  
@EXPOSURE,  
@ADDITIONAL_EXPOSURE,  
@RATING_BASE,  
@RATE,
@INSURED_TYPE  
--@TERRITORY,  
--@EACH_OCCURRRENCE,  
--@DAMAGE_TO_PREMISES,  
--@MEDICAL_EXPENSE,  
--@GENERAL_AGGREGATE,  
--@FIRE_DAMAGE_LEGAL,  
)  
END



GO

