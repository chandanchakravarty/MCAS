IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_DWELLING_COVERAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_DWELLING_COVERAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertPOL_DWELLING_COVERAGE          
Created by      : SHAFI         
Date                : 16/02/06 
Purpose       :Evaluation          
Revison History :          
Used In        : Wolverine          
          
M
Purpose : Following fields has been removed          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/    
--DROP PROC Proc_InsertPOL_DWELLING_COVERAGE      
CREATE PROC Dbo.Proc_InsertPOL_DWELLING_COVERAGE          
(          
@CUSTOMER_ID     int,          
@POL_ID     int,          
@POL_VERSION_ID     smallint,          
@DWELLING_ID     smallint,          
--@FORM     int,          
--@FORM_OTHER_DESC     nvarchar(200),          
--@COVERAGE     int,          
--@COVERAGE_OTHER_DESC     nvarchar(200),          
@DWELLING_LIMIT    decimal (18,2),          
--@DWELLING_PREMIUM     decimal (18,2),          
@DWELLING_REPLACE_COST     nchar(2),          
@OTHER_STRU_LIMIT     decimal (18,2),          
@OTHER_STRU_DESC     nvarchar(200),          
@PERSONAL_PROP_LIMIT     decimal (18,2),          
@REPLACEMENT_COST_CONTS     nchar(2),          
@LOSS_OF_USE     decimal (18,2),          
--@LOSS_OF_USE_PREMIUM     decimal (18,2),          
@PERSONAL_LIAB_LIMIT     decimal (18,2),          
--@PERSONAL_LIAB_PREMIUM     decimal (18,2),          
@MED_PAY_EACH_PERSON     decimal (18,2),          
--@MED_PAY_EACH_PERSON_PREMIUM     decimal (18,2),          
--@INFLATION_GUARD     varchar(3),          
@ALL_PERILL_DEDUCTIBLE_AMT     decimal (18,2),          
--@WIND_HAIL_DEDUCTIBLE_AMT     decimal (18,2),          
@THEFT_DEDUCTIBLE_AMT     decimal (18,2)          
)          
AS          
BEGIN          
          
  
 INSERT INTO POL_DWELLING_COVERAGE          
 (          
 CUSTOMER_ID,          
 POLICY_ID,          
 POLICY_VERSION_ID,          
 DWELLING_ID,          
 --FORM,          
 --FORM_OTHER_DESC,          
 --COVERAGE,          
 --COVERAGE_OTHER_DESC,          
 DWELLING_LIMIT,          
 --DWELLING_PREMIUM,          
 DWELLING_REPLACE_COST,          
 OTHER_STRU_LIMIT,          
 OTHER_STRU_DESC,          
 PERSONAL_PROP_LIMIT,          
 REPLACEMENT_COST_CONTS,          
 LOSS_OF_USE,          
 --LOSS_OF_USE_PREMIUM,          
 PERSONAL_LIAB_LIMIT,          
 --PERSONAL_LIAB_PREMIUM,          
 MED_PAY_EACH_PERSON,          
 --MED_PAY_EACH_PERSON_PREMIUM,          
 --INFLATION_GUARD,          
 ALL_PERILL_DEDUCTIBLE_AMT,          
 --WIND_HAIL_DEDUCTIBLE_AMT,          
 THEFT_DEDUCTIBLE_AMT          
 )          
 VALUES          
 (          
 @CUSTOMER_ID,          
 @POL_ID,          
 @POL_VERSION_ID,          
 @DWELLING_ID,          
 --@FORM,          
 --@FORM_OTHER_DESC,          
 --@COVERAGE,          
 --@COVERAGE_OTHER_DESC,          
 @DWELLING_LIMIT,          
 --@DWELLING_PREMIUM,          
 @DWELLING_REPLACE_COST,          
 @OTHER_STRU_LIMIT,          
 @OTHER_STRU_DESC,          
 @PERSONAL_PROP_LIMIT,          
 @REPLACEMENT_COST_CONTS,          
 @LOSS_OF_USE,          
 --@LOSS_OF_USE_PREMIUM,          
 @PERSONAL_LIAB_LIMIT,          
 --@PERSONAL_LIAB_PREMIUM,          
 @MED_PAY_EACH_PERSON,          
 --@MED_PAY_EACH_PERSON_PREMIUM,          
 --@INFLATION_GUARD,          
 @ALL_PERILL_DEDUCTIBLE_AMT,          
 --@WIND_HAIL_DEDUCTIBLE_AMT,          
 @THEFT_DEDUCTIBLE_AMT          
 )        
   
   
  
END        
        
      
    
    
    
    
    
  



GO

