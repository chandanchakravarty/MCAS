IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetForm1099]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetForm1099]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : Proc_GetForm1099              
Created by      : Praveen             
Date            : 4 March 2008        
Purpose     : To Get 1099 Info           
Revison History :              
Used In  : Wolverine              
      
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
        
-- drop proc dbo.Proc_GetForm1099              
CREATE PROC [dbo].[Proc_GetForm1099]       
(              
 @FORM_1099_ID int              
)              
AS              
BEGIN              
SELECT     
 FORM_1099_ID,    
 ENTITY_ID,    
 ENTITY_TYPE,    
 RENTS,    
 ROYALATIES,    
 OTHERINCOME,    
 FEDERAL_INCOME_TAXWITHHELD,    
 FISHING_BOAT_PROCEEDS,    
 MEDICAL_AND_HEALTH_CARE_PRODUCTS,    
 NON_EMPLOYEMENT_COMPENSATION,    
 SUBSTITUTE_PAYMENTS,    
 PAYER_MADE_DIRECT_SALES,    
 CROP_INSURANCE_PROCEED,    
 EXCESS_GOLDEN_PARACHUTE_PAYMENTS,    
 GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY,  
 STATE_TAX_WITHHELD,    
 STATE_PAYER_STATE_NO,    
 STATE_INCOME,    
 RECIPIENT_IDENTIFICATION,    
 RECIPIENT_NAME,    
 RECIPIENT_STREET_ADDRESS1,    
 RECIPIENT_STREET_ADDRESS2,    
 RECIPIENT_CITY,    
 RECIPIENT_STATE,    
 RECIPIENT_ZIP,    
 ACCOUNT_NO,    
 IS_ACTIVE,    
 CREATED_BY,    
 CREATED_DATETIME,    
 MODIFIED_BY,    
 LAST_UPDATED_DATETIME, 
 FED_SSN_1099 , 
 ISNULL(IS_COMMITED,'N')  AS IS_COMMITED  
FROM FORM_1099 WITH(NOLOCK)     
WHERE FORM_1099_ID = @FORM_1099_ID    
     
          
END              
    
    
              
    
    
    
    
    
    
    
  
  
  
  
  
  
  
GO

