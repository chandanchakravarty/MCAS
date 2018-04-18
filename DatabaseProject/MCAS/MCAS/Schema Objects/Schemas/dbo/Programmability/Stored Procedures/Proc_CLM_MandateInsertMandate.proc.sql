CREATE PROCEDURE [dbo].[Proc_CLM_MandateInsertMandate]
	@MId [int] output,  
	@p_ClaimantName [nvarchar](max),
	@RId [int],
	@p_ClaimType [int],
	@p_Createdby [nvarchar](max),
	@p_Createddate [datetime],
	@p_AccidentId [int],
	@p_PolicyId [int],
	@p_IsActive [nvarchar](1),
	@p_ClaimID [int]
WITH EXECUTE AS CALLER
AS
BEGIN  
  SET FMTONLY OFF;  
  IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL DROP TABLE #mytemptable  
  DECLARE @OutputTbl TABLE (ID int)  
  DECLARE @OldData xml  
  DECLARE @NewData xml  
  DECLARE @Tname nvarchar(50)  
  DECLARE @CreatedBy nvarchar(50)  
  DECLARE @MandateID int  
  DECLARE @ClaimID int  
  DECLARE @ChangedFiled nvarchar(4000)  
  SET @Tname = 'CLM_Mandate'  
  DECLARE @Nodeval TABLE (  
    Nodevalue varchar(30) NULL  
  )  
  
 BEGIN  
  
    INSERT INTO CLM_Mandate (ReserveId,ClaimantName, ClaimType, Createdby, Createddate, AccidentId, IsActive, ClaimID,PolicyId,MovementType) OUTPUT INSERTED.MandateId INTO @OutputTbl (ID)  VALUES (@RId,@p_ClaimantName, @p_ClaimType, @p_Createdby, @p_Createddate, @p_AccidentId, @p_IsActive, @p_ClaimID,@p_PolicyId,'I')  
    SELECT @MId = SCOPE_IDENTITY();
    SET @MandateID = (SELECT TOP 1  ID  FROM @OutputTbl  ORDER BY ID)  
  
    SELECT * INTO #mytemptable   FROM CLM_Mandate  WHERE MandateId   = @MandateID  
  
    SET @NewData = (SELECT * FROM #mytemptable AS CLM_Mandate FOR xml AUTO, ELEMENTS)  
  
    INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],[ChangedColumns], [TansDescription], [ClaimID], [EntityCode], [EntityType], [EntityTypeId], [AccidentId]) VALUES (GETDATE(), 'CLM_Mandate', @p_Createdby, 'I', '', @NewData, '', 'Record inserted in Mandate', @p_ClaimID, @MandateID, 'Claims', 'MandateId', @p_AccidentId)  
  
  END  
END


