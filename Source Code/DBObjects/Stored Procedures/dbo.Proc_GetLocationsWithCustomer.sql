IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationsWithCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationsWithCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP PROC  dbo.Proc_GetLocationsWithCustomer
--go
/*              
----------------------------------------------------------                  
Proc Name       : dbo.Proc_GetLocationsWithCustomer              
Created by      : PraVESH CHANDEL                
Date            : 08 JAN 2007                  
Purpose         : Selects ALL the location for an application              
Revison History :                  
Used In         : Wolverine         
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------          
Proc_GetLocationsWithCustomer 1250,39,1,'WAT'     
DROP PROC  dbo.Proc_GetLocationsWithCustomer    
*/              
              
CREATE     PROCEDURE dbo.Proc_GetLocationsWithCustomer              
(              
 @CUSTOMER_ID Int,              
 @APP_ID Int,              
 @APP_VERSION_ID smallint,  
 @CALLED_FROM varchar(20)=null      
)              
              
As              
BEGIN    
IF ISNULL(@CALLED_FROM,'') = ''            
BEGIN  
SELECT AL.LOCATION_ID,    
      isnull(AL.LOCATION_TYPE,'0') as LOCATION_TYPE,          
       --CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +                
       RTRIM(ISNULL(AL.LOC_ADD1,'')) LOC_ADD1 ,    
 ISNULL(AL.LOC_ADD2,'') LOC_ADD2,    
       ISNULL(AL.LOC_CITY,'')CITY_NAME ,    
       ISNULL(SL.STATE_NAME,'') STATE_NAME,    
 ISNULL(SL.STATE_CODE,'') STATE_CODE,    
       ISNULL(AL.LOC_ZIP,'')LOC_ZIP,    
 FIRST_NAME,    
 ISNULL(MIDDLE_NAME,'') AS MIDDLE_NAME,    
 ISNULL(LAST_NAME,'') as LAST_NAME,    
 CONVERT(VARCHAR,ISNULL(CO_APPL_DOB,0),101)DATE_OF_BIRTH,    
 ISNULL(CO_APPL_SSN_NO,'')SSN_NO    
            
FROM APP_LOCATIONS AL
INNER JOIN APP_APPLICANT_LIST AAL ON AL.CUSTOMER_ID=AAL.CUSTOMER_ID AND AL.APP_ID = AAL.APP_ID AND  AL.APP_VERSION_ID = AAL.APP_VERSION_ID                   
INNER JOIN CLT_APPLICANT_LIST CAL ON AAL.CUSTOMER_ID=CAL.CUSTOMER_ID AND AAL.APPLICANT_ID = CAL.APPLICANT_ID   
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON              
 AL.LOC_COUNTRY = SL.COUNTRY_ID AND              
 AL.LOC_STATE = SL.STATE_ID              
    
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND              
      AL.APP_ID = @APP_ID AND              
      AL.APP_VERSION_ID = @APP_VERSION_ID AND              
      AL.IS_ACTIVE = 'Y' AND
	  ISNULL(AAL.IS_PRIMARY_APPLICANT,0) = 1       
/*   AND AL.LOCATION_ID NOT IN (SELECT DISTINCT LOCATION_ID         
                              FROM APP_DWELLINGS_INFO         
                              WHERE CUSTOMER_ID=@CUSTOMER_ID AND         
                                    APP_ID=@APP_ID AND                                  
        APP_VERSION_ID=@APP_VERSION_ID)        
*/    
END  

ELSE IF ISNULL(@CALLED_FROM,'') = 'WAT'            
BEGIN
SELECT AL.BOAT_ID AS LOCATION_ID,    
      '0' as LOCATION_TYPE,          
       --CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +                
       RTRIM(ISNULL(AL.LOCATION_ADDRESS,'')) LOC_ADD1 ,    
 	'' LOC_ADD2,    
       ISNULL(AL.LOCATION_CITY,'')CITY_NAME ,    
 ISNULL(SL.STATE_CODE,'') STATE_CODE,    
 ISNULL(SL.STATE_NAME,'') STATE_NAME,    
       ISNULL(AL.LOCATION_ZIP,'')LOC_ZIP,    
 FIRST_NAME,    
 ISNULL(MIDDLE_NAME,'') AS MIDDLE_NAME,    
 ISNULL(LAST_NAME,'') as LAST_NAME,    
 CONVERT(VARCHAR,ISNULL(CO_APPL_DOB,0),101)DATE_OF_BIRTH,    
 ISNULL(CO_APPL_SSN_NO,'')SSN_NO    
            
FROM APP_WATERCRAFT_INFO AL              
INNER JOIN APP_APPLICANT_LIST AAL ON AL.CUSTOMER_ID=AAL.CUSTOMER_ID AND AL.APP_ID = AAL.APP_ID AND  AL.APP_VERSION_ID = AAL.APP_VERSION_ID                   
INNER JOIN CLT_APPLICANT_LIST CAL ON AAL.CUSTOMER_ID=CAL.CUSTOMER_ID AND AAL.APPLICANT_ID = CAL.APPLICANT_ID   
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON              
 AL.LOCATION_STATE = CONVERT(VARCHAR,SL.STATE_ID)              
    
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND              
      AL.APP_ID = @APP_ID AND              
      AL.APP_VERSION_ID = @APP_VERSION_ID AND              
      AL.IS_ACTIVE = 'Y' AND
	  ISNULL(AAL.IS_PRIMARY_APPLICANT,0) = 1   
END
  
ELSE IF ISNULL(@CALLED_FROM,'') = 'POLICY'            
BEGIN  
SELECT PL.LOCATION_ID,    
      isnull(PL.LOCATION_TYPE,'0') as LOCATION_TYPE,          
       --CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +              
       RTRIM(ISNULL(PL.LOC_ADD1,'')) LOC_ADD1 ,    
 ISNULL(PL.LOC_ADD2,'') LOC_ADD2,    
       ISNULL(PL.LOC_CITY,'')CITY_NAME ,    
       ISNULL(SL.STATE_NAME,'') STATE_NAME,    
 ISNULL(SL.STATE_CODE,'') STATE_CODE,    
       ISNULL(PL.LOC_ZIP,'')LOC_ZIP,    
 FIRST_NAME,    
 ISNULL(MIDDLE_NAME,'') AS MIDDLE_NAME,    
 ISNULL(LAST_NAME,'') as LAST_NAME,    
 CONVERT(VARCHAR,ISNULL(CO_APPL_DOB,0),101)DATE_OF_BIRTH,    
 ISNULL(CO_APPL_SSN_NO,'')SSN_NO    
            
FROM POL_LOCATIONS PL              
INNER JOIN POL_APPLICANT_LIST PAL ON PL.CUSTOMER_ID=PAL.CUSTOMER_ID AND PL.POLICY_ID = PAL.POLICY_ID AND  PL.POLICY_VERSION_ID = PAL.POLICY_VERSION_ID
INNER JOIN CLT_APPLICANT_LIST CAL ON PAL.CUSTOMER_ID=CAL.CUSTOMER_ID AND PAL.APPLICANT_ID = CAL.APPLICANT_ID    
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON              
 PL.LOC_COUNTRY = SL.COUNTRY_ID AND              
 PL.LOC_STATE = SL.STATE_ID              
    
WHERE PL.CUSTOMER_ID = @CUSTOMER_ID AND              
      PL.POLICY_ID = @APP_ID AND              
      PL.POLICY_VERSION_ID = @APP_VERSION_ID AND              
      PL.IS_ACTIVE = 'Y' AND
	  ISNULL(PAL.IS_PRIMARY_APPLICANT,0) = 1       
/*   AND AL.LOCATION_ID NOT IN (SELECT DISTINCT LOCATION_ID         
                              FROM APP_DWELLINGS_INFO         
                              WHERE CUSTOMER_ID=@CUSTOMER_ID AND         
                                    APP_ID=@APP_ID AND                                  
        APP_VERSION_ID=@APP_VERSION_ID)        
*/    
END  
  
ELSE IF ISNULL(@CALLED_FROM,'') = 'POLICY-WAT'            
BEGIN
SELECT PL.BOAT_ID AS LOCATION_ID,    
      '0' as LOCATION_TYPE,          
       --CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +                
       RTRIM(ISNULL(PL.LOCATION_ADDRESS,'')) LOC_ADD1 ,    
       '' LOC_ADD2,    
       ISNULL(PL.LOCATION_CITY,'')CITY_NAME ,    
       ISNULL(SL.STATE_NAME,'') STATE_NAME,    
 ISNULL(SL.STATE_CODE,'') STATE_CODE,    
       ISNULL(PL.LOCATION_ZIP,'')LOC_ZIP,    
 FIRST_NAME,    
 ISNULL(MIDDLE_NAME,'') AS MIDDLE_NAME,    
 ISNULL(LAST_NAME,'') as LAST_NAME,    
 CONVERT(VARCHAR,ISNULL(CO_APPL_DOB,0),101)DATE_OF_BIRTH,    
 ISNULL(CO_APPL_SSN_NO,'')SSN_NO    
            
FROM POL_WATERCRAFT_INFO PL              
INNER JOIN POL_APPLICANT_LIST PAL ON PL.CUSTOMER_ID=PAL.CUSTOMER_ID AND PL.POLICY_ID = PAL.POLICY_ID AND  PL.POLICY_VERSION_ID = PAL.POLICY_VERSION_ID                  
INNER JOIN CLT_APPLICANT_LIST CAL ON PAL.CUSTOMER_ID=CAL.CUSTOMER_ID AND PAL.APPLICANT_ID = CAL.APPLICANT_ID 
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON              
 PL.LOCATION_STATE = convert(varchar,SL.STATE_ID)              
    
WHERE PL.CUSTOMER_ID = @CUSTOMER_ID AND              
      PL.POLICY_ID = @APP_ID AND              
      PL.POLICY_VERSION_ID = @APP_VERSION_ID AND              
      PL.IS_ACTIVE = 'Y' AND
	  ISNULL(PAL.IS_PRIMARY_APPLICANT,0) = 1        
END

END  



--go
--Proc_GetLocationsWithCustomer 1350,105,1,''
--rollback tran
GO

