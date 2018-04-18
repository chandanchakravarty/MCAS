/*---------------Insert Script For Login -------------*/

INSERT INTO [MNT_Users]
           ([UserId]
           ,[LoginPassword]
           ,[GroupId]
           ,[UserFullName]
           ,[UserDispName]
           ,[DeptCode]
           ,[BranchCode]
           ,[PaymentLimit]
           ,[CreditNoteLimit]
           ,[IsActive]
           ,[IsEnabled]
           ,[UserTypeCode]
           ,[AccessLevel]
           ,[FirstTime]
           ,[MainClass]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           ('pravesh'
           ,'DkOjLH0t+Oc='
           ,'G0001'
           ,'Pravesh Kumar'
           ,'Pravesh'
           ,'ADM'
           ,'01'
           ,'10000000.0000'
           ,'10000000.0000'
           ,'Y'
           ,NULL
           ,NULL
           ,0
           ,NULL
           ,NULL
           ,NULL
           ,'2014-06-02 11:42:01.987'
           ,NULL
           ,'2014-06-02 11:42:01.987')
GO

/* ----------Update Scripts for User Contry -----------*/

update MNT_UserCountryProducts
set UserId='pravesh'
where UserId='sanjay'
