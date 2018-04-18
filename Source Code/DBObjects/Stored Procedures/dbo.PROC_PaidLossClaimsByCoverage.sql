IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_PaidLossClaimsByCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_PaidLossClaimsByCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                                                                                    
PROC NAME       : DBO.PROC_PaidLossClaimsByCoverage        
CREATED BY      : SUMIT CHHABRA                                                                                                                                                                  
DATE            : 06/04/2007                                                                                                                                                                    
PURPOSE         : FETCH DATA FOR PAID LOSS BY CLAIMS BY COVERAGE REPORT        
CREATED BY      : SUMIT CHHABRA                                                                                                                                                                   
REVISON HISTORY :                                                                                                                                                                    
USED IN        : WOLVERINE                                                                                                                                                                    
------------------------------------------------------------                                                                                                                                                                    
Modified by  : Asfa Praveen      
Date  : 24-Sept-2007      
Purpose  : To correct use of Adjuster_Id and User_Id      
    
Modified by  : Pravesh K chandel    
Date  :  10 Dec 2008    
Purpose  : To correct date type while selecting    
------------------------------------------------------------        
DATE     REVIEW BY          COMMENTS                                                                                                                                                                    
------   ------------       -------------------------*/                                                                                                                                                                    
-- DROP  PROC dbo.PROC_PaidLossClaimsByCoverage        
CREATE PROC [dbo].[PROC_PaidLossClaimsByCoverage]        
@CLAIM_ID INT        
AS                                                                                                                                                                    
BEGIN        
        
 --If neither of payment,recovery or expense activities have been performed, return        
 IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND ISNULL(IS_ACTIVE,'Y')='Y')        
 BEGIN        
  IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID=@CLAIM_ID AND ISNULL(IS_ACTIVE,'Y')='Y')        
  BEGIN        
   IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RECOVERY WHERE CLAIM_ID=@CLAIM_ID AND ISNULL(IS_ACTIVE,'Y')='Y')        
    RETURN 2        
  END        
 END          
        
 SELECT         
  AGENCY.AGENCY_DISPLAY_NAME AS  AGENCY_NAME,        
  ISNULL(USERS.USER_FNAME,'') + ' ' + ISNULL(USERS.USER_LNAME,'') AS ADJUSTER_NAME,        
  ISNULL(CUSTOMER.CUSTOMER_ADDRESS1,'') + ' ' + ISNULL(CUSTOMER.CUSTOMER_ADDRESS2,'') AS CUSTOMER_ADDRESS_LINE1,        
  ISNULL(CUSTOMER.CUSTOMER_CITY,'') + ', ' + ISNULL(STATE.STATE_CODE,'') + ' ' +         
  ISNULL(CUSTOMER.CUSTOMER_ZIP,'') AS CUSTOMER_ADDRESS_LINE2,
  --Done for Itrack Issue 6427 on 18 Sept 09 --- Pick Phone Nos from Claim Notification Page        
  --CUSTOMER_BUSINESS_PHONE,CUSTOMER_HOME_PHONE,        
  CLAIM.WORK_PHONE AS CUSTOMER_BUSINESS_PHONE,CLAIM.HOME_PHONE AS CUSTOMER_HOME_PHONE,
  ISNULL(POLICY.POLICY_NUMBER,'') + '-'+ ISNULL(STATUS_MASTER.POLICY_DESCRIPTION,'')        
  +'('+ISNULL(MNT_LOB_MASTER.LOB_DESC,'')+':'+ CONVERT(VARCHAR(15),ISNULL(POLICY.APP_EFFECTIVE_DATE,''),101)+'-'        
  + CONVERT(VARCHAR(15),ISNULL(POLICY.APP_EXPIRATION_DATE,''),101)+')' POLICY_DETAILS,        
   CONVERT(CHAR,CLAIM.LOSS_DATE,101) AS LOSS_DATE,CLAIM.CLAIM_DESCRIPTION,        
  (ISNULL(CUSTOMER.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER.CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CUSTOMER.CUSTOMER_LAST_NAME,'')) AS CUSTOMER_NAME,         
  POLICY.POLICY_NUMBER,CLAIM.CLAIM_NUMBER      
  FROM CLM_CLAIM_INFO CLAIM         
  LEFT OUTER JOIN         
    CLT_CUSTOMER_LIST CUSTOMER         
   ON        
    CLAIM.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID         
   LEFT OUTER JOIN         
    POL_CUSTOMER_POLICY_LIST POLICY         
   ON        
    CLAIM.CUSTOMER_ID = POLICY.CUSTOMER_ID AND        
    CLAIM.POLICY_ID = POLICY.POLICY_ID AND        
    CLAIM.POLICY_VERSION_ID = POLICY.POLICY_VERSION_ID        
   LEFT OUTER JOIN MNT_AGENCY_LIST AGENCY        
   ON        
    POLICY.AGENCY_ID = AGENCY.AGENCY_ID        
   /*LEFT OUTER JOIN CLM_TYPE_DETAIL TYPES         
   ON        
    TYPES.DETAIL_TYPE_ID = PAYMENT.ACTION_ON_PAYMENT */        
    LEFT OUTER JOIN CLM_ADJUSTER ADJUSTER        
   ON        
--    CLAIM.ADJUSTER_CODE = ADJUSTER.ADJUSTER_ID  //Commented by Asfa (24-Sept-2007)      
     CLAIM.ADJUSTER_ID = ADJUSTER.ADJUSTER_ID      
   LEFT OUTER JOIN MNT_USER_LIST USERS        
   ON        
--    ADJUSTER.ADJUSTER_CODE = USERS.USER_ID  //Commented by Asfa (24-Sept-2007)      
     ADJUSTER.USER_ID = USERS.USER_ID        
   LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST STATE        
   ON        
    CUSTOMER.CUSTOMER_STATE = STATE.STATE_ID        
   LEFT OUTER JOIN MNT_LOB_MASTER  ON  POLICY.POLICY_LOB = MNT_LOB_MASTER.LOB_ID                             
   LEFT OUTER JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER ON STATUS_MASTER.POLICY_STATUS_CODE = POLICY.POLICY_STATUS           
 WHERE CLAIM.CLAIM_ID=@CLAIM_ID        
        
        
 SELECT       
  ACTIVITY.ACTIVITY_ID ,      
  CONVERT(CHAR,ACTIVITY.CREATED_DATETIME,101) AS ENTRY_DATE,        
  CONVERT(CHAR,CLAIM.LOSS_DATE,101) AS DATE_OF_LOSS,        
  CLAIM.CLAIM_DESCRIPTION,        
  DETAIL.DETAIL_TYPE_DESCRIPTION AS TRANS_DESCRIPTION,        
--  M.COV_ID,         
  RESERVE.COVERAGE_ID AS COV_ID,        
  CASE RESERVE.COVERAGE_ID         
    WHEN '50001' THEN 'Medical'         
    WHEN '50002' THEN 'Work Loss'                                                                  
    WHEN '50003' THEN 'Death Benefits'        
    WHEN '50004' THEN 'Survivors Benefits'        
    WHEN '50005' THEN 'BI'         
    WHEN '50006' THEN 'PD'        
    WHEN '50007' THEN 'BI'         
    WHEN '50008' THEN 'PD'         
    WHEN '50009' THEN 'BI'         
    WHEN '50010' THEN 'PD'
	WHEN '50011' THEN 'BI' 
	WHEN '50012' THEN 'PD' 
    WHEN '50013' THEN 'BI' 
	WHEN '50014' THEN 'PD'                                      
    WHEN '50015' THEN 'BI'
	WHEN '50016' THEN 'PD'
	--Done for Itrack Issue 6433 on 24 Sept 09
	WHEN '20001' THEN 'Trailer'
	WHEN '20002' THEN 'Trailer'
	WHEN '20003' THEN 'Trailer'         
    ELSE M.COV_DES         
     END AS COVERAGE_DESC,             
  --M.COV_DES AS COVERAGE_DESC,        
  PAYMENT.PAYMENT_AMOUNT,PAYMENT.ACTION_ON_PAYMENT AS ACTION_ON_PAYMENT,        
  ISNULL(ACI.CHECK_NUMBER,'') AS CHECK_NUMBER,         
  CASE ISNULL(ACI.CHECK_NUMBER,'-1') WHEN '-1' THEN '' ELSE CONVERT(VARCHAR,ACI.CHECK_DATE,101) END AS CHECK_DATE        
 FROM         
  CLM_ACTIVITY_PAYMENT PAYMENT         
 JOIN         
  CLM_ACTIVITY_RESERVE RESERVE         
 ON        
  PAYMENT.CLAIM_ID=RESERVE.CLAIM_ID AND PAYMENT.RESERVE_ID = RESERVE.RESERVE_ID        
 LEFT OUTER JOIN         
-- JOIN         
  MNT_COVERAGE M         
 ON          
  RESERVE.COVERAGE_ID = M.COV_ID        
 JOIN         
  CLM_ACTIVITY ACTIVITY         
 ON         
  PAYMENT.CLAIM_ID = ACTIVITY.CLAIM_ID AND PAYMENT.ACTIVITY_ID=ACTIVITY.ACTIVITY_ID        
 JOIN         
  CLM_CLAIM_INFO CLAIM         
 ON         
  RESERVE.CLAIM_ID=CLAIM.CLAIM_ID        
 JOIN         
  CLM_TYPE_DETAIL DETAIL         
 ON        
  PAYMENT.ACTION_ON_PAYMENT = DETAIL.DETAIL_TYPE_ID        
 LEFT OUTER JOIN   
  ACT_CHECK_INFORMATION ACI        
 ON        
  ACTIVITY.CHECK_ID = ACI.CHECK_ID        
 WHERE         
  PAYMENT.CLAIM_ID=@CLAIM_ID        
  AND ISNULL(ACTIVITY.IS_ACTIVE,'Y')='Y'        
  AND ISNULL(PAYMENT.IS_ACTIVE,'Y')='Y'        
  --Added For Itrack Issue #5649  
  AND ISNULL(PAYMENT.PAYMENT_AMOUNT,0) <> 0        
 UNION        
        
 SELECT        
  ACTIVITY.ACTIVITY_ID ,     
  CONVERT(CHAR,ACTIVITY.CREATED_DATETIME,101) AS ENTRY_DATE,        
  CONVERT(CHAR,CLAIM.LOSS_DATE,101) AS DATE_OF_LOSS,        
  CLAIM.CLAIM_DESCRIPTION,        
  DETAIL.DETAIL_TYPE_DESCRIPTION AS TRANS_DESCRIPTION,        
--  M.COV_ID,         
  RESERVE.COVERAGE_ID AS COV_ID,        
  CASE RESERVE.COVERAGE_ID         
    WHEN '50001' THEN 'Medical'         
    WHEN '50002' THEN 'Work Loss'                              
    WHEN '50003' THEN 'Death Benefits'        
    WHEN '50004' THEN 'Survivors Benefits'        
    WHEN '50005' THEN 'BI'         
    WHEN '50006' THEN 'PD'        
	WHEN '50007' THEN 'BI'         
    WHEN '50008' THEN 'PD'         
    WHEN '50009' THEN 'BI'         
    WHEN '50010' THEN 'PD'
	WHEN '50011' THEN 'BI' 
	WHEN '50012' THEN 'PD' 
    WHEN '50013' THEN 'BI' 
	WHEN '50014' THEN 'PD'                                      
    WHEN '50015' THEN 'BI'
	WHEN '50016' THEN 'PD'         
    ELSE M.COV_DES         
     END AS COVERAGE_DESC,          
  --M.COV_DES AS COVERAGE_DESC,        
  EXPENSE.PAYMENT_AMOUNT,EXPENSE.ACTION_ON_PAYMENT AS ACTION_ON_PAYMENT,        
  ISNULL(ACI.CHECK_NUMBER,'') AS CHECK_NUMBER,         
  CASE ISNULL(ACI.CHECK_NUMBER,'-1') WHEN '-1' THEN '' ELSE CONVERT(VARCHAR,ACI.CHECK_DATE,101) END AS CHECK_DATE        
 FROM         
  CLM_ACTIVITY_EXPENSE EXPENSE         
 JOIN         
  CLM_ACTIVITY_RESERVE RESERVE         
 ON        
  EXPENSE.CLAIM_ID=RESERVE.CLAIM_ID AND EXPENSE.RESERVE_ID = RESERVE.RESERVE_ID        
 LEFT OUTER JOIN         
  MNT_COVERAGE M         
 ON          
  RESERVE.COVERAGE_ID = M.COV_ID        
 JOIN         
  CLM_ACTIVITY ACTIVITY         
 ON         
  EXPENSE.CLAIM_ID = ACTIVITY.CLAIM_ID AND EXPENSE.ACTIVITY_ID=ACTIVITY.ACTIVITY_ID        
 JOIN         
  CLM_CLAIM_INFO CLAIM         
 ON         
  RESERVE.CLAIM_ID=CLAIM.CLAIM_ID        
 JOIN         
  CLM_TYPE_DETAIL DETAIL         
 ON        
  EXPENSE.ACTION_ON_PAYMENT = DETAIL.DETAIL_TYPE_ID        
 LEFT OUTER JOIN        
  ACT_CHECK_INFORMATION ACI        
 ON        
  ACTIVITY.CHECK_ID = ACI.CHECK_ID        
 WHERE         
  EXPENSE.CLAIM_ID=@CLAIM_ID        
  AND ISNULL(ACTIVITY.IS_ACTIVE,'Y')='Y'        
  AND ISNULL(EXPENSE.IS_ACTIVE,'Y')='Y'    
  --Added For Itrack Issue #5649  
 AND ISNULL(EXPENSE.PAYMENT_AMOUNT,0) <> 0       
        
        
 UNION         
        
 SELECT     
  ACTIVITY.ACTIVITY_ID ,        
  CONVERT(CHAR,ACTIVITY.CREATED_DATETIME,101) AS ENTRY_DATE,        
  CONVERT(CHAR,CLAIM.LOSS_DATE,101) AS DATE_OF_LOSS,        
  CLAIM.CLAIM_DESCRIPTION,        
  DETAIL.DETAIL_TYPE_DESCRIPTION AS TRANS_DESCRIPTION,        
--  M.COV_ID,         
  RESERVE.COVERAGE_ID AS COV_ID,        
  CASE RESERVE.COVERAGE_ID         
    WHEN '50001' THEN 'Medical'         
    WHEN '50002' THEN 'Work Loss'                                                                  
    WHEN '50003' THEN 'Death Benefits'        
    WHEN '50004' THEN 'Survivors Benefits'        
    WHEN '50005' THEN 'BI'         
    WHEN '50006' THEN 'PD'        
    WHEN '50007' THEN 'BI'         
    WHEN '50008' THEN 'PD'         
    WHEN '50009' THEN 'BI'         
    WHEN '50010' THEN 'PD' 
	WHEN '50011' THEN 'BI' 
	WHEN '50012' THEN 'PD' 
    WHEN '50013' THEN 'BI' 
	WHEN '50014' THEN 'PD'                                      
    WHEN '50015' THEN 'BI'
	WHEN '50016' THEN 'PD'
	WHEN '20001' THEN 'Trailer'
	WHEN '20002' THEN 'Trailer'
	WHEN '20003' THEN 'Trailer'        
    ELSE M.COV_DES         
     END AS COVERAGE_DESC,          
  --M.COV_DES AS COVERAGE_DESC,        
  RECOVER.AMOUNT AS PAYMENT_AMOUNT,RECOVER.ACTION_ON_RECOVERY AS ACTION_ON_PAYMENT,        
  ISNULL(ACI.CHECK_NUMBER,'') AS CHECK_NUMBER,         
  CASE ISNULL(ACI.CHECK_NUMBER,'-1') WHEN '-1' THEN '' ELSE CONVERT(VARCHAR,ACI.CHECK_DATE,101) END AS CHECK_DATE        
  FROM         
   CLM_ACTIVITY_RECOVERY RECOVER        
  JOIN         
   CLM_ACTIVITY_RESERVE RESERVE         
  ON        
   RECOVER.CLAIM_ID=RESERVE.CLAIM_ID AND RECOVER.RESERVE_ID = RESERVE.RESERVE_ID        
  LEFT OUTER JOIN         
   MNT_COVERAGE M         
  ON          
   RESERVE.COVERAGE_ID = M.COV_ID        
  JOIN         
   CLM_ACTIVITY ACTIVITY         
  ON         
   RECOVER.CLAIM_ID = ACTIVITY.CLAIM_ID AND RECOVER.ACTIVITY_ID=ACTIVITY.ACTIVITY_ID        
  JOIN         
   CLM_CLAIM_INFO CLAIM         
  ON         
   RESERVE.CLAIM_ID=CLAIM.CLAIM_ID        
  JOIN         
   CLM_TYPE_DETAIL DETAIL         
  ON        
   RECOVER.ACTION_ON_RECOVERY = DETAIL.DETAIL_TYPE_ID        
 LEFT OUTER JOIN        
  ACT_CHECK_INFORMATION ACI        
 ON        
  ACTIVITY.CHECK_ID = ACI.CHECK_ID        
  WHERE         
   RECOVER.CLAIM_ID=@CLAIM_ID         
  AND ISNULL(ACTIVITY.IS_ACTIVE,'Y')='Y'        
  AND ISNULL(RECOVER.IS_ACTIVE,'Y')='Y'         
  --Added For Itrack Issue #5649  
  AND ISNULL(RECOVER.AMOUNT,0) <> 0         
         
 ORDER BY          
--  COV_ID,ACTION_ON_PAYMENT DESC        
  COV_ID,ACTIVITY.ACTIVITY_ID         
        
 RETURN 1        
        
END     
 


GO

