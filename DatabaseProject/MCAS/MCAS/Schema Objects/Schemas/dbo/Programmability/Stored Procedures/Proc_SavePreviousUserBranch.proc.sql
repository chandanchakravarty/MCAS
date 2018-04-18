﻿CREATE PROCEDURE [dbo].[Proc_SavePreviousUserBranch]
	@UserId [varchar](200),
	@LoginId [varchar](200)
WITH EXECUTE AS CALLER
AS
BEGIN
	Declare @ModifiedDate DateTime,@BranchCode VARCHAR(50),@BranchCodes VARCHAR(MAX),@BranchName VARCHAR(500),@BranchNames VARCHAR(MAX)
	SET @BranchCodes=''
	SET @BranchNames=''
	DECLARE UserBranch_Cursor CURSOR FOR
	SELECT
	ISNULL(UB.BranchCode,'') AS BranchCode,ISNULL(BR.BranchName,'') AS BranchName
	FROM MNT_UserBranches UB, MNT_BranchLogin BR
	WHERE 
	UB.BranchCode=BR.BranchCode and
	UserId=@UserId
	OPEN UserBranch_Cursor
	FETCH NEXT FROM UserBranch_Cursor
	INTO @BranchCode,@BranchName
	WHILE @@FETCH_STATUS = 0
	BEGIN		
		IF(ISNULL(@BranchCode,'')<>'')
		BEGIN
			SET @BranchCodes=@BranchCodes + @BranchCode +','
		END
		IF(ISNULL(@BranchName,'')<>'')
		BEGIN
			SET @BranchNames=@BranchNames + @BranchName +','
		END
	FETCH NEXT FROM UserBranch_Cursor
	INTO @BranchCode,@BranchName
	END
	CLOSE UserBranch_Cursor
	DEALLOCATE UserBranch_Cursor
	SELECT @BranchCodes,@BranchNames
	INSERT INTO UserBranchAuditLog(UserId,PrevBranchCode,PrevBranchName,ModifiedBy,ModifiedDate,Status)
	VALUES(@UserId,@BranchCodes,@BranchNames,@LoginId,getdate(),'N')
END


