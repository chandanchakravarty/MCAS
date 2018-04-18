CREATE TABLE [dbo].[SVC_TRANSACTION_LOG_FIELDS](
      [ID] int identity primary key,
      [FIELD_NAME] [nvarchar](2000) NULL,
      [TABLE_NAME] [nvarchar](2000) NULL,
      [VALUE_ID] [nvarchar](2000) NULL,
      [VALUE_DESC1] [nvarchar](2000) NULL,
      [VALUE_DESC2] [nvarchar](2000) NULL,
      [VALUE_ID1] [nvarchar](2000) NULL,
      [IS_ACTIVE] [nchar](2) NULL,
      [M_TABLE_NAME] [varchar](50) NULL,
      [VALUE_DESC3] [nvarchar](2000) NULL
) ON [PRIMARY]