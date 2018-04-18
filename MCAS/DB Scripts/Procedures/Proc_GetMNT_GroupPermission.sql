

/****** Object:  StoredProcedure [dbo].[Proc_GetMNT_GroupPermission]    Script Date: 09/01/2014 10:39:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_GroupPermission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_GroupPermission]
GO



/****** Object:  StoredProcedure [dbo].[Proc_GetMNT_GroupPermission]    Script Date: 09/01/2014 10:39:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create Proc [dbo].[Proc_GetMNT_GroupPermission]
(
@GroupId nvarchar(50)
)
as
BEGIN
SET FMTONLY OFF;
SELECT [GroupPermissionId]
      ,[GroupId]
      ,[MenuId]
      ,[Status]
      ,[RowId]
      ,[Read]
      ,[Write]
      ,[Delete]
      ,[SplPermission]
  FROM [MNT_GroupPermission] where [GroupId] =@GroupId order by 1 desc

END




GO


