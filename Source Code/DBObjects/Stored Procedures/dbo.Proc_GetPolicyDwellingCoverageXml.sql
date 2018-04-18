IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDwellingCoverageXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDwellingCoverageXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE Proc_GetPolicyDwellingCoverageXml    
(    
 @CUSTOMER_ID     int,    
 @POL_ID     int,    
 @POL_VERSION_ID     smallint,    
 @DWELLING_ID     smallint     
)    
AS    
BEGIN    
SELECT     
CUSTOMER_ID,    
POLICY_ID,    
POLICY_VERSION_ID,    
DWELLING_ID,    
DWELLING_LIMIT,    
DWELLING_REPLACE_COST,    
OTHER_STRU_LIMIT,    
OTHER_STRU_DESC,    
PERSONAL_PROP_LIMIT,
REPLACEMENT_COST_CONTS,    
LOSS_OF_USE,    
CAST(PERSONAL_LIAB_LIMIT as int) as PERSONAL_LIAB_LIMIT, 
MED_PAY_EACH_PERSON,    
ceiling (ALL_PERILL_DEDUCTIBLE_AMT) ALL_PERILL_DEDUCTIBLE_AMT,    
THEFT_DEDUCTIBLE_AMT    
FROM POL_DWELLING_COVERAGE    
WHERE      
CUSTOMER_ID=@CUSTOMER_ID          AND     
POLICY_ID=@POL_ID                                      AND     
POLICY_VERSION_ID= @POL_VERSION_ID AND     
DWELLING_ID=@DWELLING_ID    
END    
    
    



GO

