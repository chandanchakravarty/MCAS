
    IF NOT EXISTS 
    (   SELECT  * from MNT_Lookups where Description='Claimant'  AND Category = 'PTO'
    )
    BEGIN
        INSERT INTO [MNT_Lookups] ([Lookupvalue] ,[Lookupdesc] ,[Description] ,[Category]) VALUES ('CLMT' ,'Claimant' ,'Claimant' ,'PTO')

    END
    IF NOT EXISTS 
    (   SELECT  * from MNT_Lookups where Description='Third Party Adjuster'  AND Category = 'PTO'
    )
    BEGIN
    INSERT INTO [MNT_Lookups] ([Lookupvalue] ,[Lookupdesc] ,[Description] ,[Category]) VALUES ('TPA' ,'Third Party Adjuster' ,'Third Party Adjuster' ,'PTO')
    END
    IF NOT EXISTS 
    (   SELECT  * from MNT_Lookups where Description='Third Party Lawyer'  AND Category = 'PTO'
    )
    BEGIN
       INSERT INTO [MNT_Lookups] ([Lookupvalue] ,[Lookupdesc] ,[Description] ,[Category]) VALUES ('TPL' ,'Third Party Lawyer' ,'Third Party Lawyer' ,'PTO')

    END
    IF NOT EXISTS 
    (   SELECT  * from MNT_Lookups where Description='Third Party workshop'  AND Category = 'PTO'
    )
    BEGIN
        INSERT INTO [MNT_Lookups] ([Lookupvalue] ,[Lookupdesc] ,[Description] ,[Category]) VALUES ('TPW' ,'Third Party workshop' ,'Third Party workshop' ,'PTO')

    END