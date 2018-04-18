
/****** Object:  StoredProcedure [dbo].[Proc_ReAssignSave]    Script Date: 07/23/2014 12:44:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReAssignSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReAssignSave]
GO



/****** Object:  StoredProcedure [dbo].[Proc_ReAssignSave]    Script Date: 07/23/2014 12:44:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_ReAssignSave] @ReAssignFrom int = 0, @ReAssignTo int = 0
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



  SELECT
    * INTO #mytemptable
  FROM TODODIARYLIST
  WHERE TOUSERID = @ReAssignFrom
  UPDATE #mytemptable
  SET TOUSERID = @ReAssignTo
  WHERE TOUSERID = @ReAssignFrom
  INSERT INTO TODODIARYLIST
    SELECT
      [RECBYSYSTEM],
      [RECDATE],
      [FOLLOWUPDATE],
      [LISTTYPEID],
      [POLICYBROKERID],
      [SUBJECTLINE],
      [LISTOPEN],
      [SYSTEMFOLLOWUPID],
      [PRIORITY],
      [TOUSERID],
      [FROMUSERID],
      [STARTTIME],
      [ENDTIME],
      [NOTE],
      [PROPOSALVERSION],
      [QUOTEID],
      [CLAIMID],
      [CLAIMMOVEMENTID],
      [TOENTITYID],
      [FROMENTITYID],
      [CUSTOMER_ID],
      [APP_ID],
      [APP_VERSION_ID],
      [POLICY_ID],
      [POLICY_VERSION_ID],
      [RULES_VERIFIED],
      [PROCESS_ROW_ID],
      [MODULE_ID],
      [INSURERNAME],
      [POLICYNO],
      [CLAIMNO],
      [ExpectedPaymentDate],
      [ReminderBeforeCompletion],
      [Escalation],
      [EscalationTo],
      [EmailBody],
      [CreatedBy],
      [ModifiedBy],
       GETDATE(),
      [CreatedDate],
      [UserId],
      [ReassignedDiary],
      [ReassignedDiaryDate]
    FROM #mytemptable
  --select * from #mytemptable

  SET @NewData = (SELECT
    *
  FROM #mytemptable AS TODODIARYLIST
  FOR xml AUTO, ELEMENTS)
  --SELECT @NewData
  SET @ClaimID = (SELECT TOP 1
    CLAIMID
  FROM #mytemptable)
  SET @CreatedBy = (SELECT TOP 1
    CreatedBy
  FROM #mytemptable)

  INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],
  [ChangedColumns], [TansDescription], [ClaimID])
    VALUES (GETDATE(), 'TODODIARYLIST', @CreatedBy, 'I', '', @NewData, '', 'Record Inserted TODODIARYLIST', @ClaimID)

GO


