IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFPolicyDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFPolicyDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
----------------------------------------------------------                        
Proc Name          : Dbo.[Proc_GetPDFPolicyDetails]                        
Created by         :                        
Date               :                     
Purpose            :                         
Revison History    :                        
Used In            : Ebix Adventage   
Modified by		   : Praveen Kumar
Modification Date  : 15/09/2010
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       ---------------------------                       
--DROP PROCEDURE dbo.[Proc_GetPDFPolicyDetails]  
*/

CREATE  PROCEDURE [dbo].[Proc_GetPDFPolicyDetails]                                        
(                                              
 @CUSTOMERID   int,                                              
 @POLID                int,                                              
 @VERSIONID   int,                                              
 @CALLEDFROM  VARCHAR(20)                                              
)                                              
AS  
BEGIN                                              
-- IF (@CALLEDFROM='APPLICATION')                                              
-- BEGIN                                              
--  SELECT                                               
--   CASE APP_TERMS  WHEN 12 THEN 12                                               
--     WHEN 6 THEN 6                                               
--     ELSE 1                                              
--   END APP_TERMS,APP_NUMBER POLICY_NUMBER,CONVERT(VARCHAR(11),ISNULL(APP_EFFECTIVE_DATE,''),101) APP_EFFECTIVE_DATE,CONVERT(VARCHAR(11),ISNULL(APP_EXPIRATION_DATE,''),101) APP_EXPIRATION_DATE,1 BINDERCVGNOTBOUND,  
--ISNULL(YEAR_AT_CURR_RESI,'') YEAR_AT_CURR_RESI,ISNULL(YEARS_AT_PREV_ADD,'') YEARS_AT_PREV_ADD,                                              
--   '' BINDEREFFDATE,                        
--    '' BINDEREXPDATE,RECEIVED_PRMIUM,ISNULL(MLV.LOOKUP_VALUE_DESC,'') POLICY_TYPE,PIC_OF_LOC,                                              
--   --CASE POLICY_STATUS WHEN 'UISSUE' THEN 'New Business' WHEN 'UISSUE' THEN 'RENEWAL - Replacing Prior Declaration' WHEN 'UREINST' THEN 'Policy Reinstated & date Reinstated' when 'UENDRS' then 'Amended Declaration' END REASON,                            
  
--   '' REASON,                                              
--   CASE BILL_TYPE WHEN 'DB'  
--     THEN  
--      CASE BILL_TYPE_ID WHEN '11276'  
--       THEN 'MB'  
--      ELSE  'DB'   
--     END  
--WHEN 'DM' THEN 'DB' WHEN 'MB' THEN 'MB' WHEN 'IM' THEN 'DB' ELSE 'AB' END PAYMENTBILLING,                                              
--   CASE MLV_BILL.LOOKUP_VALUE_CODE WHEN 'DM' THEN 'BA' WHEN 'DB' THEN 'BA' WHEN 'AM' THEN 'BA' WHEN 'IM' THEN 'BA' WHEN 'MB' THEN 'BM' ELSE 'N' END PAYMENTDIRECTBILL,                                
--   --CASE BILL_TYPE WHEN 'DB' THEN 'BA' WHEN 'DM' THEN 'BA' WHEN 'MB' THEN 'BM' WHEN 'IM' THEN 'BA' WHEN 'AB' THEN '' WHEN 'DI' THEN '' WHEN 'AM' THEN '' ELSE 'N' END PAYMENTDIRECTBILL,                                              
                                    
---- Commented by Asfa (29-Jan-2008) - iTrack issue #3495  
----      CASE WHEN SYSTEM_GENERATED_FULL_PAY =1  THEN 'FP' ELSE 'OP' END PAYMENTAPPBILL                                
--      CASE WHEN MLV_bill.LOOKUP_UNIQUE_ID IN (8459,11191,11277)  THEN 'FP' ELSE 'OP' END PAYMENTAPPBILL                                
                               
--   ,PLAN_DESCRIPTION,PLAN_CODE,PROPRTY_INSP_CREDIT,                                              
--   AGENCY_CODE,NUM_AGENCY_CODE,                                  
--   CASE AgencyName WHEN 1 THEN AGENCY_DBA WHEN 0 THEN AGENCY_DISPLAY_NAME ELSE AGENCY_DISPLAY_NAME END AGENCY_DISPLAY_NAME,                                  
--   isnull(case AGENCY_ADD1 when '' then isnull(AGENCY_ADD1,'')  else ltrim(rtrim(AGENCY_ADD1)) end,'')                                  
--   +                                
--  isnull( case AGENCY_ADD2 when '' then isnull(AGENCY_ADD2,'')  else  ', ' + ltrim(rtrim(AGENCY_ADD2)) end,'')                                   
-- AGENCY_ADD,    isnull(                                  
--case AGENCY_CITY when '' then isnull(AGENCY_CITY,'') else ltrim(rtrim(AGENCY_CITY)) + ',' end,'')                                  
--+' '                                  
--+ isnull(case MNT_COUNTRY_STATE_LIST.STATE_CODE  when '' then isnull(MNT_COUNTRY_STATE_LIST.STATE_CODE,'') else ltrim(rtrim(MNT_COUNTRY_STATE_LIST.STATE_CODE)) end,'')                     
--+' '                                  
--+                              
--isnull(case AGENCY_ZIP  when '' then isnull(AGENCY_ZIP,'') else ltrim(rtrim(AGENCY_ZIP))   end,'')                
--AGENCY_CITYSTZIP,                                   
----   AGENCY_CITY + ' ' + STATE_CODE + ' ' + AGENCY_ZIP AGENCY_CITYSTZIP,                                    
--   AGENCY_PHONE,               
--   AGENCY_FAX, AGENCY_EMAIL,                                   
---- isnull(case when isnull(MUL.SUB_CODE,'') ='' then '' else (MUL.SUB_CODE + '/') end ,'') +                                   
-- isnULL(case when isnull(MUL1.SUB_CODE,'') ='' then '' else MUL1.SUB_CODE end ,'') SUB_CODE,              
-- APP_SUBLOB Subline,CONVERT(VARCHAR,CONVERT(MONEY,AUL.POLICY_LIMITS),1) AS POLICY_LIMITS,AUL.RETENTION_LIMITS,AGENCY_PHONE,NUM_AGENCY_CODE,                                        
--   '' COPY_TO, RECEIVED_PRMIUM, '' AS EFFECTIVE_DATETIME, 0 AS POLICY_VERSION_ID, 1 AS CURRENT_TERM,  
--   0 AS APPLY_INSURANCE_SCORE ,'' AS  PROCESS_ID,MCSL.STATE_CODE AS STATE_CODE                                            
--  FROM APP_LIST    with(nolock)                                             
--   left outer JOIN ACT_INSTALL_PLAN_DETAIL  with(nolock) ON IDEN_PLAN_ID=INSTALL_PLAN_ID                            
--   INNER JOIN MNT_AGENCY_LIST  with(nolock) ON APP_AGENCY_ID= AGENCY_ID                                              
--   LEFT JOIN MNT_COUNTRY_STATE_LIST  with(nolock) ON AGENCY_STATE=MNT_COUNTRY_STATE_LIST.STATE_ID                                              
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV  with(nolock) ON  MLV.LOOKUP_UNIQUE_ID=POLICY_TYPE                                              
--   LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_bill  with(nolock) ON  MLV_bill.LOOKUP_UNIQUE_ID=BILL_TYPE_ID                                
----   LEFT OUTER join mnt_user_list mul on app_list.csr=mul.user_id  and mul.is_active='Y'                                               
--  LEFT OUTER join mnt_user_list mul1  with(nolock) on app_list.PRODUCER=mul1.user_id and mul1.is_active='Y'                                  
--   LEFT OUTER JOIN APP_UMBRELLA_LIMITS AUL  with(nolock) ON AUL.CUSTOMER_ID=@CUSTOMERID AND AUL.APP_ID=@POLID AND AUL.APP_VERSION_ID=@VERSIONID       
-- INNER JOIN MNT_COUNTRY_STATE_LIST MCSL with(nolock) ON  MCSL.STATE_ID =APP_LIST.STATE_ID                                         
--   WHERE APP_LIST.CUSTOMER_ID=@CUSTOMERID AND APP_LIST.APP_ID=@POLID AND APP_LIST.APP_VERSION_ID=@VERSIONID and app_list.is_active='Y'                                              
     
-- END                                              
-- ELSE IF (@CALLEDFROM='POLICY')  
 IF (@CALLEDFROM='POLICY')                                       
 BEGIN                                              
  SELECT                                               
   CASE APP_TERMS  WHEN 12 THEN 12                                               
     WHEN 6 THEN 6                                               
     ELSE 1                                              
   END APP_TERMS,POLICY_NUMBER,                                    
--CASE POL_VER_EFFECTIVE_DATE WHEN NULL THEN CONVERT(VARCHAR(11),ISNULL(APP_EFFECTIVE_DATE,''),101) ELSE CONVERT(VARCHAR(11),ISNULL(POL_VER_EFFECTIVE_DATE,''),101) END APP_EFFECTIVE_DATE,                                      
  CONVERT(VARCHAR(11),ISNULL(APP_EFFECTIVE_DATE,''),101) APP_EFFECTIVE_DATE,                                    
--CASE POL_VER_EXPIRATION_DATE WHEN NULL THEN CONVERT(VARCHAR(11),ISNULL(APP_EXPIRATION_DATE,''),101) ELSE CONVERT(VARCHAR(11),ISNULL(POL_VER_EXPIRATION_DATE,''),101) END APP_EXPIRATION_DATE,                                      
  CONVERT(VARCHAR(11),ISNULL(APP_EXPIRATION_DATE,''),101) APP_EXPIRATION_DATE,                                    
  0 BINDERCVGNOTBOUND,ISNULL(YEAR_AT_CURR_RESI,'') YEAR_AT_CURR_RESI,ISNULL(YEARS_AT_PREV_ADD,'') YEARS_AT_PREV_ADD,                                              
   CONVERT(VARCHAR(11),ISNULL(APP_EFFECTIVE_DATE,''),101) BINDEREFFDATE,                        
   CONVERT(VARCHAR(11),ISNULL(APP_EXPIRATION_DATE,''),101) BINDEREXPDATE,RECEIVED_PRMIUM,ISNULL(MLV.LOOKUP_VALUE_DESC,'') POLICY_TYPE,PIC_OF_LOC,                                
   CASE POLICY_STATUS WHEN 'UISSUE' THEN 'New Business'when 'URENEW' then 'RENEWAL - Replacing Prior Declaration' WHEN 'RENEWED' THEN 'RENEWAL - Replacing Prior Declaration' WHEN 'UREINST' THEN 'Policy Reinstated & Date Reinstated' when 'UENDRS' then    
'Amended Declaration' END REASON,                                              
   CASE BILL_TYPE WHEN 'DB' THEN CASE BILL_TYPE_ID WHEN '11276'  
       THEN 'MB'  
      ELSE  'DB'   
     END  
 WHEN 'DM' THEN 'DB' WHEN 'MB' THEN 'MB' WHEN 'IM' THEN 'DB' ELSE 'AB' END PAYMENTBILLING,                                               
--   CASE BILL_TYPE WHEN 'DB' THEN 'BA' WHEN 'DM' THEN 'BA' WHEN 'MB' THEN 'BM' WHEN 'IM' THEN 'BA' WHEN 'AB' THEN '' WHEN 'DI' THEN '' WHEN 'AM' THEN '' ELSE 'N' END PAYMENTDIRECTBILL,                                              
   CASE MLV_BILL.LOOKUP_VALUE_CODE WHEN 'DB' THEN 'BA' WHEN 'DM' THEN 'BA' WHEN 'AM' THEN 'BA' WHEN 'IM' THEN 'BA' WHEN 'MB' THEN 'BM'  ELSE 'N' END PAYMENTDIRECTBILL,                                
  
-- Commented by Asfa (29-Jan-2008) - iTrack issue #3495  
--      CASE WHEN SYSTEM_GENERATED_FULL_PAY =1  THEN 'FP' ELSE 'OP' END PAYMENTAPPBILL                                
      CASE WHEN MLV_bill.LOOKUP_UNIQUE_ID IN (8459,11191,11277)  THEN 'FP' ELSE 'OP' END PAYMENTAPPBILL                                
 ,PLAN_DESCRIPTION,PLAN_CODE,PROPRTY_INSP_CREDIT,                                              
   AGENCY_CODE,                          
   CASE AgencyName WHEN 1 THEN AGENCY_DBA WHEN 0 THEN AGENCY_DISPLAY_NAME ELSE AGENCY_DISPLAY_NAME END AGENCY_DISPLAY_NAME,                                  
   isnull(case AGENCY_ADD1 when '' then isnull(AGENCY_ADD1,'')  else ltrim(rtrim(AGENCY_ADD1)) end,'')                                  
   +           
isnull(   case AGENCY_ADD2 when '' then isnull(AGENCY_ADD2,'')  else  ', ' + ltrim(rtrim(AGENCY_ADD2)) end,'')                                   
 AGENCY_ADD,                                    
                                  
 isnull(                                  
 case AGENCY_CITY when '' then isnull(AGENCY_CITY,'') else ltrim(rtrim(AGENCY_CITY)) + ',' end,'')                                  
 +' '                                  
 + isnull(case MNT_COUNTRY_STATE_LIST.STATE_CODE  when '' then isnull(MNT_COUNTRY_STATE_LIST.STATE_CODE,'') else ltrim(rtrim(MNT_COUNTRY_STATE_LIST.STATE_CODE))  end,'')                                  
 +' ' +                                   
 isnull(case AGENCY_ZIP  when '' then isnull(AGENCY_ZIP,'') else ltrim(rtrim(AGENCY_ZIP))    end                                  
 ,'')                                  
            AGENCY_CITYSTZIP,                                                
                                  
-- AGENCY_CITY + ' ' + STATE_CODE + ' ' + AGENCY_ZIP AGENCY_CITYSTZIP,                                  
                                  
AGENCY_PHONE,                                               
   AGENCY_FAX,  AGENCY_EMAIL,                                  
-- isnull(case when isnull(MUL.SUB_CODE,'') ='' then '' else (MUL.SUB_CODE + '/') end ,'') +                                   
 ISNULL(case when isnull(MUL1.SUB_CODE,'') ='' then '' else MUL1.SUB_CODE end ,'') SUB_CODE,                                  
policy_SUBLOB Subline,CONVERT(VARCHAR,CONVERT(MONEY,PUL.POLICY_LIMITS),1) AS POLICY_LIMITS,PUL.RETENTION_LIMITS,AGENCY_PHONE,NUM_AGENCY_CODE,                                        
   CASE WHEN PPP.INSURED = 11980 THEN 'Copy To: Insured' WHEN PPP.ADD_INT = 11980 THEN 'Copy To: Additonal Interest' WHEN PPP.AGENCY_PRINT = 11980 THEN 'Copy To: Agency' ELSE '' END AS COPY_TO                                        
   ,RECEIVED_PRMIUM, CONVERT(VARCHAR,ISNULL(PPP.EFFECTIVE_DATETIME,POL_CUSTOMER_POLICY_LIST.APP_EFFECTIVE_DATE),101) AS EFFECTIVE_DATETIME, PPP.POLICY_VERSION_ID,  
   ISNULL(CURRENT_TERM,1) CURRENT_TERM, ISNULL(APPLY_INSURANCE_SCORE, 0) AS APPLY_INSURANCE_SCORE ,PPP.PROCESS_ID ,MCSL.STATE_CODE AS STATE_CODE           
  FROM POL_CUSTOMER_POLICY_LIST  with(nolock)                                             
   left outer JOIN ACT_INSTALL_PLAN_DETAIL with(nolock) ON IDEN_PLAN_ID=INSTALL_PLAN_ID                                        
   INNER JOIN MNT_AGENCY_LIST with(nolock) ON POL_CUSTOMER_POLICY_LIST.AGENCY_ID=MNT_AGENCY_LIST.AGENCY_ID       
 LEFT JOIN MNT_COUNTRY_STATE_LIST with(nolock) ON AGENCY_STATE=MNT_COUNTRY_STATE_LIST.STATE_ID                                              
   LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV with(nolock) ON  MLV.LOOKUP_UNIQUE_ID=POLICY_TYPE                                              
  LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV_bill with(nolock) ON  MLV_bill.LOOKUP_UNIQUE_ID=BILL_TYPE_ID                                
--   LEFT OUTER join mnt_user_list mul with(nolock) on POL_CUSTOMER_POLICY_LIST.csr=mul.user_id        and mul.is_active='Y'                                   
   LEFT OUTER join mnt_user_list mul1 with(nolock) on POL_CUSTOMER_POLICY_LIST.PRODUCER=mul1.user_id  and mul1.is_active='Y'         
   LEFT OUTER JOIN POL_UMBRELLA_LIMITS PUL with(nolock) ON PUL.CUSTOMER_ID=@CUSTOMERID AND PUL.POLICY_ID=@POLID AND PUL.POLICY_VERSION_ID=@VERSIONID                                              
   LEFT OUTER JOIN POL_POLICY_PROCESS PPP with(nolock) ON PPP.CUSTOMER_ID=@CUSTOMERID AND PPP.POLICY_ID=@POLID AND PPP.NEW_POLICY_VERSION_ID=@VERSIONID AND PPP.PROCESS_STATUS != 'ROLLBACK'                                       
   INNER JOIN MNT_COUNTRY_STATE_LIST MCSL ON MCSL.STATE_ID =POL_CUSTOMER_POLICY_LIST.STATE_ID  
  WHERE POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=@CUSTOMERID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=@POLID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=@VERSIONID and POL_CUSTOMER_POLICY_LIST.is_active='Y'                                              
  
 END                                              
END  
GO

