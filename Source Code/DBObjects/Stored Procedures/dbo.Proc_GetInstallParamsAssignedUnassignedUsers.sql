IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetInstallParamsAssignedUnassignedUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetInstallParamsAssignedUnassignedUsers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_GetInstallParamsAssignedUnassignedUsers
Created by      : Vijay Joshi
Date            : 27/June/2005
Purpose    	: returns list of assigned and unassigned users of installment parameters
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_GetInstallParamsAssignedUnassignedUsers
(
	@AGENCY_CODE varchar(6)
)
AS
BEGIN
	SELECT USER_ID, IsNull(USER_FNAME,'') + ' ' + IsNull(USER_LNAME,'') As USER_NAME
	FROM MNT_USER_LIST 
	WHERE  USER_SYSTEM_ID = @AGENCY_CODE AND 
	PATINDEX('%' + Convert(varchar,user_id) + '%',IsNull((SELECT INSTALL_NOTIFY_OTHER_USERS FROM ACT_INSTALL_PARAMS ),'')) <>0
	
	
	SELECT USER_ID, IsNull(USER_FNAME,'') + ' ' + IsNull(USER_LNAME,'') As USER_NAME
	FROM MNT_USER_LIST 
	WHERE  USER_SYSTEM_ID = @AGENCY_CODE AND 	
	PATINDEX('%' + Convert(varchar,user_id) + '%',IsNull((SELECT INSTALL_NOTIFY_OTHER_USERS FROM ACT_INSTALL_PARAMS ),'')) =0
END





GO

