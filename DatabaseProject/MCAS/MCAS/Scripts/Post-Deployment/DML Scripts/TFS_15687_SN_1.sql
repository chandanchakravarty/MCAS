if not exists(select * from MNT_Lookups where Category='TranComponent' and Lookupvalue='NOD')
Begin

INSERT INTO [dbo].[MNT_Lookups]([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate])
VALUES (N'NOD', N'Noofdays', N'Noofdays', N'TranComponent', N'N', NULL, NULL, NULL, NULL, NULL)
End