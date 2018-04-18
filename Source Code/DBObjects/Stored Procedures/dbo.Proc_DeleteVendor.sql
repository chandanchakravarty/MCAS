IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteVendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteVendor]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_DeleteVendor  
Created by      : Priya  
Date            : 15 Jun,2005  
Purpose         : To delete record from Vendor table  
Revison History :  
Used In         :   wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_DeleteVendor  
CREATE PROC dbo.Proc_DeleteVendor  
(  
  
 @VENDORID INT  
)  
AS  
BEGIN  
-- Check if any invoice has been generated for the vendor in ACT_VENDOR_INVOICES
 SELECT @@ROWCOUNT FROM ACT_VENDOR_INVOICES WHERE VENDOR_ID = @VENDORID
 IF @@ROWCOUNT <> 0
  RETURN -1 

-- Check if any check has been created for that vendor in TEMP_ACT_CHECK_INFORMATION
 SELECT @@ROWCOUNT FROM TEMP_ACT_CHECK_INFORMATION WHERE PAYEE_ENTITY_ID = @VENDORID
 IF @@ROWCOUNT <> 0
  RETURN -1 

-- Check if any check has been created for that vendor in ACT_CHECK_INFORMATION
 SELECT @@ROWCOUNT FROM ACT_CHECK_INFORMATION WHERE PAYEE_ENTITY_ID = @VENDORID
 IF @@ROWCOUNT <> 0
  RETURN -1 

 DELETE FROM MNT_VENDOR_LIST  
 WHERE VENDOR_ID = @VENDORID  
END  
  


GO

