IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimPaymentAmountWithPersonalInjury]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimPaymentAmountWithPersonalInjury]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                
Proc Name       : dbo.Proc_GetClaimPaymentAmountWithPersonalInjury                                                          
Created by      : Sumit Chhabra                                                              
Date            : 25 Feb 2011                                                            
Purpose         : Fetch claim payment amount with personal injury flag true
Created by      : Santosh Kumar Gautam                                              
Revison History :                                                                
Used In         : CLAIM                                                                
------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                
------   ------------       -------------------------*/                                                                
--DROP PROC dbo.Proc_GetClaimPaymentAmountWithPersonalInjury                                                                                 
CREATE PROC [dbo].[Proc_GetClaimPaymentAmountWithPersonalInjury]                                                                                   
 @CLAIM_ID int     ,
 @ACTIVITY_ID int                                       
AS                                                                
BEGIN                                                                
  
--DECLARE @DIARY_DATE DATETIME  
  
	--SELECT COVERAGE_CODE_ID AS COVERAGE_ID,SUM(R.PAYMENT_AMOUNT) AS PAYMENT_AMOUNT
	--FROM   CLM_ACTIVITY_RESERVE R  LEFT OUTER JOIN
	--       CLM_PRODUCT_COVERAGES P ON  R.CLAIM_ID=P.CLAIM_ID AND R.COVERAGE_ID =P.CLAIM_COV_ID LEFT OUTER JOIN
	--	   CLM_ACTIVITY A ON  R.CLAIM_ID=A.CLAIM_ID AND R.ACTIVITY_ID =A.ACTIVITY_ID
	--WHERE  R.CLAIM_ID =@CLAIM_ID AND R.PERSONAL_INJURY='Y' AND  A.ACTION_ON_PAYMENT IN(180,181) AND R.ACTIVITY_ID!=@ACTIVITY_ID
	--GROUP by P.COVERAGE_CODE_ID

	 ---------------------------------------------------------------------------
    -- ADDED BY SANTOSH KR GAUTAM ON 05 AUG 2011 REF (ITRACK(1043), WORKITEM-43)
    ---------------------------------------------------------------------------
    DECLARE @CUSTOMER_ID		INT  
	DECLARE @POLICY_ID			INT
	DECLARE @POLICY_VERSION_ID  INT

	SELECT @CUSTOMER_ID			= CUSTOMER_ID,
		   @POLICY_ID			= POLICY_ID  ,
		   @POLICY_VERSION_ID   = POLICY_VERSION_ID
	FROM CLM_CLAIM_INFO WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID
	 
	
	-- FETCH TOTAL PAYMENT AMOUNT FOR GIVEN POLICY
		SELECT COVERAGE_CODE_ID AS COVERAGE_ID,SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT FROM 
		(

		SELECT   CAR.PAYMENT_AMOUNT, CPC.COVERAGE_CODE_ID
		FROM CLM_CLAIM_INFO CCI   WITH(NOLOCK)INNER JOIN 
		CLM_ACTIVITY   CA         WITH(NOLOCK) ON CCI.CLAIM_ID=CA.CLAIM_ID AND CA.ACTION_ON_PAYMENT IN (180,181)
												  AND CA.IS_ACTIVE='Y' AND CA.IS_VOIDED_REVERSED_ACTIVITY IS NULL
												  AND CA.ACTIVITY_STATUS =11801 LEFT OUTER JOIN
		CLM_PRODUCT_COVERAGES CPC WITH(NOLOCK) ON CPC.CLAIM_ID=CA.CLAIM_ID LEFT OUTER JOIN
		CLM_ACTIVITY_RESERVE CAR  WITH(NOLOCK) ON CA.CLAIM_ID=CAR.CLAIM_ID AND CA.ACTIVITY_ID=CAR.ACTIVITY_ID AND CAR.COVERAGE_ID=CPC.CLAIM_COV_ID
		WHERE CCI.CUSTOMER_ID=@CUSTOMER_ID AND CCI.POLICY_ID=@POLICY_ID AND CCI.POLICY_VERSION_ID=@CLAIM_ID AND
			  CAR.PERSONAL_INJURY='Y' AND
			  CPC.COVERAGE_CODE_ID IN (SELECT DISTINCT(COVERAGE_CODE_ID) FROM CLM_PRODUCT_COVERAGES WHERE CLAIM_ID=@CLAIM_ID)			  

		) T
		GROUP BY COVERAGE_CODE_ID




     
END  




GO

