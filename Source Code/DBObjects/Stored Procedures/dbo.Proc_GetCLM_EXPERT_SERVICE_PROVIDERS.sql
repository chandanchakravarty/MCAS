IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_EXPERT_SERVICE_PROVIDERS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_EXPERT_SERVICE_PROVIDERS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                            
                                            
Proc Name       : Proc_GetCLM_EXPERT_SERVICE_PROVIDERS                          
Created by      : Sumit Chhabra                            
Date            : 21/04/2006                                            
Purpose         : Get Expert Service Provider data from CLM_EXPERT_SERVICE_PROVIDERS                            
Revison History :                                            
Used In                   : Wolverine                                            
------------------------------------------------------------                                            
Date     Review By          Comments                   
drop proc dbo.Proc_GetCLM_EXPERT_SERVICE_PROVIDERS                  
------   ------------       -------------------------*/                                            
CREATE PROC [dbo].[Proc_GetCLM_EXPERT_SERVICE_PROVIDERS]                                            
(                                            
 @EXPERT_SERVICE_ID int  ,
 @LANG_ID  int =1                                              
                                      
)                                            
AS                                            
BEGIN                                          
  SELECT                            
  EXPERT1.EXPERT_SERVICE_ID,                          
  EXPERT1.EXPERT_SERVICE_TYPE,                          
  EXPERT1.EXPERT_SERVICE_NAME,                          
  EXPERT1.EXPERT_SERVICE_ADDRESS1,                          
  EXPERT1.EXPERT_SERVICE_ADDRESS2,                          
  EXPERT1.EXPERT_SERVICE_CITY,                          
  EXPERT1.EXPERT_SERVICE_STATE,                          
  EXPERT1.EXPERT_SERVICE_ZIP,                          
  EXPERT1.EXPERT_SERVICE_VENDOR_CODE,                          
  EXPERT1.EXPERT_SERVICE_CONTACT_NAME,                          
  EXPERT1.EXPERT_SERVICE_CONTACT_PHONE,                          
  EXPERT1.EXPERT_SERVICE_CONTACT_EMAIL,                          
  EXPERT1.EXPERT_SERVICE_FEDRERAL_ID,                          
  EXPERT1.EXPERT_SERVICE_1099_PROCESSING_OPTION,                          
  EXPERT1.IS_ACTIVE,                        
  EXPERT1.EXPERT_SERVICE_TYPE_DESC,                  
  EXPERT1.EXPERT_SERVICE_COUNTRY,                      
  EXPERT1.EXPERT_SERVICE_MASTER_VENDOR_CODE AS EXPERT_SERVICE_MASTER_VENDOR_CODE_ID,                    
  EXPERT2.EXPERT_SERVICE_VENDOR_CODE AS EXPERT_SERVICE_MASTER_VENDOR_CODE,                  
  EXPERT1.PARTY_DETAIL,                  
  EXPERT1.AGE,                  
  EXPERT1.EXTENT_OF_INJURY,                  
  EXPERT1.OTHER_DETAILS,                  
  EXPERT1.BANK_NAME,                  
  EXPERT1.ACCOUNT_NUMBER,                  
  EXPERT1.ACCOUNT_NAME,                  
  EXPERT1.EXPERT_SERVICE_CONTACT_PHONE_EXT,                  
  EXPERT1.EXPERT_SERVICE_CONTACT_FAX,                  
  ADJUSTER.ADJUSTER_NAME AS PARENT_ADJUSTER, --PARTY.NAME AS PARENT_ADJUSTER,                
  EXPERT1.MAIL_1099_ADD1,                  
  EXPERT1.MAIL_1099_ADD2,                  
  EXPERT1.MAIL_1099_CITY,                  
  EXPERT1.MAIL_1099_STATE,                  
  EXPERT1.MAIL_1099_COUNTRY,                  
  EXPERT1.MAIL_1099_ZIP,                
  EXPERT1.MAIL_1099_NAME,                  
  EXPERT1.W9_FORM,
  EXPERT1.REGIONAL_IDENTIFICATION,
 -- EXPERT1.DATE_OF_BIRTH,
    ISNULL(Convert(varchar,EXPERT1.DATE_OF_BIRTH,case when @LANG_ID=2 then 103 else 101 end),'') AS DATE_OF_BIRTH,  
  EXPERT1.CPF,
--  EXPERT1.REG_ID_ISSUE_DATE ,
  ISNULL(Convert(varchar,EXPERT1.REG_ID_ISSUE_DATE,case when @LANG_ID=2 then 103 else 101 end),'') AS REG_ID_ISSUE_DATE,  

  EXPERT1.ACTIVITY,
  EXPERT1.REG_ID_ISSUE,                       
  EXPERT1.REQ_SPECIAL_HANDLING                
  FROM CLM_EXPERT_SERVICE_PROVIDERS EXPERT1                       
  LEFT OUTER JOIN CLM_EXPERT_SERVICE_PROVIDERS EXPERT2 ON                      
  EXPERT1.EXPERT_SERVICE_MASTER_VENDOR_CODE = EXPERT2.EXPERT_SERVICE_ID    
 --Added by Sibin on 8 Dec 08 for Itrack Issue 5055      
  LEFT OUTER JOIN CLM_ADJUSTER ADJUSTER ON ADJUSTER.ADJUSTER_ID=EXPERT1.PARENT_ADJUSTER              
  WHERE   EXPERT1.EXPERT_SERVICE_ID=@EXPERT_SERVICE_ID                            
                             
END 
GO

