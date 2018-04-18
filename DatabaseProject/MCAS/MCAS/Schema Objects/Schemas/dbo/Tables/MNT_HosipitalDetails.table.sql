CREATE TABLE [dbo].[MNT_HosipitalDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HospitalVal] [int] NULL,
	[HospitalText] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF__MNT_Hosip__Creat__192BAC54]  DEFAULT (getdate()),
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimID] [int] NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_HosipitalMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


