IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetApplicationLOBID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetApplicationLOBID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetApplicationLOBID    
Created by         : Ravindra    
Date               : 09/11/2009    
Purpose            :     
Revison History	   :    
Used In            :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC Dbo.Proc_GetApplicationLOBID    
CREATE PROC [dbo].[Proc_GetApplicationLOBID]    
(    
    
 @CUSTOMERID int,    
 @APPID int,    
 @APPVERSIONID int
)    
    
AS    
BEGIN    

	SELECT isnull(APP_LOB ,0)as LOB_ID FROM APP_LIST WITH(NOLOCK) 
	WHERE CUSTOMER_ID =@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID    
END  








GO

