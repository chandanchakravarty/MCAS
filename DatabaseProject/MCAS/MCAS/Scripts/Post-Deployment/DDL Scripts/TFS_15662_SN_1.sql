  --------     Drop Constraint before run below script-------
IF OBJECT_ID('FK_MNT_CurrencyTxn_MNT_CurrencyM') IS NOT NULL
  BEGIN
    ALTER TABLE [dbo].[MNT_CurrencyTxn] DROP CONSTRAINT [FK_MNT_CurrencyTxn_MNT_CurrencyM]
  END 
IF OBJECT_ID('PK_MNT_CurrencyM') IS NOT NULL
  BEGIN
    ALTER TABLE MNT_CurrencyM DROP CONSTRAINT PK_MNT_CurrencyM 
  END
IF OBJECT_ID('IX_MNT_Department_DeptName') IS NOT NULL
  BEGIN
    ALTER TABLE MNT_Department DROP CONSTRAINT IX_MNT_Department_DeptName
  END
IF OBJECT_ID('IX_MNT_Users_UserId') IS NOT NULL
  BEGIN
    ALTER TABLE MNT_Users DROP CONSTRAINT IX_MNT_Users_UserId 
  END
  --------------------------------------------------------------
  
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'Claim_Registration' and column_name = 'Remarks')
     BEGIN
         ALTER TABLE dbo.Claim_Registration ALTER COLUMN Remarks [nvarchar](500) NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'Claim_Registration' and column_name = 'ReportedBy')
     BEGIN
         ALTER TABLE dbo.Claim_Registration ALTER COLUMN ReportedBy [nvarchar](80) NULL
     END
---     
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_BranchType' and column_name = 'BranchTypeDesc')
     BEGIN
         ALTER TABLE dbo.MNT_BranchType ALTER COLUMN BranchTypeDesc [nvarchar](50) NULL
     END
---     
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_BusCaptain' and column_name = 'NRICPassportNo')
     BEGIN
         ALTER TABLE dbo.MNT_BusCaptain ALTER COLUMN NRICPassportNo [nvarchar](50) NULL
     END 
---     
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'Remarks')
     BEGIN
         ALTER TABLE dbo.MNT_Cedant ALTER COLUMN Remarks [nvarchar](800) NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'State')
     BEGIN
         ALTER TABLE dbo.MNT_Cedant ALTER COLUMN State [nvarchar](100) NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'FirstContactPersonName')
     BEGIN
         ALTER TABLE dbo.MNT_Cedant ALTER COLUMN FirstContactPersonName [nvarchar](250) NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'EmailAddress1')
     BEGIN
         ALTER TABLE dbo.MNT_Cedant ALTER COLUMN EmailAddress1 [nvarchar](100) NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'SecondContactPersonName')
     BEGIN
         ALTER TABLE dbo.MNT_Cedant ALTER COLUMN SecondContactPersonName [nvarchar](250) NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'EmailAddress2')
     BEGIN
         ALTER TABLE dbo.MNT_Cedant ALTER COLUMN EmailAddress2 [nvarchar](100) NULL
     END 
--- 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ClaimClosed' and column_name = 'CloseDesc')
     BEGIN
         ALTER TABLE dbo.MNT_ClaimClosed ALTER COLUMN CloseDesc [nvarchar](500) NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ClaimExpense' and column_name = 'ClaimExpenseDesc')
     BEGIN
         ALTER TABLE dbo.MNT_ClaimExpense ALTER COLUMN ClaimExpenseDesc [nvarchar](50) NOT NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ClaimReOpened' and column_name = 'ReopenDesc')
     BEGIN
         ALTER TABLE dbo.MNT_ClaimReOpened ALTER COLUMN ReopenDesc [nvarchar](500)  NULL
     END    
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_CurrencyM' and column_name = 'CurrencyCode')
     BEGIN
         ALTER TABLE dbo.MNT_CurrencyM ALTER COLUMN CurrencyCode [nvarchar](20)  NOT NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_CurrencyM' and column_name = 'CurrencyDispCode')
     BEGIN
         ALTER TABLE dbo.MNT_CurrencyM ALTER COLUMN CurrencyDispCode [nvarchar](20)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_CurrencyM' and column_name = 'Description')
     BEGIN
         ALTER TABLE dbo.MNT_CurrencyM ALTER COLUMN Description [nvarchar](50)  NULL
     END
--
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Department' and column_name = 'DeptCode')
     BEGIN
         ALTER TABLE dbo.MNT_Department ALTER COLUMN DeptCode [nvarchar](10)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Department' and column_name = 'DeptName')
     BEGIN
         ALTER TABLE dbo.MNT_Department ALTER COLUMN DeptName [nvarchar](50)  NULL
     END
------
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'DepotReference')
     BEGIN
         ALTER TABLE dbo.MNT_DepotMaster ALTER COLUMN DepotReference [nvarchar](50) NOT NULL
     END
     
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'City')
     BEGIN
         ALTER TABLE dbo.MNT_DepotMaster ALTER COLUMN City [nvarchar](50)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'State')
     BEGIN
         ALTER TABLE dbo.MNT_DepotMaster ALTER COLUMN State [nvarchar](50)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'Country')
     BEGIN
         ALTER TABLE dbo.MNT_DepotMaster ALTER COLUMN Country [nvarchar](50)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'Email')
     BEGIN
         ALTER TABLE dbo.MNT_DepotMaster ALTER COLUMN Email [nvarchar](50)  NULL
     END     
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'PersonInCharge')
     BEGIN
         ALTER TABLE dbo.MNT_DepotMaster ALTER COLUMN PersonInCharge [nvarchar](50)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'EmailAddress2')
     BEGIN
         ALTER TABLE dbo.MNT_DepotMaster ALTER COLUMN EmailAddress2 [nvarchar](50)  NULL
     END
---------
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_EMAIL' and column_name = 'NAME')
     BEGIN
         ALTER TABLE dbo.MNT_EMAIL ALTER COLUMN NAME [nvarchar](100)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_EMAIL' and column_name = 'EMAIL')
     BEGIN
         ALTER TABLE dbo.MNT_EMAIL ALTER COLUMN EMAIL [nvarchar](100)  NULL
     END
---
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GroupsMaster' and column_name = 'GroupCode')
     BEGIN
         ALTER TABLE dbo.MNT_GroupsMaster ALTER COLUMN GroupCode [nvarchar](5) NOT NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GroupsMaster' and column_name = 'GroupName')
     BEGIN
         ALTER TABLE dbo.MNT_GroupsMaster ALTER COLUMN GroupName [nvarchar](100)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GroupsMaster' and column_name = 'DeptCode')
     BEGIN
         ALTER TABLE dbo.MNT_GroupsMaster ALTER COLUMN DeptCode [nvarchar](10)  NULL
     END
--
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GST' and column_name = 'GSTType')
     BEGIN
         ALTER TABLE dbo.MNT_GST ALTER COLUMN GSTType [nvarchar](2) NOT NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GST' and column_name = 'GSTDesc')
     BEGIN
         ALTER TABLE dbo.MNT_GST ALTER COLUMN GSTDesc [nvarchar](50) NOT NULL
     END
 ---  
 IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_InsruanceM' and column_name = 'CurrencyCode')
     BEGIN
         ALTER TABLE dbo.MNT_InsruanceM ALTER COLUMN CurrencyCode [nvarchar](20)  NULL
     END 
 ----
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'Lookupvalue')
     BEGIN
         ALTER TABLE dbo.MNT_Lookups ALTER COLUMN Lookupvalue [nvarchar](20)  NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'Lookupdesc')
     BEGIN
         ALTER TABLE dbo.MNT_Lookups ALTER COLUMN Lookupdesc [nvarchar](50)  NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'Description')
     BEGIN
         ALTER TABLE dbo.MNT_Lookups ALTER COLUMN Description [nvarchar](50)  NULL
     END 
---
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ProductClass' and column_name = 'LloydCode')
     BEGIN
         ALTER TABLE dbo.MNT_ProductClass ALTER COLUMN LloydCode [nvarchar](10)  NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ProductClass' and column_name = 'Remarks')
     BEGIN
         ALTER TABLE dbo.MNT_ProductClass ALTER COLUMN Remarks [nvarchar](300)  NULL
     END 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ProductClass' and column_name = 'BinderRefNo')
     BEGIN
         ALTER TABLE dbo.MNT_ProductClass ALTER COLUMN BinderRefNo [nvarchar](50)  NULL
     END 
     
 ------
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ProductsCountry' and column_name = 'Product_Code')
     BEGIN
         ALTER TABLE dbo.MNT_ProductsCountry ALTER COLUMN Product_Code [nvarchar](6) NOT NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ProductsCountry' and column_name = 'CountryCode')
     BEGIN
         ALTER TABLE dbo.MNT_ProductsCountry ALTER COLUMN CountryCode [nvarchar](5)  NULL
     END
--
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'UserId')
     BEGIN
         ALTER TABLE dbo.MNT_Users ALTER COLUMN UserId [nvarchar](20) NOT NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'LoginPassword')
     BEGIN
         ALTER TABLE dbo.MNT_Users ALTER COLUMN LoginPassword [nvarchar](50)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'UserFullName')
     BEGIN
         ALTER TABLE dbo.MNT_Users ALTER COLUMN UserFullName [nvarchar](100)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'UserDispName')
     BEGIN
         ALTER TABLE dbo.MNT_Users ALTER COLUMN UserDispName [nvarchar](20)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'DeptCode')
     BEGIN
         ALTER TABLE dbo.MNT_Users ALTER COLUMN DeptCode [nvarchar](10)  NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'BranchCode')
     BEGIN
         ALTER TABLE dbo.MNT_Users ALTER COLUMN BranchCode [nvarchar](10)  NULL
     END   
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_CurrencyTxn' and column_name = 'CurrencyCode')
     BEGIN
       ALTER TABLE dbo.MNT_CurrencyTxn ALTER COLUMN CurrencyCode [nvarchar](20)  NOT NULL
     END
 ----- Add Constraint after run above script
 IF OBJECT_ID('CK_ConstraintName') IS  NULL
  BEGIN
   ALTER TABLE MNT_CurrencyM ADD CONSTRAINT PK_MNT_CurrencyM PRIMARY KEY (CurrencyCode)
  END
 IF OBJECT_ID('FK_MNT_CurrencyTxn_MNT_CurrencyM') IS  NULL
  BEGIN
   ALTER TABLE [dbo].[MNT_CurrencyTxn]  WITH CHECK ADD CONSTRAINT [FK_MNT_CurrencyTxn_MNT_CurrencyM] FOREIGN KEY([CurrencyCode])
   REFERENCES [dbo].[MNT_CurrencyM] ([CurrencyCode])
  END
 IF OBJECT_ID('IX_MNT_Department_DeptName') IS  NULL
  BEGIN
   ALTER TABLE MNT_Department ADD CONSTRAINT IX_MNT_Department_DeptName unique  (DeptCode)   
  END 
 IF OBJECT_ID('IX_MNT_Users_UserId') IS  NULL
  BEGIN                 
   ALTER TABLE MNT_Users ADD CONSTRAINT IX_MNT_Users_UserId unique  (UserId)
  END