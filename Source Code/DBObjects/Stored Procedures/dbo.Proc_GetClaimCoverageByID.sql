IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimCoverageByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimCoverageByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
 /*----------------------------------------------------------                                                              
Proc Name             : Dbo.Proc_GetClaimCoverageByID                                                              
Created by            : Santosh Kumar Gautam                                                             
Date                  : 31/01/2011                                                             
Purpose               : To fetch the claim coverage                
Revison History       :                                                              
Used In               : claim module                    
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc Proc_GetClaimCoverageByID                                                    
------   ------------       -------------------------*/                                                              
--                                 
                                  
--                               
                            
CREATE PROCEDURE [dbo].[Proc_GetClaimCoverageByID]                                  
                                 
                 
 @CLAIM_ID        int                    
,@CLAIM_COV_ID    smallint           
                                  
AS                                  
BEGIN                       
    
SELECT CLAIM_COV_ID, 
      COVERAGE_CODE_ID,
      IS_RISK_COVERAGE,    
      LIMIT_1,
      LIMIT_OVERRIDE,
      POLICY_LIMIT,
      DEDUCTIBLE_1,
      POLICY_LIMIT  ,
      DEDUCTIBLE1_AMOUNT_TEXT   ,
      IS_USER_CREATED  ,
      MINIMUM_DEDUCTIBLE,
      VICTIM_ID,
      RI_APPLIES,  
      C.IS_ACTIVE,  
      C.CREATED_BY,  
	  C.CREATED_DATETIME,  
	  C.MODIFIED_BY,  
	  C.LAST_UPDATED_DATETIME ,  
	  0 AS COV_DES      ,  
	  '' AS CREATE_MODE ,
	 CASE WHEN L.IS_VICTIM_CLAIM =10963 THEN 'Y'
	      WHEN L.LITIGATION_FILE =10963 THEN 'Y'
	      ELSE 'N' 
	 END AS ALLOW_COPY,
	 COVERAGE_SI_FLAG,
	 DEDUCTIBLE_1_TYPE -- ADDED BY SANTOSH KR GAUTAM ON 11 AUG 2011 FOR ITRACK 1316 (TFS NO 429)
	 FROM CLM_PRODUCT_COVERAGES C  INNER JOIN
	      CLM_CLAIM_INFO L ON  L.CLAIM_ID=C.CLAIM_ID
	 WHERE (C.CLAIM_ID=@CLAIM_ID AND  CLAIM_COV_ID= @CLAIM_COV_ID )       
    
   
END 
GO

