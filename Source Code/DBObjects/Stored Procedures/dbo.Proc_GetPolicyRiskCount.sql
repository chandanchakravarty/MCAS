IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRiskCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRiskCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ===========================================================================================================                                                                                      
Proc Name                : dbo.Proc_GetProductRule_Pol                                                                                                                                   
Created by               : Lalit Chauhan                                                                                                                                             
Date                     : OCT 20,2010                                                                                                                              
Purpose                  : COUNT PRODUCT RISK  
Used In                  : EbixAdvantage  web   
  
<MODIFICATION HISTORY>  
Date      :   
Purpose      :   
Modified By     :   
===========================================================================================================                                                                                      
Date     Review By          Comments     
DROP PROC Proc_GetPolicyRiskCount   2156,430,1,17                                                                                                                                              
=========================================================================================================      
*/  
CREATE PROC [dbo].[Proc_GetPolicyRiskCount]  
(  
@CUSTOMER_ID INT,  
@POLICY_ID INT,  
@POLICY_VERSION_ID INT,  
@LOB_ID INT, 
@COUNT INT = NULL OUT  
  
)  
AS  
BEGIN  
DECLARE @RISK_COUNT INT  
  
   IF(@LOB_ID in (9,26)) --All Risk And Named Perils  
     SELECT @RISK_COUNT = COUNT(PERIL_ID) FROM POL_PERILS  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  
     
   ELSE  IF(@LOB_ID = 13)  
     SELECT @RISK_COUNT = COUNT(MARITIME_ID) FROM POL_MARITIME  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  
     
   ELSE  IF(@LOB_ID in(17,18,28,29,31,30,36))  
     SELECT @RISK_COUNT = COUNT(VEHICLE_ID) FROM POL_CIVIL_TRANSPORT_VEHICLES  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  
     
   ELSE  IF(@LOB_ID in (20,23))  
     SELECT @RISK_COUNT = COUNT(COMMODITY_ID) FROM POL_COMMODITY_INFO  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  
     
   ELSE  IF(@LOB_ID in (15,21,33,34))  
     SELECT @RISK_COUNT = COUNT(PERSONAL_INFO_ID) FROM POL_PERSONAL_ACCIDENT_INFO  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  
     
   ELSE  IF(@LOB_ID  = 22)  
     SELECT @RISK_COUNT = COUNT(PERSONAL_ACCIDENT_ID) FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  
     
   ELSE IF(@LOB_ID  in (10,11,12,14,16,19,25,27,32))  
     SELECT @RISK_COUNT = COUNT(PRODUCT_RISK_ID) FROM POL_PRODUCT_LOCATION_INFO  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  
  
  ELSE IF (@LOB_ID IN (35,37) )  --RURAL LIEN         
    SELECT @RISK_COUNT = COUNT(PENHOR_RURAL_ID) FROM POL_PENHOR_RURAL_INFO  WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND ISNULL(IS_ACTIVE,'Y')='Y'  


      
    SET @COUNT =  @RISK_COUNT  
    --SELECT @COUNT  
  
END

GO

