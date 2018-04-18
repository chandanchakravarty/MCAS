IF EXISTS (SELECT  *  FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CLM_ReserveInsertClaim]') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CLM_ReserveInsertClaim]
GO

CREATE PROCEDURE [dbo].[Proc_CLM_ReserveInsertClaim]
@RId int output,
@p_ClaimantName nvarchar(max),
@p_ClaimType int,
@p_Createdby nvarchar(max),
@p_Createddate datetime,
@p_AccidentId int,
@p_PolicyId int,
@p_IsActive nvarchar(1),
@p_ClaimID int
AS
BEGIN
  SET FMTONLY OFF;
  IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL DROP TABLE #mytemptable
  DECLARE @OutputTbl TABLE (ID int)
  DECLARE @OldData xml
  DECLARE @NewData xml
  DECLARE @Tname nvarchar(50)
  DECLARE @CreatedBy nvarchar(50)
  DECLARE @ReserveID int
  DECLARE @ClaimID int
  DECLARE @ChangedFiled nvarchar(4000)
  SET @Tname = 'CLM_Reserve'
  DECLARE @Nodeval TABLE (Nodevalue varchar(30) NULL)

  BEGIN

    INSERT INTO CLM_Reserve (ClaimantName, ClaimType, Createdby, Createddate, AccidentId, IsActive, ClaimID,PolicyId,MovementType) OUTPUT INSERTED.ReserveId INTO @OutputTbl (ID) VALUES (@p_ClaimantName, @p_ClaimType, @p_Createdby, @p_Createddate, @p_AccidentId, @p_IsActive, @p_ClaimID,@p_PolicyId,'I') 
	SELECT @RId = SCOPE_IDENTITY();
    SET @ReserveID = (SELECT TOP 1 ID FROM @OutputTbl ORDER BY ID)
    SELECT * INTO #mytemptable FROM CLM_Reserve WHERE ReserveId = @ReserveID
    SET @NewData = (SELECT * FROM #mytemptable AS CLM_Reserve FOR xml AUTO, ELEMENTS)

    INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],[ChangedColumns], [TansDescription], [ClaimID], [EntityCode], [EntityType], [EntityTypeId], [AccidentId]) VALUES (GETDATE(), 'CLM_Reserve', @p_Createdby, 'I', '', @NewData, '', 'Record inserted in Reserve', @p_ClaimID, @ReserveID, 'Claims', 'ReserveId', @p_AccidentId)

  END
END



GO