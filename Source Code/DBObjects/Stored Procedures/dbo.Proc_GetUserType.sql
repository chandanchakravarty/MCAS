IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetUserType
Created by           : Gaurav
Date                    : 31/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_GetUserType
CREATE   PROCEDURE dbo.Proc_GetUserType
(
	@UserTypeId int,
	@Lang_id int=1 
)
AS
BEGIN
Select 
usrType.USER_TYPE_ID,
usrType.USER_TYPE_CODE,
isnull(MUTM.USER_TYPE_DESC,usrType.USER_TYPE_DESC)USER_TYPE_DESC,
usrType.IS_ACTIVE,
usrType.USER_TYPE_SYSTEM,
usrType.SYSTEM_GENERATED_CODE 
From MNT_USER_TYPES usrType
left outer join MNT_USER_TYPES_MULTILINGUAL MUTM ON MUTM.USER_TYPE_ID=usrType.USER_TYPE_ID and MUTM.LANG_ID=@Lang_id  
where usrType.USER_TYPE_ID=@UserTypeId

END




GO

