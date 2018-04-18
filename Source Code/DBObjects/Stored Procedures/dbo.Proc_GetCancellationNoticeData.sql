IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCancellationNoticeData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCancellationNoticeData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_GetCancellationNoticeData]                                                                           
(                                                                                  
@CUSTOMER_ID int,                                                                  
@POLICY_ID int,                                                                  
@POLICY_VERSION_ID int,                                              
@CALLED_FROM varchar(20)=null,                            
@PROCESS_STATUS varchar(30)=null                                              
)                                                                                  
AS                                                                                  
BEGIN                             
DECLARE @PROCESS_STATUS_LOCAL varchar(30)                            
IF @PROCESS_STATUS IS NULL                            
   SET @PROCESS_STATUS_LOCAL = 'PENDING'                            
ELSE                            
   SET @PROCESS_STATUS_LOCAL = @PROCESS_STATUS                                                                  
if (@CALLED_FROM is null or UPPER(@CALLED_FROM)='POLICY')                                                  
BEGIN                                              
 SELECT                                                                  
  RTRIM(ISNULL(CCL.CUSTOMER_FIRST_NAME,'')) + ' ' + CASE WHEN ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') = '' THEN '' ELSE RTRIM(CCL.CUSTOMER_MIDDLE_NAME) + ' ' END                                        
  + ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,  
  ISNULL(CCL.CUSTOMER_SUFFIX,'') AS CUSTOMER_SUFFIX,                                                              
  RTRIM(CCL.CUSTOMER_ADDRESS1) AS CUSTOMER_ADDRESS1, RTRIM(CCL.CUSTOMER_ADDRESS2) AS CUSTOMER_ADDRESS2,                                                                   
  RTRIM(CCL.CUSTOMER_CITY) + ',' AS CUSTOMER_CITY, MCSL.STATE_CODE AS STATE_CODE,MCSL2.STATE_CODE AS POLICY_STATE_CODE, CCL.CUSTOMER_ZIP,                                                                  
  MAL.AGENCY_DISPLAY_NAME, MAL.AGENCY_CODE,                                                                  
  RTRIM(MAL.AGENCY_ADD1) AS AGENCY_ADD1, RTRIM(MAL.AGENCY_ADD2) AS AGENCY_ADD2, CASE WHEN MAL.AGENCY_CITY IS NOT NULL THEN RTRIM(MAL.AGENCY_CITY) + ',' ELSE '' END AS AGENCY_CITY,                          
  MCSL1.STATE_CODE AS AGENCY_STATE, MAL.AGENCY_ZIP, MAL.AGENCY_PHONE, 'Cancellation Notice' AS NOTICE_TYPE,                                 
  '' AS POLICY_NUMBER, PCPL.POLICY_NUMBER AS POL_NUMBER, '$' + CONVERT(VARCHAR,CONVERT(MONEY,PPP.RETURN_PREMIUM),1) AS RETURN_PREMIUM, PCPL.APP_EFFECTIVE_DATE AS APP_INCEPTION_DATE, CASE PPP.PROCESS_ID WHEN 24  THEN  PCPL.APP_EFFECTIVE_DATE WHEN 25  THEN  PCPL.APP_EFFECTIVE_DATE ELSE PPP.EFFECTIVE_DATETIME END AS PROCESS_EFFECTIVE_DATE,  PCPL.APP_EXPIRATION_DATE,           
MLM.LOB_CODE, MLM.LOB_DESC + '/' + PCPL.POLICY_NUMBER AS LOB_DESC,                          
  CAL.CO_APPLI_EMPL_NAME, CAL.CO_APPLI_EMPL_ADDRESS, CAL.CO_APPLI_EMPL_ADDRESS,                                                          
  CAL.CO_APPLI_EMPL_CITY, CAL.CO_APPLI_EMPL_STATE, CAL.CO_APPLI_EMPL_ZIP_CODE,
  CASE  PPP.REASON
  WHEN '11550' THEN 'Pro Rate Cancelled for Non-Payment'
  WHEN '11552' THEN 'Cancelled Flat for Non-Payment'                             
  ELSE
  (SELECT TOP 1 LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID = PPP.REASON )end--AND LOOKUP_ID=1231)               
  + CASE WHEN PPP.INCLUDE_REASON_DESC = 'Y' AND ISNULL(PPP.OTHER_REASON,'') != '' THEN ' (' + ISNULL(PPP.OTHER_REASON,'') + ')' ELSE '' END AS REASON,              
                
                                          
  CONVERT(VARCHAR,PPP.EFFECTIVE_DATETIME,101) AS PROCESS_DATE, (SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID = PPP.CANCELLATION_OPTION AND LOOKUP_ID=1307) AS CANCELLATION_OPTION,    
 DATENAME(MM, GETDATE()) + RIGHT(CONVERT(VARCHAR(12), GETDATE(), 107), 9) AS  CURRENT_DATES ,    
 (select  LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_UNIQUE_ID = PPP.CANCELLATION_TYPE AND LOOKUP_ID=1302) AS  CANCELLATION_TYPE,                                        
    
  (SELECT SUM(STATS_FEES) from ACT_PREMIUM_PROCESS_SUB_DETAILS with(nolock) WHERE   
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
    GROUP BY  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID) AS STATS_FEES  
                                                                   
 FROM POL_CUSTOMER_POLICY_LIST  PCPL  with(nolock)          
                 
   INNER JOIN CLT_CUSTOMER_LIST CCL  with(nolock)  ON                                                                  
    CCL.CUSTOMER_ID=PCPL.CUSTOMER_ID                                          
                                                              
   LEFT OUTER JOIN CLT_APPLICANT_LIST CAL  with(nolock) ON                                                          
    CCL.CUSTOMER_ID=CAL.CUSTOMER_ID                                                          
                                                           
   LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL   with(nolock) ON                                                                  
   CCL.CUSTOMER_STATE = MCSL.STATE_ID                                                                  
                                                                   
   LEFT OUTER JOIN MNT_LOB_MASTER MLM   with(nolock) ON                                                   
   PCPL.POLICY_LOB = MLM.LOB_ID                                                                  
                                                    
   LEFT OUTER JOIN MNT_AGENCY_LIST MAL  with(nolock) ON                                                                  
   PCPL.AGENCY_ID = MAL.AGENCY_ID                                                          
                                                     
   LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL1  with(nolock) ON                                                                  
   MAL.AGENCY_STATE = CONVERT(VARCHAR(10),MCSL1.STATE_ID)  
  
   LEFT OUTER JOIN  MNT_COUNTRY_STATE_LIST MCSL2  with(nolock) ON                                                                  
   CONVERT(VARCHAR(10),MCSL2.STATE_ID)= PCPL.STATE_ID                          
                                                                   
   LEFT OUTER JOIN POL_POLICY_PROCESS PPP with(nolock) ON                                                    
  PCPL.CUSTOMER_ID=PPP.CUSTOMER_ID AND PCPL.POLICY_ID=PPP.POLICY_ID AND PCPL.POLICY_VERSION_ID=PPP.NEW_POLICY_VERSION_ID --AND ISNULL(PPP.REASON,0) <> 0                                                    
                                                                   
 WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID AND PCPL.POLICY_ID=@POLICY_ID AND PCPL.POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
-- AND ISNULL(PPP.PROCESS_STATUS,'') LIKE '%' + @PROCESS_STATUS_LOCAL + '%'                            
 ORDER BY PPP.COMPLETED_DATETIME DESC                         
                      
SELECT   RTRIM(ISNULL(CL.FIRST_NAME,'')) + ' ' + CASE WHEN ISNULL(CL.MIDDLE_NAME,'') = '' THEN '' ELSE RTRIM(CL.MIDDLE_NAME) + ' ' END + ISNULL(CL.LAST_NAME,'') + ' '   APPNAME ,                     
ISNULL(SUFFIX,'') AS APP_SUFFIX  
FROM POL_APPLICANT_LIST PL  with(nolock)                                                   
 INNER JOIN CLT_APPLICANT_LIST CL with(nolock) ON PL.CUSTOMER_ID=CL.CUSTOMER_ID AND PL.APPLICANT_ID = CL.APPLICANT_ID                       
WHERE PL.CUSTOMER_ID=@CUSTOMER_ID AND PL.POLICY_ID=@POLICY_ID AND PL.POLICY_VERSION_ID=@POLICY_VERSION_ID                       
                                                             
END                                              
ELSE IF(UPPER(@CALLED_FROM)='APPLICATION')                                              
BEGIN                                                                  
 SELECT                   
 RTRIM(ISNULL(CCL.CUSTOMER_FIRST_NAME,'')) + ' ' + CASE WHEN ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') = '' THEN '' ELSE RTRIM(CCL.CUSTOMER_MIDDLE_NAME) + ' ' END                                        
 + ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,  
 ISNULL(CCL.CUSTOMER_SUFFIX,'') AS CUSTOMER_SUFFIX,            
 RTRIM(CCL.CUSTOMER_ADDRESS1) AS CUSTOMER_ADDRESS1, RTRIM(CCL.CUSTOMER_ADDRESS2) AS CUSTOMER_ADDRESS2,                                                   
 RTRIM(CCL.CUSTOMER_CITY) + ',' AS CUSTOMER_CITY, MCSL.STATE_CODE AS STATE_CODE,MCSL2.STATE_CODE AS POLICY_STATE_CODE, CCL.CUSTOMER_ZIP,                                                                  
 MAL.AGENCY_DISPLAY_NAME, MAL.AGENCY_CODE, MCSL1.STATE_CODE AS AGENCY_STATE,                                                                  
 RTRIM(MAL.AGENCY_ADD1) AS AGENCY_ADD1, RTRIM(MAL.AGENCY_ADD2) AS AGENCY_ADD2, CASE WHEN MAL.AGENCY_CITY IS NOT NULL THEN RTRIM(MAL.AGENCY_CITY) + ',' ELSE '' END AS AGENCY_CITY,                                                                  
 MAL.AGENCY_ZIP, MAL.AGENCY_PHONE, 'Cancellation Notice' AS NOTICE_TYPE,                                                                  
 APP.APP_NUMBER AS POLICY_NUMBER, APP.APP_NUMBER AS POL_NUMBER, APP.RECEIVED_PRMIUM, APP.APP_EFFECTIVE_DATE AS APP_INCEPTION_DATE, '' AS PROCESS_EFFECTIVE_DATE, APP.APP_EXPIRATION_DATE, MLM.LOB_CODE, MLM.LOB_DESC,                                             
 CAL.CO_APPLI_EMPL_NAME, CAL.CO_APPLI_EMPL_ADDRESS, CAL.CO_APPLI_EMPL_ADDRESS,                                                          
 CAL.CO_APPLI_EMPL_CITY, CAL.CO_APPLI_EMPL_STATE, CAL.CO_APPLI_EMPL_ZIP_CODE,                                          
-- (SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID = PPP.REASON) + ' - ' + PPP.OTHER_REASON AS REASON                                                          
 NULL AS PROCESS_DATE                                              
 FROM APP_LIST  APP   with(nolock)                                                                
                                               
 INNER JOIN CLT_CUSTOMER_LIST CCL  with(nolock)      ON                                                                  
 CCL.CUSTOMER_ID=APP.CUSTOMER_ID                                                                  
                                               
 LEFT OUTER JOIN CLT_APPLICANT_LIST CAL  with(nolock) ON                                                          
 CCL.CUSTOMER_ID=CAL.CUSTOMER_ID                          
                                               
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL with(nolock) ON                                                                  
 CCL.CUSTOMER_STATE = MCSL.STATE_ID                                                                  
                                               
 LEFT OUTER JOIN MNT_LOB_MASTER MLM  with(nolock) ON                                                                  
 APP.APP_LOB = MLM.LOB_ID                                     
                                               
 LEFT OUTER JOIN MNT_AGENCY_LIST MAL with(nolock) ON                                                                  
 APP.APP_AGENCY_ID = MAL.AGENCY_ID                                                          
                                              
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL1  with(nolock) ON                                                                  
 MAL.AGENCY_STATE = CONVERT(VARCHAR(10),MCSL1.STATE_ID)                            
   
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL2  with(nolock) ON                                                                  
 CONVERT(VARCHAR(10),MCSL2.STATE_ID)   = APP.STATE_ID                    
                                              
WHERE APP.CUSTOMER_ID=@CUSTOMER_ID AND APP.APP_ID=@POLICY_ID AND APP.APP_VERSION_ID=@POLICY_VERSION_ID                        
                      
SELECT   RTRIM(ISNULL(CL.FIRST_NAME,'')) + ' ' + CASE WHEN ISNULL(CL.MIDDLE_NAME,'') = '' THEN '' ELSE RTRIM(CL.MIDDLE_NAME) + ' ' END + ISNULL(CL.LAST_NAME,'')  APPNAME,                      
ISNULL(SUFFIX,'') AS APP_SUFFIX  
FROM APP_APPLICANT_LIST AL with(nolock)                    
 INNER JOIN CLT_APPLICANT_LIST CL with(nolock) ON AL.CUSTOMER_ID=CL.CUSTOMER_ID AND AL.APPLICANT_ID = CL.APPLICANT_ID                       
WHERE AL.CUSTOMER_ID=@CUSTOMER_ID AND AL.APP_ID=@POLICY_ID AND AL.APP_VERSION_ID=@POLICY_VERSION_ID                            
                            
END                                              
END                                                             



GO

