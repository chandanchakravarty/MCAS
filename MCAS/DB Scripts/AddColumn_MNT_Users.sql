  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'DID_No')
     BEGIN
          ALTER TABLE  MNT_Users ADD DID_No varchar(20) NULL
     END
  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'FAX_No')
     BEGIN
           ALTER TABLE  MNT_Users ADD FAX_No varchar(20) NULL
     END