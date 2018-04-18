IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserLoginName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserLoginName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetUserLoginName  
Created by      : Sibin Philip           
Date            : 6 Nov 08  
Purpose         : Get the current login User Name  
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/ 
CREATE  PROC dbo.Proc_GetUserLoginName            
(            
   @UserId  int        
)            
AS            
BEGIN            
SELECT ISNULL(USER_LOGIN_ID,'') AS [USER LOGIN]  
FROM MNT_USER_LIST WHERE [USER_ID]=@UserId      
END    
    
GO

