IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDwellingCoverageXmlForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDwellingCoverageXmlForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE Proc_GetDwellingCoverageXmlForPolicy  
(    
 @CUSTOMER_ID     int,    
 @POLICY_ID     int,    
 @POLICY_VERSION_ID     smallint,    
 @DWELLING_ID     smallint     
)    
AS    
BEGIN    
SELECT     
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
ceiling (ALL_PERILL_DEDUCTIBLE_AMT) ALL_PERILL_DEDUCTIBLE_AMT,    
--WIND_HAIL_DEDUCTIBLE_AMT,    
THEFT_DEDUCTIBLE_AMT    
FROM POL_DWELLING_COVERAGE    
WHERE      
CUSTOMER_ID=@CUSTOMER_ID          AND     
POLICY_ID=@POLICY_ID                                      AND     
POLICY_VERSION_ID= @POLICY_VERSION_ID AND     
DWELLING_ID=@DWELLING_ID    
END  
  



GO

