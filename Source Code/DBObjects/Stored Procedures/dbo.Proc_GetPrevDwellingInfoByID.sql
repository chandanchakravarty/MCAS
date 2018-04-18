IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPrevDwellingInfoByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPrevDwellingInfoByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








/*----------------------------------------------------------        
Proc Name   : dbo.Proc_GetDwellingInfoByID       
Created by  :Pradeep        
Date        :17 May,2005      
Purpose     :         
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/  

CREATE    PROCEDURE Proc_GetPrevDwellingInfoByID
(
	
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID SmallInt,
	@DWELLING_ID SmallInt
)

As

SELECT ISNULL(MAX(DWELLING_ID),0)
FROM APP_DWELLINGS_INFO
WHERE CUSTOMER_ID = CUSTOMER_ID AND
	APP_ID = @APP_ID AND 
	APP_VERSION_ID = @APP_VERSION_ID AND		
	DWELLING_ID < @DWELLING_ID	 		

/*
SELECT 
	CUSTOMER_ID,
	APP_ID,
	APP_VERSION_ID,
	DWELLING_ID,
	DWELLING_NUMBER,
	LOCATION_ID,
	SUB_LOC_ID,
	YEAR_BUILT,
	PURCHASE_YEAR,
	PURCHASE_PRICE,
	MARKET_VALUE,
	REPLACEMENT_COST,
	BUILDING_TYPE,
	OCCUPANCY,
	NEED_OF_UNITS,
	USAGE,
	NEIGHBOURS_VISIBLE,
	IS_VACENT_OCCUPY,
	IS_RENTED_IN_PART,
	OCCUPIED_DAILY,
	NO_WEEKS_RENTED,
	IS_DWELLING_OWNED_BY_OTHER
	IS_ACTIVE,
	CREATED_BY,
	CREATED_DATETIME,
	MODIFIED_BY,
	LAST_UPDATED_DATETIME,
	IS_DWELLING_OWNED_BY_OTHER

FROM APP_DWELLINGS_INFO
WHERE CUSTOMER_ID = CUSTOMER_ID AND
      APP_ID = @APP_ID AND 
      APP_VERSION_ID = @APP_VERSION_ID AND
      DWELLING_ID = 
	(
		 
	)
 	 
*/







GO

