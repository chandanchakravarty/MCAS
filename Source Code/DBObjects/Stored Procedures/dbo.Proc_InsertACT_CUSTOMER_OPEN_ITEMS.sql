IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_CUSTOMER_OPEN_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_CUSTOMER_OPEN_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------------      
Proc Name       : dbo.Proc_InsertACT_CUSTOMER_OPEN_ITEMS      
Created by      : Shafi      
Date            : 7 Sep 2006     
Purpose         : To insert records  For NFS entery       
Revison History :        
Used In         : Wolverine       
exec Proc_InsertACT_CUSTOMER_OPEN_ITEMS @TOTAL_DUE = 1.119900000000000e+002, @CUSTOMER_ID = 0, @POLICY_ID = 0, @POLICY_VERSION_ID = 0, @TRAN_ID = 26, @POLICY_NUMBER = N'A1000046'
modified by 	: Pravesh k Chandel
Modified date	: 2 aug-2007
Purpose		: to write Late fee in case of cancellation Noy pay DB

modified by 	: Praveen Kasana
Modified date	: 26 May 2008
Purpose		: Installment Fee written 

------------------------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------------------------------------*/        
-- drop proc dbo.Proc_InsertACT_CUSTOMER_OPEN_ITEMS       
CREATE PROC dbo.Proc_InsertACT_CUSTOMER_OPEN_ITEMS       
(        
 @TOTAL_DUE decimal(18,2),
 @CUSTOMER_ID int,
 @POLICY_ID int,
 @POLICY_VERSION_ID int,
 @POLICY_NUMBER varchAR(20)=NULL,
 @TRAN_ID INT
)        
AS        
BEGIN        
   
--Get The Policy State,Country,LOb
               
DECLARE @STATEID INT
DECLARE @LOBID INT
DECLARE @AGENCY_ID Int 
DECLARE @COUNTRY_ID  Int 
DECLARE @ROW_ID INT
DECLARE @BILL_TYPE VARCHAR(5)

  --Check Wether Policy Id,Policy Version Id Is Supplied if not fetch it from Policy Number  

IF (@POLICY_ID=0 or @POLICY_VERSION_ID=0 )
BEGIN
	/*SELECT TOP 1
	@BILL_TYPE = CPL.BILL_TYPE,
	@POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID = POLICY_VERSION_ID,
	@CUSTOMER_ID = IsNull(CPL.CUSTOMER_ID,0)	
	FROM POL_CUSTOMER_POLICY_LIST CPL with(nolock)
	LEFT JOIN APP_LIST AL with(nolock) ON AL.APP_ID = CPL.APP_ID 
	AND AL.APP_VERSION_ID = CPL.APP_VERSION_ID 
	AND AL.CUSTOMER_ID = CPL.CUSTOMER_ID
	WHERE POLICY_NUMBER = @POLICY_NUMBER
	GROUP BY IsNull(POLICY_ID,0), IsNull(POLICY_ACCOUNT_STATUS,0), CPL.CUSTOMER_ID , CPL.BILL_TYPE*/
    
    SELECT TOP 1 @BILL_TYPE  = BILL_TYPE , 
    @POLICY_ID = isnull(POLICY_ID,0), @POLICY_VERSION_ID = POLICY_VERSION_ID, 
    @CUSTOMER_ID = isnull(CUSTOMER_ID,0)
    FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)
    WHERE POLICY_NUMBER = @POLICY_NUMBER
    ORDER BY POLICY_VERSION_ID DESC


	-- Check if Policy is DB or AB type
	IF(@BILL_TYPE = 'AB')
	BEGIN
		RETURN -2
	END	

	if IsNull(@POLICY_ID,0) = 0 
	BEGIN
		--Policy number is not valid, hence exiting with return status
		return 6			
	END
END
         
SELECT @STATEID = STATE_ID,                                      
@LOBID = POLICY_LOB ,            
@AGENCY_ID = AGENCY_ID,
@COUNTRY_ID =COUNTRY_ID                                     
FROM POL_CUSTOMER_POLICY_LIST with(nolock)                              
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                      
POLICY_ID = @POLICY_ID AND                                      
POLICY_VERSION_ID = @POLICY_VERSION_ID 
   
--Get The Transanction Code , Description ,Type according Transanction Id
DECLARE @TRANS_CODE VARCHAR(20)
DECLARE @TRANS_TYPE VARCHAR(20)
DECLARE @TRANS_DES VARCHAR(50)
DECLARE @UPDATED_FROM CHAR(2)


SELECT @TRANS_CODE=TRAN_CODE,@TRANS_DES=DISPLAY_DESCRIPTION,@TRANs_TYPE=TRAN_TYPE
FROM ACT_TRANSACTION_CODES with(nolock) WHERE TRAN_ID=@TRAN_ID
    
if(@TRAN_ID=37)
BEGIN 	
	SET  @UPDATED_FROM='N' --Non Sufficient Fund Fees
	SET  @TRANS_DES = 'NSF Fees written'
END
ELSE IF(@TRAN_ID=38)
BEGIN 
	SET  @UPDATED_FROM='L'  --Late Fee
	SET  @TRANS_DES = 'Late Fees written'  -- Changed by Pravesh on 2 Aug 2007 
END
IF(@TRAN_ID =  39)-- For Itrack Issue #4906
BEGIN 
	SET  @UPDATED_FROM='P'  
	SET  @TRANS_DES = 'Reinstatement Fees written'
END
--Added For Charge Instllment Fee Screen
IF(@TRAN_ID =  7)
BEGIN 
	SET  @UPDATED_FROM='P'  
	SET  @TRANS_DES = 'Installment Fees written'
END
--ELSE IF(@TRAN_ID=39)
--BEGIN 
	--SET  @UPDATED_FROM='P'  --Ins Fee
	--SET  @TRANS_DES = 'Installment Fees written'  -- Changed by Praveen on 23 May 2008
	--SET  @TRANS_CODE = 'INSF'
	--SET  @TRANS_TYPE = 'FEES'

--END

INSERT INTO ACT_CUSTOMER_OPEN_ITEMS      
(        
	UPDATED_FROM,SOURCE_TRAN_DATE,SOURCE_EFF_DATE,POSTING_DATE,TOTAL_DUE,TOTAL_PAID,
	AGENCY_COMM_APPLIES,AGENCY_COMM_PERC,AGENCY_COMM_AMT,AGENCY_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,PAYMENT_DATE,
	DATE_FULL_PAID,PAYMENT_STATUS,NOT_COUNTED_RECEIVABLE,PAYOR_TYPE,DIV_ID,DEPT_ID,PC_ID,IS_TEMP_ENTRY,IN_RECON,IS_PREBILL,
	BILL_CODE,GROSS_AMOUNT,ITEM_TRAN_CODE,ITEM_TRAN_CODE_TYPE,TRAN_ID,CASH_ACCOUNTING,RECUR_JOURNAL_VERSION,JE_RECON_COUNTER,
	AMT_IN_RECON,OPEN_RECON_CTR,LOB_ID,SUB_LOB_ID,COUNTRY_ID,
        STATE_ID,COMMISSION_TYPE,ITEM_STATUS,IS_CHECK_CREATED,ITEM_ORIGINAL_STATUS,
	TRANS_DESC,
	DUE_DATE,
	PROCESSED_AMOUNT_FOR_AGENCY,INSTALLMENT_ROW_ID,NET_PREMIUM
)        
VALUES        
(        
	@UPDATED_FROM,GETDATE(),GETDATE(),GETDATE(),@TOTAL_DUE,0,
	'N',NULL,NULL,@AGENCY_ID,@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,NULL,
	NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,
	NULL,NULL,@TRANS_CODE,@TRANS_TYPE,null,null,null,null,
	null,null,@LOBID,null,@COUNTRY_ID,@STATEID
       ,null,null,null,null,@TRANS_DES,GETDATE(),
	null,null,null     
)   
	
--Update The Record  SOURCE_ROW_ID ,SOURCE_NUM    
UPDATE ACT_CUSTOMER_OPEN_ITEMS SET SOURCE_ROW_ID=@@IDENTITY,SOURCE_NUM=@@IDENTITY WHERE IDEN_ROW_ID =@@IDENTITY
   
--Inserting the entry in customer balance informartion  for LATE FEE and NSF
INSERT INTO ACT_CUSTOMER_BALANCE_INFORMATION    
	(    
	CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,    
	OPEN_ITEM_ROW_ID, AMOUNT, AMOUNT_DUE,    
	TRAN_DESC,    
	UPDATED_FROM, CREATED_DATE,    
	IS_PRINTED, PRINT_DATE, SOURCE_ROW_ID    
	)    
	SELECT     
	CDOI.CUSTOMER_ID, CDOI.POLICY_ID, CDOI.POLICY_VERSION_ID,     
	CDOI.IDEN_ROW_ID, CDOI.TOTAL_DUE, CDOI.TOTAL_DUE,    
	CDOI.TRANS_DESC,    
	@UPDATED_FROM, GETDATE(),    
	0,NULL, CDOI.SOURCE_ROW_ID    
	FROM     
	ACT_CUSTOMER_OPEN_ITEMS CDOI    
	WHERE 	CDOI.IDEN_ROW_ID = @@IDENTITY
		

END        
























GO

