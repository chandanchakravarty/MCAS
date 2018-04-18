IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStateNameforID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStateNameforID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : dbo.Proc_GetStateNameforID
Created by         : Swastika Gaur
Date               : 11th August'06
Purpose            : Get State Name based on the parameters passed
Revison History    :
Used In            :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/ 
-- drop proc dbo.Proc_GetStateNameforID
CREATE PROCEDURE [dbo].[Proc_GetStateNameforID]
(
	@custId  int,		
	@appId  int,		
	@appVerId  int	
)
AS
BEGIN 
	SELECT   A.STATE_ID,M.STATE_NAME from APP_LIST A JOIN MNT_COUNTRY_STATE_LIST M
	ON A.STATE_ID = M.STATE_ID 
	WHERE CUSTOMER_ID=@custId   AND 
        APP_ID=@appId  AND 
        APP_VERSION_ID= @appVerId
END
 



GO

