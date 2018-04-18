  
 /*==============================================================                           
 Proc Name            :  Dbo.Proc_GetPolicyHeaderDetails                
 Created by             :  Vijay Arora                
 Date                    :  08-12-2005                
 Purpose                 :  To get the Policy Header Details                            
 Date Review By Comments  :                          
 modified by       :  Pravesh K. Chandel          
 Date    :  14 feb 2007          
 Purpose     :  Proc goes to not responding          
 Drop proc dbo.Proc_GetPolicyHeaderDetails 2043,374,1,2       
********************************************************************/                            
ALTER PROC [dbo].[Proc_GetPolicyHeaderDetails]             
(                            
 @CUSTOMER_ID  INT,                
 @POLICY_ID INT,                
 @POLICY_VERSION_ID INT,  
 @Lang_id int=1                 
)                            
AS                            
BEGIN                     
declare @POLICY_STATUS_CANCELLED smallint,@CANCELLATION_TYPE_INSURED smallint,@POLICY_STATUS VARCHAR(100)                 
SET @POLICY_STATUS_CANCELLED = 12          
SET @CANCELLATION_TYPE_INSURED = 11989          
          
--Call to SP to fetch policy status          
exec Proc_GetPolicyDisplayStatus @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@POLICY_STATUS out,null,@Lang_id          
          
SELECT                   
 CLT.CUSTOMER_ID, CLT.CUSTOMER_CODE,                            
 ISNULL(CLT.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CLT.CUSTOMER_LAST_NAME,'') AS CUSTOMER_FULLNAME,                
 ISNULL(case when POL.POLICY_NUMBER='To be generated' and @Lang_id =3 then 'Para ser gerada' else  POL.POLICY_NUMBER end,'')POLICY_NUMBER, POL.POLICY_DISP_VERSION,                
--       STATUS_MASTER.POLICY_DESCRIPTION AS POLICY_STATUS,                
/*CASE           
 PROCESS.PROCESS_ID            
  when  @POLICY_STATUS_CANCELLED           
  THEN            
   CASE PROCESS.CANCELLATION_TYPE WHEN @CANCELLATION_TYPE_INSURED THEN STATUS_MASTER.POLICY_DESCRIPTION + ' by Insured/ ' + convert(char,PROCESS.EFFECTIVE_DATETIME,101) ELSE STATUS_MASTER.POLICY_DESCRIPTION END          
  ELSE          
   STATUS_MASTER.POLICY_DESCRIPTION          
    END AS POLICY_STATUS,*/          
 ISNULL(@POLICY_STATUS,'') as POLICY_STATUS,          
 POL.POLICY_LOB,Convert(varchar(10),APP_EFFECTIVE_DATE,case when @Lang_id=  3 then 103 else 101 end) APP_EFFECTIVE_DATE,                
 Convert(varchar(10),APP_EXPIRATION_DATE,case when @Lang_id=  3 then 103 else 101 end) APP_EXPIRATION_DATE,                
Convert(varchar(10),APP_INCEPTION_DATE,case when @Lang_id=  3 then 103 else 101 end) APP_INCEPTION_DATE,                
 AGENCY.AGENCY_DISPLAY_NAME,  --Added ISNULL check, and changed to sub_code from POL.CSR & POL.PRODUCER by Charles on 10-Sep-09 for Itrack 6377      
ISNULL(CONVERT(VARCHAR,USER_LIST.SUB_CODE)+' - ','')+ISNULL(USER_LIST.USER_FNAME+' ','')+ISNULL(USER_LIST.USER_LNAME,'')+ISNULL(' / '+CONVERT(VARCHAR,USER_LIST1.SUB_CODE),'') AS CSRNAME, -- POL.PRODUCER Added by Manoj Rathore  on 1 Nov 2007              
CASE WHEN (CLT.CUSTOMER_ATTENTION_NOTE IS NULL) THEN '0'                            
WHEN (CLT.CUSTOMER_ATTENTION_NOTE='') THEN '0'                             
ELSE CLT.CUSTOMER_ATTENTION_NOTE                            
END CUSTOMER_ATTENTION_NOTE,              
  ISNULL(mlv.LOB_DESC,MNT_LOB_MASTER.LOB_DESC)LOB_DESC,              
  ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,case when @Lang_id=  3 then 'Todos' else 'All' end) AS STATE_NAME,              
  ISNULL(MNT_SUB_LOB_MASTER_MULTILINGUAL.SUB_LOB_DESC,MNT_SUB_LOB_MASTER.SUB_LOB_DESC)SUB_LOB_DESC,                     
  MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC,               
  A.LOOKUP_VALUE_DESC AS PROXY_SIGN_OBTAINED,              
  B.LOOKUP_VALUE_DESC AS CHARGE_OFF_PRMIUM,              
  POL.RECEIVED_PRMIUM,              
  POL.COMPLETE_APP,              
  POL.YEAR_AT_CURR_RESI,              
  POL.AGENCY_ID,              
  CASE WHEN @LANG_ID=3 then case when POL.BILL_TYPE='DB' THEN 'DIR' ELSE POL.BILL_TYPE END ELSE POL.BILL_TYPE END AS BILL_TYPE,              
  POL.APP_TERMS,              
  POL.UNDERWRITER,              
  POL.INSTALL_PLAN_ID,          
  ISNULL(CLAIM.CLAIM_ID,0) AS CLAIM_ID,ISNULL(CLAIM.CLAIM_NUMBER,'') AS CLAIM_NUMBER,        
  CLAIM.POLICY_VERSION_ID AS CLAIM_POLICY_VERSION_ID --Added by Charles on 3-Sep-09 for Itrack 6317           
 ,ISNULL(INS.PLAN_CODE,'N.A.') AS PLAN_CODE ,       
  ISNULL(UW.user_fname +' '+ UW.user_lname, 'N.A') AS ASSIGNED_UNDERWRITER         
                
FROM                  
 CLT_CUSTOMER_LIST CLT  WITH(NOLOCK)              
 LEFT JOIN  POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)  ON CLT.CUSTOMER_ID = POL.CUSTOMER_ID                
 LEFT JOIN  MNT_AGENCY_LIST  AGENCY  WITH(NOLOCK)  ON  POL.AGENCY_ID = AGENCY.AGENCY_ID                 
 LEFT JOIN  MNT_USER_LIST USER_LIST WITH(NOLOCK)   ON  POL.CSR = USER_LIST.[USER_ID]       
 LEFT JOIN  MNT_USER_LIST USER_LIST1 WITH(NOLOCK)   ON  POL.PRODUCER = USER_LIST1.[USER_ID] --Added by Charles on 10-Sep-09 for Itrack 6377                
 LEFT JOIN  MNT_LOB_MASTER WITH(NOLOCK) ON POL.POLICY_LOB = MNT_LOB_MASTER.LOB_ID   
 LEFT JOIN  MNT_LOB_MASTER_MULTILINGUAL MLV WITH(NOLOCK) ON POL.POLICY_LOB = MLV.LOB_ID and MLV.LANG_ID=@Lang_id                  
 LEFT JOIN MNT_COUNTRY_STATE_LIST WITH(NOLOCK) ON POL.STATE_ID =  MNT_COUNTRY_STATE_LIST.STATE_ID               
 LEFT JOIN MNT_SUB_LOB_MASTER WITH(NOLOCK) ON POL.POLICY_SUBLOB = MNT_SUB_LOB_MASTER.SUB_LOB_ID AND POL.POLICY_LOB = MNT_SUB_LOB_MASTER.LOB_ID  
 LEFT JOIN MNT_SUB_LOB_MASTER_MULTILINGUAL WITH(NOLOCK) ON POL.POLICY_SUBLOB = MNT_SUB_LOB_MASTER_MULTILINGUAL.SUB_LOB_ID AND POL.POLICY_LOB = MNT_SUB_LOB_MASTER_MULTILINGUAL.LOB_ID and MNT_SUB_LOB_MASTER_MULTILINGUAL.LANG_ID=@Lang_id                     
    
 LEFT JOIN MNT_LOOKUP_VALUES WITH(NOLOCK) ON POL.POLICY_TYPE = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID              
 LEFT JOIN MNT_LOOKUP_VALUES A WITH(NOLOCK) ON POL.PROXY_SIGN_OBTAINED = A.LOOKUP_UNIQUE_ID              
 LEFT JOIN MNT_LOOKUP_VALUES B WITH(NOLOCK) ON POL.CHARGE_OFF_PRMIUM = B.LOOKUP_UNIQUE_ID              
 LEFT JOIN CLM_CLAIM_INFO CLAIM WITH(NOLOCK) ON POL.CUSTOMER_ID = CLAIM.CUSTOMER_ID AND POL.POLICY_ID = CLAIM.POLICY_ID AND          
         POL.POLICY_VERSION_ID = CLAIM.POLICY_VERSION_ID          
-- LEFT JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER ON STATUS_MASTER.POLICY_STATUS_CODE = POL.POLICY_STATUS            
/*LEFT OUTER JOIN POL_POLICY_PROCESS PROCESS ON          
  POL.CUSTOMER_ID = PROCESS.CUSTOMER_ID AND            
  POL.POLICY_ID = PROCESS.POLICY_ID AND            
  POL.POLICY_VERSION_ID = PROCESS.POLICY_VERSION_ID AND            
  POL.POLICY_STATUS = PROCESS.POLICY_CURRENT_STATUS */          
LEFT JOIN ACT_INSTALL_PLAN_DETAIL INS WITH(NOLOCK)       
 ON INS.IDEN_PLAN_ID = POL.INSTALL_PLAN_ID      
LEFT JOIN MNT_USER_LIST UW WITH(NOLOCK) ON       
 UW.USER_ID = POL.UNDERWRITER      
      
WHERE                 
POL.CUSTOMER_ID =  @CUSTOMER_ID AND                
POL.POLICY_ID = @POLICY_ID AND                
POL.POLICY_VERSION_ID = @POLICY_VERSION_ID             
ORDER BY CLAIM.CLAIM_ID DESC              
END   