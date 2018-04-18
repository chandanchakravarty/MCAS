IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_pagnetImport_OLD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_pagnetImport_OLD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--drop table #temp_table
--DROP PROC proc_pagnetImport
create proc proc_pagnetImport_OLD
--@PATH VARCHAR(MAX)=''
@records TEXT=''
as
BEGIN
CREATE TABLE #temp_table
(
row VARCHAR(1600)
)

BULK INSERT #temp_table
FROM 'E:\Ankitfolder\COMISS_201102188.txt' 
WITH (ROWTERMINATOR = '\n') 


--INSERT 
--SELECT * FROM MAIN_COMMISSION
--DELETE FROM MAIN_COMMISSION
  
IF NOT EXISTS (SELECT * FROM MAIN_COMMISSION )
BEGIN
	INSERT   MAIN_COMMISSION
	(
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
	Exchange_rate_2
	)	
	 
	SELECT 
		Commission_Payment_date,
		Commission_Paid_Amount_sign,
		Commission_paid_amount ,
		Payment_currency,
		Bank_account_number,
		Bank_branch_code,
		No_da_Conta_Corrente,
		Cheque_number,
		Sinal_do_Valor_IR_2,
		[Valor_IR_2],
		Sinal_do_Valor_ISS_2,
		[Valor_ISS],
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
		Exchange_rate_2 
		--INTO MAIN_COMMISSION 
	FROM
		(SELECT 
			SUBSTRING([row],1,3)  AS 'Interface_code',
			SUBSTRING([row],4,10) AS 'Sequential_number',
			SUBSTRING([row],14,1) AS 'Beneficiary_Type',
			SUBSTRING([row],15,1) AS 'Foreign',
			SUBSTRING([row],16,3) AS 'Beneficiary_Class',
			SUBSTRING([row],19,60) AS 'Beneficiary_name',
			SUBSTRING([row],79,14) AS 'Beneficiary_ID',
			SUBSTRING([row],93,14) AS 'Beneficiary_foreign_ID',
			SUBSTRING([row],107,30) AS 'Beneficiary_Address_street_name',
			SUBSTRING([row],137,5) AS 'Beneficiary_Address_number',
			SUBSTRING([row],142,10) AS 'Beneficiary_Address_complement',
			SUBSTRING([row],152,30) AS 'Beneficiary_Address_district',
			SUBSTRING([row],182,2) AS 'Beneficiary_Address_state',
			SUBSTRING([row],184,30) AS 'Beneficiary_Address_city',
			SUBSTRING([row],214,10) AS 'Beneficiary_Address_zip_code',
			SUBSTRING([row],224,60) AS 'Beneficiary_ email_address',
			SUBSTRING([row],284,5) AS 'Beneficiary_ Bank_Number',
			SUBSTRING([row],289,10) AS 'Beneficiary_Bank_Branch',
			SUBSTRING([row],299,1) AS 'Beneficiary_Bank_Branch_Verifier_Digit',
			SUBSTRING([row],300,12) AS 'Beneficiary_Bank_Account_number',
			SUBSTRING([row],312,2) AS 'Beneficiary_Bank_Account_Verifier_Digit',
			SUBSTRING([row],314,2) AS 'Beneficiary_Bank_Account_type',
			SUBSTRING([row],316,5) AS 'Beneficiary_Bank_Account_Currency',
			SUBSTRING([row],321,2) AS 'Cód_Tributação_IRRF',
			SUBSTRING([row],323,5) AS 'Natureza_do_Rendimento',
			SUBSTRING([row],328,1) AS 'Calcula_ISS_?',
			SUBSTRING([row],329,1) AS 'Calcula_INSS_?',
			SUBSTRING([row],330,2) AS 'Cód_Tributação_INSS',
			SUBSTRING([row],332,2) AS 'Cód_Tributação_CSLL',
			SUBSTRING([row],334,2) AS 'Cód_Tributação_COFINS',
			SUBSTRING([row],336,2) AS 'Cód_Tributação_PIS',
			SUBSTRING([row],338,2) 'No_de_Dependentes',
			SUBSTRING([row],340,11) 'No_PIS',
			SUBSTRING([row],351,11) 'Inscrição_Municipal',
			SUBSTRING([row],362,10) 'Número_interno_do_corretor',
			SUBSTRING([row],372,10) 'CBO_Classific_Brasileira_Ocupação',
			SUBSTRING([row],382,14) 'Código_SUSEP',
			SUBSTRING([row],396,10) 'No_do_Funcionário',
			SUBSTRING([row],406,15) 'Cód_da_Filial',
			SUBSTRING([row],421,15) 'Cód_do_Centro_de_Custo',
			SUBSTRING([row],436,8) 'Data_de_Nascimento',
			SUBSTRING([row],444,1) 'Tipo_de_Pessoa',
			SUBSTRING([row],445,1) 'Estrangeiro',
			SUBSTRING([row],446,3) 'Payee_Class',
			SUBSTRING([row],449,60) 'Payee_Name',
			SUBSTRING([row],509,14) 'Payee_ID_CPF',
			SUBSTRING([row],523,14) 'Payee_foreign_ID',
			SUBSTRING([row],537,5)  'Payee_Bank_Number',
			SUBSTRING([row],542,10) 'Payee_Bank_Branch',
			SUBSTRING([row],552,1) 'Payee_Bank_Branch_Verifier_Digit',
			SUBSTRING([row],553,12) 'Payee_Bank_Account_no',
			SUBSTRING([row],565,2)  'Payee_Bank_Account_Verifier_digit',
			SUBSTRING([row],567,2) 'Payee_Bank_Account_Type',
			SUBSTRING([row],569,5) 'Payee_Bank_Account_Currency',
			SUBSTRING([row],574,25) 'Payment_ID',
			SUBSTRING([row],599,5) 'Carrier_Code',
			SUBSTRING([row],604,5) 'Carrier_Policy_Branch_Code',
			SUBSTRING([row],609,5) 'Event_Code',
			SUBSTRING([row],614,3) 'Operation_Type',
			SUBSTRING([row],617,3) 'Payment_Method',
			SUBSTRING([row],620,20) 'Document_number',
			SUBSTRING([row],640,5) 'Document_number_serial_number',
			SUBSTRING([row],645,8) 'Invoice_issuance_date',
			SUBSTRING([row],653,8) 'Invoice_due_date',
			SUBSTRING([row],661,5) 'Policy_Currency',
			CAST(SUBSTRING(SUBSTRING([row],666,15),1,8)+'.'+SUBSTRING(SUBSTRING([row],666,15),9,7) AS DECIMAL(18,2)) 'Exchange_rate' ,
			SUBSTRING([row],681,1) 'Commission_amount_sign',
			CAST(SUBSTRING(SUBSTRING([row],682,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],682,18),17,2) AS DECIMAL(18,2)) 'Commission_amount',
			SUBSTRING([row],700,1) 'Sinal_do_Valor_Isento_IR',
			CAST(SUBSTRING(SUBSTRING([row],701,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],701,18),17,2) AS DECIMAL(18,2))   'Valor_Isento_IR',
			SUBSTRING([row],719,1) 'Sinal_do_Valor_Tributável_IR',
			CAST(SUBSTRING(SUBSTRING([row],720,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],720,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_IR',
			SUBSTRING([row],738,1) 'Sinal_do_Valor_Isento_ISS',
			CAST(SUBSTRING(SUBSTRING([row],739,18),1,16)+'.'+ SUBSTRING(SUBSTRING([row],739,18),17,2)AS DECIMAL(18,2)) 'Valor_Isento_ISS',
			SUBSTRING([row],757,1) 'Sinal_do_Valor_Tributável_ISS',
			CAST(SUBSTRING(SUBSTRING([row],758,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],758,18),17,2) AS DECIMAL(18,2))  'Valor_Tributável_ISS',
			SUBSTRING([row],776,1) 'Sinal_do_Valor_Isento_INSS',
			CAST(SUBSTRING(SUBSTRING([row],777,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],777,18),17,2) AS DECIMAL(18,2)) 'Valor_Isento_INSS',
			SUBSTRING([row],795,1) 'Sinal_do_Valor_Tributável_INSS',
			CAST(SUBSTRING(SUBSTRING([row],796,18),1,16)+'.'+ SUBSTRING(SUBSTRING([row],777,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_INSS',
			SUBSTRING([row],814,1) 'Sinal_do_Valor_Isento_CSLL/COFINS/PIS',
			CAST(SUBSTRING(SUBSTRING([row],815,18),1,16)+'.'+ SUBSTRING(SUBSTRING([row],815,18),17,2) AS DECIMAL(18,2)) 'Valor_Isento_ CSLL/COFINS/PIS',
			SUBSTRING([row],833,1) 'Sinal_do_Valor_Tributável_CSLL/COFINS/PIS',
			CAST(SUBSTRING(SUBSTRING([row],834,18),1,16)+'.'+ SUBSTRING(SUBSTRING([row],834,18),17,2) AS DECIMAL(18,2)) 'Valor_Tributável_CSLL/COFINS/PIS',
			SUBSTRING([row],852,1) 'Sinal_do_Valor_IR',
			CAST(SUBSTRING(SUBSTRING([row],853,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],853,18),17,2) AS DECIMAL(18,2))  'Valor_IR',
			SUBSTRING([row],871,1) 'Sinal_do_Valor_ISS',
			CAST(SUBSTRING(SUBSTRING([row],872,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],872,18),17,2) AS DECIMAL(18,2)) 'Valor ISS',
			SUBSTRING([row],890,1) 'Sinal_do_Valor_Desconto',
			CAST(SUBSTRING(SUBSTRING([row],891,18),1,16)+'.'+ SUBSTRING(SUBSTRING([row],891,18),17,2) AS DECIMAL(18,2)) 'Valor_Desconto',
			SUBSTRING([row],909,1) 'Sinal_do_Valor_Multa',
			CAST(SUBSTRING(SUBSTRING([row],910,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],910,18),17,2) AS DECIMAL(18,2)) 'Valor_Multa',
			SUBSTRING([row],928,60) 'Payment_Description',
			SUBSTRING([row],988,15) 'Policy_Branch_Code',
			SUBSTRING([row],1003,15) 'Profit_Center_code',
			SUBSTRING([row],1018,15) 'A_DEFINIR',
			SUBSTRING([row],1033,15) 'A_DEFINIR2',
			SUBSTRING([row],1048,15) 'Policy_Accounting_LOB',
			SUBSTRING([row],1063,15) 'A_DEFINIR3',
			SUBSTRING([row],1078,15) 'Policy_number' ,
			SUBSTRING([row],1093,10) 'Policy_Number_remaining_digits',
			SUBSTRING([row],1103,5) 'Endorsement_number',
			SUBSTRING([row],1108,5) 'Installment_number ',
			SUBSTRING([row],1113,15) 'A_DEFINIR4',
			SUBSTRING([row],1128,10) 'A_DEFINIR5',
			SUBSTRING([row],1138,15) 'Installment_payment_date',
			SUBSTRING([row],1153,60) 'Applicant/CoApplicant_name',
			CAST(SUBSTRING(SUBSTRING([row],1213,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1213,18),17,2) AS DECIMAL(18,2)) 'Premium_amount',
			CAST(SUBSTRING(SUBSTRING([row],1231,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1231,18),17,2) AS DECIMAL(18,2)) 'Commission_percentage',
			CAST(SUBSTRING(SUBSTRING([row],1249,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1249,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR6',
			CAST(SUBSTRING(SUBSTRING([row],1267,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1267,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR7',
			CAST(SUBSTRING(SUBSTRING([row],1285,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1285,18),17,2) AS DECIMAL(18,2)) 'A_DEFINIR8',
			SUBSTRING([row],1303,1) 'Movement_type',
			SUBSTRING([row],1304,8) 'Commission_Payment_date',
			SUBSTRING([row],1312,1) 'Commission_Paid_Amount_sign',
			CAST(SUBSTRING(SUBSTRING([row],1313,18),1,16) +'.'+SUBSTRING(SUBSTRING([row],1313,18),17,2) AS DECIMAL(18,2))  'Commission_paid_amount' ,
			SUBSTRING([row],1331,5) 'Payment_currency',
			SUBSTRING([row],1336,5) 'Bank_account_number',
			SUBSTRING([row],1341,10) 'Bank_branch_code',
			SUBSTRING([row],1351,12) 'No_da_Conta_Corrente',
			SUBSTRING([row],1363,15) 'Cheque_number',
			SUBSTRING([row],1378,1) 'Sinal_do_Valor_IR_2',
			CAST(SUBSTRING(SUBSTRING([row],1379,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1379,18),17,2) AS DECIMAL(18,2)) 'Valor_IR_2',
			SUBSTRING([row],1397,1) 'Sinal_do_Valor_ISS_2',
			CAST(SUBSTRING(SUBSTRING([row],1398,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1398,18),17,2) AS DECIMAL(18,2)) 'Valor_ISS',
			SUBSTRING([row],1416,1) 'Sinal_do_Valor_INSS',
			CAST(SUBSTRING(SUBSTRING([row],1417,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1417,18),17,2) AS DECIMAL(18,2)) 'Valor_INSS',
			SUBSTRING([row],1435,1) 'Sinal_do_Valor_CSLL' ,
			CAST(SUBSTRING(SUBSTRING([row],1436,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1436,18),17,2) AS DECIMAL(18,2)) 'Valor_CSLL',
			SUBSTRING([row],1454,1) 'Sinal_do_Valor_COFINS',
			CAST(SUBSTRING(SUBSTRING([row],1455,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1455,18),17,2) AS DECIMAL(18,2)) 'Valor_COFINS',
			SUBSTRING([row],1473,1) 'Sinal_do_Valor_PIS',
			CAST(SUBSTRING(SUBSTRING([row],1474,18),1,16)+'.'+SUBSTRING(SUBSTRING([row],1474,18),17,2) AS DECIMAL(18,2)) 'Valor_PIS',
			SUBSTRING([row],1492,3) 'Cod_Ocorrencia',
			SUBSTRING([row],1495,60) 'Occurrence_code',
			SUBSTRING([row],1555,3) 'Payment_method_2',
			SUBSTRING([row],1558,5) 'Carrier_Bank_Number',
			SUBSTRING([row],1563,10) 'Carrier_Bank_Branch_number',
			SUBSTRING([row],1573,12) 'Carrier_Bank_Account_number',
			CAST(SUBSTRING(SUBSTRING([row],1585,15),1,8)+'.'+SUBSTRING(SUBSTRING([row],1585,15),9,7) AS DECIMAL(18,2)) 'Exchange_rate_2' 

		 FROM #temp_table
	 )  TEMP	 
	 
DROP TABLE #temp_table
END
END

--SELECT * FROM #temp_table
GO

