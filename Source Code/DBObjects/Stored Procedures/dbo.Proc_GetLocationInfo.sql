IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : Proc_GetLocationInfo          
Created by      : Vijay Joshi          
Date            : 5/11/2005          
Purpose       :Evaluation          
Modified by      : Sumit Chhabra      
Date            : 11/10/2005          
Purpose       :Removed the field named_peril and deductible from being fetched      
Revison History :          
Used In        : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--drop proc dbo.Proc_GetLocationInfo  
CREATE PROC dbo.Proc_GetLocationInfo          
(          
 @CUSTOMER_ID int,          
 @APP_ID      int,          
 @APP_VERSION_ID smallint,          
 @LOCATION_ID    smallint          
)          
AS          
BEGIN          
 SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID, LOCATION_ID,          
  LOC_NUM, IS_PRIMARY, LOC_ADD1, LOC_ADD2, LOC_CITY,          
  LOC_COUNTY, LOC_STATE, LOC_ZIP, LOC_COUNTRY, PHONE_NUMBER,          
  FAX_NUMBER,     
--DEDUCTIBLE,  -- NAMED_PERILL,          
  LOCATION_TYPE,  
  IS_ACTIVE, CREATED_BY, CREATED_DATETIME, [DESCRIPTION]      
,RENTED_WEEKLY, WEEKS_RENTED, LOSSREPORT_ORDER, CONVERT(VARCHAR,LOSSREPORT_DATETIME, 101) AS LOSSREPORT_DATETIME,
REPORT_STATUS
 FROM APP_LOCATIONS          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  APP_ID = @APP_ID AND          
  APP_VERSION_ID = @APP_VERSION_ID AND          
  LOCATION_ID = @LOCATION_ID          
          
END          
      
    
  
  
  
  
  




GO

