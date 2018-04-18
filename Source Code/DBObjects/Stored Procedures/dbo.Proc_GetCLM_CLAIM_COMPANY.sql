IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_CLAIM_COMPANY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_CLAIM_COMPANY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                      
Proc Name       : dbo.Proc_GetCLM_CLAIM_COMPANY                                                
Created by      : Sumit Chhabra                                                    
Date            : 19/05/2006                                                      
Purpose         : Fetch data from CLM_CLAIM_COMPANY table for watercraft company page            
Created by      : Sumit Chhabra                                                     
Revison History :                                                      
Used In        : Wolverine                                                      
------------------------------------------------------------                                                      
Date     Review By          Comments                                                      
------   ------------       -------------------------*/                                                      
create PROC dbo.Proc_GetCLM_CLAIM_COMPANY               
(                                               
@CLAIM_ID int,            
@COMPANY_ID int,            
@AGENCY_ID int            
)                             
AS                                                      
BEGIN                
DECLARE @POLICY_STATUS varchar(250)        
DECLARE @POLICY_NUMBER nvarchar(75)        
--Get Policy status and policy number corresponding to current claim id        
 SELECT         
  @POLICY_STATUS = ISNULL(STATUS_MASTER.POLICY_DESCRIPTION,''),@POLICY_NUMBER=POLICY_NUMBER         
 FROM         
  CLM_CLAIM_INFO CLAIM JOIN POL_CUSTOMER_POLICY_LIST POLICY        
 ON         
  CLAIM.CUSTOMER_ID=POLICY.CUSTOMER_ID AND        
  CLAIM.POLICY_ID=POLICY.POLICY_ID AND        
  CLAIM.POLICY_VERSION_ID=POLICY.POLICY_VERSION_ID        
 LEFT JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER         
 ON         
  STATUS_MASTER.POLICY_STATUS_CODE = POLICY.POLICY_STATUS          
 WHERE         
  CLAIM.CLAIM_ID=@CLAIM_ID        
        
--GET COMPANY DATA        
 SELECT              
 @POLICY_STATUS AS POLICY_STATUS,        
 @POLICY_NUMBER AS POLICY_NUMBER,        
  NAIC_CODE,            
  REFERENCE_NUMBER,            
  CAT_NUMBER,            
  convert(char,EFFECTIVE_DATE,101) EFFECTIVE_DATE,           
  convert(char,EXPIRATION_DATE,101) EXPIRATION_DATE,         
  convert(char,ACCIDENT_DATE_TIME,101) ACCIDENT_DATE,            
 PREVIOUSLY_REPORTED,        
 INSURED_CONTACT_ID,        
  CONTACT_NAME,            
  CONTACT_ADDRESS1,            
  CONTACT_ADDRESS2,            
  CONTACT_CITY,            
  CONTACT_STATE,            
  CONTACT_COUNTRY,            
  CONTACT_ZIP,            
  CONTACT_HOMEPHONE,            
  CONTACT_WORKPHONE,            
  IS_ACTIVE,      
  ACCIDENT_DATE_TIME AS ACCIDENT_TIME,
 LOSS_TIME_AM_PM   
 FROM            
  CLM_CLAIM_COMPANY             
 WHERE            
  CLAIM_ID=@CLAIM_ID AND             
  COMPANY_ID=@COMPANY_ID        
END            
          



GO

