IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_FORM_1099]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_FORM_1099]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc  [Proc_Insert_FORM_1099]  
CREATE PROC [dbo].[Proc_Insert_FORM_1099]     
(        
  @FORM_1099_ID     INT OUTPUT,    
  @ENTITY_ID     INT = NULL ,    
  @ENTITY_TYPE      NVARCHAR(50) = null ,    
  @PAYORS_CARRIER_CODE     Varchar(10)    = null ,      
  @RENTS     decimal(18,2)   = null ,                                            
  @ROYALATIES     decimal(18,2)   = null ,    
  @OTHERINCOME     decimal(18,2)   = null ,    
  @FEDERAL_INCOME_TAXWITHHELD   decimal(18,2)   = null ,    
  @FISHING_BOAT_PROCEEDS   decimal(18,2)   = null ,    
  @MEDICAL_AND_HEALTH_CARE_PRODUCTS decimal(18,2)   = null ,     
  @NON_EMPLOYEMENT_COMPENSATION   decimal(18,2)   = null ,    
  @SUBSTITUTE_PAYMENTS    decimal(18,2)   = null ,    
  @PAYER_MADE_DIRECT_SALES   decimal(18,2)   = null ,    
  @CROP_INSURANCE_PROCEED   decimal(18,2)   = null ,     
  @EXCESS_GOLDEN_PARACHUTE_PAYMENTS  decimal(18,2)   = null ,    
  @GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY  decimal(18,2)   = null ,    
  @STATE_TAX_WITHHELD    decimal(18,2)   = null ,    
  @STATE_PAYER_STATE_NO    decimal(18,2)   = null ,     
  @STATE_INCOME     decimal(18,2)   = null ,    
  @RECIPIENT_IDENTIFICATION   Varchar(100)  = null,                                                                                     
  @RECIPIENT_NAME     nVarchar(70)    = null ,      
  @RECIPIENT_STREET_ADDRESS1      nVarchar(200)  = null ,       
  @RECIPIENT_STREET_ADDRESS2      nVarchar(200)  = null ,                  
  @RECIPIENT_CITY    NVARCHAR(80)  = null ,              
  @RECIPIENT_STATE    NVARCHAR(10)  = null ,                                                                              
  @RECIPIENT_ZIP    NVARCHAR(11)  = null ,       
  @ACCOUNT_NO     NVARCHAR(11)  = null ,    
  @CREATED_BY     int  = null ,    
  @CREATED_DATETIME    datetime = null ,    
  @MODIFIED_BY     int = null ,    
  @LAST_UPDATED_DATETIME   datetime = null ,    
  @INSERTUPDATE     varchar(10),  --Called from proc and Screen 22   
  @FED_SSN_1099     varchar(1) = null ,  
  @RECIPIENT_NAME_1   nVarchar(70)    = null ,
  @YEAR_1099 int = null	  
   
)      
AS      
BEGIN      
    
--Get Payors Id from Carrier Code    
DECLARE @PAYORS_ID INT    
SELECT @PAYORS_ID = AGENCY_ID FROM MNT_AGENCY_LIST WHERE AGENCY_CODE = @PAYORS_CARRIER_CODE    
    
    
    
      
IF @INSERTUPDATE = 'I'      
BEGIN      
 --Inserting the record      
      
 --retreiving the maximum id , which we will save in database      
 SELECT @FORM_1099_ID = IsNull(Max(FORM_1099_ID),0) + 1      
 FROM FORM_1099      
      
 INSERT INTO FORM_1099      
 (      
 FORM_1099_ID,    
 ENTITY_ID,    
 ENTITY_TYPE,    
 PAYORS_ID,    
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
 ACCOUNT_NO ,    
 CREATED_BY ,    
 CREATED_DATETIME ,    
 MODIFIED_BY ,    
 LAST_UPDATED_DATETIME ,  
 FED_SSN_1099,  
 RECIPIENT_NAME_1,
 YEAR_1099   
 )      
 VALUES      
 (      
 @FORM_1099_ID,    
 @ENTITY_ID,    
 @ENTITY_TYPE,    
 @PAYORS_ID,    
 @RENTS,    
 @ROYALATIES,    
 @OTHERINCOME,    
 @FEDERAL_INCOME_TAXWITHHELD,    
 @FISHING_BOAT_PROCEEDS,    
 @MEDICAL_AND_HEALTH_CARE_PRODUCTS,    
 @NON_EMPLOYEMENT_COMPENSATION,    
 @SUBSTITUTE_PAYMENTS,    
 @PAYER_MADE_DIRECT_SALES,    
 @CROP_INSURANCE_PROCEED,    
 @EXCESS_GOLDEN_PARACHUTE_PAYMENTS,    
 @GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY,    
 @STATE_TAX_WITHHELD, 
 @STATE_PAYER_STATE_NO,    
 @STATE_INCOME,    
 @RECIPIENT_IDENTIFICATION,    
 @RECIPIENT_NAME,    
 @RECIPIENT_STREET_ADDRESS1,    
 @RECIPIENT_STREET_ADDRESS2,    
 @RECIPIENT_CITY,    
 @RECIPIENT_STATE,    
 @RECIPIENT_ZIP,    
 @ACCOUNT_NO,    
 @CREATED_BY ,    
 @CREATED_DATETIME ,    
 @MODIFIED_BY ,    
 @LAST_UPDATED_DATETIME ,   
 @FED_SSN_1099   ,  
 @RECIPIENT_NAME_1,
 @YEAR_1099 
 )      
END      
ELSE      
BEGIN      
 --Updating the record      
 UPDATE FORM_1099      
  SET       
 --ENTITY_TYPE     = @ENTITY_TYPE,    
 PAYORS_ID    = @PAYORS_ID,    
 RENTS     = @RENTS,    
 ROYALATIES    = @ROYALATIES,    
 OTHERINCOME    = @OTHERINCOME,    
 FEDERAL_INCOME_TAXWITHHELD  = @FEDERAL_INCOME_TAXWITHHELD,    
 FISHING_BOAT_PROCEEDS   = @FISHING_BOAT_PROCEEDS,    
 MEDICAL_AND_HEALTH_CARE_PRODUCTS = @MEDICAL_AND_HEALTH_CARE_PRODUCTS,    
 NON_EMPLOYEMENT_COMPENSATION  = @NON_EMPLOYEMENT_COMPENSATION,    
 SUBSTITUTE_PAYMENTS   = @SUBSTITUTE_PAYMENTS,    
 PAYER_MADE_DIRECT_SALES   = @PAYER_MADE_DIRECT_SALES,    
 CROP_INSURANCE_PROCEED   = @CROP_INSURANCE_PROCEED,    
 EXCESS_GOLDEN_PARACHUTE_PAYMENTS = @EXCESS_GOLDEN_PARACHUTE_PAYMENTS,    
 GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY = @GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY,    
 STATE_TAX_WITHHELD   = @STATE_TAX_WITHHELD,    
 STATE_PAYER_STATE_NO   = @STATE_PAYER_STATE_NO,    
 STATE_INCOME    = @STATE_INCOME,    
 RECIPIENT_IDENTIFICATION  = @RECIPIENT_IDENTIFICATION,    
 RECIPIENT_NAME    = @RECIPIENT_NAME,    
 RECIPIENT_STREET_ADDRESS1  = @RECIPIENT_STREET_ADDRESS1,    
 RECIPIENT_STREET_ADDRESS2  = @RECIPIENT_STREET_ADDRESS2,    
 RECIPIENT_CITY    = @RECIPIENT_CITY,    
 RECIPIENT_STATE    = @RECIPIENT_STATE,    
 RECIPIENT_ZIP    = @RECIPIENT_ZIP,    
 ACCOUNT_NO     = @ACCOUNT_NO,    
 MODIFIED_BY     =  @MODIFIED_BY,    
 LAST_UPDATED_DATETIME    = @LAST_UPDATED_DATETIME  ,  
 FED_SSN_1099             = @FED_SSN_1099,  
 RECIPIENT_NAME_1  = @RECIPIENT_NAME_1   
     
 WHERE      
   FORM_1099_ID = @FORM_1099_ID      
      
END      
       
END    
  
    




GO

