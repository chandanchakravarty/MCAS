IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePOL_WATER_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePOL_WATER_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
      
Proc Name       : Proc_Deletepol_WATER_MVR_INFORMATION      
Created by      : Anurag verma     
Date            : 08/11/2005      
Purpose         : Delete of Water Driver Policy MVR Information      
Revison History :      
Used In                   : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_DeletePOL_WATER_MVR_INFORMATION      
(      
 @APP_WATER_MVR_ID  int,
 @CUSTOMER_ID  int,
 @POL_ID INT,
 @POL_VERSION_ID INT,
 @DRIVER_ID INT	    
)      
AS      
BEGIN      
 DELETE FROM POL_WATERCRAFT_MVR_INFORMATION WHERE APP_WATER_MVR_ID=@APP_WATER_MVR_ID  AND
 CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND DRIVER_ID=@DRIVER_ID
END     
  





GO

