
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
          
          
/* ===========================================================================================================                                                                                                                      
Proc Name                : dbo.Proc_GetProductRule_Pol  2156,749,1                                                                                                                                                                 
Created by               : Pravesh K Chandel                                                                                                                                                                                    
Date                     : 04 May. 2010                                                                                                                                                                    
Purpose                  : To get the policy /Product info                     
Used In                  : EbixAdvantage                                    
                    
<MODIFICATION HISTORY>                                  
Modified By     : Lalit Chauhan                                   
Date   : August 18,2010                                  
Purpose   : To get billing tab premium and coverages premium sum                                  
=======================================================================                    
drop proc dbo.Proc_GetProductRule_Pol                                   
dbo.Proc_GetProductRule_Pol 2764,1059,1,1          
*/                                                      
aLTER proc [dbo].[Proc_GetProductRule_Pol]                                  
(                                                                            
 @CUSTOMER_ID    int,                                                               
 @POLICY_ID    int,                                                               
 @POLICY_VERSION_ID   int,                          
 @Lang_id  int =1                                         
 )                                                               
AS                                                
BEGIN                                      
  DECLARE @POLICY_LOB NVARCHAR(5),                                  
   @CO_INSURANCE int,@WRITTEN_PREMIUM decimal(25,2),                                  
       @END_EFF_DATE DATETIME,@END_EXP_DATE DATETIME,@PREV_VERSION INT,@PREV_PREMIUM decimal(25,2),@POLICY_STATUS NVARCHAR(50),                                  
       @PRORATE_AMOUNT decimal(25,2),@POLICY_EFF_DATE DATETIME,@POLICY_EXPI_DATE DATETIME,                                  
       @PRODUCT_TYPE INT = 0,@STATUS nvarchar(25)='',                                   
       --@MASTER_POLICY INT = 14680 ,  --Commented By Lalit,Master policy Based on selected Transaction_Type  from application page                                  
       @MASTER_POLICY INT = 14560, --(Transaction Type = Open policy)  is master policy                                  
       @SIMPLE_POLICY INT= 14681, @RISK_COUNT INT = 0,@IS_MANDATORY CHAR(1) = 'Y',                                  
  @CO_INSURANCE_LEADER INT = 14548  , @CO_INSURANCE_FOLLOWER INT =  14549,                                  
       @TRANSACTION_TYPE INT,@POLICY_EFF_DAY INT,@CO_APPLICANT_NO  INT,@REMUNERATION_CO_APPLICANT INT ,                              
       @COUNT_LEADER INT ,@SUM_LIMIT DECIMAL(25,2),@BASIC_SUM_LIMIT DECIMAL(25,2),@COUNT_FOLLOWER INT ,                          
       @COUNT_ISMAIN INT, @COUNT_REMUNERATION INT , @LEADER_COUNT INT ,@POLICY_LEVEL_COMM_APPLIES char ,                      
        @TRANS_ID INT,@IS_MAIN_APPLY SMALLINT,@POLICY_SUB_LOB INT,@VALUE_AT_RISK_APPLICABLE INT ,              
        @POLICY_DUE_DATE INT ,@ENDORSEMENT_TYPE INT ,@TOTAL_COINSURANCE_PERCENT DECIMAL(25,2),@TERMINATION_DATE INT,          
        @TOTAL_COINSURANCE_FEE DECIMAL(25,2),@COINSURANCE_FEE DECIMAL(25,2),@AGENCY_ID INT,@DELETE_BROKER_NBS INT,          
      @MAX_LAYER_AMOUNT DECIMAL(25,2),@TIV DECIMAL(25,2),@INSTALLMENT_AMOUNT DECIMAL(25,2),@IS_DISREGARD_RI_CONTRACT INT,          
  @COUNT_NUMBER INT,@DISREGARD_VALUE INT,@CESSION_PRECNT DECIMAL(18,4)          
  ,@ACTIVITY_TYPE INT,@SUBMIT_ANYWAY INT,  ---Added by Pradeep for itrack#1152 / TFS# 2598          
   @TOTAL_TIV_RI DECIMAL(25,2), @SUM_RI DECIMAL(25,2)  -- Added by aditya for tfs # 2685          
                            
                                         
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                                                 
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID )                                                                                                                                                           

  
   
       
        
          
                                 
  BEGIN          
  SELECT @INSTALLMENT_AMOUNT= SUM(INSTALLMENT_AMOUNT) FROM ACT_POLICY_INSTALLMENT_DETAILS --Added by Aditya on 30-08-2011 for TFS Bug # 478          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID           
            
 SET @DELETE_BROKER_NBS = 1  --Changes done by aditya for itrack # 1282          
           
 SELECT @TOTAL_COINSURANCE_PERCENT = isnull(SUM(COINSURANCE_PERCENT),0) from POL_CO_INSURANCE PC WITH(NOLOCK) INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON             
 PC.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PC.POLICY_ID = PCPL.POLICY_ID AND PC.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID            
 WHERE PCPL.CUSTOMER_ID = @CUSTOMER_ID AND PCPL.POLICY_ID = @POLICY_ID AND PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID                  
            
 SELECT @TOTAL_COINSURANCE_FEE = isnull(SUM(COINSURANCE_FEE),0) from POL_CO_INSURANCE PC WITH(NOLOCK) INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON             
 PC.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PC.POLICY_ID = PCPL.POLICY_ID AND PC.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID            
 AND  PCPL.CO_INSURANCE = 14548             
 WHERE PCPL.CUSTOMER_ID = @CUSTOMER_ID AND PCPL.POLICY_ID = @POLICY_ID AND PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID                                                               
                                    
 SELECT @COINSURANCE_FEE = isnull(COINSURANCE_FEE,0) from POL_CO_INSURANCE PC WITH(NOLOCK) INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON             
 PC.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PC.POLICY_ID = PCPL.POLICY_ID AND PC.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID            
 AND  PCPL.CO_INSURANCE = 14548 AND PC.LEADER_FOLLOWER = 14549            
 WHERE PCPL.CUSTOMER_ID = @CUSTOMER_ID AND PCPL.POLICY_ID = @POLICY_ID AND PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID          
                                  
 SELECT  @POLICY_LOB=POLICY_LOB,@CO_INSURANCE=CO_INSURANCE,@POLICY_STATUS= POLICY_STATUS,                                  
 @POLICY_EFF_DATE= APP_EFFECTIVE_DATE,@POLICY_EXPI_DATE = APP_EXPIRATION_DATE,                                  
 @STATUS =ISNULL(POLICY_STATUS,APP_STATUS),                                 
 @TRANSACTION_TYPE = ISNULL(TRANSACTION_TYPE,0),                        
 @POLICY_LEVEL_COMM_APPLIES = ISNULL(POLICY_LEVEL_COMM_APPLIES,'N'),                    
 @POLICY_SUB_LOB = POLICY_SUBLOB,          
 @AGENCY_ID = AGENCY_ID                    
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                                                                                                                 
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                
               
select @DISREGARD_VALUE=DISREGARD_RI_CONTRACT from POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID          
and POLICY_VERSION_ID = @POLICY_VERSION_ID          
          
select @COUNT_NUMBER = COUNT(*) from POL_REINSURANCE_INFO          
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID          
          
if(@DISREGARD_VALUE = 10963 and @COUNT_NUMBER <1)           
set @IS_DISREGARD_RI_CONTRACT = 0          
else          
set @IS_DISREGARD_RI_CONTRACT = 1           
           
               
 SET @POLICY_DUE_DATE = 0              
 IF EXISTS(select * from ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID               
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND              
 INSTALLMENT_EFFECTIVE_DATE > @POLICY_EXPI_DATE)              
 BEGIN                    
 SET @POLICY_DUE_DATE =1                    
 END            
           
 SET @TERMINATION_DATE = 0           
 IF EXISTS(SELECT AGENCY_ID FROM  MNT_AGENCY_LIST MAL WITH(NOLOCK) WHERE          
 MAL.AGENCY_ID = @AGENCY_ID  AND  MAL.TERMINATION_DATE < @POLICY_EFF_DATE)          
 BEGIN           
 SET @TERMINATION_DATE =1                      
 END             
                                       
                               
 SELECT  @SUM_LIMIT=ISNULL(SUM(LIMIT_1),0)                                
 FROM POL_PRODUCT_COVERAGES PPC  WITH(NOLOCK)                              
 INNER JOIN MNT_COVERAGE MC ON                          
 MC.COV_ID = PPC.COVERAGE_CODE_ID                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID and                           
 POLICY_ID=@POLICY_ID                           
 and POLICY_VERSION_ID=@POLICY_VERSION_ID                           
 and MC.REINSURANCE_LOB = '10963'                          
 --table 0 POlicy Table                                   
                               
SELECT @BASIC_SUM_LIMIT = ISNULL(MAX(LIMIT_1),0)                                 
FROM POL_PRODUCT_COVERAGES WITH(NOLOCK)                           
JOIN MNT_COVERAGE  WITH(NOLOCK) ON                                
POL_PRODUCT_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                 
WHERE CUSTOMER_ID=@CUSTOMER_ID                           
and POLICY_ID=@POLICY_ID                           
and POLICY_VERSION_ID=@POLICY_VERSION_ID                                
AND MNT_COVERAGE.IS_MAIN = 1                          
                          
SELECT @COUNT_ISMAIN = ISNULL(COUNT(IS_MAIN),0)                                 
FROM POL_PRODUCT_COVERAGES WITH(NOLOCK)                           
JOIN MNT_COVERAGE  WITH(NOLOCK) ON                                
POL_PRODUCT_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                 
WHERE CUSTOMER_ID=@CUSTOMER_ID                           
and POLICY_ID=@POLICY_ID                           
and POLICY_VERSION_ID=@POLICY_VERSION_ID                                
AND MNT_COVERAGE.IS_MAIN = 1                          
                          
--SELECT @COUNT_REMUNERATION = ISNULL(SUM(COMMISSION_PERCENT),0)                          
--FROM POL_REMUNERATION WITH(NOLOCK)                          
--WHERE  CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND                                   
--POLICY_VERSION_ID=@POLICY_VERSION_ID GROUP BY COMMISSION_TYPE                          
                    
--Added by Lalit March 02,2011                    
SET @IS_MAIN_APPLY= 0                    
IF EXISTS(SELECT * FROM MNT_COVERAGE WITH(NOLOCK) WHERE LOB_ID=@POLICY_LOB AND SUB_LOB_ID=@POLICY_SUB_LOB                    
    AND IS_MAIN=1 AND   ISNULL(MNT_COVERAGE.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') >= @POLICY_EFF_DATE)                    
BEGIN                    
 SET @IS_MAIN_APPLY =1                    
END                    
                    
SET @VALUE_AT_RISK_APPLICABLE= 0  
IF(@POLICY_LOB IN(9,10,11,14,19))                     
BEGIN                    
 SET @VALUE_AT_RISK_APPLICABLE =1                    
END                    
                    
                          
SELECT @LEADER_COUNT = ISNULL(COUNT(*),0) FROM POL_REMUNERATION                              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                               
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND LEADER = 10963                           
                               
                                   
SELECT  @POLICY_EFF_DAY = DATEDIFF(DAY,@POLICY_EFF_DATE,@POLICY_EXPI_DATE)              
                                  
 SELECT @PRODUCT_TYPE = PRODUCT_TYPE FROM MNT_LOB_MASTER WHERE LOB_ID=@POLICY_LOB                                  
                                   
 IF(UPPER(@STATUS)='UENDRS' AND (@PRODUCT_TYPE =@MASTER_POLICY OR @CO_INSURANCE = @CO_INSURANCE_FOLLOWER))                                   
    SELECT @IS_MANDATORY = 'Y'                             
 ELSE  IF(UPPER(@STATUS)='UENDRS' AND (@PRODUCT_TYPE <> @MASTER_POLICY AND @CO_INSURANCE <> @CO_INSURANCE_FOLLOWER))                                   
   SELECT @IS_MANDATORY = 'N'               
             
   --Added by Lalit .itrack # 1329.get Layer Amount and  TIV          
   select @MAX_LAYER_AMOUNT =  MAX(CAST(ISNULL(A.LAYER_AMOUNT,0) AS Decimal)) from MNT_REIN_LOSSLAYER A JOIN --modified by Lalit August 3,2011 . itrack # 1329          
   MNT_REIN_CONTRACT_LOB B ON           
   A.CONTRACT_ID = B.CONTRACT_ID           
  JOIN           
   POL_CUSTOMER_POLICY_LIST C ON           
   B.CONTRACT_LOB = C.POLICY_LOB           
  JOIN          
   MNT_REINSURANCE_CONTRACT D ON           
   D.CONTRACT_ID = B.CONTRACT_ID          
  WHERE C.CUSTOMER_ID = @CUSTOMER_ID           
   AND C.POLICY_ID = @POLICY_ID  AND           
   C.POLICY_VERSION_ID =  @POLICY_VERSION_ID          
             
          
--Added by Pradeep for itrack#1152 / TFS# 2598          
 IF(@POLICY_LOB IN(10,11,14,25,34))          
 BEGIN          
  SELECT @ACTIVITY_TYPE = ACTIVITY_TYPE           
  FROM POL_PRODUCT_LOCATION_INFO NOLOCK           
  WHERE  CUSTOMER_ID = @CUSTOMER_ID  AND           
   POLICY_ID = @POLICY_ID   AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID          
 END          
 ELSE IF(@POLICY_LOB IN(9,26))          
 BEGIN           
  SELECT @ACTIVITY_TYPE = ACTIVITY_TYPE           
  FROM POL_PERILS NOLOCK           
  WHERE  CUSTOMER_ID = @CUSTOMER_ID  AND           
   POLICY_ID = @POLICY_ID   AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID          
 END             
 ELSE           
 BEGIN           
 --Added by Pradeep for itrack#1152 / TFS# 2598          
  SELECT @ACTIVITY_TYPE = ACTIVITY_TYPE           
  FROM POL_LOCATIONS NOLOCK           
  WHERE  CUSTOMER_ID = @CUSTOMER_ID  AND           
   POLICY_ID = @POLICY_ID   AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID          
 END          
 SELECT @SUBMIT_ANYWAY=SUBMIT_ANYWAY FROM MNT_ACTIVITY_MASTER WITH(NOLOCK) WHERE ACTIVITY_ID=@ACTIVITY_TYPE          
           
 --Added till here           
            
  SELECT @TIV =  SUM(ISNULL(LIMIT_1,0)) FROM POL_PRODUCT_COVERAGES --modified by Lalit August 3,2011 . itrack # 1329          
  JOIN MNT_COVERAGE B ON           
  B.COV_ID = POL_PRODUCT_COVERAGES.COVERAGE_CODE_ID          
  WHERE CUSTOMER_ID  = @CUSTOMER_ID           
  AND POLICY_ID = @POLICY_ID  AND POLICY_VERSION_ID = @POLICY_VERSION_ID   --Commented by Aditya For itrack # 1532,this msg has been shifted to confirmation box          
          
SELECT  @TOTAL_TIV_RI = SUM(ISNULL(LIMIT_1,0)) FROM POL_PRODUCT_COVERAGES WITH(NOLOCK)  -- Added by aditya for tfs # 2685          
  JOIN MNT_COVERAGE B WITH(NOLOCK) ON               
  B.COV_ID = POL_PRODUCT_COVERAGES.COVERAGE_CODE_ID              
  WHERE CUSTOMER_ID  = @CUSTOMER_ID               
  AND POLICY_ID = @POLICY_ID  AND POLICY_VERSION_ID = @POLICY_VERSION_ID          
  and B.REINSURANCE_LOB = '10963'            
           
  SELECT @SUM_RI = (A.LAYER_AMOUNT*ISNULL(REINSURANCE_CEDED,0))/100 + (ISNULL(D.RETENTION_AMOUNT,0) + ISNULL(D.REIN_CEDED,0))            
  FROM POL_REINSURANCE_INFO A WITH(NOLOCK)          
  JOIN POL_CUSTOMER_POLICY_LIST B WITH(NOLOCK) ON           
  A.CUSTOMER_ID = B.CUSTOMER_ID AND A.POLICY_ID = B.POLICY_ID AND A.POLICY_VERSION_ID = B.POLICY_VERSION_ID          
  JOIN MNT_REIN_CONTRACT_LOB C WITH(NOLOCK) ON  B.POLICY_LOB = C.CONTRACT_LOB           
  JOIN MNT_REINSURANCE_CONTRACT E WITH(NOLOCK) ON         
  E.CONTRACT_ID = C.CONTRACT_ID AND         
  ISNULL(B.POLICY_EFFECTIVE_DATE,B.APP_EFFECTIVE_DATE) <= ISNULL(E.TERMINATION_DATE,'3000-01-01')              
  AND IsNULL(B.POLICY_EFFECTIVE_DATE,B.APP_EFFECTIVE_DATE) >= ISNULL(E.effective_date,'3000-01-01')           
  AND ISNULL(DISREGARD_RI_CONTRACT,10964) =  10964          
  JOIN MNT_REIN_LOSSLAYER D WITH(NOLOCK) ON  D.CONTRACT_ID = C.CONTRACT_ID            
  WHERE B.CUSTOMER_ID = @CUSTOMER_ID  AND B.POLICY_ID = @POLICY_ID AND B.POLICY_VERSION_ID = @POLICY_VERSION_ID  --Added till here          
                                
                                   
 SELECT  [APPLICATION].CUSTOMER_ID,[APPLICATION].POLICY_ID,[APPLICATION].POLICY_VERSION_ID,                                  
 CASE WHEN ISNULL(POLICY_STATUS,'')='' THEN '0' ELSE '1' END AS CALLED_FROM,                                    
 APP_ID,APP_VERSION_ID,POLICY_TYPE,ISNULL(case when POLICY_NUMBER='To be generated' and @Lang_id =2 then 'Para ser gerada' else POLICY_NUMBER end,'')POLICY_NUMBER,                                     
   CASE WHEN ISNULL(POLICY_STATUS,'')='' THEN APP_NUMBER ELSE ISNULL(case when POLICY_NUMBER='To be generated' and @Lang_id =2 then 'Para ser gerada' else POLICY_NUMBER end,POLICY_NUMBER) END AS APP_NO,POLICY_DISP_VERSION AS APP_VERSION_NO,              
 
 
     
     
        
   POLICY_DISP_VERSION,ISNULL(POLICY_STATUS,APP_STATUS)  AS POLICY_STATUS,                                  
   POLICY_LOB,POLICY_SUBLOB,ISNULL(CSR,'')as CSR,ISNULL(UNDERWRITER,'0')as UNDERWRITER ,[APPLICATION].IS_ACTIVE,ISNULL(AGENCY_ID,'0')as AGENCY_ID ,APP_STATUS,APP_NUMBER,APP_VERSION,APP_TERMS,                                    
   ISNULL(APP_INCEPTION_DATE,'')as APP_INCEPTION_DATE ,ISNULL(APP_EFFECTIVE_DATE,'')as APP_EFFECTIVE_DATE ,ISNULL(APP_EXPIRATION_DATE,'')as APP_EXPIRATION_DATE ,[APPLICATION].STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,--COMPLETE_APP,                         

  
    
      
        
         
   INSTALL_PLAN_ID,            
   @TERMINATION_DATE as TERMINATION_DATE,                             
   RECEIVED_PRMIUM,--SHOW_QUOTE,                                  
   POLICY_TERMS,--POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE,                                    
   BILL_TYPE_ID,--POL_VER_EFFECTIVE_DATE,POL_VER_EXPIRATION_DATE,                                    
   PRODUCER,DOWN_PAY_MODE,---CURRENT_TERM,                                  
   --REFER_UNDERWRITER,REFERAL_INSTRUCTIONS,IS_REWRITE_POLICY,                                  
   ISNULL(POLICY_CURRENCY,'')as POLICY_CURRENCY,POLICY_LEVEL_COMISSION ,ISNULL(POLICY_LEVEL_COMM_APPLIES,'N')as POLICY_LEVEL_COMM_APPLIES,                                    
   ISNULL(BILLTO,'')as BILLTO,ISNULL(PAYOR,'0')as PAYOR,ISNULL(CO_INSURANCE,'')as CO_INSURANCE ,CONTACT_PERSON,TRANSACTION_TYPE,                                  
   PREFERENCE_DAY,BROKER_REQUEST_NO, ISNULL(COAPPLICANT.IS_PRIMARY_APPLICANT,0) AS IS_PRIMARY_APPLICANT, ISNULL(CUSTOMER_TYPE,'')as CUSTOMER_TYPE1,                                   
   ISNULL(CUSTOMER_HOME_PHONE,'')CUSTOMER_HOME_PHONE, POLICY_LOB as LOB_ID,CASE WHEN   CUSTOMER_ZIP = '' THEN '0' ELSE ISNULL(CUSTOMER_ZIP,'0') END CUSTOMER_ZIP,                                  
   CASE WHEN   CUSTOMER_ADDRESS1 = '' THEN '0' ELSE ISNULL(CUSTOMER_ADDRESS1,'0') END CUSTOMER_ADDRESS1,                    
   CASE WHEN   CUSTOMER_CITY = '' THEN '0' ELSE ISNULL(CUSTOMER_CITY,'0') END CUSTOMER_CITY,                    
   CASE WHEN   CUSTOMER_STATE = '' THEN '0' ELSE ISNULL(CUSTOMER_STATE,'0') END CUSTOMER_STATE,                      
   CASE WHEN   CUSTOMER.NUMBER = '' THEN '0' ELSE ISNULL(CUSTOMER.NUMBER,'0') END NUMBER,                    
   CASE WHEN   CUSTOMER.DISTRICT = '' THEN '0' ELSE ISNULL(CUSTOMER.DISTRICT,'0') END CUSTOMER_DISTRICT,                       
   CASE WHEN  convert(VARCHAR(100),CUSTOMER.REG_ID_ISSUE) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),CUSTOMER.REG_ID_ISSUE ),'0') END REG_ID_ISSUE,                      
   CASE WHEN   CUSTOMER.ORIGINAL_ISSUE = '' THEN '0' ELSE ISNULL(CUSTOMER.ORIGINAL_ISSUE,'0') END ORIGINAL_ISSUE,                      
   CASE WHEN   CUSTOMER.REGIONAL_IDENTIFICATION = '' THEN '0' ELSE ISNULL(CUSTOMER.REGIONAL_IDENTIFICATION,'0') END REGIONAL_IDENTIFICATION,                      
   CASE WHEN   MARITAL_STATUS = '' THEN '0' ELSE ISNULL(MARITAL_STATUS,'0') END MARITAL_STATUS,               
   CASE WHEN   CUSTOMER.GENDER = '' THEN '0' ELSE ISNULL(CUSTOMER.GENDER,'0') END GENDER,                         
   CASE WHEN  convert(VARCHAR(100),DATE_OF_BIRTH  ) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),DATE_OF_BIRTH  ),'0') END DATE_OF_BIRTH,                    
   CASE WHEN  convert(VARCHAR(100),DATE_OF_BIRTH  ) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),DATE_OF_BIRTH  ),'0') END CREATION_DATE,                      
   CASE WHEN   CAL.ACCOUNT_TYPE = '' THEN '0' ELSE ISNULL(CAL.ACCOUNT_TYPE,'0') END ACCOUNT_TYPE,                       
   --CASE WHEN   CAL.ZIP_CODE = '' THEN '0' ELSE ISNULL(CAL.ZIP_CODE,'0') END ZIP_CODE,                     
   --CASE WHEN   CAL.ADDRESS1 = '' THEN '0' ELSE ISNULL(CAL.ADDRESS1,'0') END ADDRESS1,                      
   --CASE WHEN   CAL.CITY = '' THEN '0' ELSE ISNULL(CAL.CITY,'0') END CITY,                    
   --CASE WHEN   CAL.STATE = '' THEN '0' ELSE ISNULL(CAL.STATE,'0') END STATE,                    
   --CASE WHEN   CAL.COUNTRY = '' THEN '0' ELSE ISNULL(CAL.COUNTRY,'0') END COUNTRY,                    
   --CASE WHEN   CAL.NUMBER = '' THEN '0' ELSE ISNULL(CAL.NUMBER,'0') END CO_APP_NUMBER,                
   --CASE WHEN   CAL.DISTRICT = '' THEN '0' ELSE ISNULL(CAL.DISTRICT,'0') END CO_APP_DISTRICT,                   
   --CASE WHEN   CAL.CO_APPL_GENDER = '' THEN '0' ELSE ISNULL(CAL.CO_APPL_GENDER,'0') END CO_APPL_GENDER,                  
   --CASE WHEN  convert(VARCHAR(100),CAL.REG_ID_ISSUE) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),CAL.REG_ID_ISSUE ),'0') END CO_APP_REG_ID_ISSUE,                      
   --CASE WHEN   CAL.ORIGINAL_ISSUE = '' THEN '0' ELSE ISNULL(CAL.ORIGINAL_ISSUE,'0') END CO_APP_ORIGINAL_ISSUE,                    
   --CASE WHEN   CAL.REGIONAL_IDENTIFICATION = '' THEN '0' ELSE ISNULL(CAL.REGIONAL_IDENTIFICATION,'0') END CO_APP_REGIONAL_IDENTIFICATION,                    
   --CASE WHEN   CAL.CO_APPL_MARITAL_STATUS = '' THEN '0' ELSE ISNULL(CAL.CO_APPL_MARITAL_STATUS,'0') END CO_APPL_MARITAL_STATUS,                    
   --CASE WHEN  convert(VARCHAR(100),CAL.CO_APPL_DOB ) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),CAL.CO_APPL_DOB  ),'0') END CO_APPL_DOB,                     
   --CASE WHEN  convert(VARCHAR(100),CAL.CO_APPL_DOB ) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),CAL.CO_APPL_DOB  ),'0') END CO_APPL_CREATION_DATE,                     
   --CASE WHEN   CAL.ACCOUNT_TYPE = '' THEN '0' ELSE ISNULL(CAL.ACCOUNT_TYPE,'0') END CO_APP_ACCOUNT_TYPE,                    
   ISNULL(CUSTOMER.CUSTOMER_ADDRESS1,'') +case when CUSTOMER.CUSTOMER_ADDRESS1!='' THEN CASE WHEN CUSTOMER.NUMBER!='' THEN ', ' ELSE '' END ELSE ''END          
   + ISNULL(' ' +CUSTOMER.NUMBER,'')+  ISNULL(' ' + CUSTOMER.CUSTOMER_ADDRESS2,'')            
   +CASE WHEN CUSTOMER.DISTRICT!='' THEN ' - ' ELSE ' ' END +ISNULL(CUSTOMER.DISTRICT,'')          
   +CASE WHEN CUSTOMER.CUSTOMER_CITY!='' THEN ' - ' ELSE ' ' END+ISNULL(CUSTOMER.CUSTOMER_CITY,'')          
   +CASE WHEN MNT_COUNTRY_STATE_LIST.STATE_CODE!='' THEN CASE WHEN CUSTOMER.CUSTOMER_CITY!='' THEN '/' ELSE ' - ' END ELSE '' END          
   +ISNULL(MNT_COUNTRY_STATE_LIST.STATE_CODE,'')+CASE WHEN CUSTOMER.CUSTOMER_ZIP!='' THEN CASE WHEN MNT_COUNTRY_STATE_LIST.STATE_CODE!='' THEN ' - ' ELSE ' ' END ELSE '' END             
   +ISNULL(CUSTOMER.CUSTOMER_ZIP,'')  AS [ADDRESS],          
   CUSTOMER.CUSTOMER_FIRST_NAME +ISNULL(' '+ CUSTOMER.CUSTOMER_MIDDLE_NAME,'')+             
   ISNULL(' ' + CUSTOMER.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,                                    
    case when lang_id=2 then MLV.LOOKUP_VALUE_DESC else isnull(CUSTOMER_TYPE.LOOKUP_VALUE_DESC,'') end CUSTOMER_TYPE  ,                                  
   @PRODUCT_TYPE as PRODUCT_TYPE,@IS_DISREGARD_RI_CONTRACT AS IS_DISREGARD_RI_CONTRACT --Added by Aditya for tfs # 180,Moved to policy level                                
                                       
 FROM  POL_CUSTOMER_POLICY_LIST [APPLICATION] WITH(NOLOCK)                                     
 INNER JOIN CLT_CUSTOMER_LIST CUSTOMER WITH(NOLOCK)  ON CUSTOMER.CUSTOMER_ID=[APPLICATION].CUSTOMER_ID                     
 LEFT JOIN POL_APPLICANT_LIST COAPPLICANT WITH(NOLOCK)  ON COAPPLICANT.CUSTOMER_ID=[APPLICATION].CUSTOMER_ID                                       
 LEFT JOIN CLT_APPLICANT_LIST CAL  WITH(NOLOCK)  ON CAL.CUSTOMER_ID=COAPPLICANT.CUSTOMER_ID  AND  COAPPLICANT.APPLICANT_ID=CAL.APPLICANT_ID                                       
 AND COAPPLICANT.POLICY_ID=[APPLICATION].POLICY_ID AND COAPPLICANT.POLICY_VERSION_ID =[APPLICATION].POLICY_VERSION_ID                                      
 AND COAPPLICANT.IS_PRIMARY_APPLICANT=1                                     
 LEFT OUTER JOIN MNT_LOOKUP_VALUES CUSTOMER_TYPE WITH(NOLOCK)  ON CUSTOMER.CUSTOMER_TYPE=CUSTOMER_TYPE.LOOKUP_UNIQUE_ID                           
  LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLV WITH(NOLOCK)  ON CUSTOMER_TYPE.LOOKUP_UNIQUE_ID =MLV.LOOKUP_UNIQUE_ID and LANG_ID=@Lang_id                                             
  left outer join MNT_COUNTRY_STATE_LIST on MNT_COUNTRY_STATE_LIST.STATE_ID =CUSTOMER.CUSTOMER_STATE                
 WHERE [APPLICATION].CUSTOMER_ID=@CUSTOMER_ID and [APPLICATION].POLICY_ID=@POLICY_ID and [APPLICATION].POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                
 FOR XML AUTO,ELEMENTS, Root('APPLICATIONS')                      
                 
                 
                 
                           
 --table 1 Location Table                                    
                                   
 -----Count Co-Applicants On pol_APPLICANT_LIST                                  
 --if master policy multiple co-applicant can use in remuneration ,so we need check no of co-applicant added on co-applicant tab and                                   
 --No of co-applicant considered on remuneration. IF normal policy there is no need for check no of co-applicant so we set count                                  
 --of applicant as 1                                   
 SELECT @CO_APPLICANT_NO = CASE  WHEN @TRANSACTION_TYPE = @MASTER_POLICY THEN ISNULL(COUNT(APPLICANT_ID),1)                         
         ELSE 1 END                                   
  FROM POL_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID                                   
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                   
                                   
                                   
 --IF(ISNULL(@PRODUCT_TYPE,@SIMPLE_POLICY) = @MASTER_POLICY AND (UPPER(@STATUS) = 'APPLICATION' OR UPPER(@STATUS) = 'UISSUE'))                                  
 IF ((@TRANSACTION_TYPE = @MASTER_POLICY OR @CO_INSURANCE = @CO_INSURANCE_FOLLOWER) AND (UPPER(@STATUS) = 'APPLICATION' OR UPPER(@STATUS) = 'UISSUE'))                                  
  BEGIN                
  --Added by Pradeep for itrack#1152 / TFS# 2598 for moster policy                            
  SELECT @ACTIVITY_TYPE = ACTIVITY_TYPE           
  FROM POL_LOCATIONS NOLOCK           
  WHERE  CUSTOMER_ID = @CUSTOMER_ID  AND           
   POLICY_ID = @POLICY_ID   AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID          
 SELECT @SUBMIT_ANYWAY=SUBMIT_ANYWAY FROM MNT_ACTIVITY_MASTER WITH(NOLOCK) WHERE ACTIVITY_ID=@ACTIVITY_TYPE          
           
    SELECT @PRODUCT_TYPE AS PRODUCT_TYPE ,'N'  as IS_MANDATORY, @SUBMIT_ANYWAY AS SUBMIT_ANYWAY --Added by Pradeep for itrack#1152 / TFS# 2598                    
                     
    FROM POL_CUSTOMER_POLICY_LIST LOCATION WITH(NOLOCK)                                   
    WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
    FOR XML AUTO,ELEMENTS ,Root('LOCATIONS')                                                                             
                                    
 END                                  
ELSE                                  
 BEGIN                
          
                      
   SELECT @IS_MANDATORY AS IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,LOCATION_ID,LOC_NUM,IS_PRIMARY,LOC_ADD1,LOC_ADD2,LOC_CITY,LOC_COUNTY,LOC_STATE,                                    
     LOC_ZIP,LOC_COUNTRY,PHONE_NUMBER,FAX_NUMBER,DEDUCTIBLE,NAMED_PERILL,[DESCRIPTION],LOCATION.IS_ACTIVE,                                    
     LOC_TERRITORY,LOCATION_TYPE,RENTED_WEEKLY,WEEKS_RENTED,                                   
     CAL_NUM,NAME,NUMBER,DISTRICT,OCCUPIED,EXT,CATEGORY,ACTIVITY_TYPE,CONSTRUCTION,SOURCE_LOCATION_ID,          
     ISNULL(@SUBMIT_ANYWAY,0) AS SUBMIT_ANYWAY --Added by Pradeep for itrack#1152 / TFS# 2598                                   
     FROM POL_LOCATIONS LOCATION WITH(NOLOCK)                                   
     WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID               
     FOR XML AUTO,ELEMENTS ,Root('LOCATIONS')                                                                             
                                    
 END                                 
                               
                               
 ---get leader Count                              
                               
 SELECT @COUNT_LEADER = ISNULL(COUNT(*),0) FROM POL_CO_INSURANCE                              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                               
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND LEADER_FOLLOWER = 14548                              
                           
 ---get follower count                          
                           
 SELECT @COUNT_FOLLOWER = ISNULL(COUNT(*),0) FROM POL_CO_INSURANCE                              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND                               
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND LEADER_FOLLOWER = 14549                               
                         
                
                        
                       
                       
  SELECT COINSURANCE.COINSURANCE_ID,COINSURANCE.COMPANY_ID,COINSURANCE.CO_INSURER_NAME,COINSURANCE.LEADER_FOLLOWER,isnull(COINSURANCE.COINSURANCE_PERCENT,'0.00') as COINSURANCE_PERCENT,                                 
 @CO_INSURANCE as APP_CO_INSURANCE, ISNULL(@COUNT_LEADER,0) AS LEADER_COUNT, ISNULL(@COUNT_FOLLOWER,0) AS FOLLOWER_COUNT,                                
 COINSURANCE.COINSURANCE_FEE,COINSURANCE.BROKER_COMMISSION,isnull(COINSURANCE.TRANSACTION_ID,'')as TRANSACTION_ID,isnull(COINSURANCE.LEADER_POLICY_NUMBER,'')as LEADER_POLICY_NUMBER,COINSURANCE.IS_ACTIVE  ,                                    
 @CO_INSURANCE as CO_INSURANCE,           
 ISNULL(@TOTAL_COINSURANCE_PERCENT,0) AS TOTAL_COINSURANCE_PERCENT,            
 ISNULL(@TOTAL_COINSURANCE_FEE,0) AS  TOTAL_COINSURANCE_FEE,          
 ISNULL(@COINSURANCE_FEE,0) AS  COINSURANCE_FEE_FOLLOWER,                 
 ISNULL(DUPLICATE,0) AS DUPLICATE,                      
 REIN_COMAPANY_NAME                            
                       
 FROM  POL_CO_INSURANCE COINSURANCE WITH(NOLOCK)                       
 left outer join MNT_REIN_COMAPANY_LIST WITH(NOLOCK) on COINSURANCE.COMPANY_ID=MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_ID                               
 LEFT OUTER JOIN(                      
 SELECT *,1 AS DUPLICATE FROM POL_CO_INSURANCE PCI WHERE CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and TRANSACTION_ID           
IN (SELECT DISTINCT ISNULL(TRANSACTION_ID,'') TRANSACTION_ID FROM POL_CO_INSURANCE PCI                         
 WITH(NOLOCK) LEFT OUTER JOIN                         
 POL_POLICY_PROCESS  PPP WITH(NOLOCK) ON                         
 PCI.CUSTOMER_ID=PPP.CUSTOMER_ID AND                        
  PCI.POLICY_ID=PPP.POLICY_ID                          
    AND PCI.POLICY_VERSION_ID=PPP.NEW_POLICY_VERSION_ID                         
 AND PPP.PROCESS_STATUS='COMPLETE'                         
 AND PROCESS_ID=25 WHERE PCI.CUSTOMER_ID<>@CUSTOMER_ID AND PCI.POLICY_ID<>@POLICY_ID                      
 AND  PCI.POLICY_VERSION_ID<>@POLICY_VERSION_ID and TRANSACTION_ID<>'' and  TRANSACTION_ID is not null                     
 )) A                      
 On A.COMPANY_ID = MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_ID                      
 WHERE COINSURANCE.CUSTOMER_ID=@CUSTOMER_ID and COINSURANCE.POLICY_ID=@POLICY_ID and COINSURANCE.POLICY_VERSION_ID=@POLICY_VERSION_ID                      
                      
                       
                                                                               
 FOR XML AUTO,ELEMENTS,Root('COINSURANCES')                                    
                       
                       
 --table 3 Remunaration Table                                    
 SELECT  @REMUNERATION_CO_APPLICANT = ISNULL(COUNT(DISTINCT CO_APPLICANT_ID),1) FROM  POL_REMUNERATION REMUNERATION WITH(NOLOCK)                                   
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID= @POLICY_VERSION_ID                                  
                            
        
 SELECT ISNULL(SUM(REMUNERATION.COMMISSION_PERCENT),0) COMMISSION_PERCENT,COMMISSION_TYPE,                    
 ISNULL(a.APPLICANT_ID,0) as CO_APPLICANT_ID,             
@CO_INSURANCE as CO_INSURANCE   ,  -- for itrack no 1391             
                          
 @REMUNERATION_CO_APPLICANT AS CO_APPLICANT ,           
                           
 @CO_APPLICANT_NO AS NO_CO_APPLICANTS, --@LEADER_COUNT AS COUNT_LEADER ,            
 CASE WHEN (          
SELECT LEADER FROM POL_REMUNERATION PR with(nolock)  WHERE PR.CUSTOMER_ID  = @CUSTOMER_ID and PR.POLICY_ID = @POLICY_ID          
and PR.POLICY_VERSION_ID  = @POLICY_VERSION_ID and PR.LEADER  = 10963 and PR.CO_APPLICANT_ID = a.APPLICANT_ID          
)=10963          
THEN 1 ELSE 0 END COUNT_LEADER ,                   
 CASE WHEN (@TRANSACTION_TYPE = 14560 and @CO_INSURANCE <> 14549)THEN 'Y' ELSE   --for itrack no 1391                       
 @POLICY_LEVEL_COMM_APPLIES END AS POLICY_LEVEL_COMM_APPLIES   ,            
 @TRANSACTION_TYPE AS TRANSACTION_TYPE ,           
  (IsNull(FIRST_NAME,''))  CO_APPLICANT_NAME,        -- Changed by Aditya For itrack # 1282            
 isnull(sum(a.COMMISSION_PERCENT),0)POL_COMMISSION_PERCENT,                    
 isnull(sum(a.FEES_PERCENT),0)POL_FEES_PERCENT,                    
 isnull(sum(a.PRO_LABORE_PERCENT),0)POL_PRO_LABORE_PERCENT,                  
 CASE WHEN isnull(sum(a.COMMISSION_PERCENT),0) >= 0                     
  THEN                     
     ISNULL( (SELECT ISNULL(sum(COMMISSION_PERCENT),0) FROM POL_REMUNERATION R WITH(NOLOCK)WHERE R.CUSTOMER_ID = @CUSTOMER_ID                     
      and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =@POLICY_VERSION_ID and COMMISSION_TYPE = 43               
      and @TRANSACTION_TYPE = 14560  and R.CO_APPLICANT_ID = isnull(a.APPLICANT_ID,0)),0)                    
END   REMU_COMMISSION_PERCENT ,                    
CASE WHEN isnull(sum(a.FEES_PERCENT),0) >= 0                     
  THEN                     
     ISNULL( (SELECT ISNULL(sum(COMMISSION_PERCENT),0) FROM POL_REMUNERATION R WITH(NOLOCK) WHERE R.CUSTOMER_ID = @CUSTOMER_ID                     
      and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =@POLICY_VERSION_ID and COMMISSION_TYPE = 44               
      and @TRANSACTION_TYPE = 14560 and R.CO_APPLICANT_ID = isnull(a.APPLICANT_ID,0)),0)                    
END   REMU_FEES_PERCENT,                    CASE WHEN isnull(sum(a.PRO_LABORE_PERCENT),0) >= 0                     
  THEN                     
     ISNULL( (SELECT ISNULL(sum(COMMISSION_PERCENT),0) FROM POL_REMUNERATION R WITH(NOLOCK) WHERE R.CUSTOMER_ID = @CUSTOMER_ID                     
      and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =@POLICY_VERSION_ID and COMMISSION_TYPE = 45               
      and @TRANSACTION_TYPE = 14560 and R.CO_APPLICANT_ID = isnull(a.APPLICANT_ID,0)),0)                    
END REMU_PRO_LABORE_PERCENT                    
 from POL_APPLICANT_LIST a WITH(NOLOCK)                    
                           
 left outer JOIN                    
  POL_REMUNERATION REMUNERATION with(nolock)                    
     on  a.APPLICANT_ID=REMUNERATION.CO_APPLICANT_ID and a.CUSTOMER_ID=REMUNERATION.CUSTOMER_ID and a.POLICY_ID=REMUNERATION.POLICY_ID and a.POLICY_VERSION_ID=REMUNERATION.POLICY_VERSION_ID                     
      left outer join CLT_APPLICANT_LIST CAL with(nolock) on a.APPLICANT_ID=CAL.APPLICANT_ID                    
  WHERE REMUNERATION.CUSTOMER_ID=@CUSTOMER_ID AND REMUNERATION.POLICY_ID=@POLICY_ID AND REMUNERATION.POLICY_VERSION_ID=@POLICY_VERSION_ID                     
GROUP BY a.APPLICANT_ID,COMMISSION_TYPE ,FIRST_NAME               
order by a.APPLICANT_ID                        
 FOR XML AUTO,ELEMENTS,ROOT('REMUNERATIONS')                     
                                 
 --SELECT REMUNERATION_ID,BROKER_ID,isnull(COMMISSION_PERCENT,0.0000)as COMMISSION_PERCENT,                                  
 --  @REMUNERATION_CO_APPLICANT as CO_APPLICANT,                                  
 --  @CO_APPLICANT_NO AS NO_CO_APPLICANTS,                                  
 --COMMISSION_TYPE,REMUNERATION.IS_ACTIVE,BRANCH,isnull(AMOUNT,0.00)as AMOUNT,AGENCY_DISPLAY_NAME ,@COUNT_REMUNERATION AS COUNT_REMUNERATION               
 --FROM  POL_REMUNERATION REMUNERATION WITH(NOLOCK)                                   
 --left outer join MNT_AGENCY_LIST WITH(NOLOCK)                                    
 --on BROKER_ID=AGENCY_ID                                                                                                                   
 --WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and                                   
 --POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                
 --FOR XML AUTO,ELEMENTS,Root('REMUNERATIONS')                                    
 --table 4 Clause Table                                    
 SELECT POL_CLAUSE_ID,CLAUSE_ID,CLAUSE_TITLE,CLAUSE_DESCRIPTION,IS_ACTIVE,SUSEP_LOB_ID                                    
 FROM  POL_CLAUSES CLAUSE WITH(NOLOCK)                                                                                                                                                  
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
                                                                       
 FOR XML AUTO,ELEMENTS,Root('CLAUSES')                                    
 --table 5 Discount/Surcharge Table                                    
 SELECT RISK_ID,DISCOUNT_ROW_ID,DISCOUNT_ID,PERCENTAGE,DISCOUNT.IS_ACTIVE,CUSTOMER_FIRST_NAME                                    
 FROM  POL_DISCOUNT_SURCHARGE DISCOUNT WITH(NOLOCK) left outer join CLT_CUSTOMER_LIST WITH(NOLOCK)on  CLT_CUSTOMER_LIST.CUSTOMER_ID=DISCOUNT.CUSTOMER_ID                                       
                     
                        
                          
                             
                              
                                
                                       
 WHERE DISCOUNT.CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                
 FOR XML AUTO,ELEMENTS,Root('DISCOUNTS')                         
 --table 6 Reinsurance Table           
 EXEC Proc_GET_POLICY_RICOMMISSION_PRECENTAGE @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@CESSION_PRECNT OUT                                    
 SELECT REINSURANCE_ID,COMPANY_ID,CONTRACT_FACULTATIVE,CONTRACT ,                                   
   REINSURANCE_CEDED,REINSURANCE_COMMISSION,IS_ACTIVE,ISNULL(@CESSION_PRECNT,0) AS CESSION_PRECNT,50.00 as REINSURANCE_CEDED_SUM,@IS_DISREGARD_RI_CONTRACT AS IS_DISREGARD_RI_CONTRACT                               
FROM  POL_REINSURANCE_INFO REINSURANCE WITH(NOLOCK)                       
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                
 FOR XML AUTO,ELEMENTS,Root('REINSURANCES')                                    
 --------Product Info                                    
 --table 7 RISK Table                                     
  --IF(ISNULL(@PRODUCT_TYPE,@SIMPLE_POLICY) = @MASTER_POLICY AND (UPPER(@STATUS) = 'APPLICATION' OR UPPER(@STATUS) = 'UISSUE'))                                  
  -- BEGIN                                  
  --   SELECT @PRODUCT_TYPE AS PRODUCT_TYPE                                   
  --   FROM POL_CUSTOMER_POLICY_LIST RISK WITH(NOLOCK)                                   
  --   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
  --   FOR XML AUTO,ELEMENTS ,Root('RISKS')                                    
                                     
  -- END                                  
  --ELSE                                  
  -- BEGIN                                  
                                     
         --Count risk if policy have any risk then biling info is mendatory                           
     EXEC Proc_GetPolicyRiskCount @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@POLICY_LOB,@RISK_COUNT OUT                         
                          
                            
             
     IF((@TRANSACTION_TYPE =  @MASTER_POLICY OR @CO_INSURANCE = @CO_INSURANCE_FOLLOWER) AND (UPPER(@STATUS) = 'APPLICATION' OR UPPER(@STATUS) = 'UISSUE') and @RISK_COUNT=0)    --for Risk info not mandatory in case of master policy and when co-insurance type is follower                      
     BEGIN                                  
     --SELECT @IS_MANDATORY = 'N'                                  
      SELECT @PRODUCT_TYPE AS PRODUCT_TYPE ,'N' AS IS_MANDATORY ,                              
     @POLICY_LOB as LOB_ID,'0' AS MAXIMUM_LIMIT,'0' as SUM_LIMIT,'0' VALUE_AT_RISK ,'0' BASIC_COVERAGE_SI ,                       
 '0' AS IS_MAIN_APPLY ,'0' AS COUNT_ISMAIN,                    
 '0' AS VALUE_AT_RISK_APPLICABLE ,              
 '0' as  SUB_COV_SI_SUM ,              
 '0' as MAIN_COV_SI_SUM                     
     FROM POL_CUSTOMER_POLICY_LIST RISK WITH(NOLOCK)                                   
      WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
      FOR XML AUTO,ELEMENTS ,Root('RISKS')                                    
                                
             END                                  
             ELSE                                  
       BEGIN                                  
        IF (@POLICY_LOB in (9,26)  )                         
        --Changed by Lalit Chauhan,March 02,2011                    
        BEGIN                                   
         SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS                     
         VALUE_AT_RISK_APPLICABLE,@IS_MANDATORY AS IS_MANDATORY,                    
         ISNULL(LMI,0) MAXIMUM_LIMIT,                    
         CAST(ROUND(ISNULL(VR,0),2)AS DECIMAL(25,2)) VALUE_AT_RISK,                    
         CAST(ROUND(ISNULL(@BASIC_SUM_LIMIT,0),2)AS DECIMAL(25,2)) BASIC_SUM_LIMIT,                    
         CAST(ROUND(ISNULL(MAX_LIMIT,0),2)AS DECIMAL(25,2)) AS BASIC_COVERAGE_SI,                    
         @PRODUCT_TYPE AS PRODUCT_TYPE,RISK.PERIL_ID,                    
         CALCULATION_NUMBER,LOCATION,ADDRESS,RISK.NUMBER,COMPLEMENT,CITY,COUNTRY,                                    
         STATE,ZIP,TELEPHONE,EXTENTION,FAX,RISK.CATEGORY,ATIV_CONTROL,LOC,LOCALIZATION,OCCUPANCY,ISNULL(RISK.CONSTRUCTION,'0')AS CONSTRUCTION,RISK.LOC_CITY,CONSTRUCTION_TYPE,                                    
RISK.ACTIVITY_TYPE,RISK_TYPE,VR,LMI,BUILDING,MMU,MMP,MRI,TYPE,LOSS,LOYALTY,PERC_LOYALTY,DEDUCTIBLE_OPTION,MULTIPLE_DEDUCTIBLE,                                    
         OCCUPATION_TEXT,ASSIST24,RAWVALUES,REMARKS,PARKING_SPACES,CLAIM_RATIO,RAW_MATERIAL_VALUE,CONTENT_VALUE,                              
         ISNULL(CAST(L.LOC_NUM AS NVARCHAR),0) AS RISK_DESC,                    
         @POLICY_LOB AS LOB_ID,               
         --ISNULL(@SUM_LIMIT,0) AS SUM_LIMIT ,              
         CAST(ROUND(ISNULL(SUM_LIMIT,0),2)AS DECIMAL(25,2)) AS SUM_LIMIT ,                   
         ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN                    
         FROM  POL_PERILS RISK WITH(NOLOCK)                         
         JOIN                    
         (SELECT RISK_ID,        
   CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT ,                    
   MAX(                    
   CASE WHEN IS_MAIN = 1  THEN COVG.LIMIT_1 ELSE 0 END                    
   )                   
   MAX_LIMIT,              
    SUM(                
    CASE WHEN MNT.REINSURANCE_LOB = '10963'                 
    THEN COVG.LIMIT_1                 
    ELSE 0 END                
    )                 
    AS SUM_LIMIT                     
   FROM POL_PERILS RISK                    
   JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.PERIL_ID=COVG.RISK_ID                    
   JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.PERIL_ID                    
   JOIN POL_LOCATIONS L WITH(NOLOCK) ON                     
   L.CUSTOMER_ID = RISK.CUSTOMER_ID AND                     
   L.POLICY_ID  =RISK.POLICY_ID AND                     
   L.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID                     
   and L.LOCATION_ID = RISK.LOCATION                  
                      
          WHERE RISK.CUSTOMER_ID=@CUSTOMER_ID AND RISK.POLICY_ID=@POLICY_ID                               
          AND RISK.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                                   
          FOR XML AUTO,ELEMENTS,ROOT('RISKS')                            
                                          
        END                                    
       else if (@POLICY_LOB=13)                                    
        begin                                    
          SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE                    
          ,@IS_MANDATORY as IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,                    
          MARITIME_ID,VESSEL_NUMBER,NAME_OF_VESSEL,TYPE_OF_VESSEL,MANUFACTURE_YEAR,MANUFACTURER,                                    
          BUILDER,CONSTRUCTION,PROPULSION,CLASSIFICATION,LOCAL_OPERATION,LIMIT_NAVIGATION,PORT_REGISTRATION,REGISTRATION_NUMBER,TIE_NUMBER,                                    
           VESSEL_ACTION_NAUTICO_CLUB,NAME_OF_CLUB,LOCAL_CLUB,NUMBER_OF_CREW,NUMBER_OF_PASSENGER,REMARKS,IS_ACTIVE  , @POLICY_LOB as LOB_ID                                  
          ,ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
          CAST(ISNULL(RISK.VESSEL_NUMBER,0) AS NVARCHAR)+ISNULL('-'+RISK.NAME_OF_VESSEL,'') AS RISK_DESC                    
          FROM  POL_MARITIME RISK WITH(NOLOCK)                        
          left outer JOIN                    
         (SELECT RISK_ID,                    
   CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT                     
   FROM POL_MARITIME RISK                    
   left outer JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.MARITIME_ID=COVG.RISK_ID                    
   JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.MARITIME_ID                    
          WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                           
   AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
          FOR XML AUTO,ELEMENTS,Root('RISKS')                        
                              
                              
                                          
        end                                    
       else IF (@POLICY_LOB in(17,18,28,29,31,30,36))                          
         begin                                  
   SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE                    
         ,@IS_MANDATORY as IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,VEHICLE_ID,CLIENT_ORDER,VEHICLE_NUMBER,MANUFACTURED_YEAR,FIPE_CODE,CATEGORY,CAPACITY,MAKE_MODEL,LICENSE_PLATE,CHASSIS,                                  
           MANDATORY_DEDUCTIBLE,FACULTATIVE_DEDUCTIBLE,SUB_BRANCH,ISNULL(RISK_EFFECTIVE_DATE,'')AS RISK_EFFECTIVE_DATE ,ISNULL(RISK_EXPIRE_DATE,'')AS RISK_EXPIRE_DATE,REGION,COV_GROUP_CODE,                                  
           FINANCE_ADJUSTMENT,REFERENCE_PROPOSASL,REMARKS,IS_ACTIVE, @POLICY_LOB as LOB_ID                       
           ,ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
          ISNULL(CAST(RISK.VEHICLE_NUMBER AS NVARCHAR),'') AS RISK_DESC                               
         FROM  POL_CIVIL_TRANSPORT_VEHICLES RISK WITH(NOLOCK)                                                                                                                                                 
         JOIN                    
         (SELECT RISK_ID,                    
    CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT                     
                       
   FROM POL_CIVIL_TRANSPORT_VEHICLES RISK                    
   JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.VEHICLE_ID=COVG.RISK_ID                    
   JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.VEHICLE_ID                    
         WHERE RISK.CUSTOMER_ID=@CUSTOMER_ID and RISK.POLICY_ID=@POLICY_ID and                     
         RISK.POLICY_VERSION_ID=@POLICY_VERSION_ID                             
         AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                               
         FOR XML AUTO,ELEMENTS,Root('RISKS')                                    
         end          
         ELSE IF(@POLICY_LOB IN (38))      
         begin                                  
   SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE                    
         ,@IS_MANDATORY as IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,VEHICLE_ID,'' as CLIENT_ORDER,INSURED_VEH_NUMBER AS VEHICLE_NUMBER,VEHICLE_YEAR AS MANUFACTURED_YEAR,VIN AS FIPE_CODE,VEHICLE_TYPE as CATEGORY,'' as CAPACITY,MAKE +'_'+ MODEL AS  MAKE_MODEL,'' AS LICENSE_PLATE,CHASIS_NUMBER AS  CHASSIS,                                  
           0 as MANDATORY_DEDUCTIBLE,0 as FACULTATIVE_DEDUCTIBLE,0 as SUB_BRANCH,ISNULL(CREATED_DATETIME,'')AS RISK_EFFECTIVE_DATE ,ISNULL(LAST_UPDATED_DATETIME,'')AS RISK_EXPIRE_DATE,'' as REGION,VEHICLE_ID as COV_GROUP_CODE,                             

  
    
     
           '' as FINANCE_ADJUSTMENT,'' as REFERENCE_PROPOSASL,'' as REMARKS,'Y' as IS_ACTIVE, @POLICY_LOB as LOB_ID                       
           ,ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
          ISNULL(CAST(RISK.INSURED_VEH_NUMBER AS NVARCHAR),'') AS RISK_DESC                               
         FROM  POL_VEHICLES RISK WITH(NOLOCK)                                                                                                                                                 
         LEFT OUTER JOIN                    
         (SELECT RISK_ID,                    
    CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT                     
                       
   FROM POL_VEHICLES RISK                    
   left outer JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.VEHICLE_ID=COVG.RISK_ID                    
   left outer JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.VEHICLE_ID                    
         WHERE RISK.CUSTOMER_ID=@CUSTOMER_ID and RISK.POLICY_ID=@POLICY_ID and                     
         RISK.POLICY_VERSION_ID=@POLICY_VERSION_ID                             
         AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                               
         FOR XML AUTO,ELEMENTS,Root('RISKS')                
               
                       
                                   
         end                                
       else if (@POLICY_LOB in (20,23))                                    
        begin                                    
          SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE,                    
          @IS_MANDATORY as IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,COMMODITY_ID,COMMODITY_NUMBER,COMMODITY,CONVEYANCE,SUM_INSURED,ISNULL(DEPARTING_DATE,'')as DEPARTING_DATE ,ISNULL(ARRIVAL_DATE,'') as ARRIVAL_DATE,ORIGIN_COUNTRY,                       

 
     
      
       
          
            
          ORIGIN_STATE,ORIGIN_CITY,DESTINATION_COUNTRY,DESTINATION_STATE,DESTINATION_CITY,REMARKS,IS_ACTIVE,CONVEYANCE_TYPE , @POLICY_LOB as LOB_ID                                  
           ,ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
          CAST(ISNULL(RISK.COMMODITY_NUMBER,0) AS NVARCHAR)+ISNULL('-'+RISK.COMMODITY,'') AS RISK_DESC                     
          FROM  POL_COMMODITY_INFO RISK WITH(NOLOCK)                                                                                                                                                 
          JOIN                   
         (SELECT RISK_ID,                       
            CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT                     
   FROM POL_COMMODITY_INFO RISK                    
   JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.COMMODITY_ID=COVG.RISK_ID                    
   JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.COMMODITY_ID                    
                              
          WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                     
          AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                                                         
          FOR XML AUTO,ELEMENTS,Root('RISKS')                                    
        end                                    
       else if (@POLICY_LOB in (15,21,33,34))                                    
        begin                                    
          SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE,                    
          @IS_MANDATORY as IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,PERSONAL_INFO_ID,APPLICANT_ID,INDIVIDUAL_NAME,CODE,POSITION_ID,CPF_NUM,STATE_ID,                                    
          COUNTRY_ID,DATE_OF_BIRTH,GENDER,REG_IDEN,REG_ID_ISSUES,REG_ID_ORG,REMARKS,IS_ACTIVE, @POLICY_LOB as LOB_ID                                  
          ,ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
  CAST(ISNULL(RISK.INDIVIDUAL_NAME,0) AS NVARCHAR)+ISNULL('-'+RISK.CODE,'') AS RISK_DESC                     
          FROM  POL_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK)                    
          JOIN                    
         (SELECT RISK_ID,                    
   CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT                     
   FROM POL_PERSONAL_ACCIDENT_INFO RISK                    
   JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.PERSONAL_INFO_ID=COVG.RISK_ID                    
   JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.PERSONAL_INFO_ID                 
                              
          WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                      
          AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                                                                              
          FOR XML AUTO,ELEMENTS,Root('RISKS')                                    
        end                                    
       else if (@POLICY_LOB =22 )                                    
        begin                                    
          SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE,                    
          @IS_MANDATORY as IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,PERSONAL_ACCIDENT_ID,ISNULL(START_DATE,'')AS START_DATE,ISNULL(END_DATE,'')AS END_DATE,NUMBER_OF_PASSENGERS,IS_ACTIVE , @POLICY_LOB as LOB_ID                                  
           ,ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
          CAST(ISNULL(RISK.START_DATE,0) AS NVARCHAR)+ISNULL('-'+CAST(RISK.END_DATE AS NVARCHAR),'') AS RISK_DESC                     
          FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK)                                 
          JOIN                    
         (SELECT RISK_ID,                    
   CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT                      
   FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK                    
   JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)               
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.PERSONAL_ACCIDENT_ID=COVG.RISK_ID                    
   JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.PERSONAL_ACCIDENT_ID                    
                            
          WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                      
          AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                                                                              
          FOR XML AUTO,ELEMENTS,Root('RISKS')                                    
        end                                 
        else if (@POLICY_LOB in (35,37) )  --Rural Lien                                   
        begin                                    
          SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE,                    
          @IS_MANDATORY as IS_MANDATORY,@PRODUCT_TYPE AS PRODUCT_TYPE,                              
          PENHOR_RURAL_ID,ITEM_NUMBER,MODE,                              
          PROPERTY,IS_ACTIVE , @POLICY_LOB as LOB_ID                                  
          ,ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
       CAST(ISNULL(RISK.ITEM_NUMBER,0) AS NVARCHAR)+ISNULL('-'+RISK.CITY,'') AS RISK_DESC                     
          FROM  POL_PENHOR_RURAL_INFO RISK WITH(NOLOCK)                                                                                                                                             
          JOIN                    
         (SELECT RISK_ID,                    
   CASE WHEN                    
   SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
   THEN 1 ELSE 0                    
   END                    
   AS MAIN_COUNT                     
   FROM POL_PENHOR_RURAL_INFO RISK                    
   JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
   ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
   AND RISK.POLICY_ID=COVG.POLICY_ID                     
   AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
   AND RISK.PENHOR_RURAL_ID=COVG.RISK_ID                    
   JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
   MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
   WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
   RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
   GROUP BY RISK_ID                    
   )TEMP                     
   ON TEMP.RISK_ID = RISK.PENHOR_RURAL_ID                    
                              
          WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                      
          AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                                                                              
          FOR XML AUTO,ELEMENTS,Root('RISKS')                                    
        end                                   
       else  --in(10,11,12,14,16,19,25,27,32)   (10,11,9,14,19)                               
        begin                      
                                 
               SELECT * INTO #tempRiskCovrages                  
      from  ( SELECT c.COV_ID,c.RISK_ID,c.SUB_COV_SI_SUM,d.MAIN_COV_SI_SUM FROM (select b.RISK_ID,SUM(LIMIT_1) SUB_COV_SI_SUM,COV_REF_CODE COV_ID from mnt_coverage a              
    join POL_PRODUCT_COVERAGES b               
    on a.COV_ID = b.COVERAGE_CODE_ID               
    where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID              
    AND COV_REF_CODE IS NOT NULL               
    GROUP BY a.COV_REF_CODE ,b.RISK_ID) c              
    JOIN              
    (select b.RISK_ID,a.COV_ID,SUM(LIMIT_1) MAIN_COV_SI_SUM from mnt_coverage a              
    join POL_PRODUCT_COVERAGES b               
    on a.COV_ID = b.COVERAGE_CODE_ID               
    where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID              
    GROUP BY a.COV_ID,b.RISK_ID )d              
    on c.COV_ID =d.COV_ID AND              
    c.RISK_ID=d.RISK_ID)b              
                                 
         SELECT * INTO #tempRiskCoverages2                  
      from  ( SELECT c.COV_ID,c.RISK_ID,c.SUB_COV_WP_SUM,d.MAIN_COV_WP_SUM FROM (select b.RISK_ID,ISNULL(SUM(WRITTEN_PREMIUM),0) SUB_COV_WP_SUM,COV_REF_CODE COV_ID from mnt_coverage a              
    join POL_PRODUCT_COVERAGES b               
    on a.COV_ID = b.COVERAGE_CODE_ID               
    where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID             
    AND COV_REF_CODE IS NOT NULL               
    GROUP BY a.COV_REF_CODE ,b.RISK_ID) c              
    JOIN              
    (select b.RISK_ID,a.COV_ID,SUM(WRITTEN_PREMIUM) MAIN_COV_WP_SUM from mnt_coverage a              
    join POL_PRODUCT_COVERAGES b               
    on a.COV_ID = b.COVERAGE_CODE_ID               
    where CUSTOMER_ID =  @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID             
    GROUP BY a.COV_ID,b.RISK_ID )d              
    on c.COV_ID =d.COV_ID AND              
    c.RISK_ID=d.RISK_ID)b              
                              
                                    
          SELECT ISNULL(@VALUE_AT_RISK_APPLICABLE,0) AS VALUE_AT_RISK_APPLICABLE,                    
          @IS_MANDATORY as IS_MANDATORY,                            
          @PRODUCT_TYPE AS PRODUCT_TYPE,                            
          PRODUCT_RISK_ID,LOCATION,                            
          CAST(ROUND(ISNULL(VALUE_AT_RISK,0),2)AS DECIMAL(25,2)) VALUE_AT_RISK,                            
          BUILDING_VALUE,                            
          CONTENTS_VALUE,                            
          RAW_MATERIAL_VALUE,                                    
          CONTENTS_RAW_VALUES,                            
          MRI_VALUE,                    
          CAST(ROUND(ISNULL(MAXIMUM_LIMIT,0),2)AS DECIMAL(25,2)) AS MAXIMUM_LIMIT,                    
          POSSIBLE_MAX_LOSS,MULTIPLE_DEDUCTIBLE,PARKING_SPACES,RISK.ACTIVITY_TYPE,OCCUPIED_AS,RISK.CONSTRUCTION,                                    
          RUBRICA,ASSIST24,REMARKS,RISK.IS_ACTIVE  , @POLICY_LOB as LOB_ID ,                    
          --CAST(ROUND(ISNULL(@SUM_LIMIT,0),0)AS DECIMAL(12,0)) AS SUM_LIMIT ,    --commented by aditya for compare maximum limit from sum ri coverages at per risk                
          CAST(ROUND(ISNULL(SUM_LIMIT,0),2)AS DECIMAL(25,2)) AS SUM_LIMIT ,                
          CAST(ROUND(ISNULL(MAX_LIMIT,0),2)AS DECIMAL(25,2)) AS BASIC_COVERAGE_SI,                    
          ISNULL(@IS_MAIN_APPLY,0) AS IS_MAIN_APPLY ,ISNULL(TEMP.MAIN_COUNT,0) AS COUNT_ISMAIN,                    
          ISNULL(CAST(L.LOC_NUM AS NVARCHAR),0) AS RISK_DESC    ,              
          ISNULL(tempCov.COV_ID,'') COV_ID,          
          ISNULL(tempCov.SUB_COV_SI_SUM,0) SUB_COV_SI_SUM,          
          ISNULL(tempCov.MAIN_COV_SI_SUM,0) MAIN_COV_SI_SUM,          
           ISNULL(tempCove.COV_ID,'') COV_ID,          
           ISNULL(tempCove.MAIN_COV_WP_SUM,0) MAIN_COV_WP_SUM,          
           --CASE WHEN ISNULL(tempCove.SUB_COV_WP_SUM,0) = 0 --if sub coverages written premium sum is then we consider main coverage WP SUM==to sub coverage WP sum for product rule          
           --THEN ISNULL(tempCove.MAIN_COV_WP_SUM,0) ELSE          
  ISNULL(tempCove.SUB_COV_WP_SUM,0)   SUB_COV_WP_SUM          
                     
                   
                          
        FROM  POL_PRODUCT_LOCATION_INFO RISK WITH(NOLOCK)                   
                            
         JOIN                    
    (SELECT RISK_ID,                    
      CASE WHEN                    
    SUM(CASE WHEN IS_MAIN = 1 THEN 1 ELSE 0 END) > 0                    
    THEN 1 ELSE 0                    
    END                   
    AS MAIN_COUNT  ,                    
    MAX(                    
    --CASE WHEN IS_MAIN = 1  THEN --- commented by aditya now value at risk should not lower than the max SI between selected coverages - iTrack #-688                
    COVG.LIMIT_1                 
    --ELSE 0 END                    
    )                    
    MAX_LIMIT   ,                
                    
    SUM(                
    CASE WHEN MNT.REINSURANCE_LOB = '10963'                 
    THEN COVG.LIMIT_1                 
    ELSE 0 END                
    )                 
    AS SUM_LIMIT                 
                  
    FROM POL_PRODUCT_LOCATION_INFO RISK                    
    JOIN POL_PRODUCT_COVERAGES COVG WITH(NOLOCK)                     
    ON RISK.CUSTOMER_ID=COVG.CUSTOMER_ID                    
    AND RISK.POLICY_ID=COVG.POLICY_ID                     
    AND RISK.POLICY_VERSION_ID=COVG.POLICY_VERSION_ID                    
    AND RISK.PRODUCT_RISK_ID=COVG.RISK_ID                    
    JOIN MNT_COVERAGE MNT WITH(NOLOCK) ON                     
    MNT.COV_ID=COVG.COVERAGE_CODE_ID                    
    WHERE RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID =@POLICY_ID AND                     
    RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                    
    GROUP BY RISK_ID                    
    )TEMP                     
    ON TEMP.RISK_ID = RISK.PRODUCT_RISK_ID                       
    JOIN POL_LOCATIONS L WITH(NOLOCK) ON        
    L.CUSTOMER_ID = RISK.CUSTOMER_ID AND                     
    L.POLICY_ID  =RISK.POLICY_ID AND                     
    L.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID                     
    and L.LOCATION_ID = RISK.LOCATION                              
    LEFT OUTER JOIN               
    #tempRiskCovrages tempCov  ON               
    tempCov.RISK_ID =  RISK.PRODUCT_RISK_ID             
    LEFT OUTER JOIN               
    #tempRiskCoverages2 tempCove  ON               
    tempCove.RISK_ID =  RISK.PRODUCT_RISK_ID                         
          WHERE RISK.CUSTOMER_ID=@CUSTOMER_ID and RISK.POLICY_ID=@POLICY_ID and RISK.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                
          AND ISNULL(RISK.IS_ACTIVE,'Y') = 'Y'                      
          FOR XML AUTO,ELEMENTS,Root('RISKS')                  
                        
          DROP TABLE  #tempRiskCovrages              
                                          
        end                                   
      END                                  
                                        
                                         
    --END                                  
                                    
   --table 8 Covererage Table                                    
   SELECT RISK_ID,COVERAGE_ID,COVERAGE_CODE_ID,RI_APPLIES,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,                                    
   LIMIT_2,LIMIT_2_TYPE,LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,                                    
   MINIMUM_DEDUCTIBLE,DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,DEDUCTIBLE_REDUCES,INITIAL_RATE,FINAL_RATE,AVERAGE_RATE,WRITTEN_PREMIUM,                                    
   FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE,LIMIT_ID,DEDUC_ID,ADD_INFORMATION,ISNULL(@COUNT_ISMAIN,0)  AS COUNT_ISMAIN,ISNULL(@TIV,0) AS  TIV,           
   ISNULL(@TOTAL_TIV_RI,0) as TOTAL_TIV_RI,ISNULL(@SUM_RI,0) AS SUM_RI, -- Added by aditya for tfs # 2685          
   ISNULL( @MAX_LAYER_AMOUNT,0) LAYER_AMOUNT --itrack # 1329          
   , IS_DEFAULT , IS_MANDATORY          
             
   FROM  POL_PRODUCT_COVERAGES COVERAGE WITH(NOLOCK)LEFT OUTER JOIN MNT_COVERAGE WITH(NOLOCK)                                  
   ON COVERAGE_CODE_ID = COV_ID and COVERAGE.IS_ACTIVE='Y'                                                                                                                                              
   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID               
   FOR XML AUTO,ELEMENTS,Root('COVERAGES')                                                                                
                                     
  --table 9 Protective Devices Table                                    
  SELECT RISK_ID,PROTECTIVE_DEVICE_ID,FIRE_EXTINGUISHER,SPL_FIRE_EXTINGUISHER_UNIT,MANUAL_FOAM_SYSTEM,                                    
    FOAM,INERT_GAS_SYSTEM,MANUAL_INERT_GAS_SYSTEM,COMBAT_CARS,CORRAL_SYSTEM,ALARM_SYSTEM,FREE_HYDRANT,SPRINKLERS,SPRINKLERS_CLASSIFICATION,                                    
    FIRE_FIGHTER,QUESTIION_POINTS,IS_ACTIVE,LOCATION_ID                               
   FROM  POL_PROTECTIVE_DEVICES P_DEVICE WITH(NOLOCK)                                                                                                                                                 
   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
   FOR XML AUTO,ELEMENTS,Root('P_DEVICES')                                              
  END                                  
  --table 10 BILLING INFO TABLE                                     
  ---For Billing Info In Endorsment process                                 
                            
  -- IF(ISNULL(@PRODUCT_TYPE,@SIMPLE_POLICY) = @MASTER_POLICY AND (UPPER(@STATUS) = 'APPLICATION' OR UPPER(@STATUS) = 'UISSUE'))                                  
  --IF(@TRANSACTION_TYPE = @MASTER_POLICY)                                  
  -- BEGIN                            
  --   --SELECT @IS_MANDATORY = 'N'                                  
  --   SELECT @PRODUCT_TYPE AS PRODUCT_TYPE,                          
  --   CASE WHEN ISNULL(@RISK_COUNT,0) < 1                   
  --   THEN 'N'                           
  --   ELSE 'Y'                           
  --   END AS IS_MANDATORY,                          
  --   '0' AS COVERAGES_PREMIUM ,'0' AS PREV_PREMIUM ,                                 
  --   ISNULL(@RISK_COUNT,0) AS RISK                          
  --   FROM POL_CUSTOMER_POLICY_LIST BILLING_INFO WITH(NOLOCK)                                   
  --   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
  --   FOR XML AUTO,ELEMENTS ,Root('BILLING_INFOS')                                    
  -- END                                 
  --ELSE                           
  IF(@CO_INSURANCE = @CO_INSURANCE_FOLLOWER  AND @RISK_COUNT < 1  AND  (UPPER(@STATUS) = 'APPLICATION' OR UPPER(@STATUS) = 'UISSUE'))                                  
  BEGIN                            
      SELECT @TRANSACTION_TYPE AS PRODUCT_TYPE,'N' AS IS_MANDATORY   , @POLICY_DUE_DATE AS POLICY_DUE_DATE,                         
      ISNULL(@RISK_COUNT,0) AS RISK                               
     FROM POL_CUSTOMER_POLICY_LIST BILLING_INFO WITH(NOLOCK)                                   
     WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                      
     FOR XML AUTO,ELEMENTS ,Root('BILLING_INFOS')                
   END                                  
  ELSE                                  
  BEGIN                           
                             
  --  IF( @PRODUCT_TYPE = @MASTER_POLICY)                                  
  --      EXEC Proc_Generate_MasterPolicyInstallments @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,null,null,null,null,'RULES',@WRITTEN_PREMIUM OUT                                  
  --ELSE                                  
                                      
     EXEC Proc_GetPolicyCoveragePremiumSum @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@POLICY_LOB,@WRITTEN_PREMIUM OUT,'RULES'                              
                                  
        
    IF EXISTS(SELECT PROCESS_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID  =@POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_ID IN (3,14,8,9))                                  
      BEGIN                            
  IF EXISTS(SELECT EFFECTIVE_DATETIME FROM POL_POLICY_PROCESS WITH(NOLOCK)    WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS <> 'ROLLBACK')                                  
   BEGIN                                  
         SELECT @END_EFF_DATE = ISNULL(EFFECTIVE_DATETIME,@POLICY_EFF_DATE),@END_EXP_DATE =ISNULL([EXPIRY_DATE],@POLICY_EXPI_DATE),@PREV_VERSION = POLICY_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK)                                             
          WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID  =@POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS <> 'ROLLBACK'                                  
         END                                  
      ELSE                                  
          BEGIN                            
             SELECT @END_EFF_DATE = POLICY_EFFECTIVE_DATE,@END_EXP_DATE = APP_EXPIRATION_DATE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                   
           LEFT OUTER JOIN                                   
           POL_POLICY_PROCESS WITH(NOLOCK) ON                                  
           POL_POLICY_PROCESS.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID AND                                   
           POL_POLICY_PROCESS.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID AND                                   
           POL_POLICY_PROCESS.NEW_POLICY_VERSION_ID = POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID                                  
             WHERE POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID = @CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID =@POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID = @POLICY_VERSION_ID AND POL_POLICY_PROCESS.PROCESS_STATUS <> 'ROLLBACK'             

  
    
      
        
          
            
              
                
                  
                    
                     
         END                                   
                                                           
  IF(@PREV_VERSION IS NOT NULL )                                  
          BEGIN                             
   EXEC Proc_GetPolicyCoveragePremiumSum @CUSTOMER_ID,@POLICY_ID,@PREV_VERSION,@POLICY_LOB,@PREV_PREMIUM OUT,'RULES'                                      
          END                                
                                   
          IF(@TRANSACTION_TYPE = @MASTER_POLICY)                            
   EXEC Proc_Generate_MasterPolicyInstallments     @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,null,null,null,null,'RULES',@PRORATE_AMOUNT OUT                            
        ELSE                            
   SELECT @PRORATE_AMOUNT =@WRITTEN_PREMIUM -- CONVERT(decimal(25,2),((ISNULL(@WRITTEN_PREMIUM,0)-ISNULL(@PREV_PREMIUM,0))*(DATEDIFF(DAY,@END_EFF_DATE,@END_EXP_DATE)))/@POLICY_EFF_DAY)                                    
         --- IN CASE OF ENDORSEMENT                      
          SELECT @ENDORSEMENT_TYPE=ENDORSEMENT_TYPE FROM   POL_POLICY_PROCESS  WHERE CUSTOMER_ID=@CUSTOMER_ID           
          AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID                           
        SELECT @TRANSACTION_TYPE AS PRODUCT_TYPE,ISNULL(TOTAL_PREMIUM,0) TOTAL_PREMIUM,                                  
         ISNULL(TOTAL_TRAN_PREMIUM,0) TOTAL_TRAN_PREMIUM ,@POLICY_DUE_DATE AS POLICY_DUE_DATE,                                  
         ISNULL(@PRORATE_AMOUNT,0) AS PRORATA_AMOUNT,      
         ISNULL(@PREV_PREMIUM,0) PREV_PREMIUM,                                  
         ISNULL(@WRITTEN_PREMIUM,0) AS COVERAGES_PREMIUM,                                  
         ISNULL(POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER,'') AS POLICY_NUMBER,                 
         @IS_MANDATORY AS IS_MANDATORY ,            
         ISNULL(@RISK_COUNT,0) AS RISK ,              
   ISNULL(@INSTALLMENT_AMOUNT,0) AS INSTALLMENT_AMOUNT,                     
         CASE when (@ENDORSEMENT_TYPE=14682)          
         THEN  '14682'else '0'           
         END as  ADDITIONAL_ENDORSEMENT,          
         CASE WHEN (@ENDORSEMENT_TYPE=14684)          
         THEN '14684' else '0'          
         END as REFUND_ENDORSEMENT                 
                   
         FROM ACT_POLICY_INSTALL_PLAN_DATA BILLING_INFO WITH(NOLOCK)                                  
         RIGHT OUTER JOIN                                  
         POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) ON                                   
         BILLING_INFO.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID AND                                   
         BILLING_INFO.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID AND                                   
         BILLING_INFO.POLICY_VERSION_ID= POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID            
                                                             
         WHERE POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID=@CUSTOMER_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_ID=@POLICY_ID AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
         FOR XML AUTO,ELEMENTS,ROOT('BILLING_INFOS')                                   
     END                                  
   ELSE                                  
   BEGIN                            
       SELECT @TRANSACTION_TYPE AS PRODUCT_TYPE,@IS_MANDATORY AS IS_MANDATORY,                         
        TOTAL_PREMIUM,TOTAL_TRAN_PREMIUM,ISNULL(@WRITTEN_PREMIUM,0) AS COVERAGES_PREMIUM ,                          
        ISNULL(@RISK_COUNT,0) AS RISK,@POLICY_DUE_DATE AS POLICY_DUE_DATE           
       FROM ACT_POLICY_INSTALL_PLAN_DATA BILLING_INFO WITH(NOLOCK)                                  
       WHERE BILLING_INFO.CUSTOMER_ID=@CUSTOMER_ID AND BILLING_INFO.POLICY_ID=@POLICY_ID AND BILLING_INFO.POLICY_VERSION_ID=@POLICY_VERSION_ID                
       FOR XML AUTO,ELEMENTS,ROOT('BILLING_INFOS')                                   
     END                                  
                                      
   END                  
                   
  SELECT             
  ISNULL(COAPPLICANT.COMMISSION_PERCENT,0)  CURRENT_COMMISSION,          
  ISNULL(PAL2.COMMISSION_PERCENT,0) PREV_COMMISSION,          
  ISNULL(COAPPLICANT.FEES_PERCENT,0)  AS CURRENT_FEES,           
  ISNULL(PAL2.FEES_PERCENT,0) AS  PREV_FEES  ,          
  ISNULL(COAPPLICANT.PRO_LABORE_PERCENT,0) CURRENT_PRO_LABORE ,           
  ISNULL(PAL2.PRO_LABORE_PERCENT,0) PREV_PROLABORE ,           
  ISNULL(CAL.APPLICANT_TYPE,'')as APPLICANT_TYPE,                
  ISNULL(CO_INSURANCE,'')as CO_INSURANCE,                
  ISNULL(COAPPLICANT.IS_PRIMARY_APPLICANT,'') as IS_PRIMARY_APPLICANT,                 
   CASE WHEN   CAL.FIRST_NAME = '' THEN '0' ELSE ISNULL(CAL.FIRST_NAME,'0') END FIRST_NAME,                  
   CASE WHEN   CAL.ACCOUNT_TYPE = '' THEN '0' ELSE ISNULL(CAL.ACCOUNT_TYPE,'0') END ACCOUNT_TYPE,                       
   CASE WHEN   CAL.ZIP_CODE = '' THEN '0' ELSE ISNULL(CAL.ZIP_CODE,'0') END ZIP_CODE,                     
   CASE WHEN   CAL.ADDRESS1 = '' THEN '0' ELSE ISNULL(CAL.ADDRESS1,'0') END ADDRESS1,                      
   CASE WHEN   CAL.CITY = '' THEN '0' ELSE ISNULL(CAL.CITY,'0') END CITY,                    
   CASE WHEN   CAL.STATE = '' THEN '0' ELSE ISNULL(CAL.STATE,'0') END STATE,                    
   CASE WHEN   CAL.COUNTRY = '' THEN '0' ELSE ISNULL(CAL.COUNTRY,'0') END COUNTRY,                    
   CASE WHEN   CAL.NUMBER = '' THEN '0' ELSE ISNULL(CAL.NUMBER,'0') END CO_APP_NUMBER,                
   CASE WHEN   CAL.DISTRICT = '' THEN '0' ELSE ISNULL(CAL.DISTRICT,'0') END CO_APP_DISTRICT,                   
   CASE WHEN   CAL.CO_APPL_GENDER = '' THEN '0' ELSE ISNULL(CAL.CO_APPL_GENDER,'0') END CO_APPL_GENDER,                  
   CASE WHEN  convert(VARCHAR(100),CAL.REG_ID_ISSUE) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),CAL.REG_ID_ISSUE ),'0') END CO_APP_REG_ID_ISSUE,                 
   CASE WHEN   CAL.ORIGINAL_ISSUE = '' THEN '0' ELSE ISNULL(CAL.ORIGINAL_ISSUE,'0') END CO_APP_ORIGINAL_ISSUE,                    
   CASE WHEN   CAL.REGIONAL_IDENTIFICATION = '' THEN '0' ELSE ISNULL(CAL.REGIONAL_IDENTIFICATION,'0') END CO_APP_REGIONAL_IDENTIFICATION,                    
   CASE WHEN   CAL.CO_APPL_MARITAL_STATUS = '' THEN '0' ELSE ISNULL(CAL.CO_APPL_MARITAL_STATUS,'0') END CO_APPL_MARITAL_STATUS,                    
   CASE WHEN  convert(VARCHAR(100),CAL.CO_APPL_DOB ) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),CAL.CO_APPL_DOB  ),'0') END CO_APPL_DOB,                     
   CASE WHEN  convert(VARCHAR(100),CAL.CO_APPL_DOB ) = '' THEN '0' ELSE ISNULL(convert(VARCHAR(100),CAL.CO_APPL_DOB  ),'0') END CO_APPL_CREATION_DATE,                     
   CASE WHEN   CAL.ACCOUNT_TYPE = '' THEN '0' ELSE ISNULL(CAL.ACCOUNT_TYPE,'0') END CO_APP_ACCOUNT_TYPE,@IS_DISREGARD_RI_CONTRACT AS IS_DISREGARD_RI_CONTRACT                  
                                   
   FROM  POL_CUSTOMER_POLICY_LIST [APPLICANT] WITH(NOLOCK)                   
--  INNER JOIN CLT_CUSTOMER_LIST CUSTOMER WITH(NOLOCK)  ON CUSTOMER.CUSTOMER_ID=[APPLICATION].CUSTOMER_ID                     
 LEFT JOIN POL_APPLICANT_LIST COAPPLICANT WITH(NOLOCK)  ON COAPPLICANT.CUSTOMER_ID=[APPLICANT].CUSTOMER_ID           
 JOIN POL_APPLICANT_LIST PAL2  WITH(NOLOCK)  ON COAPPLICANT.CUSTOMER_ID = PAL2.CUSTOMER_ID AND COAPPLICANT.POLICY_ID = PAL2.POLICY_ID AND          
 COAPPLICANT.APPLICANT_ID = PAL2.APPLICANT_ID AND PAL2.POLICY_VERSION_ID = (          
  SELECT POLICY_VERSION_ID from POL_POLICY_PROCESS PPP WITH(NOLOCK) WHERE           
  PPP.CUSTOMER_ID = COAPPLICANT.CUSTOMER_ID and PPP.POLICY_ID = COAPPLICANT.POLICY_ID AND           
  PPP.NEW_POLICY_VERSION_ID = COAPPLICANT.POLICY_VERSION_ID AND UPPER(PPP.PROCESS_STATUS) <> 'ROLLBACK'          
  )   -- for comparing with previous Version , changed by aditya for itrack # 1282                                      
 INNER JOIN CLT_APPLICANT_LIST CAL  WITH(NOLOCK)  ON CAL.CUSTOMER_ID=COAPPLICANT.CUSTOMER_ID  AND  COAPPLICANT.APPLICANT_ID=CAL.APPLICANT_ID                                       
 AND COAPPLICANT.POLICY_ID=[APPLICANT].POLICY_ID AND COAPPLICANT.POLICY_VERSION_ID =[APPLICANT].POLICY_VERSION_ID                 
                  
                                                                                                                                                             
WHERE [APPLICANT].CUSTOMER_ID=@CUSTOMER_ID and [APPLICANT].POLICY_ID=@POLICY_ID and [APPLICANT].POLICY_VERSION_ID=@POLICY_VERSION_ID                  
-- and      CAL.IS_PRIMARY_APPLICANT <> 1  Commented by aditya for itrack # 1282                                                                
 FOR XML AUTO,ELEMENTS,Root('APPLICANTS')             
           
           
 SELECT           
REMU.BROKER_ID,ISNULL(MAL.AGENCY_DISPLAY_NAME,'') AGENCY_NAME,          
MAL.AGENCY_TYPE_ID,  --Added by Aditya for itrack # 1282          
(IsNull(FIRST_NAME,''))  CO_APPLICANT_NAME          
,REMU.CO_APPLICANT_ID,          
REPLACE(@DELETE_BROKER_NBS,1,0) AS DELETE_BROKER_NBS -- changed by Aditya For itrack # 1282            
,REMU.COMMISSION_PERCENT,          
ISNULL(PR2.COMMISSION_PERCENT,0) PREV_COMMISSION          
,REMU.COMMISSION_TYPE          
,REMU.LEADER,          
CASE  WHEN REMU.COMMISSION_PERCENT <> PR2.COMMISSION_PERCENT          
THEN 1 ELSE 0 END IS_COMMISSION_CHANGED,          
(                 
  ISNULL((SELECT ISNULL(COUNT(BROKER_ID),0) FROM POL_REMUNERATION R WITH(NOLOCK) WHERE R.CUSTOMER_ID = @CUSTOMER_ID                     
     and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = (          
  SELECT POLICY_VERSION_ID from POL_POLICY_PROCESS PPP WITH(NOLOCK) WHERE           
  PPP.CUSTOMER_ID = REMU.CUSTOMER_ID and PPP.POLICY_ID = REMU.POLICY_ID AND           
  PPP.NEW_POLICY_VERSION_ID = REMU.POLICY_VERSION_ID AND UPPER(PPP.PROCESS_STATUS) <> 'ROLLBACK'          
  )  -- for comparing with previous Version, changed by aditya for itrack # 1282           
     and COMMISSION_TYPE = REMU.COMMISSION_TYPE          
     and R.CO_APPLICANT_ID = REMU.CO_APPLICANT_ID ),0)          
)PREV_BROKER_COUNT,          
(                 
  ISNULL((SELECT ISNULL(COUNT(BROKER_ID),0) FROM POL_REMUNERATION R WITH(NOLOCK) WHERE R.CUSTOMER_ID = @CUSTOMER_ID                     
     and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =@POLICY_VERSION_ID and COMMISSION_TYPE = REMU.COMMISSION_TYPE          
     and R.CO_APPLICANT_ID = REMU.CO_APPLICANT_ID),0)          
)CURRENT_BROKER_COUNT          
          
FROM POL_REMUNERATION REMU WITH(NOLOCK)           
          
          
JOIN POL_REMUNERATION PR2 WITH(NOLOCK) ON REMU.CUSTOMER_ID  = PR2.CUSTOMER_ID AND REMU.POLICY_ID = PR2.POLICY_ID          
AND REMU.CO_APPLICANT_ID = PR2.CO_APPLICANT_ID           
AND PR2.POLICY_VERSION_ID = (          
  SELECT POLICY_VERSION_ID from POL_POLICY_PROCESS PPP WITH(NOLOCK) WHERE           
  PPP.CUSTOMER_ID = REMU.CUSTOMER_ID and PPP.POLICY_ID = REMU.POLICY_ID AND           
  PPP.NEW_POLICY_VERSION_ID = REMU.POLICY_VERSION_ID AND UPPER(PPP.PROCESS_STATUS) <> 'ROLLBACK'          
  )  -- for comparing with previous Version, changed by aditya for itrack # 1282           
AND PR2.BROKER_ID = REMU.BROKER_ID          
AND PR2.COMMISSION_TYPE = REMU.COMMISSION_TYPE          
left outer join CLT_APPLICANT_LIST CAL with(nolock) on REMU.CO_APPLICANT_ID=CAL.APPLICANT_ID            
JOIN MNT_AGENCY_LIST MAL WITH(NOLOCK)ON MAL.AGENCY_ID = REMU.BROKER_ID -- changed by Aditya For itrack # 1282          
WHERE REMU.CUSTOMER_ID = @CUSTOMER_ID and REMU.POLICY_ID = @POLICY_ID           
and REMU.POLICY_VERSION_ID = @POLICY_VERSION_ID          
          
UNION  --Added by aditya for itrack # 1282          
SELECT           
REMU.BROKER_ID,ISNULL(MAL.AGENCY_DISPLAY_NAME,'') AGENCY_NAME,          
MAL.AGENCY_TYPE_ID, --Added by Aditya for itrack # 1282          
(IsNull(FIRST_NAME,''))  CO_APPLICANT_NAME,          
REMU.CO_APPLICANT_ID,          
@DELETE_BROKER_NBS AS DELETE_BROKER_NBS          
,REMU.COMMISSION_PERCENT,          
ISNULL(PR2.COMMISSION_PERCENT,0) PREV_COMMISSION          
,REMU.COMMISSION_TYPE          
,REMU.LEADER ,          
CASE  WHEN REMU.COMMISSION_PERCENT <> PR2.COMMISSION_PERCENT          
THEN 1 ELSE 0 END IS_COMMISSION_CHANGED,          
(                 
  ISNULL((SELECT ISNULL(COUNT(BROKER_ID),0) FROM POL_REMUNERATION R WITH(NOLOCK) WHERE R.CUSTOMER_ID = @CUSTOMER_ID                     
    and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = (          
  SELECT POLICY_VERSION_ID from POL_POLICY_PROCESS PPP WITH(NOLOCK) WHERE           
  PPP.CUSTOMER_ID = REMU.CUSTOMER_ID and PPP.POLICY_ID = REMU.POLICY_ID AND           
  PPP.NEW_POLICY_VERSION_ID = REMU.POLICY_VERSION_ID AND UPPER(PPP.PROCESS_STATUS) <> 'ROLLBACK'          
  )  -- for comparing with previous Version, changed by aditya for itrack # 1282           
     and COMMISSION_TYPE = REMU.COMMISSION_TYPE          
     and R.CO_APPLICANT_ID = REMU.CO_APPLICANT_ID),0)          
)PREV_BROKER_COUNT,          
(                 
  ISNULL((SELECT ISNULL(COUNT(BROKER_ID),0) FROM POL_REMUNERATION R WITH(NOLOCK) WHERE R.CUSTOMER_ID = @CUSTOMER_ID                     
     and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =@POLICY_VERSION_ID and COMMISSION_TYPE = REMU.COMMISSION_TYPE          
     and R.CO_APPLICANT_ID = REMU.CO_APPLICANT_ID),0)          
)CURRENT_BROKER_COUNT          
          
FROM POL_REMUNERATION REMU WITH(NOLOCK)           
JOIN POL_REMUNERATION PR2 WITH(NOLOCK) ON REMU.CUSTOMER_ID  = PR2.CUSTOMER_ID AND REMU.POLICY_ID = PR2.POLICY_ID          
AND REMU.CO_APPLICANT_ID = PR2.CO_APPLICANT_ID           
AND REMU.BROKER_ID = PR2.BROKER_ID          
AND PR2.POLICY_VERSION_ID = (          
  SELECT POLICY_VERSION_ID from POL_POLICY_PROCESS PPP WITH(NOLOCK) WHERE             
  PPP.CUSTOMER_ID = REMU.CUSTOMER_ID and PPP.POLICY_ID = REMU.POLICY_ID AND             
  PPP.NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND UPPER(PPP.PROCESS_STATUS) <> 'ROLLBACK'           
  )  -- for comparing with previous Version, changed by aditya for itrack # 1282           
AND PR2.BROKER_ID = REMU.BROKER_ID          
          
AND PR2.COMMISSION_TYPE = REMU.COMMISSION_TYPE          
JOIN MNT_AGENCY_LIST MAL WITH(NOLOCK)ON MAL.AGENCY_ID = REMU.BROKER_ID          
left outer join CLT_APPLICANT_LIST CAL with(nolock) on REMU.CO_APPLICANT_ID=CAL.APPLICANT_ID              
WHERE REMU.CUSTOMER_ID = @CUSTOMER_ID and REMU.POLICY_ID = @POLICY_ID           
and REMU.POLICY_VERSION_ID = (          
  SELECT POLICY_VERSION_ID from POL_POLICY_PROCESS PPP WITH(NOLOCK) WHERE           
  PPP.CUSTOMER_ID = REMU.CUSTOMER_ID and PPP.POLICY_ID = REMU.POLICY_ID AND           
  PPP.NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND UPPER(PPP.PROCESS_STATUS) <> 'ROLLBACK'          
  )  -- for comparing with previous Version , changed by aditya for itrack # 1282            
AND REMU.BROKER_ID NOT IN (SELECT BROKER_ID FROM POL_REMUNERATION WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID          
and COMMISSION_TYPE=REMU.COMMISSION_TYPE AND CO_APPLICANT_ID = REMU.CO_APPLICANT_ID)       -- Changed by Aditya For itrack # 1282          
          
FOR XML AUTO,ELEMENTS,ROOT('ENDORSEMENT_REMUNERATIONS')             
      
 IF(@POLICY_LOB IN(38))        
 BEGIN      
  select * from POL_DRIVER_DETAILS where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID AND IS_ACTIVE = 'Y'      
         FOR XML AUTO,ELEMENTS,Root('DRIVERS')                
         END       
   --                                
  END 

