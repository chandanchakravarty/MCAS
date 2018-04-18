CREATE PROCEDURE [dbo].[Proc_ProvideDefaultPermission]
	@GroupId [nvarchar](20)
WITH EXECUTE AS CALLER
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


