
/****** Object:  StoredProcedure [dbo].[Proc_MNT_UserOrgAccessDelete]    Script Date: 01/21/2015 17:33:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_UserOrgAccessDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_UserOrgAccessDelete]
GO


/****** Object:  StoredProcedure [dbo].[Proc_MNT_UserOrgAccessDelete]    Script Date: 01/21/2015 17:33:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Proc_MNT_UserOrgAccessDelete]
(
@p_UserId nvarchar(50)
)
as
SET FMTONLY OFF;
BEGIN
IF EXISTS (SELECT OrgCode,OrgName FROM [MNT_UserOrgAccess] WHERE UserId = @p_UserId)  
DELETE FROM MNT_UserOrgAccess WHERE UserId = @p_UserId 
END
GO


