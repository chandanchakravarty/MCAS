IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStateIdForApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStateIdForApplication]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetStateIdForApplication
Created by           : Mohit Gupta
Date                    : 27/09/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetStateIdForApplication
(
	@custId  int,		
	@appId  int,		
	@appVerId  int,
	@STATE_ID int output		
)
AS
BEGIN


SELECT   @STATE_ID= STATE_ID from APP_LIST 
WHERE CUSTOMER_ID=@custId   AND 
              APP_ID=@appId                 AND 
              APP_VERSION_ID= @appVerId


END

GO

