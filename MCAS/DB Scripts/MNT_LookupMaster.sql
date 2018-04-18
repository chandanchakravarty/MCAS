


CREATE TABLE [dbo].[MNT_LookupsMaster](
	[LookUpMasterID] [int] IDENTITY(1,1) NOT NULL,
	[LookupCategoryCode] [nvarchar](40) null,
	[LookupCategoryDesc] [nvarchar](200) null,
	[IsActive] [char] Null,
	[IsCommonMaster] [char] Null,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] datetime null,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifiedDate] datetime null	
) ON [PRIMARY]






IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='AccidentCause') 
BEGIN 

INSERT [dbo].[MNT_LookupsMaster] ( [LookupCategoryCode], [LookupCategoryDesc], [IsActive], [IsCommonMaster]) VALUES ('AccidentCause', 'Accident Cause', 'Y', 'Y')

END

IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='ClaimantType') 
BEGIN 

INSERT [dbo].[MNT_LookupsMaster] ( [LookupCategoryCode], [LookupCategoryDesc], [IsActive], [IsCommonMaster]) VALUES ('ClaimantType', 'Claimant Type', 'Y', 'Y')

END

IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='CaseCategory') 
BEGIN 

INSERT [dbo].[MNT_LookupsMaster] ( [LookupCategoryCode], [LookupCategoryDesc], [IsActive], [IsCommonMaster]) VALUES ('CaseCategory', 'Case Category', 'Y', 'Y')

END

IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='CaseStatus') 
BEGIN 

INSERT [dbo].[MNT_LookupsMaster] ( [LookupCategoryCode], [LookupCategoryDesc], [IsActive], [IsCommonMaster]) VALUES ('CaseStatus', 'Case Status', 'Y', 'Y')

END


IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='Constituency') 
BEGIN 

INSERT [dbo].[MNT_LookupsMaster] ( [LookupCategoryCode], [LookupCategoryDesc], [IsActive], [IsCommonMaster]) VALUES ('Constituency', 'Constituency', 'Y', 'Y')

END

IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='MP') 
BEGIN 

INSERT [dbo].[MNT_LookupsMaster] ( [LookupCategoryCode], [LookupCategoryDesc], [IsActive], [IsCommonMaster]) VALUES ('MP', 'MP', 'Y', 'Y')

END



IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='TASKTYPE') 
BEGIN 

INSERT [dbo].[MNT_LookupsMaster] ( [LookupCategoryCode], [LookupCategoryDesc], [IsActive], [IsCommonMaster]) VALUES ('TASKTYPE', 'Prompt Details', 'Y', 'Y')

END



