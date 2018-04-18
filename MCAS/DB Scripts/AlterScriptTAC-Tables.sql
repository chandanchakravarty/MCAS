	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_ACC_REP' and column_name = 'ErrorMessage')
     BEGIN
         alter Table STG_TAC_ACC_REP alter column ErrorMessage nvarchar(400)
     END
     
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'ErrorMessage')
     BEGIN
         alter Table STG_TAC_IP alter column ErrorMessage nvarchar(400)
     END     

	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP_BUS' and column_name = 'ErrorMessage')
     BEGIN
         alter Table STG_TAC_IP_BUS alter column ErrorMessage nvarchar(400)
     END          