IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserIDForUnderwriter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserIDForUnderwriter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetUserIDForUnderwriter  
Created by           : Sumit Chhabra
Date                    : 30/01/2006
Purpose               :   
Revison History :  
Used In                :   Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROCEDURE Proc_GetUserIDForUnderwriter  
(  
 @USERID int  
)  
AS  
BEGIN  
--print @userid
SELECT   
USER_ID
FROM MNT_USER_LIST  
WHERE USER_TYPE_ID=@USERID  
END  


GO

