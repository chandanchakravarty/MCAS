IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPassword]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetPassword
Created by      : Gaurav
Date            : 4/7/2005
Purpose         : To  retreive password 
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetPassword
(
@User_Email		nvarchar(50),		
@User_Login_Id	nvarchar(10)

)
 AS
BEGIN

SELECT USER_PWD FROM MNT_USER_LIST WHERE USER_EMAIL=@User_Email and USER_LOGIN_ID=@User_Login_Id

END



GO

