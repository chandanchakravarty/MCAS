CREATE PROCEDURE [dbo].[Proc_CheckUserBranchChanges]
	@UserId [varchar](100),
	@LoginId [varchar](100),
	@BranchCode [varchar](100)
WITH EXECUTE AS CALLER
AS
DECLARE @UserBranch Table
		(
		 UserId		VARCHAR(100),
		 BranchCode VARCHAR(100)
		)

DECLARE @StatusChange VARCHAR(10),@BranchCodeCheck VARCHAR(10)
SET @StatusChange='N'

IF(len(@BranchCode)>0)
BEGIN
SET @BranchCode = LEFT(@BranchCode,len(@BranchCode)-1)
END


INSERT INTO @UserBranch(UserId,BranchCode)
SELECT @UserId as UserId,Item as BranchCode
FROM F_SPLIT(@BranchCode,',')

	
	DECLARE CheckUserBranchChange1 CURSOR FOR
	SELECT BranchCode  FROM  @UserBranch
	WHERE UserId=@UserId 
	OPEN  CheckUserBranchChange1
	FETCH NEXT FROM  CheckUserBranchChange1
	INTO @BranchCodeCheck
	WHILE @@FETCH_STATUS = 0
	BEGIN				
		IF NOT EXISTS(SELECT 1 FROM MNT_UserBranches WHERE UserId	= @UserId and	BranchCode	= @BranchCodeCheck)	
		BEGIN			
			SET @StatusChange='Y'
		END	
	FETCH NEXT FROM  CheckUserBranchChange1
	INTO @BranchCodeCheck
	END
	CLOSE  CheckUserBranchChange1
	DEALLOCATE   CheckUserBranchChange1
	

	DECLARE CheckUserBranchChange CURSOR FOR
	SELECT BranchCode  FROM  MNT_UserBranches
	WHERE UserId=@UserId 
	OPEN  CheckUserBranchChange
	FETCH NEXT FROM  CheckUserBranchChange
	INTO @BranchCodeCheck
	WHILE @@FETCH_STATUS = 0
	BEGIN				
		IF NOT EXISTS(SELECT 1 FROM @UserBranch WHERE UserId	= @UserId and	BranchCode	= @BranchCodeCheck)	
		BEGIN			
			SET @StatusChange='Y'
		END	
	FETCH NEXT FROM  CheckUserBranchChange
	INTO @BranchCodeCheck
	END
	CLOSE  CheckUserBranchChange
	DEALLOCATE   CheckUserBranchChange

SELECT  @StatusChange AS StatusChange


