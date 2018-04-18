IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTotalClaimPaymentofCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTotalClaimPaymentofCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*----------------------------------------------------------                                                                
Proc Name       : dbo.Proc_GetTotalClaimPaymentofCoverage                                                          
Created by      : Santosh Kumar Gautam                                                  
Date            : 05 Aug 2011                                                            
Purpose         : Fetch total maount paid against a coverage in all claims of a same policy
Created by      : Santosh Kumar Gautam                                              
Revison History :                                                                
Used In         : CLAIM                                                                
------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                
------   ------------       -------------------------*/                                                                
--DROP PROC dbo.Proc_GetTotalClaimPaymentofCoverage  
                                                                               
CREATE PROC [dbo].[Proc_GetTotalClaimPaymentOfCoverage] 
 @CLAIM_ID    INT ,                                                                                 
 @COVERAGE_ID INT                                       
AS                                                                
BEGIN                                                                
  
	DECLARE @CUSTOMER_ID		INT  
	DECLARE @POLICY_ID			INT
	DECLARE @POLICY_VERSION_ID  INT

	SELECT @CUSTOMER_ID			= CUSTOMER_ID,
		   @POLICY_ID			= POLICY_ID  ,
		   @POLICY_VERSION_ID   = POLICY_VERSION_ID
	FROM CLM_CLAIM_INFO WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID
	 
	-- FETCH TOTAL PAYMENT AMOUNT FOR A COVERAGE FOR GIVEN POLICY
SELECT * FROM
(
	SELECT CLAIM_NUMBER,SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM 
	(

	SELECT  CCI.CLAIM_NUMBER , CAR.PAYMENT_AMOUNT, CPC.COVERAGE_CODE_ID
	FROM CLM_CLAIM_INFO CCI   WITH(NOLOCK)INNER JOIN 
	CLM_ACTIVITY   CA         WITH(NOLOCK) ON CCI.CLAIM_ID=CA.CLAIM_ID AND CA.ACTION_ON_PAYMENT IN (180,181)
											  AND CA.IS_ACTIVE='Y' AND CA.IS_VOIDED_REVERSED_ACTIVITY IS NULL
											  AND CA.ACTIVITY_STATUS =11801 LEFT OUTER JOIN
	CLM_PRODUCT_COVERAGES CPC WITH(NOLOCK) ON CPC.CLAIM_ID=CA.CLAIM_ID LEFT OUTER JOIN
	CLM_ACTIVITY_RESERVE CAR  WITH(NOLOCK) ON CA.CLAIM_ID=CAR.CLAIM_ID AND CA.ACTIVITY_ID=CAR.ACTIVITY_ID AND CAR.COVERAGE_ID=CPC.CLAIM_COV_ID
	WHERE CCI.CUSTOMER_ID=@CUSTOMER_ID AND CCI.POLICY_ID=@POLICY_ID AND CCI.POLICY_VERSION_ID=@POLICY_VERSION_ID AND
		  CPC.COVERAGE_CODE_ID=@COVERAGE_ID AND CAR.PAYMENT_AMOUNT<>0

	) T
	GROUP BY CLAIM_NUMBER
	)T1
COMPUTE  SUM(PAYMENT_AMOUNT)




     
END  




