
begin tran
IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='RuleTest')
BEGIN 
CREATE TABLE [dbo].RuleTest(
	[Customer_ID] [int] NULL,
	[Policy_ID] [int] NULL,
	[Policy_VersionID] [smallint] NULL,
	[Policy_Number] [nvarchar](25) NULL,
	[Status] [bit] NULL
             
)
END
rollback tran 