CREATE TABLE [dbo].[MNT_ProductsCountry](
	[ProductCountryId] [int] IDENTITY(1,1) NOT NULL,
	[Product_Code] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryCode] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_ProductsCountry] PRIMARY KEY CLUSTERED 
(
	[ProductCountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


