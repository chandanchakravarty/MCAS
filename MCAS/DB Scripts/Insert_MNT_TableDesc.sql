    IF NOT EXISTS(SELECT * FROM MNT_TableDesc WHERE TableName='CLM_LogRequest')
     BEGIN
           Insert into MNT_TableDesc values('CLM_LogRequest','Log Request','Claims')
     END
