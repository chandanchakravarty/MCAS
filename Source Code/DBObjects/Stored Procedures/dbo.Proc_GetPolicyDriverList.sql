IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDriverList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDriverList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                
Proc Name                : Dbo.Proc_GetPolicyDriverList          
Created by               : Vijay Arora          
Date                     : 21-03-2006          
Purpose                  : Get the Driver ID of the Particular Policy.          
Revison History          :                                                                
Used In                  : Wolverine                                                                
------------------------------------------------------------                                                                
Date     Review By          Comments                                                                
------   ------------       -------------------------*/                                                                
-- DROP PROC Proc_GetPolicyDriverList        
CREATE proc Dbo.Proc_GetPolicyDriverList                                                             
(                                                                
 @CUSTOMER_ID    INT,                                                                
 @POLICY_ID    INT,                                                                
 @POLICY_VERSION_ID   INT               
)                                                                
AS          
BEGIN          
          
          
SELECT     PDD.CUSTOMER_ID, PDD.POLICY_ID, PDD.POLICY_VERSION_ID, PDD.DRIVER_ID, PDD.DRIVER_FNAME, PDD.DRIVER_MNAME,           
           PDD.DRIVER_LNAME, PDD.DRIVER_CITY, PDD.DRIVER_ZIP, PDD.DRIVER_CODE, PDD.DRIVER_SUFFIX,           
           PDD.DRIVER_ADD1, PDD.DRIVER_ADD2, PDD.DRIVER_STATE, PDD.DRIVER_DOB,           
           PDD.DRIVER_SSN, PDD.DRIVER_SEX, PDD.DRIVER_DRIV_LIC,           
           MNT_COUNTRY_STATE_LIST.STATE_CODE,  
    (SELECT STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID) AS STATE_ID,
	  MNT_COUNTRY_STATE_LIST.STATE_NAME
FROM       POL_DRIVER_DETAILS PDD LEFT JOIN          
           MNT_COUNTRY_STATE_LIST ON PDD.DRIVER_LIC_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID           
     AND PDD.DRIVER_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID        
WHERE (CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)          
END          
    
  



GO

