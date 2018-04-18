

/****** Object:  StoredProcedure [dbo].[Proc_ProvideDefaultPermission]    Script Date: 09/24/2014 17:15:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ProvideDefaultPermission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ProvideDefaultPermission]
GO



/****** Object:  StoredProcedure [dbo].[Proc_ProvideDefaultPermission]    Script Date: 09/24/2014 17:15:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










CREATE PROCEDURE [dbo].[Proc_ProvideDefaultPermission] @GroupId nvarchar(20)
AS
Begin
  IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL
    /*Then it exists*/
  DROP TABLE #mytemptable

  SELECT * INTO #mytemptable  FROM MNT_GroupPermission where GroupId=1 and MenuId not in(select menuid from MNT_GroupPermission where GroupId = @GroupId)
  
  UPDATE #mytemptable  SET GroupId = @GroupId
  ALTER TABLE #mytemptable
  DROP COLUMN GroupPermissionId 
  
  INSERT INTO MNT_GroupPermission  SELECT * FROM #mytemptable
  select * from #mytemptable
EnD

GO


