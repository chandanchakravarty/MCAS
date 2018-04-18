IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLAPP_GENERAL_COVERAGE_LIMIT_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLAPP_GENERAL_COVERAGE_LIMIT_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetXMLAPP_GENERAL_COVERAGE_LIMIT_INFO    
Created by      : Gaurav    
Date            : 8/18/2005    
Purpose       :Evaluation    
Revison History :    
Used In        : Wolverine    
-----------------------------------------------------------    
Modified By        :  Mohit    
Date                   : 09/16/2005    
Purpose              : Adding & commenting fields.     

Modified By        :  Vijay Arora
Date                   : 09-03-2006
Purpose              : Added the Insured Type field.
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_GetXMLAPP_GENERAL_COVERAGE_LIMIT_INFO    
(    
@CUSTOMER_ID     int,    
@APP_ID int,    
@APP_VERSION_ID int    
   
)    
AS    
BEGIN    
select CUSTOMER_ID,    
APP_ID,    
APP_VERSION_ID,    
COVERAGE_ID,    
LOCATION_ID,    
CLASS_CODE,    
BUSINESS_DESCRIPTION,    

COVERAGE_TYPE,    
COVERAGE_FORM,    
convert(varchar,RETRO_DATE,101) RETRO_DATE,    
convert(varchar,EXTENDED_REPORTING_DATE,101) EXTENDED_REPORTING_DATE,    
FREE_TRADE_ZONE,    
PERSONAL_ADVERTISING_INJURY,    
PRODUCTS_COMPLETED_OPERATIONS,    
PRODUCTS_COMPLETED_OPERATIONS_AGGREGATE,    
EMPLOYEE_BENEFITS,    
LIMIT_IDENTIFIER,    
MODIFIED_BY,    
LAST_UPDATED_DATETIME,    
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
from  APP_GENERAL_COVERAGE_LIMIT_INFO    
where  CUSTOMER_ID = @CUSTOMER_ID    
AND APP_ID= @APP_ID    
AND APP_VERSION_ID=@APP_VERSION_ID     
    
END  
  



GO

