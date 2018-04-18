    
    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
/*----------------------------------------------------------                                                                            
PROC NAME  : DBO.PROC_GETPOLICYINFORMATION                                                                            
CREATED BY      : VIJAY ARORA                                                                      
DATE            : 27-10-2005                                                                      
PURPOSE         : TO GET THE POLICY INFORMATION                                                                             
                                                                          
------------------------------------------------------------                                                                            
*/                                                                      
-- drop proc dbo.Proc_GetPolicyInformation 28236,56,1,2                                                                    
ALTER PROC [dbo].[Proc_GetPolicyInformation]                                                                     
(                                                                            
                                                                      
 @CUSTOMER_ID  INT,                                                                            
 @POLICY_ID  INT,                                                                            
 @POLICY_VERSION_ID INT,      
 @LANG_ID INT  = 1                                                                            
                                                                      
)                                                                            
AS                                                                            
BEGIN                                                             
DECLARE @POLICY_STATUS_CANCELLED SMALLINT,        
@CANCELLATION_TYPE_INSURED SMALLINT,        
@POLICY_STATUS VARCHAR(100),        
@POLICY_STATUS_CODE VARCHAR(100),          
@POLICY_REINSTATMENT_TYPE SMALLINT,        
@CURRENT_TERM SMALLINT,        
@LastReinstateVersion int,        
@REINS_LAPSE INT,         
@REINS_EFFECTIVE_DATE DATETIME,      
@ACTIVE_STR NVARCHAR(20),      
@IN_ACTIVE_STR NVARCHAR(20),      
@TERMINATED_STR NVARCHAR(20),      
@REN_BROKER_COUNT INT             
      
--Added by Charles on 24-May-2010       
SELECT @ACTIVE_STR = ISNULL(MLM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK)        
LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLM WITH(NOLOCK) ON MLM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID AND MLM.LANG_ID = @LANG_ID        
WHERE MLV.LOOKUP_UNIQUE_ID = 8441 --ACTIVE        
        
SELECT @IN_ACTIVE_STR = ISNULL(MLM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK)        
LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLM WITH(NOLOCK) ON MLM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID AND MLM.LANG_ID = @LANG_ID        
WHERE MLV.LOOKUP_UNIQUE_ID = 9784 --INACTIVE        
      
SELECT @TERMINATED_STR = ISNULL(MLM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK)        
LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLM WITH(NOLOCK) ON MLM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID AND MLM.LANG_ID = @LANG_ID        
WHERE MLV.LOOKUP_UNIQUE_ID = 11780 --TERMINATED       
      
SELECT @REN_BROKER_COUNT = COUNT(REMUNERATION_ID) FROM POL_REMUNERATION WITH(NOLOCK)        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID      
--Added till here           
                                                                                                      
--added by pravesh on 6 june 2008 for cancellation date can not be less then reinstatement date if reinstate with lapse                                
--14244 Reinstate/Lapse                                
--14245 Reinstate/No Lapse                                
--16 commit Reinstatement Process           
      
SET @POLICY_STATUS_CANCELLED = 12                                                  
SET @CANCELLATION_TYPE_INSURED = 11989     
SET @REINS_LAPSE=14244                              
                                         
SELECT @CURRENT_TERM=CURRENT_TERM          
FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)         
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                          
                          
                     
IF EXISTS(SELECT CANCELLATION_TYPE FROM POL_POLICY_PROCESS WITH(NOLOCK)                                 
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                                
   AND NEW_POLICY_VERSION_ID IN                           
 (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND CURRENT_TERM=@CURRENT_TERM)                           
   AND PROCESS_ID=16   AND PROCESS_STATUS='COMPLETE' )                                
BEGIN                                
 SELECT @LASTREINSTATEVERSION = ISNULL(MAX(POLICY_VERSION_ID),0) FROM POL_POLICY_PROCESS WITH(NOLOCK)                                
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND PROCESS_ID=16  AND PROCESS_STATUS='COMPLETE'                          
  AND NEW_POLICY_VERSION_ID IN                           
 (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                           
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND CURRENT_TERM=@CURRENT_TERM )                           
                          
 SELECT @POLICY_REINSTATMENT_TYPE=CANCELLATION_TYPE,@REINS_EFFECTIVE_DATE=EFFECTIVE_DATETIME FROM POL_POLICY_PROCESS WITH(NOLOCK)                                 
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                        
 AND PROCESS_ID=16 AND POLICY_VERSION_ID > = @LASTREINSTATEVERSION                                
END                       
ELSE                                
SET @POLICY_REINSTATMENT_TYPE=0                                
--end here                                    
                                      
EXEC Proc_GetPolicyDisplayStatus @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@POLICY_STATUS out,@POLICY_STATUS_CODE out,@LANG_ID                                                  
                                                  
                                                  
SELECT  LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME, ''))) + ' ' +                                                      
  CASE WHEN LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, ''))) ='' THEN ''       
  ELSE LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME, ''))) + ' ' END                                                   
+ LTRIM(RTRIM(ISNULL(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME, ''))) AS CUSTOMERNAME,                               
                                                
POLICY.CUSTOMER_ID,                                                                       
POLICY.POLICY_ID,                                                                       
POLICY.POLICY_VERSION_ID,                                                                           
POLICY.APP_ID,                                                                             
POLICY.APP_VERSION_ID,                                 
POLICY.PARENT_APP_VERSION_ID,                                                                       
 /*CASE                                                   
 PROCESS.PROCESS_ID                                          
  when  @POLICY_STATUS_CANCELLED                                                   
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
ISNULL(MNT_SUB_LOB_MASTER_MULTILINGUAL.SUB_LOB_DESC ,MNT_SUB_LOB_MASTER.SUB_LOB_DESC) as SUB_LOB_DESC,               
MNT_SUB_LOB_MASTER.SUB_LOB_ID, --Added by Charles on 13-Apr-10 for Clauses Page                                                                 
MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME,                                                                      
MNT_AGENCY_LIST.AGENCY_CODE,                                                                      
MNT_AGENCY_LIST.AGENCY_PHONE AS AGENCY_PHONE,                                              
CONVERT(VARCHAR,POLICY.CSR) + ' - ' + USER_LIST.USER_FNAME + ' ' + USER_LIST.USER_LNAME AS CSRNAME,                                                   
POLICY.APP_TERMS,                                                                           
CONVERT(VARCHAR(10),POLICY.APP_INCEPTION_DATE, 101) AS APP_INCEPTION_DATE,          
CONVERT(VARCHAR(10), POLICY.APP_EFFECTIVE_DATE, 101) AS APP_EFFECTIVE_DATE,                                                           
CONVERT(VARCHAR(10), POLICY.APP_EXPIRATION_DATE, 101) AS APP_EXPIRATION_DATE,    
APP_EXPIRATION_DATE as EXPIRATION_DATE,                                                                         
ISNULL(POLICY.CSR,-1) AS CSR,                                                                      
POLICY.UNDERWRITER,                                                    
POLICY.IS_UNDER_REVIEW,                                      
POLICY.POLICY_LOB,                                                                           
POLICY.POLICY_SUBLOB,                                                 
POLICY.AGENCY_ID,                                                        
ISNULL(LOBM.LOB_DESC,MNT_LOB_MASTER.LOB_DESC )AS LOB,                                                                             
MNT_LOB_MASTER.LOB_CODE,                                                 
POLICY.STATE_ID,                                                                           
MNT_USER_LIST.USER_FNAME + ' ' +                                                                           
MNT_USER_LIST.USER_LNAME AS UNDERWRITERNAME,                                                                          
DIV_ID,                                                                          
DEPT_ID,                                                                          
PC_ID,                                                                      
ISNULL(BILL_TYPE_ID,'') AS BILL_TYPE,                                             
ISNULL(POLICY.BILL_TYPE,'') AS BILLTYPE,                                                               
ISNULL(COMPLETE_APP,'N') AS COMPLETE_APP,                                                                 
INSTALL_PLAN_ID,                                      
CHARGE_OFF_PRMIUM,                                                                            
RECEIVED_PRMIUM,                                            
isnull(PRODUCER,'') as PRODUCER,                                                        
PROXY_SIGN_OBTAINED,        
CLT_CUSTOMER_LIST.IS_ACTIVE IS_ACTIVE,                                                                          
--STATUS_MASTER.POLICY_STATUS_CODE POLICY_STATUS_CODE,                                                          
@POLICY_STATUS_CODE AS POLICY_STATUS_CODE,                                                  
                                                                
isnull(POLICY.POLICY_NUMBER,POLICY.APP_NUMBER) + '-' +                                                   
  (/*CASE                                                   
 PROCESS.PROCESS_ID                                                    
  when  @POLICY_STATUS_CANCELLED                                                   
  THEN                                                    
   CASE PROCESS.CANCELLATION_TYPE WHEN @CANCELLATION_TYPE_INSURED THEN STATUS_MASTER.POLICY_DESCRIPTION + ' by Insured/ ' +       
   convert(char,PROCESS.EFFECTIVE_DATETIME,101) ELSE STATUS_MASTER.POLICY_DESCRIPTION END                                                  
  ELSE                                                  
   STATUS_MASTER.POLICY_DESCRIPTION                                                  
    END*/                                                  
 isnull(@POLICY_STATUS,''))                                                  
-- isnull(STATUS_MASTER.POLICY_DESCRIPTION,'')                                                   
 + '(' + isnull(MNT_LOB_MASTER.LOB_DESC,'') + ':' +                                                            
 Isnull(PTL.LOOKUP_VALUE_DESC+',','') +                                                                
 CONVERT(VARCHAR(15),isnull(POLICY.APP_EFFECTIVE_DATE,''),101) + '-' +                                                                     
 CONVERT(VARCHAR(15),isnull(POLICY.APP_EXPIRATION_DATE,''),101) + ')' POLICY,                                                                    
--ISNULL(POLICY.POLICY_TYPE,-1)                                         
 POLICY_TYPE,                                                                          
ISNULL(POLICY.SHOW_QUOTE,'0') SHOW_QUOTE,                                                                          
YEARS_AT_PREV_ADD,                                                                          
CASE YEAR_AT_CURR_RESI WHEN 0.0 THEN ' ' ELSE CONVERT(VARCHAR(10),YEAR_AT_CURR_RESI)  END YEAR_AT_CURR_RESI,                                                                    
CASE                                                                     
  WHEN (CLT.CUSTOMER_ATTENTION_NOTE IS NULL) THEN '0'                                                                    
  WHEN (CLT.CUSTOMER_ATTENTION_NOTE='') THEN '0'                                                                     
  ELSE CLT.CUSTOMER_ATTENTION_NOTE                                                                    
  END CUSTOMER_ATTENTION_NOTE,                                                                    
ISNULL(CLT.CUSTOMER_ADDRESS1,'') + ' ' + ISNULL(CLT.CUSTOMER_ADDRESS2,'') + ' ' + ISNULL(CLT.CUSTOMER_CITY,'') + ' ' +        
ISNULL(STATE.STATE_NAME,'') + ' ' + ISNULL(COUNTRY.COUNTRY_NAME,'') + ' ' + ISNULL(CLT.CUSTOMER_ZIP,'') [ADDRESS],                             
                               
ISNULL(CUSTOMER_TYPE_LOOKUP.LOOKUP_VALUE_DESC,'') AS CUSTOMER_TYPE_DESC,                                                                    
ISNULL(CLT.CUSTOMER_BUSINESS_PHONE,'') CUSTOMER_BUSINESS_PHONE,                                          
PIC_OF_LOC,                                                            
PROPRTY_INSP_CREDIT,                                                  
                                                  
/*Modified By kasana */     
CASE WHEN ISNULL(ADD_INT.HOLDER_ID,0) = 0 THEN                                 
  RTRIM(LTRIM(ISNULL(ADD_INT.HOLDER_NAME,'') + CASE  WHEN ISNULL(ADD_INT.HOLDER_NAME,'') = '' THEN '' ELSE  ', ' END +                                
  ISNULL(ADD_INT.HOLDER_ADD1,'') + CASE WHEN ISNULL(ADD_INT.HOLDER_ADD1,'') = '' THEN '' ELSE ', ' END  +        
  ISNULL(ADD_INT.HOLDER_ADD2,'') + CASE WHEN  ISNULL(ADD_INT.HOLDER_ADD2,'') = '' THEN '' ELSE ', ' END +                                 
  ISNULL(ADD_INT.HOLDER_CITY,'') + CASE WHEN ISNULL(ADD_INT.HOLDER_CITY,'') = '' THEN '' ELSE ', ' END +                                     
ISNULL(UPPER(SL.STATE_CODE),'') + ' ' +                                 
 (ISNULL(ADD_INT.HOLDER_ZIP,''))))                                 
 ELSE                                 
  RTRIM(LTRIM(ISNULL(L.HOLDER_NAME,'') + CASE  WHEN ISNULL(L.HOLDER_NAME,'') = '' THEN '' ELSE  ', ' END +                                
  ISNULL(L.HOLDER_ADD1,'') + CASE WHEN ISNULL(L.HOLDER_ADD1,'') = '' THEN '' ELSE ', ' END  +      
  ISNULL(L.HOLDER_ADD2,'') + CASE WHEN  ISNULL(L.HOLDER_ADD2,'') = '' THEN '' ELSE ', ' END +                                 
  ISNULL(L.HOLDER_CITY,'') + CASE WHEN ISNULL(L.HOLDER_CITY,'') = '' THEN '' ELSE ', ' END +                                     
  ISNULL(UPPER(SL.STATE_CODE),'') + ' ' +                                 
 (ISNULL(L.HOLDER_ZIP,''))))                                 
 END AS BILL_MORTAGAGEE,                                      
                                              
ISNULL(DOWN_PAY_MODE ,'') AS DOWN_PAY_MODE ,                                                  
ISNULL(NOT_RENEW,'N') AS NOT_RENEW,                              
 --NOT_RENEW,                                                
NOT_RENEW_REASON,                                                  
ISNULL(REFER_UNDERWRITER ,'N') AS REFER_UNDERWRITER  ,                                     
--REFER_UNDERWRITER  ,                                                  
case  isnull(NOT_RENEW_REASON,'0') when  '0'  then '' else (SELECT LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES WITH(NOLOCK)       
WHERE LOOKUP_UNIQUE_ID=NOT_RENEW_REASON) end As NOT_RENEW_REASON_DESC ,                                              
APP_NUMBER,                                            
'' AS CLAIM_NUMBER,0 AS CLAIM_ID    ,                   
--ISNULL(CLAIM.CLAIM_ID,0) AS CLAIM_ID, ISNULL(CLAIM.CLAIM_NUMBER,'') AS CLAIM_NUMBER                                         
isnull(REFERAL_INSTRUCTIONS,'') REFERAL_INSTRUCTIONS,                                        
ISNULL(REINS_SPECIAL_ACPT, 10964) REINS_SPECIAL_ACPT , ISNULL(IS_REWRITE_POLICY,'') IS_REWRITE_POLICY,                                
--ADDED BY PRAVESH                                
CASE WHEN @POLICY_REINSTATMENT_TYPE = @REINS_LAPSE                                 
  THEN CONVERT(VARCHAR(10), isnull(@REINS_EFFECTIVE_DATE,POLICY.APP_EFFECTIVE_DATE), 101)                                  
  ELSE CONVERT(VARCHAR(10),POLICY.APP_EFFECTIVE_DATE,101)                                
  END                                
 AS POL_REINS_EFFECTIVE_DATE,                        
--Added by Charles on 24-Aug-09 for APP/POL Optimization                        
CASE when ISNULL(DATEDIFF(DAY,CASE WHEN @CURRENT_TERM>1 THEN MNT_AGENCY_LIST.TERMINATION_DATE_RENEW ELSE MNT_AGENCY_LIST.TERMINATION_DATE END,       
POLICY.APP_EFFECTIVE_DATE),0) <=0                                
THEN                                 
 CASE when ISNULL(DATEDIFF(DAY,MNT_AGENCY_LIST.TERMINATION_DATE, POLICY.APP_EFFECTIVE_DATE),0)<= 0                                
 THEN                                
  CASE MNT_AGENCY_LIST.IS_ACTIVE                                
   WHEN 'Y' THEN RTRIM(ISNULL (MNT_AGENCY_LIST.AGENCY_CODE,'')) +'-'+ cast(isnull(MNT_AGENCY_LIST.NUM_AGENCY_CODE,'') as varchar) +'-'+ ISNULL(MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME,'') + '- ('+@ACTIVE_STR+')'                                
   WHEN 'N' THEN RTRIM(ISNULL (MNT_AGENCY_LIST.AGENCY_CODE,'')) +'-'+ cast(isnull(MNT_AGENCY_LIST.NUM_AGENCY_CODE,'') as varchar) +'-'+ ISNULL(MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME,'') + '- ('+@IN_ACTIVE_STR+')'                                
  END                                
 ELSE                                
  case when ISNULL(DATEDIFF(DAY,MNT_AGENCY_LIST.TERMINATION_DATE, POLICY.APP_EFFECTIVE_DATE),0) >0                                
  then  RTRIM(ISNULL (MNT_AGENCY_LIST.AGENCY_CODE,'')) +'-'+ cast(isnull(MNT_AGENCY_LIST.NUM_AGENCY_CODE,'') as varchar) +'-'+  ISNULL(MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME,'') + '- ('+@TERMINATED_STR+')' end                                
 END                                
ELSE                                
 case when ISNULL(DATEDIFF(DAY,MNT_AGENCY_LIST.TERMINATION_DATE_RENEW, POLICY.APP_EFFECTIVE_DATE),0) >0                                
 then  RTRIM(ISNULL (MNT_AGENCY_LIST.AGENCY_CODE,'')) +'-'+ cast(isnull(MNT_AGENCY_LIST.NUM_AGENCY_CODE,'') as varchar) +'-'+ ISNULL(MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME,'') + '- ('+@TERMINATED_STR+')' end                                
END AS AGENCY_NAME_ACTIVE_STATUS,                         
CASE                            
 WHEN ISNULL(DATEDIFF(DAY,CASE WHEN @CURRENT_TERM>1 THEN MNT_AGENCY_LIST.TERMINATION_DATE_RENEW ELSE MNT_AGENCY_LIST.TERMINATION_DATE END,                       
POLICY.APP_EFFECTIVE_DATE),0)>0 THEN 'Y'                            
 ELSE 'N'                            
END AS IS_TERMINATED,                
--Added till here                              
--Added by Charles on 18-Mar-10 for Policy Page Implementation                
POLICY_CURRENCY,                
ISNULL(POLICY_LEVEL_COMISSION,0) AS POLICY_LEVEL_COMISSION,                
BILLTO,                
PAYOR,                
CO_INSURANCE,                
CONTACT_PERSON,              
POLICY.IS_ACTIVE as POLICY_IS_ACTIVE,        
--Added till here          
--Added by Charles on 14-MAY-10 for Policy Page Implementation            
ISNULL(TRANSACTION_TYPE,'') AS TRANSACTION_TYPE,        
ISNULL(PREFERENCE_DAY,'') AS PREFERENCE_DAY,        
ISNULL(BROKER_REQUEST_NO,'') AS BROKER_REQUEST_NO,        
ISNULL(POLICY_LEVEL_COMM_APPLIES,'N') AS POLICY_LEVEL_COMM_APPLIES,        
ISNULL(BROKER_COMM_FIRST_INSTM,'N') AS BROKER_COMM_FIRST_INSTM,                                              
--Added till here      
@REN_BROKER_COUNT AS REN_BROKER_COUNT   ,      
POLICY.APP_STATUS,      
ISNULL(POLICY.POLICY_STATUS,'') AS POL_POLICY_STATUS,      
CURR.CURR_SYMBOL AS CURRENCY_SYMBOL  ,    
ISNULL(OLD_POLICY_NUMBER,'') AS OLD_POLICY_NUMBER ,--Added By Lalit. April 20,2011    
ISNULL(POLICY.POLICY_DESCRIPTION, '') AS POLICY_DESCRIPTION  ,  
POLICY.FUND_TYPE,POLICY.BILLING_CURRENCY,
CLT.CUSTOMER_TYPE as CUSTOMER_TYPE   
--ISNULL(POLICY.REMARKS, '') AS REMARKS
FROM                                                                     
POL_CUSTOMER_POLICY_LIST POLICY  WITH(NOLOCK)                                          
                                           
LEFT OUTER JOIN CLT_CUSTOMER_LIST CLT  WITH(NOLOCK) ON POLICY.CUSTOMER_ID = CLT.CUSTOMER_ID           
INNER JOIN MNT_LOB_MASTER  WITH(NOLOCK) ON  POLICY.POLICY_LOB = MNT_LOB_MASTER.LOB_ID        
left JOIN MNT_LOB_MASTER_MULTILINGUAL  LOBM WITH(NOLOCK) ON  LOBM.LOB_ID = MNT_LOB_MASTER.LOB_ID  AND LOBM.LANG_ID=@LANG_ID                                                
LEFT OUTER JOIN MNT_USER_LIST WITH(NOLOCK) ON POLICY.UNDERWRITER = MNT_USER_LIST.USER_ID                                                                           
RIGHT OUTER JOIN CLT_CUSTOMER_LIST WITH(NOLOCK) ON POLICY.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID                                                                        
LEFT JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK) ON POLICY.STATE_ID = MNT_COUNTRY_STATE_LIST.STATE_ID                                                                          
LEFT JOIN MNT_SUB_LOB_MASTER WITH(NOLOCK) ON POLICY.POLICY_SUBLOB = MNT_SUB_LOB_MASTER.SUB_LOB_ID                                                               
AND POLICY.POLICY_LOB = MNT_SUB_LOB_MASTER.LOB_ID                                                    
LEFT JOIN MNT_AGENCY_LIST WITH(NOLOCK) ON POLICY.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID                                                                       
LEFT JOIN MNT_USER_LIST USER_LIST WITH(NOLOCK) ON POLICY.CSR = USER_LIST.[USER_ID]                                                                       
LEFT JOIN MNT_COUNTRY_LIST COUNTRY WITH(NOLOCK) ON COUNTRY.COUNTRY_ID = CLT.CUSTOMER_COUNTRY                                                                    
LEFT JOIN MNT_COUNTRY_STATE_LIST STATE WITH(NOLOCK) ON CLT.CUSTOMER_COUNTRY = STATE.COUNTRY_ID AND CLT.CUSTOMER_STATE = STATE.STATE_ID                                                                      
LEFT JOIN MNT_LOOKUP_VALUES CUSTOMER_TYPE_LOOKUP WITH(NOLOCK)  ON CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_TYPE                                     
--LEFT JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER ON STATUS_MASTER.POLICY_STATUS_CODE = POLICY.POLICY_STATUS                                     
LEFT JOIN MNT_LOOKUP_VALUES PTL WITH(NOLOCK) ON POLICY.POLICY_TYPE= PTL.LOOKUP_UNIQUE_ID                                                         
LEFT OUTER JOIN POL_HOME_OWNER_ADD_INT ADD_INT WITH(NOLOCK)                                                     
 ON                                                     
  POLICY.CUSTOMER_ID = ADD_INT.CUSTOMER_ID AND                               
  POLICY.POLICY_ID = ADD_INT.POLICY_ID AND                                                    
  POLICY.POLICY_VERSION_ID = ADD_INT.POLICY_VERSION_ID AND                                                    
  POLICY.DWELLING_ID = ADD_INT.DWELLING_ID AND                                                    
  POLICY.ADD_INT_ID = ADD_INT.ADD_INT_ID                                           
                                          
LEFT OUTER JOIN MNT_HOLDER_INTEREST_LIST L WITH(NOLOCK)ON ADD_INT.HOLDER_ID = L.HOLDER_ID                                  
      
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL WITH(NOLOCK) ON SL.STATE_ID = ADD_INT.HOLDER_STATE         
LEFT OUTER JOIN MNT_CURRENCY_MASTER CURR WITH(NOLOCK)   ON CURR.CURRENCY_ID = POLICY.POLICY_CURRENCY      
left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL on MNT_SUB_LOB_MASTER_MULTILINGUAL.SUB_LOB_ID =MNT_SUB_LOB_MASTER.SUB_LOB_ID and MNT_SUB_LOB_MASTER_MULTILINGUAL.LOB_ID =MNT_SUB_LOB_MASTER.LOB_ID  and MNT_SUB_LOB_MASTER_MULTILINGUAL.LANG_ID=@LANG_ID      
 
/*LEFT OUTER JOIN CLM_CLAIM_INFO CLAIM WITH (NOLOCK)                      
 ON                              
  POLICY.CUSTOMER_ID = CLAIM.CUSTOMER_ID AND                                                    
  POLICY.POLICY_ID = CLAIM.POLICY_ID AND                                       
  POLICY.POLICY_VERSION_ID = CLAIM.POLICY_VERSION_ID                                                  
 */                                            
--                                                  
/*LEFT OUTER JOIN POL_POLICY_PROCESS PROCESS ON                                                  
  POLICY.CUSTOMER_ID = PROCESS.CUSTOMER_ID AND                                                    
  POLICY.POLICY_ID = PROCESS.POLICY_ID AND                                                    
  POLICY.POLICY_VERSION_ID = PROCESS.POLICY_VERSION_ID AND                                                    
  POLICY.POLICY_STATUS = PROCESS.POLICY_CURRENT_STATUS */                                                  
WHERE                                                            
(POLICY.CUSTOMER_ID = @CUSTOMER_ID)                                                                         
AND (POLICY.POLICY_ID=@POLICY_ID)                         
AND (POLICY.POLICY_VERSION_ID=@POLICY_VERSION_ID)                                            
--ORDER BY CLAIM.CLAIM_ID DESC                                            
;                                                  
END       
    