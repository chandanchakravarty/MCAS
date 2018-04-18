IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_PAGNET_IMPORT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_PAGNET_IMPORT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[PROC_PAGNET_IMPORT] --'',1,'','Commission270420111625.txt'
@records VARCHAR(MAX)='',
@Isformat INT =0,
@CREATED_BY VARCHAR(50)='',
@IMPORT_FILE_NAME VARCHAR(50)='',
@DEL_FLAG INT=NULL, 
--@USER_NAME VARCHAR(100)=NULL,
@RETURN_VALUE INT =0  OUT
AS 
BEGIN

DECLARE @FLAG_UPDATE_STATUS INT = 0

SET @RETURN_VALUE =0
IF(@Isformat =0 AND LEN(RTRIM(LTRIM(@records))) > 0)

BEGIN
	IF(@DEL_FLAG =1)
	truncate table  TEMP_PAGNET_IMPORT
	
	INSERT TEMP_PAGNET_IMPORT
	SELECT @records 
	
	print '1'
	
END


IF @Isformat =1 -- AND  NOT EXISTS (SELECT * FROM PAGNET_RETURN_FILE )
	BEGIN
	
	print '2'


	--SET IDENTITY_INSERT = ON

	CREATE TABLE #TEMP_TABLE_2
	(
	ID	INT IDENTITY,
	Payment_ID VARCHAR(25),
	EVENT_CODE VARCHAR(5),
	Payment_Method VARCHAR(3),
	Commission_Payment_date	datetime,
	Commission_Paid_Amount_sign	varchar(1),
	Commission_paid_amount	decimal(18,2),
	Payment_currency	VARCHAR(5),
	Bank_account_number	VARCHAR(5),
	Bank_branch_code	VARCHAR(10),
	No_da_Conta_Corrente	VARCHAR(12),
	Cheque_number	VARCHAR(15),
	Sinal_do_Valor_IR_2	varchar(1),
	Valor_IR_2	decimal(18,2),
	Sinal_do_Valor_ISS_2 varchar(1),
	Valor_ISS	decimal(18,2),
	Sinal_do_Valor_INSS	varchar(1),
	Valor_INSS	decimal(18,2),
	Sinal_do_Valor_CSLL	varchar(1),
	Valor_CSLL	decimal(18,2),
	Sinal_do_Valor_COFINS	varchar(1),
	Valor_COFINS	decimal(18,2),
	Sinal_do_Valor_PIS	varchar(1),
	Valor_PIS	decimal(18,2),
	Occurrence_code	VARCHAR(3),
	Cheque_cancellation_reason varchar(60),
	Payment_method_2	VARCHAR(3),
	Carrier_Bank_Number	VARCHAR(5),
	Carrier_Bank_Branch_number	VARCHAR(10),
	Carrier_Bank_Account_number	VARCHAR(12),
	Exchange_rate_2	decimal(18,7),
	INCONSISTENCY_1 varchar(5),
	  INCONSISTENCY_2 varchar(5),
	 INCONSISTENCY_3 varchar(5),
	 INCONSISTENCY_4 varchar(5),
	  INCONSISTENCY_5 varchar(5),
	  FULL_RECORD VARCHAR(MAX),
	CREATE_DATE DATETIME
	)


	INSERT INTO #TEMP_TABLE_2
	(
	Payment_ID,
	EVENT_CODE,
	Payment_Method,
	Commission_Payment_date	,
	Commission_Paid_Amount_sign,
	Commission_paid_amount	,
	Payment_currency	,
	Bank_account_number	,
	Bank_branch_code	,
	No_da_Conta_Corrente,
	Cheque_number	,
	Sinal_do_Valor_IR_2,
	Valor_IR_2	,
	Sinal_do_Valor_ISS_2,
	Valor_ISS	,
	Sinal_do_Valor_INSS,
	Valor_INSS	,
	Sinal_do_Valor_CSLL,
	Valor_CSLL	,
	Sinal_do_Valor_COFINS	,
	Valor_COFINS	,
	Sinal_do_Valor_PIS	,
	Valor_PIS	,
	Occurrence_code,
	Cheque_cancellation_reason,	
	Payment_method_2,
	Carrier_Bank_Number,
	Carrier_Bank_Branch_number,
	Carrier_Bank_Account_number,
	Exchange_rate_2,
	INCONSISTENCY_1 ,
	  INCONSISTENCY_2 ,
	 INCONSISTENCY_3 ,
	 INCONSISTENCY_4 ,
	  INCONSISTENCY_5 ,
	  FULL_RECORD,
	CREATE_DATE	
	)

	SELECT 
				--SUBSTRING([STRR],1,3)  AS 'Interface_code',
				--SUBSTRING([STRR],4,10) AS 'Sequential_number',
				--SUBSTRING([STRR],14,1) AS 'Beneficiary_Type',
				--SUBSTRING([STRR],15,1) AS 'Foreign',
				--SUBSTRING([STRR],16,3) AS 'Beneficiary_Class',
				--SUBSTRING([STRR],19,60) AS 'Beneficiary_name',
				--SUBSTRING([STRR],79,14) AS 'Beneficiary_ID',
				--SUBSTRING([STRR],93,14) AS 'Beneficiary_foreign_ID',
				--SUBSTRING([STRR],107,30) AS 'Beneficiary_Address_street_name',
				--SUBSTRING([STRR],137,5) AS 'Beneficiary_Address_number',
				--SUBSTRING([STRR],142,10) AS 'Beneficiary_Address_complement',
				--SUBSTRING([STRR],152,30) AS 'Beneficiary_Address_district',
				--SUBSTRING([STRR],182,2) AS 'Beneficiary_Address_state',
				--SUBSTRING([STRR],184,30) AS 'Beneficiary_Address_city',
				--SUBSTRING([STRR],214,10) AS 'Beneficiary_Address_zip_code',
				--SUBSTRING([STRR],224,60) AS 'Beneficiary_ email_address',
				--SUBSTRING([STRR],284,5) AS 'Beneficiary_ Bank_Number',
				--SUBSTRING([STRR],289,10) AS 'Beneficiary_Bank_Branch',
				--SUBSTRING([STRR],299,1) AS 'Beneficiary_Bank_Branch_Verifier_Digit',
				--SUBSTRING([STRR],300,12) AS 'Beneficiary_Bank_Account_number',
				--SUBSTRING([STRR],312,2) AS 'Beneficiary_Bank_Account_Verifier_Digit',
				--SUBSTRING([STRR],314,2) AS 'Beneficiary_Bank_Account_type',
				--SUBSTRING([STRR],316,5) AS 'Beneficiary_Bank_Account_Currency',
				--SUBSTRING([STRR],321,2) AS 'Cód_Tributação_IRRF',
				--SUBSTRING([STRR],323,5) AS 'Natureza_do_Rendimento',
				--SUBSTRING([STRR],328,1) AS 'Calcula_ISS_?',
				--SUBSTRING([STRR],329,1) AS 'Calcula_INSS_?',
				--SUBSTRING([STRR],330,2) AS 'Cód_Tributação_INSS',
				--SUBSTRING([STRR],332,2) AS 'Cód_Tributação_CSLL',
				--SUBSTRING([STRR],334,2) AS 'Cód_Tributação_COFINS',
				--SUBSTRING([STRR],336,2) AS 'Cód_Tributação_PIS',
				--SUBSTRING([STRR],338,2) 'No_de_Dependentes',
				--SUBSTRING([STRR],340,11) 'No_PIS',
				--SUBSTRING([STRR],351,11) 'Inscrição_Municipal',
				--SUBSTRING([STRR],362,10) 'Número_interno_do_corretor',
				--SUBSTRING([STRR],372,10) 'CBO_Classific_Brasileira_Ocupação',
				--SUBSTRING([STRR],382,14) 'Código_SUSEP',
				--SUBSTRING([STRR],396,10) 'No_do_Funcionário',
				--SUBSTRING([STRR],406,15) 'Cód_da_Filial',
				--SUBSTRING([STRR],421,15) 'Cód_do_Centro_de_Custo',
				--SUBSTRING([STRR],436,8) 'Data_de_Nascimento',
				--SUBSTRING([STRR],444,1) 'Tipo_de_Pessoa',
				--SUBSTRING([STRR],445,1) 'Estrangeiro',
				--SUBSTRING([STRR],446,3) 'Payee_Class',
				--SUBSTRING([STRR],449,60) 'Payee_Name',
				--SUBSTRING([STRR],509,14) 'Payee_ID_CPF',
				--SUBSTRING([STRR],523,14) 'Payee_foreign_ID',
				--SUBSTRING([STRR],537,5)  'Payee_Bank_Number',
				--SUBSTRING([STRR],542,10) 'Payee_Bank_Branch',
				--SUBSTRING([STRR],552,1) 'Payee_Bank_Branch_Verifier_Digit',
				--SUBSTRING([STRR],553,12) 'Payee_Bank_Account_no',
				--SUBSTRING([STRR],565,2)  'Payee_Bank_Account_Verifier_digit',
				--SUBSTRING([STRR],567,2) 'Payee_Bank_Account_Type',
				--SUBSTRING([STRR],569,5) 'Payee_Bank_Account_Currency',
				SUBSTRING([STRR],574,25) 'Payment_ID',
				--SUBSTRING([STRR],599,5) 'Carrier_Code',
				--SUBSTRING([STRR],604,5) 'Carrier_Policy_Branch_Code',
				SUBSTRING([STRR],609,5) 'Event_Code',
				--SUBSTRING([STRR],614,3) 'Operation_Type',
				SUBSTRING([STRR],617,3) 'Payment_Method',
				--SUBSTRING([STRR],620,20) 'Document_number',
				--SUBSTRING([STRR],640,5) 'Document_number_serial_number',
				--SUBSTRING([STRR],645,8) 'Invoice_issuance_date',
				--SUBSTRING([STRR],653,8) 'Invoice_due_date',
				--SUBSTRING([STRR],661,5) 'Policy_Currency',
				--CAST(SUBSTRING(SUBSTRING([STRR],666,15),1,8)+'.'+SUBSTRING(SUBSTRING([STRR],666,15),9,7) AS DECIMAL(18,2)) 'Exchange_rate' ,
				--SUBSTRING([STRR],681,1) 'Commission_amount_sign',
				--CAST(SUBSTRING(SUBSTRING([STRR],682,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],682,18),17,2) AS DECIMAL(18,2)) 'Commission_amount',
				--SUBSTRING([STRR],700,1) 'Sinal_do_Valor_Isento_IR',
				--CAST(SUBSTRING(SUBSTRING([STRR],701,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],701,18),17,2) AS DECIMAL(18,2))   'Valor_Isento_IR',
				--SUBSTRING([STRR],719,1) 'Sinal_do_Valor_Tributável_IR',
				--CAST(SUBSTRING(SUBSTRING([STRR],720,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],720,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_IR',
				--SUBSTRING([STRR],738,1) 'Sinal_do_Valor_Isento_ISS',
				--CAST(SUBSTRING(SUBSTRING([STRR],739,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],739,18),17,2)AS DECIMAL(18,2)) 'Valor_Isento_ISS',
				--SUBSTRING([STRR],757,1) 'Sinal_do_Valor_Tributável_ISS',
				--CAST(SUBSTRING(SUBSTRING([STRR],758,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],758,18),17,2) AS DECIMAL(18,2))  'Valor_Tributável_ISS',
				--SUBSTRING([STRR],776,1) 'Sinal_do_Valor_Isento_INSS',
				--CAST(SUBSTRING(SUBSTRING([STRR],777,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],777,18),17,2) AS DECIMAL(18,2)) 'Valor_Isento_INSS',
				--SUBSTRING([STRR],795,1) 'Sinal_do_Valor_Tributável_INSS',
				--CAST(SUBSTRING(SUBSTRING([STRR],796,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],777,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_INSS',
				--SUBSTRING([STRR],814,1) 'Sinal_do_Valor_Isento_CSLL/COFINS/PIS',
				--CAST(SUBSTRING(SUBSTRING([STRR],815,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],815,18),17,2) AS DECIMAL(18,2)) 'Valor_Isento_ CSLL/COFINS/PIS',
				--SUBSTRING([STRR],833,1) 'Sinal_do_Valor_Tributável_CSLL/COFINS/PIS',
				--CAST(SUBSTRING(SUBSTRING([STRR],834,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],834,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_CSLL/COFINS/PIS',
				--SUBSTRING([STRR],852,1) 'Sinal_do_Valor_IR',
				--CAST(SUBSTRING(SUBSTRING([STRR],853,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],853,18),17,2) AS DECIMAL(18,2))  'Valor_IR',
				--SUBSTRING([STRR],871,1) 'Sinal_do_Valor_ISS',
				--CAST(SUBSTRING(SUBSTRING([STRR],872,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],872,18),17,2) AS DECIMAL(18,2)) 'Valor ISS',
				--SUBSTRING([STRR],890,1) 'Sinal_do_Valor_Desconto',
				--CAST(SUBSTRING(SUBSTRING([STRR],891,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],891,18),17,2) AS DECIMAL(18,2)) 'Valor_Desconto',
				--SUBSTRING([STRR],909,1) 'Sinal_do_Valor_Multa',
				--CAST(SUBSTRING(SUBSTRING([STRR],910,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],910,18),17,2) AS DECIMAL(18,2)) 'Valor_Multa',
				--SUBSTRING([STRR],928,60) 'Payment_Description',
				--SUBSTRING([STRR],988,15) 'Policy_Branch_Code',
				--SUBSTRING([STRR],1003,15) 'Profit_Center_code',
				--SUBSTRING([STRR],1018,15) 'A_DEFINIR',
				--SUBSTRING([STRR],1033,15) 'A_DEFINIR2',
				--SUBSTRING([STRR],1048,15) 'Policy_Accounting_LOB',
				--SUBSTRING([STRR],1063,15) 'A_DEFINIR3',
				--SUBSTRING([STRR],1078,15) 'Policy_number' ,
				--SUBSTRING([STRR],1093,10) 'Policy_Number_remaining_digits',
				--SUBSTRING([STRR],1103,5) 'Endorsement_number',
				--SUBSTRING([STRR],1108,5) 'Installment_number ',
				--SUBSTRING([STRR],1113,15) 'A_DEFINIR4',
				--SUBSTRING([STRR],1128,10) 'A_DEFINIR5',
				--SUBSTRING([STRR],1138,15) 'Installment_payment_date',
				--SUBSTRING([STRR],1153,60) 'Applicant/CoApplicant_name',
				--CAST(SUBSTRING(SUBSTRING([STRR],1213,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1213,18),17,2) AS DECIMAL(18,2)) 'Premium_amount',
				--CAST(SUBSTRING(SUBSTRING([STRR],1231,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1231,18),17,2) AS DECIMAL(18,2)) 'Commission_percentage',
				--CAST(SUBSTRING(SUBSTRING([STRR],1249,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1249,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR6',
				--CAST(SUBSTRING(SUBSTRING([STRR],1267,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1267,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR7',
				--CAST(SUBSTRING(SUBSTRING([STRR],1285,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1285,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR8',
				--SUBSTRING([STRR],1303,1) 'Movement_type',		
				CASE  WHEN  SUBSTRING(SUBSTRING([STRR],1304,8),1,4)<>'0000' 
				THEN CONVERT(DATETIME, SUBSTRING(SUBSTRING([STRR],1304,8),1,4)+'-'+SUBSTRING(SUBSTRING([STRR],1304,8),5,2)+ '-'+SUBSTRING(SUBSTRING([STRR],1304,8),7,2)+' 00:00:00') 			
				ELSE CONVERT(DATETIME,CONVERT(DATETIME,NULL)) END AS 'Commission_Payment_date',
				SUBSTRING([STRR],1312,1) 'Commission_Paid_Amount_sign',
				CAST(SUBSTRING(SUBSTRING([STRR],1313,18),1,16) +'.'+SUBSTRING(SUBSTRING([STRR],1313,18),17,2) AS DECIMAL(18,2))  'Commission_paid_amount' ,
				SUBSTRING([STRR],1331,5)  'Payment_currency' ,
				SUBSTRING([STRR],1336,5)  'Bank_account_number',
				SUBSTRING([STRR],1341,10) 'Bank_branch_code',
				SUBSTRING([STRR],1351,12)  'No_da_Conta_Corrente',
				SUBSTRING([STRR],1363,15)  'Cheque_number',
				SUBSTRING([STRR],1378,1) 'Sinal_do_Valor_IR_2',
				CAST(SUBSTRING(SUBSTRING([STRR],1379,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1379,18),17,2) AS DECIMAL(18,2)) 'Valor_IR_2',
				SUBSTRING([STRR],1397,1) 'Sinal_do_Valor_ISS_2',
				CAST(SUBSTRING(SUBSTRING([STRR],1398,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1398,18),17,2) AS DECIMAL(18,2)) 'Valor_ISS',
				SUBSTRING([STRR],1416,1) 'Sinal_do_Valor_INSS',
				CAST(SUBSTRING(SUBSTRING([STRR],1417,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1417,18),17,2) AS DECIMAL(18,2)) 'Valor_INSS',
				SUBSTRING([STRR],1435,1) 'Sinal_do_Valor_CSLL' ,
				CAST(SUBSTRING(SUBSTRING([STRR],1436,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1436,18),17,2) AS DECIMAL(18,2)) 'Valor_CSLL',
				SUBSTRING([STRR],1454,1) 'Sinal_do_Valor_COFINS',
				CAST(SUBSTRING(SUBSTRING([STRR],1455,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1455,18),17,2) AS DECIMAL(18,2)) 'Valor_COFINS',
				SUBSTRING([STRR],1473,1) 'Sinal_do_Valor_PIS',
				CAST(SUBSTRING(SUBSTRING([STRR],1474,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1474,18),17,2) AS DECIMAL(18,2)) 'Valor_PIS',
				SUBSTRING([STRR],1492,3) 'Occurrence_code',
				SUBSTRING([STRR],1495,60) 'Cheque_cancellation_reason',
				SUBSTRING([STRR],1555,3)  'Payment_method_2',
				SUBSTRING([STRR],1558,5) 'Carrier_Bank_Number',
				SUBSTRING([STRR],1563,10) 'Carrier_Bank_Branch_number',
				SUBSTRING([STRR],1573,12) 'Carrier_Bank_Account_number',
				CAST(SUBSTRING(SUBSTRING([STRR],1585,15),1,8)+'.'+SUBSTRING(SUBSTRING([STRR],1585,15),9,7) AS DECIMAL(18,7)) 'Exchange_rate_2',
				SUBSTRING([STRR],1600,5) 'INCONSISTENCY_1',
				SUBSTRING([STRR],1605,5) 'INCONSISTENCY_2',
				SUBSTRING([STRR],1610,5) 'INCONSISTENCY_3',
				SUBSTRING([STRR],1615,5) 'INCONSISTENCY_4',
				SUBSTRING([STRR],1620,5) 'INCONSISTENCY_5',
				[STRR],
				GETDATE() 
			
			 FROM TEMP_PAGNET_IMPORT 
			 DECLARE @FILE_STATUS VARCHAR(200)=NULL,
				@FILE_ID INT=NULL
			 
			PRINT @IMPORT_FILE_NAME
			 --SELECT  [STATUS],* FROM PAGNET_EXPORT_FILES WHERE FILE_NAMES = 'Customer_Refund250420111641.txt'
			SELECT @FILE_ID =ID, @FILE_STATUS = [STATUS] FROM PAGNET_EXPORT_FILES WHERE FILE_NAMES = @IMPORT_FILE_NAME
			
			DECLARE @COUNT INT, @COUNTER INT =0,	
				@TEMP_PAYMENT_ID VARCHAR(25)='', @TEMP_EVENT_CODE VARCHAR(5)='',
				@OCCURRENCE_CODE VARCHAR(3),
				@FULL_RECORD VARCHAR(MAX),
				@TEMP_ID INT =NULL,
				@RETURN_STATUS  VARCHAR(200) = NULL,
				@PAYMENT_METHOD VARCHAR(3)=NULL
				--@PAYMENT_METHOD_2 VARCHAR(3)=NULL
				
				--,@INCONSISTENCY_1 VARCHAR(5)=NULL,
				--  @INCONSISTENCY_2 VARCHAR(5)=NULL,
				-- @INCONSISTENCY_3 VARCHAR(5)=NULL,
				-- @INCONSISTENCY_4 VARCHAR(5)=NULL,
				--  @INCONSISTENCY_5 VARCHAR(5)=NULL
				print 3
			
			IF(RTRIM(Ltrim(@FILE_STATUS)) ='Imported by PagNet with success') --IMPORTING IN PAGNET_RETURN_FILE (TABLE)
			 BEGIN
				 print 'Imported by PagNet with success'
					SELECT @COUNT = COUNT(*) FROM #TEMP_TABLE_2
					
					SET @COUNTER =1	
					
					WHILE (@COUNT > 0)		
					BEGIN			
					
						--SELECT  @FULL_RECORD = STRR FROM TEMP_PAGNET_IMPORT WHERE ID = @COUNTER
						
						SELECT 	@TEMP_PAYMENT_ID = Payment_ID,@TEMP_EVENT_CODE=EVENT_CODE,
						@OCCURRENCE_CODE =OCCURRENCE_CODE,
						@PAYMENT_METHOD= CASE WHEN Payment_method_2 ='000' THEN Payment_method
						ELSE Payment_method_2 END
						--,@INCONSISTENCY_1 = INCONSISTENCY_1,
						--  @INCONSISTENCY_2 = INCONSISTENCY_2,
						-- @INCONSISTENCY_3 = INCONSISTENCY_3,
						-- @INCONSISTENCY_4 = INCONSISTENCY_4,
						--  @INCONSISTENCY_5 = INCONSISTENCY_5
						
						
						 FROM #TEMP_TABLE_2 WHERE ID =@COUNTER
								
						IF NOT EXISTS( SELECT * FROM PAGNET_RETURN_FILE  WHERE  RTRIM(LTRIM(Payment_ID)) = RTRIM(LTRIM(@TEMP_PAYMENT_ID)) 
											AND RTRIM(LTRIM(EVENT_CODE)) = RTRIM(LTRIM(@TEMP_EVENT_CODE)) 
											AND RTRIM(LTRIM(OCCURRENCE_CODE)) = RTRIM(LTRIM(@OCCURRENCE_CODE)))
						 BEGIN
						 
								--IMPORT RECORD INT PAGNET_RETURN FILE
								INSERT  INTO PAGNET_RETURN_FILE
							(
								PAYMENT_ID,
								EVENT_CODE,
								COMMISSION_PAYMENT_DATE,
								COMMISSION_PAID_AMOUNT_SIGN,
								COMMISSION_PAID_AMOUNT,
								PAYMENT_CURRENCY,
								BANK_ACCOUNT_NUMBER,
								BANK_BRANCH_CODE,
								NO_DA_CONTA_CORRENTE,
								CHEQUE_NUMBER,
								SINAL_DO_VALOR_IR_2,
								[VALOR_IR_2],
								SINAL_DO_VALOR_ISS_2,
								VALOR_ISS,
								SINAL_DO_VALOR_INSS,
								VALOR_INSS,
								SINAL_DO_VALOR_CSLL,
								VALOR_CSLL,
								SINAL_DO_VALOR_COFINS,
								VALOR_COFINS,
								SINAL_DO_Valor_PIS,
								VALOR_PIS,						
								OCCURRENCE_CODE,
								CHEQUE_CANCELLATION_REASON,
								PAYMENT_METHOD_2,
								CARRIER_BANK_NUMBER,
								CARRIER_BANK_BRANCH_NUMBER,
								CARRIER_BANK_ACCOUNT_NUMBER,
								EXCHANGE_RATE_2,
								CREATED_BY,
								CREATED_DATE,
								[FILE_NAME],
								FULL_RECORD,
								INCONSISTENCY_1,
							    INCONSISTENCY_2,
							    INCONSISTENCY_3,
							    INCONSISTENCY_4,
							    INCONSISTENCY_5 
									
							)	
							SELECT 
								PAYMENT_ID,
								EVENT_CODE,
								COMMISSION_PAYMENT_DATE,
								COMMISSION_PAID_AMOUNT_SIGN,
								COMMISSION_PAID_AMOUNT,
								PAYMENT_CURRENCY,
								BANK_ACCOUNT_NUMBER,
								BANK_BRANCH_CODE,
								NO_DA_CONTA_CORRENTE,
								CHEQUE_NUMBER,
								SINAL_DO_VALOR_IR_2,
								[VALOR_IR_2],
								SINAL_DO_VALOR_ISS_2,
								VALOR_ISS,
								SINAL_DO_VALOR_INSS,
								VALOR_INSS,
								SINAL_DO_VALOR_CSLL,
								VALOR_CSLL,
								SINAL_DO_VALOR_COFINS,
								VALOR_COFINS,
								SINAL_Do_VALOR_PIS,
								VALOR_PIS,						
								OCCURRENCE_CODE,
								CHEQUE_CANCELLATION_REASON,
								PAYMENT_METHOD_2,
								CARRIER_BANK_NUMBER,
								CARRIER_BANK_BRANCH_NUMBER,
								CARRIER_BANK_ACCOUNT_NUMBER,
								EXCHANGE_RATE_2,
								@CREATED_BY,
								GETDATE(),
								@IMPORT_FILE_NAME,
								FULL_RECORD,
								INCONSISTENCY_1 ,
								INCONSISTENCY_2 ,
								INCONSISTENCY_3 ,
								INCONSISTENCY_4 ,
								INCONSISTENCY_5 
								--CREATED_DATE	
							 FROM #TEMP_TABLE_2 WHERE ID =@COUNTER --AND PAYMENT_ID  NOT IN (SELECT  Payment_ID  FROM PAGNET_RETURN_FILE)
							
							
							SELECT @RETURN_STATUS = ENG_DESCRIPTION FROM PAGNET_INTERFACE_CODES WITH(NOLOCK)
							 WHERE PAYMENT_METHOD_CODE = @PAYMENT_METHOD AND INTERFACE_CODE = @OCCURRENCE_CODE

							
							

									 						 
										
									print 'check'
									
									-- UPDATED 	PAGNET_EXPORT_RECORD
									 UPDATE PER
									 SET PER.RETURN_STATUS = @RETURN_STATUS, 
									  PER.PROCESSED ='Y',
										PER.DATA_DO_PAGAMENTO	=	PRF.COMMISSION_PAYMENT_DATE,
										PER.SINAL_DO_VALOR_PAGO	=	PRF.COMMISSION_PAID_AMOUNT_SIGN,
										PER.VALOR_PAGO	=	PRF.COMMISSION_PAID_AMOUNT,
										PER.MOEDA_DO_PAGAMENTO	=	PRF.PAYMENT_CURRENCY,
										PER.CÓDIGO_DO_BANCO	=	PRF.BANK_ACCOUNT_NUMBER,
										PER.NO_DA_AGÊNCIA	=	PRF.BANK_BRANCH_CODE,
										PER.NO_DA_CONTA_CORRENTE	=	PRF.No_DA_CONTA_CORRENTE,
										PER.NO_DO_CHEQUE	=	PRF.CHEQUE_NUMBER,
										PER.SINAL_DO_VALOR_IR_2	=	PRF.SINAL_DO_VALOR_IR_2,
										PER.VALOR_IR_2	=	PRF.VALOR_IR_2,
										PER.SINAL_DO_VALOR_ISS_2	=	PRF.SINAL_DO_VALOR_ISS_2,
										PER.VALOR_ISS_2	=	PRF.VALOR_ISS,
										PER.SINAL_DO_VALOR_INSS	=	PRF.SINAL_DO_VALOR_INSS,
										PER.VALOR_INSS	=	PRF.VALOR_INSS,
										PER.SINAL_DO_VALOR_CSLL	=	PRF.SINAL_DO_VALOR_CSLL,
										PER.VALOR_CSLL	=	PRF.VALOR_CSLL,
										PER.SINAL_DO_VALOR_COFINS	=	PRF.SINAL_DO_VALOR_COFINS,
										PER.VALOR_COFINS	=	PRF.VALOR_COFINS,
										PER.SINAL_DO_VALOR_PIS	=	PRF.SINAL_DO_VALOR_PIS,
										PER.VALOR_PIS	=	PRF.VALOR_PIS,
										PER.COD_OCORRENCIA	=	PRF.OCCURRENCE_CODE,
										PER.MOTIVO_DO_CANCELAMENTO_DO_CHEQUE	=	PRF.CHEQUE_CANCELLATION_REASON,
										PER.FORMA_DE_PAGAMENTO	=	PRF.PAYMENT_METHOD_2,
										PER.CÓDIGO_DO_BANCO_PAGADOR	=	PRF.CARRIER_BANK_NUMBER,
										PER.NO_DA_AGÊNCIA_2	=	PRF.CARRIER_BANK_BRANCH_NUMBER,
										PER.NO_DA_CONTA_CORRENTE_2	=	PRF.CARRIER_BANK_ACCOUNT_NUMBER,
										PER.TAXA_CONVERSÃO	=	PRF.EXCHANGE_RATE_2,
										PER.INCONSISTENCY_1 =PRF.INCONSISTENCY_1,
										PER.INCONSISTENCY_2 =PRF.INCONSISTENCY_2,
										PER.INCONSISTENCY_3 =PRF.INCONSISTENCY_3,
										PER.INCONSISTENCY_4 =PRF.INCONSISTENCY_4,
										PER.INCONSISTENCY_5 =PRF.INCONSISTENCY_5


									  --WHERE ID = @TEMP_ID
										from  pagnet_export_record PER
										INNER JOIN PAGNET_RETURN_FILE PRF
										on PER.PAYMENT_ID  = PRF.PAYMENT_ID

										INNER join PAGNET_EXPORT_FILES PEF
										on PEF.ID = PER.[FILE_ID]
										AND PEF.FILE_NAMES = PRF.[FILE_NAME]
										where PEF.ID = @FILE_ID AND RTRIM(LTRIM(PER.Payment_ID)) = RTRIM(LTRIM(@TEMP_PAYMENT_ID))
									  --WHERE [FILE_ID]=@FILE_ID AND RTRIM(LTRIM(Payment_ID)) = RTRIM(LTRIM(@TEMP_PAYMENT_ID))
								  
				IF(@FLAG_UPDATE_STATUS=0)
				BEGIN
				SET @FLAG_UPDATE_STATUS = 1
				IF EXISTS(SELECT * FROM PAGNET_EXPORT_RECORD WHERE [FILE_ID]=@FILE_ID AND RTRIM(LTRIM(Payment_ID)) = RTRIM(LTRIM(@TEMP_PAYMENT_ID)) )
							 BEGIN	 
								  
								  --UPDATED  PAGNET_EXPORT_FILES
								  UPDATE PAGNET_EXPORT_FILES
								   SET [STATUS] ='Return Processed'
								   WHERE FILE_NAMES = @IMPORT_FILE_NAME
								   
								    ---INSERT RECORD IN FILE HISTROY TABLE
								  INSERT INTO PAGNET_EXPORT_FILES_HISTORY
									([FILE_ID] ,
									INTERFACE_CODE ,
									CREATE_DATE ,
									[STATUS] ,
									USER_NAMES )
									
									SELECT 
									
									[ID],
									INTERFACE_CODE,
									GETDATE(),
									[STATUS],
									--@USER_NAME
									@CREATED_BY
									
									FROM PAGNET_EXPORT_FILES 
									WHERE FILE_NAMES = @IMPORT_FILE_NAME
							  
							  END
							 
					 END
								
							
								 
							 
							 SET @RETURN_VALUE = 1
							--PRINT 'INSERTED' 
						END 
						ELSE 
						
							BEGIN
							
							--SELECT * FROM #TEMP_TABLE_2
								INSERT  INTO DUPLICATE_PAGNET_RETURN_FILE
								(
									PAYMENT_ID,
									EVENT_CODE,
									COMMISSION_PAYMENT_DATE,
									COMMISSION_PAID_AMOUNT_SIGN,
									COMMISSION_PAID_AMOUNT,
									PAYMENT_CURRENCY,
									BANK_ACCOUNT_NUMBER,
									BANK_BRANCH_CODE,
									NO_DA_CONTA_CORRENTE,
									CHEQUE_NUMBER,
									SINAL_DO_VALOR_IR_2,
									[VALOR_IR_2],
									SINAL_DO_VALOR_ISS_2,
									VALOR_ISS,
									SINAL_DO_VALOR_INSS,
									VALOR_INSS,
									SINAL_DO_VALOR_CSLL,
									VALOR_CSLL,
									SINAL_DO_VALOR_COFINS,
									VALOR_COFINS,
									SINAL_DO_VALOR_PIS,
									VALOR_PIS,						
									OCCURRENCE_CODE,
									[CHEQUE_CANCELLATION_REASON],
									PAYMENT_METHOD_2,
									CARRIER_BANK_NUMBER,
									CARRIER_BANK_BRANCH_NUMBER,
									Carrier_Bank_Account_number,
									EXCHANGE_RATE_2,
									CREATED_BY,
									CREATED_DATE,
									[FILE_NAME],
									FULL_RECORD,
									INCONSISTENCY_1,
									INCONSISTENCY_2,
									INCONSISTENCY_3,
									INCONSISTENCY_4,
									INCONSISTENCY_5	
								)	
								SELECT 
									Payment_ID,
									EVENT_CODE,
									Commission_Payment_date,
									Commission_Paid_Amount_sign,
									Commission_paid_amount,
									Payment_currency,
									Bank_account_number,
									Bank_branch_code,
									No_da_Conta_Corrente,
									Cheque_number,
									Sinal_do_Valor_IR_2,
									[Valor_IR_2],
									Sinal_do_Valor_ISS_2,
									Valor_ISS,
									Sinal_do_Valor_INSS,
									Valor_INSS,
									Sinal_do_Valor_CSLL,
									Valor_CSLL,
									Sinal_do_Valor_COFINS,
									Valor_COFINS,
									Sinal_do_Valor_PIS,
									Valor_PIS,						
									Occurrence_code,
									CHEQUE_CANCELLATION_REASON,
									Payment_method_2,
									Carrier_Bank_Number,
									Carrier_Bank_Branch_number,
									Carrier_Bank_Account_number,
									Exchange_rate_2,
									@CREATED_BY,
									GETDATE(),									
									@IMPORT_FILE_NAME,
									FULL_RECORD,
									INCONSISTENCY_1,
									INCONSISTENCY_2,
									INCONSISTENCY_3,
									INCONSISTENCY_4,
									INCONSISTENCY_5				
								 FROM #TEMP_TABLE_2 WHERE ID =@COUNTER
								
								--PRINT 'RECORD NOT INSERTED' 
							 --PRINT @COUNTER
							 --PRINT @COUNT	
		 					END 
			 				
			 				
 								SET @COUNTER = @COUNTER +1
								SET @COUNT=@COUNT-1	
								
					
							
					END --LOOP END
				
				
				
					
					--SELECT * FROM #TEMP_TABLE_2
					DROP TABLE #TEMP_TABLE_2		
					DELETE FROM TEMP_PAGNET_IMPORT
			 END --IF END
			 
			 ELSE IF(@FILE_STATUS ='Imported by PagNet with error') --FOR ERROR FILE PROCESS UPDATING PAGNET_EXPORT_RECORD (TABLE)
				BEGIN
					
					
					SELECT @COUNT = COUNT(*) FROM #TEMP_TABLE_2
					
					SET @COUNTER =1	
					
					WHILE (@COUNT > 0)		
					BEGIN			
					
						--SELECT  @FULL_RECORD = STRR FROM TEMP_PAGNET_IMPORT WHERE ID = @COUNTER
						SELECT 	@TEMP_PAYMENT_ID = Payment_ID,@TEMP_EVENT_CODE=EVENT_CODE,@OCCURRENCE_CODE =OCCURRENCE_CODE  FROM #TEMP_TABLE_2 WHERE ID =@COUNTER
						
								 IF(@OCCURRENCE_CODE = '999') 
								SET @RETURN_STATUS = 'To Be Analyzed'
								ELSE 
									SET @RETURN_STATUS = 'Re-sent to Pagnet'
								
						
						IF EXISTS(SELECT * FROM PAGNET_EXPORT_RECORD WHERE [FILE_ID]=@FILE_ID AND RTRIM(LTRIM(Payment_ID)) = RTRIM(LTRIM(@TEMP_PAYMENT_ID)) )
							 BEGIN				 
							 					 
							  
							  
							  -- UPDATED 	PAGNET_EXPORT_RECORD
									 UPDATE PAGNET_EXPORT_RECORD
									 SET RETURN_STATUS = @RETURN_STATUS,  PROCESSED ='N'
									  --WHERE ID = @TEMP_ID
									  WHERE [FILE_ID]=@FILE_ID AND RTRIM(LTRIM(Payment_ID)) = RTRIM(LTRIM(@TEMP_PAYMENT_ID))
								  
								  --UPDATED  PAGNET_EXPORT_FILES
								  --UPDATE PAGNET_EXPORT_FILES
								  -- SET [STATUS] ='Imported by PagNet with error'
								  -- WHERE FILE_NAMES = @IMPORT_FILE_NAME	 
							  
								
								 
							  SET @RETURN_VALUE = 1
								
							END 
						
						 SET @COUNTER = @COUNTER +1
						SET @COUNT=@COUNT-1	
					END
					
					
					--SELECT * FROM #TEMP_TABLE_2
					DROP TABLE #TEMP_TABLE_2		
					DELETE FROM TEMP_PAGNET_IMPORT
			END
			
			 ELSE
			 print '5'
			 --SET @RETURN_VALUE = -1
			 
	END
END
GO

