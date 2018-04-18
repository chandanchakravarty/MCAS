IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'RefundDataParamters' AND ss.name = N'dbo')
DROP TYPE [dbo].[RefundDataParamters]
GO

CREATE TYPE [dbo].[RefundDataParamters] AS TABLE(
	[ROW_ID] [int] NULL,
	[TABLE_NAME] [varchar] (MAX) NULL,
	[IS_COMMISSION_PROCESS] [varchar] (2) NULL,
	[CLAIM_ID] [int] NULL,
	[ACTIVITY_ID] [int] NULL,
	[COV_ID] [int] NULL,
	[FLAG_RI_CLAIM] [int] NULL,
	[COMP_ID] [int] NULL
)
GO

