IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTierForUnderWriter_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTierForUnderWriter_POL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_GetTierForUnderWriter_POL
CREATE  PROC dbo.Proc_GetTierForUnderWriter_POL  
(  
 @CUSTOMER_ID INT,  
 @POLICY_ID INT,  
 @POLICY_VERSION_ID INT  
)  
AS  
BEGIN  
  
  
DECLARE @EFFECTIVE_DATE DATETIME  
DECLARE @EXP_DATE DATETIME  
  
DECLARE @LAPSE_DAYS INT  
SET @LAPSE_DAYS = 0  
  
DECLARE @LIMIT_TYPE VARCHAR(22)  
DECLARE @PRIOR_LIMIT VARCHAR(30)  
DECLARE @STATE_ID INT  
  
/*  
114183 563 70 TOWL Towing & Labor  
114184 563 71 RENT Rental Reimburse  
*/  
  
DECLARE @TOWING_LABOR INT  
SET @TOWING_LABOR = 114183  
  
DECLARE @RENTAL_REIMBURSE INT  
SET @RENTAL_REIMBURSE = 114184  
  
  
DECLARE @TOTAL_NAF INT  
SET @TOTAL_NAF = 0  
  
DECLARE @LOSS_OCCURENCE_DATE DATETIME  
  
DECLARE @THREEYEARDAYS INT  
 SET @THREEYEARDAYS=0  
  
DECLARE @THREEYEARLESSDATE DATETIME  
  
  
--CALCULATE EFFECTIVE DATE OF POLICY  
  
 /*LAPSE DAYS*/  
 SELECT   
 @EFFECTIVE_DATE = APP_EFFECTIVE_DATE,  
 @STATE_ID = STATE_ID    
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
 WHERE CUSTOMER_ID = @CUSTOMER_ID   
 AND POLICY_ID = @POLICY_ID   
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
 SELECT TOP 1 @EXP_DATE = EXP_DATE, @PRIOR_LIMIT = PRIOR_BI_CSL_LIMIT   
 FROM   
 APP_PRIOR_CARRIER_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID  
 AND LOB='2'  
 AND ISNULL(IS_ACTIVE,'N') = 'Y' ORDER BY EXP_DATE  
  
 SELECT @LAPSE_DAYS = DATEDIFF(DAY,@EXP_DATE,@EFFECTIVE_DATE)  
 IF(@LAPSE_DAYS IS NULL)  
  SET @LAPSE_DAYS = 0  
  
  
 /*LIMIT TYPE */  
IF( NOT EXISTS(SELECT 1 FROM APP_PRIOR_CARRIER_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND LOB='2' AND ISNULL(IS_ACTIVE,'N') = 'Y'))  
BEGIN  
	 SET @PRIOR_LIMIT = '25';  
	 SET @LIMIT_TYPE = 'BI'  --NO PRIOR POLICY INFO  	  
END
ELSE
BEGIN	
	 IF (EXISTS(SELECT TOP 1 PRIOR_BI_CSL_LIMIT FROM APP_PRIOR_CARRIER_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID  
	 AND LOB='2' AND ISNULL(IS_ACTIVE,'N') = 'Y' AND PRIOR_BI_CSL_LIMIT LIKE '%/%' ORDER BY EFF_DATE))  
	 BEGIN  
		  SET @PRIOR_LIMIT = LTRIM(RTRIM(UPPER(DBO.PIECE(@PRIOR_LIMIT,'/',1))))  
		  SET @LIMIT_TYPE = 'BI'    
	 END  
	 ELSE  
	 BEGIN  
		  SET @PRIOR_LIMIT = LTRIM(RTRIM(replace(@PRIOR_LIMIT,',','')))  
		  SET @LIMIT_TYPE = 'CSL'  
	 END  
END
  
 /* NAF CLAIMS - PRIOR LOSS  
 Look at the Prior Loss Tab       
 Look at the number of claims based on the same Line of Business within the last 36 months      
 "Total the number of Claims showing as NO Fault or Blank  and where the amount paid is >0 (12.08.2009)"       
 If the Loss Type is Rental Reimburse or Towing and Labour than the claims is not be included as a Not at Fault Claim      
 Looking for flexibility here so the WM can add/delete loss types as required       
 */  
 SET @THREEYEARLESSDATE = DATEADD(YEAR,-3,@EFFECTIVE_DATE)  
 SET @THREEYEARDAYS = DATEDIFF(DAY,@THREEYEARLESSDATE,@EFFECTIVE_DATE)  
 --SELECT @THREEYEARLESSDATE,@EFFECTIVE_DATE_POLICY   

	DECLARE @PRIOR_LOSS INT
	 ---PRIOR LOSSES COUNT  
	  
	 SELECT  @PRIOR_LOSS = COUNT(ISNULL(AT_FAULT,0))  
	 FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID  
	 AND LOB='2'   
	 AND ISNULL(IS_ACTIVE,'N') = 'Y'   
	 AND ISNULL(AT_FAULT,10964) IN(10964,0)  
	 AND ISNULL(AMOUNT_PAID,0) > 0  
	 AND ISNULL(LOSS_TYPE,0) NOT IN (@TOWING_LABOR,@RENTAL_REIMBURSE)  
	 AND (DATEDIFF(DAY,APP_PRIOR_LOSS_INFO.OCCURENCE_DATE,@EFFECTIVE_DATE) <= @THREEYEARDAYS)   

	DECLARE @CLAIM_LOSS INT
	--CLAIM LOSS COUNT
	SELECT @CLAIM_LOSS = COUNT(CUSTOMER_ID) 
	FROM CLM_CLAIM_INFO WITH (NOLOCK) 
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB_ID=2 
	AND DATEDIFF(DAY,ISNULL(CLM_CLAIM_INFO.LOSS_DATE,'0'),@EFFECTIVE_DATE)<=@THREEYEARDAYS   
	AND ISNULL(AT_FAULT_INDICATOR,0) IN (1,0) --No and blank..2 is YES
	AND (ISNULL(PAID_LOSS,0) + ISNULL(PAID_EXPENSE,0) > 0)

	SET @TOTAL_NAF = ISNULL(@PRIOR_LOSS ,0) + ISNULL(@CLAIM_LOSS,0)

  
-----------############################# RENEWAL ##################################3  
--STEP :1  
--During the proceeding 36 months (on Brics)  see if there has been a Status of Reinstatement - Lapsed coverage        
--If yes then calculate the number of days where coverage has been lapsed - and use this for the number of Lapsed days        
--If there has been more than 1 reinstatement - lapsed coverage - then accumulate the number of days for all cases        
  
DECLARE @SUSPENDED_VEHILCE INT   
SET @SUSPENDED_VEHILCE = 11618  
  
    SELECT @LAPSE_DAYS =  SUM((DATEDIFF(DAY,PCPLS.APP_EFFECTIVE_DATE,PPPS.EFFECTIVE_DATETIME)))   
	FROM     
    POL_POLICY_PROCESS PPPS  WITH(NOLOCK)  
	INNER JOIN POL_CUSTOMER_POLICY_LIST PCPLS  WITH (NOLOCK)    
    ON PPPS.CUSTOMER_ID=PCPLS.CUSTOMER_ID  
	AND PPPS.POLICY_ID=PCPLS.POLICY_ID   
	AND PPPS.NEW_POLICY_VERSION_ID=PCPLS.POLICY_VERSION_ID     
    AND PPPS.PROCESS_STATUS!='ROLLBACK'    
    WHERE  
	--AND ISNULL(REVERT_BACK,'N') = 'N'   
	PPPS.CUSTOMER_ID=@CUSTOMER_ID   
	AND PPPS.POLICY_ID=@POLICY_ID     
	AND PPPS.PROCESS_ID =16  
	GROUP BY PCPLS.APP_EFFECTIVE_DATE  
  
  
 /*Look at Vehicle Coverages tab 1st vehicle        
 Coverage Single Limit Liability (CSL) or Bodily Injury Liability (Split Limits) to determine the Prior BI/CSL Limit        
  If vehicle coverage is suspended then look to see if there are any other vehicles on the policy with Coverage Single Limit Liability (CSL) or Bodily Injury Liability (Split Limits)       
  If one or all vehicles have suspended coverage then refer to underwriters - must have an underwriting tier in order to process the policy         
 */  
  
 SELECT TOP 1   
 @PRIOR_LIMIT = CASE MNT_COV.COV_ID WHEN 2   
     THEN DBO.PIECE(CAST(ISNULL(LIMIT_1,0) AS VARCHAR) + '/' + CAST(ISNULL(LIMIT_2,0) AS VARCHAR),'/',1)  
     WHEN 1   
     THEN  CAST(LIMIT_1 AS VARCHAR) END ,  
 @LIMIT_TYPE = CASE MNT_COV.COV_ID WHEN 2 THEN 'BI'  
     WHEN 1 THEN  'CSL' END   
 FROM POL_VEHICLES VEH WITH(NOLOCK)  
 LEFT JOIN  POL_VEHICLE_COVERAGES COV WITH(NOLOCK)  
 LEFT JOIN MNT_COVERAGE MNT_COV WITH(NOLOCK)  
 ON MNT_COV.COV_ID = COV.COVERAGE_CODE_ID  
 ON VEH.CUSTOMER_ID = COV.CUSTOMER_ID  
 AND VEH.POLICY_ID  = COV.POLICY_ID  
 AND VEH.POLICY_VERSION_ID = COV.POLICY_VERSION_ID   
 WHERE COV.CUSTOMER_ID = @CUSTOMER_ID   
 AND COV.POLICY_ID =@POLICY_ID   
 AND COV.POLICY_VERSION_ID = @POLICY_VERSION_ID  
 AND MNT_COV.COV_CODE IN ('BISPL','SLL')  
 --AND  APP_VEHICLE_PERTYPE_ID NOT IN (@SUSPENDED_VEHILCE)
 AND ISNULL(VEH.IS_SUSPENDED,'') != 10963 --SUSPENDED_VEHILCE --Added by Charles on 5-Jan-09 for Itrack 6830
 AND ISNULL(VEH.IS_ACTIVE,'')='Y'     
 ORDER BY VEH.VEHICLE_ID  
  
  
--------------############################# END RENEWAL ##################################3  
  
  
SELECT   
 @LAPSE_DAYS  AS LAPSE_DAYS  
,@PRIOR_LIMIT AS PRIOR_BI_LIMIT  
,@TOTAL_NAF AS TOTAL_NAF  
,@LIMIT_TYPE AS LIMIT_TYPE  
,@EFFECTIVE_DATE AS APP_EFFECTIVE_DATE  
,@STATE_ID AS STATE_ID  
  
END   
  
  



GO

