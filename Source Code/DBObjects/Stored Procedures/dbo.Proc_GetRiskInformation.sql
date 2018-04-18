IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRiskInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRiskInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                          
Proc Name             : Dbo.Proc_GetRiskInformation                                          
Created by            : Santosh Kumar Gautam                                         
Date                  : 11/11/2010                                         
Purpose               : To get risk information details
Revison History       :                                          
Used In               : claim module
------------------------------------------------------------                                          
Date     Review By          Comments             
    
drop Proc Proc_GetRiskInformation                                 
------   ------------       -------------------------*/  

CREATE PROC [dbo].[Proc_GetRiskInformation]  

@CLAIM_ID			int  

AS                                                                            
BEGIN     
  
SELECT 
		[INSURED_PRODUCT_ID]
		,[CLAIM_ID]
		,[POL_RISK_ID]     
		,VEHICLE_INSURED_PLEADED_GUILTY
		,VEHICLE_MAKER
		,VEHICLE_MODEL
		,VEHICLE_VIN
		,DAMAGE_DESCRIPTION
		,VESSEL_TYPE
		,VESSEL_NAME
		,VESSEL_MANUFACTURER
		,LOCATION_ADDRESS
		,LOCATION_COMPLIMENT
		,LOCATION_DISTRICT
		,LOCATION_ZIPCODE
		,VOYAGE_CONVEYENCE_TYPE
		,VOYAGE_DEPARTURE_DATE
		,INSURED_NAME
		,EFFECTIVE_DATE
		,EXPIRE_DATE
		,IS_ACTIVE
		,CREATED_BY
		,CREATED_DATETIME
		,MODIFIED_BY
		,LAST_UPDATED_DATETIME
		,VESSEL_NUMBER
		,PERSON_DISEASE_DATE
		,VOYAGE_PREFIX
		,VOYAGE_ARRIVAL_DATE
		,VOYAGE_SURVEY_DATE
		,DAMAGE_TYPE
		,PERSON_DOB
		,VOYAGE_CERT_NUMBER
		,VOYAGE_TRAN_COMPANY
		,VOYAGE_IO_DESC
		,ITEM_NUMBER
		,RURAL_INSURED_AREA
		,RURAL_PROPERTY
		,RURAL_CULTIVATION
		,RURAL_FESR_COVERAGE
		,RURAL_MODE
		,RURAL_SUBSIDY_PREMIUM
		,PA_NUM_OF_PASS
		,DP_TICKET_NUMBER
		,DP_CATEGORY
		,STATE1
		,COUNTRY1
		,COUNTRY2
		,STATE2
		,CITY1
		,CITY2
		,LICENCE_PLATE_NUMBER
		,[YEAR]
		,[ACTUAL_INSURED_OBJECT]	      
  FROM [dbo].[CLM_INSURED_PRODUCT]
  WHERE (CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y')
  
END  




GO

