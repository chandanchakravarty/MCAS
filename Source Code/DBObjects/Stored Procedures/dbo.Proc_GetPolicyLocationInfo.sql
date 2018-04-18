IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyLocationInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyLocationInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop PROC dbo.Proc_GetPolicyLocationInfo 27987 ,79,1,46       
CREATE PROC [dbo].[Proc_GetPolicyLocationInfo]              
(              
 @CUSTOMER_ID int,              
 @POL_ID      int,              
 @POL_VERSION_ID smallint,              
 @LOCATION_ID    smallint              
)              
AS              
BEGIN              
 SELECT CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID,              
  LOC_NUM, IS_PRIMARY, LOC_ADD1, LOC_ADD2, LOC_CITY,             
  LOC_COUNTY, LOC_STATE, LOC_ZIP, ISNULL(LOC_COUNTRY,'') LOC_COUNTRY, PHONE_NUMBER,              
  FAX_NUMBER,           
--DEDUCTIBLE, NAMED_PERILL,              
  IS_ACTIVE, CREATED_BY, CREATED_DATETIME, [DESCRIPTION] ,LOCATION_TYPE,IS_BILLING      
          
,RENTED_WEEKLY      
,WEEKS_RENTED, LOSSREPORT_ORDER, CONVERT(VARCHAR,LOSSREPORT_DATETIME, 101) AS LOSSREPORT_DATETIME ,    
REPORT_STATUS,CAL_NUM,NAME,NUMBER,DISTRICT,OCCUPIED,EXT,CATEGORY,ACTIVITY_TYPE,CONSTRUCTION    
      
 FROM POL_LOCATIONS              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND              
  POLICY_ID = @POL_ID AND              
  POLICY_VERSION_ID = @POL_VERSION_ID AND              
  LOCATION_ID = @LOCATION_ID              
              
END              
            
            
          
        
      
      
      
    
    
GO

