IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEligiblePolicies]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEligiblePolicies]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetEligiblePolicies                
Created by      : Vijay Arora                
Date            : 14/03/2006                
Purpose        : Gets eligible policies of customer of currently logged user agency.                
Revison History :                        
Used In    : Wolverine                        
-------------------------------------------------------------*/                
--DROP PROC Proc_GetEligiblePolicies                 
CREATE PROC Proc_GetEligiblePolicies                 
(                
  @CUSTOMER_ID INT,                
  @AGENCY_ID INT,                
  @POLICY_LOB INT,          
  @POLICY_NUMBER VARCHAR(150)                
                
)                
AS                
BEGIN                
                
 /*                
 1 HOME Homeowners                
 2 AUTOP Automobile                
 3 CYCL Motorcycle                
 4 BOAT Watercraft                
 5 UMB Umbrella                
 6 REDW Rental                
 7 GENL General Liability                
 */                
DECLARE @TEMP_VAL VARCHAR(10)
                
 IF (@POLICY_LOB IN (2,3,4))                 
  BEGIN                
   SELECT DISTINCT POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST, MNT_AGENCY_LIST                
          WHERE CUSTOMER_ID = @CUSTOMER_ID     
    AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID             
   AND POLICY_NUMBER <>  @POLICY_NUMBER            
    AND POL_CUSTOMER_POLICY_LIST.IS_ACTIVE = 'Y' AND APP_EXPIRATION_DATE > GETDATE() AND POLICY_STATUS = 'NORMAL'          
   AND POLICY_LOB = 1  ORDER BY POLICY_NUMBER              
  END                
                
 ELSE IF (@POLICY_LOB = 1)                 
  BEGIN                
   SELECT  DISTINCT POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST, MNT_AGENCY_LIST            
          WHERE CUSTOMER_ID = @CUSTOMER_ID     
  AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID            
  AND POLICY_NUMBER <>  @POLICY_NUMBER          
 AND POL_CUSTOMER_POLICY_LIST.IS_ACTIVE = 'Y' AND APP_EXPIRATION_DATE > GETDATE() AND POLICY_STATUS = 'NORMAL'           
   AND POLICY_LOB = 2  ORDER BY POLICY_NUMBER              
  END                
                
 ELSE IF (@POLICY_LOB = 6)                  
  BEGIN                
   SELECT  DISTINCT POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST, MNT_AGENCY_LIST              
          WHERE CUSTOMER_ID = @CUSTOMER_ID     
   AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID            
   AND POLICY_NUMBER <>  @POLICY_NUMBER          
 AND POL_CUSTOMER_POLICY_LIST.IS_ACTIVE = 'Y' AND APP_EXPIRATION_DATE > GETDATE()  AND POLICY_STATUS = 'NORMAL'          
   ORDER BY POLICY_NUMBER              
  END

ELSE IF (@POLICY_LOB=5 OR @POLICY_LOB=7)                
  BEGIN
	SET @TEMP_VAL = 'N.A.'
	SELECT @TEMP_VAL AS POLICY_NUMBER
  END
ELSE
	BEGIN
	SET @TEMP_VAL = 'N.A.'
	SELECT @TEMP_VAL AS POLICY_NUMBER
	END 	
END                
             
  




GO

