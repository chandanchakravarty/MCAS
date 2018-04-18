IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAndUpdatePlanApplicability]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAndUpdatePlanApplicability]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------------  
Created By  : Ravindra Gupta  
Created Date  : June 04 2007  
Purpose   : To move Minimum Premium condition for Billing Plan from posting logic  
  
Revision History  
  
Modified By   :   ac
Purpose   :   
  
Reviewed by : Anurag Verma  
Reviewed On : 12-07-2007  s
****************************************************************************************/  
-- drop proc dbo.Proc_CheckAndUpdatePlanApplicability  
  
CREATE proc  dbo.Proc_CheckAndUpdatePlanApplicability  
 (  
 @CUSTOMER_ID   int,   -- Id of customer whose premium policy will be posted  
 @POLICY_ID  int,   -- Policy identification number  
 @POLICY_VERSION_ID int,   -- Policy version identification number  
 @PREMIUM_AMOUNT  decimal(25,8),  -- Premium amount  
 @MCCA_FEES  decimal(25,8),  -- MCCA fees amount  
 @OTHER_FEES  decimal(25,8),  -- Other fees  
 @TRANS_DESC  varchar(255) OUTPUT, -- Transaction descrtipyion   
 @RetVal   int  OUTPUT   
)  
AS  
BEGIN   
   
DECLARE @MIN_INSTALLMENT_AMOUNT DECIMAL(25,2) ,  
 @INSTALLMENT  Int  
  
IF EXISTS (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST   
  WHERE CUSTOMER_ID = @CUSTOMER_ID   
   AND POLICY_ID = @POLICY_ID   
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
   AND BILL_TYPE = 'AB')  
BEGIN 
	SET @RetVal = 1    
	RETURN  
END  
--Fetching the application or policy information  
SELECT  @MIN_INSTALLMENT_AMOUNT  =  ISNULL(IPD.MIN_INSTALLMENT_AMOUNT, 0)  
FROM POL_CUSTOMER_POLICY_LIST CPL  
LEFT JOIN ACT_INSTALL_PLAN_DETAIL IPD   
 ON IPD.IDEN_PLAN_ID = CPL.INSTALL_PLAN_ID  
WHERE CPL.CUSTOMER_ID = @CUSTOMER_ID   
AND CPL.POLICY_ID = @POLICY_ID   
AND CPL.POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
  
IF @PREMIUM_AMOUNT + ISNULL(@MCCA_FEES,0) + ISNULL(@OTHER_FEES,0) < @MIN_INSTALLMENT_AMOUNT   
BEGIN  
	IF EXISTS ( SELECT IDEN_PLAN_ID FROM ACT_INSTALL_PLAN_DETAIL   
		WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0) = 1 )  
	BEGIN  
		--Peemium amount is less then min installment amount, hence changing installment plan to Full Pay Plan  
		SELECT @INSTALLMENT = IDEN_PLAN_ID   
		FROM ACT_INSTALL_PLAN_DETAIL  
		WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0) = 1  
    
		--Updating the installment field of applist table also  
		UPDATE POL_CUSTOMER_POLICY_LIST  
		SET INSTALL_PLAN_ID = @INSTALLMENT  
		WHERE POLICY_ID = @POLICY_ID   
		AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
		AND CUSTOMER_ID = @CUSTOMER_ID  
		--Updating the transaction description  
		SET @TRANS_DESC = 'Policy billing plan has been updated to default plan.'  
		SET @RetVal = 2   
	END   
	ELSE  
	BEGIN   
		SET @RetVal = -1  
	END  
END  
ELSE  
BEGIN  
	SET @RetVal = 1  
END  
END  
  
  
  
  
  





GO

