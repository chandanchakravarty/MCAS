IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyGeneralUnderWriting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyGeneralUnderWriting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetPolicyGeneralUnderWriting  
Created by      : Sumit Chhabra
Date            : 03/05/2006  
Purpose       	: Fetch data from POL_GENERAL_UNDERWRITING_INFO
Revison History :  

------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetPolicyGeneralUnderWriting  
(  
@CUSTOMER_ID     int,  
@POLICY_ID     int,  
@POLICY_VERSION_ID     smallint  
  
)  
AS  
BEGIN  
SELECT   
  
CUSTOMER_ID,  
POLICY_ID,  
POLICY_VERSION_ID,  
INSURANCE_DECLINED_FIVE_YEARS,  
MEDICAL_PROFESSIONAL_EMPLOYEED,  
EXPOSURE_RATIOACTIVE_NUCLEAR,  
HAVE_PAST_PRESENT_OPERATIONS,  
ANY_OPERATIONS_SOLD,  
MACHINERY_LOANED,  
ANY_WATERCRAFT_LEASED,  
ANY_PARKING_OWNED,  
FEE_CHARGED_PARKING,  
RECREATION_PROVIDED,  
SWIMMING_POOL_PREMISES,  
SPORTING_EVENT_SPONSORED,  
STRUCTURAL_ALTERATION_CONTEMPATED,  
DEMOLITION_EXPOSURE_CONTEMPLATED,  
CUSTOMER_ACTIVE_JOINT_VENTURES,  
LEASE_EMPLOYEE,  
LABOR_INTERCHANGE_OTH_BUSINESS,  
DAY_CARE_FACILITIES,  
IS_ACTIVE,  
CREATED_BY,  
CREATE_DATETIME,  
MODIFIED_BY,  
LAST_UPDATED_DATETIME,  
ADDITIONAL_COMMENTS,  
DESC_INSURANCE_DECLINED,  
DESC_MEDICAL_PROFESSIONAL,  
DESC_EXPOSURE_RATIOACTIVE,  
DESC_HAVE_PAST_PRESENT,  
DESC_ANY_OPERATIONS,  
DESC_MACHINERY_LOANED,  
DESC_ANY_WATERCRAFT,  
DESC_ANY_PARKING,  
DESC_FEE_CHARGED,  
DESC_RECREATION_PROVIDED,  
DESC_SWIMMING_POOL,  
DESC_SPORTING_EVENT,  
DESC_STRUCTURAL_ALTERATION,  
DESC_DEMOLITION_EXPOSURE,  
DESC_CUSTOMER_ACTIVE,  
DESC_LEASE_EMPLOYEE,  
DESC_LABOR_INTERCHANGE,  
DESC_DAY_CARE  
FROM  POL_GENERAL_UNDERWRITING_INFO  
WHERE  
CUSTOMER_ID =@CUSTOMER_ID AND  
POLICY_ID=@POLICY_ID AND  
POLICY_VERSION_ID=@POLICY_VERSION_ID  
END  
  



GO

