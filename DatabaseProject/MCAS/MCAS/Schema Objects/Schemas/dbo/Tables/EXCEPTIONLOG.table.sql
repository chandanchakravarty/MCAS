CREATE TABLE [dbo].[EXCEPTIONLOG](
	[exceptionid] [int] IDENTITY(1,1) NOT NULL,
	[exceptiondate] [datetime] NULL,
	[exceptiondesc] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[customer_id] [int] NULL,
	[accidentClaim_id] [int] NULL,
	[policy_id] [int] NULL,
	[policy_version_id] [smallint] NULL,
	[claim_id] [int] NULL,
	[entity_id] [int] NULL,
	[entity_type] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[source] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[message] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[class_name] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[method_name] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[query_string_params] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[system_id] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[user_id] [int] NULL,
	[exception_type] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


