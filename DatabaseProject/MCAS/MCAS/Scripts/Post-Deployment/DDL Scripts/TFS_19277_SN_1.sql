IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ClaimOfficer') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ClaimOfficer 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'DriversLiability') 
BEGIN
Alter table CLM_Claims DROP COLUMN  DriversLiability 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'PaidDate') 
BEGIN
Alter table CLM_Claims DROP COLUMN  PaidDate 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'BalanceLOG') 
BEGIN
Alter table CLM_Claims DROP COLUMN  BalanceLOG 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'LOGAmount') 
BEGIN
Alter table CLM_Claims DROP COLUMN  LOGAmount 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'LOURate') 
BEGIN
Alter table CLM_Claims DROP COLUMN  LOURate 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'LOUDays') 
BEGIN
Alter table CLM_Claims DROP COLUMN  LOUDays 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ReportSentInsurer') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ReportSentInsurer 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ReferredInsurers') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ReferredInsurers 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'InformInsurer') 
BEGIN
Alter table CLM_Claims DROP COLUMN  InformInsurer 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ReserveCurr') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ReserveCurr 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ReserveExRate') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ReserveExRate 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ReserveAmt') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ReserveAmt 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ExpensesCurr') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ExpensesCurr 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ExpensesExRate') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ExpensesExRate 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'TotalReserve') 
BEGIN
Alter table CLM_Claims DROP COLUMN  TotalReserve 
END



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ExpensesAmt') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ExpensesAmt 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'PayableTo') 
BEGIN
Alter table CLM_Claims DROP COLUMN  PayableTo 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ClaimAmountCurr') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ClaimAmountCurr 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ClaimAmtPayout') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ClaimAmtPayout 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ClaimAmtPayoutExRate') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ClaimAmtPayoutExRate 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ExpensesAmount') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ExpensesAmount 
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ReserveAmount') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ReserveAmount 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'RecordDeletionDate') 
BEGIN
Alter table CLM_Claims DROP COLUMN  RecordDeletionDate 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'RecordDeletionReason') 
BEGIN
Alter table CLM_Claims DROP COLUMN  RecordDeletionReason 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ReferredToInsurers') 
BEGIN
Alter table CLM_Claims DROP COLUMN  ReferredToInsurers 
END