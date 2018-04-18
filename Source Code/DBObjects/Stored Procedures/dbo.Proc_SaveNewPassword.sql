IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveNewPassword]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveNewPassword]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*---------------------------------------------------------- 
Proc Name : dbo.Proc_SaveNewPassword 
Created by : Sibin Thomas Philip 
Date : 4 Nov 08 
Purpose : Change the User Password 
Revison History : 
modified by : Pravesh K Chandel
Date		: 4 Dec 08 
Purpose		: To handel Case Sensitive Password
Used In : Wolverine 
------------------------------------------------------------ 
Date Review By Comments 
drop  proc [dbo].[Proc_SaveNewPassword] 
------ ------------ -------------------------*/ 
create proc [dbo].[Proc_SaveNewPassword] 
( 
@USER_ID int, 
@User_Login_ID nvarchar(10) , 
@OLD_PWD nvarchar(100), 
@NEW_PWD nvarchar(100), 
@RET_VALUE int out 
) 
as 
 BEGIN 
	Declare @ExistingPwd nvarchar(100)
	IF EXISTS( SELECT USER_LOGIN_ID FROM MNT_USER_LIST WHERE USER_ID = @USER_ID AND USER_LOGIN_ID = @User_Login_ID ) 
			BEGIN 
				SELECT @ExistingPwd = dbo.fun_DecriptText(USER_PWD) FROM MNT_USER_LIST WHERE USER_ID = @USER_ID AND USER_LOGIN_ID = @User_Login_ID 
			IF EXISTS (SELECT USER_PWD FROM MNT_USER_LIST WHERE USER_ID = @USER_ID AND USER_LOGIN_ID = @User_Login_ID AND cast(@ExistingPwd as varbinary) =  cast(@OLD_PWD as varbinary) ) 
				BEGIN 
					UPDATE MNT_USER_LIST SET USER_PWD = dbo.fun_EncriptText(@NEW_PWD) ,CHANGE_PWD_NEXT_LOGIN=null
						WHERE USER_ID = @USER_ID AND USER_LOGIN_ID = @User_Login_ID AND cast(@ExistingPwd as varbinary) =  cast(@OLD_PWD as varbinary)
					SET @RET_VALUE = 1 --Password Changed
				END 
			ELSE 
			BEGIN 
				set @RET_VALUE = -2 --password does not exixts 
			END 
	  END 
	ELSE 
	BEGIN 
	set @RET_VALUE = -1 --user login id does not exist 
	END 
END 




GO

