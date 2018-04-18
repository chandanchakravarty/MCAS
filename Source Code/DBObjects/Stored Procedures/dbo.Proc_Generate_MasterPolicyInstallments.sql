IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Generate_MasterPolicyInstallments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Generate_MasterPolicyInstallments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran        
--drop proc Proc_Generate_MasterPolicyInstallments        
--Go        
/*----------------------------------------------------------                                                    
Proc Name        : dbo.Proc_Generate_MasterPolicyInstallments                
Created by       : Lalit chauhan                
Date             : 22/10/2010                                                  
Purpose          :                 
Used In          : Ebix Advantage                                                
-----------------------------------------------------        
Modified By   : Lalit Kr Chauhan         
Modified date  : 15/03/2011        
Purpose   : changed Request in installment generation, reference i-track# 600        
------------------------------------------------------------             
-----------------------------------------------------        
Modified By   : Lalit Kr Chauhan         
Modified date  : 13/04/2011        
Purpose   : generate installment per co-applicant,Selected on endorsement screen.i-track #948      
Chnages    - 2      
------------------------------------------------------------             
drop proc Proc_Generate_MasterPolicyInstallments  28070,247,2      
exec Proc_Generate_MasterPolicyInstallments 2043,847,1,398,'01/01/2010',null,null,null,null           
*/        
CREATE PROC [dbo].[Proc_Generate_MasterPolicyInstallments]                
 (                
 @CUSTOMER_ID INT,                
 @POLICY_ID INT,                
 @POLICY_VERSION_ID SMALLINT,                 
 @CREATED_BY INT = null,                
 @CREATED_DATETIME DATETIME = null,                
 @PLAN_ID INT = null,                 
 @RETVAL INT = NULL OUT,                
 @CALLED_FROM NVARCHAR(15)= NULL ,                
 @TOTAL_RISK_PREMIUM  DECIMAL(25,2)= NULL OUT                
 )                
 AS                 
 BEGIN                
                   
  --BEGIN TRAN              
                   
 DECLARE @TOTAL_PREMIUM DECIMAL(25,2),@TRAN_TYPE NVARCHAR(10)=''                
 DECLARE @INSTALLMENT_EFFECTIVE_DATE DATETIME --=  DATEADD(DAY,20,GETDATE())                
 DECLARE @END_EFFECTIVE_DATE DATETIME,@END_EXPIRY_DATE DATETIME,@LOB_ID INT,@TOTAL_INFORCE_PREMIUM DECIMAL(25,2),@CHANGE_INFORCE_PREMIUM DECIMAL(25,2),@TOTAL_PRM DECIMAL(25,2)               
 DECLARE @PAYMENT_MODE INT,@CURRENT_TERM INT,@PLAN_DESCRIPTION NVARCHAR(500),@PLAN_TYPE NCHAR(10),@NO_OF_PAYMENTS INT,@TRAN_PREMIUM DECIMAL(25,2) ,               
 @MONTHS_BETWEEN  SMALLINT,@PERCENT_BREAKDOWN1 DECIMAL(7,4),@PERCENT_BREAKDOWN2 DECIMAL(7,4),@PERCENT_BREAKDOWN3 DECIMAL(7,4),@PERCENT_BREAKDOWN4 DECIMAL(7,4)                
    ,@PERCENT_BREAKDOWN5 DECIMAL(7,4),@PERCENT_BREAKDOWN6 DECIMAL(7,4),@PERCENT_BREAKDOWN7 DECIMAL(7,4),@PERCENT_BREAKDOWN8 DECIMAL(7,4)                
    ,@PERCENT_BREAKDOWN9 DECIMAL(7,4),@PERCENT_BREAKDOWN10 DECIMAL(7,4),@PERCENT_BREAKDOWN11 DECIMAL(7,4),@PERCENT_BREAKDOWN12 DECIMAL(7,4)                
     ,@MODE_OF_PAYMENT INT,@IS_ACTIVE_PLAN NCHAR(1),@POLICY_EFFECTIVE_DATE DATETIME,@POL_EXP_DATE DATETIME,@INSTALLMENT_NO INT  ,@PREV_POLICY_VERSION_ID INT,@PREV_POLICY_PREMIUM DECIMAL(25,2)                
  DECLARE @EQUALLY_DISTRIBUTION_IN_INSTALLMENTS INT= 14672                
  DECLARE @ADJUST_IN_FIRST_INSTALLMENT INT = 14673                
  DECLARE @ADJUST_IN_LAST_INSTALLMENT INT = 14674                
  DECLARE @SEPARATE_INSTALLMENT INT = 14675,@PROCESS_ID INT,                
  @TRAN_TYPE_NBS_INPREOGRESS INT = 24,         ---- DECLARE CONSTANT PROCESS ID FOR PROCESS AS NEW BUSINESS IN PROGRESS AS TABLE POL_PROCESS_MASTER                    
     @TRAN_TYPE_NBS_COMMIT INT = 25,                                       ---- PRODESS ID FOR NBS COMMIT                    
     @TRAN_TYPE_REN_INPREOGRESS INT = 5,         ---- FOR RENEWAL IN PROGRESS                    
     @TRAN_TYPE_REN_COMMIT INT = 18,                                       ---- FOR RENEWAL COMMIT                    
     @TRAN_TYPE_END_INPREOGRESS INT = 3,           ---- FOR ENDORSMENT                     
     @TRAN_TYPE_END_COMMIT INT = 14 ,        
     @END_CO_APPLICANT INT  ,@PREV_END_EFF_DATE DATETIME ,@PREV_END_EFF_DAYS INT  ,    
 @END_REISSUE INT   ,
 -- START changed by praveer for itrack no 1761
 @NO_OF_INSTALLMENTS INT ,
 @INTEREST_RATE DECIMAL(12,4),
 @TOTAL_INTEREST_AMOUNT DECIMAL(12,4),
 @TOTAL_TRAN_INTEREST_AMOUNT DECIMAL(12,4),
 @FEES_PERCENTAGE DECIMAL(12,4),
 @MAXIMUM_LIMIT  DECIMAL(12,4),
 @TOTAL_FEES DECIMAL(12,4),
 @TOTAL_TRAN_FEES DECIMAL(12,4),
 @IOF_PERCENTAGE DECIMAL(12,4),
 @TOTAL_TAXES DECIMAL(12,4),
 @TOTAL_TRAN_TAXES  DECIMAL(12,4),
 @PREMIUM DECIMAL(12,4),
 @CO_INSURANCE INT
   --  END changes by praveer for itrack no 1761
 IF(ISNULL(@CALLED_FROM,'') <> 'RULES')        
  BEGIN          
            
   DELETE FROM  POL_INSTALLMENT_BOLETO where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
   DELETE FROM ACT_POLICY_CO_BILLING_DETAILS  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
   DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS_HISTORY   where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
   DELETE FROM ACT_POLICY_INSTALLMENT_DETAILS where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
   DELETE FROM ACT_POLICY_INSTALL_PLAN_DATA where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
  END          
  IF(@@ERROR<> 0)              
  GOTO ERRHANDLER              
                 
    --Select from POL_CUSTOMER_POLICY_LIST                
     SELECT @PAYMENT_MODE= DOWN_PAY_MODE,                
     @CURRENT_TERM= CURRENT_TERM,                
     @LOB_ID=POLICY_LOB,                
     @PLAN_ID= INSTALL_PLAN_ID,                
     @POLICY_EFFECTIVE_DATE=ISNULL(POLICY_EFFECTIVE_DATE,APP_EFFECTIVE_DATE),             
     @POL_EXP_DATE=ISNULL(POLICY_EXPIRATION_DATE,APP_EXPIRATION_DATE)  ,
     @IOF_PERCENTAGE=IOF_PERCENTAGE    ,
     @CO_INSURANCE=CO_INSURANCE       
     FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  --  changes by praveer for itrack no 1761  
     LEFT OUTER JOIN MNT_LOB_MASTER WITH(NOLOCK) ON MNT_LOB_MASTER.LOB_ID=POL_CUSTOMER_POLICY_LIST.POLICY_LOB
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                
            
  --Select from ACT_INSTALL_PLAN_DETAIL                 
     SELECT               
     @PLAN_DESCRIPTION=PLAND.PLAN_DESCRIPTION,                                      -- select selected PLAN description from ACT_INSTALL_PLAN_DETAIL                             
     @PLAN_TYPE=PLAND.PLAN_TYPE,             -- select PLAN Type                       
     @NO_OF_PAYMENTS=PLAND.NO_OF_PAYMENTS,                                          -- select No of Payment                             
     @MONTHS_BETWEEN =PLAND.MONTHS_BETWEEN,                              
     @PERCENT_BREAKDOWN1 =PLAND.PERCENT_BREAKDOWN1,                              
     @PERCENT_BREAKDOWN2=PLAND.PERCENT_BREAKDOWN2,                              
     @PERCENT_BREAKDOWN3=PLAND.PERCENT_BREAKDOWN3,                              
     @PERCENT_BREAKDOWN4=PLAND.PERCENT_BREAKDOWN4,                              
     @PERCENT_BREAKDOWN5=PLAND.PERCENT_BREAKDOWN5,                              
     @PERCENT_BREAKDOWN6=PLAND.PERCENT_BREAKDOWN6,                              
     @PERCENT_BREAKDOWN7=PLAND.PERCENT_BREAKDOWN7,                              
     @PERCENT_BREAKDOWN8=PLAND.PERCENT_BREAKDOWN8,                              
     @PERCENT_BREAKDOWN9=PLAND.PERCENT_BREAKDOWN9,                              
     @PERCENT_BREAKDOWN10=PLAND.PERCENT_BREAKDOWN10,                              
     @PERCENT_BREAKDOWN11=PLAND.PERCENT_BREAKDOWN11,                              
     @PERCENT_BREAKDOWN12=PLAND.PERCENT_BREAKDOWN12,                               
     @MODE_OF_PAYMENT=ISNULL(PLAND.PLAN_PAYMENT_MODE,0) ,                             
     @IS_ACTIVE_PLAN=PLAND.IS_ACTIVE                             
     --@INSTALLMENTS_IN_DOWN_PAYMENT=ISNULL(PLAND.NO_INS_DOWNPAY,0),                
     --@BASE_DATE_DOWN_PAYMENT=ISNULL(BASE_DATE_DOWNPAYMENT,0),         ---select base down payment date                
     --@DOWN_PAYMENT_PLAN=ISNULL(DOWN_PAYMENT_PLAN,0) ,                                          --select if downpayment installment,(0+N or 1+N)  o for no downpaymebt and 1 for 1 installment in downpayment                
     --@BDATE_INSTALL_NXT_DOWNPYMT=ISNULL(BDATE_INSTALL_NXT_DOWNPYMT,0),                
     --@DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT=ISNULL(DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT,0),                
     --@DUE_DAYS_DOWNPYMT=ISNULL(DUE_DAYS_DOWNPYMT,0),                
     --@DAYS_SUBSEQUENT_INSTALLMENTS=ISNULL(DAYS_SUBSEQUENT_INSTALLMENTS,0),                
     --@SUBSEQUENT_INSTALLMENTS_OPTION=ISNULL(SUBSEQUENT_INSTALLMENTS_OPTION,0),                
  --@MODE_OF_DOWNPAY=  MODE_OF_DOWNPAY                
     FROM ACT_INSTALL_PLAN_DETAIL PLAND WITH(NOLOCK)                            
      WHERE IDEN_PLAN_ID=@PLAN_ID                   
                
                
                
  --Select from POL_POLICY_PROCESS                
   IF EXISTS(SELECT PROCESS_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) where NEW_CUSTOMER_ID=@CUSTOMER_ID AND NEW_POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID AND  PROCESS_STATUS<> 'ROLLBACK')                
   BEGIN             
           
  SELECT @END_EFFECTIVE_DATE =         
   CASE WHEN PROCESS_ID = @TRAN_TYPE_REN_INPREOGRESS        
    THEN        
   ISNULL(PP.EFFECTIVE_DATETIME,POL.POL_VER_EFFECTIVE_DATE)        
   ELSE        
  ISNULL(PP.EFFECTIVE_DATETIME,@POLICY_EFFECTIVE_DATE)                
   END,        
   @PROCESS_ID=PROCESS_ID ,                
   @END_EXPIRY_DATE =         
   CASE WHEN PROCESS_ID = @TRAN_TYPE_REN_INPREOGRESS        
   THEN        
  ISNULL(PP.EXPIRY_DATE,POL.POL_VER_EXPIRATION_DATE)        
   ELSE        
   ISNULL(PP.EXPIRY_DATE,@POL_EXP_DATE)        
   END,        
   @PREV_POLICY_VERSION_ID = pp.POLICY_VERSION_ID    ,        
   @END_CO_APPLICANT = ISNULL(PP.CO_APPLICANT_ID,0),    
       @END_REISSUE = ENDORSEMENT_RE_ISSUE    
   FROM POL_POLICY_PROCESS PP WITH(NOLOCK) JOIN        
   POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)        
   ON        
  POL.CUSTOMER_ID = PP.CUSTOMER_ID AND         
  POL.POLICY_ID = PP.POLICY_ID and         
  POL.POLICY_VERSION_ID = PP.NEW_POLICY_VERSION_ID         
           
   where pp.NEW_CUSTOMER_ID=@CUSTOMER_ID               
   AND pp.NEW_POLICY_ID = @POLICY_ID               
   AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID AND  PP.PROCESS_STATUS<> 'ROLLBACK'                
   END                
   ELSE                
 SELECT @INSTALLMENT_EFFECTIVE_DATE = @POLICY_EFFECTIVE_DATE                
             
             
             
                
     IF(@PROCESS_ID = @TRAN_TYPE_NBS_INPREOGRESS OR @PROCESS_ID=@TRAN_TYPE_NBS_COMMIT)                        
  SELECT @TRAN_TYPE='NBS'                        
     ELSE  IF(@PROCESS_ID=@TRAN_TYPE_REN_INPREOGRESS OR @PROCESS_ID=@TRAN_TYPE_REN_COMMIT)                        
  SELECT @TRAN_TYPE='REN'                        
     ELSE  IF(@PROCESS_ID=@TRAN_TYPE_END_INPREOGRESS OR @PROCESS_ID=@TRAN_TYPE_END_COMMIT)                        
  SELECT @TRAN_TYPE='END'                                    
     ELSE IF (@PROCESS_ID = '' OR @PROCESS_ID IS NULL)                        ----if Policy Process Id null or blank then it consider as New Business                    
  SELECT @TRAN_TYPE='NBS'                  
                
                
  --DEFAULT INSTALLMENT EFFECTIVE DATE WILL 20 DAY AFTER ENDORSMENT EFFFECTIVE DATE ACCORDING TO (Master Policy Implementation_v3)                
        
          
        
                        
        IF(@PROCESS_ID = @TRAN_TYPE_END_INPREOGRESS )                
   SELECT @INSTALLMENT_EFFECTIVE_DATE = CONVERT(varchar(100),DATEADD(DAY,20,@END_EFFECTIVE_DATE),111)                
        ELSE IF(@PROCESS_ID = @TRAN_TYPE_REN_INPREOGRESS)           
   SELECT @INSTALLMENT_EFFECTIVE_DATE = CONVERT(varchar(100),DATEADD(DAY,20,@END_EFFECTIVE_DATE),111)                
        ELSE            
     SELECT @INSTALLMENT_EFFECTIVE_DATE = CONVERT(varchar(100),DATEADD(DAY,20,@INSTALLMENT_EFFECTIVE_DATE),111)                
                
                      
                        
          CREATE TABLE #tempRISK_PREMIUM (CO_APPLICANT_ID INT,PREMIUM DECIMAL(25,2),RISK_EFFECTIVE_DATE DATETIME                
          ,RISK_EXPIRY_DATETIME DATETIME,RISK_ID INT,COVERAGE_CODE_ID INT,IS_ACTIVE CHAR(15)  ,END_EFF_DAYS INT             
          )                
                   
                   
     --MAUTO  USE For monthly auto billing plan              
     --MAUTO  USE For monthly mannual billing plan              
     --MMANNUAL               
   --Select Risk premium                 
                 
        
   IF(@PROCESS_ID in(@TRAN_TYPE_END_INPREOGRESS,@TRAN_TYPE_END_COMMIT) AND UPPER(RTRIM(LTRIM(@PLAN_TYPE))) ='MAUTO') --For Endosment  if Billing plan is selected for auto billing              
  BEGIN          
   IF(@LOB_ID=18 OR @LOB_ID=17) --For Civil Liability Transportation                
    BEGIN           
     INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM,RISK_ID                
     ,RISK_EFFECTIVE_DATE,RISK_EXPIRY_DATETIME,COVERAGE_CODE_ID,IS_ACTIVE)         
                
     SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
     ,RISK.VEHICLE_ID,RISK.RISK_EFFECTIVE_DATE,RISK.RISK_EXPIRE_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE                
     FROM  POL_CIVIL_TRANSPORT_VEHICLES RISK WITH(NOLOCK) INNER JOIN                
     POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
     COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
     COV.POLICY_ID = RISK.POLICY_ID AND                 
     COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
     COV.RISK_ID = RISK.VEHICLE_ID              
     WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and         
     COV.POLICY_ID=@POLICY_ID and         
     COV.POLICY_VERSION_ID=@POLICY_VERSION_ID         
     AND RISK.IS_ACTIVE='Y'        
     AND RISK.CO_APPLICANT_ID = @END_CO_APPLICANT --i-track #948      
  
    UNION  
    --get deactivated risk premium while deactivated from button .after deactivate risk premium would be refund  
    SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.VEHICLE_ID,RISK.RISK_EFFECTIVE_DATE,RISK_EXPIRE_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE   
       FROM POL_PRODUCT_COVERAGES  COV WITH(NOLOCK)   
    JOIN  POL_CIVIL_TRANSPORT_VEHICLES   RISK WITH(NOLOCK)  
    ON   
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID and   
    COV.POLICY_ID = RISK.POLICY_ID and COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID AND   
    RISK.VEHICLE_ID = COV.RISK_ID  
    WHERE  RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID = @POLICY_ID  
    AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK.IS_ACTIVE= 'N'  
    and RISK.ORIGINAL_VERSION_ID = @POLICY_VERSION_ID     
          
           
      SELECT @PREV_END_EFF_DATE = PPP.EFFECTIVE_DATETIME,      
      --EXPIRY_DATE,      
      @PREV_END_EFF_DAYS =  DATEDIFF(DAY,PPP.EFFECTIVE_DATETIME,[EXPIRY_DATE]) --END_EFF_DAY      
      --ENDORSEMENT_NO,      
     -- PPP.NEW_POLICY_VERSION_ID       
      FROM       
      POL_POLICY_PROCESS PPP WITH(NOLOCK)      
      JOIN       
      (SELECT TOP 1 MAX(EFFECTIVE_DATETIME) EFFECTIVE_DATETIME,NEW_POLICY_VERSION_ID,CUSTOMER_ID ,POLICY_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND       
      EFFECTIVE_DATETIME <= (SELECT MAX( RISK_EXPIRY_DATETIME) FROM #tempRISK_PREMIUM       
      WHERE RISK_EXPIRY_DATETIME < @END_EFFECTIVE_DATE ) AND   PROCESS_ID = 14      
      GROUP BY NEW_POLICY_VERSION_ID ,CUSTOMER_ID ,POLICY_ID      
      ORDER BY NEW_POLICY_VERSION_ID DESC) a      
      ON a.NEW_POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID       
      and a.EFFECTIVE_DATETIME = PPP.EFFECTIVE_DATETIME AND       
      a.CUSTOMER_ID = PPP.CUSTOMER_ID AND       
      a.POLICY_ID = PPP.POLICY_ID AND       
      PPP.PROCESS_STATUS <> 'ROLLBACK'      
          
  UPDATE #tempRISK_PREMIUM SET       
  RISK_EFFECTIVE_DATE = CASE WHEN RISK_EXPIRY_DATETIME < @END_EFFECTIVE_DATE       
       THEN @PREV_END_EFF_DATE       
       ELSE @END_EFFECTIVE_DATE END        
  ,END_EFF_DAYS = CASE WHEN RISK_EXPIRY_DATETIME < @END_EFFECTIVE_DATE       
     THEN @PREV_END_EFF_DAYS END      
              
                
                
                                
     --GROUP BY CO_APPLICANT_ID                
       END                
   ELSE IF(@LOB_ID=21 OR @LOB_ID=34) -- Group Personal Accident for Passenger               
    BEGIN            
     INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM,RISK_ID                
     ,RISK_EFFECTIVE_DATE,RISK_EXPIRY_DATETIME,COVERAGE_CODE_ID,IS_ACTIVE)             
     SELECT RISK.APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM,COV.RISK_ID,@END_EFFECTIVE_DATE,@END_EXPIRY_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE            
     FROM  POL_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
     POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
     COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
     COV.POLICY_ID = RISK.POLICY_ID AND                 
     COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
     COV.RISK_ID = RISK.PERSONAL_INFO_ID                
     WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID and COV.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK.IS_ACTIVE='Y'                                                              
        AND RISK.APPLICANT_ID = @END_CO_APPLICANT  --i-track #948      
   UNION  
    --get deactivated risk premium while deactivated from button .after deactivate risk premium would be refund  
    SELECT RISK.APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.PERSONAL_INFO_ID,@END_EFFECTIVE_DATE,@END_EXPIRY_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE   
       FROM POL_PRODUCT_COVERAGES  COV WITH(NOLOCK)   
    JOIN  POL_PERSONAL_ACCIDENT_INFO   RISK WITH(NOLOCK)  
    ON   
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID and   
    COV.POLICY_ID = RISK.POLICY_ID and COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID   
    AND COV.RISK_ID = RISK.PERSONAL_INFO_ID  
    WHERE  RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID = @POLICY_ID   
    AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK.IS_ACTIVE= 'N'  
    and RISK.ORIGINAL_VERSION_ID = @POLICY_VERSION_ID  
          
                               
   END                
   ELSE IF(@LOB_ID=22) --Personal Accident for Passengers        
    BEGIN        
      
     INSERT INTO #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM,RISK_ID                
     ,RISK_EFFECTIVE_DATE,RISK_EXPIRY_DATETIME,COVERAGE_CODE_ID,IS_ACTIVE)             
     SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM,            
     COV.RISK_ID,RISK.START_DATE,            
     RISK.END_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE            
     FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
     POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
     COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
     COV.POLICY_ID = RISK.POLICY_ID AND                 
     COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
     COV.RISK_ID = RISK.PERSONAL_ACCIDENT_ID                
     WHERE COV.CUSTOMER_ID=@CUSTOMER_ID AND COV.POLICY_ID=@POLICY_ID             
     AND COV.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK.IS_ACTIVE='Y'            
     AND RISK.CO_APPLICANT_ID = @END_CO_APPLICANT   -- i-track #948      
     --GROUP BY APPLICANT_ID                    
     
     UNION  
    --get deactivated risk premium while deactivated from button .after deactivate risk premium would be refund  
    SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.PERSONAL_ACCIDENT_ID,RISK.START_DATE,RISK.END_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE   
       FROM POL_PRODUCT_COVERAGES  COV WITH(NOLOCK)   
    JOIN  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO   RISK WITH(NOLOCK)  
    ON   
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID and   
    COV.POLICY_ID = RISK.POLICY_ID and COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID   
    AND COV.RISK_ID = RISK.PERSONAL_ACCIDENT_ID  
    WHERE  RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID = @POLICY_ID   
    AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK.IS_ACTIVE= 'N'  
    and RISK.ORIGINAL_VERSION_ID = @POLICY_VERSION_ID  
      
    
                
      SELECT @PREV_END_EFF_DATE = PPP.EFFECTIVE_DATETIME,      
      --EXPIRY_DATE,      
      @PREV_END_EFF_DAYS =  DATEDIFF(DAY,PPP.EFFECTIVE_DATETIME,[EXPIRY_DATE]) --END_EFF_DAY      
      --ENDORSEMENT_NO,      
     -- PPP.NEW_POLICY_VERSION_ID       
      FROM       
      POL_POLICY_PROCESS PPP WITH(NOLOCK)      
      JOIN       
      (SELECT TOP 1 MAX(EFFECTIVE_DATETIME) EFFECTIVE_DATETIME,NEW_POLICY_VERSION_ID,CUSTOMER_ID ,POLICY_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND       
      EFFECTIVE_DATETIME <= (SELECT MAX( RISK_EXPIRY_DATETIME) FROM #tempRISK_PREMIUM       
      WHERE RISK_EXPIRY_DATETIME < @END_EFFECTIVE_DATE ) AND   PROCESS_ID = 14      
      GROUP BY NEW_POLICY_VERSION_ID ,CUSTOMER_ID ,POLICY_ID      
      ORDER BY NEW_POLICY_VERSION_ID DESC) a      
      ON a.NEW_POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID       
      and a.EFFECTIVE_DATETIME = PPP.EFFECTIVE_DATETIME AND       
      a.CUSTOMER_ID = PPP.CUSTOMER_ID AND       
      a.POLICY_ID = PPP.POLICY_ID AND       
      PPP.PROCESS_STATUS <> 'ROLLBACK'      
          
  UPDATE #tempRISK_PREMIUM SET       
  RISK_EFFECTIVE_DATE = CASE WHEN RISK_EXPIRY_DATETIME < @END_EFFECTIVE_DATE       
       THEN @PREV_END_EFF_DATE       
       ELSE @END_EFFECTIVE_DATE END        
  ,END_EFF_DAYS = CASE WHEN RISK_EXPIRY_DATETIME < @END_EFFECTIVE_DATE       
     THEN @PREV_END_EFF_DAYS END      
       
       
               
                
       --SELECT * FROM  #tempRISK_PREMIUM    
                           
       END               
  END                
   ELSE IF(@PROCESS_ID in(@TRAN_TYPE_END_INPREOGRESS,@TRAN_TYPE_END_COMMIT) AND @PLAN_TYPE ='MMANNUAL') --For Endosment  if Billing plan is selected for mannual billing              
  BEGIN          
   DECLARE  @MAX_RISK_ID INT = 0              
   IF(@LOB_ID=18 OR @LOB_ID=17) --For Civil Liability Transportation /facultative liability              
   BEGIN          
       SELECT @MAX_RISK_ID =  MAX(ISNULL(VEHICLE_ID,0))              
       FROM  POL_CIVIL_TRANSPORT_VEHICLES RISK WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID              
       AND POLICY_VERSION_ID = @PREV_POLICY_VERSION_ID               
                   
     --  IF(ISNULL(@END_REISSUE,0)<> 10963) --if endorsement is not re-issue endorsemnt                
   INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM,RISK_ID                
       ,RISK_EFFECTIVE_DATE,RISK_EXPIRY_DATETIME,COVERAGE_CODE_ID,IS_ACTIVE)         
       SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.VEHICLE_ID,RISK.RISK_EFFECTIVE_DATE,RISK_EXPIRE_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE                
       FROM  POL_CIVIL_TRANSPORT_VEHICLES RISK WITH(NOLOCK) INNER JOIN                
       POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
       COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
       COV.POLICY_ID = RISK.POLICY_ID AND                 
       COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
       COV.RISK_ID = RISK.VEHICLE_ID                
       WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID              
     AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID         
     AND RISK.VEHICLE_ID > ISNULL(@MAX_RISK_ID,0)
     AND RISK.IS_ACTIVE = 'Y'   --commented by Lalit ,May 20,2011 itrack # 948/1126 deactivated risk should consider for refund premium    
         AND RISK.CO_APPLICANT_ID = @END_CO_APPLICANT  --i-track #948      
     
  UNION  
    --get deactivated risk premium while deactivated from button .after deactivate risk premium would be refund  
    SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.VEHICLE_ID,RISK.RISK_EFFECTIVE_DATE,RISK_EXPIRE_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE   
       FROM POL_PRODUCT_COVERAGES  COV WITH(NOLOCK)   
    JOIN  POL_CIVIL_TRANSPORT_VEHICLES   RISK WITH(NOLOCK)  
    ON   
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID and   
    COV.POLICY_ID = RISK.POLICY_ID and COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID AND   
    RISK.VEHICLE_ID = COV.RISK_ID  
    WHERE  RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID = @POLICY_ID  
    AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK.IS_ACTIVE= 'N'  
    and RISK.ORIGINAL_VERSION_ID = @POLICY_VERSION_ID  
     
  UNION      --get deactivated risk premium.which is deactivated bia changing risk expiry date  
      SELECT a.CO_APPLICANT_ID,        
      --ISNULL(a.PREMIUM-b.PREMIUM,0) AS PREMIUM           
    CASE WHEN DATEDIFF(day,a.RISK_EXPIRE_DATE,@END_EFFECTIVE_DATE)>0        
    THEN          
     ROUND(CAST(          
     a.PREMIUM-(a.PREMIUM  * DATEDIFF(day,b.RISK_EFFECTIVE_DATE,a.RISK_EXPIRE_DATE)          
     / DATEDIFF(day,b.RISK_EFFECTIVE_DATE,b.RISK_EXPIRE_DATE))        
     AS DECiMAL(25,0)),2) *-1        
             
    ELSE         
    ISNULL(a.PREMIUM-b.PREMIUM,0)        
      END        
      AS PREMIUM        
                   
       ,a.VEHICLE_ID,a.RISK_EFFECTIVE_DATE,a.RISK_EXPIRE_DATE,a.COVERAGE_CODE_ID,a.IS_ACTIVE  FROM         
      (SELECT RISK.POLICY_VERSION_ID,RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.VEHICLE_ID,RISK.RISK_EFFECTIVE_DATE,RISK_EXPIRE_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE                
       FROM  POL_CIVIL_TRANSPORT_VEHICLES RISK WITH(NOLOCK) INNER JOIN                
       POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
       COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
       COV.POLICY_ID = RISK.POLICY_ID AND                 
       COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
       COV.RISK_ID = RISK.VEHICLE_ID                
       WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID              
     AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID         
     AND RISK.IS_ACTIVE = 'Y' AND ISNULL(COV.WRITTEN_PREMIUM,0) <> 0    
     )a        
      JOIN        
              
      (SELECT RISK.POLICY_VERSION_ID,RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.VEHICLE_ID,RISK.RISK_EFFECTIVE_DATE,RISK_EXPIRE_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE                
       FROM  POL_CIVIL_TRANSPORT_VEHICLES RISK WITH(NOLOCK) INNER JOIN                
       POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
       COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
       COV.POLICY_ID = RISK.POLICY_ID AND                 
       COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
       COV.RISK_ID = RISK.VEHICLE_ID                
       WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID              
     AND RISK.POLICY_VERSION_ID = @PREV_POLICY_VERSION_ID        
     AND RISK.IS_ACTIVE = 'Y' AND ISNULL(COV.WRITTEN_PREMIUM,0) <> 0         
     )b        
              
       on a.VEHICLE_ID = b.VEHICLE_ID        
       and ((a.PREMIUM<> b.PREMIUM and a.RISK_EXPIRE_DATE > @END_EFFECTIVE_DATE)  OR a.RISK_EXPIRE_DATE < @END_EFFECTIVE_DATE)        
      AND a.CO_APPLICANT_ID = @END_CO_APPLICANT  --i-track #948      
  
          
    END                   
   ELSE IF(@LOB_ID=22) --For Personal Accident for Passengers            
   BEGIN          
                    
      SELECT @MAX_RISK_ID =  MAX(ISNULL(PERSONAL_ACCIDENT_ID,0))              
      FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO            
      RISK WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID              
      AND POLICY_VERSION_ID = @PREV_POLICY_VERSION_ID         
          
      --IF(ISNULL(@END_REISSUE,0)<> 10963) --if endorsement is not re-issue endorsemnt    
    INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM,RISK_ID          
      ,RISK_EFFECTIVE_DATE,RISK_EXPIRY_DATETIME,COVERAGE_CODE_ID,IS_ACTIVE)         
      SELECT RISK.CO_APPLICANT_ID,        
      ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
      ,RISK.PERSONAL_ACCIDENT_ID,RISK.START_DATE,RISK.END_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE                
      FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
      POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
      COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
      COV.POLICY_ID = RISK.POLICY_ID AND                 
      COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
      COV.RISK_ID = RISK.PERSONAL_ACCIDENT_ID                
      WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID              
      AND COV.POLICY_VERSION_ID=@POLICY_VERSION_ID     
      AND RISK.IS_ACTIVE='Y'              
      AND RISK.PERSONAL_ACCIDENT_ID > ISNULL(@MAX_RISK_ID,0)            
         AND CO_APPLICANT_ID = @END_CO_APPLICANT        
           
      UNION  
    --get deactivated risk premium while deactivated from button .after deactivate risk premium would be refund  
    SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.PERSONAL_ACCIDENT_ID,RISK.START_DATE,RISK.END_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE   
       FROM POL_PRODUCT_COVERAGES  COV WITH(NOLOCK)   
    JOIN  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO   RISK WITH(NOLOCK)  
    ON   
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID and   
    COV.POLICY_ID = RISK.POLICY_ID and COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID   
    AND COV.RISK_ID = RISK.PERSONAL_ACCIDENT_ID  
    WHERE  RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID = @POLICY_ID   
    AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK.IS_ACTIVE= 'N'  
    and RISK.ORIGINAL_VERSION_ID = @POLICY_VERSION_ID --AND ISNULL(COV.WRITTEN_PREMIUM,0)<>0  
        
           
     /*    
     --calculate risk deactivation premium     
     (if risk expiry date is less then endorsment effective date/    
     deactivate precious risk in current endorsement by changing risk expiry date)    
     */    
     UNION        
     SELECT a.CO_APPLICANT_ID,        
      CASE WHEN DATEDIFF(day,a.END_DATE,@END_EFFECTIVE_DATE)>0        
    THEN          
     ROUND(CAST(          
     a.PREMIUM-(a.PREMIUM  * DATEDIFF(day,b.START_DATE,a.END_DATE)          
     / DATEDIFF(day,b.START_DATE,b.END_DATE))        
     AS DECiMAL(25,0)),2) *-1        
             
    ELSE         
    ISNULL(a.PREMIUM-b.PREMIUM,0)        
      END        
     AS PREMIUM           
      ,a.PERSONAL_ACCIDENT_ID,a.START_DATE,a.END_DATE,a.COVERAGE_CODE_ID,a.IS_ACTIVE  FROM         
     (SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
      ,RISK.PERSONAL_ACCIDENT_ID,RISK.START_DATE,RISK.END_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE        
      FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
      POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
      COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
      COV.POLICY_ID = RISK.POLICY_ID AND                 
      COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
      COV.RISK_ID = RISK.PERSONAL_ACCIDENT_ID                
      WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID              
    AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID         
    AND RISK.IS_ACTIVE = 'Y' AND RISK.CO_APPLICANT_ID = @END_CO_APPLICANT   AND ISNULL(COV.WRITTEN_PREMIUM,0) <> 0   
    )a        
     JOIN        
             
     (SELECT RISK.CO_APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
      ,RISK.PERSONAL_ACCIDENT_ID,RISK.START_DATE,RISK.END_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE        
      FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
      POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
      COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
      COV.POLICY_ID = RISK.POLICY_ID AND                 
      COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
      COV.RISK_ID = RISK.PERSONAL_ACCIDENT_ID                
      WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID              
    AND RISK.POLICY_VERSION_ID = @PREV_POLICY_VERSION_ID        
    AND RISK.IS_ACTIVE = 'Y' AND RISK.CO_APPLICANT_ID = @END_CO_APPLICANT     
    AND ISNULL(COV.WRITTEN_PREMIUM,0) <> 0  
    )b        
             
      on a.PERSONAL_ACCIDENT_ID = b.PERSONAL_ACCIDENT_ID        
      and ((a.PREMIUM<> b.PREMIUM and a.START_DATE > @END_EFFECTIVE_DATE) OR a.END_DATE < @END_EFFECTIVE_DATE)        
        AND a.CO_APPLICANT_ID = @END_CO_APPLICANT       
         
             
   END                       
   ELSE IF(@LOB_ID=21 OR @LOB_ID=34) --For Group personal accident for pessenger                
   BEGIN          
     SELECT @MAX_RISK_ID =  MAX(ISNULL(PERSONAL_INFO_ID,0))              
     FROM  POL_PERSONAL_ACCIDENT_INFO            
     RISK WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID              
     AND POLICY_VERSION_ID < @POLICY_VERSION_ID          
               
    INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM,RISK_ID              
      ,RISK_EFFECTIVE_DATE,RISK_EXPIRY_DATETIME,COVERAGE_CODE_ID,IS_ACTIVE)        
    SELECT RISK.APPLICANT_ID,        
    ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM,             
                   
    RISK.PERSONAL_INFO_ID,@END_EFFECTIVE_DATE AS RISK_EFFECTIVE_DATE,        
    DATEADD(DAY,30,@END_EXPIRY_DATE) AS RISK_EXPIRY_DATETIME,              
      COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE               
      FROM  POL_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
      POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
      COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
      COV.POLICY_ID = RISK.POLICY_ID AND                 
      COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
      COV.RISK_ID = RISK.PERSONAL_INFO_ID                
      WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID               
      and COV.POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK.IS_ACTIVE='Y'     
      AND RISK.PERSONAL_INFO_ID > ISNULL(@MAX_RISK_ID,0)              
      AND RISK.APPLICANT_ID = @END_CO_APPLICANT         
   UNION  
    --get deactivated risk premium while deactivated from button .after deactivate risk premium would be refund  
    SELECT RISK.APPLICANT_ID,ISNULL(COV.WRITTEN_PREMIUM,0) AS PREMIUM                
       ,RISK.PERSONAL_INFO_ID,@END_EFFECTIVE_DATE,@END_EXPIRY_DATE,COV.COVERAGE_CODE_ID,RISK.IS_ACTIVE   
       FROM POL_PRODUCT_COVERAGES  COV WITH(NOLOCK)   
    JOIN  POL_PERSONAL_ACCIDENT_INFO   RISK WITH(NOLOCK)  
    ON   
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID and   
    COV.POLICY_ID = RISK.POLICY_ID and COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID   
    AND COV.RISK_ID = RISK.PERSONAL_INFO_ID  
    WHERE  RISK.CUSTOMER_ID = @CUSTOMER_ID AND RISK.POLICY_ID = @POLICY_ID   
    AND RISK.POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK.IS_ACTIVE= 'N'  
    and RISK.ORIGINAL_VERSION_ID = @POLICY_VERSION_ID  
      
      
        
   END                
  END              
   ELSE               
  BEGIN          
    IF(@LOB_ID=18 OR @LOB_ID=17) --For Civil Liability Transportation/facultative liability              
       BEGIN              
    INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM) SELECT RISK.CO_APPLICANT_ID,SUM(ISNULL(COV.WRITTEN_PREMIUM,0)) PREMIUM                
    FROM  POL_CIVIL_TRANSPORT_VEHICLES RISK WITH(NOLOCK) INNER JOIN                
    POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
    COV.POLICY_ID = RISK.POLICY_ID AND                 
    COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
    COV.RISK_ID = RISK.VEHICLE_ID                
    WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID and COV.POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK.IS_ACTIVE='Y'     
    GROUP BY CO_APPLICANT_ID                
     END                
    ELSE IF(@LOB_ID=21 OR @LOB_ID=34)   --Added By Lalit Dec 28,2010 For impliment new product group life as master policy            
       BEGIN              
    INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM) SELECT RISK.APPLICANT_ID,SUM(ISNULL(COV.WRITTEN_PREMIUM,0)) PREMIUM                
    FROM  POL_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
    POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
    COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
    COV.POLICY_ID = RISK.POLICY_ID AND                 
    COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
    COV.RISK_ID = RISK.PERSONAL_INFO_ID                
    WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID and COV.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK.IS_ACTIVE='Y'                                                              
    GROUP BY APPLICANT_ID                
     END                
    ELSE IF(@LOB_ID=22)   --Personal Accident for Passengers, Added By Lalit dec 04,2011            
       BEGIN              
  INSERT INTO  #tempRISK_PREMIUM(CO_APPLICANT_ID,PREMIUM) SELECT RISK.CO_APPLICANT_ID,SUM(ISNULL(COV.WRITTEN_PREMIUM,0)) PREMIUM                
  FROM  POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK WITH(NOLOCK) INNER JOIN                
  POL_PRODUCT_COVERAGES COV WITH(NOLOCK) ON                
  COV.CUSTOMER_ID = RISK.CUSTOMER_ID AND                 
  COV.POLICY_ID = RISK.POLICY_ID AND                 
  COV.POLICY_VERSION_ID = RISK.POLICY_VERSION_ID  AND                
  COV.RISK_ID = RISK.PERSONAL_ACCIDENT_ID                
  WHERE COV.CUSTOMER_ID=@CUSTOMER_ID and COV.POLICY_ID=@POLICY_ID and COV.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK.IS_ACTIVE='Y'                                                              
  GROUP BY CO_APPLICANT_ID                
     END                
    END                
                    
     --SELECT * FROM #tempRISK_PREMIUM          
    --Calculate Prorated Amount                
    DECLARE @rskCO_APP_ID INT,                
    @rskRISK_ID INT,                
    @rskPRO_AMOUNT DECIMAL(25,2),                
    @RISK_AMOUNT DECIMAL(25,2),                
    @rskEFFECTIVE_DATE DATETIME,               
    @rskEXPIRY_DATE DATETIME,                
    @RISK_EFFECTIVE_DAYS INT,                
    @END_EFFEVTIVE_DAYS INT,                
    @TOTAL_COUNT INT = 0,                
    @riskCOVERAGE_CODE_ID INT,                    
    @COUNTER INT = 1                
                    
                    
    CREATE TABLE #tempProratePremium (CO_APPLICANT_ID INT,PREMIUM DECIMAL(25,2),TRAN_PREMIUM DECIMAL(25,2))                
    --CREATE TABLE #tempRiskProAmt (CO_APPLICANT_ID INT,PREMIUM DECIMAL(12,2))                
    --CREATE TABLE #tempEndRiskPremium (CO_APPLICANT_ID INT,PREMIUM DECIMAL(12,2))                
       --SELECT  @LOB_ID             
    --SELECT * FROM #tempRISK_PREMIUM                
   --- AND @PLAN_TYPE ='MAUTO'              
                  
    IF EXISTS(SELECT RISK_ID  FROM #tempRISK_PREMIUM)                  
        BEGIN         
   IF(@PROCESS_ID = @TRAN_TYPE_END_INPREOGRESS  AND  @PLAN_TYPE ='MAUTO')                
    BEGIN          
        
  UPDATE  #tempRISK_PREMIUM SET  PREMIUM = (PREMIUM - (PREMIUM *DATEDIFF(DAY,RISK_EFFECTIVE_DATE,RISK_EXPIRY_DATETIME)/ END_EFF_DAYS))*-1      
  WHERE RISK_EXPIRY_DATETIME < @END_EFFECTIVE_DATE       
  --SELECT * FROM #tempRISK_PREMIUM      
        
    INSERT INTO #tempProratePremium(CO_APPLICANT_ID,PREMIUM,TRAN_PREMIUM)         
      SELECT CO_APPLICANT_ID,SUM(PREMIUM),SUM(PREMIUM) FROM #tempRISK_PREMIUM  GROUP BY  CO_APPLICANT_ID              
      SELECT @TOTAL_RISK_PREMIUM =  SUM(PREMIUM) FROM #tempProratePremium          
      
    END        
   ELSE IF(@PROCESS_ID = @TRAN_TYPE_END_INPREOGRESS  AND  @PLAN_TYPE ='MMANNUAL')                
     BEGIN          
      INSERT INTO #tempProratePremium(CO_APPLICANT_ID,PREMIUM,TRAN_PREMIUM)         
      SELECT CO_APPLICANT_ID,SUM(PREMIUM),SUM(PREMIUM) FROM #tempRISK_PREMIUM  GROUP BY  CO_APPLICANT_ID              
      SELECT @TOTAL_RISK_PREMIUM =  SUM(PREMIUM) FROM #tempProratePremium          
      
     END          
   ELSE                
     BEGIN          
      INSERT INTO #tempProratePremium(CO_APPLICANT_ID,PREMIUM,TRAN_PREMIUM) SELECT CO_APPLICANT_ID,PREMIUM,PREMIUM FROM #tempRISK_PREMIUM                 
     END                
                       
                       
                     
                   
      IF(@PROCESS_ID = @TRAN_TYPE_END_INPREOGRESS AND (@PREV_POLICY_VERSION_ID <> 0 OR  @PREV_POLICY_VERSION_ID IS NOT NULL))                 
     BEGIN              
                    
                    
       SELECT @TRAN_PREMIUM = SUM(ISNULL(PREMIUM,0)) FROM  #tempProratePremium               
       SELECT @PREV_POLICY_PREMIUM = TOTAL_PREMIUM FROM ACT_POLICY_INSTALL_PLAN_DATA  WITH(NOLOCK)              
       WHERE CUSTOMER_ID =@CUSTOMER_ID AND               
       POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @PREV_POLICY_VERSION_ID                     
              
       SELECT @TOTAL_PRM = @TOTAL_INFORCE_PREMIUM;              
       SELECT @TOTAL_PREMIUM = ISNULL(@TRAN_PREMIUM,0)+ ISNULL(@PREV_POLICY_PREMIUM,0)              
       SELECT @CHANGE_INFORCE_PREMIUM = @TOTAL_PRM--ISNULL(@PREV_POLICY_PREMIUM - @TOTAL_PRM,0)              
                     
       SELECT  @TOTAL_INFORCE_PREMIUM =  @TOTAL_INFORCE_PREMIUM + ISNULL(@PREV_POLICY_PREMIUM,0)              
                     
      END              
    ELSE              
      BEGIN                
       SELECT @TOTAL_PREMIUM = SUM(ISNULL(PREMIUM,0)) FROM  #tempProratePremium WITH(NOLOCK)                
       SELECT @TRAN_PREMIUM =  SUM(ISNULL(TRAN_PREMIUM,0)) FROM  #tempProratePremium WITH(NOLOCK)                
       SELECT @TOTAL_INFORCE_PREMIUM = @TOTAL_PREMIUM              
       SELECT @CHANGE_INFORCE_PREMIUM = 0              
      END              
                      
    IF(@CALLED_FROM = 'RULES')                
     BEGIN         
      
  IF (@PROCESS_ID IS NOT NULL AND @PROCESS_ID<>0)  
   SELECT @TOTAL_RISK_PREMIUM =  SUM(PREMIUM) FROM #tempProratePremium  WHERE CO_APPLICANT_ID = @END_CO_APPLICANT         
  ELSE   
   SELECT @TOTAL_RISK_PREMIUM =  SUM(PREMIUM) FROM #tempProratePremium      
   
   RETURN           
      
     END                
    ELSE                
     BEGIN                
        
        -- Start changes by praveer for itrack no 1761  

   IF(@CO_INSURANCE<>14549)
   BEGIN
     SELECT @NO_OF_PAYMENTS=ISNULL(NO_OF_PAYMENTS,0) FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK) WHERE IDEN_PLAN_ID=@PLAN_ID
     SELECT TOP 1 @INTEREST_RATE=ISNULL(INTEREST_RATE,0),@NO_OF_INSTALLMENTS=ISNULL(NO_OF_INSTALLMENTS,0) FROM MNT_INTEREST_RATES WITH(NOLOCK) WHERE  @POLICY_EFFECTIVE_DATE >= RATE_EFFECTIVE_DATE  AND @NO_OF_PAYMENTS=NO_OF_INSTALLMENTS  AND INTEREST_TYPE='15009' ORDER BY RATE_EFFECTIVE_DATE DESC
     IF(ISNULL(@INTEREST_RATE,0)=0)
     BEGIN
     RETURN -10
     END     
     IF(@NO_OF_INSTALLMENTS>0)
     BEGIN
     SELECT @TOTAL_INTEREST_AMOUNT=((ISNULL(@INTEREST_RATE,0)*ISNULL(@TRAN_PREMIUM,0))*ISNULL(@NO_OF_INSTALLMENTS,0))-ISNULL(@TRAN_PREMIUM,0)
     SELECT @TOTAL_TRAN_INTEREST_AMOUNT=((@INTEREST_RATE*@TRAN_PREMIUM)*@NO_OF_INSTALLMENTS)-@TRAN_PREMIUM 
     END
    
     SELECT @FEES_PERCENTAGE=ISNULL(FEES_PERCENTAGE,0),@MAXIMUM_LIMIT=ISNULL(MAXIMUM_LIMIT,0) FROM MNT_SYSTEM_PARAMS WITH(NOLOCK)   
      SELECT @TOTAL_FEES=ISNULL(@FEES_PERCENTAGE,0)*ISNULL(@TRAN_PREMIUM,0)/100
     IF(@TOTAL_FEES >@MAXIMUM_LIMIT)
     BEGIN
     SELECT @TOTAL_FEES=@MAXIMUM_LIMIT
     END
      SELECT @TOTAL_TRAN_FEES=ISNULL(@FEES_PERCENTAGE,0)*ISNULL(@TRAN_PREMIUM,0)/100
     IF(@TOTAL_TRAN_FEES >@MAXIMUM_LIMIT)
     BEGIN
     SELECT @TOTAL_TRAN_FEES=@MAXIMUM_LIMIT
     END   
     
      IF(@TOTAL_INTEREST_AMOUNT < 0 or @LOB_ID =34)
     BEGIN
     SELECT @TOTAL_INTEREST_AMOUNT=0  
     SELECT @TOTAL_TRAN_INTEREST_AMOUNT=0  
     END    
      IF(@TRAN_PREMIUM < 0 or @LOB_ID =34)
     BEGIN
     SELECT @TOTAL_INTEREST_AMOUNT=0   
     SELECT @TOTAL_FEES=0    
     SELECT @TOTAL_TRAN_INTEREST_AMOUNT=0
     SELECT @TOTAL_TRAN_FEES=0
    
     END
      SELECT @TOTAL_TAXES=ISNULL((ISNULL(@IOF_PERCENTAGE,0)*(ISNULL(@TRAN_PREMIUM,0) +ISNULL(@TOTAL_TRAN_INTEREST_AMOUNT,0) +ISNULL(@TOTAL_TRAN_FEES,0)))/100,0)
     SELECT @TOTAL_TRAN_TAXES=ISNULL((ISNULL(@IOF_PERCENTAGE,0)*(ISNULL(@TRAN_PREMIUM,0) +ISNULL(@TOTAL_TRAN_INTEREST_AMOUNT,0) +ISNULL(@TOTAL_TRAN_FEES,0)))/100,0)  
     IF(@TRAN_PREMIUM < 0 or @LOB_ID =34)
     BEGIN
      SELECT @TOTAL_TAXES=0
      SELECT @TOTAL_TRAN_TAXES=0
      END
     END
     -- END changes by praveer for itrack no 1761     
      --SELECT * FROM #tempProratePremium            
       INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA (                
       POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,PLAN_ID,APP_ID,APP_VERSION_ID,PLAN_DESCRIPTION,                
       PLAN_TYPE,NO_OF_PAYMENTS,MONTHS_BETWEEN,PERCENT_BREAKDOWN1,PERCENT_BREAKDOWN2,PERCENT_BREAKDOWN3,                
       PERCENT_BREAKDOWN4,PERCENT_BREAKDOWN5,PERCENT_BREAKDOWN6,PERCENT_BREAKDOWN7,PERCENT_BREAKDOWN8,                
       PERCENT_BREAKDOWN9,PERCENT_BREAKDOWN10,PERCENT_BREAKDOWN11,PERCENT_BREAKDOWN12,                
       MODE_OF_PAYMENT,CURRENT_TERM,IS_ACTIVE_PLAN,TOTAL_PREMIUM,                
       TOTAL_INTEREST_AMOUNT,TOTAL_FEES,TOTAL_TAXES,TOTAL_AMOUNT,TRAN_TYPE,TOTAL_TRAN_PREMIUM,                
       TOTAL_TRAN_INTEREST_AMOUNT,TOTAL_TRAN_FEES,TOTAL_TRAN_TAXES,TOTAL_TRAN_AMOUNT,CREATED_BY,                
       CREATED_DATETIME,TOTAL_CHANGE_INFORCE_PRM,PRM_DIST_TYPE,                
       TOTAL_INFO_PRM                
       )values                
       (@POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID,@PLAN_ID,@POLICY_ID,@POLICY_VERSION_ID,@PLAN_DESCRIPTION,                
       @PLAN_TYPE,@NO_OF_PAYMENTS,@MONTHS_BETWEEN,@PERCENT_BREAKDOWN1,@PERCENT_BREAKDOWN2,@PERCENT_BREAKDOWN3,              
       @PERCENT_BREAKDOWN4,@PERCENT_BREAKDOWN5,@PERCENT_BREAKDOWN6,@PERCENT_BREAKDOWN7,@PERCENT_BREAKDOWN8,              
       @PERCENT_BREAKDOWN9,@PERCENT_BREAKDOWN10,@PERCENT_BREAKDOWN11,@PERCENT_BREAKDOWN12,              
       @MODE_OF_PAYMENT,@CURRENT_TERM,@IS_ACTIVE_PLAN,@TOTAL_PREMIUM,ISNULL(@TOTAL_INTEREST_AMOUNT,0),ISNULL(@TOTAL_FEES,0),ISNULL(@TOTAL_TAXES,0),@TOTAL_PREMIUM,                
       @TRAN_TYPE,@TRAN_PREMIUM,ISNULL(@TOTAL_TRAN_INTEREST_AMOUNT,0),ISNULL(@TOTAL_TRAN_FEES,0),ISNULL(@TOTAL_TRAN_TAXES,0),
       @TRAN_PREMIUM,@CREATED_BY,@CREATED_DATETIME,@CHANGE_INFORCE_PREMIUM,@SEPARATE_INSTALLMENT              
       ,@TOTAL_INFORCE_PREMIUM                
       )                
      IF(@@ERROR<> 0)              
        GOTO ERRHANDLER              
         -- print 'vhjg'           
        --i-track #638         
        --installment no should be start form 1 for each transaction                  
      --IF EXISTS(SELECT INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID)                
      --BEGIN                
      -- SELECT @INSTALLMENT_NO = --ISNULL(MAX(INSTALLMENT_NO),1)+1 FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                
                         
      --END                
      --ELSE                
       SELECT @INSTALLMENT_NO = 1                
                 --SELECT @INSTALLMENT_NO        
        SELECT   @PREMIUM=ISNULL(@TRAN_PREMIUM,0)+ ISNULL(@TOTAL_TRAN_INTEREST_AMOUNT,0)+ ISNULL(@TOTAL_FEES,0) +ISNULL(@TOTAL_TRAN_TAXES,0)   
       INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS(POLICY_ID,                
        POLICY_VERSION_ID,CUSTOMER_ID,APP_ID,                
        APP_VERSION_ID,INSTALLMENT_AMOUNT,INSTALLMENT_EFFECTIVE_DATE,                
        RELEASED_STATUS,INSTALLMENT_NO,RISK_TYPE,PAYMENT_MODE,                
        CURRENT_TERM,PERCENTAG_OF_PREMIUM,INTEREST_AMOUNT,FEE,                
        TAXES,TOTAL,TRAN_INTEREST_AMOUNT,TRAN_FEE,TRAN_TAXES,TRAN_TOTAL,                
        BOLETO_NO,IS_BOLETO_GENRATED,CREATED_BY,CREATED_DATETIME,                
        MODIFIED_BY,LAST_UPDATED_DATETIME,TRAN_PREMIUM_AMOUNT,CO_APPLICANT_ID)                
        SELECT @POLICY_ID,@POLICY_VERSION_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID, PREMIUM,                
        @INSTALLMENT_EFFECTIVE_DATE,'N',@INSTALLMENT_NO,null,@PAYMENT_MODE,@CURRENT_TERM        
        ,100.00--null--for master policy installment amount will be total amount of applicant premium        
        ,ISNULL(@TOTAL_INTEREST_AMOUNT,0),ISNULL(@TOTAL_FEES,0),ISNULL(@TOTAL_TAXES,0),@PREMIUM,
        ISNULL(@TOTAL_TRAN_INTEREST_AMOUNT,0),ISNULL(@TOTAL_TRAN_FEES,0),ISNULL(@TOTAL_TRAN_TAXES,0),TRAN_PREMIUM,                
        null,null,@CREATED_BY,@CREATED_DATETIME,NULL,null,TRAN_PREMIUM,CO_APPLICANT_ID                
      FROM #tempProratePremium                 
                    
      IF(@@ERROR<> 0)              
        GOTO ERRHANDLER              
                     
     END              
                   
     END --end here IF EXISTS(SELECT RISK_ID  FROM #tempRISK_PREMIUM)                 
    --drop temporary tables                
    DROP  TABLE #tempProratePremium                 
    DROP  TABLE #tempRISK_PREMIUM                 
    --DROP TABLE #tempRiskProAmt                
    --DROP TABLE #tempEndRiskPremium                
                
    EXEC Proc_InsertInstallmentExpireDate @CUSTOMER_ID, @POLICY_ID , @POLICY_VERSION_ID             
    EXEC Proc_AddDefaultClause  @CUSTOMER_ID, @POLICY_ID , @POLICY_VERSION_ID  ,@CREATED_BY            
  --COMMIT TRAN              
        RETURN 1         
    ERRHANDLER:                
      RETURN -1                               
 END           
 --GO           
 --DECLARE @CUSTOMER_ID INT= 28070,@POLICY_ID INT =408,@POLICY_VERSION_ID INT = 3       
 --,@PRORATE_AMOUNT DECIMAL(25,2) = NULL,@CALLED_FROM  NVARCHAR(15) ='RULES'  
 --EXEC Proc_Generate_MasterPolicyInstallments         
 --@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,398,null,null,null,@CALLED_FROM,@PRORATE_AMOUNT OUT                            
 --SELECT @PRORATE_AMOUNT  
 --select * from ACT_POLICY_INSTALLMENT_DETAILS where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID        
 --AND POLICY_VERSION_ID = @POLICY_VERSION_ID        
         
     
 --ROLLBACK TRAN        
    
  
GO

