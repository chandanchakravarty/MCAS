IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserLoggedStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserLoggedStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name          : Dbo.Proc_GetUserLoggedStatus              
Created by         : Praveen kasana         
Date               : 20/08/2007       
Purpose            : To get login Sesison ID [user]
Revison History    :              
Used In            : Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/     
       
CREATE   PROCEDURE Dbo.Proc_GetUserLoggedStatus      
 @USER_ID   INT    
AS             
BEGIN              
 SELECT ISNULL(RTRIM(LTRIM(LOGGED_STATUS)),'N') AS LOGGED_STATUS     ,
 isnull(SESSION_ID,'') as SESSION_ID
 FROM MNT_USER_LIST WITH(NOLOCK) WHERE [USER_ID]= @USER_ID    
END              
              
    
  





GO

