IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPremiumNoticeData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPremiumNoticeData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop PROC Dbo.Proc_GetPremiumNoticeData  
--go 

/*----------------------------------------------------------                                                        
Proc Name       : dbo.Proc_GetPremiumNoticeData                                                        
Created by      :  Mohit Agarwal                                                        
Date            :  04-Jan-2007                                                        
Purpose         :  To get premium notice data                                                        
Revison History :                                                        
Used In         :    Wolverine                                                        
                                                        
Modified By : Ravindra                                                     
Modified Date  : 03-08-2007                                        
Purpose  : Change Hardcoded Version to Actual Implementation                                                      
                                        
exec Proc_GetPremiumNoticeData  1360,55,3                                                         
exec Proc_GetTransactionHistory                           
Reviewed By : Anurag Verma                            
Reviewed On : 22-06-2007                            
                            
------------------------------------------------------------                                                        
Date     Review By          Comments                                                        
------   ------------       -------------------------*/                                                        
-- drop PROC Dbo.Proc_GetPremiumNoticeData                                                          
create PROC [dbo].[Proc_GetPremiumNoticeData]                                                      
(                                                              
		@CUSTOMER_ID int,                                              
		@POLICY_ID int,                                              
		@POLICY_VERSION_ID int,           
		@NOTICE_DUE_DATE Datetime = null,                                    
		@IS_EOD		 SmallInt = null,                                   
		@POLICY_NUMBER nvarchar(75) = null         
)                                                              
AS                                                              
BEGIN                                           
                                        
DECLARE @MINIMUM_DUE Decimal(18,2),   
		@TOTAL_PREMIUM_DUE	Decimal(25,2) ,                                      
		@TOTAL_DUE Decimal(18,2),                                        
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
		@CURRENT_TERM SmallInt ,                           
		@BILL_DATE DateTime,                        
		@OCRA varchar(25),             
        @DUE_DATE DateTime  ,
		@PLAN_DESCRIPTION NVARCHAR(35),
		@SHOW_INSTALLMENTS	Char(1) ,
		@ADD_INT_ID		Char(2)  ,
		@LOB_ID				Int  ,
		@DWELLING_ID		Int , 
		@PLAN_PAYMENT_MODE	Int, 
		@EFT				Int, 
		@CREDITCARD			Int,
		@IS_HOME_EMP		Bit, 
		@MAX_VERSION_ID		INt , 
		@CURRENT_STATUS		Varchar(20) 
		

SET		@EFT	     = 11973
SET		@CREDITCARD	 = 11974
SET		@ADD_INT_ID  = 0 
                                        
IF(ISNULL(@CUSTOMER_ID,0) = 0 AND @POLICY_NUMBER IS NOT NULL )
BEGIN                                
	SELECT @CUSTOMER_ID=CUSTOMER_ID, @POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID     
	FROM POL_CUSTOMER_POLICY_LIST                           
	WHERE POLICY_NUMBER=@POLICY_NUMBER    
END

SELECT @MAX_VERSION_ID = MAX(POLICY_VERSION_ID ) FROM POL_CUSTOMER_POLICY_LIST 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                     

SELECT @CURRENT_STATUS = POLICY_STATUS,
		--Ravindra(10-14-2008): Pick mortagee from latest version 
		@ADD_INT_ID				=  ISNULL(ADD_INT_ID,0),
		@DWELLING_ID			=  ISNULL(DWELLING_ID , 0) 
FROM POL_CUSTOMER_POLICY_LIST 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND POLICY_VERSION_ID = @MAX_VERSION_ID                    

--Get Current Policy Version       
SELECT @CURRENT_VERSION_ID =ISNULL(MAX(NEW_POLICY_VERSION_ID),0) FROM POL_POLICY_PROCESS                                               
WHERE CUSTOMER_ID=@CUSTOMER_ID                                         
	 AND POLICY_ID=@POLICY_ID                     
	 AND PROCESS_ID IN(18,25,32)                                              
	 AND ISNULL(REVERT_BACK,'N')  <> 'Y'  --Ravindra(06-24-2008)
	                                              
IF(@CURRENT_VERSION_ID=0)         
		SET @CURRENT_VERSION_ID=1                                              
                                        
SELECT  @INSTALL_PLAN_ID = CPL.INSTALL_PLAN_ID ,                                        
		@MINDAYS_PREMIUM = INS_MASTER.MINDAYS_PREMIUM,                                        
		@DAYS_DUE_PREM_NOTICE_PRINTD = INS_MASTER.DAYS_DUE_PREM_NOTICE_PRINTD,                                        
		@NON_SUFFICIENT_FUND_FEES = INS_MASTER.NON_SUFFICIENT_FUND_FEES,                                         
		@REINSTATEMENT_FEES  = INS_MASTER.REINSTATEMENT_FEES ,                                        
		@LATE_FEES   = INS_MASTER.LATE_FEES,                                        
		@INSTALLMENT_FEES       = INSTALLMENT_FEES,                  
		@CURRENT_TERM           = CPL.CURRENT_TERM  ,                          
		@BILL_DATE   =    DATEADD(DD,INS_MASTER.DAYS_DUE_PREM_NOTICE_PRINTD, GETDATE() ),                        
		@OCRA = CPL.POLICY_NUMBER         ,
		@PLAN_DESCRIPTION       = INS_MASTER.PLAN_DESCRIPTION , 
		@LOB_ID					= CPL.POLICY_LOB , 
----Ravindra(10-14-2008): Pick mortagee from latest version 
--		@ADD_INT_ID				=  ISNULL(ADD_INT_ID,0),
--		@DWELLING_ID			=  ISNULL(DWELLING_ID , 0) , 
		@IS_HOME_EMP			=  ISNULL(CPL.IS_HOME_EMP, 0) , 
		@SHOW_INSTALLMENTS	  = CASE ISNULL(INS_MASTER.SYSTEM_GENERATED_FULL_PAY,0) WHEN 1 THEN 'N' ELSE 'Y' END              
FROM POL_CUSTOMER_POLICY_LIST CPL                                        
INNER JOIN ACT_INSTALL_PLAN_DETAIL INS_MASTER                                        
ON INS_MASTER.IDEN_PLAN_ID = CPL.INSTALL_PLAN_ID                                        
WHERE CPL.CUSTOMER_ID =@CUSTOMER_ID                                  
AND   CPL.POLICY_ID = @POLICY_ID                                         
AND   CPL.POLICY_VERSION_ID = @CURRENT_VERSION_ID                                         
        

SELECT  @PLAN_PAYMENT_MODE = MODE_OF_PAYMENT
FROM ACT_POLICY_INSTALL_PLAN_DATA 
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM 
AND ISNULL(IS_ACTIVE_PLAN ,'Y') = 'Y'

--Calculate Due Date 6-Nov-2007        
      
IF(@NOTICE_DUE_DATE IS NULL)      
BEGIN        
		SET  @NOTICE_DUE_DATE = GETDATE()      
		SELECT  @DUE_DATE = GETDATE()        
		IF( CAST(CONVERT(VARCHAR,DATEADD(DD,@MINDAYS_PREMIUM, GetDate() ),101) AS DATETIME)          
		> CAST(CONVERT(VARCHAR,@DUE_DATE,101) AS VARCHAR))          
		BEGIN           
				SET @DUE_DATE = DATEADD(DD,@MINDAYS_PREMIUM, GetDate() )          
		END          
END      
ELSE      
BEGIN            
		SET @DUE_DATE  = @NOTICE_DUE_DATE      
END      
      
IF(DATENAME(DW,@DUE_DATE) = 'Sunday')          
BEGIN           
		SET @DUE_DATE = DATEADD(DD,1, @DUE_DATE )          
END          
IF(DATENAME(DW,@DUE_DATE) = 'Saturday')          
BEGIN           
		SET @DUE_DATE = DATEADD(DD,2, @DUE_DATE )          
END          
      
SET @AMOUNT_PAID = 0  
                 
--Use Accounting SP for Total and Minimum Due 6-Nov-2007        
DECLARE @AMOUNT_DUE TABLE 
(
		MINIMUM_DUE DECIMAL(18,2),                                        
		TOTAL_DUE DECIMAL(18,2),                                        
		AGENCY_ID INT,        
		AGENCYCODE VARCHAR(20),
		PREM Decimal(25,2) , 
		FEE Decimal(18,2),            
		FIRST_INS_FEE Decimal(18,2) ,
		TOTAL_PREMIUM_DUE Decimal(25,2) 
)        
        
INSERT INTO @AMOUNT_DUE exec Proc_GetTotalAndMinimumDue @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID , @DUE_DATE      
        
SELECT @TOTAL_DUE=TOTAL_DUE, @MINIMUM_DUE=MINIMUM_DUE,  @TOTAL_FEE=FEE, @FIRST_INS_FEE=FIRST_INS_FEE  ,
		@TOTAL_PREMIUM_DUE  = TOTAL_PREMIUM_DUE
FROM @AMOUNT_DUE            
        
IF (@TOTAL_DUE < 9999999 OR @TOTAL_DUE > -9999999) AND (@MINIMUM_DUE > -9999999 OR @MINIMUM_DUE < 9999999)    
		SET @OCRA = @OCRA + ' X ' + RIGHT('000000' + CONVERT(VARCHAR,CONVERT(INT,@TOTAL_DUE * 100)),6) + 
		RIGHT('000000' + CONVERT(VARCHAR,CONVERT(INT,@MINIMUM_DUE * 100)),6)                        
                                             
-- Record Set 1     
SELECT                                               
RTRIM(ISNULL(CCL.CUSTOMER_FIRST_NAME,'')) + ' ' +                                         
CASE WHEN CCL.CUSTOMER_MIDDLE_NAME IS NULL THEN '' ELSE RTRIM(CCL.CUSTOMER_MIDDLE_NAME) + ' ' END +                       
ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,
ISNULL(CCL.CUSTOMER_SUFFIX,'') AS CUSTOMER_SUFFIX,                                       
RTRIM(CCL.CUSTOMER_ADDRESS1) CUSTOMER_ADDRESS1, RTRIM(CCL.CUSTOMER_ADDRESS2) CUSTOMER_ADDRESS2,                                               
CCL.CUSTOMER_CITY, MCSL.STATE_CODE, CCL.CUSTOMER_ZIP,                                              
MAL.AGENCY_ID,
MAL.AGENCY_DISPLAY_NAME, MAL.AGENCY_CODE,                                              
MAL.AGENCY_ADD1, MAL.AGENCY_ADD2, MAL.AGENCY_CITY,                                  

PCPL.APP_EFFECTIVE_DATE AS APP_INCEPTION_DATE, 
PCPL.APP_EXPIRATION_DATE,                                              

MAL.AGENCY_ZIP, MAL.AGENCY_PHONE, 'Premium Notice' AS NOTICE_TYPE,                                  
MLM.LOB_DESC AS POLICY_TYPE,                          
PCPL.POLICY_NUMBER, MLM.LOB_CODE,                                         
'$' + convert(varchar(30),convert(money,@TOTAL_DUE),1) AS TOTAL_DUE,                                         
'$' + convert(varchar(30),convert(money,@MINIMUM_DUE),1)   AS MIN_DUE ,               
'$' + convert(varchar(30),convert(money,@AMOUNT_PAID),1)   AS AMOUNT_PAID ,                        
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(@NON_SUFFICIENT_FUND_FEES,0)),1) AS NSF_FEE,                       
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(@LATE_FEES,0)),1) AS LATE_FEE,                         
PCPL.BILL_TYPE_ID   AS BILL_PLAN,  
convert(varchar,PCPL.APP_EFFECTIVE_DATE,101) AS EFFECTIVE_DATE, CONVERT(VARCHAR,@DUE_DATE,101) AS DUE_DATE,                  
@OCRA AS OCRA, @PLAN_DESCRIPTION  AS PLAN_DESCRIPTION,                       
@SHOW_INSTALLMENTS AS SHOW_INS_SCHEDULE  ,
@ADD_INT_ID AS BILL_MORTAGAGEE  ,
@TOTAL_PREMIUM_DUE AS TOTAL_PREMIUM_DUE
FROM POL_CUSTOMER_POLICY_LIST PCPL                                         
INNER JOIN CLT_CUSTOMER_LIST CCL                                         
ON CCL.CUSTOMER_ID=PCPL.CUSTOMER_ID                                              
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL                                   
ON CCL.CUSTOMER_STATE = MCSL.STATE_ID 
LEFT OUTER JOIN MNT_LOB_MASTER MLM                                         
ON PCPL.POLICY_LOB = MLM.LOB_ID                                              
LEFT OUTER JOIN MNT_AGENCY_LIST MAL                                         
ON PCPL.AGENCY_ID = MAL.AGENCY_ID                                   
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON                                          
MLV.LOOKUP_UNIQUE_ID = PCPL.POLICY_TYPE                                          
                                             
WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID                                         
AND PCPL.POLICY_ID=@POLICY_ID   
AND PCPL.POLICY_VERSION_ID=@CURRENT_VERSION_ID                                           
                          
--Added for the proc change 23-Nov-2007    
DECLARE @ARREPORT TABLE 
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
		[TEMP_PREMIUM] [decimal](25,2) NULL ,                                                                            
		[INSF_FEE] [decimal] (18,2) NULL,                                                                         
		[LATE_FEE] [decimal](18,2) NULL ,                             
		[REINS_FEE] [decimal](18,2) NULL ,               
		[NSF_FEE] [decimal](18,2) NULL,              
		[ADJUSTED] [decimal](18,2),        
		[TYPE] VARCHAR(2),              
		[NOTICE_URL] Varchar(400), 
		--Ravindra(12-08-2008) : For RTL View Integration
		[RTL_BATCH_NUMBER]	Varchar(8), 
		[RTL_GROUP_NUMBER]	Varchar(8), 
       
		TOTAL_FEE  [decimal](18,2) NULL,       
		TOTAL_PREMIUM [decimal](25,2) NULL ,
	    TOTAL_PREMIUM_DUE	Decimal(25,2) 	     
)                                         

INSERT INTO @ARREPORT exec Proc_GetTransactionHistory @CUSTOMER_ID , @POLICY_ID , NULL                                        
                                        

-- Recordset 2 -- Transaction History of insured

SELECT  SOURCE , SOURCE_ROW_ID, PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                        
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                               
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                        
DESCRIPTION AS ACTIVITY_DESC,                                                            
'$' + 
CASE TYPE WHEN 'T' THEN CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_PREMIUM_DUE),1) 
WHEN 'A' THEN CONVERT(VARCHAR(30),CONVERT(MONEY,TEMP_PREMIUM),1) 
ELSE
CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_AMOUNT),1)   
END AS AMOUNT,                                         
TYPE  ,POSTING_DATE , 0 AS DISPLAY_ORDER                                       
FROM @ARREPORT                               
WHERE TYPE NOT IN('N','L','R','F')                                        
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
          
UNION

--Ravindra(06-20-2008): Fees transaction through JE not to be printed 
-- Fees reversal against unpaid fees not to be printed. Ref: iTrack 4359
--SELECT  SOURCE , SOURCE_ROW_ID, PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                        
--CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                               
--CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                        
--DESCRIPTION AS ACTIVITY_DESC,                                                            
--'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,TOTAL_AMOUNT * - 1),1)   AS AMOUNT ,                         
--TYPE  ,POSTING_DATE                                        
--FROM #ARREPORT   
--WHERE TYPE IN('F')                                        
--AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'   
                              
SELECT  SOURCE , SOURCE_ROW_ID, PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                                        
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                               
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                        
DESCRIPTION AS ACTIVITY_DESC,                                                            
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TOTAL_PREMIUM,0) * - 1),1)   AS AMOUNT ,  
TYPE  ,POSTING_DATE , 0 AS DISPLAY_ORDER    
FROM @ARREPORT                               
WHERE TYPE IN('F')                                        
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'   
AND ISNULL(TOTAL_PREMIUM,0) <> 0

UNION                                         
                                        
                                        
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE, SOURCE_TRAN_DATE,                                
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                                 
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                        
'Service Fees charged ' AS ACTIVITY_DESC,                                                            
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,INSF_FEE * - 1),1)   AS AMOUNT,                                         
TYPE  ,POSTING_DATE , 1 AS DISPLAY_ORDER                                       
FROM @ARREPORT                  
WHERE INSF_FEE <> 0                                         
AND  TYPE NOT IN('N','L','R','F')                                        
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
                                        
UNION                                        
                                        
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE, SOURCE_TRAN_DATE,                                       
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,                            
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                                        
'NSF Fees charged ' as ACTIVITY_DESC,                                                            
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,NSF_FEE * - 1 ),1)   AS AMOUNT,                                         
TYPE  ,POSTING_DATE  , 2 AS DISPLAY_ORDER                                      
FROM @ARREPORT                                          
WHERE NSF_FEE <> 0           
AND  TYPE NOT IN('N','L','R','F')                                        
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
                                        
UNION         
                                        
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE, SOURCE_TRAN_DATE,             
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,    
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,          
'Late Fees charged ' as ACTIVITY_DESC,                                                            
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,LATE_FEE * - 1 ),1)   AS AMOUNT,                          
TYPE  ,POSTING_DATE , 3 AS DISPLAY_ORDER                                       
FROM @ARREPORT                                      
WHERE LATE_FEE <> 0                                         
AND  TYPE NOT IN('N','L','R','F')                                        
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
                                    
UNION                                         
                                        
SELECT  SOURCE , SOURCE_ROW_ID,PRINTED_ON_NOTICE,SOURCE_TRAN_DATE,                 
CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS BILL_DATE,            
CONVERT(VARCHAR(20),POSTING_DATE,101) AS EFF_DATE,                               
'Reinstatement Fees charged ' as ACTIVITY_DESC,                                                           
'$' + CONVERT(VARCHAR(30),CONVERT(MONEY,REINS_FEE * - 1 ),1)   AS AMOUNT,                  
TYPE  ,POSTING_DATE    , 4 AS DISPLAY_ORDER                                
FROM @ARREPORT                                          
WHERE REINS_FEE <> 0                                         
AND  TYPE NOT IN('N','L','R','F')                                        
AND ISNULL(ltrim(rtrim(PRINTED_ON_NOTICE)),'N') <> 'Y'                                        
        
ORDER BY SOURCE_TRAN_DATE  , DISPLAY_ORDER 
                         

-- Ravindra (07-09-2007)                           
-- Mark these records as printed on Notice so that they does not get repeated on next notice.                          
IF @IS_EOD = 1
BEGIN                          
		UPDATE ACT_PREMIUM_PROCESS_DETAILS SET PRINTED_ON_NOTICE  = 'Y'           
		WHERE PROCESS_ID IN (  SELECT SOURCE_ROW_ID FROM @ARREPORT WHERE SOURCE = 'PPD' )                          

		UPDATE ACT_CUSTOMER_OPEN_ITEMS  SET PRINTED_ON_NOTICE  = 'Y'                          
		WHERE IDEN_ROW_ID IN (  SELECT SOURCE_ROW_ID FROM @ARREPORT WHERE SOURCE = 'COI' )                          

		UPDATE ACT_CUSTOMER_BALANCE_INFORMATION SET PRINTED_ON_NOTICE  = 'Y'                          
		WHERE ROW_ID IN (  SELECT SOURCE_ROW_ID FROM @ARREPORT WHERE SOURCE = 'BAL' ) 

		--Ravindra(03-07-2008) Manual Adjustment records
	    UPDATE ACT_RECONCILIATION_GROUPS SET PRINTED_ON_NOTICE  = 'Y'                          
		WHERE GROUP_ID IN (  SELECT SOURCE_ROW_ID FROM @ARREPORT WHERE SOURCE = 'REC' ) 
                     
END                          
                                                         
DECLARE @MESSAGE VARCHAR(1000) 

-- Ravindra: iTrack 4657 for cancelled policies no need to show this message
IF ( ISNULL(@INSTALLMENT_FEES,0) <> 0 AND @IS_HOME_EMP <> 1 AND @CURRENT_STATUS <> 'CANCEL' AND @CURRENT_STATUS <> 'RESCIND' 
		AND @CURRENT_STATUS <> 'SCANCEL' )                                    
		SET @MESSAGE = 'Each Installment includes a $' + CONVERT(VARCHAR(30),CONVERT(MONEY,@INSTALLMENT_FEES),1) + ' service fee.'                                        
ELSE                
		SET @MESSAGE = ''              
                                        
-- Footer message                                         
                                       
--DECLARE @FOOTER_MESSAGE VARCHAR(1000)  
DECLARE @NSF_MESSAGE	Varchar(200),
		@LF_MESSAGE		Varchar(200)
            
--SET @FOOTER_MESSAGE = ' '                                         
SET @NSF_MESSAGE = ''
SET @LF_MESSAGE  = ''
    
IF  @NON_SUFFICIENT_FUND_FEES  IS NOT NULL                        
BEGIN                                         
--		SET @FOOTER_MESSAGE = @FOOTER_MESSAGE + 'A $' +  CONVERT(VARCHAR(30),CONVERT(MONEY,@NON_SUFFICIENT_FUND_FEES),1)                          
--		+ ' NSF fee is charged if your check is returned due to non sufficient funds.'                     
		SET @NSF_MESSAGE = 'A $' +  CONVERT(VARCHAR(30),CONVERT(MONEY,@NON_SUFFICIENT_FUND_FEES),1)                          
		+ ' NSF fee will be charged if your check is returned due to non-sufficient funds.' 
END                                        
              
IF  @LATE_FEES  IS NOT NULL                                        
BEGIN                   
--		SET @FOOTER_MESSAGE = @FOOTER_MESSAGE + '            A $' +  CONVERT(VARCHAR(30),CONVERT(MONEY,@LATE_FEES),1)            
--		+ ' late fee will be charged if a Cancellation Notice is issued.'                                        

		SET @LF_MESSAGE = 'A $' +  CONVERT(VARCHAR(30),CONVERT(MONEY,@LATE_FEES),1)                                        
		+ ' late fee will be charged if a Cancellation Notice is issued.' 
END                   


DECLARE @MSG_DO_NOT_PAY Varchar(1000)

IF ( @PLAN_PAYMENT_MODE	= 	@EFT ) 
BEGIN 
	SET @MSG_DO_NOT_PAY  = 'Please do not send payment directly to the Company. Your bank account will be charged the minimum due on or before ' + CONVERT(VARCHAR,@DUE_DATE,101) + '.' 
END
ELSE IF (@PLAN_PAYMENT_MODE	= 	@CREDITCARD ) 
BEGIN 
	SET @MSG_DO_NOT_PAY  = 'Please do not send payment directly to the Company. Your credit card will be charged the minimum due on or before ' + CONVERT(VARCHAR,@DUE_DATE,101) + '.'
END
ELSE
BEGIN 
	SET @MSG_DO_NOT_PAY  = ''
END


-- Record Set 3 Footer messages                
--SELECT @MESSAGE As MESSAGE_INS ,   @FOOTER_MESSAGE  MESSAGE_FEES                       
SELECT @MESSAGE As MESSAGE_INS ,   @NSF_MESSAGE AS NSF_MESSAGE , @LF_MESSAGE AS  LF_MESSAGE    ,
		@MSG_DO_NOT_PAY AS MSG_DO_NOT_PAY                   
 
CREATE TABLE #ARREPORT_INS              
(     
		[IDEN_ID]	Int IDENTITY(1,1) not null,                
		[AMOUNT]	Decimal(18,2) ,              
		[DUE_DATE]	Datetime,        
		[INS_NO]    Int , 
		[CURRENT_TERM]	Int
)       

INSERT INTO #ARREPORT_INS                                     
SELECT 
	SUM( ISNULL(OI.TOTAL_DUE,0)-ISNULL(OI.TOTAL_PAID,0)),
	CONVERT(VARCHAR,OI.DUE_DATE,101),
	B.INSTALLMENT_NO    , B.CURRENT_TERM                                  
FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)    
INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS B  WITH(NOLOCK) 
	ON OI.INSTALLMENT_ROW_ID  = B.ROW_ID                                        
WHERE  OI.CUSTOMER_ID = @CUSTOMER_ID                                         
AND OI.POLICY_ID = @POLICY_ID   
AND OI.ITEM_TRAN_CODE_TYPE <> 'FEES'  
AND OI.ITEM_TRAN_CODE	 <>'INSF' 
GROUP BY B.INSTALLMENT_NO, OI.DUE_DATE  , B.CURRENT_TERM  
HAVING  SUM((ISNULL(OI.TOTAL_DUE,0))-(ISNULL(OI.TOTAL_PAID,0))) <> 0                                              
ORDER BY  B.CURRENT_TERM  , B.INSTALLMENT_NO 

-- FETCH FEE ONLY
DECLARE @ARREPORT_INS_FEE   TABLE
(  
		[IDEN_ID] int IDENTITY(1,1) not null,                       
		[AMOUNT]  Decimal(18,2),
		[DUE_DATE] datetime,            
		[INS_NO]   int       
)

INSERT INTO @ARREPORT_INS_FEE                                                   
SELECT 
SUM( ISNULL(OI.TOTAL_DUE,0)-ISNULL(OI.TOTAL_PAID,0)) ,                                              
CONVERT(VARCHAR,OI.DUE_DATE,101) --AS DUE_DATE 
,B.INSTALLMENT_NO --AS INS_NO                                   
FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)                                           
INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS B WITH(NOLOCK)                             
	ON OI.INSTALLMENT_ROW_ID  = B.ROW_ID                    
WHERE  B.CUSTOMER_ID = @CUSTOMER_ID                               
AND B.POLICY_ID = @POLICY_ID  
AND B.CURRENT_TERM = @CURRENT_TERM  
AND OI.ITEM_TRAN_CODE_TYPE = 'FEES'  
AND OI.ITEM_TRAN_CODE = 'INSF' 
GROUP BY B.INSTALLMENT_NO, OI.DUE_DATE  , ISNULL(B.CURRENT_TERM,50)
HAVING  SUM((ISNULL(OI.TOTAL_DUE,0))-(ISNULL(OI.TOTAL_PAID,0))) <> 0                                              
ORDER BY B.INSTALLMENT_NO


-- UPDATE INSTALLMENT AMOUNT WITH FEE AMOUNT

UPDATE #ARREPORT_INS
SET #ARREPORT_INS.AMOUNT = #ARREPORT_INS.AMOUNT + FEES.AMOUNT
FROM #ARREPORT_INS INNER JOIN @ARREPORT_INS_FEE FEES
	ON #ARREPORT_INS.INS_NO = FEES.INS_NO
WHERE #ARREPORT_INS.DUE_DATE > @DUE_DATE
	AND #ARREPORT_INS.DUE_DATE = FEES.DUE_DATE

 
DECLARE @SUM_AMOUNT NVARCHAR(100)

UPDATE #ARREPORT_INS
SET DUE_DATE = @DUE_DATE WHERE
CAST(CONVERT(VARCHAR,DUE_DATE, 101) AS DATETIME) <=  
CAST(CONVERT(VARCHAR,@DUE_DATE, 101) AS DATETIME) 

IF @IS_EOD = 1
BEGIN  
	--Ravindra(02-18-2008): Update Due Dates of items included in first installment
	--Ravindra(1-12-2010): As per Enhancement 6906, Make Sweep date for installment equal to due date 
	UPDATE ACT_CUSTOMER_OPEN_ITEMS SET DUE_DATE = @DUE_DATE ,
			SWEEP_DATE = @DUE_DATE 
	WHERE CUSTOMER_ID	= @CUSTOMER_ID 
	AND POLICY_ID		= @POLICY_ID 
	AND INSTALLMENT_ROW_ID 
	IN (
		SELECT INSD.ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
		INNER JOIN #ARREPORT_INS INS_SCH
		ON INSD.INSTALLMENT_NO  = INS_SCH.INS_NO
		AND INSD.CURRENT_TERM	= INS_SCH.CURRENT_TERM 
		WHERE INSD.CUSTOMER_ID  = @CUSTOMER_ID 
		AND   INSD.POLICY_ID	= @POLICY_ID 
		AND CAST(CONVERT(VARCHAR,INS_SCH.DUE_DATE, 101) AS DATETIME) <=  
		CAST(CONVERT(VARCHAR,@DUE_DATE, 101) AS DATETIME) 
		)
END

/*
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
SELECT ISNULL(TOTAL_DUE ,0) - ISNULL(TOTAL_PAID, 0)  , DUE_DATE
FROM ACT_CUSTOMER_OPEN_ITEMS  WITH(NOLOCK)
WHERE CUSTOMER_ID =@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID
AND UPDATED_FROM =  'J'
AND ITEM_TRAN_CODE_TYPE  = 'JE'

-- Fetch Fee Reversal records
INSERT INTO #ARREPORT_JUNRAL        
SELECT ISNULL(TOTAL_DUE ,0) - ISNULL(TOTAL_PAID, 0)  , DUE_DATE 
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
*/


DECLARE @ARREPORT_JUNRAL TABLE
(   
 [IDEN_COL] int IDENTITY(1,1) not null,
 [AMOUNT_J] decimal(18,2),
 [CREATED_DATETIME] datetime            
)
INSERT INTO @ARREPORT_JUNRAL         
SELECT ISNULL(TOTAL_DUE ,0) - ISNULL(TOTAL_PAID, 0)  , DUE_DATE 
FROM ACT_CUSTOMER_OPEN_ITEMS  WITH(NOLOCK)
WHERE CUSTOMER_ID =@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID
AND 
(	
	UPDATED_FROM ='F'
	OR 
	( UPDATED_FROM =  'J' AND ITEM_TRAN_CODE_TYPE  = 'JE' )
	OR
	(UPDATED_FROM =  'P' AND ITEM_TRAN_CODE = 'CANCP') 
)

-- Run While loop for updation of amount
declare @AMOUNT decimal(18,2)
declare @TRAN_DATE DATETIME,
		@IDENT_COL Int

SET @IDENT_COL = 1    
WHILE (1= 1) 
BEGIN 
	IF NOT EXISTS (SELECT IDEN_COL FROM @ARREPORT_JUNRAL    WHERE IDEN_COL = @IDENT_COL ) 
	BEGIN 
		BREAK
	END
	SELECT  @AMOUNT = AMOUNT_J ,
			@TRAN_DATE  = CREATED_DATETIME
	FROM @ARREPORT_JUNRAL  
	WHERE IDEN_COL = @IDENT_COL 

	UPDATE #ARREPORT_INS
 	SET #ARREPORT_INS.AMOUNT = #ARREPORT_INS.AMOUNT + @AMOUNT
	WHERE CAST(CONVERT(VARCHAR,#ARREPORT_INS.DUE_DATE,101) AS DATETIME)
			> =  CAST(CONVERT(VARCHAR, @TRAN_DATE ,101) AS DATETIME)
	AND IDEN_ID = (SELECT TOP 1 TMP_IN.IDEN_ID FROM #ARREPORT_INS TMP_IN  WITH(NOLOCK) 
			WHERE 	CAST(CONVERT(VARCHAR, TMP_IN.DUE_DATE,101) AS DATETIME)
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
WHERE DUE_DATE = @DUE_DATE

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


--RecordSet 4 Installment schedule
SELECT                                       
'$ ' + convert(varchar(30), convert(money,AMOUNT),1) AS AMOUNT,
CONVERT(VARCHAR, DUE_DATE,101) AS DUE_DATE,
CAST (DUE_DATE AS DATETIME) AS DUE_DATE_ACTUAL ,
INS_NO AS INS_NO
FROM #ARREPORT_INS
WITH(NOLOCK) GROUP BY #ARREPORT_INS.DUE_DATE ,#ARREPORT_INS.INS_NO,#ARREPORT_INS.AMOUNT
ORDER BY DUE_DATE_ACTUAL  --INS_NO 
ASC 


DROP TABLE #ARREPORT_INS         


-- Recordset 5 Mortagagee Details
--Ravindra(03-10-2008) fetch mortgagee information
IF( @ADD_INT_ID <> 0 AND (@LOB_ID  = 1 OR @LOB_ID = 6) )
BEGIN 
	SELECT
	CASE ISNULL(POL_ADD.HOLDER_ID,0) WHEN 0 THEN POL_ADD.HOLDER_NAME  ELSE ADD_IN.HOLDER_NAME END AS HOLDER_NAME,                                       
	CASE ISNULL(POL_ADD.HOLDER_ID,0) WHEN 0 THEN POL_ADD.HOLDER_ADD1  ELSE RTRIM(ADD_IN.HOLDER_ADD1) END AS HOLDER_ADDRESS1, 
	CASE ISNULL(POL_ADD.HOLDER_ID,0) WHEN 0 THEN POL_ADD.HOLDER_ADD2  ELSE RTRIM(ADD_IN.HOLDER_ADD2) END AS HOLDER_ADDRESS2,     
	CASE ISNULL(POL_ADD.HOLDER_ID,0) WHEN 0 THEN POL_ADD.HOLDER_CITY  ELSE ADD_IN.HOLDER_CITY END AS HOLDER_CITY,   
	CASE ISNULL(POL_ADD.HOLDER_ID,0) WHEN 0 THEN MCSL2.STATE_CODE	  ELSE MCSL.STATE_CODE END AS STATE_CODE,   
	CASE ISNULL(POL_ADD.HOLDER_ID,0) WHEN 0 THEN POL_ADD.HOLDER_ZIP  ELSE ADD_IN.HOLDER_ZIP END AS HOLDER_ZIP
	FROM POL_HOME_OWNER_ADD_INT POL_ADD
	LEFT JOIN MNT_HOLDER_INTEREST_LIST ADD_IN
	ON POL_ADD.HOLDER_ID = ADD_IN.HOLDER_ID
	LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL                                   
	ON ADD_IN.HOLDER_STATE = MCSL.STATE_ID     
	LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL2                       
	ON POL_ADD.HOLDER_STATE = MCSL2.STATE_ID   
	WHERE POL_ADD.CUSTOMER_ID = @CUSTOMER_ID 
	AND POL_ADD.POLICY_ID	  = @POLICY_ID 
	--Ravindra(10-14-2008): Pick mortagee from latest version 
	AND POL_ADD.POLICY_VERSION_ID = @MAX_VERSION_ID --@CURRENT_VERSION_ID 
	AND POL_ADD.ADD_INT_ID   = @ADD_INT_ID 
	AND POL_ADD.DWELLING_ID  = @DWELLING_ID 

END
                               
END                                              

--GO
--
--
--EXEC Proc_GetPremiumNoticeData 1432,	38 ,	2  ,null,null,NULL
--
--
--ROLLBACK TRAN
--





GO

