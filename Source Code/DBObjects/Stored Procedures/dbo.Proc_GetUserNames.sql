IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserNames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserNames]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name      	 : dbo.Proc_GetUserNames
Created by       : Anurag Verma  
Date             : 03/16/2007  
Purpose       	 : retrieving user name list on the basis of user id list
Revison History  :  
Used In          : Wolverine    
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_GetUserNames
@NAME_LIST nvarchar(200)
AS  
  
BEGIN  
SET @NAME_LIST=RIGHT(@NAME_LIST,LEN(@NAME_LIST)-1)
SET @NAME_LIST=LEFT(@NAME_LIST,LEN(@NAME_LIST)-1)

SELECT DISTINCT USER_ID,ISNULL(USER_FNAME,'')  + ' ' + ISNULL(USER_LNAME,'') USER_NAME  FROM MNT_USER_LIST WHERE USER_ID IN (@NAME_LIST)

END  
  




GO

