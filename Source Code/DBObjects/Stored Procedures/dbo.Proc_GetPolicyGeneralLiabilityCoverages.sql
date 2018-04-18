IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyGeneralLiabilityCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyGeneralLiabilityCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetPolicyGeneralLiabilityCoverages          
Created by      : Ravindra         
Date            : 03-28-2006      
Purpose         : To Fetch record from APP_GENERAL_COVERAGE_LIMITS table      
Revison History :          
Used In         :   Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--- drop proc Proc_GetPolicyGeneralLiabilityCoverages  
CREATE PROC dbo.Proc_GetPolicyGeneralLiabilityCoverages  
(          
  @CUSTOMER_ID              int,          
  @POLICY_ID                int,          
  @POLICY_VERSION_ID        smallint        
)          
AS          
         
BEGIN        
          
 SELECT   
  COVERAGE_L_AMOUNT ,  
  COVERAGE_L_ID ,  
  COVERAGE_L_AGGREGATE ,  
  COVERAGE_O_AMOUNT ,  
  COVERAGE_O_ID ,  
  COVERAGE_O_AGGREGATE ,  
  COVERAGE_M_EACH_PERSON_AMOUNT ,  
  COVERAGE_M_EACH_PERSON_ID ,  
  COVERAGE_M_EACH_OCC_AMOUNT,  
  COVERAGE_M_EACH_OCC_ID,  
  TOTAL_GENERAL_AGGREGATE,       
  IS_ACTIVE  
      FROM  POL_GENERAL_COVERAGE_LIMITS       
  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
   POLICY_ID = @POLICY_ID AND        
   POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
END  
     



GO

