IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCancellationNoticeNonPayDBData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCancellationNoticeNonPayDBData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                            
Proc Name       :  dbo.Proc_GetCancellationNoticeNonPayDBData                                                                                            
Created by      :  Mohit Agarwal                                                                                            
Date            :  14-Apr-2007                                                                                            
Purpose         :  To get cancellation notice data                                                                                            
Revison History :                                                                                            
Used In         :    Wolverine                                                                                            
                                                                                            
Modified By:                                                                                          
Modified Date :                                                                                           
                                    
Reviewed By  : Anurag Verma                                    
Reviewed On  : 12-07-2007                                    
                                                                                               
Modified By:      Pravesh K Chandel                                                                                  
Modified Date :   7 aug 2007                                                                                        
                                 
Modified By  : Ravindra  
Modified Date : 03-07-2008  
Purpose   : iTrack 3821           
------------------------------------------------------------                                                                                            
Date     Review By          Comments                                                                                            
------   ------------       -------------------------*/                                                                                            
--drop PROC Dbo.Proc_GetCancellationNoticeNonPayDBData  2156,128,2 
create PROC [dbo].[Proc_GetCancellationNoticeNonPayDBData]                                                            
(                                                                                                  
 @CUSTOMER_ID int,                                                                                  
 @POLICY_ID int,                                                                                  
 @POLICY_VERSION_ID int                                       
)                                                                                                  
AS                                                                                                  
BEGIN                
  
DECLARE @MINIMUM_DUE Decimal(18,2),                                                      
   @TOTAL_DUE Decimal(18,2),         
   @TOTAL_PREMIUM_DUE Decimal(18,2) ,                                                
   @AMOUNT_PAID Decimal(18,2),  
   @TOTAL_FEE DECIMAL(18,2),  
   @FIRST_INS_FEE  DECIMAL(18,2),                                               
   @INSTALL_PLAN_ID Int,                                                       
   @CURRENT_VERSION_ID  Int,                                                      
   @MINDAYS_PREMIUM  Int,                                                      
   @DAYS_DUE_PREM_NOTICE_PRINTD Int,                                                      
   @NON_SUFFICIENT_FUND_FEES Decimal(18,2),                                    
   @REINSTATEMENT_FEES Decimal(18,2),       
   @LATE_FEES Decimal(18,2),                                       
   @INSTALLMENT_FEES Decimal(18,2),                                
   @BILL_DATE Datetime,            
   @CURRENT_TERM SmallInt,                                                  
   @DUE_DATE DateTime,                                       
   @CANCELLATION_OPTION int,                                                  
   @CANCELLATION_DATE DateTime,                                
   @OCRA varchar(25),                  
   @PLAN_DESCRIPTION NVARCHAR(35)   ,  
   @SHOW_INSTALLMENTS Char(1)   ,  
   @LOB_ID   Int   ,   
   @ADD_INT_ID  Char(2)  ,  
   @DWELLING_ID  Int,  
   @IS_HOME_EMP  Bit  
  
SET  @ADD_INT_ID  =  0                                    
    
  
DECLARE @Equity  Int,  
  @Flat  Int,  
  @ProRata Int  
SET @Equity  = 11996  
SET @Flat  = 11995  
SET @ProRata = 11994  
                                                    
SET @BILL_DATE = DATEADD(dd,1, GETDATE())                     
  
--Get Base Policy Version ID for current Version                                                           
SELECT @CURRENT_VERSION_ID =ISNULL(MAX(NEW_POLICY_VERSION_ID),0)   
FROM POL_POLICY_PROCESS  (nolock)                                                          
WHERE CUSTOMER_ID=@CUSTOMER_ID                                                       
 AND POLICY_ID=@POLICY_ID                           
 AND PROCESS_ID IN(18,25,32)                                                               
 AND ISNULL(REVERT_BACK,'N')  <> 'Y'  --Ravindra(06-24-2008)  
                                                            
--Ravindra(03-07-2008) Why to make it 1   
--IF(@CURRENT_VERSION_ID=0)                                                            
-- SET @CURRENT_VERSION_ID=1                                                            
                            
SELECT  @OCRA = CPL.POLICY_NUMBER,                                
  @INSTALL_PLAN_ID = CPL.INSTALL_PLAN_ID ,                                                      
  @MINDAYS_PREMIUM = INS_MASTER.MINDAYS_PREMIUM,                                                      
  @DAYS_DUE_PREM_NOTICE_PRINTD = INS_MASTER.DAYS_DUE_PREM_NOTICE_PRINTD,                                                      
  @NON_SUFFICIENT_FUND_FEES = INS_MASTER.NON_SUFFICIENT_FUND_FEES,                                                       
  @REINSTATEMENT_FEES  = INS_MASTER.REINSTATEMENT_FEES ,                                                     
  @LATE_FEES   = INS_MASTER.LATE_FEES,                                                      
  @INSTALLMENT_FEES       = INSTALLMENT_FEES,                  
  @PLAN_DESCRIPTION       = INS_MASTER.PLAN_DESCRIPTION,   
  @SHOW_INSTALLMENTS   = CASE ISNULL(INS_MASTER.SYSTEM_GENERATED_FULL_PAY,0) WHEN 1 THEN 'N' ELSE 'Y' END    ,                                              
  @CURRENT_TERM           = CPL.CURRENT_TERM,                   
  @LOB_ID     = CPL.POLICY_LOB ,                
  @BILL_DATE   =    DATEADD(DD,INS_MASTER.DAYS_DUE_PREM_NOTICE_PRINTD, GETDATE() ) ,  
--Ravindra(10-14-2008): Pick mortagee from latest version                                                     
--  @ADD_INT_ID    =  ISNULL(ADD_INT_ID,0),  
--  @DWELLING_ID   =  ISNULL(DWELLING_ID , 0) ,  
  @IS_HOME_EMP   =  ISNULL(CPL.IS_HOME_EMP, 0)                                                       
  FROM POL_CUSTOMER_POLICY_LIST CPL (nolock)                                                     
INNER JOIN ACT_INSTALL_PLAN_DETAIL INS_MASTER (nolock)                                                     
ON INS_MASTER.IDEN_PLAN_ID = CPL.INSTALL_PLAN_ID                                                      
WHERE CPL.CUSTOMER_ID =@CUSTOMER_ID                                                       
AND   CPL.POLICY_ID = @POLICY_ID                                                  
AND   CPL.POLICY_VERSION_ID = @CURRENT_VERSION_ID    
  
--Ravindra(10-14-2008): Pick mortagee from latest version                                                     
DECLARE @MAX_VERSION_ID INt   
SELECT @MAX_VERSION_ID = MAX(POLICY_VERSION_ID ) FROM POL_CUSTOMER_POLICY_LIST   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
  
SELECT  @ADD_INT_ID    =  ISNULL(ADD_INT_ID,0),  
  @DWELLING_ID   =  ISNULL(DWELLING_ID , 0)   
FROM POL_CUSTOMER_POLICY_LIST   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND POLICY_VERSION_ID = @MAX_VERSION_ID                      
  
                           
--Charge Late Fees  
exec Proc_InsertACT_CUSTOMER_OPEN_ITEMS @LATE_FEES , @CUSTOMER_ID, @POLICY_ID , @POLICY_VERSION_ID  , null , 38                                           
  
    
              
SELECT @CANCELLATION_OPTION = CANCELLATION_OPTION, @CANCELLATION_DATE = PPP.EFFECTIVE_DATETIME, @DUE_DATE = PPP.DUE_DATE    
FROM POL_POLICY_PROCESS PPP (nolock)   
WHERE PPP.CUSTOMER_ID= @CUSTOMER_ID AND PPP.POLICY_ID= @POLICY_ID AND PPP.NEW_POLICY_VERSION_ID= @POLICY_VERSION_ID               
 AND PPP.PROCESS_STATUS = 'PENDING'            
  
                    
              
              
--Use Accounting SP for Total and Minimum Due 6-Nov-2007              
CREATE TABLE #AMOUNT_DUE   
(  
 MINIMUM_DUE  DECIMAL(18,2),                                              
 TOTAL_DUE  DECIMAL(18,2),                                              
 AGENCY_ID  INT,              
 AGENCYCODE  VARCHAR(20),  
 PREM   Decimal(18,2) ,   
 FEE    Decimal(18,2),              
 FIRST_INS_FEE Decimal(18,2),  
 TOTAL_PREMIUM_DUE Decimal(18,2)   
)            
INSERT INTO #AMOUNT_DUE exec Proc_GetTotalAndMinimumDue @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID , @DUE_DATE              
              
SELECT @TOTAL_DUE=TOTAL_DUE, @MINIMUM_DUE=MINIMUM_DUE, @TOTAL_FEE=FEE, @FIRST_INS_FEE=FIRST_INS_FEE ,  
   @TOTAL_PREMIUM_DUE  = TOTAL_PREMIUM_DUE  
FROM #AMOUNT_DUE WITH(NOLOCK)              
DROP TABLE #AMOUNT_DUE              
          
SET @OCRA = @OCRA + ' X ' + RIGHT('000000' + CONVERT(VARCHAR,CONVERT(INT,ISNULL(@TOTAL_DUE,0) * 100)),6)   
  + RIGHT('000000' + CONVERT(VARCHAR,CONVERT(INT,ISNULL(@MINIMUM_DUE,0) * 100)),6)                                  
  
           
  
DECLARE @NSF_MESSAGE Varchar(200),  
  @LF_MESSAGE  Varchar(200)  
  
SET @NSF_MESSAGE = ''  
SET @LF_MESSAGE  = ''  
      
IF  @NON_SUFFICIENT_FUND_FEES  IS NOT NULL                          
BEGIN                                           
  SET @NSF_MESSAGE = 'A $' +  CONVERT(VARCHAR(30),CONVERT(MONEY,@NON_SUFFICIENT_FUND_FEES),1)                            
  + ' NSF fee will be charged if your check is returned due to non-sufficient funds.'   
END                                          
                
IF  @LATE_FEES  IS NOT NULL                                          
BEGIN                                           
  SET @LF_MESSAGE = 'A $' +  CONVERT(VARCHAR(30),CONVERT(MONEY,@LATE_FEES),1)                                          
  + ' late fee has been added to this Cancellation.'   
END            
  
--Record set 1                                        
SELECT DISTINCT                                                                                
CCL.CUSTOMER_FIRST_NAME + ' ' + CASE WHEN ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'')='' THEN '' ELSE CCL.CUSTOMER_MIDDLE_NAME + ' ' END                                                        
+ ISNULL(CCL.CUSTOMER_LAST_NAME ,'') AS CUSTOMER_NAME,   
ISNULL(CCL.CUSTOMER_SUFFIX,'') AS CUSTOMER_SUFFIX,                                                                               
CCL.CUSTOMER_ADDRESS1, CCL.CUSTOMER_ADDRESS2,                                                                                   
CCL.CUSTOMER_CITY + ',' AS CUSTOMER_CITY, MCSL.STATE_CODE AS STATE_CODE, MCSL1.STATE_CODE AS POLICY_STATE_CODE, CCL.CUSTOMER_ZIP,                                                                                  
MAL.AGENCY_DISPLAY_NAME, MAL.AGENCY_CODE,                                                                                  
MAL.AGENCY_ADD1, MAL.AGENCY_ADD2, MAL.AGENCY_CITY,                                                                                  
MAL.AGENCY_ZIP, MAL.AGENCY_PHONE, 'Cancellation Notice' AS NOTICE_TYPE,                                                                                  
'' AS POLICY_NUMBER, PCPL.POLICY_NUMBER AS POL_NUMBER, PCPL.APP_EFFECTIVE_DATE AS APP_INCEPTION_DATE, PCPL.APP_EXPIRATION_DATE, MLM.LOB_CODE, MLM.LOB_DESC + '/' + PCPL.POLICY_NUMBER AS LOB_DESC,                                                         
PPP.EFFECTIVE_DATETIME AS PROCESS_DATE, CONVERT(VARCHAR(11),PPP.DUE_DATE,101) AS DUE_DATE, PPP.CANCELLATION_TYPE,   
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,@TOTAL_DUE),1) AS TOTAL_DUE,                                                    
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,@MINIMUM_DUE),1) AS MINIMUM_DUE,                              
'$' +  convert(varchar(30),convert(money,@AMOUNT_PAID),1) AS AMOUNT_PAID ,                              
'A $' + CONVERT(VARCHAR(30),CONVERT(MONEY,@LATE_FEES),1) AS LATE_FEE,                               
'A $' + convert(varchar(30),convert(money,ISNULL(@NON_SUFFICIENT_FUND_FEES,0) ),1) AS NSF_FEE,   
@NSF_MESSAGE AS NSF_MESSAGE ,  
@LF_MESSAGE AS  LF_MESSAGE  ,  
MLM.LOB_DESC --+ CASE WHEN MLV.LOOKUP_VALUE_DESC IS NULL THEN '' ELSE ' (' + MLV.LOOKUP_VALUE_DESC + ')' END  
AS POLICY_TYPE,                                                                    
@OCRA AS OCRA,                              
convert(varchar,PCPL.APP_EFFECTIVE_DATE,101) AS EFFECTIVE_DATE,  
@PLAN_DESCRIPTION AS BILL_PLAN  ,  
@SHOW_INSTALLMENTS AS SHOW_INS_SCHEDULE ,  
@ADD_INT_ID AS BILL_MORTAGAGEE  ,  
@LOB_ID AS LOB_ID,   
@TOTAL_PREMIUM_DUE AS TOTAL_PREMIUM_DUE  
FROM POL_CUSTOMER_POLICY_LIST  PCPL  with(nolock)      
INNER JOIN CLT_CUSTOMER_LIST CCL with(nolock)   
 ON CCL.CUSTOMER_ID=PCPL.CUSTOMER_ID                                                                                  
LEFT OUTER JOIN CLT_APPLICANT_LIST CAL with(nolock)   
 ON  CCL.CUSTOMER_ID=CAL.CUSTOMER_ID        
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL with(nolock)   
 ON CCL.CUSTOMER_STATE = MCSL.STATE_ID    
LEFT OUTER JOIN MNT_LOB_MASTER MLM with(nolock) ON                 
PCPL.POLICY_LOB = MLM.LOB_ID                                               
LEFT OUTER JOIN MNT_AGENCY_LIST MAL with(nolock) ON                                                                                  
PCPL.AGENCY_ID = MAL.AGENCY_ID    
  
LEFT OUTER JOIN  MNT_COUNTRY_STATE_LIST MCSL1  with(nolock) ON                                                                  
   CONVERT(VARCHAR(10),MCSL1.STATE_ID)= PCPL.STATE_ID      
                                                                        
LEFT OUTER JOIN POL_POLICY_PROCESS PPP (nolock)     ON                                                                    
PCPL.CUSTOMER_ID=PPP.CUSTOMER_ID AND PCPL.POLICY_ID=PPP.POLICY_ID AND PCPL.POLICY_VERSION_ID=PPP.NEW_POLICY_VERSION_ID AND ISNULL(PPP.REASON,0) <> 0                                                                    
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV  with(nolock) ON                                            
MLV.LOOKUP_UNIQUE_ID = PCPL.POLICY_TYPE                           
WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID AND PCPL.POLICY_ID=@POLICY_ID AND PCPL.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                     
AND PPP.PROCESS_STATUS = 'PENDING'                                      
ORDER BY PROCESS_DATE DESC                      
                                                   
--Added for the proc change 23-Nov-2007          
CREATE TABLE #ARREPORT                    
(                           
  [IDENT_COL] [int] NOT NULL ,                    
  [SOURCE_ROW_ID] [int],              
  [SOURCE]    Varchar(50),              
  [PRINTED_ON_NOTICE] Char(1),              
  [SOURCE_TRAN_DATE] DateTime,                                                                                  
  [SOURCE_EFF_DATE] DateTime,                                              
  [POSTING_DATE] DateTime,                                                     
  [DESCRIPTION] VarChar(100) null,    
  [VERSION_NO] Varchar(10) null,                                                                                    
  [TOTAL_AMOUNT] [decimal] (18,2)  ,                                             
  [TEMP_PREMIUM] [decimal](18,2) NULL ,                                                                                  
  [INSF_FEE] [decimal] (18,2) NULL,                                                                               
  [LATE_FEE] [decimal](18,2) NULL ,                                   
  [REINS_FEE] [decimal](18,2) NULL ,                     
  [NSF_FEE] [decimal](18,2) NULL,                    
  [ADJUSTED] [decimal](18,2),              
  [TYPE] VARCHAR(2),                    
  [NOTICE_URL] Varchar(400),      
 --Ravindra(12-08-2008) : For RTL View Integration  
 [RTL_BATCH_NUMBER] Varchar(8),   
 [RTL_GROUP_NUMBER] Varchar(8),   
  
  TOTAL_FEE  [decimal](18,2) NULL,             
  TOTAL_PREMIUM [decimal](18,2) NULL   ,  
  TOTAL_PREMIUM_DUE Decimal(18,2)              
)    
                                             
INSERT INTO #ARREPORT exec Proc_GetTransactionHistory @CUSTOMER_ID , @POLICY_ID , NULL                                                      
          
       
--Record Set 2: Transaction History  
SELECT  SOURCE , SOURCE_ROW_ID, PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                                      
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                               
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                                      
DESCRIPTION AS ACTIVITY_DESC,                                
'$' +  CASE TYPE WHEN 'T' THEN CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_PREMIUM_DUE),1)     
ELSE  
CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_AMOUNT),1)     
END AS AMOUNT,     TYPE  ,POSTING_DATE  , 0 AS DISPLAY_ORDER                  
FROM #ARREPORT (nolock)      
WHERE TYPE NOT IN('N','L','R','F')                                  
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                                      
  
UNION   
  
  
--Ravindra(06-20-2008): Fees transaction through JE not to be printed   
-- Fees reversal against unpaid fees not to be printed. Ref: iTrack 4359  
  
--SELECT  SOURCE , SOURCE_ROW_ID, PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                                      
--CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                               
--CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                                      
--DESCRIPTION AS ACTIVITY_DESC,                                
--'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_AMOUNT * -1 ),1)   AS AMOUNT,  --TYPE  ,POSTING_DATE                   
--FROM #ARREPORT  (nolock)                                                          
--WHERE TYPE IN('F')                                                      
--AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'      
  
SELECT  SOURCE , SOURCE_ROW_ID, PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                                      
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                               
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                                      
DESCRIPTION AS ACTIVITY_DESC,                                
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TOTAL_PREMIUM,0) * -1 ),1)   AS AMOUNT,  TYPE  ,POSTING_DATE , 0 AS DISPLAY_ORDER                   
FROM #ARREPORT  (nolock)                                                          
WHERE TYPE IN('F')                                                      
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'      
AND ISNULL(TOTAL_PREMIUM,0) <> 0   
  
UNION                                       
                                                      
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                                      
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,     
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,   
'Service Fees charged ' AS ACTIVITY_DESC,                                                                       
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,(INSF_FEE* -1)),1)   AS AMOUNT,                                                 
TYPE  ,POSTING_DATE , 1 AS DISPLAY_ORDER                             
FROM #ARREPORT (nolock)                                                          
WHERE INSF_FEE <> 0                                                       
AND  TYPE NOT IN('N','L','R','F')                                                      
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                                      
                                                      
UNION                                                      
                              
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                                   
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                                                      
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                                      
'NSF Fees charged ' as ACTIVITY_DESC,                                                                          
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,NSF_FEE * - 1 ),1)   AS AMOUNT,                                                       
TYPE  ,POSTING_DATE , 2 AS DISPLAY_ORDER                                                      
FROM #ARREPORT  (nolock)                                                    
WHERE NSF_FEE <> 0                                                       
AND  TYPE NOT IN('N','L','R','F')                              
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                             
        
UNION       
                                  
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                                      
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                                                      
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                                   
'Late Fees charged ' as ACTIVITY_DESC,                                                                          
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,LATE_FEE * - 1 ),1)   AS AMOUNT,                                                       
TYPE  ,POSTING_DATE   , 3 AS DISPLAY_ORDER                                                    
FROM #ARREPORT     (nolock)                                                       
WHERE LATE_FEE <> 0                                                       
AND  TYPE NOT IN('N','L','R','F')             
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                                      
                   
UNION                                                       
                                                    
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                 
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                                                      
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                                      
'Reinstallment Fees charged ' as ACTIVITY_DESC,                                                 
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,REINS_FEE * - 1 ),1)   AS AMOUNT,                                                       
TYPE  ,POSTING_DATE   , 4 AS DISPLAY_ORDER                                                    
FROM #ARREPORT   (nolock)         
WHERE REINS_FEE <> 0                                                       
AND  TYPE NOT IN('N','L','R','F')                                                     
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                                      
ORDER BY SOURCE_TRAN_DATE , DISPLAY_ORDER                                  
      
  
 ----Ravindra (03-07-2008)                             
-- Mark these records as printed on Notice so that they does not get repeated on next notice.                            
UPDATE ACT_PREMIUM_PROCESS_DETAILS SET PRINTED_ON_NOTICE  = 'Y'            
WHERE PROCESS_ID IN (  SELECT SOURCE_ROW_ID FROM #ARREPORT WHERE SOURCE = 'PPD' )                            
  
UPDATE ACT_CUSTOMER_OPEN_ITEMS  SET PRINTED_ON_NOTICE  = 'Y'                            
WHERE IDEN_ROW_ID IN (  SELECT SOURCE_ROW_ID FROM #ARREPORT WHERE SOURCE = 'COI' )                            
  
UPDATE ACT_CUSTOMER_BALANCE_INFORMATION SET PRINTED_ON_NOTICE  = 'Y'                            
WHERE ROW_ID IN (  SELECT SOURCE_ROW_ID FROM #ARREPORT WHERE SOURCE = 'BAL' )   
  
UPDATE ACT_RECONCILIATION_GROUPS SET PRINTED_ON_NOTICE  = 'Y'                            
WHERE GROUP_ID IN (  SELECT SOURCE_ROW_ID FROM #ARREPORT WHERE SOURCE = 'REC' )   
  
  
DROP TABLE #ARREPORT     
  
CREATE TABLE #ARREPORT_INS                
(       
  [IDEN_ID] Int IDENTITY(1,1) not null,                  
  [AMOUNT] Decimal(18,2) ,                
  [DUE_DATE] Datetime,          
  [INS_NO]    Int ,   
  [CURRENT_TERM] Int  
)         
  
INSERT INTO #ARREPORT_INS                                     
SELECT   
 SUM( ISNULL(OI.TOTAL_DUE,0)-ISNULL(OI.TOTAL_PAID,0)),  
 CONVERT(VARCHAR,OI.DUE_DATE,101),  
 B.INSTALLMENT_NO    , B.CURRENT_TERM                                    
FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)                                          
INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS B                                           
WITH(NOLOCK) ON OI.INSTALLMENT_ROW_ID  = B.ROW_ID                                          
INNER JOIN POL_CUSTOMER_POLICY_LIST PCL   WITH(NOLOCK)   
 ON  OI.CUSTOMER_ID = PCL.CUSTOMER_ID          
 AND OI.POLICY_ID = PCL.POLICY_ID        
 AND OI.POLICY_VERSION_ID = PCL.POLICY_VERSION_ID            
WHERE  PCL.CUSTOMER_ID = @CUSTOMER_ID                                           
AND PCL.POLICY_ID = @POLICY_ID     
--Ravindra(02-18-2008) : Need to pick unpaid installment from prior term also                                        
--AND PCL.CURRENT_TERM = @CURRENT_TERM               
AND (    
  SELECT SUM(ISNULL(COI.TOTAL_DUE,0))     
  FROM ACT_CUSTOMER_OPEN_ITEMS COI WITH(NOLOCK)     
  WHERE COI.INSTALLMENT_ROW_ID IN    
  (     
   SELECT  INSDI.ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS INSDI   WITH(NOLOCK)   
   WHERE INSDI.INSTALLMENT_NO  = B.INSTALLMENT_NO    
  )    
 )                   
 -     
 (    
  SELECT SUM(ISNULL(COI.TOTAL_PAID,0))     
  FROM ACT_CUSTOMER_OPEN_ITEMS COI WITH(NOLOCK)     
  WHERE COI.INSTALLMENT_ROW_ID IN    
  (     
   SELECT  INSDI.ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS INSDI   WITH(NOLOCK)   
   WHERE INSDI.INSTALLMENT_NO  = B.INSTALLMENT_NO    
  )    
 )    <> 0                
AND OI.ITEM_TRAN_CODE_TYPE <> 'FEES'    
AND OI.ITEM_TRAN_CODE  <>'INSF'   
GROUP BY B.INSTALLMENT_NO, OI.DUE_DATE  , B.CURRENT_TERM    
HAVING  SUM((ISNULL(OI.TOTAL_DUE,0))-(ISNULL(OI.TOTAL_PAID,0))) <> 0                                                
ORDER BY  B.CURRENT_TERM  , B.INSTALLMENT_NO   
  
-- FETCH FEE ONLY  
CREATE TABLE #ARREPORT_INS_FEE                    
(    
  [IDEN_ID] int IDENTITY(1,1) not null,                         
  [AMOUNT]  Decimal(18,2),  
  [DUE_DATE] datetime,              
  [INS_NO]   int,          
)  
INSERT INTO #ARREPORT_INS_FEE                                                     
SELECT   
SUM( ISNULL(OI.TOTAL_DUE,0)-ISNULL(OI.TOTAL_PAID,0)) ,                   
CONVERT(VARCHAR,OI.DUE_DATE,101) --AS DUE_DATE   
,B.INSTALLMENT_NO --AS INS_NO               
FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)                                 
INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS B WITH(NOLOCK)                               
 ON OI.INSTALLMENT_ROW_ID  = B.ROW_ID                        
INNER JOIN POL_CUSTOMER_POLICY_LIST PCL (nolock)                                          
 ON  OI.CUSTOMER_ID = PCL.CUSTOMER_ID                                                      
 AND OI.POLICY_ID = PCL.POLICY_ID                                                       
 AND OI.POLICY_VERSION_ID = PCL.POLICY_VERSION_ID                                                      
WHERE  PCL.CUSTOMER_ID = @CUSTOMER_ID                   
AND PCL.POLICY_ID = @POLICY_ID    
AND PCL.CURRENT_TERM = @CURRENT_TERM    
AND (    
  SELECT SUM(ISNULL(COI.TOTAL_DUE,0))     
  FROM ACT_CUSTOMER_OPEN_ITEMS COI WITH(NOLOCK)     
  WHERE COI.INSTALLMENT_ROW_ID IN    
  (     
   SELECT  INSDI.ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS INSDI   WITH(NOLOCK)   
   WHERE INSDI.INSTALLMENT_NO  = B.INSTALLMENT_NO    
  )    
 )                   
 -     
 (    
  SELECT SUM(ISNULL(COI.TOTAL_PAID,0))     
  FROM ACT_CUSTOMER_OPEN_ITEMS COI WITH(NOLOCK)     
  WHERE COI.INSTALLMENT_ROW_ID IN    
  (     
   SELECT  INSDI.ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS INSDI   WITH(NOLOCK)   
   WHERE INSDI.INSTALLMENT_NO  = B.INSTALLMENT_NO    
  )    
 ) <> 0  
AND OI.ITEM_TRAN_CODE_TYPE = 'FEES'    
AND OI.ITEM_TRAN_CODE = 'INSF'   
GROUP BY B.INSTALLMENT_NO, OI.DUE_DATE  , ISNULL(B.CURRENT_TERM,50)  
HAVING  SUM((ISNULL(OI.TOTAL_DUE,0))-(ISNULL(OI.TOTAL_PAID,0))) <> 0             
ORDER BY B.INSTALLMENT_NO  
  
  
-- UPDATE INSTALLMENT AMOUNT WITH FEE AMOUNT  
  
UPDATE #ARREPORT_INS  
SET #ARREPORT_INS.AMOUNT = #ARREPORT_INS.AMOUNT + #ARREPORT_INS_FEE.AMOUNT  
FROM #ARREPORT_INS INNER JOIN  
#ARREPORT_INS_FEE  
ON #ARREPORT_INS.INS_NO = #ARREPORT_INS_FEE.INS_NO  
WHERE #ARREPORT_INS.DUE_DATE > @DUE_DATE  
AND #ARREPORT_INS.DUE_DATE = #ARREPORT_INS_FEE.DUE_DATE  
   
-- total instalment fees due upto due date  
Declare @TOTAL_INSTALMENT_FEE_DUE DECIMAL(18,2)  
  
SELECT @TOTAL_INSTALMENT_FEE_DUE =   
 SUM((ISNULL(OI.TOTAL_DUE,0))-(ISNULL(OI.TOTAL_PAID,0)))  
 FROM ACT_CUSTOMER_OPEN_ITEMS OI with (nolock)                       
  WHERE  CAST(CONVERT(VARCHAR,OI.DUE_DATE,101) AS Datetime) <= @DUE_DATE                          
    AND OI.CUSTOMER_ID = @CUSTOMER_ID                          
    AND OI.POLICY_ID  = @POLICY_ID                          
    AND OI.ITEM_TRAN_CODE_TYPE = 'FEES'    
    AND OI.ITEM_TRAN_CODE = 'INSF'  
  
DECLARE @SUM_AMOUNT NVARCHAR(100)  
UPDATE #ARREPORT_INS  
  
SET DUE_DATE = @DUE_DATE WHERE  
CAST(CONVERT(VARCHAR,DUE_DATE, 101) AS DATETIME) <=    
CAST(CONVERT(VARCHAR,@DUE_DATE, 101) AS DATETIME)   
  
--Ravindra(02-18-2008): Update Due Dates of items included in first installment  
  
UPDATE ACT_CUSTOMER_OPEN_ITEMS SET DUE_DATE = @DUE_DATE   
WHERE CUSTOMER_ID = @CUSTOMER_ID   
AND POLICY_ID  = @POLICY_ID   
AND INSTALLMENT_ROW_ID   
IN (  
 SELECT INSD.ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS INSD  
 INNER JOIN #ARREPORT_INS INS_SCH  
 ON INSD.INSTALLMENT_NO  = INS_SCH.INS_NO  
 AND INSD.CURRENT_TERM = INS_SCH.CURRENT_TERM   
 WHERE INSD.CUSTOMER_ID  = @CUSTOMER_ID   
 AND   INSD.POLICY_ID = @POLICY_ID   
 AND CAST(CONVERT(VARCHAR,INS_SCH.DUE_DATE, 101) AS DATETIME) <=    
 CAST(CONVERT(VARCHAR,@DUE_DATE, 101) AS DATETIME)   
 )  
  
-- If fullpay update due dates   
IF(@SHOW_INSTALLMENTS = 'N' )   
BEGIN   
 UPDATE ACT_CUSTOMER_OPEN_ITEMS SET DUE_DATE = @DUE_DATE   
 WHERE CUSTOMER_ID = @CUSTOMER_ID   
 AND POLICY_ID  = @POLICY_ID   
 AND CAST(CONVERT(VARCHAR,DUE_DATE, 101) AS DATETIME) <=    
  CAST(CONVERT(VARCHAR,@DUE_DATE, 101) AS DATETIME)   
 AND UPDATED_FROM = 'P'  
END  
  
  
-- If jounral entry have been made against customer in transaction code as DB Premium  
-- that amount will be added with the first installment of billing plan  
/* Store data from jounral entry into temp table*/  
CREATE TABLE #ARREPORT_JUNRAL                    
(     
 [IDEN_COL] int IDENTITY(1,1) not null,  
 [AMOUNT_J] decimal(18,2),  
 [CREATED_DATETIME] datetime              
 --[TRAN_CODE]   nvarchar(100),          
  
)  
  
-- Fetch DB type JEs  
INSERT INTO #ARREPORT_JUNRAL                                
SELECT ISNULL(TOTAL_DUE ,0) - ISNULL(TOTAL_PAID, 0) , DUE_DATE  
FROM ACT_CUSTOMER_OPEN_ITEMS  WITH(NOLOCK)  
WHERE CUSTOMER_ID =@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  
AND UPDATED_FROM =  'J'  
AND ITEM_TRAN_CODE_TYPE  = 'JE'  
  
-- Fetch Fee Reversal records  
INSERT INTO #ARREPORT_JUNRAL                                
SELECT ISNULL(TOTAL_DUE ,0) - ISNULL(TOTAL_PAID, 0) , DUE_DATE   
FROM ACT_CUSTOMER_OPEN_ITEMS  WITH(NOLOCK)  
WHERE CUSTOMER_ID =@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  
AND UPDATED_FROM ='F'  
  
--Ravindra(11-25-2008): If chack issued and voided after cancellation for cancellation credit  
-- and reinstate no lapse is done there will be negative balance on cancellation item. Reduce this from   
-- JE item of Check Reversal done at reinstatement   
INSERT INTO #ARREPORT_JUNRAL                                                     
SELECT ISNULL(TOTAL_DUE ,0) - ISNULL(TOTAL_PAID, 0)  , DUE_DATE  
FROM ACT_CUSTOMER_OPEN_ITEMS  WITH(NOLOCK)  
WHERE CUSTOMER_ID =@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  
AND UPDATED_FROM =  'P'  
AND ITEM_TRAN_CODE = 'CANCP'  
  
  
-- Run While loop for updation of amount  
declare @AMOUNT decimal(18,2)  
declare @TRAN_DATE DATETIME,  
 @IDENT_COL Int  
  
SET @IDENT_COL = 1      
WHILE (1= 1)   
BEGIN   
 IF NOT EXISTS (SELECT IDEN_COL FROM #ARREPORT_JUNRAL  WITH(NOLOCK) WHERE IDEN_COL = @IDENT_COL )   
 BEGIN   
  BREAK  
 END  
 SELECT  @AMOUNT = AMOUNT_J ,  
  @TRAN_DATE  = CREATED_DATETIME  
 FROM #ARREPORT_JUNRAL  WITH(NOLOCK)  
 WHERE IDEN_COL = @IDENT_COL   
  
 UPDATE #ARREPORT_INS  
  SET #ARREPORT_INS.AMOUNT = #ARREPORT_INS.AMOUNT + @AMOUNT  
 WHERE CAST(CONVERT(VARCHAR,#ARREPORT_INS.DUE_DATE,101) AS DATETIME)  
   > =  CAST(CONVERT(VARCHAR, @TRAN_DATE ,101) AS DATETIME)  
 AND IDEN_ID = (SELECT TOP 1 TMP_IN.IDEN_ID FROM #ARREPORT_INS TMP_IN  WITH(NOLOCK)   
   WHERE  CAST(CONVERT(VARCHAR, TMP_IN.DUE_DATE,101) AS DATETIME)  
   > =  CAST(CONVERT(VARCHAR, @TRAN_DATE ,101) AS DATETIME)  
   ORDER BY TMP_IN.DUE_DATE , TMP_IN.IDEN_ID )   
  
  
 SET @IDENT_COL = @IDENT_COL + 1  
     
END      
  
-- As disscussed with Rajan if  due dates are minimum due date then installment amount should be added and   
--there will be one entry for that on cancelation notice.  
SELECT   
@SUM_AMOUNT = ((CONVERT(NVARCHAR(100),SUM(CONVERT(MONEY,AMOUNT,1))) +  @TOTAL_FEE)) + @FIRST_INS_FEE  
FROM #ARREPORT_INS  WITH(NOLOCK) WHERE DUE_DATE = @DUE_DATE  
  
  
  
UPDATE #ARREPORT_INS   
SET AMOUNT = @SUM_AMOUNT,  
INS_NO = 1  
WHERE CAST(CONVERT(VARCHAR,DUE_DATE,101) AS DATETIME) = CAST(CONVERT(VARCHAR,@DUE_DATE,101) AS DATETIME)  
  
--Ravindra(08-19-2008) : iTrack 4653 : Add negative installment to next due installment  
SET @IDENT_COL = 1      
WHILE (1= 1)   
BEGIN   
 IF NOT EXISTS (SELECT IDEN_ID FROM #ARREPORT_INS  WHERE IDEN_ID = @IDENT_COL )   
 BEGIN   
  BREAK  
 END  
 SELECT  @AMOUNT = AMOUNT FROM #ARREPORT_INS  WHERE IDEN_ID = @IDENT_COL   
  
 IF (@AMOUNT < 0 )   
 BEGIN   
  IF EXISTS (SELECT IDEN_ID FROM #ARREPORT_INS  WHERE IDEN_ID = @IDENT_COL + 1 )   
  BEGIN   
   UPDATE #ARREPORT_INS  SET AMOUNT = AMOUNT + @AMOUNT WHERE IDEN_ID = @IDENT_COL + 1   
   UPDATE #ARREPORT_INS  SET AMOUNT = 0 WHERE IDEN_ID = @IDENT_COL    
  END  
 END   
 SET @IDENT_COL = @IDENT_COL + 1  
     
END      
  
DELETE  FROM #ARREPORT_INS WHERE AMOUNT = 0   
  
  
  
--Record Set 3   
SELECT                
'$ ' + convert(varchar(30), convert(money,AMOUNT),1) AS AMOUNT,  
CONVERT(VARCHAR, DUE_DATE,101) AS DUE_DATE,  
INS_NO AS INS_NO ,   
CAST( DUE_DATE AS DATETIME) AS DUE_DATE_ACTUAL   
FROM #ARREPORT_INS  
WITH(NOLOCK) GROUP BY #ARREPORT_INS.DUE_DATE ,#ARREPORT_INS.INS_NO,#ARREPORT_INS.AMOUNT  
ORDER BY DUE_DATE_ACTUAL    
--INS_NO   
ASC   
  
  
DROP TABLE #ARREPORT_JUNRAL   
DROP TABLE #ARREPORT_INS                                    
  
  
DECLARE @INSMESS VARCHAR(500)                                                  
                    
IF ( ISNULL(@INSTALLMENT_FEES,0) <> 0   AND @IS_HOME_EMP <> 1 )   
 SET @INSMESS = 'Each Installment includes a $' + CONVERT(VARCHAR, ISNULL(@INSTALLMENT_FEES,0)) + ' Service Fee. '                     
ELSE                
 SET @INSMESS = ''                                               
  
--Record Set 4  
   
                                   
SELECT CASE WHEN @CANCELLATION_OPTION = @Equity THEN   
'According to our records, the scheduled payment on your policy is past due. However, payments received to date will provide coverage through '   
--+CONVERT(VARCHAR(11),ISNULL(@CANCELLATION_DATE,''),101)   
+ CONVERT(VARCHAR(11),DateAdd(day, -1, ISNULL(@CANCELLATION_DATE,'')),101)   
+ '. If payment of $' + CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1) + ' is not received by '   
+ CONVERT(VARCHAR(11),ISNULL(@DUE_DATE,''),101) +                                         
', your policy is cancelled for non-payment of premium effective 12:01 A.M. Standard Time on '   
--+ CONVERT(VARCHAR(11),DateAdd(day, 1, ISNULL(@CANCELLATION_DATE,'')),101)   
+ CONVERT(VARCHAR(11),@CANCELLATION_DATE,101)   
+ '. Please disregard this notice if payment has been made. '                                                   
          
WHEN @CANCELLATION_OPTION = @ProRata THEN   
'According to our records, the scheduled payment on your policy is past due. If payment of $'   
+ CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1) + ' is not received by ' + CONVERT(VARCHAR(11),ISNULL(@DUE_DATE,'') ,101)   
+ ', your policy is cancelled for non-payment of premium effective 12:01 A.M. Standard Time on '   
+ CONVERT(VARCHAR(11),ISNULL(@CANCELLATION_DATE,''),101) +        
'. Please disregard this notice if payment has been made. '    
 WHEN @CANCELLATION_OPTION = 11995 THEN  'According to our records, the scheduled payment on your policy is past due. If payment of $'  + CONVERT(VARCHAR(30),CONVERT(MONEY,(ISNULL(@MINIMUM_DUE,0))),1)  + ' is not received by ' +      
CONVERT(VARCHAR(11),  
ISNULL(@DUE_DATE,''), 101)  
+ ', your policy is cancelled for non-payment of premium effective 12:01 A.M. Standard Time on '   
+ CONVERT(VARCHAR(11),ISNULL(@CANCELLATION_DATE,''),101) +                                         
'. Please disregard this notice if payment has been made. '                       
ELSE ''                                                  
END AS MESSAGE   ,  @INSMESS  AS INS_MESSAGE                                           
                                                  
SELECT 'WARNING: According to Michigan law, you must not operate or permit the operation of any motor vehicle to which this notice applies, or operate any other vehicle, unless the vehicle is insured as required by the law.' AS MESSAGE_MI              
  
  
-- Recordset 5 Mortagagee Details  
--Ravindra(03-10-2008) fetch mortgagee information  
IF( @ADD_INT_ID <> 0 AND (@LOB_ID  = 1 OR @LOB_ID = 6) )  
BEGIN   
 SELECT   
 ADD_IN.HOLDER_NAME AS HOLDER_NAME,                                         
 RTRIM(ADD_IN.HOLDER_ADD1) HOLDER_ADDRESS1,   
 RTRIM(ADD_IN.HOLDER_ADD2) HOLDER_ADDRESS2,                                                 
 ADD_IN.HOLDER_CITY, MCSL.STATE_CODE,   
 ADD_IN.HOLDER_ZIP  
 FROM POL_HOME_OWNER_ADD_INT POL_ADD  
 INNER JOIN MNT_HOLDER_INTEREST_LIST ADD_IN  
 ON POL_ADD.HOLDER_ID = ADD_IN.HOLDER_ID  
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL                                     
 ON ADD_IN.HOLDER_STATE = MCSL.STATE_ID         
 WHERE POL_ADD.CUSTOMER_ID = @CUSTOMER_ID   
 AND POL_ADD.POLICY_ID   = @POLICY_ID   
 --Ravindra(10-14-2008): Pick mortagee from latest version                                                     
 AND POL_ADD.POLICY_VERSION_ID = @MAX_VERSION_ID --@POLICY_VERSION_ID   
 AND POL_ADD.ADD_INT_ID   = @ADD_INT_ID   
 AND POL_ADD.DWELLING_ID  = @DWELLING_ID   
END  
        
                          
END                                               
  
  
  
  
  
  
GO

