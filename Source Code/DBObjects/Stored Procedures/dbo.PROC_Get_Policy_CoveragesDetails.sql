IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_Get_Policy_CoveragesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_Get_Policy_CoveragesDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
/*                                                                      
----------------------------------------------------------                                                                          
Proc Name       : dbo.Proc_GetProductRiskId                                                                     
Created by      : Lalit Kumar Chauhan                                                                       
Date            : April-30-2010        
Purpose         : Selects records from POL_PRODUCT_COVERAGES  For Policy      
Revison History :                                                                          
Used In         : EbixAdvantage        
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------          
drop Proc Proc_GetProductRiskId 2101,45,1,9      
sp_find Proc_GetProductRiskId,p      
LOBID   DESC    
1   Homeowners    
2  Automobile    
3  Motorcycle    
4  Watercraft    
5  Umbrella    
6  Rental    
7  General Liability    
8  Aviation    
9  All Risks and Named Perils    
10  Comprehensive Condominium    
11  Comprehensive Company    
12  General Civil Liability    
13  Maritime    
14  Diversified Risks    
15  Individual Personal Accident    
16  Robbery    
17  Facultative Liability    
18  Civil Liability Transportation    
19  Dwelling    
20  National Cargo Transport    
21  Group Passenger Personal Accident     
22  Passenger Personal Accident     
23  International Cargo Transport     
    
    Proc_GetPolicyCoveragePremium
       drop proc PROC_GETPRODUCTRISKID          
         sp_find PROC_Get_Policy_CoveragesPremimum,p                                                        
         
*/                                                                      
CREATE PROC [dbo].[PROC_Get_Policy_CoveragesDetails]     
(     
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT,    
@LOBID INT     
)      
AS      
BEGIN    

   
  --IF(@LOBID=1)  
    
  --ELSE IF (@LOBID=2)  
    
  --ELSE IF (@LOBID=3)  
    
  --ELSE IF (@LOBID=4)  
    
  --ELSE IF (@LOBID=5)  
    
  --ELSE IF (@LOBID=6)  
    
  --ELSE IF (@LOBID=7)  
    
  --ELSE IF (@LOBID=8)  
    
  IF (@LOBID=9)  
	  BEGIN
		    
		 SELECT 
		 POL_COV.RISK_ID,
		 POL_COV.COVERAGE_CODE_ID,
		 MNT_COV.COV_DES,
		 ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,    
		 ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,    
		 ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE
		     
		 FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)  
		  
		 LEFT OUTER JOIN    
		   POL_PERILS POL_RISKINFO WITH(NOLOCK) ON      
		   POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND 
		   POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND
		   POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND 
		   POL_COV.RISK_ID=POL_RISKINFO.PERIL_ID      
		   
		 LEFT OUTER JOIN          
		   MNT_COVERAGE MNT_COV WITH(NOLOCK) ON    
		   POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID
		          
		 WHERE     
		  POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID
		  
	  END
  
  
  
  
  ELSE IF (@LOBID=10 or  @LOBID=11 or @LOBID=12 or @LOBID=14 or @LOBID=16)  
	  BEGIN
		 SELECT 
		 POL_COV.RISK_ID,
		 POL_COV.COVERAGE_CODE_ID,
		 MNT_COV.COV_DES,
		 ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,    
		 ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,    
		 ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE
		     
		 FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)  
		  
		 LEFT OUTER JOIN    
		   POL_PRODUCT_LOCATION_INFO POL_RISKINFO WITH(NOLOCK) ON      
		   POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND 
		   POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND
		   POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND 
		   POL_COV.RISK_ID=POL_RISKINFO.PRODUCT_RISK_ID      
		   
		 LEFT OUTER JOIN          
		   MNT_COVERAGE MNT_COV WITH(NOLOCK) ON    
		   POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID
		          
		 WHERE     
		  POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID
		  
	  
	  END  
    --AS RISK_ID
  --ELSE IF (@LOBID=13)    
    
  --ELSE IF (@LOBID=15)   
    
  --ELSE IF (@LOBID=17)  
    
  --ELSE IF (@LOBID=18)  
    
  --ELSE IF (@LOBID=19)  
    
  --ELSE IF (@LOBID=20)  
    
  --ELSE IF (@LOBID=21)  
    
  --ELSE IF (@LOBID=22)  
    
  --ELSE IF (@LOBID=23)  
  
    
    
END    
    
    
    
      
    
         
    
    
        
        
      
      
      
      
      
GO

