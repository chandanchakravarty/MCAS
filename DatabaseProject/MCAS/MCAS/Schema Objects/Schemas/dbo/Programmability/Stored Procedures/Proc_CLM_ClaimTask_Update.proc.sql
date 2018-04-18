CREATE PROCEDURE [dbo].[Proc_CLM_ClaimTask_Update]
	@p_Id [int],
	@p_TaskNo [int],
	@p_ClaimID [int],
	@p_ActionDue [datetime],
	@p_CloseDate [datetime],
	@p_PromtDetails [nvarchar](100),
	@p_isApprove [int],
	@p_ApproveDate [datetime],
	@p_ApproveBy [nvarchar](25),
	@p_ModifiedDate [datetime],
	@p_CreatedDate [datetime],
	@p_CreatedBy [nvarchar](25),
	@p_ModifiedBy [nvarchar](25),
	@p_Remarks [nvarchar](500),
	@AccidentClaimId [int],
	@Id [int],
    @ClaimantNames [nvarchar](255),
    @ClaimsOfficer [int]
WITH EXECUTE AS CALLER
AS
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL
    DROP TABLE #mytemptable
  IF OBJECT_ID('tempdb..#mytemptable1') IS NOT NULL
    DROP TABLE #mytemptable1

  DECLARE @OldData xml
  DECLARE @NewData xml
  DECLARE @Tname nvarchar(50)
  DECLARE @Pdetails nvarchar(50)
  DECLARE @CreatedBy nvarchar(50)
  DECLARE @ClaimID int
  DECLARE @ChangedFiled nvarchar(4000)
  SET @Tname = 'CLM_ClaimTask'
  DECLARE @Nodeval TABLE (
    Nodevalue varchar(30) NULL
  )


  BEGIN
    SET FMTONLY OFF;

    SELECT * INTO #mytemptable1 FROM CLM_ClaimTask WHERE Id = @Id
    
    SET @Pdetails = (SELECT  Lookupdesc  FROM MNT_Lookups  WHERE Lookupvalue = (SELECT PromtDetails FROM CLM_ClaimTask WHERE Id = @Id)  AND  Category = 'TASKTYPE')
    UPDATE #mytemptable1  SET PromtDetails = @Pdetails WHERE Id = @Id and TaskNo=@p_TaskNo 

    UPDATE [CLM_ClaimTask]
    SET [TaskNo] = @p_TaskNo,
        [ClaimID] = @p_ClaimID,
        [ActionDue] = @p_ActionDue,
        [CloseDate] = @p_CloseDate,
        [PromtDetails] = @p_PromtDetails,
        [isApprove] = 1,
        [ApproveDate] = @p_ApproveDate,
        [ApproveBy] = @p_ApproveBy,
        [ModifiedDate] = @p_ModifiedDate,
        [ModifiedBy] = @p_ModifiedBy,
        [Remarks] = @p_Remarks,
        [ClaimantNames] = @ClaimantNames,
        [ClaimsOfficer] = @ClaimsOfficer 
    WHERE [TaskNo] = @p_TaskNo
    AND [ClaimID] = @p_ClaimID
    AND Id = @p_Id

    SELECT * INTO #mytemptable FROM CLM_ClaimTask WHERE Id = @Id
    SET @Pdetails = (SELECT Lookupdesc FROM MNT_Lookups WHERE Lookupvalue = @p_PromtDetails AND Category = 'TASKTYPE')
    UPDATE #mytemptable SET PromtDetails = @Pdetails WHERE Id = @Id
    

    SET @OldData = (SELECT * FROM #mytemptable1 AS CLM_ClaimTask FOR xml AUTO, ELEMENTS)
    SET @NewData = (SELECT * FROM #mytemptable AS CLM_ClaimTask FOR xml AUTO, ELEMENTS)
    SET @ClaimID = (SELECT TOP 1 ClaimID FROM #mytemptable WHERE Id = @Id)
    SET @CreatedBy = (SELECT TOP 1 CreatedBy FROM #mytemptable WHERE Id = @Id)
    

    INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData], [ChangedColumns], [TansDescription], [ClaimID],[EntityCode],[EntityType],[EntityTypeId],[AccidentId])
      VALUES (GETDATE(), 'CLM_ClaimTask', @CreatedBy, 'U', @OldData, @NewData, '', 'Record Updated In CLM_ClaimTask', @ClaimID,@Id,'Claims','ID',@AccidentClaimId)

  END


