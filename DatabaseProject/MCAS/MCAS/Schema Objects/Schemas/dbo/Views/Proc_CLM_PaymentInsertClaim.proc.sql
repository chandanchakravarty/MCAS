CREATE PROCEDURE [dbo].[Proc_CLM_PaymentInsertClaim]
	@RId [int],
	@MId [int],
	@p_ClaimantName [nvarchar](max),
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
  DECLARE @PaymentId int  
  DECLARE @ClaimID int  
  DECLARE @ChangedFiled nvarchar(4000)  
  SET @Tname = 'CLM_Payment'  
  DECLARE @Nodeval TABLE (Nodevalue varchar(30) NULL)  
  
  BEGIN  
  
    INSERT INTO CLM_Payment (ReserveId,MandateId,ClaimantName, ClaimType, Createdby, Createddate, AccidentClaimId, IsActive, ClaimID,PolicyId) OUTPUT INSERTED.PaymentId INTO @OutputTbl (ID) VALUES (@RId,@MId, @p_ClaimantName, @p_ClaimType, @p_Createdby, @p_Createddate, @p_AccidentId, @p_IsActive, @p_ClaimID,@p_PolicyId)   
  
    SET @PaymentId = (SELECT TOP 1 ID FROM @OutputTbl ORDER BY ID)  
  
    SELECT * INTO #mytemptable FROM CLM_Payment WHERE PaymentId = @PaymentId  
  
    SET @NewData = (SELECT * FROM #mytemptable AS CLM_Payment FOR xml AUTO, ELEMENTS)  
  
    INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],[ChangedColumns], [TansDescription], [ClaimID], [EntityCode], [EntityType], [EntityTypeId], [AccidentId]) VALUES (GETDATE(), 'CLM_Payment', @p_Createdby, 'I', '', @NewData, '', 'Record inserted in Payment', @p_ClaimID, @PaymentId, 'Claims', 'PaymentId', @p_AccidentId)  
  
  END  
END


