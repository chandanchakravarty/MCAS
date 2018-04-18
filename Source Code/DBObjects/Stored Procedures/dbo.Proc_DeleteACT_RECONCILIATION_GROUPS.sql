IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_RECONCILIATION_GROUPS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_RECONCILIATION_GROUPS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_DeleteACT_RECONCILIATION_GROUPS    
Created by      : Swastika Gaur
Date            : 11th Jan'06    
Purpose     	: Delete values from ACT_RECONCILIATION_GROUP    
Revison History :    
Used In         : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   
-- drop proc dbo.Proc_DeleteACT_RECONCILIATION_GROUPS    
CREATE PROC dbo.Proc_DeleteACT_RECONCILIATION_GROUPS    
(    
 @GROUP_ID int,    
 @RECON_ENTITY_TYPE NVARCHAR(10)
)    
AS    
BEGIN    

--	Delete Reconciliation Details
 IF @RECON_ENTITY_TYPE = 'VEN'
    DELETE FROM ACT_VENDOR_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID

 IF @RECON_ENTITY_TYPE = 'AGN'
	DELETE FROM ACT_AGENCY_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID
 	 
 IF @RECON_ENTITY_TYPE = 'CUST'
	DELETE FROM ACT_CUSTOMER_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID
-- Delete Reconcilation Group
 DELETE FROM ACT_RECONCILIATION_GROUPS WHERE GROUP_ID = @GROUP_ID

END    
    




GO

