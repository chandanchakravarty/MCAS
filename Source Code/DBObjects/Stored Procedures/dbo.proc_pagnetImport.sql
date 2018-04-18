IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_pagnetImport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_pagnetImport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BULK INSERT temp_table 
--FROM 'E:\ANKITFOLDER\COMISS_201102188.TXT' 
--WITH (ROWTERMINATOR = '\n') 

--SELECT POLICY_EFFECTIVE_DATE, * FROM POL_CUSTOMER_POLICY_LIST

--DECLARE @TEST VARCHAR(10)='',
--@TEST2 DATETIME
--SET @TEST =  '20110205' --REPLACE(CONVERT(VARCHAR,GETDATE(),111),'/','')
--PRINT @TEST
-- PRINT SUBSTRING(@TEST,1,4)+'-'+SUBSTRING(@TEST,5,2)+ '-'+SUBSTRING(@TEST,7,2)+' 00:00:00'
--PRINT CONVERT(DATETIME, SUBSTRING(@TEST,1,4)+'-'+SUBSTRING(@TEST,5,2)+ '-'+SUBSTRING(@TEST,7,2)+' 00:00:00')
--SET @TEST2 =CONVERT(DATETIME, SUBSTRING(@TEST,6,2)+'/'+SUBSTRING(@TEST,4,2)+ '/'+SUBSTRING(@TEST,1,4),103)
--PRINT @TEST2


--select * from temp_table
--  SELECT * FROM MAIN_COMMISSION
--  DELETE FROM temp_table
--  DELETE FROM MAIN_COMMISSION
  
--drop table temp_table
--drop table MAIN_COMMISSION
--DROP PROC proc_pagnetImport
--DROP PROC proc_pagnetImport
CREATE PROC proc_pagnetImport --'adfsadfadfas'
--@PATH VARCHAR(MAX)=''
@records VARCHAR(MAX)='',
 @Isformat INT =0
as
BEGIN

--IF EXISTS (SELECT * FROM temp_table) AND 
--DELETE from temp_table

IF(@Isformat =0 AND LEN(RTRIM(LTRIM(@records))) > 0)
BEGIN
INSERT temp_table
SELECT @records 
END
 
IF @Isformat =1 -- AND  NOT EXISTS (SELECT * FROM MAIN_COMMISSION )
BEGIN
	INSERT   MAIN_COMMISSION
	(
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
	Cod_Ocorrencia,
	Occurrence_code,
	Payment_method_2,
	Carrier_Bank_Number,
	Carrier_Bank_Branch_number,
	Carrier_Bank_Account_number,
	Exchange_rate_2,
	CREATED_DATE	
	)	
	 
	SELECT 
		TEMP.Payment_ID,
		TEMP.Event_Code,
		TEMP.Commission_Payment_date,
		TEMP.Commission_Paid_Amount_sign,
		TEMP.Commission_paid_amount ,
		TEMP.Payment_currency,
		TEMP.Bank_account_number,
		TEMP.Bank_branch_code,
		TEMP.No_da_Conta_Corrente,
		TEMP.Cheque_number,
		TEMP.Sinal_do_Valor_IR_2,
		TEMP.[Valor_IR_2],
		TEMP.Sinal_do_Valor_ISS_2,
		TEMP.[Valor_ISS],
		TEMP.Sinal_do_Valor_INSS,
		TEMP.Valor_INSS,
		TEMP.Sinal_do_Valor_CSLL,
		TEMP.Valor_CSLL,
		TEMP.Sinal_do_Valor_COFINS,
		TEMP.Valor_COFINS,
		TEMP.Sinal_do_Valor_PIS,
		TEMP.Valor_PIS,
		TEMP.Cod_Ocorrencia,
		TEMP.Occurrence_code,
		TEMP.Payment_method_2,
		TEMP.Carrier_Bank_Number,
		TEMP.Carrier_Bank_Branch_number,
		TEMP.Carrier_Bank_Account_number,
		TEMP.Exchange_rate_2,
		GETDATE() 
		--INTO MAIN_COMMISSION 
	FROM
		(SELECT 
			SUBSTRING([STRR],1,3)  AS 'Interface_code',
			SUBSTRING([STRR],4,10) AS 'Sequential_number',
			SUBSTRING([STRR],14,1) AS 'Beneficiary_Type',
			SUBSTRING([STRR],15,1) AS 'Foreign',
			SUBSTRING([STRR],16,3) AS 'Beneficiary_Class',
			SUBSTRING([STRR],19,60) AS 'Beneficiary_name',
			SUBSTRING([STRR],79,14) AS 'Beneficiary_ID',
			SUBSTRING([STRR],93,14) AS 'Beneficiary_foreign_ID',
			SUBSTRING([STRR],107,30) AS 'Beneficiary_Address_street_name',
			SUBSTRING([STRR],137,5) AS 'Beneficiary_Address_number',
			SUBSTRING([STRR],142,10) AS 'Beneficiary_Address_complement',
			SUBSTRING([STRR],152,30) AS 'Beneficiary_Address_district',
			SUBSTRING([STRR],182,2) AS 'Beneficiary_Address_state',
			SUBSTRING([STRR],184,30) AS 'Beneficiary_Address_city',
			SUBSTRING([STRR],214,10) AS 'Beneficiary_Address_zip_code',
			SUBSTRING([STRR],224,60) AS 'Beneficiary_ email_address',
			SUBSTRING([STRR],284,5) AS 'Beneficiary_ Bank_Number',
			SUBSTRING([STRR],289,10) AS 'Beneficiary_Bank_Branch',
			SUBSTRING([STRR],299,1) AS 'Beneficiary_Bank_Branch_Verifier_Digit',
			SUBSTRING([STRR],300,12) AS 'Beneficiary_Bank_Account_number',
			SUBSTRING([STRR],312,2) AS 'Beneficiary_Bank_Account_Verifier_Digit',
			SUBSTRING([STRR],314,2) AS 'Beneficiary_Bank_Account_type',
			SUBSTRING([STRR],316,5) AS 'Beneficiary_Bank_Account_Currency',
			SUBSTRING([STRR],321,2) AS 'Cód_Tributação_IRRF',
			SUBSTRING([STRR],323,5) AS 'Natureza_do_Rendimento',
			SUBSTRING([STRR],328,1) AS 'Calcula_ISS_?',
			SUBSTRING([STRR],329,1) AS 'Calcula_INSS_?',
			SUBSTRING([STRR],330,2) AS 'Cód_Tributação_INSS',
			SUBSTRING([STRR],332,2) AS 'Cód_Tributação_CSLL',
			SUBSTRING([STRR],334,2) AS 'Cód_Tributação_COFINS',
			SUBSTRING([STRR],336,2) AS 'Cód_Tributação_PIS',
			SUBSTRING([STRR],338,2) 'No_de_Dependentes',
			SUBSTRING([STRR],340,11) 'No_PIS',
			SUBSTRING([STRR],351,11) 'Inscrição_Municipal',
			SUBSTRING([STRR],362,10) 'Número_interno_do_corretor',
			SUBSTRING([STRR],372,10) 'CBO_Classific_Brasileira_Ocupação',
			SUBSTRING([STRR],382,14) 'Código_SUSEP',
			SUBSTRING([STRR],396,10) 'No_do_Funcionário',
			SUBSTRING([STRR],406,15) 'Cód_da_Filial',
			SUBSTRING([STRR],421,15) 'Cód_do_Centro_de_Custo',
			SUBSTRING([STRR],436,8) 'Data_de_Nascimento',
			SUBSTRING([STRR],444,1) 'Tipo_de_Pessoa',
			SUBSTRING([STRR],445,1) 'Estrangeiro',
			SUBSTRING([STRR],446,3) 'Payee_Class',
			SUBSTRING([STRR],449,60) 'Payee_Name',
			SUBSTRING([STRR],509,14) 'Payee_ID_CPF',
			SUBSTRING([STRR],523,14) 'Payee_foreign_ID',
			SUBSTRING([STRR],537,5)  'Payee_Bank_Number',
			SUBSTRING([STRR],542,10) 'Payee_Bank_Branch',
			SUBSTRING([STRR],552,1) 'Payee_Bank_Branch_Verifier_Digit',
			SUBSTRING([STRR],553,12) 'Payee_Bank_Account_no',
			SUBSTRING([STRR],565,2)  'Payee_Bank_Account_Verifier_digit',
			SUBSTRING([STRR],567,2) 'Payee_Bank_Account_Type',
			SUBSTRING([STRR],569,5) 'Payee_Bank_Account_Currency',
			SUBSTRING([STRR],574,25) 'Payment_ID',
			SUBSTRING([STRR],599,5) 'Carrier_Code',
			SUBSTRING([STRR],604,5) 'Carrier_Policy_Branch_Code',
			SUBSTRING([STRR],609,5) 'Event_Code',
			SUBSTRING([STRR],614,3) 'Operation_Type',
			SUBSTRING([STRR],617,3) 'Payment_Method',
			SUBSTRING([STRR],620,20) 'Document_number',
			SUBSTRING([STRR],640,5) 'Document_number_serial_number',
			SUBSTRING([STRR],645,8) 'Invoice_issuance_date',
			SUBSTRING([STRR],653,8) 'Invoice_due_date',
			SUBSTRING([STRR],661,5) 'Policy_Currency',
			CAST(SUBSTRING(SUBSTRING([STRR],666,15),1,8)+'.'+SUBSTRING(SUBSTRING([STRR],666,15),9,7) AS DECIMAL(18,2)) 'Exchange_rate' ,
			SUBSTRING([STRR],681,1) 'Commission_amount_sign',
			CAST(SUBSTRING(SUBSTRING([STRR],682,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],682,18),17,2) AS DECIMAL(18,2)) 'Commission_amount',
			SUBSTRING([STRR],700,1) 'Sinal_do_Valor_Isento_IR',
			CAST(SUBSTRING(SUBSTRING([STRR],701,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],701,18),17,2) AS DECIMAL(18,2))   'Valor_Isento_IR',
			SUBSTRING([STRR],719,1) 'Sinal_do_Valor_Tributável_IR',
			CAST(SUBSTRING(SUBSTRING([STRR],720,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],720,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_IR',
			SUBSTRING([STRR],738,1) 'Sinal_do_Valor_Isento_ISS',
			CAST(SUBSTRING(SUBSTRING([STRR],739,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],739,18),17,2)AS DECIMAL(18,2)) 'Valor_Isento_ISS',
			SUBSTRING([STRR],757,1) 'Sinal_do_Valor_Tributável_ISS',
			CAST(SUBSTRING(SUBSTRING([STRR],758,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],758,18),17,2) AS DECIMAL(18,2))  'Valor_Tributável_ISS',
			SUBSTRING([STRR],776,1) 'Sinal_do_Valor_Isento_INSS',
			CAST(SUBSTRING(SUBSTRING([STRR],777,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],777,18),17,2) AS DECIMAL(18,2)) 'Valor_Isento_INSS',
			SUBSTRING([STRR],795,1) 'Sinal_do_Valor_Tributável_INSS',
			CAST(SUBSTRING(SUBSTRING([STRR],796,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],777,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_INSS',
			SUBSTRING([STRR],814,1) 'Sinal_do_Valor_Isento_CSLL/COFINS/PIS',
			CAST(SUBSTRING(SUBSTRING([STRR],815,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],815,18),17,2) AS DECIMAL(18,2)) 'Valor_Isento_ CSLL/COFINS/PIS',
			SUBSTRING([STRR],833,1) 'Sinal_do_Valor_Tributável_CSLL/COFINS/PIS',
			CAST(SUBSTRING(SUBSTRING([STRR],834,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],834,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_CSLL/COFINS/PIS',
			SUBSTRING([STRR],852,1) 'Sinal_do_Valor_IR',
			CAST(SUBSTRING(SUBSTRING([STRR],853,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],853,18),17,2) AS DECIMAL(18,2))  'Valor_IR',
			SUBSTRING([STRR],871,1) 'Sinal_do_Valor_ISS',
			CAST(SUBSTRING(SUBSTRING([STRR],872,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],872,18),17,2) AS DECIMAL(18,2)) 'Valor ISS',
			SUBSTRING([STRR],890,1) 'Sinal_do_Valor_Desconto',
			CAST(SUBSTRING(SUBSTRING([STRR],891,18),1,16)+'.'+ SUBSTRING(SUBSTRING([STRR],891,18),17,2) AS DECIMAL(18,2)) 'Valor_Desconto',
			SUBSTRING([STRR],909,1) 'Sinal_do_Valor_Multa',
			CAST(SUBSTRING(SUBSTRING([STRR],910,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],910,18),17,2) AS DECIMAL(18,2)) 'Valor_Multa',
			SUBSTRING([STRR],928,60) 'Payment_Description',
			SUBSTRING([STRR],988,15) 'Policy_Branch_Code',
			SUBSTRING([STRR],1003,15) 'Profit_Center_code',
			SUBSTRING([STRR],1018,15) 'A_DEFINIR',
			SUBSTRING([STRR],1033,15) 'A_DEFINIR2',
			SUBSTRING([STRR],1048,15) 'Policy_Accounting_LOB',
			SUBSTRING([STRR],1063,15) 'A_DEFINIR3',
			SUBSTRING([STRR],1078,15) 'Policy_number' ,
			SUBSTRING([STRR],1093,10) 'Policy_Number_remaining_digits',
			SUBSTRING([STRR],1103,5) 'Endorsement_number',
			SUBSTRING([STRR],1108,5) 'Installment_number ',
			SUBSTRING([STRR],1113,15) 'A_DEFINIR4',
			SUBSTRING([STRR],1128,10) 'A_DEFINIR5',
			SUBSTRING([STRR],1138,15) 'Installment_payment_date',
			SUBSTRING([STRR],1153,60) 'Applicant/CoApplicant_name',
			CAST(SUBSTRING(SUBSTRING([STRR],1213,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1213,18),17,2) AS DECIMAL(18,2)) 'Premium_amount',
			CAST(SUBSTRING(SUBSTRING([STRR],1231,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1231,18),17,2) AS DECIMAL(18,2)) 'Commission_percentage',
			CAST(SUBSTRING(SUBSTRING([STRR],1249,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1249,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR6',
			CAST(SUBSTRING(SUBSTRING([STRR],1267,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1267,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR7',
			CAST(SUBSTRING(SUBSTRING([STRR],1285,18),1,16)+'.'+SUBSTRING(SUBSTRING([STRR],1285,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR8',
			SUBSTRING([STRR],1303,1) 'Movement_type',		
			CONVERT(DATETIME, SUBSTRING(SUBSTRING([STRR],1304,8),1,4)+'-'+SUBSTRING(SUBSTRING([STRR],1304,8),5,2)+ '-'+SUBSTRING(SUBSTRING([STRR],1304,8),7,2)+' 00:00:00') 'Commission_Payment_date',
			--SUBSTRING(SUBSTRING([STRR],1304,8),8,2) +'/'+SUBSTRING(SUBSTRING([STRR],1304,8),5,2) + SUBSTRING(SUBSTRING([STRR],1304,8), 1,4)  'Commission_Payment_date',
			SUBSTRING([STRR],1312,1) 'Commission_Paid_Amount_sign',
			CAST(SUBSTRING(SUBSTRING([STRR],1313,18),1,16) +'.'+SUBSTRING(SUBSTRING([STRR],1313,18),17,2) AS DECIMAL(18,2))  'Commission_paid_amount' ,
			SUBSTRING([STRR],1331,5) 'Payment_currency',
			SUBSTRING([STRR],1336,5) 'Bank_account_number',
			SUBSTRING([STRR],1341,10) 'Bank_branch_code',
			SUBSTRING([STRR],1351,12) 'No_da_Conta_Corrente',
			SUBSTRING([STRR],1363,15) 'Cheque_number',
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
			SUBSTRING([STRR],1492,3) 'Cod_Ocorrencia',
			SUBSTRING([STRR],1495,60) 'Occurrence_code',
			SUBSTRING([STRR],1555,3) 'Payment_method_2',
			SUBSTRING([STRR],1558,5) 'Carrier_Bank_Number',
			SUBSTRING([STRR],1563,10) 'Carrier_Bank_Branch_number',
			SUBSTRING([STRR],1573,12) 'Carrier_Bank_Account_number',
			CAST(SUBSTRING(SUBSTRING([STRR],1585,15),1,8)+'.'+SUBSTRING(SUBSTRING([STRR],1585,15),9,7) AS DECIMAL(18,2)) 'Exchange_rate_2' 

		 FROM temp_table  
	 )  TEMP --,MAIN_COMMISSION
		 WHERE TEMP.PAYMENT_ID+TEMP.EVENT_CODE  NOT IN (SELECT  MAIN_COMMISSION.Payment_ID+EVENT_CODE  FROM MAIN_COMMISSION)	
	
	 
DELETE FROM temp_table
END
END

--SELECT PAYMENT_ID,* FROM MAIN_COMMISSION
GO

