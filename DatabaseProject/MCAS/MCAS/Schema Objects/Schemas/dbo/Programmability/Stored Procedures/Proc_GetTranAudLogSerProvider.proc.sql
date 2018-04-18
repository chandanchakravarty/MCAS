CREATE PROCEDURE [dbo].[Proc_GetTranAudLogSerProvider]
	@EntityCode [nvarchar](20),
	@TableName [nvarchar](20)
WITH EXECUTE AS CALLER
AS
BEGIN
  SET FMTONLY OFF;
  SELECT
    m1.[TranAuditId] AS TranAuditId,
    m1.[TimeStamp] AS TimeStamp,
    m2.[TableDesc] AS TableName,
    m1.[UserName] AS UserName,
    m1.[Actions] AS Actions,
    m1.[OldData] AS OldData,
    m1.[NewData] AS NewData,
    m1.[ChangedColumns] AS ChangedColumns,
    m1.[TansDescription] AS TansDescription,
    m1.[ClaimID] AS ClaimID,
    m1.[EntityCode] AS EntityCode,
    m1.[EntityType] AS EntityType,
    m1.[EntityTypeId] AS EntityTypeId
  FROM MNT_TransactionAuditLog m1
  LEFT JOIN MNT_TableDesc m2
    ON m1.TableName = m2.TableName
  WHERE m1.[EntityCode] = @EntityCode
  AND m1.TableName = @TableName
  ORDER BY [TimeStamp] DESC
END


