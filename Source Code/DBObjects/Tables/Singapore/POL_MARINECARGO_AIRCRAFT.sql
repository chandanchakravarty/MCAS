IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_MARINECARGO_AIRCRAFT]') AND type in (N'U'))
DROP TABLE [dbo].[POL_MARINECARGO_AIRCRAFT]
GO
--POL_MARINECARGO_AIRCRAFT
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
 /*----------------------------------------------------------      
Proc Name       : [POL_MARINECARGO_AIRCRAFT]      
Created by      : Avijit Goswami
Date            : 21/03/2012      
Purpose			: Demo      
Revison History :      
Used In        : Singapore  
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/ 
CREATE TABLE [dbo].[POL_MARINECARGO_AIRCRAFT](
						AIRCRAFT_ID VARCHAR(10)NOT NULL,
						AIRCRAFT_NUMBER VARCHAR(10),
						AIRLINE VARCHAR(10),
						AIRCRAFT_FROM VARCHAR(40),
						AIRCRAFT_TO VARCHAR(40),
						AIRWAY_BILL VARCHAR(40),						
						IS_ACTIVE CHAR,
						CREATED_BY INT,
						CREATED_DATETIME DATETIME,
						MODIFIED_BY INT,
						LAST_UPDATED_DATETIME DATETIME  
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_MARINECARGO_AIRCRAFT] ADD CONSTRAINT [PK_POL_MARINECARGO_AIRCRAFT_AIRCRAFT_ID] PRIMARY KEY
	CLUSTERED
	(
		AIRCRAFT_ID
	)	
GO