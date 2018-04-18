CREATE PROCEDURE [dbo].[Proc_CLM_MandateInsertClaim]
	@p_ClaimantName [nvarchar](max),
	@p_ClaimType [int],
	@p_Createdby [nvarchar](max),
	@p_Createddate [datetime],
	@p_AccidentId [int],
	@p_IsActive [nvarchar](1),
	@p_ClaimID [int]
WITH EXECUTE AS CALLER
AS
BEGIN  
  SET FMTONLY OFF;  
  IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL  
    DROP TABLE #mytemptable  
  IF OBJECT_ID('tempdb..#mytemptable1') IS NOT NULL  
    DROP TABLE #mytemptable1  
  IF OBJECT_ID('tempdb..#mytemptable2') IS NOT NULL  
    DROP TABLE #mytemptable2  
  DECLARE @OutputTbl TABLE (  
    ID int  
  )  
  DECLARE @OldData xml  
  DECLARE @NewData xml  
  DECLARE @Tname nvarchar(50)  
  DECLARE @CreatedBy nvarchar(50)  
  DECLARE @ReserveID int  
  DECLARE @ClaimID int  
  DECLARE @ChangedFiled nvarchar(4000)  
  SET @Tname = 'CLM_Mandate'  
  DECLARE @Nodeval TABLE (  
    Nodevalue varchar(30) NULL  
  )  
  
  IF EXISTS (SELECT  
      1  
    FROM CLM_Mandate  
    WHERE ClaimID = @p_ClaimID)  
  BEGIN  
    SET @ReserveID = (SELECT TOP 1  
      MandateId  
    FROM CLM_Mandate  
    WHERE ClaimID = @p_ClaimID  
    ORDER BY ClaimID)  
  
    SELECT  
      * INTO #mytemptable  
    FROM CLM_Mandate  
    WHERE MandateId = @ReserveID  
    SET @OldData = (SELECT  
      *  
    FROM #mytemptable AS CLM_Mandate  
    FOR xml AUTO, ELEMENTS)  
  
    UPDATE [dbo].CLM_Mandate  
    SET ClaimantName = @p_ClaimantName,  
        ClaimType = @p_ClaimType,  
        Modifiedby = @p_Createdby,  
        Modifieddate = @p_Createddate,  
        ClaimID = @p_ClaimID  
    WHERE MandateId = @ReserveID  
  
    SELECT  
      * INTO #mytemptable1  
    FROM CLM_Mandate  
    WHERE MandateId = @ReserveID  
    SET @NewData = (SELECT  
      *  
    FROM #mytemptable1 AS CLM_Mandate  
    FOR xml AUTO, ELEMENTS)  
  
    INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],  
    [ChangedColumns], [TansDescription], [ClaimID], [EntityCode], [EntityType], [EntityTypeId], [AccidentId])  
      VALUES (GETDATE(), 'CLM_Reserve', @p_Createdby, 'U', @OldData, @NewData, '', 'Record updated in Reserve', @p_ClaimID, @ReserveID, 'Claims', 'ReserveId', @p_AccidentId)  
  
  END  
  ELSE  
  BEGIN  
  
    INSERT INTO CLM_Mandate (ClaimantName, ClaimType, Createdby, Createddate, AccidentId, IsActive, ClaimID)  
    OUTPUT INSERTED.MandateId INTO @OutputTbl (ID)  
      VALUES (@p_ClaimantName, @p_ClaimType, @p_Createdby, @p_Createddate, @p_AccidentId, @p_IsActive, @p_ClaimID)  
  
    SET @ReserveID = (SELECT TOP 1  
      ID  
    FROM @OutputTbl  
    ORDER BY ID)  
  
    SELECT  
      * INTO #mytemptable2  
    FROM CLM_Mandate  
    WHERE MandateId = @ReserveID  
  
    SET @NewData = (SELECT  
      *  
    FROM #mytemptable2 AS CLM_Mandate  
    FOR xml AUTO, ELEMENTS)  
  
    INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],  
    [ChangedColumns], [TansDescription], [ClaimID], [EntityCode], [EntityType], [EntityTypeId], [AccidentId])  
      VALUES (GETDATE(), 'CLM_Reserve', @p_Createdby, 'I', '', @NewData, '', 'Record inserted in Reserve', @p_ClaimID, @ReserveID, 'Claims', 'ReserveId', @p_AccidentId)  
  
  END  
END


