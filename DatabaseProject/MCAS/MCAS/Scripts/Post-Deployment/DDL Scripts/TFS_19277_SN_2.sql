IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'InvoiceNo') 
BEGIN 
Alter Table CLM_Claims
Add InvoiceNo nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'JobNo') 
BEGIN 
Alter Table CLM_Claims
Add JobNo nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'CustomerCode') 
BEGIN 
Alter Table CLM_Claims
Add CustomerCode nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'BusinessArea') 
BEGIN 
Alter Table CLM_Claims
Add BusinessArea nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'TPVehicleNo') 
BEGIN 
Alter Table CLM_Claims
Add TPVehicleNo nvarchar(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'LawerRef') 
BEGIN 
Alter Table CLM_Claims
Add LawerRef nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'OwnLawyer') 
BEGIN 
Alter Table CLM_Claims
Add OwnLawyer nvarchar(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'TPInsurer') 
BEGIN 
Alter Table CLM_Claims
Add TPInsurer nvarchar(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'TPRef') 
BEGIN 
Alter Table CLM_Claims
Add TPRef nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'LawyerGIADRM') 
BEGIN 
Alter Table CLM_Claims
Add LawyerGIADRM nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'LawyerDate') 
BEGIN 
Alter Table CLM_Claims
Add LawyerDate datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'DateToGIADRM') 
BEGIN 
Alter Table CLM_Claims
Add DateToGIADRM datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'NatureOfAcc') 
BEGIN 
Alter Table CLM_Claims
Add NatureOfAcc nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'Collisiontype') 
BEGIN 
Alter Table CLM_Claims
Add Collisiontype nvarchar(10)
END 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'NoOfDaysForRepairs') 
BEGIN 
Alter Table CLM_Claims
Add NoOfDaysForRepairs decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'DateOfSurvey') 
BEGIN 
Alter Table CLM_Claims
Add DateOfSurvey datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'Invoicedate') 
BEGIN 
Alter Table CLM_Claims
Add Invoicedate datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'Gst') 
BEGIN 
Alter Table CLM_Claims
Add Gst nvarchar(3) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'WsActRcvr') 
BEGIN 
Alter Table CLM_Claims
Add WsActRcvr nvarchar(3) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ConfirmDate') 
BEGIN 
Alter Table CLM_Claims
Add ConfirmDate datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'FileArchievedRef') 
BEGIN 
Alter Table CLM_Claims
Add FileArchievedRef nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'FileReceivedDate') 
BEGIN 
Alter Table CLM_Claims
Add FileReceivedDate datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'CDGEStatus') 
BEGIN 
Alter Table CLM_Claims
Add CDGEStatus nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'FollowUpDate') 
BEGIN 
Alter Table CLM_Claims
Add FollowUpDate datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'AdminSupport') 
BEGIN 
Alter Table CLM_Claims
Add AdminSupport nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'SettledBy') 
BEGIN 
Alter Table CLM_Claims
Add SettledBy nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'InvoiceAmt') 
BEGIN 
Alter Table CLM_Claims
Add InvoiceAmt decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ClaimAmt') 
BEGIN 
Alter Table CLM_Claims
Add ClaimAmt decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ActualAmt') 
BEGIN 
Alter Table CLM_Claims
Add ActualAmt decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ConfirmAmt') 
BEGIN 
Alter Table CLM_Claims
Add ConfirmAmt decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'SettledAmt') 
BEGIN 
Alter Table CLM_Claims
Add SettledAmt decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'PaymentDetails') 
BEGIN 
Alter Table CLM_Claims
Add PaymentDetails nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ChequeDetails') 
BEGIN 
Alter Table CLM_Claims
Add ChequeDetails nvarchar(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ContractorInvoiceNo') 
BEGIN 
Alter Table CLM_Claims
Add ContractorInvoiceNo nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'Sharellocation') 
BEGIN 
Alter Table CLM_Claims
Add Sharellocation nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'WSONo') 
BEGIN 
Alter Table CLM_Claims
Add WSONo nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'WsoInvoiceAmt') 
BEGIN 
Alter Table CLM_Claims
Add WsoInvoiceAmt decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'WsoCnNo') 
BEGIN 
Alter Table CLM_Claims
Add WsoCnNo nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'WsoCNAmt') 
BEGIN 
Alter Table CLM_Claims
Add WsoCNAmt decimal(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'CaseTypeL1') 
BEGIN 
Alter Table CLM_Claims
Add CaseTypeL1 nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'CaseTypeL2') 
BEGIN 
Alter Table CLM_Claims
Add CaseTypeL2 nvarchar(10) 
END