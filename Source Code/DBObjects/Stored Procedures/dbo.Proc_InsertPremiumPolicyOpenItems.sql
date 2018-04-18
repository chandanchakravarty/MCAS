IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPremiumPolicyOpenItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPremiumPolicyOpenItems]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN 
--drop proc dbo.Proc_InsertPremiumPolicyOpenItems
--go 
/*----------------------------------------------------------------------
Created By		: Vijay Joshi
Created Date	: July, 18 2005
Purpose			: To insert the open items against customer and agency

Revision History---------------------------------------------------->

Revision Date	:07, Sep 2005
Purpose			: To implement new functionality as per new document
				  Debit entry in positive amount
				  Credit entry in negative amount
Modified By 	: Ravindra Gupta
Modified On		: 09-15-2006
Purpose			: Added Parameter @COMMISSION_CLASS , @RISK_ID & @IS_SAME_POLICY
				To Calculate & Post Commission Specific to each risk in Policy 
				based on Commission Class in which the risk fall.

Modified By 	: Ravindra Gupta
Modified On		: 10-13-2006
Purpose			: Resolved problem of Premium Posting in case of Home Employee
				Discount need to be Posted to Commission Incurred account as well
				Transaction codes corrected

Modified By 	: Ravindra Gupta
Modified On		: 10-16-2006
Purpose			: Resolved problem of Rounding in case of Installment plan 
				The difference in Total Premium & Total Of Installment created will be now added to 
				first Installment

Modified By 	: Ravindra Gupta
Modified On		: 11-17-2006
Purpose			: Added a check so that Complete App Commission not to be granted for Renewal Policies

Modified By 	: Ravindra Gupta
Modified On		: 11-20-2006
Purpose			: Old implementation is considering only one deposit (suspense payment)
					made changes to handle multiple Deposits

Modified By 	: Ravindra Gupta
Modified On		: 11-21-2006
Purpose			: Changes for fees not be charged from home employee


Modified By 	: Pravesh K Chandel
Modified On		: 6 Aug 2007
Purpose			: Changes duae date casting for the Month April,June,Sep,Nov as these months are of 30 days

Modified By 	: Praveen Kasana
Modified On		: 2 Sep 2009
Purpose			: Itrack 6320 :
				When due date of EFT installment is different than effective date of the policy, the 1st EFT installment
				should be set based on the 15 day rule, the next EFT installment should be due on the same date in the following month,
				and the next EFT installment should be due on the same date in the next month, and so forth.  

Modified By 	: Praveen Kasana
Modified On		: 30 Oct 2009
Purpose			: Itrack 6775 
				
          
-----------------------------------------------------------------------*/
-- drop proc dbo.Proc_InsertPremiumPolicyOpenItems

CREATE proc  [dbo].[Proc_InsertPremiumPolicyOpenItems]
 (
	@CUSTOMER_ID 		int,			-- Id of customer whose premium policy will be posted
	@APP_ID 		int,			-- Application id 
	@APP_VERSION_ID		int,			-- Application version  id
	@POLICY_ID		int,			-- Policy identification number
	@POLICY_VERSION_ID	int,			-- Policy version identification number
	@PREMIUM_AMOUNT		decimal(25,8),		-- Premium amount
	@MCCA_FEES		decimal(20,8),		-- MCCA fees amount
	@OTHER_FEES		decimal(20,8),		-- Other fees
	@TRANS_DESC		varchar(255) OUTPUT,	-- Transaction descrtipyion 
	@COMMISSION_CLASS	int  =null,		-- Commission Class in which risk Fall
	@RISK_ID		int  =null,		-- RiskID of the risk as in risk info table
	@RISK_TYPE		varchar(15),		-- Type Of Risk for which premium is posted
	@IS_SAME_POLICY		varchar(2)=null,	-- Will hold value 'Y' if the SP is called for same
							-- policy with different risk. This is used to track 
							-- the installment fee .Ins fee will be paid once for a policy
							-- for each installment	 
	@PARAM2			varchar(10),		-- Source Row ID
	@PARAM3			varchar(10),		-- User Id
	@PARAM4			varchar(10)	,	-- Extra parameters
	@RetVal			int  OUTPUT ,
	@TOTAL_FEES		Decimal(18,2) Out

)
AS
BEGIN
	

	/*Varibale declaration for retreiving the information from the database*/
	DECLARE @IS_PREBILL 		SMALLINT,
			@BILL_TYPE 		CHAR(2),
			@INSTALLMENT 		SMALLINT,
			@TRAN_ID 		SMALLINT,
			@HOME_EMP 		CHAR(1),
			@COMPLETE_APP 		CHAR(1),
			@CSR			Int,
			@PROPRTY_INSP_CREDIT 	CHAR(1),
			@AGENCY_REG_COMM_PRCT 	DECIMAL(20,8),
			@AGENCY_REG_COMM	DECIMAL(20,8),
			
			@COMPLETE_APP_COMM 	DECIMAL(20,8),
			@COMPLETE_APP_COMM_PRCT DECIMAL(20,8),
			@PROPERTY_INS_COMM 	DECIMAL(20,8),
			@PROPERTY_INS_COMM_PRCT DECIMAL(20,8),
			@COMMISSION_AMOUNT_TYPE Char(1),

			@AGENCY_ADDI_COMM_PRCT 	DECIMAL(20,8),
			@AGENCY_ADDI_COMM 	DECIMAL(20,8),
		
			@MODE 			CHAR(1),
			@STATE_ID 		SMALLINT,
			@APP_LOB 		VARCHAR(10),
			@APP_SUBLOB 		VARCHAR(10),
			@APP_AGENCY_ID 		SMALLINT,
			@PREBILL_DATE 		DATETIME,
			@GL_ID 			SMALLINT,
			@POLICY_EFFECTIVE_DATE	DATETIME,
			@INSTALLMENT_FEES	DECIMAL(18,2),
			@APP_TERMS		NVARCHAR(10),
			@TOTAL_PREMIUM_AMOUNT	DECIMAL(25,4),
			@APP_EFFECTIVE_DATE	DATETIME,
			@MIN_INSTALLMENT_AMOUNT	DECIMAL(18,2),
			@FISCAL_ID		SMALLINT,
			@TRAN_CODE_INSTALLMENT	VARCHAR(10),
			@TRAN_TYPE_INSTALLMENT	VARCHAR(10),
			@SOURCE_ROW_ID 		INTEGER,
			@USER_ID		SMALLINT,
			@USER_CODE		VARCHAR(10),
			@USER_NAME		VARCHAR(60),
			@FULL_PAY_PLAN_ID	SMALLINT,
			@PROCESS_TERM		VARCHAR(5),
			@SELECTED_DOWN_PAYMENT_MODE Int,
			--Added For Transaction Codes 
			@TRAN_TYPE		VARCHAR(10),
			@TRAN_TYPE_CODE		VARCHAR(10),
			@COMMISSION_TYPE_CODE	VARCHAR(10),
			@IS_REWRITE_POLICY		Char(1) ,
			@CURRENT_TERM 		SmallInt,	
			
			-- Added For Entity Name & Transaction Description 
			@CUSTOMER_NAME Varchar(200),
			@AGENCY_NAME Varchar(200),
			@TRAN_DESC Varchar(500),
			
			@EFT_TENTATIVE_DATE Int,
			@EFT   INt,
			@TOTAL_SERVICE_CHARGE 	Decimal(18,2),

			--Ravindra(09-09-20009):optimisation
			@DIV_ID				Int, 
			@DEPT_ID			Int, 
			@PC_ID				Int,	
			@POLICY_LOB			Int,
			@POLICY_SUBLOB		Int,
			@COUNTRY_ID			Int
			
			SET @TOTAL_SERVICE_CHARGE = 0 
			SET @EFT   = 11973

			SET @SOURCE_ROW_ID 	= @PARAM2 
			SET @USER_ID		= @PARAM3	
			SET @TOTAL_FEES=0 

			select @USER_CODE = USER_LOGIN_ID, @USER_NAME =  (USER_FNAME + ' ' + USER_LNAME) from MNT_USER_LIST 
				WITH(NOLOCK) where user_id = @USER_ID 

			SELECT @FULL_PAY_PLAN_ID = IDEN_PLAN_ID 	FROM ACT_INSTALL_PLAN_DETAIL
				WITH(NOLOCK) WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0) = 1
			
			SET @PROCESS_TERM =NULL
		
			SELECT @PROCESS_TERM = TERM_TYPE FROM ACT_PREMIUM_PROCESS_DETAILS
				WITH(NOLOCK) WHERE PROCESS_ID = @SOURCE_ROW_ID

			-- If Process Term is nnot found treat it as First Term 
			-- Required if exception is occured & default premium(400) is posted
			SET @PROCESS_TERM = ISNULL(@PROCESS_TERM,'F')


	---------------	



	/*Variables for holding various account numbers, to be used in posting*/	
	DECLARE @FULLPREMIUM_ACCOUNT		SMALLINT,	--Premium account number
			@MCCA_FEES_ACCOUNT 		SMALLINT,	--MCCA fee account number
			@OTHER_FEES_ACCOUNT 		SMALLINT,	--Other fee account number
			@PREMIUM_INCOME_ACCOUNT 	SMALLINT,	--Premium income account number
			@COMM_EXPENSE_ACCOUNT 		SMALLINT,	--Comm expense account number
			@COMM_PAYABLE_ACCOUNT 		SMALLINT	--Commission payable account number

	--Initializing the values
	SET	@TRAN_TYPE_INSTALLMENT = 'FEES'
	SET	@TRAN_CODE_INSTALLMENT = 'INSF'

	CREATE TABLE #TMP_POST
	(	
		IDENT_COL 			[int] IDENTITY(1,1) NOT NULL ,      
		ACCOUNT_ID 			SMALLINT,
		AMT 				DECIMAL(20,8),
		APP_ID         			INT,
		APP_VERSION_ID 			INT,
		CUSTOMER_ID			INT,
		AGENCY_ID			INT,
		SOURCE_EFFECTIVE_DATE		DATETIME,
		DUE_DATE			DATETIME,
		TRANS_DESC			VARCHAR(100),
		POLICY_ID			INT,
		POLICY_VERSION_ID		INT,
		TRAN_CODE			VARCHAR(10),
		TRAN_TYPE			VARCHAR(10),
		ACCOUNTS			BIT,
		OPEN_ITEMS			BIT,
		INSTALLMENT_ROW_ID		INT,
		TRAN_ENTITY			Varchar(200),
		TRAN_DESC 			Varchar(500) ,
		COMMISSION_PERCENTAGE	Decimal(18,2)
	)
	
	-- Fetching System Parameters 

	DECLARE @SYS_COMPLETE_APP_COMM_APPLICABILITY Int
	DECLARE @SYS_PROPERTY_INS_COMM_APPLICABILITY Int
	
	SELECT  @SYS_COMPLETE_APP_COMM_APPLICABILITY = ISNULL(SYS_COMPLETE_APP_COMM_APPLICABILITY,0) 
			,@SYS_PROPERTY_INS_COMM_APPLICABILITY = ISNULL(SYS_PROPERTY_INS_COMM_APPLICABILITY,0) 
	FROM MNT_SYSTEM_PARAMS WITH(NOLOCK)

	--Fetching the application or policy information
	SELECT 	@BILL_TYPE 				= 	ISNULL(CPL.BILL_TYPE,''), 
			@INSTALLMENT 			= 	ISNULL(CPL.INSTALL_PLAN_ID, 0),
			@COMPLETE_APP 			= 	ISNULL(CPL.COMPLETE_APP, 'N'), 
			@PROPRTY_INSP_CREDIT 	= 	CASE ISNULL(CPL.PROPRTY_INSP_CREDIT, 0) WHEN 10963 THEN 'Y' ELSE 'N' END,  
			@STATE_ID 				= 	CPL.STATE_ID, 
			@APP_LOB 				= 	CPL.POLICY_LOB, 
			@APP_SUBLOB           	= 	CPL.POLICY_SUBLOB, 
			@APP_AGENCY_ID 			= 	CPL.AGENCY_ID,
			@APP_TERMS 				= 	CPL.POLICY_TERMS,
			@POLICY_EFFECTIVE_DATE 	= 	CPL.APP_EFFECTIVE_DATE,
			@APP_EFFECTIVE_DATE		=	CPL.APP_EFFECTIVE_DATE,
			@TRANS_DESC				=	'',
			@MIN_INSTALLMENT_AMOUNT = 	ISNULL(IPD.MIN_INSTALLMENT_AMOUNT, 0),
			@TRAN_DESC 				=	POLICY_NUMBER + ' ' + POLICY_DISP_VERSION,		@SELECTED_DOWN_PAYMENT_MODE	=	DOWN_PAY_MODE,
			@CURRENT_TERM 			= 	CURRENT_TERM	,
			@CSR 					=	ISNULL(CSR,0) ,
			@EFT_TENTATIVE_DATE 	=	ISNULL(EFT_INFO.EFT_TENTATIVE_DATE,0) , 
			@IS_REWRITE_POLICY		=	ISNULL(IS_REWRITE_POLICY , 'N') , 
			@HOME_EMP				=	CASE ISNULL(IS_HOME_EMP, 0) WHEN 1 THEN 'Y' ELSE 'N' END ,
			@DIV_ID					=	CPL.DIV_ID, 
			@DEPT_ID				=	CPL.DEPT_ID, 
			@PC_ID					=	CPL.PC_ID,	
			@POLICY_LOB				=	CPL.POLICY_LOB,
			@POLICY_SUBLOB			=	CPL.POLICY_SUBLOB,
			@COUNTRY_ID				=	CPL.COUNTRY_ID
			
	FROM POL_CUSTOMER_POLICY_LIST CPL WITH(NOLOCK)
	LEFT JOIN ACT_INSTALL_PLAN_DETAIL IPD 
		ON IPD.IDEN_PLAN_ID = CPL.INSTALL_PLAN_ID
	LEFT JOIN ACT_POL_EFT_CUST_INFO EFT_INFO 
		ON  CPL.CUSTOMER_ID = EFT_INFO.CUSTOMER_ID 
		AND CPL.POLICY_ID =   EFT_INFO.POLICY_ID 
		AND CPL.POLICY_VERSION_ID = EFT_INFO.POLICY_VERSION_ID
	WHERE CPL.CUSTOMER_ID = @CUSTOMER_ID AND CPL.POLICY_ID = @POLICY_ID AND CPL.POLICY_VERSION_ID = @POLICY_VERSION_ID

	
	IF @@ERROR <> 0
		GOTO ERRHANDLER


	--Checking whetherinstall plan exists or not
	-- Ravindra(06-28-2007) : Need to update in case of DB policies only, 
	-- for AB type this will be Zero or null always
	IF (@INSTALLMENT = 0 AND @BILL_TYPE = 'DB')
	BEGIN
		-- Ravindra(06-28-2007) :  If default plan exists for this policy term move policy 
		-- to this plan else to Full Pay Plan 
		IF EXISTS (SELECT IDEN_PLAN_ID FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK)
				WHERE ISNULL(DEFAULT_PLAN,0) = 1 AND APPLABLE_POLTERM  = @APP_TERMS )
		BEGIN 
			SELECT @INSTALLMENT = IDEN_PLAN_ID
			FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK)
			WHERE ISNULL(DEFAULT_PLAN,0) = 1
			AND APPLABLE_POLTERM  = @APP_TERMS 
		END
		ELSE 
		BEGIN
			SET @INSTALLMENT = @FULL_PAY_PLAN_ID
		END

		UPDATE POL_CUSTOMER_POLICY_LIST
		SET INSTALL_PLAN_ID = @INSTALLMENT
		WHERE CUSTOMER_ID = @CUSTOMER_ID
		AND POLICY_ID = @POLICY_ID
		AND POLICY_VERSION_ID = @POLICY_VERSION_ID
	END


	--Calculating the total premium amount
	SELECT @TOTAL_PREMIUM_AMOUNT = ISNULL(@PREMIUM_AMOUNT,0) + ISNULL(@MCCA_FEES,0) + ISNULL(@OTHER_FEES,0)

	IF @@ERROR <> 0
		GOTO ERRHANDLER
		
	--Fetching the agency commission from reguler agency commission setup table
	SET @AGENCY_REG_COMM_PRCT =0


	SELECT  @AGENCY_REG_COMM_PRCT = ISNULL(COMMISSION_PERCENT,0) 
	FROM ACT_REG_COMM_SETUP WITH(NOLOCK)
		WHERE (STATE_ID = @STATE_ID OR STATE_ID = 0) AND 
			(LOB_ID = @APP_LOB OR LOB_ID = 0) 
			AND (ISNULL(SUB_LOB_ID,0) = ISNULL(@APP_SUBLOB,0)  OR ISNULL(SUB_LOB_ID,0) = 0) 
			AND CLASS_RISK = @COMMISSION_CLASS
			AND @POLICY_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE
			AND COMMISSION_TYPE = 'R'  
			AND TERM = @PROCESS_TERM
			AND ISNULL(IS_ACTIVE,'Y') = 'Y'
	ORDER BY STATE_ID DESC, LOB_ID DESC, SUB_LOB_ID DESC, CLASS_RISK DESC 
 
	
	-- Fetch Additional Commission Percentage For this Agency If Applicable
	SET @AGENCY_ADDI_COMM_PRCT =0

	SELECT @AGENCY_ADDI_COMM_PRCT = ISNULL(COMMISSION_PERCENT,0) 
	FROM ACT_REG_COMM_SETUP WITH(NOLOCK)
		WHERE (STATE_ID = @STATE_ID OR STATE_ID = 0) AND 
			(LOB_ID = @APP_LOB OR LOB_ID = 0) 
			AND (ISNULL(SUB_LOB_ID,0) = ISNULL(@APP_SUBLOB,0)  OR ISNULL(SUB_LOB_ID,0) = 0) 
			AND CLASS_RISK = @COMMISSION_CLASS
			AND @POLICY_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE
			AND COMMISSION_TYPE = 'A'  
			AND TERM = @PROCESS_TERM
			AND ISNULL(IS_ACTIVE,'Y') = 'Y'
			AND AGENCY_ID = @APP_AGENCY_ID
	ORDER BY STATE_ID DESC, LOB_ID DESC, SUB_LOB_ID DESC, CLASS_RISK DESC 
 
	IF(convert(INT,@AGENCY_ADDI_COMM_PRCT) <> 0)
	BEGIN
		SELECT @AGENCY_ADDI_COMM = ROUND(((@PREMIUM_AMOUNT * @AGENCY_ADDI_COMM_PRCT)/100), 2)
	END	

	--Fetch Insured Name & Agency Name	
	SELECT @CUSTOMER_NAME = ISNULL(CUSTOMER_CODE,'') + ' - ' + ISNULL(CUSTOMER_FIRST_NAME,'') 
		+ ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') +
		CASE WHEN ISNULL(CUSTOMER_MIDDLE_NAME,'') = '' THEN '' ELSE ' ' END
		+ 
		ISNULL(CUSTOMER_LAST_NAME ,'') 
	FROM CLT_CUSTOMER_LIST WITH(NOLOCK)
	WHERE CUSTOMER_ID = @CUSTOMER_ID 

	SELECT @AGENCY_NAME = ISNULL(AGENCY_CODE,'') + ' - ' + ISNULL(AGENCY_DISPLAY_NAME,'') 
	FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE AGENCY_ID = @APP_AGENCY_ID

	
	--Select  @AGENCY_REG_COMM_PRCT as comm,  @STATE_ID as state,@APP_LOB as lob,@APP_SUBLOB as sublob ,@PROCESS_TERM as term ,@POLICY_EFFECTIVE_DATE as E_Date
        --POST DIARY ENTRY 
	--RETURN ERROR CODE 
	--USE THIS ERROR CODE TO ROLLBACK POLICY PROCESS AND DISPLAYING ERROR MESSAGE 	 */


	IF(convert(INT,@AGENCY_REG_COMM_PRCT) =0)
	BEGIN
		set @RetVal =-2
		RETURN -2
	END

	IF @@ERROR <> 0
		GOTO ERRHANDLER
		
	SELECT @AGENCY_REG_COMM = ROUND(((@PREMIUM_AMOUNT * @AGENCY_REG_COMM_PRCT)/100), 2)
 

	IF @@ERROR <> 0
		GOTO ERRHANDLER	

	-- Commented By Ravindra(06-04-2007) Condition will be checked before premium posting 
	-- for Premium Amount of whole policy	
-- 	IF @PREMIUM_AMOUNT < @MIN_INSTALLMENT_AMOUNT	
-- 	BEGIN
-- 		--Peemium amount is less then min installment amount, hence changing installment plan to Full Pay Plan
-- 		SELECT @INSTALLMENT = IDEN_PLAN_ID 
-- 		FROM ACT_INSTALL_PLAN_DETAIL
-- 		WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0) = 1
-- 		
-- 		--Updating the installment field of applist table also
-- 		UPDATE POL_CUSTOMER_POLICY_LIST
-- 		SET INSTALL_PLAN_ID = @INSTALLMENT
-- 		WHERE POLICY_ID = @POLICY_ID 
-- 			AND POLICY_VERSION_ID = @POLICY_VERSION_ID
-- 			AND CUSTOMER_ID = @CUSTOMER_ID
-- 
-- 		IF @@ERROR <> 0
-- 			GOTO ERRHANDLER
-- 
-- 		--Updating the transaction description
-- 		SET @TRANS_DESC = 'Application billing plan has been updated to default plan.'
-- 	END

	--Fetching the 	installment fees
	SELECT @INSTALLMENT_FEES = ISNULL(INSTALLMENT_FEES, 0)
	FROM ACT_INSTALL_PLAN_DETAIL	WITH(NOLOCK)
	WHERE IDEN_PLAN_ID = @INSTALLMENT

	IF @@ERROR <> 0
		GOTO ERRHANDLER
		
	--Retreiving whether prebill or not
	SELECT @PREBILL_DATE = GETDATE(), @GL_ID = 1, @IS_PREBILL = 0, @FISCAL_ID = 1
	
	IF ( DATEDIFF(DD,@PREBILL_DATE , @POLICY_EFFECTIVE_DATE  ) > 0) 
	BEGIN
		SELECT @IS_PREBILL = 1
	END
	
--RAvindra(09-09-09):Optimisation	
--	SELECT  @HOME_EMP = CASE ISNULL(IS_HOME_EMP, 0) WHEN 1 THEN 'Y' ELSE 'N' END
--		FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
--                WHERE POLICY_ID = @POLICY_ID 	AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND CUSTOMER_ID = @CUSTOMER_ID

	IF @@ERROR <> 0
		GOTO ERRHANDLER

	IF @IS_PREBILL = 0 	-- No prebill
	BEGIN
		SELECT 
			@FULLPREMIUM_ACCOUNT = CASE @BILL_TYPE 
				WHEN 'AB' THEN AST_UNCOLL_PRM_AGENCY 
  		  		ELSE AST_UNCOLL_PRM_CUSTOMER 
				  END,
-- Commented By Ravindra (12-01-2006) Account no longer valid
--			@MCCA_FEES_ACCOUNT   	= INC_PRM_WRTN_MCCA,
			@OTHER_FEES_ACCOUNT  	= INC_PRM_WRTN_OTH_STATE_ASSESS_FEE,
			@PREMIUM_INCOME_ACCOUNT = INC_PRM_WRTN,
			@COMM_EXPENSE_ACCOUNT  	= EXP_COMM_INCURRED ,
			@COMM_PAYABLE_ACCOUNT 	= CASE @BILL_TYPE 
				WHEN 'AB' THEN LIB_COMM_PAYB_AGENCY_BILL 
				ELSE LIB_COMM_PAYB_DIRECT_BILL 
				  END
		FROM ACT_GENERAL_LEDGER WITH(NOLOCK)
		WHERE FISCAL_ID = @FISCAL_ID 

		IF @@ERROR <> 0
			GOTO ERRHANDLER		

		--Updating the transaction description
		SET @TRANS_DESC = @TRANS_DESC + CHAR(13) + 'Premium has been posted in relevant accounts.'
		
	END	
	ELSE
	BEGIN 
		SELECT 	
			@FULLPREMIUM_ACCOUNT = CASE @BILL_TYPE 
				WHEN 'AB' THEN AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL 
				ELSE AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL 
				END,
--Comented By Ravindra(12-01-2006) Account No Longer Used
-- 			@MCCA_FEES_ACCOUNT = CASE @BILL_TYPE 
-- 				WHEN 'AB' THEN AST_MCCA_FEE_SUSPENSE_AGENCY_BILL 
-- 	 			ELSE AST_MCCA_FEE_SUSPENSE_DIRECT_BILL 
-- 				END,
			@OTHER_FEES_ACCOUNT = CASE @BILL_TYPE 
				WHEN 'AB' THEN AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL 
				ELSE AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL 
				END,
			@PREMIUM_INCOME_ACCOUNT = CASE @BILL_TYPE 
				WHEN 'AB' THEN AST_PRM_WRIT_SUSPENSE_AGENCY_BILL 
				ELSE AST_PRM_WRIT_SUSPENSE_DIRECT_BILL 
				END, 
			@COMM_EXPENSE_ACCOUNT = CASE @BILL_TYPE 
				WHEN 'AB' THEN AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL 
				ELSE AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL 
				END,
			@COMM_PAYABLE_ACCOUNT = CASE @BILL_TYPE 
				WHEN 'AB' THEN AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL 
				ELSE AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL 
				END
		FROM ACT_GENERAL_LEDGER WITH(NOLOCK)
		WHERE FISCAL_ID = @FISCAL_ID 
		
		--Updating the transaction description
		SET @TRANS_DESC = @TRANS_DESC + CHAR(13) + 'Premium has been posted in suspense accounts.'

		IF @@ERROR <> 0
			GOTO ERRHANDLER
		
	END


	IF @HOME_EMP = 'Y'
	BEGIN 
		SELECT @COMM_PAYABLE_ACCOUNT = @FULLPREMIUM_ACCOUNT
	END

	SET @TRAN_TYPE = 'PREM'
	SET @TRAN_TYPE_CODE = 'NBSP'
	SET @COMMISSION_TYPE_CODE = 'NBSC'

	-- Chk here for the process type
	DECLARE @PROCESS_ID INT -- 5 for RENEWAL
	DECLARE @RENEWAL_PROCESS  Int , @NBS_PROCESS Int
	SET @NBS_PROCESS		= 24
	SET @RENEWAL_PROCESS  	= 5			 

	SELECT @PROCESS_ID = PROCESS_ID FROM POL_POLICY_PROCESS  WITH(NOLOCK)
	WHERE POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND CUSTOMER_ID = @CUSTOMER_ID


	IF(@PROCESS_ID = @RENEWAL_PROCESS )-- renewal 
	BEGIN
		--Set Transaction Code For Renewal
		SET @TRAN_TYPE_CODE = 'RENP'
		SET @COMMISSION_TYPE_CODE = 'RENC'
	END

	--Finding whether bill type is agency bill or direct bill
	IF UPPER(@BILL_TYPE) = 'AB'			--FOR AGENCY BILL
	BEGIN
		
		--For agency bill, entering the record in act_agency_open_item table
		INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID, SOURCE_EFFECTIVE_DATE, 
				TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,TRAN_TYPE, TRAN_CODE,
				TRAN_ENTITY,TRAN_DESC,COMMISSION_PERCENTAGE)	
		VALUES ( @FULLPREMIUM_ACCOUNT, @TOTAL_PREMIUM_AMOUNT , @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @APP_AGENCY_ID, @APP_EFFECTIVE_DATE,
				 'Premium due.', @POLICY_ID, @POLICY_VERSION_ID, 1, 1,@TRAN_TYPE,@TRAN_TYPE_CODE,
				@CUSTOMER_NAME,@TRAN_DESC,@AGENCY_REG_COMM_PRCT)

		IF @@ERROR <> 0
				GOTO ERRHANDLER

	END
	ELSE IF (UPPER(@BILL_TYPE) = 'DB')
	BEGIN
		
		--For direct bill, entering the record in customer open item table

		--If policy is not under any installment plan, (means on ful pay plan)
		-- then entering the consolidated entry in customer
		--open items table
 

		IF ISNULL(@INSTALLMENT,0) = @FULL_PAY_PLAN_ID 
		BEGIN

			INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE
				, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_TYPE,TRAN_CODE,
				TRAN_ENTITY,TRAN_DESC,
				DUE_DATE,
				COMMISSION_PERCENTAGE)	
			VALUES (@FULLPREMIUM_ACCOUNT, @TOTAL_PREMIUM_AMOUNT , @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @APP_EFFECTIVE_DATE, 
				'Premium due.', @POLICY_ID, @POLICY_VERSION_ID, 1, 1, @TRAN_TYPE,@TRAN_TYPE_CODE,
				@CUSTOMER_NAME,@TRAN_DESC,
				CASE WHEN CAST(CONVERT(VARCHAR,@APP_EFFECTIVE_DATE,101) AS DATETIME) < 
					CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME) 
					THEN CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME)
				ELSE 	@APP_EFFECTIVE_DATE END,
				@AGENCY_REG_COMM_PRCT)

			
			IF @@ERROR <> 0
				GOTO ERRHANDLER
		END
		ELSE
		BEGIN
			--Policy is  under any installment plan, 
			-- if the  installment plan is deactivated and process is renewal then diary entry shld go 
			-- Ravindra(03-14-2007)this rule will be applicable to all processes	
			
			--Retreiving the details of installments from database
				-- chk here for activte/deactivate of installment plan
			IF NOT EXISTS( SELECT IDEN_PLAN_ID FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK)
				WHERE IDEN_PLAN_ID = @INSTALLMENT AND isnull(IS_ACTIVE,'N')='Y')
			BEGIN
				-- if installment plan is deactivated then diary entry will go 
				set @RetVal =-3
				RETURN -3  					

			END

			DECLARE @INSTALLMENT_ROW_ID INT
			DECLARE @INSTALLMENT_ROW_ID_DIS INT

			--Getting the details of installments
			DECLARE @IDEN_PLAN_ID SMALLINT, @PLAN_DESCRIPTION NVARCHAR(70),
				@PLAN_TYPE NCHAR(2), @NO_OF_PAYMENTS SMALLINT, @MONTHS_BETWEEN SMALLINT, 
				@PERCENT_BREAKDOWN1 DECIMAL(10,4), @PERCENT_BREAKDOWN2 DECIMAL(10,4), 
				@PERCENT_BREAKDOWN3 DECIMAL(10,4), @PERCENT_BREAKDOWN4 DECIMAL(10,4),
				@PERCENT_BREAKDOWN5 DECIMAL(10,4), @PERCENT_BREAKDOWN6 DECIMAL(10,4),
				@PERCENT_BREAKDOWN7 DECIMAL(10,4), @PERCENT_BREAKDOWN8 DECIMAL(10,4),
				@PERCENT_BREAKDOWN9 DECIMAL(10,4), @PERCENT_BREAKDOWN10 DECIMAL(10,4), 
				@PERCENT_BREAKDOWN11 DECIMAL(10,4), @PERCENT_BREAKDOWN12 DECIMAL(10,4),
				@PLAN_PAYMENT_MODE Int,@NO_INS_DOWNPAY INt,
				@DUE_DATE  Datetime


			IF(@PROCESS_ID = @RENEWAL_PROCESS )-- renewal 
			BEGIN
	
				SELECT 
					@IDEN_PLAN_ID = IDEN_PLAN_ID, @PLAN_DESCRIPTION = PLAN_DESCRIPTION,
					@PLAN_TYPE = PLAN_TYPE, @NO_OF_PAYMENTS = NO_OF_PAYMENTS, @MONTHS_BETWEEN = MONTHS_BETWEEN, 
					@PERCENT_BREAKDOWN1 = PERCENT_BREAKDOWNRP1, @PERCENT_BREAKDOWN2 = PERCENT_BREAKDOWNRP2, 
					@PERCENT_BREAKDOWN3 = PERCENT_BREAKDOWNRP3, @PERCENT_BREAKDOWN4 = PERCENT_BREAKDOWNRP4,
					@PERCENT_BREAKDOWN5 = PERCENT_BREAKDOWNRP5, @PERCENT_BREAKDOWN6 = PERCENT_BREAKDOWNRP6,
					@PERCENT_BREAKDOWN7 = PERCENT_BREAKDOWNRP7, @PERCENT_BREAKDOWN8 = PERCENT_BREAKDOWNRP8, 
					@PERCENT_BREAKDOWN9 = PERCENT_BREAKDOWNRP9, @PERCENT_BREAKDOWN10 = PERCENT_BREAKDOWNRP10, 
					@PERCENT_BREAKDOWN11 = PERCENT_BREAKDOWNRP11, @PERCENT_BREAKDOWN12 = PERCENT_BREAKDOWNRP12,
					@PLAN_PAYMENT_MODE  = PLAN_PAYMENT_MODE,
					@NO_INS_DOWNPAY   = NO_INS_DOWNPAY_RENEW
				FROM ACT_INSTALL_PLAN_DETAIL	WITH(NOLOCK)
				WHERE IDEN_PLAN_ID = @INSTALLMENT

				IF @@ERROR <> 0
					GOTO ERRHANDLER

			END
			ELSE
			BEGIN 
				
				SELECT 
					@IDEN_PLAN_ID = IDEN_PLAN_ID, @PLAN_DESCRIPTION = PLAN_DESCRIPTION,
					@PLAN_TYPE = PLAN_TYPE, @NO_OF_PAYMENTS = NO_OF_PAYMENTS, @MONTHS_BETWEEN = MONTHS_BETWEEN, 
					@PERCENT_BREAKDOWN1 = PERCENT_BREAKDOWN1, @PERCENT_BREAKDOWN2 = PERCENT_BREAKDOWN2, 
					@PERCENT_BREAKDOWN3 = PERCENT_BREAKDOWN3, @PERCENT_BREAKDOWN4 = PERCENT_BREAKDOWN4,
					@PERCENT_BREAKDOWN5 = PERCENT_BREAKDOWN5, @PERCENT_BREAKDOWN6 = PERCENT_BREAKDOWN6,
					@PERCENT_BREAKDOWN7 = PERCENT_BREAKDOWN7, @PERCENT_BREAKDOWN8 = PERCENT_BREAKDOWN8, 
					@PERCENT_BREAKDOWN9 = PERCENT_BREAKDOWN9, @PERCENT_BREAKDOWN10 = PERCENT_BREAKDOWN10, 
					@PERCENT_BREAKDOWN11 = PERCENT_BREAKDOWN11, @PERCENT_BREAKDOWN12 = PERCENT_BREAKDOWN12,
					@PLAN_PAYMENT_MODE  = PLAN_PAYMENT_MODE,
					@NO_INS_DOWNPAY   = NO_INS_DOWNPAY
				FROM ACT_INSTALL_PLAN_DETAIL	WITH(NOLOCK)
				WHERE IDEN_PLAN_ID = @INSTALLMENT
	
				IF @@ERROR <> 0
					GOTO ERRHANDLER
				
				
			END


			--Entering the details of plan in ACT_POLICY_INSTALL_PLAN_DATA
			--Condition Added By Ravindra(09-14-2006)
			-- If SP is called fro same policy & different risk then installment plan details 
			-- have already added in first call
			IF NOT EXISTS (SELECT POLICY_ID FROM ACT_POLICY_INSTALL_PLAN_DATA	WITH(NOLOCK)
					WHERE POLICY_ID = @POLICY_ID
					AND POLICY_VERSION_ID = @POLICY_VERSION_ID
					AND CUSTOMER_ID	= @CUSTOMER_ID
					AND PLAN_ID	= @IDEN_PLAN_ID
					)
			INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA
			(
				APP_ID, APP_VERSION_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PLAN_ID, PLAN_DESCRIPTION, 
				PLAN_TYPE, NO_OF_PAYMENTS, MONTHS_BETWEEN, 
				PERCENT_BREAKDOWN1, PERCENT_BREAKDOWN2, PERCENT_BREAKDOWN3, PERCENT_BREAKDOWN4, PERCENT_BREAKDOWN5,
				PERCENT_BREAKDOWN6, PERCENT_BREAKDOWN7, PERCENT_BREAKDOWN8, PERCENT_BREAKDOWN9, PERCENT_BREAKDOWN10, 
				PERCENT_BREAKDOWN11, PERCENT_BREAKDOWN12,
				MODE_OF_DOWN_PAYMENT,INSTALLMENTS_IN_DOWN_PAYMENT,MODE_OF_PAYMENT,CURRENT_TERM

			)
			VALUES
			(
				@APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @IDEN_PLAN_ID, @PLAN_DESCRIPTION,
				@PLAN_TYPE, @NO_OF_PAYMENTS, @MONTHS_BETWEEN, 
				@PERCENT_BREAKDOWN1, @PERCENT_BREAKDOWN2, @PERCENT_BREAKDOWN3, @PERCENT_BREAKDOWN4, @PERCENT_BREAKDOWN5,
				@PERCENT_BREAKDOWN6, @PERCENT_BREAKDOWN7, @PERCENT_BREAKDOWN8, @PERCENT_BREAKDOWN9, @PERCENT_BREAKDOWN10, 
				@PERCENT_BREAKDOWN11, @PERCENT_BREAKDOWN12,
				@SELECTED_DOWN_PAYMENT_MODE,@NO_INS_DOWNPAY,@PLAN_PAYMENT_MODE,@CURRENT_TERM
			)
			
			IF @@ERROR <> 0
				GOTO ERRHANDLER
		

			--Inserting the record in to ACT_POLICY_INSTALLMENT_DETAILS
			DECLARE @INSTALLMENT_EFFECTIVE_DATE DATETIME, @INSTALLMENT_AMOUNT DECIMAL(20,2)
			DECLARE @CTR SMALLINT
			DECLARE @IS_DOWN_PAYMENT SmallINt,
					@MODE_OF_PAYMENT Int,
					@PERCENTAG_OF_PREMIUM  Decimal(9,4),
					@EFT_COUNT int
				
		
			SET @CTR = 1;
			SET @IS_DOWN_PAYMENT   = 1
			SET @EFT_COUNT = 1
						
			DECLARE @DISCOUNT_APPLICABLE SmallInt
			DECLARE @DISCOUNT_AMOUNT DECIMAL(20,8)
			DECLARE @TOTAL_INSTALLMENT_AMT DECIMAL(20,8)
			DECLARE @TOTAL_DISCOUNT_AMT DECIMAL(20,8)
			DECLARE @DIFFERENCE DECIMAL(20,8),
					@CALCULATED_INS_EFF_DATE Datetime,
					@DAY_LIMIT INT --Itrack 6320 

			SET @INSTALLMENT_EFFECTIVE_DATE = @POLICY_EFFECTIVE_DATE
			SET @CALCULATED_INS_EFF_DATE = @POLICY_EFFECTIVE_DATE
			
			SET @DISCOUNT_APPLICABLE  = 0
			SET @TOTAL_INSTALLMENT_AMT = 0
			SET @TOTAL_DISCOUNT_AMT  = 0

			IF ISNULL(@AGENCY_REG_COMM,0) <> 0 
			BEGIN	
				IF @HOME_EMP = 'Y'
				BEGIN 
					SET @DISCOUNT_APPLICABLE = 1	
				END
			END
			WHILE @CTR <= @NO_OF_PAYMENTS		--Looping as number of times as there are installments
			BEGIN
				
				IF @CTR > 1		--Calculating the istallment effective date
					SET @CALCULATED_INS_EFF_DATE = DATEADD(MONTH, @MONTHS_BETWEEN, @CALCULATED_INS_EFF_DATE )
					--SELECT @INSTALLMENT_EFFECTIVE_DATE = DATEADD(MONTH, @MONTHS_BETWEEN, @INSTALLMENT_EFFECTIVE_DATE )

				IF @IS_DOWN_PAYMENT  > @NO_INS_DOWNPAY
				BEGIN 
					SET @MODE_OF_PAYMENT =   @PLAN_PAYMENT_MODE
				END
				ELSE
				BEGIN 
					SET @MODE_OF_PAYMENT =   @SELECTED_DOWN_PAYMENT_MODE
				END 
				
				-- Ravindra(06-11-2007): If Installment is Part of DownPayment will be billed with 
				-- first installment i.e. due date will be same as policy effective date
				IF @IS_DOWN_PAYMENT  <= @NO_INS_DOWNPAY
				BEGIN 
					SET @INSTALLMENT_EFFECTIVE_DATE = @POLICY_EFFECTIVE_DATE
				END
				ELSE
				BEGIN 
					SET @INSTALLMENT_EFFECTIVE_DATE = @CALCULATED_INS_EFF_DATE
				END

				-- In Case of EFT mode of payment
				-- if EFT Tentative date is with in 5 days of actual due date then due date will
				-- EFT Tentetive day will be date in same month
				-- else transaction eill be EFT one month prior
				IF(@MODE_OF_PAYMENT = @EFT AND @EFT_TENTATIVE_DATE <> 0)
				BEGIN 
					DECLARE @DD SmallInt,
							@MM SmallInt,
							@YYYY SmallInt


					IF( @EFT_TENTATIVE_DATE < DATEPART(DD,@INSTALLMENT_EFFECTIVE_DATE)  )  
					BEGIN   
						SET @DD = @EFT_TENTATIVE_DATE  
						SET @MM = DATEPART(MM,@INSTALLMENT_EFFECTIVE_DATE)+ 1  
						SET @YYYY = DATEPART(YYYY,@INSTALLMENT_EFFECTIVE_DATE)  
					END  
					ELSE 
					BEGIN   
						SET @DD = @EFT_TENTATIVE_DATE  
						SET @MM = DATEPART(MM,@INSTALLMENT_EFFECTIVE_DATE)  
						SET @YYYY = DATEPART(YYYY,@INSTALLMENT_EFFECTIVE_DATE)  
					END  

					
					-- If month is Feb then date will be updated to 28 if it is 29
					IF(@MM = 2 AND @DD >=29)
					BEGIN 
						SET @DD = 28
					END
					-- by Pravesh on 17 aug 2007 if @MM=0 then
					IF (@MM = 0) 
					BEGIN 
						SET @MM = 12
						SET @YYYY = @YYYY-1
					END
				    --end here 
					-- If month is of 30 days and @DD=31 then date will be updated to 30 if it is 31 --by pravesh on 6 aug 2007
					IF((@MM = 4 or @MM = 6 or @MM = 9 or @MM = 11) and @DD =31)
					BEGIN 
						SET @DD = 30
					END
		
					--Ravindra(10-14-2009): If Installment FAlling in next year 
					 --If month is 13 or greater than 
					IF(@MM >=13)	
					BEGIN
						SET @MM = 1
						SET @YYYY = @YYYY+1	
					END	
				
					DECLARE @TEMP_DUE_DATE Datetime 
					
					SET @TEMP_DUE_DATE = CAST(
					( 
					CONVERT(VARCHAR,@YYYY)
					+ '-'
					+ CONVERT(VARCHAR,@MM) 
					+ '-'
					+ CONVERT(VARCHAR, @DD) 
					)
					AS DATETIME)


					IF(@EFT_COUNT = 1)
						SET @DAY_LIMIT = ABS(DATEDIFF(DD, @TEMP_DUE_DATE,@INSTALLMENT_EFFECTIVE_DATE))
					--Ravindra(07-07-09): iTrack 6061, Change the 5 day limit to 15 days
					--IF(@EFT_TENTATIVE_DATE - DATEPART(DD,@INSTALLMENT_EFFECTIVE_DATE)  > 15) -- 5 ) --Commented Itrack 6320
						--AND DATEPART(DD,@INSTALLMENT_EFFECTIVE_DATE)  - @EFT_TENTATIVE_DATE < 0
					IF(@DAY_LIMIT > 15)
					BEGIN 
						SET @DUE_DATE = DATEADD(MM,-1,@TEMP_DUE_DATE)
					END
					ELSE
					BEGIN 
						SET @DUE_DATE = @TEMP_DUE_DATE
					END



					--Itrack 6320
					SET @EFT_COUNT = @EFT_COUNT + 1

				END
				ELSE
				BEGIN
					SET @DUE_DATE = @INSTALLMENT_EFFECTIVE_DATE
					----------Added on 30 Oct 2009 (Praveen) --Itrack 6675 -Check Payment Plans
					SET @DD = DATEPART(DD,@POLICY_EFFECTIVE_DATE)
					SET @MM = DATEPART(MM,@INSTALLMENT_EFFECTIVE_DATE)
					SET @YYYY = DATEPART(YYYY,@INSTALLMENT_EFFECTIVE_DATE)

					-- If month is Feb then date will be updated to 28 if it is 29
					IF(@MM = 2 AND @DD >=29)
					BEGIN 
						SET @DD = 28
					END

					-- by Pravesh on 17 aug 2007 if @MM=0 then
					IF (@MM = 0) 
					BEGIN 
						SET @MM = 12
						SET @YYYY = @YYYY-1
					END
				    --end here 
					-- If month is of 30 days and @DD=31 then date will be updated to 30 if it is 31 --by pravesh on 6 aug 2007
					IF((@MM = 4 or @MM = 6 or @MM = 9 or @MM = 11) and @DD =31)
					BEGIN 
						SET @DD = 30
					END
		
					--Ravindra(10-14-2009): If Installment FAlling in next year 
					 --If month is 13 or greater than 
					IF(@MM >=13)	
					BEGIN
						SET @MM = 1
						SET @YYYY = @YYYY+1	
					END	
				
					SET @DUE_DATE = CAST(
					( 
					CONVERT(VARCHAR,@YYYY)
					+ '-'
					+ CONVERT(VARCHAR,@MM) 
					+ '-'
					+ CONVERT(VARCHAR, @DD) 
					)
					AS DATETIME)

					------End Itrack 6675
				END 

				IF( @DUE_DATE < CAST(CONVERT(VARCHAR,@POLICY_EFFECTIVE_DATE,101) AS DATETIME))
				BEGIN 
					SET @DUE_DATE = @POLICY_EFFECTIVE_DATE 
				END					

	
				SELECT @PERCENTAG_OF_PREMIUM =
					CASE @CTR
						WHEN 1 THEN
							@PERCENT_BREAKDOWN1
						WHEN 2 THEN
							@PERCENT_BREAKDOWN2					
						WHEN 3 THEN
							 @PERCENT_BREAKDOWN3
						WHEN 4 THEN
							 @PERCENT_BREAKDOWN4
						WHEN 5 THEN
							 @PERCENT_BREAKDOWN5
						WHEN 6 THEN
							 @PERCENT_BREAKDOWN6
						WHEN 7 THEN
							@PERCENT_BREAKDOWN7
						WHEN 8 THEN
							@PERCENT_BREAKDOWN8
						WHEN 9 THEN
							@PERCENT_BREAKDOWN9
						WHEN 10 THEN
							@PERCENT_BREAKDOWN10
						WHEN 11 THEN
							@PERCENT_BREAKDOWN11
						WHEN 12 THEN
							@PERCENT_BREAKDOWN12
					END
	
				SET @INSTALLMENT_AMOUNT = ROUND((@PERCENTAG_OF_PREMIUM/100) * @TOTAL_PREMIUM_AMOUNT, 2)

				IF @@ERROR <> 0
					GOTO ERRHANDLER
				
				-- Inserting Installment Amount In Installment Detail Table			
				INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS
				(
					APP_ID, APP_VERSION_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, INSTALLMENT_AMOUNT,
					INSTALLMENT_EFFECTIVE_DATE, RELEASED_STATUS,INSTALLMENT_NO,RISK_ID,RISK_TYPE,
					PAYMENT_MODE,CURRENT_TERM , PERCENTAG_OF_PREMIUM
				)
				VALUES
				(	
					@APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @INSTALLMENT_AMOUNT,
					@DUE_DATE, 'N',@CTR,@RISK_ID,@RISK_TYPE,
					@MODE_OF_PAYMENT,@CURRENT_TERM , @PERCENTAG_OF_PREMIUM
				)

				
				IF @@ERROR <> 0
					GOTO ERRHANDLER
				SELECT @INSTALLMENT_ROW_ID = @@IDENTITY

				IF @@ERROR <> 0
					GOTO ERRHANDLER
				
				--Inserting the installment fees 
				-- Installment Fees not to be charged from Home Employee
				-- Check if the SP is called for same policy & different risk ,if so then installment 
				-- fees already charged 
				IF(( @IS_SAME_POLICY <> 'Y')
				 AND ( @IS_DOWN_PAYMENT  > @NO_INS_DOWNPAY OR @CTR = 1)) 
				-- Ravindra(06-11-2007): Fees will be charged for only one installments for all clubbed 
				-- installment which are part of down payment.
				BEGIN 
					IF(@HOME_EMP <> 'Y')
					BEGIN 
						INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, 
						SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, TRAN_TYPE, 
						TRAN_CODE, ACCOUNTS, OPEN_ITEMS, INSTALLMENT_ROW_ID,TRAN_ENTITY,TRAN_DESC,
						DUE_DATE)
						VALUES(@FULLPREMIUM_ACCOUNT, @INSTALLMENT_FEES, @APP_ID, @APP_VERSION_ID,@CUSTOMER_ID,
						 @DUE_DATE , 'Fees due.', @POLICY_ID, @POLICY_VERSION_ID, @TRAN_TYPE_INSTALLMENT,
						 @TRAN_CODE_INSTALLMENT, 0, 1, @INSTALLMENT_ROW_ID,@CUSTOMER_NAME,@TRAN_DESC,
						CASE WHEN CAST(CONVERT(VARCHAR,@DUE_DATE,101) AS DATETIME) < 
							CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME) 
							THEN CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME)
						ELSE @DUE_DATE END
						)
						SET @TOTAL_SERVICE_CHARGE = @TOTAL_SERVICE_CHARGE + @INSTALLMENT_FEES
					END
				END
	
				--If home Employee & Discount Is Applicable Break discount in installments		
				IF (@DISCOUNT_APPLICABLE = 1)
				--Start Breaking Discount in to Installments
				BEGIN 
					SET @DISCOUNT_AMOUNT = ROUND((@PERCENTAG_OF_PREMIUM /100) * @AGENCY_REG_COMM, 2)
					
		
					IF @@ERROR <> 0
						GOTO ERRHANDLER
				
					-- Inserting Discount Detail In Installment Detail Table
					INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS
					(
					APP_ID, APP_VERSION_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, INSTALLMENT_AMOUNT,
					INSTALLMENT_EFFECTIVE_DATE, RELEASED_STATUS,INSTALLMENT_NO,RISK_ID,RISK_TYPE,
					PAYMENT_MODE,CURRENT_TERM , PERCENTAG_OF_PREMIUM
					)
					VALUES
					(	
					@APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, -@DISCOUNT_AMOUNT,
					@DUE_DATE, 'N',@CTR,@RISK_ID,@RISK_TYPE,
					@MODE_OF_PAYMENT,@CURRENT_TERM , @PERCENTAG_OF_PREMIUM
					)

				
					IF @@ERROR <> 0
						GOTO ERRHANDLER
					SELECT @INSTALLMENT_ROW_ID_DIS = @@IDENTITY
	
					IF @@ERROR <> 0
						GOTO ERRHANDLER
					
					--Now Inserting Discount into open item table, for this inserting into temp_post table
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, 
						SOURCE_EFFECTIVE_DATE,TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS,
						OPEN_ITEMS,INSTALLMENT_ROW_ID,TRAN_TYPE, TRAN_CODE,TRAN_ENTITY,TRAN_DESC,
						DUE_DATE , COMMISSION_PERCENTAGE)
					VALUES(@FULLPREMIUM_ACCOUNT, -@DISCOUNT_AMOUNT ,@APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, 
						@DUE_DATE,'Discount For ' + Convert(Varchar(2),@CTR) + ' Installment.', @POLICY_ID, @POLICY_VERSION_ID, 1, 
						1,@INSTALLMENT_ROW_ID_DIS,@TRAN_TYPE,'DISC',@CUSTOMER_NAME,@TRAN_DESC,
						CASE WHEN CAST(CONVERT(VARCHAR,@DUE_DATE,101) AS DATETIME) < 
							CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME) 
							THEN CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME)
						ELSE @DUE_DATE END,
						@AGENCY_REG_COMM_PRCT )
				
				END  -- End Breaking Discount
				SET @TOTAL_DISCOUNT_AMT = @TOTAL_DISCOUNT_AMT  + @DISCOUNT_AMOUNT

		
				--Now Inserting Installment Amount into open item table, for this inserting into temp_post table
				INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE,
					 TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, 
					INSTALLMENT_ROW_ID,TRAN_TYPE, TRAN_CODE,TRAN_ENTITY,TRAN_DESC,DUE_DATE, COMMISSION_PERCENTAGE)
				VALUES(@FULLPREMIUM_ACCOUNT, @INSTALLMENT_AMOUNT , @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @DUE_DATE,
				 Convert(Varchar(2),@CTR) + ' Installment due.', @POLICY_ID, @POLICY_VERSION_ID, 1, 1,
				 @INSTALLMENT_ROW_ID,@TRAN_TYPE,@TRAN_TYPE_CODE,@CUSTOMER_NAME,@TRAN_DESC,
				 CASE WHEN CAST(CONVERT(VARCHAR,@DUE_DATE,101) AS DATETIME) < 
							CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME) 
							THEN CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME)
						ELSE @DUE_DATE END,
				 @AGENCY_REG_COMM_PRCT)

				SET @TOTAL_INSTALLMENT_AMT = @TOTAL_INSTALLMENT_AMT + @INSTALLMENT_AMOUNT
				
				IF @@ERROR <> 0
					GOTO ERRHANDLER
				
				SELECT @CTR = @CTR + 1
				SET @IS_DOWN_PAYMENT = @IS_DOWN_PAYMENT + 1
			END			

			--@TOTAL_PREMIUM_AMOUNT
			-- If there is a difference in Total Premium Amount & Total of Installment Created 
			-- then add this difference to First Installment
			DECLARE @INS_ROW_ID Int

			IF(@TOTAL_DISCOUNT_AMT <> @AGENCY_REG_COMM )
			BEGIN


				SET @DIFFERENCE  = 0
				SET @DIFFERENCE = @AGENCY_REG_COMM - @TOTAL_DISCOUNT_AMT
			
				SET @INS_ROW_ID = 0
				SELECT @INS_ROW_ID =MAX(ROW_ID)  FROM ACT_POLICY_INSTALLMENT_DETAILS	WITH(NOLOCK)
				WHERE	    CUSTOMER_ID		= @CUSTOMER_ID 
				AND POLICY_ID		= @POLICY_ID
				AND POLICY_VERSION_ID	= @POLICY_VERSION_ID
				AND RISK_ID		= @RISK_ID
				AND RISK_TYPE		= @RISK_TYPE
				AND INSTALLMENT_NO 	= 1 	
				
				UPDATE ACT_POLICY_INSTALLMENT_DETAILS 
				SET INSTALLMENT_AMOUNT = INSTALLMENT_AMOUNT - @DIFFERENCE 
				WHERE	    CUSTOMER_ID		= @CUSTOMER_ID 
				AND POLICY_ID		= @POLICY_ID
				AND POLICY_VERSION_ID	= @POLICY_VERSION_ID
				AND RISK_ID		= @RISK_ID
				AND RISK_TYPE		= @RISK_TYPE
				AND INSTALLMENT_NO 	= 1 
				AND ROW_ID = @INS_ROW_ID

				UPDATE #TMP_POST SET AMT = AMT - @DIFFERENCE 
				WHERE INSTALLMENT_ROW_ID = @INS_ROW_ID 
				AND CUSTOMER_ID		= @CUSTOMER_ID 
				AND POLICY_ID		= @POLICY_ID
				AND POLICY_VERSION_ID	= @POLICY_VERSION_ID
				AND TRAN_TYPE <> @TRAN_TYPE_INSTALLMENT
			END
			IF (@TOTAL_INSTALLMENT_AMT  <> @TOTAL_PREMIUM_AMOUNT)
			BEGIN
				SET @DIFFERENCE  = 0
				SET @DIFFERENCE = @TOTAL_PREMIUM_AMOUNT - @TOTAL_INSTALLMENT_AMT

				SET @INS_ROW_ID = 0
				SELECT @INS_ROW_ID =MIN(ROW_ID) FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK)
				WHERE	    CUSTOMER_ID		= @CUSTOMER_ID 
				AND POLICY_ID		= @POLICY_ID
				AND POLICY_VERSION_ID	= @POLICY_VERSION_ID
				AND RISK_ID		= @RISK_ID
				AND RISK_TYPE		= @RISK_TYPE
				AND INSTALLMENT_NO 	= 1 				

				UPDATE ACT_POLICY_INSTALLMENT_DETAILS 
				SET INSTALLMENT_AMOUNT = INSTALLMENT_AMOUNT + @DIFFERENCE 
				WHERE	  CUSTOMER_ID		= @CUSTOMER_ID 
				AND POLICY_ID		= @POLICY_ID
				AND POLICY_VERSION_ID	= @POLICY_VERSION_ID
				AND RISK_ID		= @RISK_ID
				AND RISK_TYPE		= @RISK_TYPE
				AND INSTALLMENT_NO 	= 1 
				AND ROW_ID = @INS_ROW_ID
					
				UPDATE #TMP_POST SET AMT = AMT + @DIFFERENCE 
				WHERE INSTALLMENT_ROW_ID = @INS_ROW_ID 
				AND CUSTOMER_ID		= @CUSTOMER_ID 
				AND POLICY_ID		= @POLICY_ID
				AND POLICY_VERSION_ID	= @POLICY_VERSION_ID
				AND TRAN_TYPE <> @TRAN_TYPE_INSTALLMENT
			END
		END
		
		--Updating the transaction description
-- 		SET @TRANS_DESC = @TRANS_DESC + CHAR(13) + 'Total premium amount invoiced $' + CONVERT(VARCHAR,@TOTAL_PREMIUM_AMOUNT)
-- 		IF(@TOTAL_SERVICE_CHARGE <> 0)
-- 		BEGIN 
-- 			SET @TRANS_DESC = @TRANS_DESC + CHAR(13) + 'And the Total Installment fee $' + CONVERT(VARCHAR,@TOTAL_SERVICE_CHARGE)
-- 		END 
		SET @TOTAL_FEES = @TOTAL_SERVICE_CHARGE
	END

	
	--if transfer change process_id to 1000 so that commission applicable at NBS does not applies to policy
	IF(@IS_REWRITE_POLICY = 'Y') 
	BEGIN 
		SET @PROCESS_ID = 1000
	END	

	

	--If NBS --> PREM(Type) 	NBSP(Code)
	--If Renewal --> PREM(Type) 	RENP(Code)


	--Inserting the written premium in written premium account
	INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE,
		 TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_TYPE, TRAN_CODE
		,TRAN_ENTITY,TRAN_DESC)
	VALUES(@PREMIUM_INCOME_ACCOUNT, @PREMIUM_AMOUNT * -1, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, GETDATE(),
		 '', @POLICY_ID, @POLICY_VERSION_ID, 1, 0, @TRAN_TYPE ,@TRAN_TYPE_CODE,
		CASE UPPER(@BILL_TYPE) WHEN 'AB' Then @AGENCY_NAME
		ELSE 	@CUSTOMER_NAME END,
		@TRAN_DESC
		)--'NBSP')
	
	IF @@ERROR <> 0
		GOTO ERRHANDLER

	-- FEES(T)	MCCAF(Code)
	--Inserting the MCCA fees in MCCA account
	INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC,
		 POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,TRAN_TYPE, TRAN_CODE,TRAN_ENTITY,TRAN_DESC)
--Comented By Ravindra(12-01-2006) Account No Longer Used
--	VALUES(@MCCA_FEES_ACCOUNT, @MCCA_FEES * -1, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, GETDATE(), '', 
	VALUES(@OTHER_FEES_ACCOUNT, @MCCA_FEES * -1, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, GETDATE(), '', 
		@POLICY_ID, @POLICY_VERSION_ID, 1, 0,'FEES', 'MCCAF',
		CASE UPPER(@BILL_TYPE) WHEN 'AB' Then @AGENCY_NAME
		ELSE 	@CUSTOMER_NAME END, @TRAN_DESC
	)--'NBS')

	IF @@ERROR <> 0
		GOTO ERRHANDLER
	
	-- FEES(T)	OTSAF(Code)
	--Inserting Other state assesment fees in (Other State Assessment Fees/Premiums Written - Other state assessed fees) account
	INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC, 
		POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,TRAN_TYPE, TRAN_CODE,TRAN_ENTITY,TRAN_DESC)
	VALUES(@OTHER_FEES_ACCOUNT, @OTHER_FEES * -1, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, GETDATE(), '', 
		@POLICY_ID, @POLICY_VERSION_ID, 1, 0,'FEES', 'OTSAF',
		CASE UPPER(@BILL_TYPE) WHEN 'AB' Then @AGENCY_NAME
		ELSE 	@CUSTOMER_NAME END, @TRAN_DESC
	)--'NBS')


	IF @@ERROR <> 0
		GOTO ERRHANDLER

	IF ISNULL(@AGENCY_REG_COMM,0) <> 0
	BEGIN	/*If there is some commission amount, applying the commission as agency commission or employee discount*/
		IF @HOME_EMP = 'Y'
		BEGIN 
			
			SET @TRAN_TYPE_CODE = 'DISC'
			IF ISNULL(@INSTALLMENT,0) = @FULL_PAY_PLAN_ID 
			BEGIN
				--Giving discount to customer
				--Hence entering an open item in customer
				INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE, 
				TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_TYPE,TRAN_CODE,
				TRAN_ENTITY,TRAN_DESC,
				DUE_DATE  ,
				COMMISSION_PERCENTAGE)
				VALUES(@FULLPREMIUM_ACCOUNT, -@AGENCY_REG_COMM, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_EFFECTIVE_DATE, 
				'Discount given.', @POLICY_ID, @POLICY_VERSION_ID, 1, 1,@TRAN_TYPE,
				@TRAN_TYPE_CODE,@CUSTOMER_NAME,@TRAN_DESC, 
				CASE WHEN CAST(CONVERT(VARCHAR,@POLICY_EFFECTIVE_DATE,101) AS DATETIME) < 
							CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME) 
							THEN CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME)
				ELSE @POLICY_EFFECTIVE_DATE END	,
				@AGENCY_REG_COMM_PRCT)
			END 

			IF @@ERROR <> 0
				GOTO ERRHANDLER
			-- Contra Entry fo Discount 	
			-- Discount Given To Home Employee needs To Be Posted in to  Commission Expense Account
			INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID,SOURCE_EFFECTIVE_DATE,
			 TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,TRAN_TYPE,TRAN_CODE,TRAN_ENTITY,TRAN_DESC)
			VALUES ( @COMM_EXPENSE_ACCOUNT, @AGENCY_REG_COMM, @APP_ID, @APP_VERSION_ID
			, @CUSTOMER_ID, @POLICY_EFFECTIVE_DATE, '', @POLICY_ID, @POLICY_VERSION_ID, 1, 0,@TRAN_TYPE,@TRAN_TYPE_CODE,@CUSTOMER_NAME,@TRAN_DESC)

			IF @@ERROR <> 0
				GOTO ERRHANDLER

		END
		ELSE
		BEGIN

			--Giving agency commission to agency
			--Hence entering an open item in agency
			INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID,
			 SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, 
			TRAN_CODE, TRAN_TYPE ,TRAN_ENTITY,TRAN_DESC , COMMISSION_PERCENTAGE)
			VALUES ( @COMM_PAYABLE_ACCOUNT, -@AGENCY_REG_COMM, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @APP_AGENCY_ID, 
			@POLICY_EFFECTIVE_DATE, 'Commission due.', @POLICY_ID, @POLICY_VERSION_ID, 1, 1, 
			@COMMISSION_TYPE_CODE, 'COM',@AGENCY_NAME,@TRAN_DESC , @AGENCY_REG_COMM_PRCT)


			IF @@ERROR <> 0
				GOTO ERRHANDLER			
			
			--Contra entry for agency commission
			INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, 
			CUSTOMER_ID, AGENCY_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, 
			ACCOUNTS, OPEN_ITEMS,TRAN_CODE, TRAN_TYPE ,TRAN_ENTITY,TRAN_DESC)
			VALUES ( @COMM_EXPENSE_ACCOUNT, @AGENCY_REG_COMM, @APP_ID, @APP_VERSION_ID
			, @CUSTOMER_ID, NULL, @POLICY_EFFECTIVE_DATE, '', @POLICY_ID, @POLICY_VERSION_ID,
			 1, 0,@COMMISSION_TYPE_CODE, 'COM',@AGENCY_NAME,@TRAN_DESC)


			IF @@ERROR <> 0
				GOTO ERRHANDLER
	
			-- Giving any Additional Commission Applicable To the agency 
			IF(convert(INT,@AGENCY_ADDI_COMM_PRCT) <> 0 AND (@PROCESS_ID =@NBS_PROCESS OR @PROCESS_ID = @RENEWAL_PROCESS ) ) 
			BEGIN
				INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID,
				 SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, 
				TRAN_CODE, TRAN_TYPE ,TRAN_ENTITY,TRAN_DESC , COMMISSION_PERCENTAGE)
				VALUES ( @COMM_PAYABLE_ACCOUNT, -@AGENCY_ADDI_COMM, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, 
				@APP_AGENCY_ID, @INSTALLMENT_EFFECTIVE_DATE, 'Commission due.', @POLICY_ID, @POLICY_VERSION_ID, 
				1, 1, 'ADC', 'COM',@AGENCY_NAME,@TRAN_DESC , @AGENCY_ADDI_COMM_PRCT)
		
				IF @@ERROR <> 0
					GOTO ERRHANDLER			
			
				--Contra entry for agency additional commission
				INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,TRAN_CODE, TRAN_TYPE ,TRAN_ENTITY,TRAN_DESC)
				VALUES ( @COMM_EXPENSE_ACCOUNT, @AGENCY_ADDI_COMM, @APP_ID, @APP_VERSION_ID
				, @CUSTOMER_ID, NULL, GETDATE(), '', @POLICY_ID, @POLICY_VERSION_ID, 1, 0,'ADC', 'COM',@AGENCY_NAME,@TRAN_DESC)
		
				IF @@ERROR <> 0
					GOTO ERRHANDLER
		
			END	
		END
	END

	DECLARE @GRANT_COMMISSION SmallInt
	SET @GRANT_COMMISSION  = 0
	--If application is complete And Proceess is not Renewal

	IF (@CSR = -1 )
		SET @CSR = 0 
	IF @COMPLETE_APP = 'Y' AND @CSR <> 0 AND @PROCESS_ID = @NBS_PROCESS AND @IS_SAME_POLICY <> 'Y'		
	BEGIN
		
		SELECT @COMPLETE_APP_COMM_PRCT = 0
		
		SELECT @COMPLETE_APP_COMM_PRCT = ISNULL(COMMISSION_PERCENT, 0),
			@COMMISSION_AMOUNT_TYPE =  ISNULL(AMOUNT_TYPE,'P')
		FROM ACT_REG_COMM_SETUP WITH(NOLOCK)
		WHERE (STATE_ID = @STATE_ID OR STATE_ID = 0) AND
			(LOB_ID = @APP_LOB OR LOB_ID = 0) 
			AND (ISNULL(SUB_LOB_ID,0) = ISNULL(@APP_SUBLOB,0)  OR ISNULL(SUB_LOB_ID,0) = 0) 
			AND TERM = @PROCESS_TERM 
			AND COMMISSION_TYPE = 'C' AND
			@POLICY_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE
		
		IF @@ERROR <> 0
			GOTO ERRHANDLER	

		IF(@COMMISSION_AMOUNT_TYPE = 'P')
		BEGIN
			SELECT @COMPLETE_APP_COMM = ROUND(((@PREMIUM_AMOUNT * @COMPLETE_APP_COMM_PRCT)/100), 2)		
			SET @GRANT_COMMISSION = 1
		END
		ELSE IF(@COMMISSION_AMOUNT_TYPE = 'F')
		BEGIN
			SET @COMPLETE_APP_COMM = @COMPLETE_APP_COMM_PRCT
			IF @IS_SAME_POLICY <> 'Y'
				SET @GRANT_COMMISSION = 1
		END 

		IF @@ERROR <> 0
			GOTO ERRHANDLER
	
		-- If Commission type is Fixed Amount Commission will be Given per policy
		-- If Commission type is Percentage then Commission will be given for each risk(Dwelling)

		IF @GRANT_COMMISSION = 1
		BEGIN
			IF @HOME_EMP = 'Y'
			BEGIN
				-- System Parameter SYS_COMPLETE_APP_COMM_APPLICABILITY 2 means Complete App Commission 
				-- Is applicable to HOME Employee
				IF(@SYS_COMPLETE_APP_COMM_APPLICABILITY = 2)
				BEGIN
					--Giving discount to employee
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,
					TRAN_TYPE, TRAN_CODE,TRAN_ENTITY,TRAN_DESC,DUE_DATE , COMMISSION_PERCENTAGE)
					VALUES (@FULLPREMIUM_ACCOUNT, -@COMPLETE_APP_COMM, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_EFFECTIVE_DATE, 'Discount given for complete application.', @POLICY_ID, @POLICY_VERSION_ID, 1, 1,
					@TRAN_TYPE,@TRAN_TYPE_CODE,@CUSTOMER_NAME,@TRAN_DESC,@POLICY_EFFECTIVE_DATE ,
					CASE @COMMISSION_AMOUNT_TYPE WHEN 'P' THEN @COMPLETE_APP_COMM_PRCT ELSE NULL END)
					
					IF @@ERROR <> 0						GOTO ERRHANDLER

					-- Contra Entry fo Discount 	
					-- Discount Given To Home Employee needs To Be Posted in to  Commission Expense Account
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID,SOURCE_EFFECTIVE_DATE,
					 TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,TRAN_TYPE, TRAN_CODE,TRAN_ENTITY,TRAN_DESC)
					VALUES ( @COMM_EXPENSE_ACCOUNT, @COMPLETE_APP_COMM, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_EFFECTIVE_DATE,
					 '', @POLICY_ID, @POLICY_VERSION_ID, 1, 0,@TRAN_TYPE,@TRAN_TYPE_CODE,@CUSTOMER_NAME,@TRAN_DESC)
		
					IF @@ERROR <> 0
						GOTO ERRHANDLER
				END
			END
			ELSE
			BEGIN
				IF(@SYS_COMPLETE_APP_COMM_APPLICABILITY = 1)
				BEGIN
					--Giving commission to agency
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID, SOURCE_EFFECTIVE_DATE, 
					TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_CODE, 
					TRAN_TYPE ,TRAN_ENTITY,TRAN_DESC , COMMISSION_PERCENTAGE)
					VALUES (@COMM_PAYABLE_ACCOUNT, -@COMPLETE_APP_COMM, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @APP_AGENCY_ID, @POLICY_EFFECTIVE_DATE, 'Commission posted for complete application.', @POLICY_ID, 
						@POLICY_VERSION_ID, 1, 1, 'CAC', 'COM',@AGENCY_NAME,@TRAN_DESC,
						CASE @COMMISSION_AMOUNT_TYPE WHEN 'P' THEN @COMPLETE_APP_COMM_PRCT ELSE NULL END)
					
					IF @@ERROR <> 0
						GOTO ERRHANDLER
					--Contra Entry For Complete App Commission 
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_CODE, TRAN_TYPE ,TRAN_ENTITY,TRAN_DESC)
					VALUES ( @COMM_EXPENSE_ACCOUNT, @COMPLETE_APP_COMM, @APP_ID, @APP_VERSION_ID
					, @CUSTOMER_ID, NULL,@POLICY_EFFECTIVE_DATE, '', @POLICY_ID, @POLICY_VERSION_ID, 1, 0,'CAC', 'COM',@AGENCY_NAME,@TRAN_DESC)
					IF @@ERROR <> 0
						GOTO ERRHANDLER

				END
			END
		END
	END
	

	SET @COMMISSION_AMOUNT_TYPE = ''
	SET @GRANT_COMMISSION  = 0
	--Applying the property inspection credit 
	IF @PROPRTY_INSP_CREDIT = 'Y' AND @PROCESS_ID = @NBS_PROCESS 
	BEGIN
		
		SET @PROPERTY_INS_COMM_PRCT = 0
	   	SELECT @PROPERTY_INS_COMM_PRCT = ISNULL(COMMISSION_PERCENT, 0),
			@COMMISSION_AMOUNT_TYPE = ISNULL(AMOUNT_TYPE,'P') 
		FROM ACT_REG_COMM_SETUP WITH(NOLOCK)
		WHERE (STATE_ID = @STATE_ID OR STATE_ID = 0) AND
			(LOB_ID = @APP_LOB OR LOB_ID = 0) 
			AND (ISNULL(SUB_LOB_ID,0) = ISNULL(@APP_SUBLOB,0)  OR ISNULL(SUB_LOB_ID,0) = 0) 
			AND TERM = @PROCESS_TERM  
			AND COMMISSION_TYPE = 'P' 
			AND @POLICY_EFFECTIVE_DATE  BETWEEN EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE 

		IF @@ERROR <> 0
			GOTO ERRHANDLER

		IF(@COMMISSION_AMOUNT_TYPE = 'P')
		BEGIN
			SELECT @PROPERTY_INS_COMM = ROUND(((@PREMIUM_AMOUNT * @PROPERTY_INS_COMM_PRCT)/100), 2)
			SET @GRANT_COMMISSION  = 1
		END
		ELSE IF(@COMMISSION_AMOUNT_TYPE = 'F')
		BEGIN
			SET @PROPERTY_INS_COMM = @PROPERTY_INS_COMM_PRCT
			IF @IS_SAME_POLICY <> 'Y'
				SET @GRANT_COMMISSION = 1
		END

		IF @@ERROR <> 0
			GOTO ERRHANDLER
		
		IF @GRANT_COMMISSION = 1
		BEGIN
			IF @HOME_EMP = 'Y'
			BEGIN
				IF(@SYS_PROPERTY_INS_COMM_APPLICABILITY  = 2)
				BEGIN
					--Giving discount to employee
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, 
					POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_CODE ,TRAN_TYPE,TRAN_ENTITY,TRAN_DESC,
					DUE_DATE, COMMISSION_PERCENTAGE)
					VALUES (@FULLPREMIUM_ACCOUNT, -@PROPERTY_INS_COMM, @APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_EFFECTIVE_DATE, 'Discount given for property inspection credit.', @POLICY_ID,
					 @POLICY_VERSION_ID, 1, 1, 'DISC','PREM',@CUSTOMER_NAME,@TRAN_DESC,@POLICY_EFFECTIVE_DATE,
					CASE @COMMISSION_AMOUNT_TYPE WHEN 'P' THEN @PROPERTY_INS_COMM_PRCT ELSE NULL END)
		
					IF @@ERROR <> 0
						GOTO ERRHANDLER
					-- Contra Entry fo Discount 	
					-- Discount Given To Home Employee needs To Be Posted in to  Commission Expense Account
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID,SOURCE_EFFECTIVE_DATE,
					 TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS,TRAN_CODE ,TRAN_TYPE,TRAN_ENTITY,TRAN_DESC)
					VALUES ( @COMM_EXPENSE_ACCOUNT, @PROPERTY_INS_COMM, @APP_ID, @APP_VERSION_ID
					, @CUSTOMER_ID, @POLICY_EFFECTIVE_DATE, '', @POLICY_ID, @POLICY_VERSION_ID, 1, 0, 'DISC','PREM',@CUSTOMER_NAME,@TRAN_DESC)
		
					IF @@ERROR <> 0
						GOTO ERRHANDLER
		
				END
			END
			ELSE
			BEGIN
				IF(@SYS_PROPERTY_INS_COMM_APPLICABILITY  = 1)
				BEGIN 
					--Giving commission to agency
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID, SOURCE_EFFECTIVE_DATE, 
					TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_CODE, TRAN_TYPE,
					TRAN_ENTITY,TRAN_DESC , COMMISSION_PERCENTAGE )
					VALUES (@COMM_PAYABLE_ACCOUNT, -@PROPERTY_INS_COMM, @APP_ID, @APP_VERSION_ID, 
						@CUSTOMER_ID, @APP_AGENCY_ID, @POLICY_EFFECTIVE_DATE,
						 'Commission posted for property inspection credit.', @POLICY_ID, 
						@POLICY_VERSION_ID, 1, 1, 'PIC', 'COM',@AGENCY_NAME,@TRAN_DESC,
						CASE @COMMISSION_AMOUNT_TYPE WHEN 'P' THEN @PROPERTY_INS_COMM_PRCT ELSE NULL END)
	
					IF @@ERROR <> 0
						GOTO ERRHANDLER
					--Contra Entry For Property inspection Commission 
					INSERT INTO #TMP_POST(ACCOUNT_ID, AMT, APP_ID, APP_VERSION_ID, CUSTOMER_ID, AGENCY_ID, SOURCE_EFFECTIVE_DATE, TRANS_DESC, POLICY_ID, POLICY_VERSION_ID, ACCOUNTS, OPEN_ITEMS, TRAN_CODE, TRAN_TYPE,TRAN_ENTITY,TRAN_DESC)
					VALUES ( @COMM_EXPENSE_ACCOUNT, @PROPERTY_INS_COMM, @APP_ID, @APP_VERSION_ID
					, @CUSTOMER_ID, NULL, @POLICY_EFFECTIVE_DATE, '', @POLICY_ID, @POLICY_VERSION_ID, 1, 0, 'PIC', 'COM',@AGENCY_NAME,@TRAN_DESC)
					IF @@ERROR <> 0
						GOTO ERRHANDLER

					
				END
			END
		END
	END

	--Customer Open Item Posting
	INSERT INTO ACT_CUSTOMER_OPEN_ITEMS
	(
		UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, 
		POSTING_DATE, TOTAL_DUE, TOTAL_PAID, AGENCY_COMM_APPLIES, AGENCY_COMM_PERC, 
		AGENCY_COMM_AMT, AGENCY_ID, 
		CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS, NOT_COUNTED_RECEIVABLE, 
		PAYOR_TYPE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON, IS_PREBILL, BILL_CODE, GROSS_AMOUNT, 
		ITEM_TRAN_CODE,
		ITEM_TRAN_CODE_TYPE,
		 TRAN_ID, CASH_ACCOUNTING, RECUR_JOURNAL_VERSION, JE_RECON_COUNTER, 
		AMT_IN_RECON, OPEN_RECON_CTR, LOB_ID, SUB_LOB_ID, COUNTRY_ID, STATE_ID, TRANS_DESC, ITEM_STATUS,
		INSTALLMENT_ROW_ID, NET_PREMIUM,RISK_ID,RISK_TYPE,DUE_DATE , SWEEP_DATE
	)	
	SELECT 
		'P', @SOURCE_ROW_ID, @SOURCE_ROW_ID, @POLICY_EFFECTIVE_DATE , SOURCE_EFFECTIVE_DATE,
		GETDATE(),TP.AMT, 0, CASE @HOME_EMP WHEN 'Y' THEN 'N' ELSE 'Y' END, TP.COMMISSION_PERCENTAGE, 
		@AGENCY_REG_COMM, @APP_AGENCY_ID,
		TP.CUSTOMER_ID, TP.POLICY_ID, TP.POLICY_VERSION_ID, NULL, NULL, 1, NULL,
		'CUS', @DIV_ID, @DEPT_ID, @PC_ID, NULL, NULL, @IS_PREBILL, @BILL_TYPE, @TOTAL_PREMIUM_AMOUNT,
		TRAN_CODE, 
		TRAN_TYPE,
		NULL, NULL, NULL, NULL, 
		NULL, NULL, @POLICY_LOB, @POLICY_SUBLOB, @COUNTRY_ID, @STATE_ID, TP.TRANS_DESC, 'DP',
		TP.INSTALLMENT_ROW_ID, @PREMIUM_AMOUNT,@RISK_ID,@RISK_TYPE,TP.DUE_DATE , TP.DUE_DATE
	FROM 
		#TMP_POST TP WITH (NOLOCK) 
	WHERE ISNULL(TP.AGENCY_ID, 0) = 0 AND TP.OPEN_ITEMS = 1 AND ISNULL(TP.AMT,0) <> 0

--	INNER JOIN POL_CUSTOMER_POLICY_LIST PCL WITH (NOLOCK)ON 
--		PCL.POLICY_ID = TP.POLICY_ID AND PCL.POLICY_VERSION_ID = TP.POLICY_VERSION_ID AND PCL.CUSTOMER_ID = TP.CUSTOMER_ID
--	WHERE 
--		PCL.POLICY_ID = @POLICY_ID  AND  PCL.POLICY_VERSION_ID = @POLICY_VERSION_ID AND PCL.CUSTOMER_ID = @CUSTOMER_ID 
--		AND ISNULL(TP.AGENCY_ID, 0) = 0 AND TP.OPEN_ITEMS = 1 AND ISNULL(TP.AMT,0) <> 0

	
	IF @@ERROR <> 0
		GOTO ERRHANDLER	


	-- Commented By Ravindra(06-04-2007) Balance information will be entered after installments for all risks
	-- are being created 	
	--Inserting the entry in customer balance informartion
-- 	INSERT INTO ACT_CUSTOMER_BALANCE_INFORMATION
-- 	(
-- 		CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,
-- 		OPEN_ITEM_ROW_ID, AMOUNT, AMOUNT_DUE,
-- 		TRAN_DESC,
-- 		UPDATED_FROM, CREATED_DATE,
-- 		IS_PRINTED, PRINT_DATE, SOURCE_ROW_ID
-- 	)
-- 	SELECT 
-- 		ACOI.CUSTOMER_ID, ACOI.POLICY_ID, ACOI.POLICY_VERSION_ID, 
-- 		IDEN_ROW_ID, ACOI.TOTAL_DUE, 0,
-- 		case ITEM_TRAN_CODE_TYPE
-- 			WHEN 'FEES' THEN 'Fees written.'
-- 			ELSE CASE @INSTALLMENT 
-- 					WHEN @FULL_PAY_PLAN_ID	THEN 'Premium written.'
-- 					ELSE 'Installment created.'
-- 			END
-- 		END,
-- 		'P', GETDATE(),
-- 		0,NULL, @SOURCE_ROW_ID
-- 	FROM 
-- 		ACT_CUSTOMER_OPEN_ITEMS ACOI
-- 	WHERE 
-- 		ACOI.UPDATED_FROM = 'P' AND ACOI.SOURCE_ROW_ID = @SOURCE_ROW_ID
-- 		AND ACOI.RISK_ID = @RISK_ID
-- 	
-- 		
-- 	IF @@ERROR <> 0
-- 		GOTO ERRHANDLER	

	--Agency Open Item Posting
	INSERT INTO ACT_AGENCY_OPEN_ITEMS
	(
		UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, 
		POSTING_DATE, TOTAL_DUE, TOTAL_PAID, AGENCY_COMM_APPLIES, AGENCY_COMM_PERC, 
		AGENCY_COMM_AMT, AGENCY_ID, 
		CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS, NOT_COUNTED_RECEIVABLE, 
		PAYOR_TYPE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON, IS_PREBILL, BILL_CODE, GROSS_AMOUNT, 
		ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, CASH_ACCOUNTING, RECUR_JOURNAL_VERSION, JE_RECON_COUNTER, 
		AMT_IN_RECON, OPEN_RECON_CTR, LOB_ID, SUB_LOB_ID, COUNTRY_ID, STATE_ID, TRANS_DESC, NET_PREMIUM ,
		RISK_ID, RISK_TYPE
	)	
	SELECT 
		'P', @SOURCE_ROW_ID, @SOURCE_ROW_ID, @POLICY_EFFECTIVE_DATE, @POLICY_EFFECTIVE_DATE,
		GETDATE(), TP.AMT, 0, CASE @HOME_EMP WHEN 'Y' THEN 'N' ELSE 'Y' END, TP.COMMISSION_PERCENTAGE, 
		@AGENCY_REG_COMM, TP.AGENCY_ID,
		TP.CUSTOMER_ID, TP.POLICY_ID, TP.POLICY_VERSION_ID, NULL, NULL, 1, NULL,
		'AGN', @DIV_ID, @DEPT_ID, @PC_ID, NULL, NULL, @IS_PREBILL, @BILL_TYPE, @TOTAL_PREMIUM_AMOUNT,
		TP.TRAN_CODE, TP.TRAN_TYPE, NULL, NULL, NULL, NULL, 
		NULL, NULL, @POLICY_LOB, @POLICY_SUBLOB, @COUNTRY_ID, @STATE_ID, TP.TRANS_DESC, @PREMIUM_AMOUNT, 
		@RISK_ID,@RISK_TYPE
	FROM #TMP_POST TP WITH(NOLOCK) 
	WHERE @HOME_EMP = 'N' AND ISNULL(TP.AGENCY_ID,0) = @APP_AGENCY_ID AND TP.OPEN_ITEMS = 1 AND ISNULL(TP.AMT,0) <> 0

	
--	LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL WITH(NOLOCK)ON 
--		PCL.POLICY_ID = TP.POLICY_ID AND PCL.POLICY_VERSION_ID = TP.POLICY_VERSION_ID AND PCL.CUSTOMER_ID = TP.CUSTOMER_ID
--	WHERE 
--		PCL.POLICY_ID = @POLICY_ID  AND  PCL.POLICY_VERSION_ID = @POLICY_VERSION_ID AND PCL.CUSTOMER_ID = @CUSTOMER_ID
--		AND @HOME_EMP = 'N' AND ISNULL(TP.AGENCY_ID,0) = @APP_AGENCY_ID AND TP.OPEN_ITEMS = 1 
--		AND ISNULL(TP.AMT,0) <> 0


	IF @@ERROR <> 0
		GOTO ERRHANDLER	
	

	--Inserting the record into ACT_ACCOUNTS_POSTING_DETAILS
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS
	(
		ACCOUNT_ID, UPDATED_FROM, 
			SUBLEDGER_TYPE, 
		SOURCE_ROW_ID,
		MAPPING_SUBLEDGER_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, 
		POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC, AGENCY_COMM_AMT,
		AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID,
		PC_ID, IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE,
		TRAN_ID, LOB_ID, SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID, TAX_ID, ADNL_INFO,
		IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION, IN_BNK_RECON, IS_BALANCE_UPDATED,
		COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME, COMMISSION_TYPE
		,TRAN_ENTITY,TRAN_DESC

	)
	SELECT 
		TP.ACCOUNT_ID, 'P', 
			CASE TP.OPEN_ITEMS WHEN '1' THEN
						CASE 
							WHEN ISNULL(TP.AGENCY_ID, 0) = 0  THEN 'CUS'
							WHEN ISNULL(TP.AGENCY_ID, 0) <> 0 THEN 'AGN'
						END 
					ELSE NULL END, 
		@SOURCE_ROW_ID, 
		NULL, @SOURCE_ROW_ID, @POLICY_EFFECTIVE_DATE, @POLICY_EFFECTIVE_DATE,
		GETDATE(), SUM(TP.AMT), NULL, NULL, 
		@APP_AGENCY_ID , TP.CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @DIV_ID, @DEPT_ID,
		@PC_ID, @IS_PREBILL, @BILL_TYPE, SUM(TP.AMT), TP.TRAN_CODE, TP.TRAN_TYPE,
		NULL,  @POLICY_LOB, @POLICY_SUBLOB, @COUNTRY_ID, @STATE_ID, NULL, NULL, NULL,
		NULL, NULL, NULL, NULL,
		@USER_ID, @USER_CODE, @USER_NAME, NULL,
		TP.TRAN_ENTITY ,TP.TRAN_DESC				
	FROM #TMP_POST  TP WITH(NOLOCK) 
	
--	LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL WITH(NOLOCK)ON 
--		PCL.POLICY_ID = TP.POLICY_ID AND PCL.POLICY_VERSION_ID = TP.POLICY_VERSION_ID AND PCL.CUSTOMER_ID = @CUSTOMER_ID
	WHERE TP.ACCOUNTS = 1
		AND ISNULL(TP.AMT,0) <> 0
	GROUP BY 
		TP.ACCOUNT_ID, TP.CUSTOMER_ID, TP.OPEN_ITEMS,TP.TRAN_ENTITY ,TP.TRAN_DESC	, 
		TP.TRAN_CODE, TP.TRAN_TYPE	, TP.AGENCY_ID		
	

	IF @@ERROR <> 0
		GOTO ERRHANDLER	

	--Droping the temp table
	DROP TABLE #TMP_POST 


	UPDATE ACT_PREMIUM_PROCESS_DETAILS SET PROCESS_STATUS = 'P' 
	WHERE POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
		AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID 
		AND	CUSTOMER_ID = @CUSTOMER_ID

	
	IF @@ERROR <> 0
		GOTO ERRHANDLER
	 

	INSERT INTO ACT_PREMIUM_PROCESS_SUB_DETAILS
			(CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID , PPD_ROW_ID , 
			RISK_ID ,RISK_TYPE,  NET_PREMIUM , STATS_FEES , 
			GROSS_PREMIUM , INFORCE_PREMIUM , INFORCE_FEES  ,
			--Ravindra(03-27-2009)
			COMMISSION_RATE , COMMISSION_AMOUNT ,EFFECTIVE_DATE ,CURRENT_COMMISSION_RATE  )

	VALUES ( @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID , @SOURCE_ROW_ID , 
			@RISK_ID , @RISK_TYPE ,  @PREMIUM_AMOUNT ,  @MCCA_FEES + @OTHER_FEES ,
			@TOTAL_PREMIUM_AMOUNT , @TOTAL_PREMIUM_AMOUNT , @MCCA_FEES + @OTHER_FEES ,
			
			@AGENCY_REG_COMM_PRCT ,	@AGENCY_REG_COMM	,@POLICY_EFFECTIVE_DATE ,@AGENCY_REG_COMM_PRCT )
			
	IF @@ERROR <> 0
		GOTO ERRHANDLER	
		--Added By Ravindra (07-31-2007) : To Reconcile Discount Items with the respective installments
	IF(@BILL_TYPE <> 'AB' AND @HOME_EMP = 'Y')	
	BEGIN 
		IF ISNULL(@INSTALLMENT,0) = @FULL_PAY_PLAN_ID 	
		BEGIN 
			EXEC Proc_ReconcileDiscountItems @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID , @RISK_ID , @RISK_TYPE , 'Y'
			IF @@ERROR <> 0
				GOTO ERRHANDLER
		END
		ELSE
		BEGIN 
			EXEC Proc_ReconcileDiscountItems @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID , @RISK_ID , @RISK_TYPE , 'N'
			IF @@ERROR <> 0
				GOTO ERRHANDLER
		END
	END
	--Added By Ravindra(08-02-2007) For creating a record in ACT_PREMIUM_PROCESS_SUB_DETAILS for each risk


-- Commented By Ravindra(06-11-2007):  Moved to Seprate SP (ReconcileCustomerDepsits)
-- , this logic will be called after premium for all risks in a policy are processed 	
-- 	--Changed By Ravinda to handle multiple Deposits
-- 	--If there are some payments against this policy
-- 	--then reconciling the payments against the open items
-- 	IF EXISTS ( SELECT CD.DEPOSIT_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI
-- 				INNER JOIN ACT_CURRENT_DEPOSITS CD ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID
-- 				WHERE CDLI.CUSTOMER_ID = @CUSTOMER_ID
-- 				AND CDLI.POLICY_ID = @POLICY_ID 
-- 				AND CDLI.POLICY_VERSION_ID = @POLICY_VERSION_ID 
-- 				AND CD.IS_COMMITED = 'Y' )
-- 	BEGIN
-- 		DECLARE @DEPOSIT_ID INT, @COMMITTED_BY INT, @COMMITED_BY_CODE VARCHAR(30), @COMMITED_BY_NAME VARCHAR(50)
-- 
-- 		DECLARE @TEMP_DEPOSIT_LIST TABLE              
-- 		(              
-- 		IDENT_COL INT IDENTITY (1,1),              
-- 		DEPOSIT_ID INT,         
-- 		COMMITTED_BY INT              
-- 		)              
--    
-- 		--Extracting the deposit information
-- 		INSERT INTO @TEMP_DEPOSIT_LIST(DEPOSIT_ID,COMMITTED_BY)
-- 		SELECT 
-- 			 DEPOSIT_ID,MODIFIED_BY
-- 		FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS
-- 		WHERE CUSTOMER_ID = @CUSTOMER_ID
-- 				AND POLICY_ID = @POLICY_ID 
-- 				AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
-- 		ORDER BY DEPOSIT_ID
-- 
-- 
-- 		IF @@ERROR <> 0
-- 			GOTO ERRHANDLER	
-- 	
-- 		DECLARE @IDENT_COL INT              
-- 		SET  @IDENT_COL = 1    
-- 		WHILE(1=1)
-- 		BEGIN
-- 			IF NOT EXISTS              
-- 		  	(              
-- 		   	SELECT IDENT_COL FROM @TEMP_DEPOSIT_LIST             
-- 		   	WHERE IDENT_COL = @IDENT_COL              
-- 		        )              
-- 		  	BEGIN              
-- 		   		BREAK              
-- 		  	END
-- 		
-- 
-- 			--Extracting the deposit information
-- 			SELECT 
-- 			@DEPOSIT_ID = DEPOSIT_ID,
-- 			@COMMITTED_BY = COMMITTED_BY
-- 			FROM @TEMP_DEPOSIT_LIST
-- 			WHERE IDENT_COL = @IDENT_COL
-- 
-- 			--Extracting the user information
-- 			SELECT @COMMITED_BY_CODE = USER_LOGIN_ID, @COMMITED_BY_NAME = IsNull(USER_FNAME,'') + ' ' + IsNull(USER_LNAME,'')
-- 			FROM MNT_USER_LIST 
-- 			WHERE [USER_ID] = @COMMITTED_BY		
-- 			IF @@ERROR <> 0
-- 				GOTO ERRHANDLER	
-- 	
-- 			--Reconciling and posting the deposit
-- 			EXEC Proc_PostAndReconcileCustomerDeposit @DEPOSIT_ID, @FISCAL_ID, @COMMITTED_BY, 
-- 				@COMMITED_BY_CODE, @COMMITED_BY_NAME,2 -- 2 For Policy Process
-- 			IF @@ERROR <> 0
-- 				GOTO ERRHANDLER	
-- 
-- 			SET @IDENT_COL = @IDENT_COL +1
-- 		END
-- 	END
		SET @RetVal =1
	RETURN 1

ERRHANDLER:
	set @RetVal =-1
	RETURN -1

END 


--go
--
--declare @TRANS_DESC varchar(500)
--declare @RetVal int
--declare @TOTAL_FEES numeric(12,2)
--exec Proc_InsertPremiumPolicyOpenItems  1989 , 28 ,1,24,1,209.00,80,0,@TRANS_DESC out,7,1,'PP','N',10892,3,null,@RetVal out ,@TOTAL_FEES out
--select @TRANS_DESC,@RetVal,@TOTAL_FEES
--
--rollback tran


GO

