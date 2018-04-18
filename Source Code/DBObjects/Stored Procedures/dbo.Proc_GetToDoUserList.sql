IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetToDoUserList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetToDoUserList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--drop procedure dbo.Proc_GetToDoUserList
--GO
/************************************************************************************************
Created By : Sibin Philip  
Date	   : 3-Nov-2009
Purpose	   : Itrack 6658 to select users whose diary entry have already gone and are now deactivated 
************************************************************************************************/
-- drop procedure dbo.Proc_GetToDoUserList
CREATE   procedure dbo.Proc_GetToDoUserList

@USERID INT

AS
BEGIN  
 
 SELECT USER_SYSTEM_ID as systemid, USER_ID as userid, USER_FNAME+' '+USER_LNAME as username,IS_ACTIVE 
 FROM MNT_USER_LIST WITH(NOLOCK)
 WHERE (USER_SYSTEM_ID='w001' and IS_ACTIVE='Y') 
		OR (USER_ID=@USERID) 
 ORDER BY USER_FNAME, USER_LNAME  
 
 SELECT IS_ACTIVE 
 FROM MNT_USER_LIST WITH(NOLOCK)
 WHERE USER_ID=@USERID 
 
END

--GO
--EXEC Proc_GetToDoUserList 79
--ROLLBACK TRAN





GO

