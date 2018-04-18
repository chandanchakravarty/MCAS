IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_DISTINCT_POL_LOCATIONS_FOR_UMBRELLA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_DISTINCT_POL_LOCATIONS_FOR_UMBRELLA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                
Proc Name   : dbo.PROC_GET_DISTINCT_POL_LOCATIONS_FOR_UMBRELLA                      
Created by  : Sumit Chhabra            
Date        : 09 Oc,2006                      
Purpose     : Fetch locations for umbrella sch.             
Revison History  :                                      
 ------------------------------------------------------------                                            
Date     Review By          Comments                                          
                                 
------   ------------       -------------------------*/           
          
-- DROP PROC  dbo.PROC_GET_DISTINCT_POL_LOCATIONS_FOR_UMBRELLA                          
CREATE PROC dbo.PROC_GET_DISTINCT_POL_LOCATIONS_FOR_UMBRELLA                          
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID smallint    
AS                          
BEGIN                          
                        
                            
  SELECT                           
 LOC_ADD1,LOC_ADD2,LOC_CITY,LOC_COUNTY,LOC_STATE,LOC_ZIP,PHONE_NUMBER,FAX_NUMBER,LOC_NUM,STATE_NAME,LOCATION_ID                     
  INTO #TEMP_LOCATIONS                          
  FROM APP_LOCATIONS                          
  INNER JOIN APP_LIST ON APP_LOCATIONS.CUSTOMER_ID = APP_LIST.CUSTOMER_ID                          
  AND APP_LOCATIONS.APP_ID = APP_LIST.APP_ID                           
  AND APP_LOCATIONS.APP_VERSION_ID = APP_LIST.APP_VERSION_ID                           
  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES ON APP_LIST.CUSTOMER_ID = POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID                          
  AND APP_LIST.APP_NUMBER =                         
  LTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                      
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                            
  AND APP_LIST.APP_VERSION_ID=                        
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                      
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                        
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                      
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                      
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                      
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))       
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST S ON      
 APP_LOCATIONS.LOC_STATE = S.STATE_ID      
      
  WHERE POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID                       
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_ID = @POLICY_ID                           
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_VERSION_ID = @POLICY_VERSION_ID                           
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,0)=0                    
  AND APP_LOCATIONS.IS_ACTIVE='Y'                        
                      
  UNION                          
                            
  SELECT                           
   LOC_ADD1,LOC_ADD2,LOC_CITY,LOC_COUNTY,LOC_STATE,LOC_ZIP,PHONE_NUMBER,FAX_NUMBER,LOC_NUM,STATE_NAME,LOCATION_ID                    
  FROM POL_LOCATIONS                          
  INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_LOCATIONS.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                          
  AND POL_LOCATIONS.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID                           
  AND POL_LOCATIONS.POLICY_VERSION_ID = POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID                           
  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                       
  = POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID           
  AND POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER =                       
  LTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                      
 CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                            
  AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=                      
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                      
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                      
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                      
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                      
  CHARINDEX('-',POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@POLICY_VERSION_ID,                      
  LEN(POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))         
  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST S ON      
  POL_LOCATIONS.LOC_STATE = S.STATE_ID                         
  WHERE POL_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID AND                       
  POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_ID = @POLICY_ID                           
  AND POL_UMBRELLA_UNDERLYING_POLICIES.POLICY_VERSION_ID = @POLICY_VERSION_ID                           
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,@POLICY_VERSION_ID)=@POLICY_VERSION_ID                  
  AND POL_LOCATIONS.IS_ACTIVE='Y'                          
                  
SELECT        
 MAX(LOC_ADD1) as ADDRESS_1,          
 MAX(LOC_ADD2) as ADDRESS_2,          
 MAX(LOC_CITY) as CITY,        
 MAX(LOC_COUNTY) as LOC_COUNTY,        
 MAX(LOC_STATE) as STATE,         
 MAX(STATE_NAME) as STATE_NAME,         
 MAX(LOC_ZIP) as ZIPCODE,        
 MAX(PHONE_NUMBER) as PHONE_NUMBER,        
 MAX(FAX_NUMBER) as FAX_NUMBER,        
 MAX(LOC_NUM) as LOCATION_NUMBER,
 MAX(LOCATION_ID) as LOCATION_ID,        
 ISNULL(LOC_ADD1,'') + ' ' + ISNULL(LOC_ADD2,'') as ADDRESS        
 FROM #TEMP_LOCATIONS                          
 GROUP BY LOC_ADD1, LOC_ADD2                                                 
                  
END          
    
  



GO

