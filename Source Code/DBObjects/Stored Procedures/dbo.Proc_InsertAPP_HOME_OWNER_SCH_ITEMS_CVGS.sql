IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_HOME_OWNER_SCH_ITEMS_CVGS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_HOME_OWNER_SCH_ITEMS_CVGS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.APP_HOME_OWNER_SCH_ITEMS_CVGS    
Created by      : Vijay Joshi    
Date            : 5/18/2005    
Purpose     :Insert values in APP_HOME_OWNER_SCH_ITEMS_CVGS Table    
Revison History :    
Used In  : Wolverine    
Modify By :Vijay    
Modify On : 1 June,2005    
Purpose  : Change datatype of category from nchar to int    
    
    
Modify By :Gaurav    
Modify On : 7 Oct,2005    
Purpose  : Mearge Insert and Update within one sp    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_InsertAPP_HOME_OWNER_SCH_ITEMS_CVGS    
(    
 @CUSTOMER_ID    int,    
 @APP_ID      int,    
 @APP_VERSION_ID smallint,    
 @ITEM_ID      smallint OUTPUT,    
 @CATEGORY      int,    
 @DETAILED_DESC  nvarchar(510),    
 @SN_DETAILS     nvarchar(50),    
 @AMOUNT_OF_INSURANCE     decimal(9),    
 @PREMIUM      decimal(9),    
 @RATE      decimal(9),    
 @APPRAISAL      nchar(2),    
 @APPRAISAL_DESC   varchar(50),    
 @PURCHASE_APPRAISAL_DATE     datetime,    
 @BREAKAGE_COVERAGE      nchar(2),    
 @BREAKAGE_DESC          varchar(50),    
 @CREATED_BY      int,    
 @CREATED_DATETIME      datetime,    
 @DEDUCTIBLE decimal(18,2)   
)    
AS    
    
-- New     
    
DECLARE @ITEM_ID_MAX smallint    
BEGIN    
     
 IF ( @ITEM_ID = -1 )    
 BEGIN    
      
  SELECT @ITEM_ID_MAX = IsNull(Max(ITEM_ID),0) + 1 FROM APP_HOME_OWNER_SCH_ITEMS_CVGS    
  where CUSTOMER_ID = @CUSTOMER_ID and     
   APP_ID=@APP_ID and     
   APP_VERSION_ID = @APP_VERSION_ID     
       
IF NOT EXISTS    
  (    
   SELECT * FROM APP_HOME_OWNER_SCH_ITEMS_CVGS    
   where CUSTOMER_ID = @CUSTOMER_ID and     
    APP_ID=@APP_ID and     
    APP_VERSION_ID = @APP_VERSION_ID  and    
    CATEGORY = @CATEGORY    
  )    
--New    
    
BEGIN    
 /*Generating the Item Id*/    
 --SELECT @ITEM_ID = IsNull(Max(ITEM_ID),0) + 1 FROM APP_HOME_OWNER_SCH_ITEMS_CVGS    
 INSERT INTO APP_HOME_OWNER_SCH_ITEMS_CVGS    
 (    
  CUSTOMER_ID, APP_ID, APP_VERSION_ID, ITEM_ID, CATEGORY,    
  DETAILED_DESC, SN_DETAILS, AMOUNT_OF_INSURANCE, PREMIUM,    
  RATE, APPRAISAL, PURCHASE_APPRAISAL_DATE, BREAKAGE_COVERAGE,APPRAISAL_DESC,BREAKAGE_DESC,

  IS_ACTIVE, CREATED_BY, CREATED_DATETIME,DEDUCTIBLE    
 )    
 VALUES    
 (    
  @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @ITEM_ID_MAX, @CATEGORY,    
  @DETAILED_DESC, @SN_DETAILS, @AMOUNT_OF_INSURANCE, @PREMIUM,    
  @RATE, @APPRAISAL, @PURCHASE_APPRAISAL_DATE, @BREAKAGE_COVERAGE,@APPRAISAL_DESC,
@BREAKAGE_DESC,
  'Y', @CREATED_BY, @CREATED_DATETIME,@DEDUCTIBLE    
 )    
--New    
RETURN 1    
END    
--New    
END     
     
--New    
 --Update    
 UPDATE APP_HOME_OWNER_SCH_ITEMS_CVGS    
 SET    
  CUSTOMER_ID = @CUSTOMER_ID,    
  APP_ID =  @APP_ID,    
  APP_VERSION_ID = @APP_VERSION_ID,    
  ITEM_ID = @ITEM_ID,    
  CATEGORY = @CATEGORY,    
  DETAILED_DESC = @DETAILED_DESC,    
  SN_DETAILS = @SN_DETAILS,    
  AMOUNT_OF_INSURANCE = @AMOUNT_OF_INSURANCE,    
  PREMIUM = @PREMIUM,    
  RATE = @RATE,    
  APPRAISAL = @APPRAISAL,    
    
  PURCHASE_APPRAISAL_DATE = @PURCHASE_APPRAISAL_DATE,     
  BREAKAGE_COVERAGE = @BREAKAGE_COVERAGE,    
APPRAISAL_DESC=@APPRAISAL_DESC,    
BREAKAGE_DESC=@BREAKAGE_DESC,    
--IS_ACTIVE=@IS_ACTIVE,    
MODIFIED_BY=@CREATED_BY,    
LAST_UPDATED_DATETIME=@CREATED_DATETIME,    
DEDUCTIBLE=@DEDUCTIBLE    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND    
  APP_VERSION_ID = @APP_VERSION_ID AND    
  ITEM_ID = @ITEM_ID    
      
END    
--New    
    
    
  



GO

