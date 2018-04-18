IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_CurrencyTxn')

BEGIN

 Delete MNT_CurrencyTxn

END






IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_CurrencyM')

BEGIN

 Delete MNT_CurrencyM

END
