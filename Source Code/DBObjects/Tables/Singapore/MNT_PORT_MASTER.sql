IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_PORT_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_PORT_MASTER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
 /*----------------------------------------------------------      
TABLE  Name       : [MNT_PORT_MASTER]      
Created by      : Kuldeep Saxena     
Date            : 14/03/2012      
Purpose   : Demo      
Revison History :      
Used In        : Singapore  to STORE PORT MASTER DETAILS FROM MAINTENANCE
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------

*/ 
CREATE TABLE [dbo].[MNT_PORT_MASTER](
 PORT_CODE int NOT NULL,    
 ISO_CODE NVARCHAR(10) ,    
 PORT_TYPE NVARCHAR(10),    
 COUNTRY NVARCHAR(40),    
 ADDITIONAL_WAR_RATE DECIMAL(18,2),    
 FROM_DATE Date,    
 TO_DATE date,    
 SETTLEMENT_AGENT_CODE Nvarchar(10),
 SETTLING_AGENT_NAME NVARCHAR(25),
 CREATED_BY INT,
 CREATED_DATETIME DATETIME,
 MODIFIED_BY INT,
 LAST_UPDATED_DATETIME DATETIME,
 IS_ACTIVE CHAR   
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_PORT_MASTER] ADD CONSTRAINT [PK_MNT_PORT_MASTER_PORT_CODE] PRIMARY KEY
	CLUSTERED
	(
		PORT_CODE
	)	
GO

