CREATE TABLE [dbo].[MNT_UserCountryProducts](
	[UserCountryProductId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCode] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_UserCountryProducts] PRIMARY KEY CLUSTERED 
(
	[UserCountryProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


