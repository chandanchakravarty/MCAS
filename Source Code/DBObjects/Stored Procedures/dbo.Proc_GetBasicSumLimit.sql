IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBasicSumLimit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBasicSumLimit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                        
 Proc Name       : dbo.Proc_GetBasicSumLimit              
 Created by      : Praveer Panghal        
 Date            : jan, 2011          
 Purpose         : Get GetBasicSumLimit       
 Revison History :                        
 Used In     : Ebix Advantage                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------     
*/-- drop proc Proc_GetBasicSumLimit 9,28033,52,1,1
CREATE PROC [dbo].[Proc_GetBasicSumLimit]  
(
    @LOB_ID INT ,
	@CUSTOMER_ID  INT,                  
	@POLICY_ID  INT,                
	@POLICY_VERSION_ID INT ,
	@PRODUCT_RISK_ID INT	
)
AS
DECLARE @BASIC_SUM_LIMIT DECIMAL(18,2),
	@VALUE_AT_RISK DECIMAL(18,2)
	
BEGIN

   SELECT @BASIC_SUM_LIMIT = ISNULL(MAX(LIMIT_1),0)   
			   FROM POL_PRODUCT_COVERAGES WITH(NOLOCK)  
			   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  
			     and RISK_ID=@PRODUCT_RISK_ID

IF(@LOB_ID IN(10,11,19,14) ) --Comprehensive Condominium,Comprehensive Company ,Dwelling AND Diversified Risks
	BEGIN
		
		 SELECT @VALUE_AT_RISK=ISNULL(VALUE_AT_RISK,0) FROM POL_PRODUCT_LOCATION_INFO WITH(NOLOCK) 
				 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 
				 AND PRODUCT_RISK_ID=@PRODUCT_RISK_ID
			 IF(@BASIC_SUM_LIMIT>@VALUE_AT_RISK)
			 BEGIN	 
				UPDATE  POL_PRODUCT_LOCATION_INFO SET VALUE_AT_RISK=@BASIC_SUM_LIMIT 
				WHERE CUSTOMER_ID=@CUSTOMER_ID  and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 
					AND PRODUCT_RISK_ID=@PRODUCT_RISK_ID
		     END
   END   
 IF(@LOB_ID =9 ) --All Risks and Named Perils
	BEGIN
	
	 SELECT @VALUE_AT_RISK=ISNULL(VR,0) FROM POL_PERILS WITH(NOLOCK) 
				 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID 
				 AND PERIL_ID=@PRODUCT_RISK_ID	 
		 IF(@BASIC_SUM_LIMIT>@VALUE_AT_RISK)
			 BEGIN	 
			UPDATE  POL_PERILS SET VR=@BASIC_SUM_LIMIT WHERE CUSTOMER_ID=@CUSTOMER_ID  and POLICY_ID=@POLICY_ID
			 and POLICY_VERSION_ID=@POLICY_VERSION_ID 
			AND PERIL_ID=@PRODUCT_RISK_ID
		END
  END
END


GO

