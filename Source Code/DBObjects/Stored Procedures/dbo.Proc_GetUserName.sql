IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetUserName  
Created by      : Vijay Arora          
Date            : 15-06-2006
Purpose         : Get the current login User Name
Revison History :          
Used In         : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/   
CREATE  PROC dbo.Proc_GetUserName          
(          
   @UserId  int      
)          
AS          
BEGIN          
SELECT ISNULL(USER_TITLE,'') + ' ' + ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS USERNAME
FROM MNT_USER_LIST WHERE [USER_ID]=@UserId    
END  
  
  
  



GO

