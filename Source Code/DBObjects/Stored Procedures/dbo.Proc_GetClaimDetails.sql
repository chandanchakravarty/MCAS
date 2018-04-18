IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- DROP PROC dbo.Proc_GetClaimDetails           
CREATE PROC [dbo].[Proc_GetClaimDetails]            
(              
 @CLAIM_ID     int          
)              
AS              
BEGIN  
  
  DECLARE @DUMMY_POLICY_ID INT  
  DECLARE @ALERT_FLG CHAR(1)  
  DECLARE @ACC_COI_FLG CHAR(1)  
  
  SELECT @ACC_COI_FLG=ACC_COI_FLG FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID
  
  ------------------------------------------------------------------------ 
  -- FOR ACCEPTED COINSURANCE UPLOADED CLAIMS ONLY
  -- SELECT ANY COVERAGE WHICH HAS COVERAGE_SI_FLAG='N' AND SUM INSURED(LIMIT_1) IS NOT EQUAL TO POLICY LIMIT
  ------------------------------------------------------------------------ 
  IF @ACC_COI_FLG='Y' AND(EXISTS( SELECT CLAIM_ID FROM CLM_PRODUCT_COVERAGES WHERE CLAIM_ID=@CLAIM_ID AND COVERAGE_SI_FLAG='N' AND LIMIT_1!=POLICY_LIMIT))
    SET @ALERT_FLG='Y'
  ELSE
    SET @ALERT_FLG='N'
			  
  SELECT @DUMMY_POLICY_ID = DUMMY_POLICY_ID FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID  
  IF @DUMMY_POLICY_ID IS NOT NULL  
     BEGIN  
			SELECT           
				CCI.CUSTOMER_ID,          
				CCI.POLICY_ID,          
				CCI.POLICY_VERSION_ID,          
				CCI.CLAIM_ID,          
				CLAIM_NUMBER,          
				LOSS_DATE,          
				MUL.ADJUSTER_CODE,          
				REPORTED_BY,          
				CATASTROPHE_EVENT_CODE,          
				CLAIMANT_INSURED,          
				INSURED_RELATIONSHIP,          
				CLAIMANT_NAME,          
				CCI.COUNTRY,          
				ZIP,          
				ADDRESS1,          
				ADDRESS2,          
				CITY,          
				HOME_PHONE,          
				WORK_PHONE,          
				MOBILE_PHONE,          
				WHERE_CONTACT,          
				WHEN_CONTACT,          
				DIARY_DATE,          
				CLAIM_STATUS,          
				OUTSTANDING_RESERVE,          
				RESINSURANCE_RESERVE,          
				PAID_LOSS,          
				PAID_EXPENSE,          
				RECOVERIES,          
				CLAIM_DESCRIPTION,          
				CCI.SUB_ADJUSTER,          
				SUB_ADJUSTER_CONTACT,          
				EXTENSION,          
				CCI.DUMMY_POLICY_ID,          
				LOSS_TIME_AM_PM,          
				LITIGATION_FILE,          
				CDP.LOB_ID AS POLICY_LOB,        
				DUMMY_STATE AS STATE_ID  ,
				CCI.CO_INSURANCE_TYPE,
				@ALERT_FLG AS ALERT_FLG       
			FROM           
				CLM_CLAIM_INFO CCI     
				JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = CCI.ADJUSTER_ID    
				LEFT JOIN MNT_USER_LIST MUL ON CA.USER_ID = MUL.USER_ID           
				LEFT JOIN CLM_DUMMY_POLICY CDP ON CDP.CLAIM_ID = CCI.CLAIM_ID  
			WHERE CCI.CLAIM_ID = @CLAIM_ID          
     END  
  ELSE  
     BEGIN  
			SELECT           
				C.CUSTOMER_ID,          
				C.POLICY_ID,          
				C.POLICY_VERSION_ID,          
				CLAIM_ID,          
				CLAIM_NUMBER,          
				LOSS_DATE,          
				MUL.ADJUSTER_CODE,          
				REPORTED_BY,          
				CATASTROPHE_EVENT_CODE,          
				CLAIMANT_INSURED,          
				INSURED_RELATIONSHIP,          
				CLAIMANT_NAME,          
				C.COUNTRY,          
				ZIP,          
				ADDRESS1,          
				ADDRESS2,          
				CITY,          
				HOME_PHONE,          
				WORK_PHONE,          
				MOBILE_PHONE,          
				WHERE_CONTACT,          
				WHEN_CONTACT,          
				DIARY_DATE,          
				CLAIM_STATUS,          
				OUTSTANDING_RESERVE,          
				RESINSURANCE_RESERVE,          
				PAID_LOSS,          
				PAID_EXPENSE,          
				RECOVERIES,          
				CLAIM_DESCRIPTION,          
				C.SUB_ADJUSTER,          
				SUB_ADJUSTER_CONTACT,          
				EXTENSION,          
				DUMMY_POLICY_ID,          
				LOSS_TIME_AM_PM,          
				LITIGATION_FILE,          
				POLICY_LOB,        
				P.STATE_ID STATE_ID ,
				C.CO_INSURANCE_TYPE ,
				@ALERT_FLG AS ALERT_FLG              
			FROM           
			    CLM_CLAIM_INFO C     
				JOIN CLM_ADJUSTER CA ON CA.ADJUSTER_ID = C.ADJUSTER_ID    
				LEFT JOIN MNT_USER_LIST MUL ON CA.USER_ID = MUL.USER_ID           
				LEFT JOIN POL_CUSTOMER_POLICY_LIST P ON P.CUSTOMER_ID = C.CUSTOMER_ID AND P.POLICY_ID = C.POLICY_ID AND P.POLICY_VERSION_ID = C.POLICY_VERSION_ID          
			WHERE CLAIM_ID = @CLAIM_ID          
     END            
END        
        
      
    
  
  
  
  


GO

