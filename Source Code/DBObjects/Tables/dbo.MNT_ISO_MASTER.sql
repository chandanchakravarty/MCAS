IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_ISO_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_ISO_MASTER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_ISO_MASTER](
	[VIN] [nvarchar] (255) NULL,
	[EFFECTIVE_DATE] [nvarchar] (4) NULL,
	[REST_INDICATOR] [nvarchar] (255) NULL,
	[ALB_INDICATOR] [nvarchar] (255) NULL,
	[CYN] [nvarchar] (3) NULL,
	[ENG_TYPE_IND] [nvarchar] (255) NULL,
	[STATE] [nvarchar] (255) NULL,
	[SYM_CHG_IND] [nvarchar] (255) NULL,
	[VSR] [nvarchar] (255) NULL,
	[NON_VSR] [nvarchar] (255) NULL,
	[FCI] [nvarchar] (255) NULL,
	[MANUFACTURER] [nvarchar] (255) NULL,
	[MODEL] [nvarchar] (255) NULL,
	[VSR_PER_IND] [nvarchar] (255) NULL,
	[BODY STYLE] [nvarchar] (255) NULL,
	[ENGINE SIZE] [varchar] (3) NULL,
	[FWD_IND] [nvarchar] (3) NULL,
	[PER_IND] [nvarchar] (255) NULL,
	[FULL MODEL NAME] [nvarchar] (255) NULL,
	[SIF] [nvarchar] (255) NULL,
	[MODEL/SERIES] [nvarchar] (255) NULL,
	[BODY] [nvarchar] (255) NULL,
	[ENGINE] [nvarchar] (255) NULL,
	[RESTRAINT] [nvarchar] (255) NULL,
	[TRANSMISSION] [nvarchar] (255) NULL,
	[OTHER OPTIONS] [nvarchar] (255) NULL,
	[DRL] [nvarchar] (255) NULL,
	[NCIC] [nvarchar] (255) NULL,
	[CIRNO] [nvarchar] (4) NULL,
	[VSR_SYM] [nvarchar] (2) NULL,
	[NON_VSR_SYM] [nvarchar] (2) NULL,
	[WB] [nvarchar] (5) NULL,
	[CLASS CODE] [varchar] (2) NULL,
	[ATI] [nvarchar] (255) NULL,
	[CURB_WEIGHT] [nvarchar] (5) NULL,
	[GV_WEIGHT] [nvarchar] (5) NULL,
	[HEIGHT] [nvarchar] (5) NULL,
	[HP] [nvarchar] (3) NULL,
	[IDEN_ROW_ID] [int] NOT NULL IDENTITY (1,1)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_ISO_MASTER] ADD CONSTRAINT [PK_MNT_ISO_MASTER_IDEN_ROW_ID] PRIMARY KEY
	CLUSTERED
	(
		[IDEN_ROW_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

