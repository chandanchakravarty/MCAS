IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivate_MasterPolicyRisk]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivate_MasterPolicyRisk]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*               
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_ActivateDeactivate_MasterPolicyRisk                         
Created by      : LALIT CHAUHAN                        
Date            : Oct 20 , 2010    
Purpose         : Activate Deactivate policy risk behalf on endorsment effective or expiry date for master policy    
Used In   : Ebix Advantage                                    
    
===========================================    
Modified By  : Lalit Chauhan     
Modify Date  : Nov 08 , 2010    
===========================================    
Modified By   : Lalit Kr Chauhan       
Modified date  : 13/04/2011      
Purpose   :   risk will be deactivated only for Co-Applicant selected on endorsement screen.i-track #948    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                    
drop proc Proc_ActivateDeactivate_MasterPolicyRisk    
exec Proc_ActivateDeactivate_MasterPolicyRisk 2156,274,2,'11/08/2010','10/25/2011'    
*/                  
CREATE PROC [dbo].[Proc_ActivateDeactivate_MasterPolicyRisk]     
(    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID INT,    
@END_EFFECTIVE_DATE DATETIME,    
@END_EXPIRY_DATE DATETIME ,    
@RIKS_ID INT = null,    
@LOB_ID INT = null    
)    
AS     
BEGIN    
DECLARE @PRODUCT_TYPE INT,@PLAN_TYPE NVARCHAR(10),    
  @MASTER_POLICY INT,    
  @OPEN_POLICY INT  = 14560, -- implimented for master policy if policy transaction type is open policy    
  @END_CO_APP_ID INT ,  
  @CURRENT_TERM INT   
     
 SELECT @LOB_ID = POL.POLICY_LOB ,    
 --Modified By Lalit, Nov 08 2010    
 @PRODUCT_TYPE = POL.TRANSACTION_TYPE , --ISNULL(LOB.PRODUCT_TYPE,14681),    
 @PLAN_TYPE = INSTALL_PLAN.PLAN_TYPE  ,  
 @CURRENT_TERM = CURRENT_TERM  
 FROM POL_CUSTOMER_POLICY_LIST POL  WITH(NOLOCK)INNER JOIN    
 MNT_LOB_MASTER LOB   WITH(NOLOCK) ON    
 POL.POLICY_LOB = LOB.LOB_ID     
 LEFT OUTER JOIN ACT_INSTALL_PLAN_DETAIL  INSTALL_PLAN WITH(NOLOCK) ON    
 POL.INSTALL_PLAN_ID = INSTALL_PLAN.IDEN_PLAN_ID     
 WHERE POL.CUSTOMER_ID=@CUSTOMER_ID AND POL.POLICY_ID =@POLICY_ID AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID    
     
 SELECT @END_CO_APP_ID = CO_APPLICANT_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID    
 AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID     
     
     
     
 --AND @PLAN_TYPE = 'MAUTO'     
 --policy risk expiry date is less then endorsement effective date.    
   IF(@PRODUCT_TYPE = @OPEN_POLICY )    
    BEGIN    
    IF(@PLAN_TYPE = 'MAUTO')    
    BEGIN    
     IF(@LOB_ID =18 OR @LOB_ID =17)--Civil Liability Transportation/facultative Liability    
    --UPDATE POL_CIVIL_TRANSPORT_VEHICLES SET IS_ACTIVE='N' WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
    -- POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK_EXPIRE_DATE < @END_EFFECTIVE_DATE    
    -- AND CO_APPLICANT_ID =  @END_CO_APP_ID    
      
      
     UPDATE P SET IS_ACTIVE = 'N' FROM   
 POL_CIVIL_TRANSPORT_VEHICLES P   
 JOIN  
 (  
  SELECT VEHICLE_ID,ORIGINAL_VERSION_ID,POLICY_VERSION_ID,CUSTOMER_ID ,POLICY_ID,CO_APPLICANT_ID  
   FROM POL_CIVIL_TRANSPORT_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
  AND  RISK_EXPIRE_DATE < @END_EFFECTIVE_DATE AND POLICY_VERSION_ID <> @POLICY_VERSION_ID AND CO_APPLICANT_ID = @END_CO_APP_ID AND IS_ACTIVE = 'Y'  
  )A  
  ON A.CUSTOMER_ID = P.CUSTOMER_ID AND A.POLICY_ID = P.POLICY_ID AND P.POLICY_VERSION_ID > A.POLICY_VERSION_ID --AND   
  AND P.VEHICLE_ID = A.VEHICLE_ID AND P.CO_APPLICANT_ID = A.CO_APPLICANT_ID  
      
         
   ELSE IF(@LOB_ID =22) --For Personal Accident for Passengers    
   BEGIN    
     
   UPDATE P SET IS_ACTIVE = 'N' FROM   
 POL_PASSENGERS_PERSONAL_ACCIDENT_INFO P   
 JOIN  
 (  
  SELECT PERSONAL_ACCIDENT_ID,ORIGINAL_VERSION_ID,POLICY_VERSION_ID,CUSTOMER_ID ,POLICY_ID,CO_APPLICANT_ID  
   FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
  AND END_DATE < @END_EFFECTIVE_DATE AND POLICY_VERSION_ID <> @POLICY_VERSION_ID AND CO_APPLICANT_ID = @END_CO_APP_ID AND IS_ACTIVE = 'Y'  
  )A  
  ON A.CUSTOMER_ID = P.CUSTOMER_ID AND A.POLICY_ID = P.POLICY_ID AND P.POLICY_VERSION_ID > A.POLICY_VERSION_ID --AND   
  AND P.PERSONAL_ACCIDENT_ID = A.PERSONAL_ACCIDENT_ID AND P.CO_APPLICANT_ID = A.CO_APPLICANT_ID AND IS_ACTIVE = 'Y'  
      
   END      
   --ELSE IF(@LOB_ID =21) --For Group personal pessange Accident    
  -- BEGIN    
   -- UPDATE POL_PERSONAL_ACCIDENT_INFO SET IS_ACTIVE='N' WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
    -- POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID     
   --  END    
   END    
        
 END    
END    
GO

