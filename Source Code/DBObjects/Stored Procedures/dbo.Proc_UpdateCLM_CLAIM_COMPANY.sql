IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_CLAIM_COMPANY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_CLAIM_COMPANY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                
Proc Name       : dbo.Proc_UpdateCLM_CLAIM_COMPANY                                          
Created by      : Sumit Chhabra                                              
Date            : 19/05/2006                                                
Purpose         : Update data at CLM_CLAIM_COMPANY table for watercraft company page      
Created by      : Sumit Chhabra                                               
Revison History :                                                
Used In        : Wolverine                                                
------------------------------------------------------------                                                
Date     Review By          Comments                                                
------   ------------       -------------------------*/                                                
CREATE PROC dbo.Proc_UpdateCLM_CLAIM_COMPANY         
(                                         
@CLAIM_ID int,      
@COMPANY_ID int,      
@AGENCY_ID int,      
@NAIC_CODE varchar(15),      
@REFERENCE_NUMBER varchar(20),      
@CAT_NUMBER varchar(20),      
@EFFECTIVE_DATE datetime,      
@EXPIRATION_DATE datetime,      
@CONTACT_NAME nvarchar(75),      
@CONTACT_ADDRESS1 nvarchar(75),      
@CONTACT_ADDRESS2 nvarchar(75),      
@CONTACT_CITY nvarchar(20),      
@CONTACT_STATE int,      
@CONTACT_COUNTRY int,      
@CONTACT_ZIP varchar(11),      
@CONTACT_HOMEPHONE nvarchar(20),      
@CONTACT_WORKPHONE nvarchar(20),      
@MODIFIED_BY int,      
@LAST_UPDATED_DATETIME datetime,    
@PREVIOUSLY_REPORTED char(1),    
@INSURED_CONTACT_ID int,  
@ACCIDENT_DATE_TIME datetime,
@LOSS_TIME_AM_PM smallint
)                       
AS                                                
BEGIN                                                
 UPDATE       
  CLM_CLAIM_COMPANY       
 SET      
  NAIC_CODE=@NAIC_CODE,      
  REFERENCE_NUMBER=@REFERENCE_NUMBER,      
  CAT_NUMBER=@CAT_NUMBER,      
  EFFECTIVE_DATE=@EFFECTIVE_DATE,      
  EXPIRATION_DATE=@EXPIRATION_DATE,      
  CONTACT_NAME=@CONTACT_NAME,      
  CONTACT_ADDRESS1=@CONTACT_ADDRESS1,      
  CONTACT_ADDRESS2=@CONTACT_ADDRESS2,      
  CONTACT_CITY=@CONTACT_CITY,      
  CONTACT_STATE=@CONTACT_STATE,      
  CONTACT_COUNTRY=@CONTACT_COUNTRY,      
  CONTACT_ZIP=@CONTACT_ZIP,      
  CONTACT_HOMEPHONE=@CONTACT_HOMEPHONE,      
  CONTACT_WORKPHONE=@CONTACT_WORKPHONE,      
  MODIFIED_BY=@MODIFIED_BY,      
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,    
 PREVIOUSLY_REPORTED=@PREVIOUSLY_REPORTED,    
 INSURED_CONTACT_ID=@INSURED_CONTACT_ID,  
 ACCIDENT_DATE_TIME=@ACCIDENT_DATE_TIME,
 LOSS_TIME_AM_PM = @LOSS_TIME_AM_PM
 WHERE      
  CLAIM_ID=@CLAIM_ID AND       
  COMPANY_ID=@COMPANY_ID      
END                
              
            
          
        
    
  



GO

