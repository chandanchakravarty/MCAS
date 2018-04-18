IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_MandateSummary' and column_name = 'ProposedLiability')
BEGIN
Alter table CLM_MandateSummary Add ProposedLiability numeric(18,2)
END