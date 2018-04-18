CREATE PROCEDURE [dbo].[Proc_ReAssignSave]
	@LISTID [int] = 0,
	@ReAssignTo [int] = 0,
	@ReAssignFrom [int] = 0
WITH EXECUTE AS CALLER
AS
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL
    /*Then it exists*/
  DROP TABLE #mytemptable

  DECLARE @OldData xml
  DECLARE @NewData xml
  DECLARE @Tname nvarchar(50)
  DECLARE @CreatedBy nvarchar(50)
  DECLARE @ClaimID int
  DECLARE @ChangedFiled nvarchar(4000)
  SET @Tname = 'TODODIARYLIST'
  DECLARE @Nodeval TABLE (
    Nodevalue varchar(30) NULL
  )

  SELECT * INTO #mytemptable  FROM TODODIARYLIST WHERE LISTID = @LISTID
  
  UPDATE #mytemptable  SET TOUSERID = @ReAssignTo,FROMUSERID = @ReAssignFrom, ModifiedDate=GETDATE(),ParentId=@LISTID,MovementType='S',ReassignedDiaryDate=GETDATE()  WHERE LISTID = @LISTID
  ALTER TABLE #mytemptable
  DROP COLUMN LISTID 
  
  INSERT INTO TODODIARYLIST  SELECT * FROM #mytemptable
  --select * from #mytemptable
  SET @NewData = (SELECT * FROM #mytemptable AS TODODIARYLIST FOR xml AUTO, ELEMENTS)
  --SELECT @NewData
  SET @ClaimID = (SELECT TOP 1  CLAIMID FROM #mytemptable)
  SET @CreatedBy = (SELECT TOP 1 CreatedBy FROM #mytemptable)

  INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],
  [ChangedColumns], [TansDescription], [ClaimID])
    VALUES (GETDATE(), 'TODODIARYLIST', @CreatedBy, 'I', '', @NewData, '', 'Record Inserted TODODIARYLIST', @ClaimID)


