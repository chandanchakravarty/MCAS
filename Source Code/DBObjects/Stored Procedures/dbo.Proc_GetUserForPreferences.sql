IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserForPreferences]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserForPreferences]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Proc Name          : Dbo.Proc_GetUserForPreferences  
--Created by           : Mohit Gupta  
--Date                    : 19/05/2005  
--Purpose               :   
--Revison History :  
--Used In                :   Wolverine    
------------------------------------------------------------  
--Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE   PROCEDURE Proc_GetUserForPreferences  
(  
 @USERID int  
)  
AS  
BEGIN  
SELECT   
USER_ID,USER_COLOR_SCHEME,GRID_SIZE,LANG_ID  
FROM MNT_USER_LIST  
WHERE USER_ID=@USERID  
END  




GO

