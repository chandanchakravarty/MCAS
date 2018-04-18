  
  

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (49
           ,'Claims Notification received, Bus Captain(BC) at fault? Yes/No'
           ,Null
           ,'D')
GO

update [dbo].[TODODIARYLIST] set listtypeid = 49 

delete from [dbo].[TODODIARYLISTTYPES] where typeid <> 49
  
  INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (1
           ,'Claims Notification received, Bus Captain(BC) at fault? Yes/No'
           ,Null
           ,'D')
GO

  update [dbo].[TODODIARYLIST] set listtypeid = 1 
  delete from [dbo].[TODODIARYLISTTYPES] where typeid = 49

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (2
           ,'Pending Documents'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (3
           ,'Case Assignment'
           ,Null
           ,'D')
GO


INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (4
           ,'LOD Sent'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (5
           ,'Set Case Review Date'
           ,Null
           ,'D')
GO


INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (6
           ,'Settlement Reached'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (7
           ,'Payment Received'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (8
           ,'Case Assignment'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (9
           ,'Property Claims'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (10
           ,'Injury Claims – Claims Amount more than $15k'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (11
           ,'Injury Claims – Claims Amount less than $15k'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (12
           ,'Update Claim Development'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (13
           ,'Review Of Claim'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (14
           ,'Writ of Summons (WOS) Received'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (15
           ,'Case Resolved. Payment Required'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (16
           ,'Total Payment >15K'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (17
           ,'Debit Note Approved by SV'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (18
           ,'Payment Processing'
           ,Null
           ,'D')
GO

INSERT INTO [dbo].[TODODIARYLISTTYPES]
           ([TYPEID]
           ,[TYPEDESC]
           ,[SYSDEFAULT]
           ,[TYPE_FLAG])
     VALUES
           (19
           ,'Payment Settlement'
           ,Null
           ,'D')
GO



