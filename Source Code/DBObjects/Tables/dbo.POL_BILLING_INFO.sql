
/****** Object:  Table [dbo].[MNT_MASTER_VALUE]    Script Date: 10/22/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_BILLING_INFO]') AND type in (N'U'))
DROP TABLE [dbo].[POL_BILLING_INFO]
GO
/****** Object:  Table [dbo].[POL_BILLING_INFO]    Script Date: 16/11/2011 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_BILLING_INFO]
(
[BILLING_ID]				INT NOT NULL,
[CUSTOMER_ID]				[NVARCHAR](10) NULL,
[POLICY_ID]					[NVARCHAR](10) NULL,
[POLICY_VERSION_ID]			[NVARCHAR](10) NULL,
[LOB_ID]					[NVARCHAR](10) NULL,
[BILLING_TYPE]				[NVARCHAR](10) NULL,
[BILLING_PLAN]				[NVARCHAR](10) NULL,
[DOWN_PAYMENT_MODE]			[NVARCHAR](10) NULL,
[PROXY_SIGN_OBTAIN]			[NVARCHAR](10) NULL,
[UNDERWRITER]				[NVARCHAR](50) NULL,
[ROLLOVER]					[NVARCHAR](10) NULL,
[RECIVED_PREMIUM]			[NVARCHAR](20) NULL,
[COMP_APP_BONUS_APPLIES]	[nchar](2) NULL,
[CURRENT_RESIDENCE]			[NVARCHAR](50) NULL,
[IS_ACTIVE]					[nchar](1) NULL,
[CREATED_BY]				[INT],
[CREATED_DATETIME]			[DATETIME],
[MODIFIED_BY]				[INT],
[LAST_UPDATED_DATETIME]		[DATETIME]
CONSTRAINT [PK_POL_BILLING_INFO_BILLING_ID] PRIMARY KEY CLUSTERED
([BILLING_ID]ASC)	
)



-----------FOR [BILLING_ID]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_ID
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_ID
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_ID

-----------FOR [CUSTOMER_ID]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CUSTOMER_ID
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CUSTOMER_ID
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CUSTOMER_ID


-----------FOR [POLICY_ID]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',POLICY_ID
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',POLICY_ID
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',POLICY_ID


-----------FOR [POLICY_VERSION_ID]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',POLICY_VERSION_ID
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',POLICY_VERSION_ID
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',POLICY_VERSION_ID


-----------FOR [LOB_ID]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',LOB_ID
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',LOB_ID
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',LOB_ID

-----------FOR [BILLING_TYPE]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_TYPE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_TYPE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_TYPE

-----------FOR [BILLING_PLAN]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_PLAN
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_PLAN
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',BILLING_PLAN

-----------FOR [DOWN_PAYMENT_MODE]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',DOWN_PAYMENT_MODE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',DOWN_PAYMENT_MODE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',DOWN_PAYMENT_MODE

-----------FOR [PROXY_SIGN_OBTAIN]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',PROXY_SIGN_OBTAIN
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',PROXY_SIGN_OBTAIN
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',PROXY_SIGN_OBTAIN

-----------FOR [UNDERWRITER]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',UNDERWRITER
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',UNDERWRITER
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',UNDERWRITER

-----------FOR [ROLLOVER]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',ROLLOVER
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',ROLLOVER
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',ROLLOVER

-----------FOR [RECIVED_PREMIUM]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',RECIVED_PREMIUM
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',RECIVED_PREMIUM
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',RECIVED_PREMIUM

-----------FOR [COMP_APP_BONUS_APPLIES]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',COMP_APP_BONUS_APPLIES
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',COMP_APP_BONUS_APPLIES
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',COMP_APP_BONUS_APPLIES

-----------FOR [CURRENT_RESIDENCE]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CURRENT_RESIDENCE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CURRENT_RESIDENCE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CURRENT_RESIDENCE

-----------FOR [IS_ACTIVE]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',IS_ACTIVE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',IS_ACTIVE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',IS_ACTIVE

-----------FOR [CREATED_BY]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CREATED_BY
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CREATED_BY
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CREATED_BY

-----------FOR [CREATED_DATETIME]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CREATED_DATETIME
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CREATED_DATETIME
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',CREATED_DATETIME

-----------FOR [MODIFIED_BY]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',MODIFIED_BY
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',MODIFIED_BY
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',MODIFIED_BY

-----------FOR [LAST_UPDATED_DATETIME]------
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',LAST_UPDATED_DATETIME
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',LAST_UPDATED_DATETIME
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_BILLING_INFO', 'column',LAST_UPDATED_DATETIME

