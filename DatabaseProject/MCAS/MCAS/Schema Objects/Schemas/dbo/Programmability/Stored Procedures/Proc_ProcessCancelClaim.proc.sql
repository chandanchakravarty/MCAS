CREATE PROCEDURE [dbo].[Proc_ProcessCancelClaim]
	-- Add the parameters for the stored procedure here
	@AccidentClaimId INT,
	@LoginUser NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Ret_Result int
	BEGIN TRY
	IF EXISTS (SELECT * FROM CLM_PaymentSummary WHERE AccidentClaimId = @AccidentClaimId AND ApprovePayment = 'Y')
	BEGIN	
          SELECT @Ret_Result = 0
    END
    ELSE BEGIN  
		UPDATE ClaimAccidentDetails SET IsReadOnly = 1 , [status] = 'CAN' WHERE AccidentClaimId = @AccidentClaimId
          
        INSERT INTO ClaimAccidentHistoryDetails ([AccidentClaimId],[ClaimId],[Status],[StatusChangeDate],[CreatedBy],[CreatedDate],[Remarks])  
		VALUES (@AccidentClaimId,null,'CAN',GETDATE(),@LoginUser,GETDATE(),Null)     
		
		SELECT @Ret_Result = 1
    END
    END TRY
    BEGIN CATCH
        SELECT @Ret_Result = 0
    END CATCH  
    SELECT @Ret_Result
END