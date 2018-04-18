
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_BILLING_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[POL_BILLING_DETAILS]
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
Date            : 08-Feb-2012      
Purpose			: To store Additional details regarding Billing info      
Revison History :      
Used In        : Application screen Billing info
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------

*/ 
CREATE TABLE [dbo].[POL_BILLING_DETAILS](
	CUSTOMER_ID int NOT NULL,    
 POLICY_ID int NOT NULL,    
 POLICY_VERSION_ID int NOT NULL,    
 PLAN_ID int,    
 GROSS_PREMIUM decimal(18,2),    
 OTHER_CHARGES decimal(18,2),    
 TOTAL_BEFORE_GST decimal(18,2),    
 GST decimal(18,2),    
 TOTAL_AFTER_GST decimal(18,2),    
 GROSS_COMMISSION decimal(18,2), 
 GST_ON_COMMISSION DECIMAL(18,2),   
 TOTAL_COMM_AFTER_GST decimal(18,2),   
 NET_AMOUNT  decimal(18,2),
 CREATED_BY INT,
 CREATED_DATETIME DATETIME,
 MODIFIED_BY INT,
 LAST_UPDATED_DATETIME DATETIME
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_BILLING_DETAILS] ADD CONSTRAINT [PK_POL_BILLING_DETAILS_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] PRIMARY KEY
	CLUSTERED
	(
		CUSTOMER_ID ,    
		POLICY_ID ,    
		POLICY_VERSION_ID  
	)	
GO

