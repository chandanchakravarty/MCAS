IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFApplicantDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFApplicantDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------                
 Modified By  : Praveen Kumar               
 Created On : 14/09/2010             
 Purpose    : Fetch Policy Info to genrate Premium Documents                
  
 drop proc [Proc_GetPDFApplicantDetails]  2126,438,2,NULL,'POLICY'         
     
 [Proc_GetPDFApplicantDetails] 2156,128,2,'POLICY'                    
 ------------------------------------------------------*/    
  
CREATE  PROCEDURE [dbo].[Proc_GetPDFApplicantDetails]                                          
(                                                
 @CUSTOMERID   int,                                                
 @POLID                int,                                                
 @VERSIONID   int,                                                
 @CALLEDFROM  VARCHAR(20)                                                
)                                                
AS        
BEGIN    
  
--- Commentd by Praveen Kumar on 14/09/2010 Because using from POLICY ------------------------  
                            
-- IF (@CALLEDFROM='APPLICATION')                                                
-- BEGIN                               
--  SELECT                                                 
--   case ltrim(rtrim(CL.ADDRESS1))+ ltrim(rtrim(CL.ADDRESS2))+ltrim(rtrim(CL.CITY))+ltrim(rtrim(CL.STATE))+ltrim(rtrim(CL.ZIP_CODE))                                                
--   when (select top 1 ltrim(rtrim(loc_add1))+ltrim(rtrim(loc_add2))+ltrim(rtrim(loc_city))+ltrim(rtrim(loc_state))+ltrim(rtrim(loc_zip)) from pol_locations  with(nolock) where customer_id=@CUSTOMERID and app_id=@POLID and app_version_id=@VERSIONID and location_type='11812')                     
--   then '' else (select top 1 ltrim(rtrim(loc_add1))+ ', ' +ltrim(rtrim(loc_add2)) + case when ltrim(rtrim(loc_add2))=''then '' else ',' end from pol_locations  with(nolock) where customer_id=@CUSTOMERID and app_id=@POLID and app_version_id=@VERSIONID and  
    
-- location_type='11812') end LocAdd,                                                                    
--   case ltrim(rtrim(CL.ADDRESS1))+ ltrim(rtrim(CL.ADDRESS2))+ltrim(rtrim(CL.CITY))+ltrim(rtrim(CL.STATE))+ltrim(rtrim(CL.ZIP_CODE))                                                
--   when (select top 1 ltrim(rtrim(loc_add1))+ltrim(rtrim(loc_add2))+ltrim(rtrim(loc_city))+ltrim(rtrim(loc_state))+ltrim(rtrim(loc_zip))   
--   from pol_locations  with(nolock) where customer_id=@CUSTOMERID and app_id=@POLID and app_version_id=@VERSIONID and location_type='11812')                     
--   then '' else (select top 1 ltrim(rtrim(loc_city))+ ', ' +ltrim(rtrim(loc_county)) + case when ltrim(rtrim(loc_county))=''then ' ' else ', ' end + ltrim(rtrim(loc_state))+', '+ ltrim(rtrim(loc_zip))   
--   from pol_locations  with(nolock) where customer_id=@CUSTOMERID and     
--app_id= @POLID and app_version_id=@VERSIONID and location_type='11812') end LocAdd2,    
--   CL.ADDRESS1,CL.ADDRESS2,CL.CITY,SL.STATE_CODE,CL.ZIP_CODE,                                                
--  CASE CL.IS_ACTIVE when 'N' THEN ''     
-- ELSE CL.FIRST_NAME + CASE WHEN ISNULL(CL.MIDDLE_NAME,'')='' THEN ' ' ELSE + ' '+CL.MIDDLE_NAME + ' ' END + ISNULL(CL.LAST_NAME,'') --+ ' ' + ISNULL(CL.SUFFIX,'') --Comminted by Manoj Rathore Itrack No.3243 (ITrack 5086, ITrack 3243 interpreted wrongly Ne
--eraj)                  
--  END   
--  APPNAME,  
    
--    CASE ISNULL(CL.SUFFIX,'') WHEN '' THEN '' ELSE ' ' + CL.SUFFIX END AS SUFFIX,                  
--   CASE WHEN CL.IS_ACTIVE ='N' THEN '  ' ELSE  RTRIM(CL.ADDRESS1) + case when ISNULL(CL.ADDRESS2,'')='' then '' else ', ' end + ISNULL(CL.ADDRESS2,'')END APPADDRESS,                                                 
--   ISNULL(BUSINESS_PHONE,'') + ' ' + CASE ISNULL(EXT,'') WHEN '' THEN '' ELSE 'X ' + EXT END BUSINESS_PHONE,                                   
--   ISNULL(BUSINESS_PHONE,'') as APPLICANT_BUSINESS_PHONE,                            
--  CASE WHEN CL.IS_ACTIVE ='N' THEN '  ' ELSE   CL.CITY + ', ' + SL.STATE_CODE + ' ' + CL.ZIP_CODE END APPCITYSTZIP,ISNULL(CL.PHONE,'') PHONE,    case when CL.CO_APPLI_OCCU=11427 then DESC_CO_APPLI_OCCU                                                
--   else ISNULL(MV.LOOKUP_VALUE_DESC,'') end OCCUPATION,                                                
--   ISNULL(CAST(CO_APPLI_YEARS_WITH_CURR_EMPL AS VARCHAR),'') YEARSEMPL,ISNULL(MV1.LOOKUP_VALUE_DESC,'') MARTSTATUS,                                                
--   CONVERT(NVARCHAR(11),CL.CO_APPL_DOB,101) DOB,ISNULL(CO_APPL_SSN_NO,'') SSN,                                                
--   ISNULL(CAST(CO_APPL_YEAR_CURR_OCCU AS VARCHAR),'') YEARSOCCU,ISNULL(MV1.LOOKUP_VALUE_CODE,'') MARTSTATUSCODE,       
--   CASE CL.IS_ACTIVE when 'N' THEN '' ELSE RTRIM(CCL.CUSTOMER_ADDRESS1) + case ISNULL(CCL.CUSTOMER_ADDRESS2,'') when '' then '' else ', ' end + ISNULL(CCL.CUSTOMER_ADDRESS2,'') END as CUSTADDRESS,          
--CASE CL.IS_ACTIVE when 'N' THEN '' ELSE                                 
--isnull(case CCL.CUSTOMER_CITY when '' then isnull(CCL.CUSTOMER_CITY,'') else ltrim(rtrim(CCL.CUSTOMER_CITY)) + ',' end,'') +' '+ isnull(case SL1.STATE_CODE  when '' then isnull(SL1.STATE_CODE,'') else ltrim(rtrim(SL1.STATE_CODE))  end,'')               
    
    
               
--+' '+                                 
--isnull(case CCL.CUSTOMER_ZIP  when '' then isnull(CCL.CUSTOMER_ZIP,'') else ltrim(rtrim(CCL.CUSTOMER_ZIP))    end,'')                                
--        END    CUSTCITYSTZIP,                                
--   ISNULL(CO_APPLI_EMPL_NAME , '') CO_APPLI_EMPL_NAME ,                                   
--   --CCL.CUSTOMER_CITY + ' ' + SL1.STATE_CODE + ' ' + CCL.CUSTOMER_ZIP CUSTCITYSTZIP,                                     
-- CASE WHEN ISNULL(CO_APPLI_EMPL_ADDRESS, '') != '' THEN CO_APPLI_EMPL_ADDRESS + ', ' ELSE '' END + CASE WHEN ISNULL(CO_APPLI_EMPL_ADDRESS1, '') != '' THEN CO_APPLI_EMPL_ADDRESS1 + ', ' ELSE '' END +           
-- CASE WHEN ISNULL(CO_APPLI_EMPL_CITY, '') != '' THEN CO_APPLI_EMPL_CITY + ', ' ELSE '' END +           
-- CASE WHEN ISNULL(SL2.STATE_CODE, '') != '' THEN SL2.STATE_CODE + ' ' ELSE '' END            
-- + ISNULL(CO_APPLI_EMPL_ZIP_CODE, '') as CO_APPLI_EMPL_ADD                                                
--   ,ISNULL(CO_APPLI_EMPL_PHONE,'') CO_APPLI_EMPL_PHONE,EMAIL,CO_APPLI_EMPL_EMAIL                                                
--   ,APL.APPLY_INSURANCE_SCORE, CASE WHEN ISNULL(CCL.EMPLOYER_HOMEPHONE,'') != '' THEN CCL.EMPLOYER_HOMEPHONE WHEN ISNULL(CCL.CUSTOMER_BUSINESS_PHONE,'') != '' THEN CCL.CUSTOMER_BUSINESS_PHONE ELSE '' END CUSTOMER_BUSINESS_PHONE,                         
    
    
--   ISNULL(CCL.EMPLOYER_NAME,'') EMPLOYER_NAME, CASE WHEN ISNULL(CCL.EMPLOYER_ADD1, '') != '' THEN CCL.EMPLOYER_ADD1 + ', ' ELSE '' END + ISNULL(CCL.EMPLOYER_ADD2, '') EMPLOYER_ADDRESS,                                    
--   CASE APL.POL_LOB      
-- WHEN '3'    
-- THEN    
--  CASE     
--   WHEN  APL.APPLY_INSURANCE_SCORE =-2  THEN 'Middle Rate'     
--   WHEN APL.APPLY_INSURANCE_SCORE < 600  THEN 'Base Rate'                                                
--   WHEN (APL.APPLY_INSURANCE_SCORE >= 600 and APL.APPLY_INSURANCE_SCORE <= 700) OR (APL.APPLY_INSURANCE_SCORE IS NULL )THEN 'Middle Rate'                                                
--      WHEN APL.APPLY_INSURANCE_SCORE >= 701 then 'Best Rate'                                                
--   end    
-- ELSE    
--  CASE     
--   WHEN  APL.APPLY_INSURANCE_SCORE =-2  THEN 'Middle Rate'     
--   WHEN APL.APPLY_INSURANCE_SCORE < 600  THEN 'Base Rate'                                                
--   WHEN (APL.APPLY_INSURANCE_SCORE >= 600 and APL.APPLY_INSURANCE_SCORE < 751) OR (APL.APPLY_INSURANCE_SCORE IS NULL ) THEN 'Middle Rate'                                                
--      WHEN APL.APPLY_INSURANCE_SCORE >= 751 then 'Best Rate'                                                
--   end    
-- END    
-- CUSTOMER_INSURANCE_SCORE_TYPE ,al.IS_PRIMARY_APPLICANT,                             
-- (select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE)) =2 THEN '0' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE)       
--WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE)) =1                              
                                
-- THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE) END)) as CUSTOMER_REASON_CODE  ,                                                  
-- (select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2)) =2 THEN '0' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2) WHEN LEN(CONVERT(VARC  
--HAR(10),APL.CUSTOMER_REASON_CODE2))=1     
-- THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2) END)) as CUSTOMER_REASON_CODE2  ,                                                  
-- (select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3)) =2 THEN '0' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3) WHEN LEN(CONVERT(VARC  
--HAR(10),APL.CUSTOMER_REASON_CODE3))=1     
-- THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3) END)) as CUSTOMER_REASON_CODE3  ,                                              
-- (select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4)) =2 THEN '0' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4) WHEN LEN(CONVERT(VARC  
--HAR(10),APL.CUSTOMER_REASON_CODE4))=1     
-- THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4) END)) as CUSTOMER_REASON_CODE4,    
-- ISNULL(AUT.UNDERWRITING_TIER,'') AS UNDERWRITING_TIER                                                  
   
--  FROM POL_APPLICANT_LIST AL  with(nolock)   
                                               
-- INNER JOIN CLT_APPLICANT_LIST CL with(nolock) ON AL.CUSTOMER_ID=CL.CUSTOMER_ID AND AL.APPLICANT_ID = CL.APPLICANT_ID      
--   INNER JOIN CLT_CUSTOMER_LIST CCL with(nolock) ON AL.CUSTOMER_ID=CCL.CUSTOMER_ID       
--INNER JOIN POL_LIST APL with(nolock) ON  APL.CUSTOMER_ID=AL.CUSTOMER_ID AND APL.POLICY_ID=AL.POLICY_ID AND APL.POLICY_VERSION_ID=AL.POLICY_VERSION_ID                                            
--LEFT OUTER JOIN POL_UNDERWRITING_TIER AUT WITH(NOLOCK) ON APL.CUSTOMER_ID=AUT.CUSTOMER_ID AND APL.POLICY_ID=AUT.POLICY_ID AND APL.POLICY_VERSION_ID=AUT.POLICY_VERSION_ID    
--   INNER JOIN MNT_COUNTRY_STATE_LIST SL with(nolock) ON CL.STATE=SL.STATE_ID                                                
--INNER JOIN MNT_COUNTRY_STATE_LIST SL1 with(nolock) ON CCL.CUSTOMER_STATE=SL1.STATE_ID                                                
--   LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL2 with(nolock) ON CL.CO_APPLI_EMPL_STATE=SL2.STATE_ID                                                
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES MV with(nolock) ON MV.LOOKUP_ID=289 AND MV.LOOKUP_UNIQUE_ID = CL.CO_APPLI_OCCU                                   
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES MV1 with(nolock) ON MV1.LOOKUP_ID=721 AND MV1.LOOKUP_VALUE_CODE = CL.CO_APPL_MARITAL_STATUS                                                
--  WHERE AL.CUSTOMER_ID=@CUSTOMERID AND AL.POLICY_ID=@POLID AND AL.POLICY_VERSION_ID=@VERSIONID                                                                                     
--  ORDER BY AL.IS_PRIMARY_APPLICANT DESC,AL.APPLICANT_ID ASC, CL.FIRST_NAME,CL.MIDDLE_NAME,CL.LAST_NAME ASC                                                
    
--SELECT 'N' AS ADVERSE_LETTER_REQD     
--END    
  
                       
-- ELSE IF  
--- Commentd by Praveen Kumar on 14/09/2010 Because using from POLICY ENDS ------------------------  
  
 IF (@CALLEDFROM='POLICY')                                                
 BEGIN                                                
  SELECT                                                             
   case ltrim(rtrim(CL.ADDRESS1))+ ltrim(rtrim(CL.ADDRESS2))+ltrim(rtrim(CL.CITY))+ltrim(rtrim(CL.STATE))+ltrim(rtrim(CL.ZIP_CODE))                                                
   when (select top 1 ltrim(rtrim(loc_add1))+ltrim(rtrim(loc_add2))+ltrim(rtrim(loc_city))+ltrim(rtrim(loc_state))+ltrim(rtrim(loc_zip)) from pol_locations  with(nolock) where customer_id=@CUSTOMERID and POLICY_ID=@POLID and POLICY_VERSION_ID=@VERSIONID and location_type='11812')                     
   then 'TTT' else (select top 1 ltrim(rtrim(loc_add1))+ ', ' +ltrim(rtrim(loc_add2)) + case when ltrim(rtrim(loc_add2))=''then '' else ',' end from pol_locations  with(nolock) where customer_id=@CUSTOMERID and POLICY_ID=@POLID and POLICY_VERSION_ID=@VERSIONID   
    
and location_type='11812') end LocAdd,    
 case ltrim(rtrim(CL.ADDRESS1))+ ltrim(rtrim(CL.ADDRESS2))+ltrim(rtrim(CL.CITY))+ltrim(rtrim(CL.STATE))+ltrim(rtrim(CL.ZIP_CODE))                                                
   when (select top 1 ltrim(rtrim(loc_add1))+ltrim(rtrim(loc_add2))+ltrim(rtrim(loc_city))+ltrim(rtrim(loc_state))+ltrim(rtrim(loc_zip)) from pol_locations  with(nolock) where customer_id=@CUSTOMERID and POLICY_ID=@POLID and POLICY_VERSION_ID=@VERSIONID and location_type='11812')                     
  then 'TTT' else (select top 1 ltrim(rtrim(loc_city))+ ', ' +ltrim(rtrim(loc_county)) + case when ltrim(rtrim(loc_county))=''then ' ' else ', ' end + ltrim(rtrim(loc_state))+', '+ ltrim(rtrim(loc_zip))  
   from pol_locations  with(nolock) where customer_id=@CUSTOMERID and  POLICY_ID=@POLID and POLICY_VERSION_ID=@VERSIONID and location_type='11812') end LocAdd2,    
 CL.ADDRESS1,CL.ADDRESS2,CL.CITY,SL.STATE_CODE,CL.ZIP_CODE,                                                 
 CASE WHEN CL.IS_ACTIVE ='N' THEN '  ' ELSE  CL.FIRST_NAME + CASE WHEN ISNULL(CL.MIDDLE_NAME,'')='' THEN ' ' ELSE + ' ' +   
 CL.MIDDLE_NAME+' ' END + ISNULL(CL.LAST_NAME,'') + ' ' + ISNULL(CL.SUFFIX,'')    
 --Comminted by Manoj Rathore Itrack No.3243 (ITrack5086, ITrack 3243 interpreted wrongly Neeraj)            
   END APPNAME, CASE ISNULL(CL.SUFFIX,'') WHEN '' THEN '' ELSE ' ' + CL.SUFFIX END AS SUFFIX,                 
   CASE WHEN CL.IS_ACTIVE ='N' THEN '  ' ELSE  RTRIM(CL.ADDRESS1) + case when ISNULL(CL.ADDRESS2,'')='' then '' else ', ' end + ISNULL(CL.ADDRESS2,'') END APPADDRESS,                                                 
   ISNULL(BUSINESS_PHONE,'') + ' ' + CASE ISNULL(EXT,'') WHEN '' THEN '' ELSE 'X ' + EXT END BUSINESS_PHONE,                          
   ISNULL(BUSINESS_PHONE,'') as APPLICANT_BUSINESS_PHONE,                                            
  CASE WHEN CL.IS_ACTIVE ='N' THEN '  ' ELSE   CL.CITY + ', ' + SL.STATE_CODE + ' ' + CL.ZIP_CODE END APPCITYSTZIP,ISNULL(CL.PHONE,'') PHONE,                                                
   case when CL.CO_APPLI_OCCU=11427 then DESC_CO_APPLI_OCCU                                                 
   else ISNULL(MV.LOOKUP_VALUE_DESC,'') end OCCUPATION,                                                
   ISNULL(CAST(CO_APPLI_YEARS_WITH_CURR_EMPL AS VARCHAR),'') YEARSEMPL,ISNULL(MV1.LOOKUP_VALUE_DESC,'') MARTSTATUS,                                                
   CONVERT(NVARCHAR(11),CL.CO_APPL_DOB,101) DOB,ISNULL(CO_APPL_SSN_NO,'') SSN,                                                
   ISNULL(CAST(CO_APPL_YEAR_CURR_OCCU AS VARCHAR),'') YEARSOCCU,ISNULL(MV1.LOOKUP_VALUE_CODE,'') MARTSTATUSCODE,                                                
   CASE WHEN CL.IS_ACTIVE='N' THEN ' ' ELSE RTRIM(CCL.CUSTOMER_ADDRESS1) + case ISNULL(CCL.CUSTOMER_ADDRESS2,'') when '' then '' else ', ' end + ISNULL(CCL.CUSTOMER_ADDRESS2,'') END as CUSTADDRESS,                                                 
CASE WHEN CL.IS_ACTIVE='N' THEN ' ' ELSE  isnull(case CCL.CUSTOMER_CITY when '' then isnull(CCL.CUSTOMER_CITY,'') else ltrim(rtrim(CCL.CUSTOMER_CITY)) + ',' end,'')                                
+' '+                                 
isnull(case SL1.STATE_CODE  when '' then isnull(SL1.STATE_CODE,'') else ltrim(rtrim(SL1.STATE_CODE)) end,'')                                
+' '+                                 
isnull(case CCL.CUSTOMER_ZIP  when '' then isnull(CCL.CUSTOMER_ZIP,'') else ltrim(rtrim(CCL.CUSTOMER_ZIP)) end,'')                                
        END    CUSTCITYSTZIP,                                
   --CCL.CUSTOMER_CITY  + ' ' + SL1.STATE_CODE + ' ' + CCL.CUSTOMER_ZIP CUSTCITYSTZIP,                                                
                                
   ISNULL(CO_APPLI_EMPL_NAME , '') CO_APPLI_EMPL_NAME ,                                                
 CASE WHEN ISNULL(CO_APPLI_EMPL_ADDRESS, '') != '' THEN CO_APPLI_EMPL_ADDRESS + ', ' ELSE '' END + CASE WHEN ISNULL(CO_APPLI_EMPL_ADDRESS1, '') != '' THEN CO_APPLI_EMPL_ADDRESS1 + ', ' ELSE '' END +           
 CASE WHEN ISNULL(CO_APPLI_EMPL_CITY, '') != '' THEN CO_APPLI_EMPL_CITY + ', ' ELSE '' END +           
 CASE WHEN ISNULL(SL2.STATE_CODE, '') != '' THEN SL2.STATE_CODE + ' ' ELSE '' END            
 + ISNULL(CO_APPLI_EMPL_ZIP_CODE, '') as CO_APPLI_EMPL_ADD                      
   ,DESC_CO_APPLI_OCCU,ISNULL(CO_APPLI_EMPL_PHONE,'') CO_APPLI_EMPL_PHONE,EMAIL,CO_APPLI_EMPL_EMAIL                                                
   ,APL.APPLY_INSURANCE_SCORE,CASE WHEN ISNULL(CCL.EMPLOYER_HOMEPHONE,'') != '' THEN CCL.EMPLOYER_HOMEPHONE WHEN ISNULL(CCL.CUSTOMER_BUSINESS_PHONE,'') != '' THEN CCL.CUSTOMER_BUSINESS_PHONE ELSE '' END CUSTOMER_BUSINESS_PHONE,                           
   
   
    ISNULL(CCL.EMPLOYER_NAME,'') EMPLOYER_NAME, CASE WHEN ISNULL(CCL.EMPLOYER_ADD1, '') != '' THEN CCL.EMPLOYER_ADD1 + ', ' ELSE '' END + ISNULL(CCL.EMPLOYER_ADD2, '') EMPLOYER_ADDRESS,                                    
   CASE APL.POLICY_LOB    
 WHEN  '3'    
 THEN    
 CASE    
  WHEN APL.APPLY_INSURANCE_SCORE =-2    THEN 'Middle Rate'     
  WHEN APL.APPLY_INSURANCE_SCORE < 600  THEN 'Base Rate'                                                
        WHEN (APL.APPLY_INSURANCE_SCORE >= 600 and APL.APPLY_INSURANCE_SCORE <= 700) OR (APL.APPLY_INSURANCE_SCORE IS NULL ) THEN 'Middle Rate'                                                
           WHEN APL.APPLY_INSURANCE_SCORE >= 701 then 'Best Rate'                                                
        end     
 ELSE    
 CASE     
  WHEN APL.APPLY_INSURANCE_SCORE =-2  THEN 'Middle Rate'      
  WHEN APL.APPLY_INSURANCE_SCORE < 600  THEN 'Base Rate'                                                
        WHEN (APL.APPLY_INSURANCE_SCORE >= 600 and APL.APPLY_INSURANCE_SCORE < 751) OR (APL.APPLY_INSURANCE_SCORE IS NULL )  THEN 'Middle Rate'                                                
           WHEN APL.APPLY_INSURANCE_SCORE >= 751 then 'Best Rate'                                                
        end     
 END    
 CUSTOMER_INSURANCE_SCORE_TYPE,pl.IS_PRIMARY_APPLICANT,(select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE)) =2 THEN '0' + CONVERT(VARCHAR
  
(10),APL.CUSTOMER_REASON_CODE) WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE)) =1                            
 THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE) END)) as CUSTOMER_REASON_CODE  ,                                                  
 (select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2)) =2 THEN '0' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2) WHEN LEN(CONVERT(VARCHAR(10), APL.CUSTOMER_REASON_CODE2))             
 =1 THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE2) END)) as CUSTOMER_REASON_CODE2  ,                                                  
 (select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3)) =2 THEN '0' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3) WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3))            
 =1 THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE3) END)) as CUSTOMER_REASON_CODE3  ,                                                  
 (select LOOKUP_VALUE_DESC from mnt_lookup_values  with(nolock) where LOOKUP_ID = 1186 AND LOOKUP_VALUE_CODE =(CASE WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4)) =2 THEN '0' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4) WHEN LEN(CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4))            
 =1 THEN '00' + CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4) ELSE CONVERT(VARCHAR(10),APL.CUSTOMER_REASON_CODE4) END)) as CUSTOMER_REASON_CODE4,    
ISNULL(PUT.UNDERWRITING_TIER,'') AS UNDERWRITING_TIER    
  FROM POL_APPLICANT_LIST PL    with(nolock)     
   INNER JOIN CLT_APPLICANT_LIST CL with(nolock) ON PL.CUSTOMER_ID=CL.CUSTOMER_ID AND PL.APPLICANT_ID = CL.APPLICANT_ID AND CL.IS_ACTIVE = 'Y'    
   INNER JOIN CLT_CUSTOMER_LIST CCL with(nolock) ON PL.CUSTOMER_ID=CCL.CUSTOMER_ID        
   INNER JOIN POL_CUSTOMER_POLICY_LIST APL with(nolock) ON  APL.CUSTOMER_ID=PL.CUSTOMER_ID AND APL.POLICY_ID=PL.POLICY_ID AND APL.POLICY_VERSION_ID=PL.POLICY_VERSION_ID    
 LEFT OUTER JOIN POL_UNDERWRITING_TIER PUT WITH(NOLOCK) ON   APL.CUSTOMER_ID=PUT.CUSTOMER_ID AND APL.POLICY_ID=PUT.POLICY_ID AND APL.POLICY_VERSION_ID=PUT.POLICY_VERSION_ID    
   INNER JOIN MNT_COUNTRY_STATE_LIST SL with(nolock) ON CL.STATE=SL.STATE_ID                                                
   INNER JOIN MNT_COUNTRY_STATE_LIST SL1 with(nolock) ON CCL.CUSTOMER_STATE=SL1.STATE_ID                                                
   LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL2 with(nolock) ON CL.CO_APPLI_EMPL_STATE=SL2.STATE_ID                                                
   LEFT OUTER JOIN MNT_LOOKUP_VALUES MV with(nolock) ON MV.LOOKUP_ID=289 AND MV.LOOKUP_UNIQUE_ID = CL.CO_APPLI_OCCU                                                
   LEFT OUTER JOIN MNT_LOOKUP_VALUES MV1 with(nolock) ON MV1.LOOKUP_ID=721 AND MV1.LOOKUP_VALUE_CODE = CL.CO_APPL_MARITAL_STATUS                  
  WHERE PL.CUSTOMER_ID=@CUSTOMERID AND PL.POLICY_ID=@POLID AND PL.POLICY_VERSION_ID=@VERSIONID       
  ORDER BY PL.IS_PRIMARY_APPLICANT DESC,PL.APPLICANT_ID ASC, CL.FIRST_NAME,CL.MIDDLE_NAME,CL.LAST_NAME ASC               
 IF NOT EXISTS(SELECT * FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND NEW_POLICY_VERSION_ID=@VERSIONID)     
 BEGIN    
 SELECT 'N' AS ADVERSE_LETTER_REQD    
 END    
 ELSE    
 BEGIN    
  SELECT     
  CASE ISNULL(ADVERSE_LETTER_REQD,'')    
    WHEN '0'    
     THEN 'N'    
    WHEN 10964    
     THEN 'N'    
    ELSE    
     'Y'    
   END AS ADVERSE_LETTER_REQD    
   FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND NEW_POLICY_VERSION_ID=@VERSIONID AND PROCESS_STATUS != 'ROLLBACK'    
 END    
END                             
END 
GO

