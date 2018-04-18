IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveAPP_DWELLING_COVERAGE_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveAPP_DWELLING_COVERAGE_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_UpdateAPP_DWELLING_COVERAGE        
Created by      : Pradeep Iyer        
Date            : 11/17/2005        
Purpose        : Sves record in  APP_DWELLING_COVERAGE    
    from Quick quote        
Revison History :        
Used In         : Wolverine        
      
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
create PROC Dbo.Proc_SaveAPP_DWELLING_COVERAGE_ACORD        
(        
 @CUSTOMER_ID     int,        
 @APP_ID     int,        
 @APP_VERSION_ID     smallint,        
 @DWELLING_ID     smallint,        
 @DWELLING_LIMIT    decimal (18,2),        
 --@DWELLING_REPLACE_COST     nchar(2),        
 @DWELLING_REPLACE_COST     nchar(1),        
 @OTHER_STRU_LIMIT     decimal (18,2),        
 --@OTHER_STRU_DESC     nvarchar(200),
 @OTHER_STRU_DESC     nvarchar(100),                
 @PERSONAL_PROP_LIMIT     decimal (18,2),        
 --@REPLACEMENT_COST_CONTS     nchar(2),
 @REPLACEMENT_COST_CONTS     nchar(1),         
 @LOSS_OF_USE     varchar(20),        
 @PERSONAL_LIAB_LIMIT     decimal (18,2),        
 @MED_PAY_EACH_PERSON     decimal (18,2),        
 @ALL_PERILL_DEDUCTIBLE_AMT     decimal (18,2),        
 @THEFT_DEDUCTIBLE_AMT     decimal (18,2)        
)        
AS        
BEGIN        
       
--  IF EXISTS      
--  (      
--   SELECT * FROM  APP_DWELLING_COVERAGE      
--   WHERE  CUSTOMER_ID=@CUSTOMER_ID   AND       
--    APP_ID=@APP_ID  AND       
--    APP_VERSION_ID= @APP_VERSION_ID       
--    AND DWELLING_ID=@DWELLING_ID        
--         
--  )      
--  BEGIN      
--   UPDATE APP_DWELLING_COVERAGE        
--  SET         
--      
--   DWELLING_LIMIT=@DWELLING_LIMIT,        
--       
--   DWELLING_REPLACE_COST=@DWELLING_REPLACE_COST,        
--   OTHER_STRU_LIMIT=@OTHER_STRU_LIMIT,        
--   OTHER_STRU_DESC=@OTHER_STRU_DESC  ,        
--   PERSONAL_PROP_LIMIT=@PERSONAL_PROP_LIMIT,        
--   REPLACEMENT_COST_CONTS=@REPLACEMENT_COST_CONTS,        
--   LOSS_OF_USE=@LOSS_OF_USE,        
--      
--   PERSONAL_LIAB_LIMIT=@PERSONAL_LIAB_LIMIT,        
--   --    
--   MED_PAY_EACH_PERSON=@MED_PAY_EACH_PERSON,        
--       
--   ALL_PERILL_DEDUCTIBLE_AMT=@ALL_PERILL_DEDUCTIBLE_AMT,        
--       
--   THEFT_DEDUCTIBLE_AMT=@THEFT_DEDUCTIBLE_AMT        
--  WHERE  CUSTOMER_ID=@CUSTOMER_ID   AND       
--   APP_ID=@APP_ID  AND       
--   APP_VERSION_ID= @APP_VERSION_ID       
--   AND DWELLING_ID=@DWELLING_ID        
--  END      
--  ELSE      
--  BEGIN      
--   INSERT INTO APP_DWELLING_COVERAGE        
--   (        
--    CUSTOMER_ID,      
--    APP_ID,      
--    APP_VERSION_ID,      
--    DWELLING_ID,      
--       
--    DWELLING_LIMIT,        
--       
--    DWELLING_REPLACE_COST,OTHER_STRU_LIMIT,OTHER_STRU_DESC,PERSONAL_PROP_LIMIT,REPLACEMENT_COST_CONTS,        
--    LOSS_OF_USE,      
--     
-- PERSONAL_LIAB_LIMIT,      
--     
-- MED_PAY_EACH_PERSON,      
--     
-- ALL_PERILL_DEDUCTIBLE_AMT,      
--     
-- THEFT_DEDUCTIBLE_AMT        
--   )        
--   VALUES        
--   (        
--    @CUSTOMER_ID,      
-- @APP_ID,      
-- @APP_VERSION_ID,      
-- @DWELLING_ID,      
--     
--    @DWELLING_LIMIT,      
--     
-- @DWELLING_REPLACE_COST,      
-- @OTHER_STRU_LIMIT,      
-- @OTHER_STRU_DESC,      
-- @PERSONAL_PROP_LIMIT,        
--    @REPLACEMENT_COST_CONTS,      
-- @LOSS_OF_USE,      
--     
-- @PERSONAL_LIAB_LIMIT,      
--     
--    @MED_PAY_EACH_PERSON,      
--     
-- @ALL_PERILL_DEDUCTIBLE_AMT,      
--     
--    @THEFT_DEDUCTIBLE_AMT        
--   )        
--  END      
       
 --Save relevant coverages in APP_DWELLING_SECTION_COVERAGES***********  
 --Insert Dwelling limit      
 EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD      
    @CUSTOMER_ID, --@CUSTOMER_ID      
    @APP_ID, --@APP_ID      
    @APP_VERSION_ID,--@APP_VERSION_ID      
    @DWELLING_ID, --@DWELLING_ID      
    0,  --@COVERAGE_ID      
    0,  --@COVERAGE_CODE_ID      
    @DWELLING_LIMIT, --@LIMIT_1      
    0,  --@LIMIT_2      
    0,  --@DEDUCTIBLE_1      
    0,  --@DEDUCTIBLE_2      
    'S1',  --@COVERAGE_TYPE      
    'DWELL'  --@COVERAGE_CODE      
    
--Coverage B - Other Structures  (OS)    
EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD      
    @CUSTOMER_ID, --@CUSTOMER_ID      
    @APP_ID, --@APP_ID      
    @APP_VERSION_ID,--@APP_VERSION_ID      
    @DWELLING_ID, --@DWELLING_ID      
    0,  --@COVERAGE_ID      
    0,  --@COVERAGE_CODE_ID      
    @OTHER_STRU_LIMIT, --@LIMIT_1      
    0,  --@LIMIT_2      
    0,  --@DEDUCTIBLE_1      
    0,  --@DEDUCTIBLE_2      
    'S1',  --@COVERAGE_TYPE      
    'OS'  --@COVERAGE_CODE      
    
--Coverage C - Unscheduled Personal Property (EBUSPP)    
EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD      
    @CUSTOMER_ID, --@CUSTOMER_ID      
    @APP_ID, --@APP_ID      
    @APP_VERSION_ID,--@APP_VERSION_ID      
    @DWELLING_ID, --@DWELLING_ID      
    0,  --@COVERAGE_ID      
    0,  --@COVERAGE_CODE_ID      
    @PERSONAL_PROP_LIMIT, --@LIMIT_1      
    0,  --@LIMIT_2      
    0,  --@DEDUCTIBLE_1      
    0,  --@DEDUCTIBLE_2      
    'S1',  --@COVERAGE_TYPE      
    'EBUSPP'  --@COVERAGE_CODE      
    
DECLARE @LOSS Decimal (18,0)    
SET @LOSS = CONVERT(decimal(18,0),@LOSS_OF_USE)    
    
--Coverage D - Loss of Use (LOSUR)    
EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD      
    @CUSTOMER_ID, --@CUSTOMER_ID      
    @APP_ID, --@APP_ID      
    @APP_VERSION_ID,--@APP_VERSION_ID      
    @DWELLING_ID, --@DWELLING_ID      
    0,  --@COVERAGE_ID      
    0,  --@COVERAGE_CODE_ID      
    @LOSS, --@LIMIT_1      
    0,  --@LIMIT_2      
    0,  --@DEDUCTIBLE_1      
    0,  --@DEDUCTIBLE_2      
    'S1',  --@COVERAGE_TYPE      
    'LOSUR'  --@COVERAGE_CODE      
    
--Coverage F - Medical Payments to Others  
EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD      
    @CUSTOMER_ID, --@CUSTOMER_ID      
    @APP_ID, --@APP_ID      
    @APP_VERSION_ID,--@APP_VERSION_ID      
    @DWELLING_ID, --@DWELLING_ID      
    0,  --@COVERAGE_ID      
    0,  --@COVERAGE_CODE_ID      
    0, --@LIMIT_1      
    0,  --@LIMIT_2      
    @MED_PAY_EACH_PERSON,  --@DEDUCTIBLE_1      
    0,  --@DEDUCTIBLE_2      
    'S2',  --@COVERAGE_TYPE      
    'MEDPM'  --@COVERAGE_CODE      
  
--Coverage E - Personal Liability (PL)  
EXEC Proc_SAVE_DWELLING_SECTION_COVERAGES_ACORD      
    @CUSTOMER_ID, --@CUSTOMER_ID      
    @APP_ID, --@APP_ID      
    @APP_VERSION_ID,--@APP_VERSION_ID      
    @DWELLING_ID, --@DWELLING_ID      
    0,  --@COVERAGE_ID      
    0,  --@COVERAGE_CODE_ID      
    0, --@LIMIT_1      
    0,  --@LIMIT_2      
    @PERSONAL_LIAB_LIMIT,  --@DEDUCTIBLE_1      
    0,  --@DEDUCTIBLE_2      
    'S2',  --@COVERAGE_TYPE      
    'PL'  --@COVERAGE_CODE      
  
--***************************  
  
END    
    
  





GO

