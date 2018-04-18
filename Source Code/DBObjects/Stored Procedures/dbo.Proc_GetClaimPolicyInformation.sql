IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimPolicyInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimPolicyInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN           
--drop proc dbo.Proc_GetClaimPolicyInformation          
--go  
/*----------------------------------------------------------                                              
PROC NAME  : DBO.Proc_GetClaimPolicyInformation                                              
CREATED BY      : VIJAY ARORA                                        
DATE            : 27-10-2005                                        
PURPOSE         : TO GET THE POLICY INFORMATION                                               
REVISON HISTORY :                                              
Modified by  : Pravesh K. Chandel            
Modified Date : 29 dec 2008            
Purpose   : To shaow Policy number in proper Formate            
            
USED IN         : WOLVERINE                                              
------------------------------------------------------------                                              
*/                                        
-- drop proc dbo.Proc_GetClaimPolicyInformation 27987,212,3,2                              
CREATE PROC [dbo].[Proc_GetClaimPolicyInformation]                                       
(                                              
                                        
 @CUSTOMER_ID  INT,                                              
 @POLICY_ID  INT,                                              
 @POLICY_VERSION_ID INT  ,                                            
 @LANG_ID smallint = 1                                       
)                                              
AS                                              
BEGIN                               
DECLARE @POLICY_STATUS_CANCELLED SMALLINT,@CANCELLATION_TYPE_INSURED SMALLINT,@POLICY_STATUS VARCHAR(100),@POLICY_STATUS_CODE varchar(100)                    
SET @POLICY_STATUS_CANCELLED = 12                    
SET @CANCELLATION_TYPE_INSURED = 11989     
DECLARE @CURRENT_TERM INT                                
                    
SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
                    
EXEC Proc_GetPolicyDisplayStatus @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@POLICY_STATUS out,@POLICY_STATUS_CODE out ,  @LANG_ID                 
                    
                    
SELECT  LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME, ''))) +           
(CASE CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME WHEN '' THEN '' ELSE + ' ' +            
 LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, ''))) END)  +                                            
(CASE CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME WHEN '' THEN '' ELSE + ' ' +          
 LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME, ''))) END)            
+ (CASE CLT_CUSTOMER_LIST.CUSTOMER_SUFFIX WHEN '' THEN '' ELSE + ' ' + LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_SUFFIX, '')))END) AS CUSTOMERNAME, --Done for Itrack Issue 5485 on 17 April 2009                                            
POLICY.CUSTOMER_ID,                                         
POLICY.POLICY_ID,                                         
POLICY.POLICY_VERSION_ID,                                             
POLICY.APP_ID,                                               
POLICY.APP_VERSION_ID,                                            
POLICY.PARENT_APP_VERSION_ID,                                         
 /*CASE                     
 PROCESS.PROCESS_ID                      
 WHEN  @POLICY_STATUS_CANCELLED                     
 THEN                      
 CASE PROCESS.CANCELLATION_TYPE WHEN @CANCELLATION_TYPE_INSURED THEN STATUS_MASTER.POLICY_DESCRIPTION + ' by Insured/ ' + convert(char,PROCESS.EFFECTIVE_DATETIME,101) ELSE STATUS_MASTER.POLICY_DESCRIPTION END                    
 ELSE                    
 STATUS_MASTER.POLICY_DESCRIPTION                    
 END AS POLICY_STATUS, */                    
ISNULL(@POLICY_STATUS,'') AS POLICY_STATUS,             
--STATUS_MASTER.POLICY_DESCRIPTION  AS POLICY_STATUS,                                             
ISNULL(case when POLICY.POLICY_NUMBER='To be generated' and @Lang_id =2 then 'Para ser gerada' else  POLICY.POLICY_NUMBER end,'')POLICY_NUMBER,                                               
POLICY.POLICY_DISP_VERSION,                                         
MNT_COUNTRY_STATE_LIST.STATE_NAME,                                            
MNT_COUNTRY_STATE_LIST.STATE_CODE,                                            
isnull(SUBLOBM.SUB_LOB_DESC,SUBLOB.SUB_LOB_DESC) as SUB_LOB_DESC,     
MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME,                                        
MNT_AGENCY_LIST.AGENCY_CODE,                                        
MNT_AGENCY_LIST.AGENCY_PHONE AS AGENCY_PHONE,                
CONVERT(VARCHAR,POLICY.CSR) + ' - ' + USER_LIST.USER_FNAME + ' ' + USER_LIST.USER_LNAME AS CSRNAME,                                        
POLICY.APP_TERMS,                                             
CONVERT(VARCHAR(10),POLICY.APP_INCEPTION_DATE, 101) AS APP_INCEPTION_DATE,                                             
CONVERT(VARCHAR(10), POLICY.APP_EFFECTIVE_DATE, 101) AS APP_EFFECTIVE_DATE,                             
CONVERT(VARCHAR(10), POLICY.APP_EXPIRATION_DATE, 101) AS APP_EXPIRATION_DATE,                                         
POLICY.CSR,                                             
POLICY.UNDERWRITER,                      
POLICY.IS_UNDER_REVIEW,                                           
POLICY.POLICY_LOB,                                       
POLICY.POLICY_SUBLOB,                   
POLICY.AGENCY_ID,                                         
isnull(LOBM.LOB_DESC,MNT_LOB_MASTER.LOB_DESC) AS LOB,      
MNT_LOB_MASTER.LOB_CODE,                   
POLICY.STATE_ID,                                             
MNT_USER_LIST.USER_FNAME + ' ' +                                             
MNT_USER_LIST.USER_LNAME AS UNDERWRITERNAME,                                            
DIV_ID,                                            
DEPT_ID,                                            
PC_ID,                                        
isnull(BILL_TYPE_ID,'') as BILL_TYPE,                 
ISNULL(POLICY.BILL_TYPE,'') AS BILLTYPE,                                 
COMPLETE_APP,                                           
INSTALL_PLAN_ID,                                            
CHARGE_OFF_PRMIUM,                                              
ISNULL(RECEIVED_PRMIUM,0) AS RECEIVED_PRMIUM,              
isnull(PRODUCER,'') as PRODUCER,                          
PROXY_SIGN_OBTAINED,                                            
CLT_CUSTOMER_LIST.IS_ACTIVE IS_ACTIVE,                                            
--STATUS_MASTER.POLICY_STATUS_CODE POLICY_STATUS_CODE,                            
@POLICY_STATUS_CODE AS POLICY_STATUS_CODE,                    
                                  
ISNULL(case when POLICY.POLICY_NUMBER='To be generated' and @Lang_id =2 then 'Para ser gerada' else  POLICY.POLICY_NUMBER end,POLICY.APP_NUMBER) + '-' +                     
  (/*CASE                     
 PROCESS.PROCESS_ID                      
 when  @POLICY_STATUS_CANCELLED                     
 THEN                      
 CASE PROCESS.CANCELLATION_TYPE WHEN @CANCELLATION_TYPE_INSURED THEN STATUS_MASTER.POLICY_DESCRIPTION + ' by Insured/ ' + convert(char,PROCESS.EFFECTIVE_DATETIME,101) ELSE STATUS_MASTER.POLICY_DESCRIPTION END                    
 ELSE                    
 STATUS_MASTER.POLICY_DESCRIPTION                    
 END*/                    
ISNULL(@POLICY_STATUS,''))                    
-- ISNULL(STATUS_MASTER.POLICY_DESCRIPTION,'')                     
+ ' (' + ISNULL(isnull(LOBM.LOB_DESC,MNT_LOB_MASTER.LOB_DESC),'') + ': ' +                              
ISNULL(PTL.LOOKUP_VALUE_DESC+', ','') +                                  
CONVERT(VARCHAR(15),ISNULL(POLICY.APP_EFFECTIVE_DATE,''),case when @LANG_ID=1 then 101 else 103 end) + '-' +                                       
CONVERT(VARCHAR(15),ISNULL(POLICY.APP_EXPIRATION_DATE,''),case when @LANG_ID=1 then 101 else 103 end) + ')' POLICY,                                      
--Added for itrack -941  
CASE WHEN PROCESS.EFFECTIVE_DATETIME IS NOT NULL AND PROCESS.EXPIRY_DATE IS NOT NULL  THEN    
CONVERT(VARCHAR(15),ISNULL(PROCESS.EFFECTIVE_DATETIME,''),case when @LANG_ID=1 then 101 else 103 end)+' - '+  
CONVERT(VARCHAR(15),ISNULL(PROCESS.EXPIRY_DATE,''),case when @LANG_ID=1 then 101 else 103 end) ELSE NULL END  ENDOREMENT_DETAILS,  
--Added till here   
  
ISNULL(POLICY.POLICY_TYPE,-1) POLICY_TYPE,                                            
ISNULL(POLICY.SHOW_QUOTE,'0') SHOW_QUOTE,                                            
YEARS_AT_PREV_ADD,                                            
CASE YEAR_AT_CURR_RESI WHEN 0.0 THEN ' ' ELSE CONVERT(VARCHAR(10),YEAR_AT_CURR_RESI)  END YEAR_AT_CURR_RESI      ,                                      
CASE                                       
WHEN (CLT.CUSTOMER_ATTENTION_NOTE IS NULL) THEN '0'                                      
WHEN (CLT.CUSTOMER_ATTENTION_NOTE='') THEN '0'                                       
ELSE CLT.CUSTOMER_ATTENTION_NOTE                                      
END CUSTOMER_ATTENTION_NOTE,            
CASE             
WHEN CLT.CUSTOMER_ADDRESS2 <> NULL             
THEN ISNULL(CLT.CUSTOMER_ADDRESS2,'') + ', '              
ELSE ''            
END             
CUSTOMER_ADDRESS2,                                      
ISNULL(CLT.CUSTOMER_ADDRESS1,'') + case when CLT.CUSTOMER_ADDRESS1!='' THEN CASE WHEN CLT.NUMBER!='' THEN ', ' ELSE '' END ELSE ''END   
+ISNULL(CLT.NUMBER,'')+' '+ISNULL(CLT.CUSTOMER_ADDRESS2,'')+  
CASE WHEN CLT.DISTRICT!='' THEN ' - ' ELSE '' END +ISNULL(CLT.DISTRICT,'')  
+ CASE WHEN CLT.CUSTOMER_CITY!='' THEN ' - ' ELSE ' ' END + IsNull(CLT.CUSTOMER_CITY,'')   
+ CASE WHEN STATE.STATE_CODE!='' THEN CASE WHEN CLT.DISTRICT!='' THEN '/' ELSE ' - ' END ELSE '' END   
+ IsNull(STATE.STATE_CODE,'')   
+ CASE WHEN CLT.CUSTOMER_ZIP!='' THEN CASE WHEN STATE.STATE_CODE!='' THEN ' - ' ELSE ' ' END ELSE '' END   
+ IsNull(CLT.CUSTOMER_ZIP,'') ADDRESS,                       
    
     
            
                
--IsNull(CUSTOMER_TYPE_LOOKUP.LOOKUP_VALUE_DESC,'') AS CUSTOMER_TYPE_DESC,                                      
IsNull(dbo.fun_GetLookupDesc(CLT.CUSTOMER_TYPE,@LANG_ID) ,'') AS CUSTOMER_TYPE_DESC,                                      
ISNULL(CLT.CUSTOMER_BUSINESS_PHONE,'') CUSTOMER_BUSINESS_PHONE,            
ISNULL(CLT.CUSTOMER_HOME_PHONE,'') CUSTOMER_HOME_PHONE , -- added by Manoj Rathore                            
PIC_OF_LOC,                              
PROPRTY_INSP_CREDIT,                    
                    
  (ISNULL(ADD_INT.HOLDER_NAME,'') + ' ' + ISNULL(ADD_INT.HOLDER_ADD1,'') + ' ' +                       
 ISNULL(ADD_INT.HOLDER_ADD2,'') + ' ' + ISNULL(ADD_INT.HOLDER_CITY,'') + ' ' +                       
 ISNULL(ADD_INT.HOLDER_ZIP,'')) AS BILL_MORTAGAGEE ,                    
ISNULL(DOWN_PAY_MODE ,'') AS DOWN_PAY_MODE ,                    
isnull(NOT_RENEW,'N')as NOT_RENEW,                    
NOT_RENEW_REASON,                    
isnull(REFER_UNDERWRITER ,'N') REFER_UNDERWRITER  ,                  
case  isnull(NOT_RENEW_REASON,'0') when  '0'  then '' else     
--(SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=NOT_RENEW_REASON)     
dbo.fun_GetLookupDesc(NOT_RENEW_REASON,@LANG_ID)    
end As NOT_RENEW_REASON_DESC ,                
APP_NUMBER,              
ISNULL(CLAIM.CLAIM_ID,0) AS CLAIM_ID, ISNULL(CLAIM.CLAIM_NUMBER,'') AS CLAIM_NUMBER,      
CLAIM.POLICY_VERSION_ID AS CLAIM_POLICY_VERSION_ID, --Added by Charles on 3-Sep-09 for Itrack 6317              
CURENCY.CURR_DESC AS Policy_Currency , --Modified By Lalit Chauhan,Nov 02,2010    
POLICY.POLICY_DISP_VERSION AS POLICY_DISPLAY_VERSION
FROM                                          
POL_CUSTOMER_POLICY_LIST POLICY  with(nolock)                  
LEFT OUTER JOIN CLT_CUSTOMER_LIST CLT  with(nolock) ON POLICY.CUSTOMER_ID = CLT.CUSTOMER_ID                                      
INNER JOIN MNT_LOB_MASTER  with(nolock) ON  POLICY.POLICY_LOB = MNT_LOB_MASTER.LOB_ID                    
left outer JOIN MNT_LOB_MASTER_MULTILINGUAL  LOBM with(nolock) ON  LOBM.LOB_ID = MNT_LOB_MASTER.LOB_ID   and LOBM.LANG_ID = @LANG_ID     
LEFT OUTER JOIN MNT_USER_LIST ON POLICY.UNDERWRITER = MNT_USER_LIST.USER_ID                                             
RIGHT OUTER JOIN CLT_CUSTOMER_LIST ON POLICY.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID                                          
LEFT JOIN MNT_COUNTRY_STATE_LIST ON POLICY.STATE_ID = MNT_COUNTRY_STATE_LIST.STATE_ID                                            
LEFT JOIN MNT_SUB_LOB_MASTER SUBLOB with(nolock) ON POLICY.POLICY_SUBLOB = SUBLOB.SUB_LOB_ID                                 
AND POLICY.policy_lob = SUBLOB.LOB_ID                         
LEFT JOIN MNT_SUB_LOB_MASTER_MULTILINGUAL SUBLOBM with(nolock) ON SUBLOBM.SUB_LOB_ID  = SUBLOB.SUB_LOB_ID and SUBLOBM.LOB_ID  = SUBLOB.LOB_ID and LOBM.LANG_ID = @LANG_ID                             
LEFT JOIN MNT_AGENCY_LIST ON POLICY.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID                                         
LEFT JOIN MNT_USER_LIST USER_LIST ON POLICY.CSR = USER_LIST.[USER_ID]                                         
LEFT JOIN MNT_COUNTRY_LIST COUNTRY ON COUNTRY.COUNTRY_ID = CLT.CUSTOMER_COUNTRY                 
LEFT JOIN MNT_COUNTRY_STATE_LIST STATE ON CLT.CUSTOMER_COUNTRY = STATE.COUNTRY_ID AND CLT.CUSTOMER_STATE = STATE.STATE_ID                                        
--LEFT JOIN MNT_LOOKUP_VALUES CUSTOMER_TYPE_LOOKUP with(nolock)  ON CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_TYPE                                        
--LEFT JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER ON STATUS_MASTER.POLICY_STATUS_CODE = POLICY.POLICY_STATUS                                    
LEFT JOIN MNT_LOOKUP_VALUES PTL ON POLICY.POLICY_TYPE= PTL.LOOKUP_UNIQUE_ID       
LEFT JOIN MNT_CURRENCY_MASTER CURENCY ON POLICY.Policy_Currency= CURENCY.CURRENCY_ID  --Modified by lalit,Nov 02,2010                           
LEFT OUTER JOIN POL_HOME_OWNER_ADD_INT ADD_INT with(nolock)                       
 ON                       
  POLICY.CUSTOMER_ID = ADD_INT.CUSTOMER_ID AND                      
  POLICY.POLICY_ID = ADD_INT.POLICY_ID AND                      
  POLICY.POLICY_VERSION_ID = ADD_INT.POLICY_VERSION_ID AND                      
  POLICY.DWELLING_ID = ADD_INT.DWELLING_ID AND                      
  POLICY.ADD_INT_ID = ADD_INT.ADD_INT_ID                        
LEFT OUTER JOIN CLM_CLAIM_INFO CLAIM WITH (NOLOCK)              
 ON              
  POLICY.CUSTOMER_ID = CLAIM.CUSTOMER_ID AND                      
  POLICY.POLICY_ID = CLAIM.POLICY_ID AND                      
  CLAIM.POLICY_VERSION_ID IN --added by lalit ,once a renewal process is started against a policy, the claims added on that policy cannot be retrieved to the renewal application.  
  (  
  SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID  = @POLICY_ID  
  AND CURRENT_TERM = @CURRENT_TERM ) --itrack #1109  
  --CLAIM.POLICY_VERSION_ID --Commented by Charles on 3-Sep-09 for Itrack 6317                   
               
--                    
 LEFT OUTER JOIN POL_POLICY_PROCESS PROCESS WITH(NOLOCK) ON                    
  POLICY.CUSTOMER_ID = PROCESS.CUSTOMER_ID AND                      
  POLICY.POLICY_ID = PROCESS.POLICY_ID AND                      
  POLICY.POLICY_VERSION_ID = PROCESS.NEW_POLICY_VERSION_ID  
  AND PROCESS.PROCESS_ID in (3,14)  
  AND PROCESS.PROCESS_STATUS <>'ROLLBACK'  
  --AND POLICY.POLICY_STATUS = PROCESS.POLICY_CURRENT_STATUS                   
WHERE                                         
(POLICY.CUSTOMER_ID = @CUSTOMER_ID)                                           
AND (POLICY.POLICY_ID=@POLICY_ID)                                         
AND (POLICY.POLICY_VERSION_ID=@POLICY_VERSION_ID)              
ORDER BY CLAIM.CLAIM_ID DESC                    
END                                            
--go      
--exec Proc_GetClaimPolicyInformation 28241,143,3,1              
--rollback tran      
                    
            