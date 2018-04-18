IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicy_PrimaryApplicant_NameInsured]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicy_PrimaryApplicant_NameInsured]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN  
--drop Proc Proc_GetPolicy_PrimaryApplicant_NameInsured        
--GO  
/*----------------------------------------------------------            
Proc Name          : Dbo.Proc_GetPolicy_PrimaryApplicant_NameInsured         
Created by         : Sibin Philip          
Date               : 24 Nov 2009          
Purpose            :             
Revison History :            
Used In            :   Wolverine              
------------------------------------------------------------            
Date     Review By          Comments        
drop Proc Proc_GetPolicy_PrimaryApplicant_NameInsured        
------   ------------       -------------------------*/            
CREATE   PROCEDURE [dbo].[Proc_GetPolicy_PrimaryApplicant_NameInsured]          
(            
 @CUSTOMER_ID  int ,  
 @POLICY_ID INT,  
 @POLICY_VERSION_ID INT            
)            
AS            
BEGIN   
  
 IF EXISTS(SELECT CAL.CUSTOMER_ID FROM CLT_APPLICANT_LIST CAL INNER JOIN POL_APPLICANT_LIST PAL        
     ON CAL.CUSTOMER_ID = PAL.CUSTOMER_ID AND CAL.APPLICANT_ID= PAL.APPLICANT_ID        
     WHERE PAL.CUSTOMER_ID = @CUSTOMER_ID AND PAL.POLICY_ID=@POLICY_ID         
   AND PAL.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PAL.IS_PRIMARY_APPLICANT = 1   
    )  
 BEGIN  
  SELECT ISNULL(CAL.FIRST_NAME,'') + ' ' + CASE ISNULL(CAL.MIDDLE_NAME,'') WHEN '' THEN '' ELSE ISNULL(CAL.MIDDLE_NAME,'') END + ' ' +         
  ISNULL(CAL.LAST_NAME,'') AS CUSTOMER_NAME,   
  CAL.ADDRESS1 AS CUSTOMER_ADDRESS1,   
  CAL.ADDRESS2 AS CUSTOMER_ADDRESS2,  
  CAL.CITY AS CUSTOMER_CITY, CAL.STATE AS CUSTOMER_STATE,        
  CAL.ZIP_CODE AS CUSTOMER_ZIP,  
  CAL.PHONE AS CUSTOMER_HOME_PHONE,  
  CAL.MOBILE AS CUSTOMER_MOBILE,  
  CAL.BUSINESS_PHONE AS CUSTOMER_BUSINESS_PHONE,  
  CAL.EXT AS CUSTOMER_EXT,   
  CAL.COUNTRY AS CUSTOMER_COUNTRY ,    
  CAL.APPLICANT_TYPE 
  FROM CLT_APPLICANT_LIST CAL INNER JOIN POL_APPLICANT_LIST PAL        
  ON CAL.CUSTOMER_ID = PAL.CUSTOMER_ID AND CAL.APPLICANT_ID= PAL.APPLICANT_ID        
  WHERE PAL.CUSTOMER_ID = @CUSTOMER_ID AND PAL.POLICY_ID=@POLICY_ID         
  AND PAL.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PAL.IS_PRIMARY_APPLICANT = 1   
 END  
  
   
   
END  
--  
--GO  
--EXEC Proc_GetPolicy_PrimaryApplicant_NameInsured 1988,2,2       
--ROLLBACK TRAN  
  
GO

