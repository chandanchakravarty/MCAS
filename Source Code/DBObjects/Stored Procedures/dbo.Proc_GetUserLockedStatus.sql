IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserLockedStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserLockedStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name          : Dbo.Proc_GetUserLockedStatus              
Created by         : Pravesh Chandel
Date               : 22 Dec 2008
Purpose            : To get login Locked Status
Revison History    :              
Used In            : Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
--------   ------------       -------------------------*/     
--drop  PROCEDURE Dbo.Proc_GetUserLockedStatus      
       
CREATE   PROCEDURE Dbo.Proc_GetUserLockedStatus      
 @USER_ID   INT 
AS             
BEGIN              
 SELECT ISNULL(RTRIM(LTRIM(USER_LOCKED)),'0') AS USER_LOCKED ,
 case when isnull(CHANGE_PWD_NEXT_LOGIN,0)=10963 then 'Y' else 'N' end as CHANGE_PWD_NEXT_LOGIN 

  FROM MNT_USER_LIST WITH(NOLOCK) WHERE [USER_ID]= @USER_ID 
END              
              
    
  
GO

