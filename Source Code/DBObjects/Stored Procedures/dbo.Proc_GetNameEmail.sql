IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNameEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNameEmail]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetNameEmail  
Created by      :Priya  
Date            : 2/5/2005  
Purpose         : To get user name from CLT_CUSTOMER_LIST  
Revison History :  
Used In         :   Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetNameEmail  
(  
@UserId  smallint  
)  
AS  
BEGIN  
  
SELECT USER_FNAME,USER_LNAME,USER_EMAIL

FROM MNT_USER_LIST     
WHERE  User_Id = @UserId  
  
END


GO

