IF NOT EXISTS (select * from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 14970)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES
	   (
		    LOOKUP_UNIQUE_ID
           ,LOOKUP_ID
           ,LOOKUP_VALUE_ID
           ,LOOKUP_VALUE_CODE
           ,LOOKUP_VALUE_DESC
            ,IS_ACTIVE
           )
     VALUES
           (
			 14970 
			,1450
			,1
           ,'1'
           ,'Gas'
            ,'Y'
           )
END
Go
IF NOT EXISTS (select * from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 14971)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES
	   (
		    LOOKUP_UNIQUE_ID
           ,LOOKUP_ID
           ,LOOKUP_VALUE_ID
           ,LOOKUP_VALUE_CODE
           ,LOOKUP_VALUE_DESC
            ,IS_ACTIVE
           )
     VALUES
           (
			 14971 
			,1449
			,1
           ,'1'
           ,'Central'
            ,'Y'
           )
END
Go
IF NOT EXISTS (select * from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 14972)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES
	   (
		    LOOKUP_UNIQUE_ID
           ,LOOKUP_ID
           ,LOOKUP_VALUE_ID
           ,LOOKUP_VALUE_CODE
           ,LOOKUP_VALUE_DESC
            ,IS_ACTIVE
           )
     VALUES
           (
			 14972 
			,1448
			,1
           ,'1'
           ,'PVC'
            ,'Y'
           )
END
GO

IF NOT EXISTS (select * from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 14973)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES
	   (
		    LOOKUP_UNIQUE_ID
           ,LOOKUP_ID
           ,LOOKUP_VALUE_ID
           ,LOOKUP_VALUE_CODE
           ,LOOKUP_VALUE_DESC
            ,IS_ACTIVE
           )
     VALUES
           (
			 14973
			,1447
			,1
           ,'1'
           ,'Composite'
            ,'Y'
           )
END

GO