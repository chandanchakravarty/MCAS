CREATE PROCEDURE [dbo].[Proc_InsertTransactionAuditLog]
(
	@tableName NVARCHAR(200),
	@tablePrimaryKey NVARCHAR(200),
	@userName NVARCHAR(200),
	@claimId int = null,
	@accidentId int = null,
	@action NVARCHAR(10),
	@changedColumns NVARCHAR(MAX)	
)
AS
BEGIN
	DECLARE @TableDesc NVARCHAR(800)
	DECLARE @TableType NVARCHAR(80)
	DECLARE @TableDescription NVARCHAR(800)

	SELECT TOP(1) @TableDesc=TableDesc,@TableType=[Type] 
	FROM MNT_TableDesc 
	WHERE TableName=@tableName
	
	IF(ISNULL(@TableDesc,'')<>'')
	BEGIN
		SET @TableDescription='Recored Inserted In '+@TableDesc
	END

	INSERT INTO MNT_TransactionAuditLog
		(
			TimeStamp,
			TableName,
			UserName,
			Actions,			
			ChangedColumns,
			TansDescription,
			ClaimID,			
			EntityType,
			EntityTypeId,
			AccidentId,
			IsValidXml
		)
	VALUES(GETDATE(),@tableName,@userName,@action,@changedColumns,@TableDescription,@claimId,@TableType,@tablePrimaryKey,@accidentId,0)
END