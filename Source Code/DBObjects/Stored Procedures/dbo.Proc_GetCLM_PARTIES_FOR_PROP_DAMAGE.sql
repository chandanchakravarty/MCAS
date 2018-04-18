IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_PARTIES_FOR_PROP_DAMAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_PARTIES_FOR_PROP_DAMAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                              
Proc Name       : dbo.Proc_GetCLM_PARTIES_FOR_PROP_DAMAGE                              
Created by      : Praveen kasana                                                          
Date            : 28/04/2006                                                              
Purpose         : Get data from CLM_PARTIES table                               
                                                        
Revison History :                                                              
Used In         : Wolverine                                                              
                          
Modified BY  : Amar                          
Modified On  : 12/5/2006                          
Purpose   : Added more columns                          
------------------------------------------------------------                                                              
Date     Review By          Comments                                                              
------   ------------       -------------------------                          
*/                                                              
--DROP PROC dbo.Proc_GetCLM_PARTIES_FOR_PROP_DAMAGE                              
CREATE PROC [dbo].[Proc_GetCLM_PARTIES_FOR_PROP_DAMAGE]                              
@PROP_DAMAGED_ID int,                      
@CLAIM_ID int                              
AS                                                              
BEGIN                                
 SELECT                               
  CP1.PARTY_ID,                        
  CP1.CLAIM_ID,                        
  CP1.NAME,                        
  CP1.ADDRESS1,                        
  CP1.ADDRESS2,                        
  CP1.CITY,                        
  CP1.STATE,                        
  CP1.ZIP,                        
  CP1.CONTACT_PHONE,                        
  CP1.CONTACT_EMAIL,                        
  CP1.OTHER_DETAILS,                        
  CP1.IS_ACTIVE,                        
  CP1.PARTY_TYPE_ID,                        
  ISNULL( CP1.COUNTRY,0) COUNTRY,                        
  CP1.PARTY_DETAIL,                        
  CP1.AGE,                        
  CP1.EXTENT_OF_INJURY,                    
  CP1.REFERENCE,                  
  CP1.BANK_NAME,                  
  CP1.ACCOUNT_NUMBER,                  
  CP1.ACCOUNT_NAME,              
  CP1.CONTACT_PHONE_EXT,              
  CP1.CONTACT_FAX,CP1.PARTY_TYPE_DESC,          
  CP1.PARENT_ADJUSTER AS PARENT_ADJUSTER_ID,        
  CP2.NAME AS PARENT_ADJUSTER,      
  CP1.FEDRERAL_ID,      
  CP1.PROCESSING_OPTION_1099,      
  CP1.MASTER_VENDOR_CODE,      
  CP1.VENDOR_CODE,      
  CP1.CONTACT_NAME,      
  ISNULL(CP1.EXPERT_SERVICE_TYPE,0) AS EXPERT_SERVICE_TYPE,        
  ISNULL(CP1.EXPERT_SERVICE_TYPE_DESC,'') AS EXPERT_SERVICE_TYPE_DESC,      
 ISNULL(CP1.SUB_ADJUSTER,'') AS SUB_ADJUSTER,        
 ISNULL(CP1.SA_ADDRESS1,'') AS SA_ADDRESS1,        
 ISNULL(CP1.SA_ADDRESS2,'') AS SA_ADDRESS2,        
 ISNULL(CP1.SA_CITY,'') AS SA_CITY,        
 ISNULL(CP1.SA_COUNTRY,0) AS SA_COUNTRY,        
 ISNULL(CP1.SA_STATE,0) AS SA_STATE,        
 ISNULL(CP1.SA_ZIPCODE,'') AS SA_ZIPCODE,        
 ISNULL(CP1.SA_PHONE,'') AS SA_PHONE,        
 ISNULL(CP1.SA_FAX,'') AS SA_FAX,        
 ISNULL(CP1.SUB_ADJUSTER_CONTACT_NAME,'') AS SUB_ADJUSTER_CONTACT_NAME,        
  CP1.MAIL_1099_ADD1,      
  CP1.MAIL_1099_ADD2,      
  CP1.MAIL_1099_CITY,      
  CP1.MAIL_1099_STATE,      
  CP1.MAIL_1099_COUNTRY,      
  CP1.MAIL_1099_ZIP ,  
  CP1.MAIL_1099_NAME,      
  CP1.W9_FORM  
    
 FROM                              
  CLM_PARTIES CP1          
 LEFT OUTER JOIN          
  CLM_PARTIES CP2          
 ON          
  CP1.PARENT_ADJUSTER = CP2.PROP_DAMAGED_ID AND        
  CP1.CLAIM_ID=CP2.CLAIM_ID                             
 WHERE                              
  CP1.PROP_DAMAGED_ID = @PROP_DAMAGED_ID AND                      
  CP1.CLAIM_ID = @CLAIM_ID                         
END                
              
            
          
        
      
      
      
      
      
      
      
      
      
      
      
      
      
      
    
  
  
GO

