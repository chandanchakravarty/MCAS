IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_DWELLING_COVERAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_DWELLING_COVERAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC Dbo.Proc_UpdatePOL_DWELLING_COVERAGE          
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
          
UPDATE POL_DWELLING_COVERAGE          
SET           
--FORM=@FORM,          
--FORM_OTHER_DESC=@FORM_OTHER_DESC,          
--COVERAGE=@COVERAGE,          
--COVERAGE_OTHER_DESC=@COVERAGE_OTHER_DESC,          
DWELLING_LIMIT=@DWELLING_LIMIT,          
--DWELLING_PREMIUM=@DWELLING_PREMIUM,          
DWELLING_REPLACE_COST=@DWELLING_REPLACE_COST,          
OTHER_STRU_LIMIT=@OTHER_STRU_LIMIT,          
      
PERSONAL_PROP_LIMIT=@PERSONAL_PROP_LIMIT,          
REPLACEMENT_COST_CONTS=@REPLACEMENT_COST_CONTS,          
LOSS_OF_USE=@LOSS_OF_USE,          
--LOSS_OF_USE_PREMIUM=@LOSS_OF_USE_PREMIUM,          

--PERSONAL_LIAB_PREMIUM=@PERSONAL_LIAB_PREMIUM,          
MED_PAY_EACH_PERSON=@MED_PAY_EACH_PERSON,          
			/*when called from General Information Page  retain the previous value*/         
ALL_PERILL_DEDUCTIBLE_AMT=
							CASE @ALL_PERILL_DEDUCTIBLE_AMT
							WHEN  -1
							 THEN  ALL_PERILL_DEDUCTIBLE_AMT
							ELSE
							      @ALL_PERILL_DEDUCTIBLE_AMT
							END,
			/*when called from General Information Page  retain the previous value*/         
THEFT_DEDUCTIBLE_AMT=          
						CASE @THEFT_DEDUCTIBLE_AMT
						WHEN -1
						 THEN THEFT_DEDUCTIBLE_AMT          
						ELSE
						 @THEFT_DEDUCTIBLE_AMT
						END,
			/*when called from General Information Page  retain the previous value*/         
OTHER_STRU_DESC=
					CASE @OTHER_STRU_DESC
					WHEN  -1
					 THEN OTHER_STRU_DESC
					ELSE
					      @OTHER_STRU_DESC         
					END,
PERSONAL_LIAB_LIMIT=@PERSONAL_LIAB_LIMIT          
WHERE  CUSTOMER_ID=@CUSTOMER_ID   AND POLICY_ID=@POL_ID  AND POLICY_VERSION_ID= @POL_VERSION_ID AND DWELLING_ID=@DWELLING_ID          
        
       
      
END        
        
      
    
  



GO

