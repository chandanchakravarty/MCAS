IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVendorEFTInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVendorEFTInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetVendorEFTInfo
Created by      : Swastika Gaur
Date            : 25 Jul'07  
Purpose         :To get vendor EFT info from Vendor(Maintenance)
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- DROP PROC dbo.Proc_GetVendorEFTInfo
CREATE PROC dbo.Proc_GetVendorEFTInfo 
(  
 @VENDOR_ID int
)  
AS 
BEGIN  
	-- 10964 : NO
	SELECT ISNULL(ALLOWS_EFT,10964) AS ALLOWS_EFT FROM MNT_VENDOR_LIST (NOLOCK) WHERE VENDOR_ID  = @VENDOR_ID  
END  
  
  
  





GO

