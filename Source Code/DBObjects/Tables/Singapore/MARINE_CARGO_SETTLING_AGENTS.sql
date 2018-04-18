IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MARINE_CARGO_SETTLING_AGENTS]') AND type in (N'U'))
DROP TABLE [dbo].[MARINE_CARGO_SETTLING_AGENTS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
 /*----------------------------------------------------------      
Proc Name       : [MARINE_CARGO_SETTLING_AGENTS]      
Created by      : Avijit Goswami
Date            : 15/03/2012      
Purpose			: Demo      
Revison History :      
Used In        : Singapore  
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/ 
CREATE TABLE [dbo].[MARINE_CARGO_SETTLING_AGENTS](
						AGENT_ID VARCHAR(10)NOT NULL,
						AGENT_CODE VARCHAR(10),
						AGENT_NAME VARCHAR(40),
						AGENT_ADDRESS1 VARCHAR(40),
						AGENT_ADDRESS2 VARCHAR(40),
						AGENT_CITY VARCHAR(40),
						AGENT_COUNTRY VARCHAR(40),
						AGENT_SURVEY_CODE VARCHAR(40),
						IS_ACTIVE CHAR,
						CREATED_BY INT,
						CREATED_DATETIME DATETIME,
						MODIFIED_BY INT,
						LAST_UPDATED_DATETIME DATETIME  
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MARINE_CARGO_SETTLING_AGENTS] ADD CONSTRAINT [PK_MARINE_CARGO_SETTLING_AGENTS_AGENT_ID] PRIMARY KEY
	CLUSTERED
	(
		AGENT_ID 
	)	
GO