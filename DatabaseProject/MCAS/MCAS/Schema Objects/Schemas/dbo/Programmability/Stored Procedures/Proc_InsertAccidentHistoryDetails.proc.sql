 CREATE PROCEDURE Proc_InsertAccidentHistoryDetails
 (
  @AccidentClaimId int,
  @ClaimId int,
  @Status nvarchar(10),
  @CreatedBy nvarchar(50),
  @Remarks nvarchar(1000)
 )
 AS 
 BEGIN TRAN
    INSERT INTO ClaimAccidentHistoryDetails(AccidentClaimId,ClaimId,Status,StatusChangeDate,CreatedBy,CreatedDate,Remarks)
    VALUES(@AccidentClaimId,@ClaimId,@Status,GETDATE(),@CreatedBy,GETDATE(),@Remarks)
    IF (@@ERROR <> 0) 
      BEGIN
       PRINT 'Unexpected error occurred!'
        ROLLBACK TRAN
      END
 COMMIT TRAN