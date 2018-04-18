IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_POL_LOCATIONS_FOR_UMBRELLA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_POL_LOCATIONS_FOR_UMBRELLA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                  
Proc Name   : dbo.PROC_GET_APP_LOCATIONS_FOR_UMBRELLA                        
Created by  : Swarup Pal            
Date        : 10 May,2006                        
Purpose     : Fetch locations for umbrella sch.               
Revison History  :                                        
 ------------------------------------------------------------                                              
Date     Review By          Comments                                            
                                   
------   ------------       -------------------------*/             
            
-- DROP PROC  dbo.PROC_GET_POL_LOCATIONS_FOR_UMBRELLA  1199,15,1                          
CREATE PROC dbo.PROC_GET_POL_LOCATIONS_FOR_UMBRELLA                            
 @CUSTOMER_ID VARCHAR(100),                            
 @POLICY_ID VARCHAR(100) ,                            
 @POLICY_VERSION_ID VARCHAR(100)                            
AS                            
BEGIN                            
 SELECT LOCATION_ID,(IsNull(ADDRESS_1,'') + ' ' + IsNull(ADDRESS_2,'') + ' ' + IsNull(CITY,'') + ' ' + IsNull(COUNTY,'') + ' ' + IsNull(MNT.STATE_NAME,'') + ' ' + IsNull(ZIPCODE,'')) AS ADDRESS,PHONE_NUMBER,  
 FAX_NUMBER,REMARKS,LOCATION_NUMBER ,OCCUPIED_BY,NUM_FAMILIES,BUSS_FARM_PURSUITS,BUSS_FARM_PURSUITS_DESC,LOC_EXCLUDED,PERS_INJ_COV_82,OTHER_POLICY  
 FROM POL_UMBRELLA_REAL_ESTATE_LOCATION  APP  
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT  
ON APP.STATE = MNT.STATE_ID  
WHERE CUSTOMER_ID =@CUSTOMER_ID  
AND  POLICY_ID = @POLICY_ID   
AND POLICY_VERSION_ID = @POLICY_VERSION_ID                     
          
END            
      



GO

