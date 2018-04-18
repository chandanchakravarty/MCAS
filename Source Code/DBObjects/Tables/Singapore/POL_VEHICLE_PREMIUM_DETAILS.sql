IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_VEHICLE_PREMIUM_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[POL_VEHICLE_PREMIUM_DETAILS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
 /*----------------------------------------------------------      
Proc Name       : [Proc_GetQQVehicleRatingInfo]      
Created by      : Kuldeep Saxena     
Date            : 15/01/2012      
Purpose   : Demo      
Revison History :      
Used In        : Singapore  to STORE VEHICLE vehicle rating information when quote is generated without quick quote    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------

*/ 
CREATE TABLE [dbo].[POL_VEHICLE_PREMIUM_DETAILS](
	CUSTOMER_ID int NOT NULL,    
 POLICY_ID int NOT NULL,    
 POLICY_VERSION_ID int NOT NULL,    
 VEHICLE_ID int,    
 BASE_PREMIUM varchar(15),---decimal(18,2),    
 DEMERIT_DISC_AMT varchar(15),    
 GST_AMOUNT varchar(15),    
 FINAL_PREMIUM varchar(15)   
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_VEHICLE_PREMIUM_DETAILS] ADD CONSTRAINT [PK_POL_VEHICLE_PREMIUM_DETAILS_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] PRIMARY KEY
	CLUSTERED
	(
		CUSTOMER_ID ,    
		POLICY_ID ,    
		POLICY_VERSION_ID  
	)	
GO

